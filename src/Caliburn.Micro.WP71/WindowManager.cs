namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using System.Threading;

    /// <summary>
    ///   A service that manages windows.
    /// </summary>
    public interface IWindowManager {
        /// <summary>
        ///   Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name = "rootModel">The root model.</param>
        /// <param name="settings">The optional dialog settings.</param>
        /// <param name = "context">The context.</param>
        void ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null);

        /// <summary>
        ///   Shows a popup at the current mouse position.
        /// </summary>
        /// <param name = "rootModel">The root model.</param>
        /// <param name = "context">The view context.</param>
        /// <param name = "settings">The optional popup settings.</param>
        void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null);
    }

    /// <summary>
    ///   A service that manages windows.
    /// </summary>
    public class WindowManager : IWindowManager {
        /// <summary>
        /// Predicate used to determine whether a page being navigated is actually a system dialog, which should 
        /// cause a temporary dialog disappearance.
        /// </summary>
        /// <remarks>
        /// The default implementation just take into account DatePicker and TimePicker pages from WP7 toolkit.
        /// </remarks>
        /// /// <param name = "uri">The destination page to check</param>
        public static Func<Uri, bool> IsSystemDialogNavigation = uri => { 
            return uri != null && uri.ToString().StartsWith("/Microsoft.Phone.Controls.Toolkit"); 
        };

        /// <summary>
        ///   Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name = "rootModel">The root model.</param>
        /// <param name = "context">The context.</param>
        /// <param name = "settings">The optional dialog settings.</param>
        public virtual void ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null) {
            var navigationSvc = IoC.Get<INavigationService>();

            var host = new DialogHost(navigationSvc);
            var view = ViewLocator.LocateForModel(rootModel, host, context);
            host.Content = view;
            host.SetValue(View.IsGeneratedProperty, true);

            ViewModelBinder.Bind(rootModel, host, null);
            host.SetActionTarget(rootModel);

            ApplySettings(host, settings);

            var activatable = rootModel as IActivate;
            if (activatable != null) {
                activatable.Activate();
            }

            var deactivator = rootModel as IDeactivate;
            if (deactivator != null) {
                host.Closed += delegate { deactivator.Deactivate(true); };
            }

            host.Open();
        }

        /// <summary>
        ///   Shows a popup at the current mouse position.
        /// </summary>
        /// <param name = "rootModel">The root model.</param>
        /// <param name = "context">The view context.</param>
        /// <param name = "settings">The optional popup settings.</param>
        public virtual void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null) {
            var popup = CreatePopup(rootModel, settings);
            var view = ViewLocator.LocateForModel(rootModel, popup, context);

            popup.Child = view;
            popup.SetValue(View.IsGeneratedProperty, true);

            ViewModelBinder.Bind(rootModel, popup, null);

            var activatable = rootModel as IActivate;
            if (activatable != null) {
                activatable.Activate();
            }

            var deactivator = rootModel as IDeactivate;
            if (deactivator != null) {
                popup.Closed += delegate { deactivator.Deactivate(true); };
            }

            popup.IsOpen = true;
        }

        /// <summary>
        ///   Creates a popup for hosting a popup window.
        /// </summary>
        /// <param name = "rootModel">The model.</param>
        /// <param name = "settings">The optional popup settings.</param>
        /// <returns>The popup.</returns>
        protected virtual Popup CreatePopup(object rootModel, IDictionary<string, object> settings) {
            var popup = new Popup();
            ApplySettings(popup, settings);
            return popup;
        }

        bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>> settings) {
            if(settings != null) {
                var type = target.GetType();

                foreach(var pair in settings) {
                    var propertyInfo = type.GetProperty(pair.Key);

                    if(propertyInfo != null)
                        propertyInfo.SetValue(target, pair.Value, null);
                }

                return true;
            }

            return false;
        }

        [ContentProperty("Content")]
        internal class DialogHost : FrameworkElement {
            INavigationService navigationSvc;
            PhoneApplicationPage currentPage;

            Popup hostPopup;
            bool isOpen = false;
            ContentControl viewContainer;
            Border pageFreezingLayer;
            Border maskingLayer;
            IElementPlacementAnimator elementPlacementAnimator;
            Dictionary<IApplicationBarIconButton, bool> appBarButtonsStatus = new Dictionary<IApplicationBarIconButton, bool>();
            bool appBarMenuEnabled;

            public DialogHost(INavigationService navigationSvc) {
                this.navigationSvc = navigationSvc;

                currentPage = navigationSvc.CurrentContent as PhoneApplicationPage;
                if (currentPage == null) {
                    throw new InvalidOperationException(
                        string.Format("In order to use ShowDialog the view currently loaded in the application frame ({0})"
                                      + " should inherit from PhoneApplicationPage or one of its descendents.", navigationSvc.CurrentContent.GetType()));
                }

                navigationSvc.Navigating += OnNavigating;
                navigationSvc.Navigated += OnNavigated;

                CreateUIElements();
                elementPlacementAnimator = CreateElementsAnimator();
            }

            public EventHandler Closed = delegate { };

            public void SetActionTarget(object target) {
                Action.SetTarget(viewContainer, target);
            }

            public virtual UIElement Content {
                get { return (UIElement)viewContainer.Content; }
                set { viewContainer.Content = value; }
            }

            public void Open() {
                if (isOpen) {
                    return;
                }

                isOpen = true;

                if(currentPage.ApplicationBar != null) {
                    DisableAppBar();
                }

                ArrangePlacement();

                currentPage.BackKeyPress += CurrentPageBackKeyPress;
                currentPage.OrientationChanged += CurrentPageOrientationChanged;

                hostPopup.IsOpen = true;
            }

            public void Close() {
                Close(reopenOnBackNavigation:false);
            }

            void Close(bool reopenOnBackNavigation) {
                if (!isOpen) {
                    return;
                }

                isOpen = false;
                elementPlacementAnimator.Exit(() => { hostPopup.IsOpen = false; });

                if(currentPage.ApplicationBar != null) {
                    RestoreAppBar();
                }

                currentPage.BackKeyPress -= CurrentPageBackKeyPress;
                currentPage.OrientationChanged -= CurrentPageOrientationChanged;

                if(!reopenOnBackNavigation) {
                    navigationSvc.Navigating -= OnNavigating;
                    navigationSvc.Navigated -= OnNavigated;

                    Closed(this, EventArgs.Empty);
                }
            }

            protected virtual IElementPlacementAnimator CreateElementsAnimator() {
                return new DefaultElementPlacementAnimator(maskingLayer, viewContainer);
            }

            protected virtual void CreateUIElements() {
                viewContainer = new ContentControl {
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Top,
                };
                maskingLayer = new Border {
                    Child = viewContainer,
                    Background = new SolidColorBrush(Color.FromArgb(170, 0, 0, 0)),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                pageFreezingLayer = new Border {
                    Background = new SolidColorBrush(Colors.Transparent),
                    Width = Application.Current.Host.Content.ActualWidth,
                    Height = Application.Current.Host.Content.ActualHeight
                };

                var panel = new Canvas();
                panel.Children.Add(pageFreezingLayer);
                panel.Children.Add(maskingLayer);

                hostPopup = new Popup { Child = panel };
            }

            void DisableAppBar() {
                appBarMenuEnabled = currentPage.ApplicationBar.IsMenuEnabled;
                appBarButtonsStatus.Clear();
                currentPage.ApplicationBar.Buttons.Cast<IApplicationBarIconButton>()
                    .Apply(b => {
                        appBarButtonsStatus.Add(b, b.IsEnabled);
                        b.IsEnabled = false;
                    });

                currentPage.ApplicationBar.IsMenuEnabled = false;
            }

            void RestoreAppBar() {
                currentPage.ApplicationBar.IsMenuEnabled = appBarMenuEnabled;
                currentPage.ApplicationBar.Buttons.Cast<IApplicationBarIconButton>()
                    .Apply(b => {
                        bool status;
                        if(appBarButtonsStatus.TryGetValue(b, out status))
                            b.IsEnabled = status;
                    });
            }

            void ArrangePlacement() {
                maskingLayer.Dispatcher.BeginInvoke(() => {
                    var placement = new ElementPlacement {
                        Transform = (Transform)currentPage.TransformToVisual(null),
                        Orientation = currentPage.Orientation,
                        Size = new Size(currentPage.ActualWidth, currentPage.ActualHeight)
                    };

                    elementPlacementAnimator.AnimateTo(placement);
                });
            }

            Uri currentPageUri;
            void OnNavigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e) {
                if (IsSystemDialogNavigation(e.Uri)) {
                    currentPageUri = navigationSvc.CurrentSource;
                }
            }

            void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
                if(IsSystemDialogNavigation(e.Uri)) {
                    Close(currentPageUri != null);
                }
                else if(e.Uri.Equals(currentPageUri)) {
                    currentPageUri = null;
                    //refreshes the page instance
                    currentPage = (PhoneApplicationPage)navigationSvc.CurrentContent;

                    Open();
                }
                else {
                    Close(reopenOnBackNavigation:false);
                }
            }

            void CurrentPageBackKeyPress(object sender, CancelEventArgs e) {
                e.Cancel = true;
                Close();
            }

            void CurrentPageOrientationChanged(object sender, OrientationChangedEventArgs e) {
                ArrangePlacement();
            }

            public class ElementPlacement {
                public Transform Transform;
                public PageOrientation Orientation;
                public Size Size;

                public double AngleFromDefault {
                    get {
                        if ((Orientation & PageOrientation.Landscape) == 0) {
                            return 0;
                        }

                        return Orientation == PageOrientation.LandscapeRight ? 90 : -90;
                    }
                }
            }

            public interface IElementPlacementAnimator {
                void Enter(ElementPlacement initialPlacement);
                void AnimateTo(ElementPlacement newPlacement);
                void Exit(System.Action onCompleted);
            }

            public class DefaultElementPlacementAnimator : IElementPlacementAnimator {
                FrameworkElement maskingLayer;
                FrameworkElement viewContainer;
                Storyboard storyboard = new Storyboard();
                ElementPlacement currentPlacement;

                public DefaultElementPlacementAnimator(FrameworkElement maskingLayer, FrameworkElement viewContainer) {
                    this.maskingLayer = maskingLayer;
                    this.viewContainer = viewContainer;
                }

                public void Enter(ElementPlacement initialPlacement) {
                    currentPlacement = initialPlacement;

                    //size
                    maskingLayer.Width = currentPlacement.Size.Width;
                    maskingLayer.Height = currentPlacement.Size.Height;

                    //position and orientation
                    maskingLayer.RenderTransform = currentPlacement.Transform;

                    //enter animation
                    var projection = new PlaneProjection { CenterOfRotationY = 0.1 };
                    viewContainer.Projection = projection;
                    AddDoubleAnimation(projection, "RotationX", from:-90, to:0, ms:400);
                    AddDoubleAnimation(maskingLayer, "Opacity", from:0, to:1, ms:400);

                    storyboard.Begin();
                }

                public void AnimateTo(ElementPlacement newPlacement) {
                    storyboard.Stop();
                    storyboard.Children.Clear();

                    if(currentPlacement == null) {
                        Enter(newPlacement);
                        return;
                    }

                    //size
                    AddDoubleAnimation(maskingLayer, "Width", from:currentPlacement.Size.Width, to:newPlacement.Size.Width, ms:200);
                    AddDoubleAnimation(maskingLayer, "Height", from:currentPlacement.Size.Height, to:newPlacement.Size.Height, ms:200);

                    //rotation at orientation change
                    var transformGroup = new TransformGroup();
                    var rotation = new RotateTransform {
                        CenterX = Application.Current.Host.Content.ActualWidth / 2,
                        CenterY = Application.Current.Host.Content.ActualHeight / 2
                    };
                    transformGroup.Children.Add(newPlacement.Transform);
                    transformGroup.Children.Add(rotation);
                    maskingLayer.RenderTransform = transformGroup;
                    AddDoubleAnimation(rotation, "Angle", from:newPlacement.AngleFromDefault - currentPlacement.AngleFromDefault, to:0.0);

                    //slight fading at orientation change
                    AddFading(maskingLayer);

                    currentPlacement = newPlacement;
                    storyboard.Begin();
                }

                public void Exit(System.Action onCompleted) {
                    storyboard.Stop();
                    storyboard.Children.Clear();

                    //exit animation
                    var projection = new PlaneProjection { CenterOfRotationY = 0.1 };
                    viewContainer.Projection = projection;
                    AddDoubleAnimation(projection, "RotationX", from:0, to:90, ms:250);
                    AddDoubleAnimation(maskingLayer, "Opacity", from:1, to:0, ms:350);

                    EventHandler handler = null;
                    handler = (o, e) => {
                        storyboard.Completed -= handler;
                        onCompleted();
                        currentPlacement = null;
                    };
                    storyboard.Completed += handler;
                    storyboard.Begin();
                }

                void AddDoubleAnimation(DependencyObject target, string property, double from, double to, int ms = 500) {
                    var timeline = new DoubleAnimation {
                        From = from,
                        To = to,
                        EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 4 },
                        Duration = new Duration(TimeSpan.FromMilliseconds(ms))
                    };

                    Storyboard.SetTarget(timeline, target);
                    Storyboard.SetTargetProperty(timeline, new PropertyPath(property));
                    storyboard.Children.Add(timeline);
                }

                void AddFading(FrameworkElement target) {
                    var timeline = new DoubleAnimationUsingKeyFrames {
                        Duration = new Duration(TimeSpan.FromMilliseconds(500))
                    };
                    timeline.KeyFrames.Add(new LinearDoubleKeyFrame { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)) });
                    timeline.KeyFrames.Add(new LinearDoubleKeyFrame { Value = 0.5, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150)) });
                    timeline.KeyFrames.Add(new LinearDoubleKeyFrame { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)) });

                    Storyboard.SetTarget(timeline, target);
                    Storyboard.SetTargetProperty(timeline, new PropertyPath("Opacity"));
                    storyboard.Children.Add(timeline);
                }
            }
        }
    }
}