using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace Caliburn.Micro.WinAppSdk.Test.ViewModels
{
    public class ShellViewModel: Conductor<IScreen>.Collection.OneActive
    {
        public ShellViewModel()
        {
            Task.Run(async () =>
            {
                await ActivateItemAsync(new HomeViewModel());
            });

            var doc = new DependencyObjectCollection();
        }

    }
}
