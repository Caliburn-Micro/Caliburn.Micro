using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    /// Service that handles the <see cref="DataTransferManager.DataRequested"/> event.
    /// </summary>
    public class ShareSourceService
    {
        private readonly Frame _rootFrame;
        private readonly DataTransferManager _transferManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShareSourceService" /> class.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        public ShareSourceService(Frame rootFrame)
        {
            _rootFrame = rootFrame;

            _transferManager = DataTransferManager.GetForCurrentView();
            _transferManager.DataRequested += OnDataRequested;
        }

        /// <summary>
        /// Accepts the share request and forwards it to the view model.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DataRequestedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var view = _rootFrame.Content as FrameworkElement;
            if (view == null)
                return;

            var supportSharing = view.DataContext as ISupportSharing;
            if (supportSharing == null)
                return;

            supportSharing.OnShareRequested(args.Request);
        }
    }
}
