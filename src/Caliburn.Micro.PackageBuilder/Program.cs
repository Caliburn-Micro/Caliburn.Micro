namespace Caliburn.Micro.PackageBuilder {
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using RazorEngine;

    class Program {
        static void Main(string[] args) {
            var packages = new PackageList();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var packagesPath = Path.Combine(baseDirectory, "Packages");

            if(!Directory.Exists(packagesPath)) {
                Directory.CreateDirectory(packagesPath);
            }
            else {
                Directory.Delete(packagesPath, true);
            }

            foreach(var packageModel in packages) {
                var path = Path.Combine(packagesPath, packageModel.Id);
                Directory.CreateDirectory(path);

                RenderManifest(packageModel, path);

                if(packageModel.Content.Count > 0) {
                    var contentPath = Path.Combine(path, "content/Caliburn.Micro");
                    Directory.CreateDirectory(contentPath);

                    foreach(var source in packageModel.Content) {
                        var fileName = Path.GetFileName(source);
                        var sourceFileName = ResolveRelativePath(baseDirectory, source);
                        var destFileName = Path.Combine(contentPath, fileName);

                        File.Copy(sourceFileName, destFileName);
                    }
                }

                var nugetPath = ResolveRelativePath(baseDirectory, "../../../../nuget/nuget.exe");
                var localNuget = Path.Combine(path, "nuget.exe");
                File.Copy(nugetPath, localNuget);

                var proc = new Process {
                    StartInfo = new ProcessStartInfo(localNuget) {
                        WorkingDirectory = path,
                        Arguments = "pack " + packageModel.Id + ".nuspec"
                    }
                };
                
                proc.Start();
            }
        }

        static void RenderManifest(PackageModel model, string ouputFolder) {
            var manifestName = Path.Combine(ouputFolder, model.Id + ".nuspec").ToLower();

            using(var stream = File.OpenWrite(manifestName))
            using(var writer = new StreamWriter(stream))
            using(var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Caliburn.Micro.PackageBuilder.ManifestTemplate.cshtml"))) {
                var template = reader.ReadToEnd();
                var ouput = Razor.Parse(template, model);

                writer.Write(ouput);
            }
        }

        public static string ResolveRelativePath(string referencePath, string relativePath) {
            return Path.GetFullPath(Path.Combine(referencePath, relativePath));
        }
    }
}