using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Caliburn.Micro
{
    /// <summary>
    ///   A basic implementation of <see cref="INavigationService" /> designed to adapt the <see cref="Frame" /> control.
    /// </summary>
    public class CachingFrameAdapter : FrameAdapter
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(CachingFrameAdapter));

        private readonly Frame frame;

        private readonly List<object> viewModelBackStack = new List<object>();
        private readonly List<object> viewModelForwardStack = new List<object>();

        /// <summary>
        /// Creates an instance of <see cref="FrameAdapter" />.
        /// </summary>
        /// <param name="frame">The frame to represent as a <see cref="INavigationService" />.</param>
        /// <param name="treatViewAsLoaded">
        /// Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called.
        /// This is necessary when using the TransitionFrame.
        /// </param>
        public CachingFrameAdapter(Frame frame, bool treatViewAsLoaded = false)
            : base(frame, treatViewAsLoaded) {

            this.frame = frame;
        }

        /// <summary>
        ///   Occurs before navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected override void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            base.OnNavigating(sender, e);

            if (e.Cancel)
                return;

            var view = frame.Content as FrameworkElement;

            if (view == null)
                return;

            var viewModel = view.DataContext;

            switch (e.NavigationMode)
            {
                case NavigationMode.Back:

                    Log.Info("Pushing view model of type {0} on to the forward stack", viewModel == null ? "null" : viewModel.GetType().Name);

                    viewModelForwardStack.Add(viewModel);

                    break;

                case NavigationMode.Forward:
                case NavigationMode.Refresh:

                    Log.Info("Pushing view model of type {0} on to the back stack", viewModel == null ? "null" : viewModel.GetType().Name);

                    viewModelBackStack.Add(viewModel);

                    break;

                case NavigationMode.New:

                    Log.Info("Pushing view model of type {0} on to the forward stack", viewModel == null ? "null" : viewModel.GetType().Name);
                    Log.Info("Clearing the forward stack");

                    viewModelBackStack.Add(viewModel);
                    viewModelForwardStack.Clear();

                    break;
            }
        }

        /// <summary>
        ///   Occurs after navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected override void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Content == null)
                return;

            CurrentParameter = e.Parameter;

            var view = e.Content as Page;

            if (view == null)
            {
                throw new ArgumentException("View '" + e.Content.GetType().FullName +
                                            "' should inherit from Page or one of its descendents.");
            }

            switch (e.NavigationMode)
            {
                case NavigationMode.Back:
                {
                    var viewModel = PopOffStack(viewModelBackStack);

                    Log.Info("Popping view model of type {0} off the back stack", viewModel == null ? "null" : viewModel.GetType().Name);

                    BindViewModel(view, viewModel);

                    break;
                }
                case NavigationMode.Forward:
                {
                    var viewModel = PopOffStack(viewModelForwardStack);

                    Log.Info("Popping view model of type {0} off the forward stack", viewModel == null ? "null" : viewModel.GetType().Name);

                    BindViewModel(view, viewModel);

                    break;
                }
                case NavigationMode.New:
                case NavigationMode.Refresh:

                    BindViewModel(view);

                    break;
            }
        }

        private static T PopOffStack<T>(IList<T> stack) {
            
            if (!stack.Any())
                return default(T);

            var value = stack[stack.Count - 1];
            
            stack.RemoveAt(stack.Count - 1);

            return value;
        }

#if WinRT81
        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the backward navigation history of the Frame.
        /// </summary>
        public override IList<PageStackEntry> BackStack
        {
            get {
                var backStack = new ObservableCollection<PageStackEntry>(frame.BackStack);

                backStack.CollectionChanged += (s, e) => ApplyCollectionChanges(e, viewModelBackStack, frame.BackStack);

                return backStack;
            }
        }

        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the forward navigation history of the Frame.
        /// </summary>
        public override IList<PageStackEntry> ForwardStack
        {
            get {

                var forwardStack = new ObservableCollection<PageStackEntry>(frame.ForwardStack);

                forwardStack.CollectionChanged += (s, e) => ApplyCollectionChanges(e, viewModelForwardStack, frame.ForwardStack); 

                return forwardStack;
            }
        }

        private static void ApplyCollectionChanges(NotifyCollectionChangedEventArgs e, IList<object> viewModels, IList<PageStackEntry> frameEntries) {

            // We don't really care what the changes are, just the nature.
            // For new items we don't want to create the view model itself,
            // but just insert a null so the view model is created when get 
            // get to this part of the stack

            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:

                    for (var i = 0; i < e.NewItems.Count; i++) {
                        viewModels.Insert(e.NewStartingIndex + i, null);
                        frameEntries.Insert(e.NewStartingIndex + i, (PageStackEntry) e.NewItems[i]);
                    }

                    break;
                case NotifyCollectionChangedAction.Move:

                    var viewModelItems = viewModels.Skip(e.OldStartingIndex).Take(e.OldItems.Count).ToList();
                    var entryItems = frameEntries.Skip(e.OldStartingIndex).Take(e.OldItems.Count).ToList();

                    for (var i = 0; i < e.OldItems.Count; i++) {
                        viewModels.RemoveAt(e.OldStartingIndex);
                        frameEntries.RemoveAt(e.OldStartingIndex);
                    }

                    for (var i = 0; i < viewModelItems.Count; i++) {
                        viewModels.Insert(e.NewStartingIndex + i, viewModelItems[i]);
                        frameEntries.Insert(e.NewStartingIndex + i, entryItems[i]);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:

                    for (var i = 0; i < e.OldItems.Count; i++) {
                        viewModels.RemoveAt(e.OldStartingIndex);
                        frameEntries.RemoveAt(e.OldStartingIndex);
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:

                    for (var i = 0; i < e.OldItems.Count; i++) {
                        viewModels.RemoveAt(e.OldStartingIndex);
                        frameEntries.RemoveAt(e.OldStartingIndex);
                    }

                    for (var i = 0; i < e.NewItems.Count; i++) {
                        viewModels.Insert(e.NewStartingIndex + i, null);
                        frameEntries.Insert(e.NewStartingIndex + i, (PageStackEntry)e.NewItems[i]);
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:

                    viewModels.Clear();
                    frameEntries.Clear();

                    break;
            }
        }
#endif
    }
}
