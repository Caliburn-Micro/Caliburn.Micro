using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caliburn.Micro.Xamarin.Forms
{
    public class XamarinFormsPlatformProvider : IPlatformProvider
    {
        private readonly IPlatformProvider platformProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformProvider"></param>
        public XamarinFormsPlatformProvider(IPlatformProvider platformProvider)
        {
            this.platformProvider = platformProvider;
        }

        public bool InDesignMode {
            get { return platformProvider.InDesignMode; }
        }

        public void BeginOnUIThread(System.Action action)
        {
            platformProvider.BeginOnUIThread(action);
        }

        public Task OnUIThreadAsync(System.Action action)
        {
            return platformProvider.OnUIThreadAsync(action);
        }

        public void OnUIThread(System.Action action)
        {
            platformProvider.OnUIThread(action);
        }

        public object GetFirstNonGeneratedView(object view) {
            return platformProvider.GetFirstNonGeneratedView(view);
        }

        public void ExecuteOnFirstLoad(object view, Action<object> handler) {
            platformProvider.ExecuteOnFirstLoad(view, handler);
        }

        public void ExecuteOnLayoutUpdated(object view, Action<object> handler) {
            platformProvider.ExecuteOnLayoutUpdated(view, handler);
        }

        public System.Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult) {
            return platformProvider.GetViewCloseAction(viewModel, views, dialogResult);
        }

        public static void Init() {
            
            var current = PlatformProvider.Current;

            PlatformProvider.Current = new XamarinFormsPlatformProvider(current);
        }
    }
}
