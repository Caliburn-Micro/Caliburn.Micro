param($rootPath, $toolsPath, $package, $project)

function Get-ScriptDirectory {
  $Invoc = (Get-Variable MyInvocation -Scope 1).Value
  Split-Path $Invoc.MyCommand.Path
}

function get-content-path() {
  $moniker = $project.Properties.Item("TargetFrameworkMoniker").Value
  $frameworkname = new-object System.Runtime.Versioning.FrameworkName($moniker)

  $id = $frameworkname.Identifier

  if($id -eq ".NETFramework") { $relative = "NET40" }
  if($id -eq "Silverlight" -and $frameworkname -eq "WindowsPhone") { $relative = "SL40-WindowsPhone" }
  if($id -eq "Silverlight" ) { $relative = "SL40" }
 
  [System.IO.Path]::Combine($root, $relative)
}
 
  $contentSource = get-content-path

  ls $contentSource | foreach-object { $project.ProjectItems.AddFromFileCopy($_.FullName) }

  write-host "Added Bootstrapper.cs.  You'll want to update the namespace and add this to your App.xaml as described here: http://caliburnmicro.codeplex.com/wikipage?title=Basic%20Configuration%2c%20Actions%20and%20Conventions&referringTitle=Documentation"

 
# WPF4 will be NETFramework,Version=v4.0,Profile=Client 
# SL4 will be Silverlight,Version=v4.0 
# WP7 will be Silverlight,Version=v4.0,Profile=WindowsPhone

