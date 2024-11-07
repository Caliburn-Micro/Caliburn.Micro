using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Scenario.Autofac.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(ChildViewModel child)
        {
            Child = child;
        }

        protected override Task OnInitializedAsync(CancellationToken cancellationToken)
        {
            DisplayName = "Shell";
            return base.OnInitializedAsync(cancellationToken);
        }

        public ChildViewModel Child { get; }
    }
}
