using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace Caliburn.Micro.Maui {
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the Xamarin.Forms platfrom.
    /// </summary>
    public class FormsPlatformProvider : IPlatformProvider {
        private readonly IPlatformProvider platformProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsPlatformProvider"/> class.
        /// </summary>
        /// <param name="platformProvider">
        /// The existing platform provider (from the host platform) to encapsulate.
        /// </param>
        public FormsPlatformProvider(IPlatformProvider platformProvider)
            => this.platformProvider = platformProvider;

        /// <inheritdoc />
        public virtual bool InDesignMode
            => platformProvider.InDesignMode;

        /// <summary>
        /// Gets a value indicating whether or not classes should execute
        /// property change notications on the UI thread.
        /// </summary>
        public virtual bool PropertyChangeNotificationsOnUIThread
            => platformProvider.PropertyChangeNotificationsOnUIThread;

        /// <inheritdoc />
        public virtual void BeginOnUIThread(System.Action action)
            => platformProvider.BeginOnUIThread(action);

        /// <inheritdoc />
        public virtual Task OnUIThreadAsync(Func<Task> action)
            => platformProvider.OnUIThreadAsync(action);

        /// <inheritdoc />
        public virtual void OnUIThread(System.Action action)
            => platformProvider.OnUIThread(action);

        /// <inheritdoc />
        public virtual object GetFirstNonGeneratedView(object view)
            => platformProvider.GetFirstNonGeneratedView(view);

        /// <inheritdoc />
        public virtual void ExecuteOnLayoutUpdated(object view, Action<object> handler)
            => platformProvider.ExecuteOnLayoutUpdated(view, handler);

        /// <inheritdoc />
        public virtual Func<CancellationToken, Task> GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult)
            => platformProvider.GetViewCloseAction(viewModel, views, dialogResult);

        /// <inheritdoc />
        public virtual void ExecuteOnFirstLoad(object view, Action<object> handler) {
            if (!(view is Page page)) {
                platformProvider.ExecuteOnFirstLoad(view, handler);

                return;
            }

            void OnAppearing(object s, EventArgs e) {
                page.Appearing -= OnAppearing;

                handler(view);
            }

            page.Appearing += OnAppearing;
        }
    }
}
