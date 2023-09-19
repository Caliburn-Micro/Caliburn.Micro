using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;


namespace Caliburn.Micro;

// Add xml comments
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class NavigationFrame : TransitioningContentControl, IStyleable, INavigationService
{
    private static readonly ILog Log = LogManager.GetLog(typeof(NavigationFrame));
    
    private string defaultContent { get; }= "Default Content";

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationFrame"/> class.
    /// </summary>
    public NavigationFrame()
    {
        Content = defaultContent;
        this.AttachedToVisualTree += NavigationFrame_AttachedToVisualTree;
    }

    private void NavigationFrame_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
    {
        OnNavigationServiceReady(new EventArgs());
    }

    public event EventHandler NavigationServiceReady;

    protected virtual void OnNavigationServiceReady(EventArgs e)
    {
        EventHandler handler = this.NavigationServiceReady;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    Type IStyleable.StyleKey => typeof(TransitioningContentControl);

    /// <summary>
    /// Invoked when navigating to a view model.
    /// </summary>
    /// <param name="viewModel">ViewModel to which the user navigates.</param>
    private void NavigateToViewModel(object? viewModel)
    {

        if (viewModel == null)
        {
            Log.Info("ViewModel is null. Falling back to default content.");
            Content = defaultContent;
            return;
        }

        var viewInstance = ViewLocator.LocateForModel(viewModel, null, null);
        if (viewInstance == null)
        {

            Log.Warn($"Couldn't find view for '{viewModel}'. Is it registered? Falling back to default content.");


            Content = defaultContent;
            return;
        }

        ViewModelBinder.Bind(viewModel, viewInstance, null);
        Log.Info($"View Model {viewModel}");
        Log.Info($"View {viewInstance}");
        viewInstance.DataContext = viewModel;
        Content =   viewInstance;
    }


    public Task GoBackAsync(bool animated = true)
    {
        throw new NotImplementedException();
    }

    public Task NavigateToViewAsync(Type viewType, object parameter = null, bool animated = true)
    {
        throw new NotImplementedException();
    }

    public Task NavigateToViewAsync<T>(object parameter = null, bool animated = true)
    {
        throw new NotImplementedException();
    }
/// <inheritdoc/>

    public Task NavigateToViewModelAsync(Type viewModelType, object parameter = null, bool animated = true)
    {
        Log.Info($"View model type {viewModelType}");
        var vm = Caliburn.Micro.IoC.GetInstance(viewModelType, null);
        Log.Info($"VM is null {vm == null}");
        NavigateToViewModel(vm);
        return Task.CompletedTask;
    }

    public Task NavigateToViewModelAsync<T>(object parameter = null, bool animated = true)
    {
        throw new NotImplementedException();
    }

    public Task NavigateToViewModelAsync(Screen viewModel, object parameter = null, bool animated = true)
    {
        Log.Info("Navigate to a screen");
        NavigateToViewModel(viewModel);
        return Task.CompletedTask;
    }

    public Task GoBackToRootAsync(bool animated = true)
    {
        throw new NotImplementedException();
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
