namespace Caliburn.Micro.Xamarin.Forms {

    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Implemented by services that provide view & view model based navigation
    /// </summary>
    public interface INavigtionService {

        /// <summary>
        /// Asynchronously removes the top <see cref="T:Xamarin.Forms.Page"/> from the navigation stack, with optional animation.
        /// </summary>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        Task PopAsync(bool animated = true);

        /// <summary>
        ///  A task for asynchronously pushing a view onto the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewType">The type of the view</param>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        Task PushViewAsync(Type viewType, object parameter = null, bool animated = true);

        /// <summary>
        ///  A task for asynchronously pushing a view onto the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="T">The type of the view</typeparam>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>  
        Task PushViewAsync<T>(object parameter = null, bool animated = true);

        /// <summary>
        ///  A task for asynchronously pushing a view for the given view model onto the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewModelType">The type of the view model</param>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        Task PushViewModelAsync(Type viewModelType, object parameter = null, bool animated = true);

        /// <summary>
        ///  A task for asynchronously pushing a page for the given view model onto the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="T">The type of the view model</typeparam>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        Task PushViewModelAsync<T>(object parameter = null, bool animated = true);

        /// <summary>
        /// Pops all but the root <see cref="T:Xamarin.Forms.Page"/> off the navigation stack.
        /// </summary>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        Task PopToRootAsync(bool animated = true);
    }
}