using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Caliburn.Micro
{
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the Xamarin.Forms platfrom.
    /// </summary>
    public class FormsPlatformProvider : IPlatformProvider
    {
        /// <summary>
        /// Creates an instance of <see cref="FormsPlatformProvider"/>.
        /// </summary>
        /// <param name="platformProvider">The existing platform provider (from the host platform) to encapsulate</param>
        public FormsPlatformProvider(IPlatformProvider platformProvider) {
            this.platformProvider = platformProvider;
        }

        private readonly IPlatformProvider platformProvider;

        /// <inheritdoc />
        public bool InDesignMode => platformProvider.InDesignMode;

        /// <inheritdoc />
        public void BeginOnUIThread(Action action) => platformProvider.BeginOnUIThread(action);

        /// <inheritdoc />
        public Task OnUIThreadAsync(Action action) => platformProvider.OnUIThreadAsync(action);

        /// <inheritdoc />
        public void OnUIThread(Action action) => platformProvider.OnUIThread(action);

        /// <inheritdoc />
        public object GetFirstNonGeneratedView(object view) => platformProvider.GetFirstNonGeneratedView(view);

        /// <inheritdoc />
        public void ExecuteOnFirstLoad(object view, Action<object> handler) {

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

            platformProvider.ExecuteOnFirstLoad(view, handler);
        }

        /// <inheritdoc />
        public void ExecuteOnLayoutUpdated(object view, Action<object> handler) => platformProvider.ExecuteOnLayoutUpdated(view, handler);

        /// <inheritdoc />
        public Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult) => platformProvider.GetViewCloseAction(viewModel, views, dialogResult);
    }
}
