$files = @(Get-ChildItem ..\samples\setup\**\*.nuspec)

foreach ($file in $files)
{
    nuget pack $file -OutputDirectory ..\templates
}