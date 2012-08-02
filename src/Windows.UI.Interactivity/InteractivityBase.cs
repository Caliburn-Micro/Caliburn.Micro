using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Windows.UI.Interactivity
{
    public abstract class InteractivityBase : FrameworkElement, IAttachedObject
    {
        private FrameworkElement associatedObject;
        private Type associatedObjectTypeConstraint;

        /// <summary>
        /// Gets the object to which this behavior is attached.
        /// 
        /// </summary>
        protected FrameworkElement AssociatedObject
        {
            get { return this.associatedObject; }
            set { this.associatedObject = value; }
        }

        FrameworkElement IAttachedObject.AssociatedObject
        {
            get { return this.AssociatedObject; }
        }

        internal event EventHandler AssociatedObjectChanged;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected virtual void OnDetaching()
        {
        }

        protected void OnAssociatedObjectChanged()
        {
            if (this.AssociatedObjectChanged == null)
            {
                return;
            }
            this.AssociatedObjectChanged(this, new EventArgs());
        }
        
        /// <summary>
        /// Gets the type constraint of the associated object.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The associated object type constraint.
        /// </value>
        protected virtual Type AssociatedObjectTypeConstraint
        {
            get { return this.associatedObjectTypeConstraint; }
            set { this.associatedObjectTypeConstraint = value; }
        }

        public abstract void Attach(FrameworkElement frameworkElement);

        public abstract void Detach();

        /// <summary>
        /// Configures data context. 
        /// Courtesy of Filip Skakun
        /// http://twitter.com/xyzzer
        /// </summary>
        protected async void ConfigureDataContext()
        {
            while (associatedObject != null)
            {
                if (AssociatedObjectIsInVisualTree)
                {
                    SetBinding(DataContextProperty,
                        new Binding
                        {
                            Path = new PropertyPath("DataContext"),
                            Source = associatedObject
                        }
                    );
                    return;
                }

                await WaitForLayoutUpdateAsync();
            }
        }

        /// <summary>
        /// Checks if object is in visual tree
        /// Courtesy of Filip Skakun
        /// http://twitter.com/xyzzer
        /// </summary>
        private bool AssociatedObjectIsInVisualTree
        {
            get
            {
                if (associatedObject != null)
                {
                    return Window.Current.Content != null && this.Ancestors.Contains(Window.Current.Content);
                }
                return false;
            }
        }

        /// <summary>
        /// Finds the object's associatedobject's ancestors
        /// Courtesy of Filip Skakun
        /// http://twitter.com/xyzzer
        /// </summary>
        private IEnumerable<DependencyObject> Ancestors
        {
            get
            {
                if (associatedObject != null)
                {
                    var parent = VisualTreeHelper.GetParent(associatedObject);

                    while (parent != null)
                    {
                        yield return parent;
                        parent = VisualTreeHelper.GetParent(parent);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a task that waits for a layout update to complete
        /// Courtesy of Filip Skakun
        /// http://twitter.com/xyzzer
        /// </summary>
        /// <returns></returns>
        private async Task WaitForLayoutUpdateAsync()
        {
            await EventAsync.FromEvent<object>(
                eh => associatedObject.LayoutUpdated += eh,
                eh => associatedObject.LayoutUpdated -= eh);
        }
    }
}
