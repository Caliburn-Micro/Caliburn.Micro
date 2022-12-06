using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace Caliburn.Micro
{
    public class AppViewLocator : ReactiveUI.IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string contract = null)
        {
            var viewType = viewModel.GetType();
            var view = ViewLocator.LocateForModelType(viewType, null,null);
            return view as IViewFor;
        }
    }
}
