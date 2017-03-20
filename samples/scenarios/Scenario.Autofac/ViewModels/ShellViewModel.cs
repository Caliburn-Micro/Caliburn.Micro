using System;
using Caliburn.Micro;

namespace Scenario.Autofac.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(ChildViewModel child)
        {
            Child = child;
        }

        protected override void OnInitialize()
        {
            DisplayName = "Shell";
        }

        public ChildViewModel Child { get; }
    }
}
