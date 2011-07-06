using System.Diagnostics;
using System.Linq;
namespace Caliburn.Micro.HelloWindowManager
{
	public class DialogViewModel : Screen
	{

		public DialogViewModel()
		{
			Buttons = new BindableCollection<int>(Enumerable.Range(1, 3));
		}

		public void SomeAction()
		{
			Debug.Print("SomeAction called");
		}

		public void SomeActionWithParameter(int value)
		{
			Debug.Print("SomeActionWithParameter called through bubbling with value={0}", value);
		}

		public BindableCollection<int> Buttons { get; private set; }
	}
}