param($rootPath, $toolsPath, $package, $project)

function Get-ScriptDirectory {
  $Invoc = (Get-Variable MyInvocation -Scope 1).Value
  Split-Path $Invoc.MyCommand.Path
}

function get-content-path($contentRoot) {
  $moniker = $project.Properties.Item("TargetFrameworkMoniker").Value
  $frameworkname = new-object System.Runtime.Versioning.FrameworkName($moniker)

  $id = $frameworkname.Identifier

  write-host $frameworkname.Identifier
  write-host $frameworkname.Profile

  if($id -eq ".NETFramework") { $relative = "NET40" }
  elseif($id -eq "Silverlight" -and $frameworkname.Profile -eq "WindowsPhone") { $relative = "SL40-WindowsPhone" }
  elseif($id -eq "Silverlight" ) { $relative = "SL40" }
 
  [System.IO.Path]::Combine($contentRoot, $relative)
}
 
  $contentSource = get-content-path ($rootPath + "\tools")
  $defaultNamespace = ""
  
  $project.Properties | foreach-object { 
    if($_.Name -eq "DefaultNamespace"){
    	$defaultNamespace = $_.Value
    }
  }

  ls $contentSource | foreach-object { 
  	$content = [System.IO.File]::ReadAllText($_.FullName)
  	$content = $content.Replace("safeprojectname", $defaultNamespace)
  	$content | out-file $_.FullName
  	$project.ProjectItems.AddFromFileCopy($_.FullName)
  }

  write-host "Added Bootstrapper and Shell.  You will want to update the your App.xaml as described here: http://caliburnmicro.codeplex.com/wikipage?title=Basic%20Configuration%2c%20Actions%20and%20Conventions&referringTitle=Documentation"
 
# WPF4 will be NETFramework,Version=v4.0,Profile=Client 
# SL4 will be Silverlight,Version=v4.0 
# WP7 will be Silverlight,Version=v4.0,Profile=WindowsPhone
