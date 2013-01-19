using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Caliburn.Micro.WinRT.Sample.Settings
{
    /// <summary>
    /// A simple settings flyout using a Popup control to demonstrate Caliburn.Micro's Settings Charm integration.
    /// </summary>
    /// <remarks>
    /// In a real application you should use something more powerful (e.g. http://github.com/timheuer/callisto").
    /// </remarks>
    public class SimplePopupSettingsWindowManager : ISettingsWindowManager
    {
        private readonly Popup _popup = new Popup();

        public void ShowSettingsFlyout(object viewModel, string commandLabel, IDictionary<string, object> viewSettings = null)
        {
            var view = ViewLocator.LocateForModel(viewModel, null, null) as FrameworkElement;
            if (view == null)
                return;

            ViewModelBinder.Bind(viewModel, view, null);

            var deactivator = viewModel as IDeactivate;
            if (deactivator != null)
            {
                EventHandler<object> onFlyoutClosed = null;
                onFlyoutClosed = delegate
                {
                    deactivator.Deactivate(true);
                    _popup.Closed -= onFlyoutClosed;
                };
                _popup.Closed += onFlyoutClosed;
            }

            var activator = viewModel as IActivate;
            if (activator != null)
            {
                activator.Activate();
            }

            ShowPopup(view, (double)SettingsPanelWidth.Small);
        }

        private void ShowPopup(FrameworkElement control, double width)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            if (double.IsNaN(width))
                throw new ArgumentOutOfRangeException("width");

            // layout
            _popup.Width = width;
            _popup.HorizontalAlignment = HorizontalAlignment.Right;
            _popup.Height = Window.Current.Bounds.Height;
            _popup.SetValue(Canvas.LeftProperty, Window.Current.Bounds.Width - _popup.Width);

            // make content fit
            _popup.Child = control;
            control.VerticalAlignment = VerticalAlignment.Stretch;
            control.HorizontalAlignment = HorizontalAlignment.Stretch;
            control.Height = _popup.Height;
            control.Width = _popup.Width;

            // add pretty animation(s)
            _popup.ChildTransitions = new Windows.UI.Xaml.Media.Animation.TransitionCollection 
                { 
                    new Windows.UI.Xaml.Media.Animation.EntranceThemeTransition 
                        { 
                            FromHorizontalOffset = 20d, 
                            FromVerticalOffset = 0d 
                        }
                };

            // setup
            _popup.IsLightDismissEnabled = true;
            _popup.IsOpen = true;

            // handle when it closes
            _popup.Closed -= popup_Closed;
            _popup.Closed += popup_Closed;

            // handle making it close
            Window.Current.Activated -= Current_Activated;
            Window.Current.Activated += Current_Activated;
        }

        private void Current_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (_popup == null)
                return;
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
                _popup.IsOpen = false;
        }

        private void popup_Closed(object sender, object e)
        {
            Window.Current.Activated -= Current_Activated;
            if (_popup == null)
                return;
            _popup.IsOpen = false;
        }

        public enum SettingsPanelWidth
        {
            Small = 346,
            Large = 646
        }
    }
}
