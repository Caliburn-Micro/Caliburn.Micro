. .\templates\template-functions.ps1
clear-host

open( "WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" )
[Reflection.Assembly]::LoadFrom( (combine "Templates" "Ionic.Zip.dll") ) | ignore

# files to replace text in
$replace_list = "*.cs","*.xaml","*.xml"

# files to ignore per template
$ignore_for = @{"WP7Template" = @("WindowManager.cs")}

# relevant folder
$template_root = "templates"
$output_folder = "generated-templates"
$source_folder = "src"
$base_framework_template = combine $source_folder "Caliburn.Micro.Silverlight"

write "Cleaning old artifacts",""
del -force -recurse $output_folder -ErrorAction SilentlyContinue
md $output_folder | ignore
 
# | where {$_.Name -eq "SilverlightTemplate"}
dir $template_root `
  | where {$_.psIsContainer -eq $true} `
  | foreach {
    $template = $_
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
        $xpath = ("//*[local-name() = 'Compile' and @Include='Framework\" + $child.Name + "']")
        $d = $proj.SelectNodes($xpath)

        if($d.Count -eq 1) 
        { 
            write ("      " + $child.Name + " already in csproj, skipping... ")
            continue 
        }

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
    $zip = New-Object Ionic.Zip.ZipFile
    $zip_output = combine $output_folder ("Caliburn_Micro_" + $template_name + ".zip")
    $zip.UseUnicodeAsNecessary = $true
    $zip.AddDirectory($folder)
    $zip.Comment = ("This zip was created at " + [System.DateTime]::Now.ToString("G"))
    $zip.Save( $zip_output );

    $zip.Dispose()

    write "__________"
}

"Copying Templates to Visual Studio's project template directory"

$my_docs = [Environment]::GetFolderPath( [Environment+SpecialFolder]::MyDocuments )
$vs_template_path = combine $my_docs "Visual Studio 2010\Templates\ProjectTemplates\Visual C#"
$caliburn_template_path = combine $vs_template_path "Caliburn"

if( [IO.Directory]::Exists($vs_template_path) -eq $false ) {
    "The expected path for Visual Studio 2010 templates was not found:"
    $vs_template_path
    "Please copy the generated templates manually."
} else {
    
    if( [IO.Directory]::Exists($caliburn_template_path) -eq $false ) {
        md $caliburn_template_path | ignore
    }

    foreach($child in (dir $output_folder "*.zip" ))
    {  
        copy $child.FullName $caliburn_template_path
    }
    $caliburn_template_path
}