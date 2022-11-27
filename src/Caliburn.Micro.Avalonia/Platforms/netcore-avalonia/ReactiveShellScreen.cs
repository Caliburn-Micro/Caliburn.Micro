using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ReactiveUI;

namespace Caliburn.Micro
{

    public class ReactiveShellScreen : Screen, ReactiveUI.IScreen
    {
        public RoutingState Router { get; } = new RoutingState();

    }
}
