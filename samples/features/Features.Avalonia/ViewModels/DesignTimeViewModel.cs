using System;
using Caliburn.Micro;

namespace Features.Avalonia.ViewModels
{
    public class DesignTimeViewModel : Screen
    {
        public string Text => Execute.InDesignMode ? "Design Time" : "Run Time";
    }
}
