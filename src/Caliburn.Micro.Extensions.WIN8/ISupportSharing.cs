using Windows.ApplicationModel.DataTransfer;

namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes a class which is aware of sharing data with the Share charm.
    /// </summary>
    public interface ISupportSharing
    {
        /// <summary>
        /// Called when a share operation starts.
        /// </summary>
        /// <param name="dataRequest">The data request.</param>
        void OnShareRequested(DataRequest dataRequest);
    }
}
