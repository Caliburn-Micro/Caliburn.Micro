namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;

    /// <summary>
    ///   A strategy for determining which view model to use for a given view.
    /// </summary>
    public static class ViewModelLocator {
        ///<summary>
        /// Used to transform names.
        ///</summary>
        public static readonly NameTransformer NameTransformer = new NameTransformer();

        static ViewModelLocator() {
            //Add to list by increasing order of specificity (i.e. less specific pattern to more specific pattern)

            //Check for <Namespace>.<BaseName>View construct
            NameTransformer.AddRule
                (
                    @"(?<namespace>(.*\.)*)(?<basename>[A-Za-z]\w*)(?<suffix>View$)",
                    new[] {
                        @"${namespace}${basename}ViewModel",
                        @"${namespace}${basename}",
                        @"${namespace}I${basename}ViewModel",
                        @"${namespace}I${basename}"
                    },
                    @"(.*\.)*[A-Za-z]\w*View$"
                );

            //Check for <Namespace>.<BaseName>Page construct
            //Add "View" synonyms below: (?<namespace>(.*\.)*)(?<basename>[A-Za-z]\w*)(?<suffix>(Page$)|(Form$)|(Screen$))"
            NameTransformer.AddRule
                (
                    @"(?<namespace>(.*\.)*)(?<basename>[A-Za-z]\w*)(?<suffix>Page$)",
                    new[] {
                        @"${namespace}${basename}${suffix}ViewModel",
                        @"${namespace}I${basename}${suffix}ViewModel"
                    },
                    @"(.*\.)*[A-Za-z]\w*Page$"
                );

            //Check for <Namespace>.Views.<BaseName>View construct
            NameTransformer.AddRule
                (
                    @"(?<namespace>(.*\.)*)Views\.(?<basename>[A-Za-z]\w*)(?<suffix>View$)",
                    new[] {
                        @"${namespace}ViewModels.${basename}ViewModel",
                        @"${namespace}ViewModels.${basename}",
                        @"${namespace}ViewModels.I${basename}ViewModel",
                        @"${namespace}ViewModels.I${basename}"
                    },
                    @"(.*\.)*Views\.[A-Za-z]\w*View$"
                );

            //Check for <Namespace>.Views.<BaseName><ViewSynonym> construct
            //Add "View" synonyms below: (?<namespace>(.*\.)*)Views\.(?<basename>[A-Za-z]\w*)(?<suffix>(Page$)|(Form$)|(Screen$))"
            NameTransformer.AddRule
                (
                    @"(?<namespace>(.*\.)*)Views\.(?<basename>[A-Za-z]\w*)(?<suffix>Page$)",
                    new[] {
                        @"${namespace}ViewModels.${basename}${suffix}ViewModel",
                        @"${namespace}ViewModels.I${basename}${suffix}ViewModel"
                    },
                    @"(.*\.)*Views\.[A-Za-z]\w*Page$"
                );
        }

        /// <summary>
        ///   Makes a type name into an interface name.
        /// </summary>
        /// <param name = "typeName">The part.</param>
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
        ///   Determines the view model type based on the specified view type.
        /// </summary>
        /// <returns>The view model type.</returns>
        /// <remarks>
        ///   Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.
        /// </remarks>
        public static Func<Type, bool, Type> LocateTypeForViewType = (viewType, searchForInterface) => {
            var typeName = viewType.FullName;

            Func<string, string> getReplaceString;
            if(searchForInterface) {
                getReplaceString = r => { return r; };
            }
            else {
                getReplaceString = r => {
                    return Regex.IsMatch(r, @"I\${basename}") ? String.Empty : r;
                };
            }

            var viewModelTypeList = NameTransformer.Transform(typeName, getReplaceString);
            var viewModelType = (from assembly in AssemblySource.Instance
                                 from type in assembly.GetExportedTypes()
                                 where viewModelTypeList.Contains(type.FullName)
                                 select type).FirstOrDefault();


            return viewModelType;
        };

        /// <summary>
        ///   Locates the view model for the specified view type.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>
        ///   Pass the view type as a parameter and receive a view model instance.
        /// </remarks>
        public static Func<Type, object> LocateForViewType = viewType => {
            var viewModelType = LocateTypeForViewType(viewType, false);

            if(viewModelType != null) {
                var viewModel = IoC.GetInstance(viewModelType, null);
                if(viewModel != null) {
                    return viewModel;
                }
            }

            viewModelType = LocateTypeForViewType(viewType, true);
            return IoC.GetInstance(viewModelType, null);
        };

        /// <summary>
        ///   Locates the view model for the specified view instance.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>
        ///   Pass the view instance as a parameters and receive a view model instance.
        /// </remarks>
        public static Func<object, object> LocateForView = view => {
            if(view == null)
                return null;

            var frameworkElement = view as FrameworkElement;
            if(frameworkElement != null && frameworkElement.DataContext != null)
                return frameworkElement.DataContext;

            return LocateForViewType(view.GetType());
        };
    }
}