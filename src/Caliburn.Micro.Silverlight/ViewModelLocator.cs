namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// A strategy for determining which view model to use for a given view.
    /// </summary>
    public static class ViewModelLocator {

        private static NameResolutionManager _NameResolutionManager = new NameResolutionManager();
        static ViewModelLocator()
        {
            //Add to list by increasing order of specificity (i.e. less specific pattern to more specific pattern)

            _NameResolutionManager.AddTransformConvention
            (
                "(?<basename>.*)(?<suffix>(View$))", 
                new string[] {"${basename}ViewModel", "${basename}", "I${basename}ViewModel", "I${basename}"}
            );

            //Add "View" synonyms below: (?<basename>.*)(?<suffix>(Page$)|(Form$)|(Screen$))
            _NameResolutionManager.AddTransformConvention
            (
                "(?<basename>.*)(?<suffix>(Page$))",
                new string[] { "${basename}${suffix}ViewModel", "I${basename}${suffix}ViewModel"}
            );
        }

        /// <summary>
        /// Get the static instance of the NameResolutionManager that ViewModelLocator uses for resolving ViewModel names.
        /// </summary>
        /// <returns>The NameResolutionManager for resolving ViewModel names</returns>
        public static NameResolutionManager GetViewModelResolutionManager()
        {
            return _NameResolutionManager;
        }

        /// <summary>
        /// Makes a type name into an interface name.
        /// </summary>
        /// <param name="typeName">The part.</param>
        /// <returns></returns>
        public static string MakeInterface(string typeName) {
            var suffix = string.Empty;
            if(typeName.Contains("[[")) {
                //generic type
                var genericParStart = typeName.IndexOf("[[");
                suffix = typeName.Substring(genericParStart);
                typeName = typeName.Remove(genericParStart);
            }

            var index = typeName.LastIndexOf(".");
            return typeName.Insert(index + 1, "I") + suffix;
        }

        /// <summary>
        /// Determines the view model type based on the specified view type.
        /// </summary>
        /// <returns>The view model type.</returns>
        /// <remarks>Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.</remarks>
        public static Func<Type, bool, Type> LocateTypeForViewType = (viewType, searchForInterface) => {
            var typeName = viewType.FullName; //ex. ShellView

            Func<string, string> funcGetReplaceStr;
            if (searchForInterface)
            {
                funcGetReplaceStr = (r) => { return r; };
            }
            else
            {
                funcGetReplaceStr = (r) =>
                {
                    if (r.StartsWith("I$")) //It's an interface transform so make it something impossible to exist
                    {
                        return String.Empty;
                    }
                    return r;
                };
            }
            
            var viewModelTypeList = _NameResolutionManager.GetResolvedNameList(typeName, funcGetReplaceStr);

            var viewModelType = (from assembly in AssemblySource.Instance
                            from type in assembly.GetExportedTypes()
                            where viewModelTypeList.Contains(type.FullName)
                            select type).FirstOrDefault();


            return viewModelType;
        };

        /// <summary>
        /// Locates the view model for the specified view type.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>Pass the view type as a parameter and receive a view model instance.</remarks>
        public static Func<Type, object> LocateForViewType = viewType => {
            var viewModelType = LocateTypeForViewType(viewType, false);

            if (viewModelType != null) {
                var viewModel = IoC.GetInstance(viewModelType, null);
                if(viewModel != null) {
                    return viewModel;
                }
            }

            viewModelType = LocateTypeForViewType(viewType, true);
            return IoC.GetInstance(viewModelType, null);
        };

        /// <summary>
        /// Locates the view model for the specified view instance.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>Pass the view instance as a parameters and receive a view model instance.</remarks>
        public static Func<object, object> LocateForView = view =>{
            if(view == null)
                return null;

            var frameworkElement = view as FrameworkElement;
            if(frameworkElement != null && frameworkElement.DataContext != null)
                return frameworkElement.DataContext;

            return LocateForViewType(view.GetType());
        };
    }
}