using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro.Tutorial.Wpf.ViewModels;
using System.Diagnostics;

namespace Caliburn.Micro.Tutorial.Wpf
  {
  public class Bootstrapper: BootstrapperBase
    {
    private readonly SimpleContainer _container = new SimpleContainer();

    public Bootstrapper()
      {
      Initialize();
      StartDebugLogger();
      }

    // [Conditional("DEBUG")] You can use this conditional starting with C# 9.0
    public static void StartDebugLogger()
      {
      LogManager.GetLog = type => new DebugLog(type);
      }

    protected override void Configure()
      {
      _container.Instance(_container);
      _container
        .Singleton<IWindowManager, WindowManager>()
        .Singleton<IEventAggregator, EventAggregator>();

      foreach(var assembly in SelectAssemblies())
        {
           assembly.GetTypes()
          .Where(type => type.IsClass)
          .Where(type => type.Name.EndsWith("ViewModel"))
          .ToList()
          .ForEach(viewModelType => _container.RegisterPerRequest(
              viewModelType, viewModelType.ToString(), viewModelType));
        }
      }

    protected override async void OnStartup(object sender, StartupEventArgs e)
      {
      var c= IoC.Get<SimpleContainer>();
      await DisplayRootViewForAsync(typeof(ShellViewModel));
      }

    protected override object GetInstance(Type service, string key)
      {
      return _container.GetInstance(service, key);
      }

    protected override IEnumerable<object> GetAllInstances(Type service)
      {
      return _container.GetAllInstances(service);
      }

    protected override void BuildUp(object instance)
      {
      _container.BuildUp(instance);
      }
    }
  }
