using System;
using System.Windows.Navigation;

namespace Caliburn.Micro
{
    /// <summary>
    ///   Implemented by services that provide <see cref="Uri" /> based navigation.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to the view represented by the given view model.
        /// </summary>
        /// <param name="viewModel">The view model to navigate to.</param>
        /// <param name="extraData">Extra data to populate the view model with.</param>
        void NavigateToViewModel(Type viewModel, object extraData = null);

        /// <summary>
        /// Navigates to the view represented by the given view model.
        /// </summary>
        /// <typeparam name="TViewModel">The view model to navigate to.</typeparam>
        /// <param name="extraData">Extra data to populate the view model with.</param>
        void NavigateToViewModel<TViewModel>(object extraData = null);

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        bool CanGoForward { get; }

        /// <summary>
        ///   The current content.
        /// </summary>
        object CurrentContent { get; }

        /// <summary>
        ///   Stops the loading process.
        /// </summary>
        void StopLoading();

        /// <summary>
        ///   Navigates back.
        /// </summary>
        void GoBack();

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        void GoForward();

        /// <summary>
        ///   Removes the most recent entry from the back stack.
        /// </summary>
        /// <returns> The entry that was removed. </returns>
        JournalEntry RemoveBackEntry();

        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        event NavigatedEventHandler Navigated;

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        event NavigatingCancelEventHandler Navigating;

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        event NavigationFailedEventHandler NavigationFailed;

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        event NavigationStoppedEventHandler NavigationStopped;

        /// <summary>
        ///   Raised when a fragment navigation occurs.
        /// </summary>
        event FragmentNavigationEventHandler FragmentNavigation;
    }
}
