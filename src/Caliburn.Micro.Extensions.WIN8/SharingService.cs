using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    /// Service that handles the <see cref="DataTransferManager.DataRequested"/> event.
    /// </summary>
    public class SharingService : ISharingService
    {
        private readonly DataTransferManager transferManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharingService" /> class.
        /// </summary>
        public SharingService()
        {
            transferManager = DataTransferManager.GetForCurrentView();
            transferManager.DataRequested += OnDataRequested;
        }

        /// <summary>
        /// Programmatically initiates the user interface for sharing content with another app.
        /// </summary>
        public void ShowShareUI()
        {
            DataTransferManager.ShowShareUI();
        }

        /// <summary>
        /// Accepts the share request and forwards it to the view model.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DataRequestedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var view = GetCurrentView();
            
            if (view == null)
                return;

            var supportSharing = view.DataContext as ISupportSharing;
            
            if (supportSharing == null)
                return;

            supportSharing.OnShareRequested(args.Request);
        }

        /// <summary>
        /// Determines the current view, checks for view first with frame and then view mode first with a shell view.
        /// </summary>
        /// <returns>The current view</returns>
        protected virtual FrameworkElement GetCurrentView()
        {
            var frame = Window.Current.Content as Frame;

            if (frame != null)
            {
                return frame.Content as FrameworkElement;
            }

            return Window.Current.Content as FrameworkElement;
        }
    }
}
