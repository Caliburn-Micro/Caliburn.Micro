namespace Caliburn.Micro.HelloWP7 {
    using System;
	using System.Collections.Generic;
	using System.Windows;

    public class MainPageViewModel {

		IWindowManager windowManager;
		Func<MessageViewModel> messageViewModelFactory;
		public MainPageViewModel(IWindowManager windowManager, Func<MessageViewModel> messageViewModelFactory)
		{
			this.windowManager = windowManager;
			this.messageViewModelFactory = messageViewModelFactory;
		}

		public void ShowMessageAsPopup() {
 			var msgVM = messageViewModelFactory();
			msgVM.Message = "A message shown in the popup.\nSecond row of message shown in the popup.";
			windowManager.ShowPopup(msgVM);
		}
		public void ShowMessageAsDialog()
		{
			var msgVM = messageViewModelFactory();
            msgVM.Message = "A message shown in the dialog.\nSecond row of message shown in the dialog.";
			windowManager.ShowDialog(msgVM);
		}

		public IEnumerable<IResult> ShowInputDialog() {
            var showDialog = new ShowDialog<DialogViewModel>();
			yield return showDialog;

			var result = showDialog.Dialog.Result ;
            if (result != null) {

                yield return new ShowDialog<MessageViewModel>()
                    .Init(x=>x.Message= "The user entered: " + result);
            }
		}


		public void ShowMessageBox() {
			MessageBox.Show("Some message...", "WP7 native Message Box", MessageBoxButton.OK);
		}
    }
}