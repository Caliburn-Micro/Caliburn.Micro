namespace Caliburn.Micro {
    using System;
    using System.Windows.Interactivity;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// The interface for AppBar items capable of triggering action messages.
    /// </summary>
    public interface IAppBarActionMessage : IApplicationBarMenuItem {
        /// <summary>
        /// The action message.
        /// </summary>
        string Message { get; set; }
    }

    /// <summary>
    /// An <see cref="ApplicationBarIconButton"/> capable of triggering action messages.
    /// </summary>
    public class AppBarButton : ApplicationBarIconButton, IAppBarActionMessage {
        /// <summary>
        /// The action message.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// An <see cref="ApplicationBarMenuItem"/> capable of triggering action messages.
    /// </summary>
    public class AppBarMenuItem : ApplicationBarMenuItem, IAppBarActionMessage {
        /// <summary>
        /// The action message.
        /// </summary>
        public string Message { get; set; }
    }

    class AppBarItemTrigger : TriggerBase<PhoneApplicationPage> {
        public AppBarItemTrigger(IApplicationBarMenuItem button) {
            button.Click += ButtonClicked;
        }

        void ButtonClicked(object sender, EventArgs e) {
            InvokeActions(e);
        }
    }
}
