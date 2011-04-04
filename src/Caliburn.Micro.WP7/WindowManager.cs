namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Markup;
    using System.Windows.Media;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using System.Linq;
    using System.Windows.Media.Animation;

    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public interface IWindowManager
    {

        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        void ShowDialog(object rootModel, object context = null);

        /// <summary>
        /// Shows a popup at the current mouse position.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null);
    }

    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public class WindowManager : IWindowManager
    {

        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        public virtual void ShowDialog(object rootModel, object context = null)
        {

            var navigationSvc = IoC.Get<INavigationService>();
            var page = navigationSvc.CurrentContent as PhoneApplicationPage;
            if (page == null) throw new InvalidOperationException(
                string.Format("In order to use ShowDialog the view currently loaded in the application frame ({0})"
                + " should inherit from PhoneApplicationPage or one of its descendents.", navigationSvc.CurrentContent.GetType()));



            var host = new DialogHost(page);
            var view = ViewLocator.LocateForModel(rootModel, host, context);
            host.Content = view;
            host.SetValue(View.IsGeneratedProperty, true);

            ViewModelBinder.Bind(rootModel, host, null);
            host.SetTarget(rootModel);

            var activatable = rootModel as IActivate;
            if (activatable != null)
                activatable.Activate();

            var deactivator = rootModel as IDeactivate;
            if (deactivator != null)
                host.Closed += delegate { deactivator.Deactivate(true); };

            host.Open();
        }

        /// <summary>
        /// Shows a popup at the current mouse position.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        public virtual void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            var popup = CreatePopup(rootModel, settings);
            var view = ViewLocator.LocateForModel(rootModel, popup, context);

            popup.Child = view;
            popup.SetValue(View.IsGeneratedProperty, true);

            ViewModelBinder.Bind(rootModel, popup, null);

            var activatable = rootModel as IActivate;
            if (activatable != null)
                activatable.Activate();

            var deactivator = rootModel as IDeactivate;
            if (deactivator != null)
                popup.Closed += delegate { deactivator.Deactivate(true); };

            popup.IsOpen = true;
            //popup.CaptureMouse();
        }

        /// <summary>
        /// Creates a popup for hosting a popup window.
        /// </summary>
        /// <param name="rootModel">The model.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The popup.</returns>
        protected virtual Popup CreatePopup(object rootModel, IDictionary<string, object> settings)
        {
            var popup = new Popup();

            if (settings != null)
            {
                var type = popup.GetType();

                foreach (var pair in settings)
                {
                    var propertyInfo = type.GetProperty(pair.Key);

                    if (propertyInfo != null)
                        propertyInfo.SetValue(popup, pair.Value, null);
                }
            }

            return popup;
        }















        [ContentProperty("Content")]
        internal class DialogHost : FrameworkElement
        {
            Popup popup;
            ContentControl container;
            Border border;

            Dictionary<IApplicationBarIconButton, bool> appBarButtonsStatus = new Dictionary<IApplicationBarIconButton, bool>();
            bool appBarMenuEnabled;
            PhoneApplicationPage currentPage;

            public DialogHost(PhoneApplicationPage currentPage)
            {
                this.currentPage = currentPage;

                container = new ContentControl
                {
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Top,
                };
                border = new Border
                {
                    Child = container,
                    Background = new SolidColorBrush(Color.FromArgb(170, 0, 0, 0))
                };

                popup = new Popup { Child = border };
            }


            public void SetTarget(object target)
            {
                Action.SetTarget(container, target);

            }

            UIElement content;
            public UIElement Content
            {
                get { return content; }
                set { content = value; container.Content = content; }
            }


            public void Open()
            {
                if (popup.IsOpen) return;
                popup.IsOpen = true;

                if (currentPage.ApplicationBar != null)
                {

                    appBarMenuEnabled = currentPage.ApplicationBar.IsMenuEnabled;

                    appBarButtonsStatus.Clear();
                    currentPage.ApplicationBar.Buttons.Cast<IApplicationBarIconButton>()
                        .Apply(b =>
                        {
                            appBarButtonsStatus.Add(b, b.IsEnabled);
                            b.IsEnabled = false;
                        });

                    currentPage.ApplicationBar.IsMenuEnabled = false;
                }

                ArrangePopup();

                currentPage.BackKeyPress += currentPage_BackKeyPress;
                currentPage.OrientationChanged += currentPage_OrientationChanged;
            }
            public void Close()
            {
                if (!popup.IsOpen) return;
                popup.IsOpen = false;

                if (currentPage.ApplicationBar != null)
                {
                    currentPage.ApplicationBar.IsMenuEnabled = appBarMenuEnabled;
                    currentPage.ApplicationBar.Buttons.Cast<IApplicationBarIconButton>()
                        .Apply(b =>
                        {
                            bool status;
                            if (appBarButtonsStatus.TryGetValue(b, out status))
                                b.IsEnabled = status;
                        });
                }
                currentPage.BackKeyPress -= currentPage_BackKeyPress;
                currentPage.OrientationChanged -= currentPage_OrientationChanged;

                Closed(this, EventArgs.Empty);
            }


            public EventHandler Closed = delegate { };

            void currentPage_BackKeyPress(object sender, CancelEventArgs e)
            {
                e.Cancel = true;
                this.Close();
            }

            void currentPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
            {
                ArrangePopup();
            }

            void ArrangePopup()
            {
                border.Dispatcher.BeginInvoke(() =>
                {
                    border.RenderTransform = GetPopupRenderTransform(currentPage);
                    border.Width = border.Width = currentPage.ActualWidth;
                    border.Height = border.Height = currentPage.ActualHeight;
                });
            }


            void ExecuteDelayed(System.Action action)
            {

                System.Threading.Timer timer = null;
                timer = new System.Threading.Timer(
                     state =>
                     {
                         try
                         {
                             Execute.OnUIThread(action);
                         }
                         finally
                         {
                             timer.Dispose();
                         }
                     },
                     null, 100, System.Threading.Timeout.Infinite
                 );
            }

            Transform GetPopupRenderTransform(PhoneApplicationPage relativeTo)
            {
                var translation = relativeTo.TransformToVisual(null);
                var offset = translation.Transform(new Point(0, 0));
                switch (relativeTo.Orientation)
                {
                    case PageOrientation.Landscape:
                    case PageOrientation.LandscapeLeft:
                        return new CompositeTransform
                        {
                            Rotation = 90,
                            TranslateX = offset.X,
                            TranslateY = offset.Y
                        };
                    case PageOrientation.LandscapeRight:
                        return new CompositeTransform
                        {
                            Rotation = -90,
                            TranslateX = offset.X,
                            TranslateY = offset.Y
                        };
                    case PageOrientation.None:
                    case PageOrientation.Portrait:
                    case PageOrientation.PortraitDown:
                    case PageOrientation.PortraitUp:
                    default:
                        return new TranslateTransform
                        {
                            X = offset.X,
                            Y = offset.Y
                        };
                }
            }
        }
    }
}