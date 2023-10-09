using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms
{
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the Xamarin.Forms platfrom.
    /// </summary>
    public class FormsPlatformProvider : IPlatformProvider
    {
        private readonly IPlatformProvider _platformProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsPlatformProvider"/> class.
        /// </summary>
        /// <param name="platformProvider">The existing platform provider (from the host platform) to encapsulate.</param>
        public FormsPlatformProvider(IPlatformProvider platformProvider)
            => _platformProvider = platformProvider;

        /// <inheritdoc />
        public virtual bool InDesignMode
            => _platformProvider.InDesignMode;

        /// <summary>
        /// Gets a value indicating whether or not classes should execute property change notications on the UI thread.
        /// </summary>
        public virtual bool PropertyChangeNotificationsOnUIThread
            => _platformProvider.PropertyChangeNotificationsOnUIThread;

        /// <inheritdoc />
        public virtual void BeginOnUIThread(System.Action action)
            => _platformProvider.BeginOnUIThread(action);

        /// <inheritdoc />
        public virtual Task OnUIThreadAsync(Func<Task> action)
            => _platformProvider.OnUIThreadAsync(action);

        /// <inheritdoc />
        public virtual void OnUIThread(System.Action action)
            => _platformProvider.OnUIThread(action);

        /// <inheritdoc />
        public virtual object GetFirstNonGeneratedView(object view)
            => _platformProvider.GetFirstNonGeneratedView(view);

        /// <inheritdoc />
        public virtual void ExecuteOnFirstLoad(object view, Action<object> handler) {
            var page = view as Page;

            if (page != null) {
                EventHandler appearing = null;

                appearing = (s, e) => {
                    page.Appearing -= appearing;

                    handler(view);
                };

                page.Appearing += appearing;

                return;
            }

            _platformProvider.ExecuteOnFirstLoad(view, handler);
        }

        /// <inheritdoc />
        public virtual void ExecuteOnLayoutUpdated(object view, Action<object> handler)
            => _platformProvider.ExecuteOnLayoutUpdated(view, handler);

        /// <inheritdoc />
        public virtual Func<CancellationToken, Task> GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult)
            => _platformProvider.GetViewCloseAction(viewModel, views, dialogResult);
    }
}
