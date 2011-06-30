namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;

    /// <summary>
    ///   A strategy for determining which view model to use for a given view.
    /// </summary>
    public static class ViewModelLocator {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewModelLocator));
        ///<summary>
        /// Used to transform names.
        ///</summary>
        public static readonly NameTransformer NameTransformer = new NameTransformer();

        static ViewModelLocator() {
            //Add to list by increasing order of specificity (i.e. less specific pattern to more specific pattern)

            NameTransformer.AddRule(
                @"(?<fullname>^.*$)",
                @"${fullname}Model"
                );

            //Check for <Namespace>.<BaseName>View construct
            NameTransformer.AddRule
                (
                    @"(?<namespace>(.*\.)*)(?<basename>[A-Za-z_]\w*)(?<suffix>View$)",
                    new[] {
                        @"${namespace}${basename}ViewModel",
                        @"${namespace}${basename}",
                        @"${namespace}I${basename}ViewModel",
                        @"${namespace}I${basename}"
                    },
                    @"(.*\.)*[A-Za-z_]\w*View$"
                );

            //Check for <Namespace>.<BaseName>Page construct
            //Add "View" synonyms below: (?<namespace>(.*\.)*)(?<basename>[A-Za-z_]\w*)(?<suffix>(Page$)|(Form$)|(Screen$))"
            NameTransformer.AddRule
                (
                    @"(?<namespace>(.*\.)*)(?<basename>[A-Za-z_]\w*)(?<suffix>Page$)",
                    new[] {
                        @"${namespace}${basename}${suffix}ViewModel",
                        @"${namespace}I${basename}${suffix}ViewModel"
                    },
                    @"(.*\.)*[A-Za-z_]\w*Page$"
                );

            //Check for <Namespace>.Views.<BaseName>View construct
            NameTransformer.AddRule
                (
                    @"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsview>Views\.)(?<nsafter>[A-Za-z_]\w*\.)*(?<basename>[A-Za-z_]\w*)(?<suffix>View$)",
                    new[] {
                        @"${nsbefore}ViewModels.${nsafter}${basename}ViewModel",
                        @"${nsbefore}ViewModels.${nsafter}${basename}",
                        @"${nsbefore}ViewModels.${nsafter}I${basename}ViewModel",
                        @"${nsbefore}ViewModels.${nsafter}I${basename}"
                    },
                    @"(([A-Za-z_]\w*\.)*)?Views\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*View$"
                );

            //Check for <Namespace>.Views.<BaseName><ViewSynonym> construct
            //Add "View" synonyms below: (?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsview>Views\.)(?<nsafter>[A-Za-z_]\w*\.)*(?<basename>[A-Za-z_]\w*)(?<suffix>(Page$)|(Form$)|(Screen$))"
            NameTransformer.AddRule
                (
                    @"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsview>Views\.)(?<nsafter>[A-Za-z_]\w*\.)*(?<basename>[A-Za-z_]\w*)(?<suffix>Page$)",
                    new[] {
                        @"${nsbefore}ViewModels.${nsafter}${basename}${suffix}ViewModel",
                        @"${nsbefore}ViewModels.${nsafter}I${basename}${suffix}ViewModel"
                    },
                    @"(([A-Za-z_]\w*\.)*)?Views\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*Page$"
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

            if (viewModelType == null) {
                Log.Warn("View Model not found. Searched: {0}.", string.Join(", ", viewModelTypeList.ToArray()));
            }

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

            return viewModelType != null
                       ? IoC.GetInstance(viewModelType, null)
                       : null;
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