using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Caliburn.Micro.HelloWP7
{
	public class DialogViewModel: Screen
	{

		public string Message { get; set; }


		string someTextInput; 
		public string SomeTextInput {
			get { return someTextInput; }
			set { someTextInput = value; NotifyOfPropertyChange(() => SomeTextInput); }
		}

        DateTime? someDateInput;
        public DateTime? SomeDateInput
        {
            get { return someDateInput; }
            set { someDateInput = value; NotifyOfPropertyChange(() => SomeDateInput); }
        }

		public string Result { get; set; }

		public void Confirm() {
			Result = SomeTextInput + "\n" + SomeDateInput.ToString();
			TryClose();
		}
		public void Cancel()
		{
			Result = null;
			this.TryClose();
		}
	}
}
