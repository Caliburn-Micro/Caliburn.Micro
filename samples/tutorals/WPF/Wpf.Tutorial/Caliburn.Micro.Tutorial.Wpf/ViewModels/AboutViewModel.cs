using Caliburn.Micro.Tutorial.Wpf.Models;
using System.Threading.Tasks;

namespace Caliburn.Micro.Tutorial.Wpf.ViewModels
  {
  public class AboutViewModel: Screen
    {
    private AboutModel  _aboutData = new AboutModel();

    public AboutModel AboutData
      {
      get { return _aboutData; }
      }

    public Task CloseForm()
      {
      return TryCloseAsync();
      }
    }
  }
