@ECHO OFF
del *.nupkg
.\nuget.exe pack .\Caliburn.Micro.Core.nuspec
.\nuget.exe pack .\Caliburn.Micro.nuspec
.\nuget.exe pack .\Caliburn.Micro.Start.nuspec
