using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Markup;

[assembly: AssemblyTitle("Caliburn.Micro.Extensions")]
[assembly: AssemblyDescription("Extensions to the core Caliburn.Micro framework.")]
[assembly: AssemblyProduct("Caliburn.Micro.Extensions")]
[assembly: AssemblyCompany("Blue Spire Consulting, Inc.")]
[assembly: AssemblyCopyright("Copyright © 2010")]
[assembly: ComVisible(false)]
[assembly: Guid("76ae9648-d7f5-495b-b59f-b3bc2bd0a579")]
[assembly: AssemblyVersion("1.2.0.0")]
[assembly: AssemblyFileVersion("1.2.0.0")]

[assembly: XmlnsDefinition("http://www.caliburnproject.org", "Caliburn.Micro")]
[assembly: XmlnsPrefix("http://www.caliburnproject.org", "cal")]

#if !SILVERLIGHT
[assembly: CLSCompliant(true)]
#endif