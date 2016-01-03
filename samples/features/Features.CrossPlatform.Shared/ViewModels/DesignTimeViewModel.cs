using System;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class DesignTimeViewModel : Screen
    {
        public string Text => Execute.InDesignMode ? "Design Time" : "Run Time";
    }
}
