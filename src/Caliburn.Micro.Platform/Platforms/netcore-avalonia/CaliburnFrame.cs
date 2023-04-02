using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;


namespace Caliburn.Micro;


public class CaliburnFrame : TransitioningContentControl, IStyleable
{
    static readonly ILog Log = LogManager.GetLog(typeof(CaliburnFrame));

    private string defaultContent { get; }= "Default Content";

    /// <summary>
    /// Initializes a new instance of the <see cref="CaliburnFrame"/> class.
    /// </summary>
    public CaliburnFrame()
    {
        Content= defaultContent;
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

        viewInstance.DataContext = viewModel;
       Content = viewInstance;
    }

}
