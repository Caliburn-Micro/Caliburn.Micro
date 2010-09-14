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

function copy-template ( $source, $destination, $exclude)
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
	    if($exclude -eq $false) {
		copy $source_path -destination (combine $destination $child.Name) -recurse -force -exclude $ignore
		}
        } else {        
            $content = get-content -path $source_path
            $content | foreach {$_ -replace "Caliburn.Micro", '$safeprojectname$'} | Set-Content (combine $destination $child.Name)      
        }      
    }
}

clear-host

$template_root = "templates"
$output_folder = "generated-templates"
$source_folder = "src"
$base_framework_template = combine $source_folder "Caliburn.Micro.Silverlight"

write "cleaning old artifacts"

del -force -recurse $output_folder
$templates = dir $template_root | where {$_.psIsContainer -eq $true}
md $output_folder

foreach ($template in $templates)
{
    $template_name = $template.Name.Replace("Template","")
    $framework_template = combine $source_folder ("Caliburn.Micro." + $template_name )
    $folder = combine $output_folder $template_name 
    $framework_folder = combine $folder "Framework"

    md $folder
    md $framework_folder
    
    write "", "__________", ("Creating Template for " + $template_name)
    
    copy-template (get-item $base_framework_template) $framework_folder true
    copy-template (get-item $framework_template) $framework_folder true
    
    copy-template $template $folder false

    #add framework items to project file
    $csproj_path = (combine $folder ($template_name + "Template.csproj"))
    $proj = New-Object xml
    $proj.Load( $csproj_path )
    $ns = $proj.project.NamespaceURI
    $itemgroup = $proj.CreateElement("ItemGroup", $ns)

    $proj.project.AppendChild($itemgroup)

    foreach($child in $framework_folder | dir)
    {
	$compile = $proj.CreateElement("Compile", $ns)
	$compile.SetAttribute("Include",("Framework\" + $child.Name))
	$itemgroup.AppendChild($compile)
    }

    $proj.Save($csproj_path)

    write "__________", " ", " "
}