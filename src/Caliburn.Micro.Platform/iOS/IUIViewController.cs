using System;

namespace Caliburn.Micro
{
    public interface IUIViewController
    {
        bool IsViewLoaded { get; }

        event EventHandler ViewLoaded;
        event EventHandler ViewAppeared;
    }
}