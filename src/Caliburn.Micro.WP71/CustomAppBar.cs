namespace Caliburn.Micro {
    using System;
    using System.Windows.Interactivity;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// An <see cref="ApplicationBarIconButton"/> capable of triggering action messages.
    /// </summary>
    public class AppBarButton : ApplicationBarIconButton {
        /// <summary>
        /// The action message.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// An <see cref="ApplicationBarMenuItem"/> capable of triggering action messages.
    /// </summary>
    public class AppBarMenuItem : ApplicationBarMenuItem {
        /// <summary>
        /// The action message.
        /// </summary>
        public string Message { get; set; }
    }

    class AppBarButtonTrigger : TriggerBase<PhoneApplicationPage> {
        public AppBarButtonTrigger(IApplicationBarMenuItem button) {
            button.Click += ButtonClicked;
        }

        void ButtonClicked(object sender, EventArgs e) {
            InvokeActions(e);
        }
    }

    class AppBarMenuItemTrigger : TriggerBase<PhoneApplicationPage> {
        public AppBarMenuItemTrigger(IApplicationBarMenuItem menuItem) {
            menuItem.Click += ButtonClicked;
        }

        void ButtonClicked(object sender, EventArgs e) {
            InvokeActions(e);
        }
    }
}