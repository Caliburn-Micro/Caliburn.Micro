﻿using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Features.Avalonia.Views;

namespace Features.Avalonia.ViewModels
{
    public class ExecuteViewModel : Screen
    {
        private bool safe;

        public bool Safe
        {
            get { return safe; }
            set { Set(ref safe, value); }
        }

        public void StartBackgroundWork()
        {
            Task.Factory.StartNew(BackgroundWork);
        }

        private void BackgroundWork()
        {
            if (Safe)
                SafeBackgroundWork();
            else
                UnsafeBackgroundWork();
        }

        private void SafeBackgroundWork()
        {
            Execute.OnUIThreadAsync(UpdateView);
        }

        private void UnsafeBackgroundWork()
        {
            UpdateView();
        }

        private Task UpdateView()
        {
            var view = (ExecuteView) GetView();

            view.UpdateView();

            return Task.CompletedTask;
        }
    }
}
