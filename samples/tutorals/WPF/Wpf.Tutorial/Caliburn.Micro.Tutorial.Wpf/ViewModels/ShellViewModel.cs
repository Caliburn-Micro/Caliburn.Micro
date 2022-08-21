using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro.Tutorial.Wpf.ViewModels
  {
  public class ShellViewModel : Conductor<object>
    {
    private readonly IWindowManager _windowManager;

    public ShellViewModel(IWindowManager windowManager)
      {
      _windowManager= windowManager;
      }

    public bool CanFileMenu 
      { 
      get
        {
        return false;
        }
      }
    protected async override void OnViewLoaded(object view)
      {
      base.OnViewLoaded(view);
      await EditCategories();
      }

    public Task EditCategories()
      {
      var viewmodel = IoC.Get<CategoryViewModel>();
      return ActivateItemAsync(viewmodel, new CancellationToken());
      }

    public Task About()
      {
      return _windowManager.ShowDialogAsync(IoC.Get<AboutViewModel>());
      }
    }
  }
