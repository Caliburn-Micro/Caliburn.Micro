function ignore( $stuff ){ $stuff | Out-Null }

function open( $assembly )
{
    if ( $null -eq ([AppDomain]::CurrentDomain.GetAssemblies() |? { $_.FullName -eq $assembly }) )
    {
        [System.Reflection.Assembly]::Load($assembly) | ignore
    }
}

function combine([string]$path1,[string]$path2){
    $root = get-location
    $part1 = [System.IO.Path]::Combine($root,$path1)
    $part2 = [System.IO.Path]::Combine($part1,$path2)
    $part2
}

function match($candidate, $patterns)
{
    $result = $false
    foreach($pattern in $patterns)
    {
        if($candidate -like $pattern) 
        {
            $result = $true
            break
        }
    }
    
    $result
}

function copy-template ( $source, $destination, $exclude, $template_name)
{
    $ignore = "*.user","bin","obj"
    if($exclude -eq $true) { $ignore += ,"*.csproj" }

    write ("copy from " + $source.Name +  " >> "), ("    " + $destination)
    
    foreach($child in $source | dir)
    {      
        if( match $child.Name $ignore)
        {
            continue
        }
        
        $source_path = combine $source.FullName $child.Name
        
        if($child.GetType().Name -eq "DirectoryInfo")
        {
            if($exclude -eq $false)
            {
                $sub = combine $destination $child.Name
                if( (test-path $sub -pathType container) -eq $false) { md $sub | ignore }
                
                copy-template $child $sub $exclude $template_name
            }
        } else {        
            $content = get-content -path $source_path

            if($child.Name -eq "MyTemplate.vstemplate") 
            { 
                $content | Set-Content (combine $destination $child.Name)

            } else {
                $content | foreach {$_ -replace "Caliburn.Micro", '$safeprojectname$.Framework' -replace $template_name, '$safeprojectname$' } | Set-Content (combine $destination $child.Name)
            }
        }      
    }
}

function copy-stream($source, $dest)
{
    $maxBufferSize = 4096
    $bufferSize  = 0
    if($source.Length -le $maxBufferSize) 
    {
        $bufferSize = $source.Length 
    } else {
        $bufferSize = $maxBufferSize
    }

    $buffer = New-Object byte[] $bufferSize
    $bytesRead = 0
    $bytesWritten = 0

    while( ($bytesRead = $source.Read($buffer, 0, $buffer.Length)) -ne 0 )
    {
        $dest.Write($buffer, 0, $bytesRead);
        $bytesWritten += $bufferSize;
    }
}

function create-zip-part( $zip, $fileName )
{
    $uri = New-Object Uri($fileName, [System.UriKind]::Relative)
    $helper = [System.IO.Packaging.PackUriHelper]::CreatePartUri( $uri )

    if($fileName.EndsWith(".ico") -ne $true) 
    {
        $zip.CreatePart($helper, "", [System.IO.Packaging.CompressionOption]::Normal)
    } else {
        $zip.CreatePart($helper, "image/jpeg")
    }    
}

function add-to-zip( $zip, $root, $item )
{
    if($item.GetType().Name -ne "DirectoryInfo")
    {
        $relativeFileName = $item.FullName.Replace($root + "\","")
            write ($relativeFileName)
        $part = create-zip-part $zip $relativeFileName

        $stream = New-Object System.IO.FileStream( $item.FullName, [System.IO.FileMode]::Open, [System.IO.FileAccess]::Read )
        $dest = $part.GetStream()
        copy-stream $stream $dest

        $stream.Close()
        $dest.Close()
    } else {
        foreach($child in $item.GetFileSystemInfos())
        {
            add-to-zip $zip $folder $child
        }
    }
}

function build-template-xml($doc, $root, $folder)
{
    $ns = $vstemplate.VSTemplate.NamespaceURI

    foreach($child in $folder | dir)
    {      
        if($child.Name -eq "__TemplateIcon.ico") { continue }
        if($child.Name -eq "MyTemplate.vstemplate") { continue }

        if($child -is [System.IO.DirectoryInfo])
        {
            $folder =  $doc.CreateElement("Folder", $ns)            
            $folder.SetAttribute("Name", $child.Name)            
            $folder.SetAttribute("TargetFolderName", $child.Name)
            build-template-xml $doc $folder $child.FullName
            $root.AppendChild( $folder ) | ignore

        } else {
            $projectitem = $doc.CreateElement("ProjectItem", $ns)
            $projectitem.SetAttribute("ReplaceParameters","true")
            $projectitem.SetAttribute("TargetFileName", $child.Name)
            $projectitem.InnerText =  $child.Name

            $root.AppendChild( $projectitem ) | ignore
        }
    }
}

open("WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")

clear-host

$template_root = "templates"
$output_folder = "generated-templates"
$source_folder = "src"
$base_framework_template = combine $source_folder "Caliburn.Micro.Silverlight"

write "Cleaning old artifacts",""

del -force -recurse $output_folder
$templates = dir $template_root | where {$_.psIsContainer -eq $true}
md $output_folder | ignore

foreach ($template in $templates) # | where {$_.Name -eq "SilverlightTemplate"}
{
    $template_name = $template.Name.Replace("Template","")
    write ("Creating Template for " + $template_name)
    
    $framework_template = combine $source_folder ("Caliburn.Micro." + $template_name )
    $folder = combine $output_folder $template_name 
    $framework_folder = combine $folder "Framework"

    md $folder | ignore
    md $framework_folder | ignore
    
    write "... copying template files"
    copy-template (get-item $base_framework_template) $framework_folder true $template.Name
    copy-template (get-item $framework_template) $framework_folder true $template.Name
    
    copy-template $template $folder false $template.Name

    write "... adding framework items to csproj"
    $csproj_path = (combine $folder ($template_name + "Template.csproj"))
    $proj = New-Object xml
    $proj.Load( $csproj_path )
    $ns = $proj.project.NamespaceURI
    $itemgroup = $proj.CreateElement("ItemGroup", $ns)

    $proj.project.AppendChild($itemgroup) | ignore

    foreach($child in $framework_folder | dir)
    {
        $compile = $proj.CreateElement("Compile", $ns)
        $compile.SetAttribute("Include",("Framework\" + $child.Name))
        $itemgroup.AppendChild($compile) | ignore
    }

    $proj.Save($csproj_path)

    write "... building vstemplate"
    $vstemplate_path = combine $folder "MyTemplate.vstemplate"
    $vstemplate = New-Object xml
    $vstemplate.Load( $vstemplate_path )

    $root = $vstemplate.VSTemplate.TemplateContent.Project

    build-template-xml $vstemplate $root $folder    

    $vstemplate.Save($vstemplate_path)

    write "... zipping template directory"
    $zipPath = combine $output_folder ("Caliburn Mirco for " + $template_name + ".zip")
    $zip = [System.IO.Packaging.Package]::Open($zipPath,[System.IO.FileMode]::Create)

    foreach($child in $folder | dir)
    {      
        add-to-zip $zip $folder $child
    }

    $zip.Close()

    write "__________"
}