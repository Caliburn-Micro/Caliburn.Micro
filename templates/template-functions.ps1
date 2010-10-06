function ignore( $stuff ){ $stuff | Out-Null }

function open( $assembly )
{
    if ( $null -eq ([AppDomain]::CurrentDomain.GetAssemblies() |? { $_.FullName -eq $assembly }) )
    {
        [Reflection.Assembly]::Load($assembly) | ignore
    }
}

function combine([string]$path1,[string]$path2){
    $root = get-location
    $part1 = [IO.Path]::Combine($root,$path1)
    $part2 = [IO.Path]::Combine($part1,$path2)
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
    $ignore = "*.user","bin","obj","_ReSharper.*"
    if($exclude -eq $true) { $ignore += ,"*.csproj" }

    write ("... copying from " + $source.Name)
    
    foreach($child in $source | dir)
    {      
        if( match $child.Name $ignore) { continue }

        if( match $child.Name $ignore_for[$template_name]) 
        { 
            write ("      ignoring " + $child.Name)
            continue
        }
        
        $source_path = combine $source.FullName $child.Name
        
        if($child -is [IO.DirectoryInfo])
        {
            if($exclude -eq $false)
            {
                $sub = combine $destination $child.Name
                if( (test-path $sub -pathType container) -eq $false) { md $sub | ignore }
                
                copy-template $child $sub $exclude $template_name
            }
        } else {        
            $content = get-content -path $source_path

            if( match $child.Name $replace_list )
            {                	
                $content | foreach {$_ -replace "assembly=Caliburn.Micro", 'assembly=$safeprojectname$' -replace "Caliburn.Micro", '$safeprojectname$.Framework' -replace $template_name, '$safeprojectname$' -replace "00000000-0000-0000-0000-000000000001",'$guid1$' } | Set-Content (combine $destination $child.Name)
            } else {
                copy $child.FullName $destination
            }
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

        if($child -is [IO.DirectoryInfo])
        {
            $folder =  $doc.CreateElement("Folder", $ns)            
            $folder.SetAttribute("Name", $child.Name)            
            $folder.SetAttribute("TargetFolderName", $child.Name)
            build-template-xml $doc $folder $child.FullName
            $root.AppendChild( $folder ) | ignore

        } else {
            $shouldReplace = "false"
            if( match $child.Name $replace_list ) { $shouldReplace = "true" }
            $projectitem = $doc.CreateElement("ProjectItem", $ns)
            $projectitem.SetAttribute("ReplaceParameters",$shouldReplace)
            $projectitem.SetAttribute("TargetFileName", $child.Name)
            $projectitem.InnerText =  $child.Name

            if($child.Name -eq "readme.txt")
            {
                $projectitem.SetAttribute("OpenInEditor","true")
            }

            $root.AppendChild( $projectitem ) | ignore
        }
    }
}
