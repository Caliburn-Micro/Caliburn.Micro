using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Markup;

[assembly: AssemblyTitle("Caliburn Micro")]
[assembly: AssemblyDescription("A small, yet extremely powerful implementation of Caliburn.")]
[assembly: AssemblyProduct("Caliburn.Micro")]
[assembly: AssemblyCompany("Blue Spire Consulting, Inc.")]
[assembly: AssemblyCopyright("Copyright © 2010")]
[assembly: ComVisible(false)]
[assembly: Guid("6449e9cb-4d4d-4d79-8f73-71a2fc647109")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: XmlnsDefinition("http://www.caliburnproject.org", "Caliburn.Micro")]
[assembly: XmlnsPrefix("http://www.caliburnproject.org", "cal")]

#if !SILVERLIGHT
[assembly: CLSCompliant(true)]
#endif