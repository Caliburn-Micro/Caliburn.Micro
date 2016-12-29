using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms
{
    /// <summary>
    /// A DataTemplateSelector that selects the DataTemplate based on the viewModel
    /// </summary>
    public class ContextAwareDataTemplateSelector : DataTemplateSelector
    {
        private readonly Dictionary<Type, DataTemplate> selectorDict;

        /// <summary>
        /// Creates a new ContextAwareDataTemplateSelector
        /// </summary>
        public ContextAwareDataTemplateSelector()
        {
            selectorDict = new Dictionary<Type, DataTemplate>();
        }

        /// <summary>
        /// Determine which DataTemplate to return based on the viewModel, 
        /// construct the View and wire them together
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Type vmType = item.GetType();

            DataTemplate dataTemplate;

            // Check if DataTemplate for given type has already been created
            if (!selectorDict.TryGetValue(vmType, out dataTemplate))
            {
                Type viewType = ViewLocator.LocateTypeForModelType(vmType, null, null);

                dataTemplate = new DataTemplate(() =>
                {
                    // Create the page
                    var view = Activator.CreateInstance(viewType);

                    var bindableObject = view as BindableObject;

                    if (bindableObject != null)
                    {
                        // Once the binding context changes attach view to viewModel
                        bindableObject.BindingContextChanged += (sender, args) =>
                        {
                            var page = sender as Page;

                            var viewAware = page?.BindingContext as IViewAware;

                            viewAware?.AttachView(page);
                        };
                    }

                    return view;
                });

                // Cache created DataTemplate
                selectorDict.Add(vmType, dataTemplate);
            }

            return dataTemplate;
        }
    }
}
