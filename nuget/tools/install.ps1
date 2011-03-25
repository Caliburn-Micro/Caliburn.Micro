param($rootPath, $toolsPath, $package, $project)

function get-content-source-from-frameworkname($frameworkname) {
  $id = $frameworkname.Identifier

  if($id -eq ".NETFramework") { return "NET40" }
  if($id -eq "Silverlight" -and $frameworkname -eq "WindowsPhone") { return "SL40-WindowsPhone" }
  if($id -eq "Silverlight" ) { return "SL40" }
}
function Get-ScriptDirectory {
  $Invoc = (Get-Variable MyInvocation -Scope 1).Value
  Split-Path $Invoc.MyCommand.Path
}
 
  $moniker = $project.Properties.Item("TargetFrameworkMoniker").Value
 
  $frameworkname = new-object System.Runtime.Versioning.FrameworkName($moniker)
 
  $contentSource = get-content-source-from-frameworkname $frameworkname
  $root = get-scriptdirectory

  $contentPath = [System.IO.Path]::Combine($root, $contentSource)

  ls $contentPath | foreach-object { $project.ProjectItems.AddFromFileCopy($_.FullName) }

  write-host "Source: " $contentSource  
  write-host "Identifier: " $frameworkname.Identifier  
  write-host "Version: " $frameworkname.Version 
  write-host "Profile: " $frameworkname.Profile

 
# WPF4 will be NETFramework,Version=v4.0,Profile=Client 
# SL4 will be Silverlight,Version=v4.0 
# WP7 will be Silverlight,Version=v4.0,Profile=WindowsPhone

