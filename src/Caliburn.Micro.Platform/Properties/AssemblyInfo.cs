using System;
using System.Reflection;
using System.Runtime.InteropServices;
#if NET || SILVERLIGHT
using System.Windows.Markup;
#endif

[assembly: AssemblyTitle("Caliburn Micro Platform")]
[assembly: AssemblyDescription("A small, yet powerful framework designed for Xaml platforms, Caliburn.Micro implements a variety of UI patterns for solving real-world problems. Patterns that are highlighted include MVVM (Presentation Model), MVP and MVC.")]

#if NET || SILVERLIGHT
[assembly: CLSCompliant(true)]
#endif

[assembly: ComVisible(false)]

#if NET || SILVERLIGHT
[assembly: Guid("6449e9cb-4d4d-4d79-8f73-71a2fc647109")]
[assembly: XmlnsDefinition("http://www.caliburnproject.org", "Caliburn.Micro")]
[assembly: XmlnsPrefix("http://www.caliburnproject.org", "cal")]
#endif
