<a name='assembly'></a>
# Caliburn.Micro.Platform

## Contents

- [Action](#T-Caliburn-Micro-Action 'Caliburn.Micro.Action')
  - [TargetProperty](#F-Caliburn-Micro-Action-TargetProperty 'Caliburn.Micro.Action.TargetProperty')
  - [TargetWithoutContextProperty](#F-Caliburn-Micro-Action-TargetWithoutContextProperty 'Caliburn.Micro.Action.TargetWithoutContextProperty')
  - [GetTarget(d)](#M-Caliburn-Micro-Action-GetTarget-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Action.GetTarget(Windows.UI.Xaml.DependencyObject)')
  - [GetTargetWithoutContext(d)](#M-Caliburn-Micro-Action-GetTargetWithoutContext-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Action.GetTargetWithoutContext(Windows.UI.Xaml.DependencyObject)')
  - [HasTargetSet(element)](#M-Caliburn-Micro-Action-HasTargetSet-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Action.HasTargetSet(Windows.UI.Xaml.DependencyObject)')
  - [Invoke(target,methodName,view,source,eventArgs,parameters)](#M-Caliburn-Micro-Action-Invoke-System-Object,System-String,Windows-UI-Xaml-DependencyObject,Windows-UI-Xaml-FrameworkElement,System-Object,System-Object[]- 'Caliburn.Micro.Action.Invoke(System.Object,System.String,Windows.UI.Xaml.DependencyObject,Windows.UI.Xaml.FrameworkElement,System.Object,System.Object[])')
  - [SetTarget(d,target)](#M-Caliburn-Micro-Action-SetTarget-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.Action.SetTarget(Windows.UI.Xaml.DependencyObject,System.Object)')
  - [SetTargetWithoutContext(d,target)](#M-Caliburn-Micro-Action-SetTargetWithoutContext-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.Action.SetTargetWithoutContext(Windows.UI.Xaml.DependencyObject,System.Object)')
- [ActionExecutionContext](#T-Caliburn-Micro-ActionExecutionContext 'Caliburn.Micro.ActionExecutionContext')
  - [CanExecute](#F-Caliburn-Micro-ActionExecutionContext-CanExecute 'Caliburn.Micro.ActionExecutionContext.CanExecute')
  - [EventArgs](#F-Caliburn-Micro-ActionExecutionContext-EventArgs 'Caliburn.Micro.ActionExecutionContext.EventArgs')
  - [Method](#F-Caliburn-Micro-ActionExecutionContext-Method 'Caliburn.Micro.ActionExecutionContext.Method')
  - [Item](#P-Caliburn-Micro-ActionExecutionContext-Item-System-String- 'Caliburn.Micro.ActionExecutionContext.Item(System.String)')
  - [Message](#P-Caliburn-Micro-ActionExecutionContext-Message 'Caliburn.Micro.ActionExecutionContext.Message')
  - [Source](#P-Caliburn-Micro-ActionExecutionContext-Source 'Caliburn.Micro.ActionExecutionContext.Source')
  - [Target](#P-Caliburn-Micro-ActionExecutionContext-Target 'Caliburn.Micro.ActionExecutionContext.Target')
  - [View](#P-Caliburn-Micro-ActionExecutionContext-View 'Caliburn.Micro.ActionExecutionContext.View')
  - [Dispose()](#M-Caliburn-Micro-ActionExecutionContext-Dispose 'Caliburn.Micro.ActionExecutionContext.Dispose')
- [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage')
  - [#ctor()](#M-Caliburn-Micro-ActionMessage-#ctor 'Caliburn.Micro.ActionMessage.#ctor')
  - [ApplyAvailabilityEffect](#F-Caliburn-Micro-ActionMessage-ApplyAvailabilityEffect 'Caliburn.Micro.ActionMessage.ApplyAvailabilityEffect')
  - [BuildPossibleGuardNames](#F-Caliburn-Micro-ActionMessage-BuildPossibleGuardNames 'Caliburn.Micro.ActionMessage.BuildPossibleGuardNames')
  - [EnforceGuardsDuringInvocation](#F-Caliburn-Micro-ActionMessage-EnforceGuardsDuringInvocation 'Caliburn.Micro.ActionMessage.EnforceGuardsDuringInvocation')
  - [GetTargetMethod](#F-Caliburn-Micro-ActionMessage-GetTargetMethod 'Caliburn.Micro.ActionMessage.GetTargetMethod')
  - [InvokeAction](#F-Caliburn-Micro-ActionMessage-InvokeAction 'Caliburn.Micro.ActionMessage.InvokeAction')
  - [MethodNameProperty](#F-Caliburn-Micro-ActionMessage-MethodNameProperty 'Caliburn.Micro.ActionMessage.MethodNameProperty')
  - [ParametersProperty](#F-Caliburn-Micro-ActionMessage-ParametersProperty 'Caliburn.Micro.ActionMessage.ParametersProperty')
  - [PrepareContext](#F-Caliburn-Micro-ActionMessage-PrepareContext 'Caliburn.Micro.ActionMessage.PrepareContext')
  - [SetMethodBinding](#F-Caliburn-Micro-ActionMessage-SetMethodBinding 'Caliburn.Micro.ActionMessage.SetMethodBinding')
  - [ThrowsExceptions](#F-Caliburn-Micro-ActionMessage-ThrowsExceptions 'Caliburn.Micro.ActionMessage.ThrowsExceptions')
  - [MethodName](#P-Caliburn-Micro-ActionMessage-MethodName 'Caliburn.Micro.ActionMessage.MethodName')
  - [Parameters](#P-Caliburn-Micro-ActionMessage-Parameters 'Caliburn.Micro.ActionMessage.Parameters')
  - [Invoke(eventArgs)](#M-Caliburn-Micro-ActionMessage-Invoke-System-Object- 'Caliburn.Micro.ActionMessage.Invoke(System.Object)')
  - [OnAttached()](#M-Caliburn-Micro-ActionMessage-OnAttached 'Caliburn.Micro.ActionMessage.OnAttached')
  - [OnDetaching()](#M-Caliburn-Micro-ActionMessage-OnDetaching 'Caliburn.Micro.ActionMessage.OnDetaching')
  - [ToString()](#M-Caliburn-Micro-ActionMessage-ToString 'Caliburn.Micro.ActionMessage.ToString')
  - [TryFindGuardMethod(context,possibleGuardNames)](#M-Caliburn-Micro-ActionMessage-TryFindGuardMethod-Caliburn-Micro-ActionExecutionContext,System-Collections-Generic-IEnumerable{System-String}- 'Caliburn.Micro.ActionMessage.TryFindGuardMethod(Caliburn.Micro.ActionExecutionContext,System.Collections.Generic.IEnumerable{System.String})')
  - [UpdateAvailability()](#M-Caliburn-Micro-ActionMessage-UpdateAvailability 'Caliburn.Micro.ActionMessage.UpdateAvailability')
- [AttachedCollection\`1](#T-Caliburn-Micro-AttachedCollection`1 'Caliburn.Micro.AttachedCollection`1')
  - [#ctor()](#M-Caliburn-Micro-AttachedCollection`1-#ctor 'Caliburn.Micro.AttachedCollection`1.#ctor')
  - [AssociatedObject](#P-Caliburn-Micro-AttachedCollection`1-AssociatedObject 'Caliburn.Micro.AttachedCollection`1.AssociatedObject')
  - [Attach(dependencyObject)](#M-Caliburn-Micro-AttachedCollection`1-Attach-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.AttachedCollection`1.Attach(Windows.UI.Xaml.DependencyObject)')
  - [Detach()](#M-Caliburn-Micro-AttachedCollection`1-Detach 'Caliburn.Micro.AttachedCollection`1.Detach')
  - [OnItemAdded(item)](#M-Caliburn-Micro-AttachedCollection`1-OnItemAdded-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.AttachedCollection`1.OnItemAdded(Windows.UI.Xaml.DependencyObject)')
  - [OnItemRemoved(item)](#M-Caliburn-Micro-AttachedCollection`1-OnItemRemoved-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.AttachedCollection`1.OnItemRemoved(Windows.UI.Xaml.DependencyObject)')
- [Bind](#T-Caliburn-Micro-Bind 'Caliburn.Micro.Bind')
  - [AtDesignTimeProperty](#F-Caliburn-Micro-Bind-AtDesignTimeProperty 'Caliburn.Micro.Bind.AtDesignTimeProperty')
  - [ModelProperty](#F-Caliburn-Micro-Bind-ModelProperty 'Caliburn.Micro.Bind.ModelProperty')
  - [ModelWithoutContextProperty](#F-Caliburn-Micro-Bind-ModelWithoutContextProperty 'Caliburn.Micro.Bind.ModelWithoutContextProperty')
  - [GetAtDesignTime(dependencyObject)](#M-Caliburn-Micro-Bind-GetAtDesignTime-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Bind.GetAtDesignTime(Windows.UI.Xaml.DependencyObject)')
  - [GetModel(dependencyObject)](#M-Caliburn-Micro-Bind-GetModel-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Bind.GetModel(Windows.UI.Xaml.DependencyObject)')
  - [GetModelWithoutContext(dependencyObject)](#M-Caliburn-Micro-Bind-GetModelWithoutContext-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Bind.GetModelWithoutContext(Windows.UI.Xaml.DependencyObject)')
  - [SetAtDesignTime(dependencyObject,value)](#M-Caliburn-Micro-Bind-SetAtDesignTime-Windows-UI-Xaml-DependencyObject,System-Boolean- 'Caliburn.Micro.Bind.SetAtDesignTime(Windows.UI.Xaml.DependencyObject,System.Boolean)')
  - [SetModel(dependencyObject,value)](#M-Caliburn-Micro-Bind-SetModel-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.Bind.SetModel(Windows.UI.Xaml.DependencyObject,System.Object)')
  - [SetModelWithoutContext(dependencyObject,value)](#M-Caliburn-Micro-Bind-SetModelWithoutContext-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.Bind.SetModelWithoutContext(Windows.UI.Xaml.DependencyObject,System.Object)')
- [BindingScope](#T-Caliburn-Micro-BindingScope 'Caliburn.Micro.BindingScope')
  - [FindNamedDescendants](#F-Caliburn-Micro-BindingScope-FindNamedDescendants 'Caliburn.Micro.BindingScope.FindNamedDescendants')
  - [FindScopeNamingRoute](#F-Caliburn-Micro-BindingScope-FindScopeNamingRoute 'Caliburn.Micro.BindingScope.FindScopeNamingRoute')
  - [GetNamedElements](#F-Caliburn-Micro-BindingScope-GetNamedElements 'Caliburn.Micro.BindingScope.GetNamedElements')
  - [GetVisualParent](#F-Caliburn-Micro-BindingScope-GetVisualParent 'Caliburn.Micro.BindingScope.GetVisualParent')
  - [AddChildResolver(filter,resolver)](#M-Caliburn-Micro-BindingScope-AddChildResolver-System-Func{System-Type,System-Boolean},System-Func{Windows-UI-Xaml-DependencyObject,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}- 'Caliburn.Micro.BindingScope.AddChildResolver(System.Func{System.Type,System.Boolean},System.Func{Windows.UI.Xaml.DependencyObject,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}})')
  - [AddChildResolver\`\`1(resolver)](#M-Caliburn-Micro-BindingScope-AddChildResolver``1-System-Func{``0,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}- 'Caliburn.Micro.BindingScope.AddChildResolver``1(System.Func{``0,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}})')
  - [FindName(elementsToSearch,name)](#M-Caliburn-Micro-BindingScope-FindName-System-Collections-Generic-IEnumerable{Windows-UI-Xaml-FrameworkElement},System-String- 'Caliburn.Micro.BindingScope.FindName(System.Collections.Generic.IEnumerable{Windows.UI.Xaml.FrameworkElement},System.String)')
  - [RemoveChildResolver(resolver)](#M-Caliburn-Micro-BindingScope-RemoveChildResolver-Caliburn-Micro-ChildResolver- 'Caliburn.Micro.BindingScope.RemoveChildResolver(Caliburn.Micro.ChildResolver)')
- [BooleanToVisibilityConverter](#T-Caliburn-Micro-BooleanToVisibilityConverter 'Caliburn.Micro.BooleanToVisibilityConverter')
  - [Convert(value,targetType,parameter,language)](#M-Caliburn-Micro-BooleanToVisibilityConverter-Convert-System-Object,System-Type,System-Object,System-String- 'Caliburn.Micro.BooleanToVisibilityConverter.Convert(System.Object,System.Type,System.Object,System.String)')
  - [ConvertBack(value,targetType,parameter,language)](#M-Caliburn-Micro-BooleanToVisibilityConverter-ConvertBack-System-Object,System-Type,System-Object,System-String- 'Caliburn.Micro.BooleanToVisibilityConverter.ConvertBack(System.Object,System.Type,System.Object,System.String)')
- [CachingFrameAdapter](#T-Caliburn-Micro-CachingFrameAdapter 'Caliburn.Micro.CachingFrameAdapter')
  - [#ctor(frame,treatViewAsLoaded)](#M-Caliburn-Micro-CachingFrameAdapter-#ctor-Windows-UI-Xaml-Controls-Frame,System-Boolean- 'Caliburn.Micro.CachingFrameAdapter.#ctor(Windows.UI.Xaml.Controls.Frame,System.Boolean)')
  - [BackStack](#P-Caliburn-Micro-CachingFrameAdapter-BackStack 'Caliburn.Micro.CachingFrameAdapter.BackStack')
  - [ForwardStack](#P-Caliburn-Micro-CachingFrameAdapter-ForwardStack 'Caliburn.Micro.CachingFrameAdapter.ForwardStack')
  - [OnNavigated(sender,e)](#M-Caliburn-Micro-CachingFrameAdapter-OnNavigated-System-Object,Windows-UI-Xaml-Navigation-NavigationEventArgs- 'Caliburn.Micro.CachingFrameAdapter.OnNavigated(System.Object,Windows.UI.Xaml.Navigation.NavigationEventArgs)')
  - [OnNavigating(sender,e)](#M-Caliburn-Micro-CachingFrameAdapter-OnNavigating-System-Object,Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs- 'Caliburn.Micro.CachingFrameAdapter.OnNavigating(System.Object,Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs)')
- [CaliburnApplication](#T-Caliburn-Micro-CaliburnApplication 'Caliburn.Micro.CaliburnApplication')
  - [RootFrame](#P-Caliburn-Micro-CaliburnApplication-RootFrame 'Caliburn.Micro.CaliburnApplication.RootFrame')
  - [BuildUp(instance)](#M-Caliburn-Micro-CaliburnApplication-BuildUp-System-Object- 'Caliburn.Micro.CaliburnApplication.BuildUp(System.Object)')
  - [Configure()](#M-Caliburn-Micro-CaliburnApplication-Configure 'Caliburn.Micro.CaliburnApplication.Configure')
  - [CreateApplicationFrame()](#M-Caliburn-Micro-CaliburnApplication-CreateApplicationFrame 'Caliburn.Micro.CaliburnApplication.CreateApplicationFrame')
  - [DisplayRootView(viewType,paramter)](#M-Caliburn-Micro-CaliburnApplication-DisplayRootView-System-Type,System-Object- 'Caliburn.Micro.CaliburnApplication.DisplayRootView(System.Type,System.Object)')
  - [DisplayRootViewForAsync(viewModelType,cancellationToken)](#M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync-System-Type,System-Threading-CancellationToken- 'Caliburn.Micro.CaliburnApplication.DisplayRootViewForAsync(System.Type,System.Threading.CancellationToken)')
  - [DisplayRootViewForAsync(viewModelType)](#M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync-System-Type- 'Caliburn.Micro.CaliburnApplication.DisplayRootViewForAsync(System.Type)')
  - [DisplayRootViewForAsync\`\`1(cancellationToken)](#M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync``1-System-Threading-CancellationToken- 'Caliburn.Micro.CaliburnApplication.DisplayRootViewForAsync``1(System.Threading.CancellationToken)')
  - [DisplayRootViewForAsync\`\`1()](#M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync``1 'Caliburn.Micro.CaliburnApplication.DisplayRootViewForAsync``1')
  - [DisplayRootView\`\`1(parameter)](#M-Caliburn-Micro-CaliburnApplication-DisplayRootView``1-System-Object- 'Caliburn.Micro.CaliburnApplication.DisplayRootView``1(System.Object)')
  - [GetAllInstances(service)](#M-Caliburn-Micro-CaliburnApplication-GetAllInstances-System-Type- 'Caliburn.Micro.CaliburnApplication.GetAllInstances(System.Type)')
  - [GetInstance(service,key)](#M-Caliburn-Micro-CaliburnApplication-GetInstance-System-Type,System-String- 'Caliburn.Micro.CaliburnApplication.GetInstance(System.Type,System.String)')
  - [Initialize()](#M-Caliburn-Micro-CaliburnApplication-Initialize 'Caliburn.Micro.CaliburnApplication.Initialize')
  - [OnResuming(sender,e)](#M-Caliburn-Micro-CaliburnApplication-OnResuming-System-Object,System-Object- 'Caliburn.Micro.CaliburnApplication.OnResuming(System.Object,System.Object)')
  - [OnSuspending(sender,e)](#M-Caliburn-Micro-CaliburnApplication-OnSuspending-System-Object,Windows-ApplicationModel-SuspendingEventArgs- 'Caliburn.Micro.CaliburnApplication.OnSuspending(System.Object,Windows.ApplicationModel.SuspendingEventArgs)')
  - [OnUnhandledException(sender,e)](#M-Caliburn-Micro-CaliburnApplication-OnUnhandledException-System-Object,Windows-UI-Xaml-UnhandledExceptionEventArgs- 'Caliburn.Micro.CaliburnApplication.OnUnhandledException(System.Object,Windows.UI.Xaml.UnhandledExceptionEventArgs)')
  - [OnWindowCreated(args)](#M-Caliburn-Micro-CaliburnApplication-OnWindowCreated-Windows-UI-Xaml-WindowCreatedEventArgs- 'Caliburn.Micro.CaliburnApplication.OnWindowCreated(Windows.UI.Xaml.WindowCreatedEventArgs)')
  - [PrepareApplication()](#M-Caliburn-Micro-CaliburnApplication-PrepareApplication 'Caliburn.Micro.CaliburnApplication.PrepareApplication')
  - [PrepareViewFirst()](#M-Caliburn-Micro-CaliburnApplication-PrepareViewFirst 'Caliburn.Micro.CaliburnApplication.PrepareViewFirst')
  - [PrepareViewFirst(rootFrame)](#M-Caliburn-Micro-CaliburnApplication-PrepareViewFirst-Windows-UI-Xaml-Controls-Frame- 'Caliburn.Micro.CaliburnApplication.PrepareViewFirst(Windows.UI.Xaml.Controls.Frame)')
  - [SelectAssemblies()](#M-Caliburn-Micro-CaliburnApplication-SelectAssemblies 'Caliburn.Micro.CaliburnApplication.SelectAssemblies')
  - [StartDesignTime()](#M-Caliburn-Micro-CaliburnApplication-StartDesignTime 'Caliburn.Micro.CaliburnApplication.StartDesignTime')
  - [StartRuntime()](#M-Caliburn-Micro-CaliburnApplication-StartRuntime 'Caliburn.Micro.CaliburnApplication.StartRuntime')
- [ChildResolver](#T-Caliburn-Micro-ChildResolver 'Caliburn.Micro.ChildResolver')
  - [#ctor(filter,resolver)](#M-Caliburn-Micro-ChildResolver-#ctor-System-Func{System-Type,System-Boolean},System-Func{Windows-UI-Xaml-DependencyObject,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}- 'Caliburn.Micro.ChildResolver.#ctor(System.Func{System.Type,System.Boolean},System.Func{Windows.UI.Xaml.DependencyObject,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}})')
  - [CanResolve(type)](#M-Caliburn-Micro-ChildResolver-CanResolve-System-Type- 'Caliburn.Micro.ChildResolver.CanResolve(System.Type)')
  - [Resolve(obj)](#M-Caliburn-Micro-ChildResolver-Resolve-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.ChildResolver.Resolve(Windows.UI.Xaml.DependencyObject)')
- [ChildResolver\`1](#T-Caliburn-Micro-ChildResolver`1 'Caliburn.Micro.ChildResolver`1')
  - [#ctor(resolver)](#M-Caliburn-Micro-ChildResolver`1-#ctor-System-Func{`0,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}- 'Caliburn.Micro.ChildResolver`1.#ctor(System.Func{`0,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}})')
- [ConventionManager](#T-Caliburn-Micro-ConventionManager 'Caliburn.Micro.ConventionManager')
  - [ApplyBindingMode](#F-Caliburn-Micro-ConventionManager-ApplyBindingMode 'Caliburn.Micro.ConventionManager.ApplyBindingMode')
  - [ApplyStringFormat](#F-Caliburn-Micro-ConventionManager-ApplyStringFormat 'Caliburn.Micro.ConventionManager.ApplyStringFormat')
  - [ApplyUpdateSourceTrigger](#F-Caliburn-Micro-ConventionManager-ApplyUpdateSourceTrigger 'Caliburn.Micro.ConventionManager.ApplyUpdateSourceTrigger')
  - [ApplyValidation](#F-Caliburn-Micro-ConventionManager-ApplyValidation 'Caliburn.Micro.ConventionManager.ApplyValidation')
  - [ApplyValueConverter](#F-Caliburn-Micro-ConventionManager-ApplyValueConverter 'Caliburn.Micro.ConventionManager.ApplyValueConverter')
  - [BooleanToVisibilityConverter](#F-Caliburn-Micro-ConventionManager-BooleanToVisibilityConverter 'Caliburn.Micro.ConventionManager.BooleanToVisibilityConverter')
  - [ConfigureSelectedItem](#F-Caliburn-Micro-ConventionManager-ConfigureSelectedItem 'Caliburn.Micro.ConventionManager.ConfigureSelectedItem')
  - [ConfigureSelectedItemBinding](#F-Caliburn-Micro-ConventionManager-ConfigureSelectedItemBinding 'Caliburn.Micro.ConventionManager.ConfigureSelectedItemBinding')
  - [DefaultHeaderTemplate](#F-Caliburn-Micro-ConventionManager-DefaultHeaderTemplate 'Caliburn.Micro.ConventionManager.DefaultHeaderTemplate')
  - [DefaultItemTemplate](#F-Caliburn-Micro-ConventionManager-DefaultItemTemplate 'Caliburn.Micro.ConventionManager.DefaultItemTemplate')
  - [DerivePotentialSelectionNames](#F-Caliburn-Micro-ConventionManager-DerivePotentialSelectionNames 'Caliburn.Micro.ConventionManager.DerivePotentialSelectionNames')
  - [IncludeStaticProperties](#F-Caliburn-Micro-ConventionManager-IncludeStaticProperties 'Caliburn.Micro.ConventionManager.IncludeStaticProperties')
  - [OverwriteContent](#F-Caliburn-Micro-ConventionManager-OverwriteContent 'Caliburn.Micro.ConventionManager.OverwriteContent')
  - [SetBinding](#F-Caliburn-Micro-ConventionManager-SetBinding 'Caliburn.Micro.ConventionManager.SetBinding')
  - [Singularize](#F-Caliburn-Micro-ConventionManager-Singularize 'Caliburn.Micro.ConventionManager.Singularize')
  - [AddElementConvention(convention)](#M-Caliburn-Micro-ConventionManager-AddElementConvention-Caliburn-Micro-ElementConvention- 'Caliburn.Micro.ConventionManager.AddElementConvention(Caliburn.Micro.ElementConvention)')
  - [AddElementConvention\`\`1(bindableProperty,parameterProperty,eventName)](#M-Caliburn-Micro-ConventionManager-AddElementConvention``1-Windows-UI-Xaml-DependencyProperty,System-String,System-String- 'Caliburn.Micro.ConventionManager.AddElementConvention``1(Windows.UI.Xaml.DependencyProperty,System.String,System.String)')
  - [ApplyHeaderTemplate(element,headerTemplateProperty,headerTemplateSelectorProperty,viewModelType)](#M-Caliburn-Micro-ConventionManager-ApplyHeaderTemplate-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-DependencyProperty,Windows-UI-Xaml-DependencyProperty,System-Type- 'Caliburn.Micro.ConventionManager.ApplyHeaderTemplate(Windows.UI.Xaml.FrameworkElement,Windows.UI.Xaml.DependencyProperty,Windows.UI.Xaml.DependencyProperty,System.Type)')
  - [ApplyItemTemplate(itemsControl,property)](#M-Caliburn-Micro-ConventionManager-ApplyItemTemplate-Windows-UI-Xaml-Controls-ItemsControl,System-Reflection-PropertyInfo- 'Caliburn.Micro.ConventionManager.ApplyItemTemplate(Windows.UI.Xaml.Controls.ItemsControl,System.Reflection.PropertyInfo)')
  - [GetElementConvention(elementType)](#M-Caliburn-Micro-ConventionManager-GetElementConvention-System-Type- 'Caliburn.Micro.ConventionManager.GetElementConvention(System.Type)')
  - [GetPropertyCaseInsensitive(type,propertyName)](#M-Caliburn-Micro-ConventionManager-GetPropertyCaseInsensitive-System-Type,System-String- 'Caliburn.Micro.ConventionManager.GetPropertyCaseInsensitive(System.Type,System.String)')
  - [HasBinding()](#M-Caliburn-Micro-ConventionManager-HasBinding-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-DependencyProperty- 'Caliburn.Micro.ConventionManager.HasBinding(Windows.UI.Xaml.FrameworkElement,Windows.UI.Xaml.DependencyProperty)')
  - [SetBindingWithoutBindingOrValueOverwrite(viewModelType,path,property,element,convention,bindableProperty)](#M-Caliburn-Micro-ConventionManager-SetBindingWithoutBindingOrValueOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Windows-UI-Xaml-FrameworkElement,Caliburn-Micro-ElementConvention,Windows-UI-Xaml-DependencyProperty- 'Caliburn.Micro.ConventionManager.SetBindingWithoutBindingOrValueOverwrite(System.Type,System.String,System.Reflection.PropertyInfo,Windows.UI.Xaml.FrameworkElement,Caliburn.Micro.ElementConvention,Windows.UI.Xaml.DependencyProperty)')
  - [SetBindingWithoutBindingOverwrite()](#M-Caliburn-Micro-ConventionManager-SetBindingWithoutBindingOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Windows-UI-Xaml-FrameworkElement,Caliburn-Micro-ElementConvention,Windows-UI-Xaml-DependencyProperty- 'Caliburn.Micro.ConventionManager.SetBindingWithoutBindingOverwrite(System.Type,System.String,System.Reflection.PropertyInfo,Windows.UI.Xaml.FrameworkElement,Caliburn.Micro.ElementConvention,Windows.UI.Xaml.DependencyProperty)')
- [DependencyPropertyHelper](#T-Caliburn-Micro-DependencyPropertyHelper 'Caliburn.Micro.DependencyPropertyHelper')
  - [Register(name,propertyType,ownerType,defaultValue,propertyChangedCallback)](#M-Caliburn-Micro-DependencyPropertyHelper-Register-System-String,System-Type,System-Type,System-Object,Windows-UI-Xaml-PropertyChangedCallback- 'Caliburn.Micro.DependencyPropertyHelper.Register(System.String,System.Type,System.Type,System.Object,Windows.UI.Xaml.PropertyChangedCallback)')
  - [RegisterAttached(name,propertyType,ownerType,defaultValue,propertyChangedCallback)](#M-Caliburn-Micro-DependencyPropertyHelper-RegisterAttached-System-String,System-Type,System-Type,System-Object,Windows-UI-Xaml-PropertyChangedCallback- 'Caliburn.Micro.DependencyPropertyHelper.RegisterAttached(System.String,System.Type,System.Type,System.Object,Windows.UI.Xaml.PropertyChangedCallback)')
- [DispatcherTaskExtensions](#T-Caliburn-Micro-DispatcherTaskExtensions 'Caliburn.Micro.DispatcherTaskExtensions')
  - [RunTaskAsync(dispatcher,func,priority)](#M-Caliburn-Micro-DispatcherTaskExtensions-RunTaskAsync-Windows-UI-Core-CoreDispatcher,System-Func{System-Threading-Tasks-Task},Windows-UI-Core-CoreDispatcherPriority- 'Caliburn.Micro.DispatcherTaskExtensions.RunTaskAsync(Windows.UI.Core.CoreDispatcher,System.Func{System.Threading.Tasks.Task},Windows.UI.Core.CoreDispatcherPriority)')
  - [RunTaskAsync\`\`1(dispatcher,func,priority)](#M-Caliburn-Micro-DispatcherTaskExtensions-RunTaskAsync``1-Windows-UI-Core-CoreDispatcher,System-Func{System-Threading-Tasks-Task{``0}},Windows-UI-Core-CoreDispatcherPriority- 'Caliburn.Micro.DispatcherTaskExtensions.RunTaskAsync``1(Windows.UI.Core.CoreDispatcher,System.Func{System.Threading.Tasks.Task{``0}},Windows.UI.Core.CoreDispatcherPriority)')
- [ElementConvention](#T-Caliburn-Micro-ElementConvention 'Caliburn.Micro.ElementConvention')
  - [ApplyBinding](#F-Caliburn-Micro-ElementConvention-ApplyBinding 'Caliburn.Micro.ElementConvention.ApplyBinding')
  - [CreateTrigger](#F-Caliburn-Micro-ElementConvention-CreateTrigger 'Caliburn.Micro.ElementConvention.CreateTrigger')
  - [ElementType](#F-Caliburn-Micro-ElementConvention-ElementType 'Caliburn.Micro.ElementConvention.ElementType')
  - [GetBindableProperty](#F-Caliburn-Micro-ElementConvention-GetBindableProperty 'Caliburn.Micro.ElementConvention.GetBindableProperty')
  - [ParameterProperty](#F-Caliburn-Micro-ElementConvention-ParameterProperty 'Caliburn.Micro.ElementConvention.ParameterProperty')
- [FrameAdapter](#T-Caliburn-Micro-FrameAdapter 'Caliburn.Micro.FrameAdapter')
  - [#ctor(frame,treatViewAsLoaded)](#M-Caliburn-Micro-FrameAdapter-#ctor-Windows-UI-Xaml-Controls-Frame,System-Boolean- 'Caliburn.Micro.FrameAdapter.#ctor(Windows.UI.Xaml.Controls.Frame,System.Boolean)')
  - [BackStack](#P-Caliburn-Micro-FrameAdapter-BackStack 'Caliburn.Micro.FrameAdapter.BackStack')
  - [CanGoBack](#P-Caliburn-Micro-FrameAdapter-CanGoBack 'Caliburn.Micro.FrameAdapter.CanGoBack')
  - [CanGoForward](#P-Caliburn-Micro-FrameAdapter-CanGoForward 'Caliburn.Micro.FrameAdapter.CanGoForward')
  - [CurrentParameter](#P-Caliburn-Micro-FrameAdapter-CurrentParameter 'Caliburn.Micro.FrameAdapter.CurrentParameter')
  - [CurrentSourcePageType](#P-Caliburn-Micro-FrameAdapter-CurrentSourcePageType 'Caliburn.Micro.FrameAdapter.CurrentSourcePageType')
  - [ForwardStack](#P-Caliburn-Micro-FrameAdapter-ForwardStack 'Caliburn.Micro.FrameAdapter.ForwardStack')
  - [SourcePageType](#P-Caliburn-Micro-FrameAdapter-SourcePageType 'Caliburn.Micro.FrameAdapter.SourcePageType')
  - [BindViewModel(view,viewModel)](#M-Caliburn-Micro-FrameAdapter-BindViewModel-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.FrameAdapter.BindViewModel(Windows.UI.Xaml.DependencyObject,System.Object)')
  - [CanCloseOnNavigating(sender,e)](#M-Caliburn-Micro-FrameAdapter-CanCloseOnNavigating-System-Object,Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs- 'Caliburn.Micro.FrameAdapter.CanCloseOnNavigating(System.Object,Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs)')
  - [Dispose()](#M-Caliburn-Micro-FrameAdapter-Dispose 'Caliburn.Micro.FrameAdapter.Dispose')
  - [GoBack()](#M-Caliburn-Micro-FrameAdapter-GoBack 'Caliburn.Micro.FrameAdapter.GoBack')
  - [GoForward()](#M-Caliburn-Micro-FrameAdapter-GoForward 'Caliburn.Micro.FrameAdapter.GoForward')
  - [Navigate(sourcePageType)](#M-Caliburn-Micro-FrameAdapter-Navigate-System-Type- 'Caliburn.Micro.FrameAdapter.Navigate(System.Type)')
  - [Navigate(sourcePageType,parameter)](#M-Caliburn-Micro-FrameAdapter-Navigate-System-Type,System-Object- 'Caliburn.Micro.FrameAdapter.Navigate(System.Type,System.Object)')
  - [OnBackRequested(e)](#M-Caliburn-Micro-FrameAdapter-OnBackRequested-Windows-UI-Core-BackRequestedEventArgs- 'Caliburn.Micro.FrameAdapter.OnBackRequested(Windows.UI.Core.BackRequestedEventArgs)')
  - [OnNavigated(sender,e)](#M-Caliburn-Micro-FrameAdapter-OnNavigated-System-Object,Windows-UI-Xaml-Navigation-NavigationEventArgs- 'Caliburn.Micro.FrameAdapter.OnNavigated(System.Object,Windows.UI.Xaml.Navigation.NavigationEventArgs)')
  - [OnNavigating(sender,e)](#M-Caliburn-Micro-FrameAdapter-OnNavigating-System-Object,Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs- 'Caliburn.Micro.FrameAdapter.OnNavigating(System.Object,Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs)')
  - [ResumeStateAsync()](#M-Caliburn-Micro-FrameAdapter-ResumeStateAsync 'Caliburn.Micro.FrameAdapter.ResumeStateAsync')
  - [SuspendState()](#M-Caliburn-Micro-FrameAdapter-SuspendState 'Caliburn.Micro.FrameAdapter.SuspendState')
  - [TryInjectParameters(viewModel,parameter)](#M-Caliburn-Micro-FrameAdapter-TryInjectParameters-System-Object,System-Object- 'Caliburn.Micro.FrameAdapter.TryInjectParameters(System.Object,System.Object)')
- [IAttachedObject](#T-Caliburn-Micro-IAttachedObject 'Caliburn.Micro.IAttachedObject')
  - [AssociatedObject](#P-Caliburn-Micro-IAttachedObject-AssociatedObject 'Caliburn.Micro.IAttachedObject.AssociatedObject')
  - [Attach(dependencyObject)](#M-Caliburn-Micro-IAttachedObject-Attach-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.IAttachedObject.Attach(Windows.UI.Xaml.DependencyObject)')
  - [Detach()](#M-Caliburn-Micro-IAttachedObject-Detach 'Caliburn.Micro.IAttachedObject.Detach')
- [IHaveParameters](#T-Caliburn-Micro-IHaveParameters 'Caliburn.Micro.IHaveParameters')
  - [Parameters](#P-Caliburn-Micro-IHaveParameters-Parameters 'Caliburn.Micro.IHaveParameters.Parameters')
- [INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService')
  - [BackStack](#P-Caliburn-Micro-INavigationService-BackStack 'Caliburn.Micro.INavigationService.BackStack')
  - [CanGoBack](#P-Caliburn-Micro-INavigationService-CanGoBack 'Caliburn.Micro.INavigationService.CanGoBack')
  - [CanGoForward](#P-Caliburn-Micro-INavigationService-CanGoForward 'Caliburn.Micro.INavigationService.CanGoForward')
  - [CurrentSourcePageType](#P-Caliburn-Micro-INavigationService-CurrentSourcePageType 'Caliburn.Micro.INavigationService.CurrentSourcePageType')
  - [ForwardStack](#P-Caliburn-Micro-INavigationService-ForwardStack 'Caliburn.Micro.INavigationService.ForwardStack')
  - [SourcePageType](#P-Caliburn-Micro-INavigationService-SourcePageType 'Caliburn.Micro.INavigationService.SourcePageType')
  - [GoBack()](#M-Caliburn-Micro-INavigationService-GoBack 'Caliburn.Micro.INavigationService.GoBack')
  - [GoForward()](#M-Caliburn-Micro-INavigationService-GoForward 'Caliburn.Micro.INavigationService.GoForward')
  - [Navigate(sourcePageType)](#M-Caliburn-Micro-INavigationService-Navigate-System-Type- 'Caliburn.Micro.INavigationService.Navigate(System.Type)')
  - [Navigate(sourcePageType,parameter)](#M-Caliburn-Micro-INavigationService-Navigate-System-Type,System-Object- 'Caliburn.Micro.INavigationService.Navigate(System.Type,System.Object)')
  - [ResumeStateAsync()](#M-Caliburn-Micro-INavigationService-ResumeStateAsync 'Caliburn.Micro.INavigationService.ResumeStateAsync')
  - [SuspendState()](#M-Caliburn-Micro-INavigationService-SuspendState 'Caliburn.Micro.INavigationService.SuspendState')
- [ISharingService](#T-Caliburn-Micro-ISharingService 'Caliburn.Micro.ISharingService')
  - [ShowShareUI()](#M-Caliburn-Micro-ISharingService-ShowShareUI 'Caliburn.Micro.ISharingService.ShowShareUI')
- [ISupportSharing](#T-Caliburn-Micro-ISupportSharing 'Caliburn.Micro.ISupportSharing')
  - [OnShareRequested(dataRequest)](#M-Caliburn-Micro-ISupportSharing-OnShareRequested-Windows-ApplicationModel-DataTransfer-DataRequest- 'Caliburn.Micro.ISupportSharing.OnShareRequested(Windows.ApplicationModel.DataTransfer.DataRequest)')
- [Message](#T-Caliburn-Micro-Message 'Caliburn.Micro.Message')
  - [AttachProperty](#F-Caliburn-Micro-Message-AttachProperty 'Caliburn.Micro.Message.AttachProperty')
  - [GetAttach(d)](#M-Caliburn-Micro-Message-GetAttach-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Message.GetAttach(Windows.UI.Xaml.DependencyObject)')
  - [GetHandler(d)](#M-Caliburn-Micro-Message-GetHandler-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.Message.GetHandler(Windows.UI.Xaml.DependencyObject)')
  - [SetAttach(d,attachText)](#M-Caliburn-Micro-Message-SetAttach-Windows-UI-Xaml-DependencyObject,System-String- 'Caliburn.Micro.Message.SetAttach(Windows.UI.Xaml.DependencyObject,System.String)')
  - [SetHandler(d,value)](#M-Caliburn-Micro-Message-SetHandler-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.Message.SetHandler(Windows.UI.Xaml.DependencyObject,System.Object)')
- [MessageBinder](#T-Caliburn-Micro-MessageBinder 'Caliburn.Micro.MessageBinder')
  - [CustomConverters](#F-Caliburn-Micro-MessageBinder-CustomConverters 'Caliburn.Micro.MessageBinder.CustomConverters')
  - [EvaluateParameter](#F-Caliburn-Micro-MessageBinder-EvaluateParameter 'Caliburn.Micro.MessageBinder.EvaluateParameter')
  - [SpecialValues](#F-Caliburn-Micro-MessageBinder-SpecialValues 'Caliburn.Micro.MessageBinder.SpecialValues')
  - [CoerceValue(destinationType,providedValue,context)](#M-Caliburn-Micro-MessageBinder-CoerceValue-System-Type,System-Object,System-Object- 'Caliburn.Micro.MessageBinder.CoerceValue(System.Type,System.Object,System.Object)')
  - [DetermineParameters(context,requiredParameters)](#M-Caliburn-Micro-MessageBinder-DetermineParameters-Caliburn-Micro-ActionExecutionContext,System-Reflection-ParameterInfo[]- 'Caliburn.Micro.MessageBinder.DetermineParameters(Caliburn.Micro.ActionExecutionContext,System.Reflection.ParameterInfo[])')
  - [GetDefaultValue(type)](#M-Caliburn-Micro-MessageBinder-GetDefaultValue-System-Type- 'Caliburn.Micro.MessageBinder.GetDefaultValue(System.Type)')
- [NavigateHelper\`1](#T-Caliburn-Micro-NavigateHelper`1 'Caliburn.Micro.NavigateHelper`1')
  - [AttachTo(navigationService)](#M-Caliburn-Micro-NavigateHelper`1-AttachTo-Caliburn-Micro-INavigationService- 'Caliburn.Micro.NavigateHelper`1.AttachTo(Caliburn.Micro.INavigationService)')
  - [BuildUri()](#M-Caliburn-Micro-NavigateHelper`1-BuildUri 'Caliburn.Micro.NavigateHelper`1.BuildUri')
  - [Navigate()](#M-Caliburn-Micro-NavigateHelper`1-Navigate 'Caliburn.Micro.NavigateHelper`1.Navigate')
  - [WithParam\`\`1(property,value)](#M-Caliburn-Micro-NavigateHelper`1-WithParam``1-System-Linq-Expressions-Expression{System-Func{`0,``0}},``0- 'Caliburn.Micro.NavigateHelper`1.WithParam``1(System.Linq.Expressions.Expression{System.Func{`0,``0}},``0)')
- [NavigationExtensions](#T-Caliburn-Micro-NavigationExtensions 'Caliburn.Micro.NavigationExtensions')
  - [For\`\`1(navigationService)](#M-Caliburn-Micro-NavigationExtensions-For``1-Caliburn-Micro-INavigationService- 'Caliburn.Micro.NavigationExtensions.For``1(Caliburn.Micro.INavigationService)')
  - [NavigateToViewModel(navigationService,viewModelType,parameter)](#M-Caliburn-Micro-NavigationExtensions-NavigateToViewModel-Caliburn-Micro-INavigationService,System-Type,System-Object- 'Caliburn.Micro.NavigationExtensions.NavigateToViewModel(Caliburn.Micro.INavigationService,System.Type,System.Object)')
  - [NavigateToViewModel\`\`1(navigationService,parameter)](#M-Caliburn-Micro-NavigationExtensions-NavigateToViewModel``1-Caliburn-Micro-INavigationService,System-Object- 'Caliburn.Micro.NavigationExtensions.NavigateToViewModel``1(Caliburn.Micro.INavigationService,System.Object)')
  - [Navigate\`\`1(navigationService,parameter)](#M-Caliburn-Micro-NavigationExtensions-Navigate``1-Caliburn-Micro-INavigationService,System-Object- 'Caliburn.Micro.NavigationExtensions.Navigate``1(Caliburn.Micro.INavigationService,System.Object)')
  - [UriFor\`\`1(navigationService)](#M-Caliburn-Micro-NavigationExtensions-UriFor``1-Caliburn-Micro-INavigationService- 'Caliburn.Micro.NavigationExtensions.UriFor``1(Caliburn.Micro.INavigationService)')
- [Parameter](#T-Caliburn-Micro-Parameter 'Caliburn.Micro.Parameter')
  - [ValueProperty](#F-Caliburn-Micro-Parameter-ValueProperty 'Caliburn.Micro.Parameter.ValueProperty')
  - [Owner](#P-Caliburn-Micro-Parameter-Owner 'Caliburn.Micro.Parameter.Owner')
  - [Value](#P-Caliburn-Micro-Parameter-Value 'Caliburn.Micro.Parameter.Value')
  - [MakeAwareOf(owner)](#M-Caliburn-Micro-Parameter-MakeAwareOf-Caliburn-Micro-ActionMessage- 'Caliburn.Micro.Parameter.MakeAwareOf(Caliburn.Micro.ActionMessage)')
- [Parser](#T-Caliburn-Micro-Parser 'Caliburn.Micro.Parser')
  - [CreateParameter](#F-Caliburn-Micro-Parser-CreateParameter 'Caliburn.Micro.Parser.CreateParameter')
  - [CreateTrigger](#F-Caliburn-Micro-Parser-CreateTrigger 'Caliburn.Micro.Parser.CreateTrigger')
  - [InterpretMessageText](#F-Caliburn-Micro-Parser-InterpretMessageText 'Caliburn.Micro.Parser.InterpretMessageText')
  - [BindParameter(target,parameter,elementName,path,bindingMode)](#M-Caliburn-Micro-Parser-BindParameter-Windows-UI-Xaml-FrameworkElement,Caliburn-Micro-Parameter,System-String,System-String,Windows-UI-Xaml-Data-BindingMode- 'Caliburn.Micro.Parser.BindParameter(Windows.UI.Xaml.FrameworkElement,Caliburn.Micro.Parameter,System.String,System.String,Windows.UI.Xaml.Data.BindingMode)')
  - [CreateMessage(target,messageText)](#M-Caliburn-Micro-Parser-CreateMessage-Windows-UI-Xaml-DependencyObject,System-String- 'Caliburn.Micro.Parser.CreateMessage(Windows.UI.Xaml.DependencyObject,System.String)')
  - [Parse(target,text)](#M-Caliburn-Micro-Parser-Parse-Windows-UI-Xaml-DependencyObject,System-String- 'Caliburn.Micro.Parser.Parse(Windows.UI.Xaml.DependencyObject,System.String)')
- [ScopeNamingRoute](#T-Caliburn-Micro-BindingScope-ScopeNamingRoute 'Caliburn.Micro.BindingScope.ScopeNamingRoute')
  - [Root](#P-Caliburn-Micro-BindingScope-ScopeNamingRoute-Root 'Caliburn.Micro.BindingScope.ScopeNamingRoute.Root')
  - [AddHop(from,to)](#M-Caliburn-Micro-BindingScope-ScopeNamingRoute-AddHop-Windows-UI-Xaml-DependencyObject,Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.BindingScope.ScopeNamingRoute.AddHop(Windows.UI.Xaml.DependencyObject,Windows.UI.Xaml.DependencyObject)')
  - [TryGetHop(hopSource,hopTarget)](#M-Caliburn-Micro-BindingScope-ScopeNamingRoute-TryGetHop-Windows-UI-Xaml-DependencyObject,Windows-UI-Xaml-DependencyObject@- 'Caliburn.Micro.BindingScope.ScopeNamingRoute.TryGetHop(Windows.UI.Xaml.DependencyObject,Windows.UI.Xaml.DependencyObject@)')
- [SharingService](#T-Caliburn-Micro-SharingService 'Caliburn.Micro.SharingService')
  - [#ctor()](#M-Caliburn-Micro-SharingService-#ctor 'Caliburn.Micro.SharingService.#ctor')
  - [GetCurrentView()](#M-Caliburn-Micro-SharingService-GetCurrentView 'Caliburn.Micro.SharingService.GetCurrentView')
  - [OnDataRequested(sender,args)](#M-Caliburn-Micro-SharingService-OnDataRequested-Windows-ApplicationModel-DataTransfer-DataTransferManager,Windows-ApplicationModel-DataTransfer-DataRequestedEventArgs- 'Caliburn.Micro.SharingService.OnDataRequested(Windows.ApplicationModel.DataTransfer.DataTransferManager,Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs)')
  - [ShowShareUI()](#M-Caliburn-Micro-SharingService-ShowShareUI 'Caliburn.Micro.SharingService.ShowShareUI')
- [TriggerAction\`1](#T-Caliburn-Micro-TriggerAction`1 'Caliburn.Micro.TriggerAction`1')
  - [AssociatedObjectProperty](#F-Caliburn-Micro-TriggerAction`1-AssociatedObjectProperty 'Caliburn.Micro.TriggerAction`1.AssociatedObjectProperty')
  - [AssociatedObject](#P-Caliburn-Micro-TriggerAction`1-AssociatedObject 'Caliburn.Micro.TriggerAction`1.AssociatedObject')
  - [Execute(sender,parameter)](#M-Caliburn-Micro-TriggerAction`1-Execute-System-Object,System-Object- 'Caliburn.Micro.TriggerAction`1.Execute(System.Object,System.Object)')
  - [Invoke(parmeter)](#M-Caliburn-Micro-TriggerAction`1-Invoke-System-Object- 'Caliburn.Micro.TriggerAction`1.Invoke(System.Object)')
  - [OnAttached()](#M-Caliburn-Micro-TriggerAction`1-OnAttached 'Caliburn.Micro.TriggerAction`1.OnAttached')
  - [OnDetaching()](#M-Caliburn-Micro-TriggerAction`1-OnDetaching 'Caliburn.Micro.TriggerAction`1.OnDetaching')
- [View](#T-Caliburn-Micro-View 'Caliburn.Micro.View')
  - [ApplyConventionsProperty](#F-Caliburn-Micro-View-ApplyConventionsProperty 'Caliburn.Micro.View.ApplyConventionsProperty')
  - [ContextProperty](#F-Caliburn-Micro-View-ContextProperty 'Caliburn.Micro.View.ContextProperty')
  - [GetFirstNonGeneratedView](#F-Caliburn-Micro-View-GetFirstNonGeneratedView 'Caliburn.Micro.View.GetFirstNonGeneratedView')
  - [IsGeneratedProperty](#F-Caliburn-Micro-View-IsGeneratedProperty 'Caliburn.Micro.View.IsGeneratedProperty')
  - [IsLoadedProperty](#F-Caliburn-Micro-View-IsLoadedProperty 'Caliburn.Micro.View.IsLoadedProperty')
  - [IsScopeRootProperty](#F-Caliburn-Micro-View-IsScopeRootProperty 'Caliburn.Micro.View.IsScopeRootProperty')
  - [ModelProperty](#F-Caliburn-Micro-View-ModelProperty 'Caliburn.Micro.View.ModelProperty')
  - [InDesignMode](#P-Caliburn-Micro-View-InDesignMode 'Caliburn.Micro.View.InDesignMode')
  - [ExecuteOnLayoutUpdated(element,handler)](#M-Caliburn-Micro-View-ExecuteOnLayoutUpdated-Windows-UI-Xaml-FrameworkElement,System-EventHandler{System-Object}- 'Caliburn.Micro.View.ExecuteOnLayoutUpdated(Windows.UI.Xaml.FrameworkElement,System.EventHandler{System.Object})')
  - [ExecuteOnLoad(element,handler)](#M-Caliburn-Micro-View-ExecuteOnLoad-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-RoutedEventHandler- 'Caliburn.Micro.View.ExecuteOnLoad(Windows.UI.Xaml.FrameworkElement,Windows.UI.Xaml.RoutedEventHandler)')
  - [ExecuteOnUnload(element,handler)](#M-Caliburn-Micro-View-ExecuteOnUnload-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-RoutedEventHandler- 'Caliburn.Micro.View.ExecuteOnUnload(Windows.UI.Xaml.FrameworkElement,Windows.UI.Xaml.RoutedEventHandler)')
  - [GetApplyConventions(d)](#M-Caliburn-Micro-View-GetApplyConventions-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.View.GetApplyConventions(Windows.UI.Xaml.DependencyObject)')
  - [GetContext(d)](#M-Caliburn-Micro-View-GetContext-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.View.GetContext(Windows.UI.Xaml.DependencyObject)')
  - [GetModel(d)](#M-Caliburn-Micro-View-GetModel-Windows-UI-Xaml-DependencyObject- 'Caliburn.Micro.View.GetModel(Windows.UI.Xaml.DependencyObject)')
  - [IsElementLoaded(element)](#M-Caliburn-Micro-View-IsElementLoaded-Windows-UI-Xaml-FrameworkElement- 'Caliburn.Micro.View.IsElementLoaded(Windows.UI.Xaml.FrameworkElement)')
  - [SetApplyConventions(d,value)](#M-Caliburn-Micro-View-SetApplyConventions-Windows-UI-Xaml-DependencyObject,System-Nullable{System-Boolean}- 'Caliburn.Micro.View.SetApplyConventions(Windows.UI.Xaml.DependencyObject,System.Nullable{System.Boolean})')
  - [SetContext(d,value)](#M-Caliburn-Micro-View-SetContext-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.View.SetContext(Windows.UI.Xaml.DependencyObject,System.Object)')
  - [SetModel(d,value)](#M-Caliburn-Micro-View-SetModel-Windows-UI-Xaml-DependencyObject,System-Object- 'Caliburn.Micro.View.SetModel(Windows.UI.Xaml.DependencyObject,System.Object)')
- [ViewLocator](#T-Caliburn-Micro-ViewLocator 'Caliburn.Micro.ViewLocator')
  - [ContextSeparator](#F-Caliburn-Micro-ViewLocator-ContextSeparator 'Caliburn.Micro.ViewLocator.ContextSeparator')
  - [DeterminePackUriFromType](#F-Caliburn-Micro-ViewLocator-DeterminePackUriFromType 'Caliburn.Micro.ViewLocator.DeterminePackUriFromType')
  - [GetOrCreateViewType](#F-Caliburn-Micro-ViewLocator-GetOrCreateViewType 'Caliburn.Micro.ViewLocator.GetOrCreateViewType')
  - [LocateForModel](#F-Caliburn-Micro-ViewLocator-LocateForModel 'Caliburn.Micro.ViewLocator.LocateForModel')
  - [LocateForModelType](#F-Caliburn-Micro-ViewLocator-LocateForModelType 'Caliburn.Micro.ViewLocator.LocateForModelType')
  - [LocateTypeForModelType](#F-Caliburn-Micro-ViewLocator-LocateTypeForModelType 'Caliburn.Micro.ViewLocator.LocateTypeForModelType')
  - [ModifyModelTypeAtDesignTime](#F-Caliburn-Micro-ViewLocator-ModifyModelTypeAtDesignTime 'Caliburn.Micro.ViewLocator.ModifyModelTypeAtDesignTime')
  - [NameTransformer](#F-Caliburn-Micro-ViewLocator-NameTransformer 'Caliburn.Micro.ViewLocator.NameTransformer')
  - [TransformName](#F-Caliburn-Micro-ViewLocator-TransformName 'Caliburn.Micro.ViewLocator.TransformName')
  - [AddDefaultTypeMapping(viewSuffix)](#M-Caliburn-Micro-ViewLocator-AddDefaultTypeMapping-System-String- 'Caliburn.Micro.ViewLocator.AddDefaultTypeMapping(System.String)')
  - [AddNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-ViewLocator-AddNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.ViewLocator.AddNamespaceMapping(System.String,System.String[],System.String)')
  - [AddNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-ViewLocator-AddNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.ViewLocator.AddNamespaceMapping(System.String,System.String,System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-ViewLocator-AddSubNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.ViewLocator.AddSubNamespaceMapping(System.String,System.String[],System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-ViewLocator-AddSubNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.ViewLocator.AddSubNamespaceMapping(System.String,System.String,System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix)](#M-Caliburn-Micro-ViewLocator-AddTypeMapping-System-String,System-String,System-String[],System-String- 'Caliburn.Micro.ViewLocator.AddTypeMapping(System.String,System.String,System.String[],System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix)](#M-Caliburn-Micro-ViewLocator-AddTypeMapping-System-String,System-String,System-String,System-String- 'Caliburn.Micro.ViewLocator.AddTypeMapping(System.String,System.String,System.String,System.String)')
  - [ConfigureTypeMappings(config)](#M-Caliburn-Micro-ViewLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration- 'Caliburn.Micro.ViewLocator.ConfigureTypeMappings(Caliburn.Micro.TypeMappingConfiguration)')
  - [InitializeComponent(element)](#M-Caliburn-Micro-ViewLocator-InitializeComponent-System-Object- 'Caliburn.Micro.ViewLocator.InitializeComponent(System.Object)')
  - [RegisterViewSuffix(viewSuffix)](#M-Caliburn-Micro-ViewLocator-RegisterViewSuffix-System-String- 'Caliburn.Micro.ViewLocator.RegisterViewSuffix(System.String)')
- [ViewModelBinder](#T-Caliburn-Micro-ViewModelBinder 'Caliburn.Micro.ViewModelBinder')
  - [ApplyConventionsByDefault](#F-Caliburn-Micro-ViewModelBinder-ApplyConventionsByDefault 'Caliburn.Micro.ViewModelBinder.ApplyConventionsByDefault')
  - [Bind](#F-Caliburn-Micro-ViewModelBinder-Bind 'Caliburn.Micro.ViewModelBinder.Bind')
  - [BindActions](#F-Caliburn-Micro-ViewModelBinder-BindActions 'Caliburn.Micro.ViewModelBinder.BindActions')
  - [BindProperties](#F-Caliburn-Micro-ViewModelBinder-BindProperties 'Caliburn.Micro.ViewModelBinder.BindProperties')
  - [ConventionsAppliedProperty](#F-Caliburn-Micro-ViewModelBinder-ConventionsAppliedProperty 'Caliburn.Micro.ViewModelBinder.ConventionsAppliedProperty')
  - [HandleUnmatchedElements](#F-Caliburn-Micro-ViewModelBinder-HandleUnmatchedElements 'Caliburn.Micro.ViewModelBinder.HandleUnmatchedElements')
  - [ShouldApplyConventions(view)](#M-Caliburn-Micro-ViewModelBinder-ShouldApplyConventions-Windows-UI-Xaml-FrameworkElement- 'Caliburn.Micro.ViewModelBinder.ShouldApplyConventions(Windows.UI.Xaml.FrameworkElement)')
- [ViewModelLocator](#T-Caliburn-Micro-ViewModelLocator 'Caliburn.Micro.ViewModelLocator')
  - [InterfaceCaptureGroupName](#F-Caliburn-Micro-ViewModelLocator-InterfaceCaptureGroupName 'Caliburn.Micro.ViewModelLocator.InterfaceCaptureGroupName')
  - [LocateForView](#F-Caliburn-Micro-ViewModelLocator-LocateForView 'Caliburn.Micro.ViewModelLocator.LocateForView')
  - [LocateForViewType](#F-Caliburn-Micro-ViewModelLocator-LocateForViewType 'Caliburn.Micro.ViewModelLocator.LocateForViewType')
  - [LocateTypeForViewType](#F-Caliburn-Micro-ViewModelLocator-LocateTypeForViewType 'Caliburn.Micro.ViewModelLocator.LocateTypeForViewType')
  - [NameTransformer](#F-Caliburn-Micro-ViewModelLocator-NameTransformer 'Caliburn.Micro.ViewModelLocator.NameTransformer')
  - [TransformName](#F-Caliburn-Micro-ViewModelLocator-TransformName 'Caliburn.Micro.ViewModelLocator.TransformName')
  - [AddDefaultTypeMapping(viewSuffix)](#M-Caliburn-Micro-ViewModelLocator-AddDefaultTypeMapping-System-String- 'Caliburn.Micro.ViewModelLocator.AddDefaultTypeMapping(System.String)')
  - [AddNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-ViewModelLocator-AddNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.ViewModelLocator.AddNamespaceMapping(System.String,System.String[],System.String)')
  - [AddNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-ViewModelLocator-AddNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.ViewModelLocator.AddNamespaceMapping(System.String,System.String,System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.ViewModelLocator.AddSubNamespaceMapping(System.String,System.String[],System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.ViewModelLocator.AddSubNamespaceMapping(System.String,System.String,System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix)](#M-Caliburn-Micro-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String[],System-String- 'Caliburn.Micro.ViewModelLocator.AddTypeMapping(System.String,System.String,System.String[],System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix)](#M-Caliburn-Micro-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String,System-String- 'Caliburn.Micro.ViewModelLocator.AddTypeMapping(System.String,System.String,System.String,System.String)')
  - [ConfigureTypeMappings(config)](#M-Caliburn-Micro-ViewModelLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration- 'Caliburn.Micro.ViewModelLocator.ConfigureTypeMappings(Caliburn.Micro.TypeMappingConfiguration)')
  - [MakeInterface(typeName)](#M-Caliburn-Micro-ViewModelLocator-MakeInterface-System-String- 'Caliburn.Micro.ViewModelLocator.MakeInterface(System.String)')
- [WinRTContainer](#T-Caliburn-Micro-WinRTContainer 'Caliburn.Micro.WinRTContainer')
  - [RegisterNavigationService(rootFrame,treatViewAsLoaded,cacheViewModels)](#M-Caliburn-Micro-WinRTContainer-RegisterNavigationService-Windows-UI-Xaml-Controls-Frame,System-Boolean,System-Boolean- 'Caliburn.Micro.WinRTContainer.RegisterNavigationService(Windows.UI.Xaml.Controls.Frame,System.Boolean,System.Boolean)')
  - [RegisterSharingService()](#M-Caliburn-Micro-WinRTContainer-RegisterSharingService 'Caliburn.Micro.WinRTContainer.RegisterSharingService')
  - [RegisterWinRTServices()](#M-Caliburn-Micro-WinRTContainer-RegisterWinRTServices 'Caliburn.Micro.WinRTContainer.RegisterWinRTServices')
- [XamlMetadataProvider](#T-Caliburn-Micro-XamlMetadataProvider 'Caliburn.Micro.XamlMetadataProvider')
  - [GetXamlType(type)](#M-Caliburn-Micro-XamlMetadataProvider-GetXamlType-System-Type- 'Caliburn.Micro.XamlMetadataProvider.GetXamlType(System.Type)')
  - [GetXamlType(typeName)](#M-Caliburn-Micro-XamlMetadataProvider-GetXamlType-System-String- 'Caliburn.Micro.XamlMetadataProvider.GetXamlType(System.String)')
  - [GetXmlnsDefinitions()](#M-Caliburn-Micro-XamlMetadataProvider-GetXmlnsDefinitions 'Caliburn.Micro.XamlMetadataProvider.GetXmlnsDefinitions')
- [XamlPlatformProvider](#T-Caliburn-Micro-XamlPlatformProvider 'Caliburn.Micro.XamlPlatformProvider')
  - [#ctor()](#M-Caliburn-Micro-XamlPlatformProvider-#ctor 'Caliburn.Micro.XamlPlatformProvider.#ctor')
  - [InDesignMode](#P-Caliburn-Micro-XamlPlatformProvider-InDesignMode 'Caliburn.Micro.XamlPlatformProvider.InDesignMode')
  - [PropertyChangeNotificationsOnUIThread](#P-Caliburn-Micro-XamlPlatformProvider-PropertyChangeNotificationsOnUIThread 'Caliburn.Micro.XamlPlatformProvider.PropertyChangeNotificationsOnUIThread')
  - [BeginOnUIThread(action)](#M-Caliburn-Micro-XamlPlatformProvider-BeginOnUIThread-System-Action- 'Caliburn.Micro.XamlPlatformProvider.BeginOnUIThread(System.Action)')
  - [ExecuteOnFirstLoad(view,handler)](#M-Caliburn-Micro-XamlPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}- 'Caliburn.Micro.XamlPlatformProvider.ExecuteOnFirstLoad(System.Object,System.Action{System.Object})')
  - [ExecuteOnLayoutUpdated(view,handler)](#M-Caliburn-Micro-XamlPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}- 'Caliburn.Micro.XamlPlatformProvider.ExecuteOnLayoutUpdated(System.Object,System.Action{System.Object})')
  - [GetFirstNonGeneratedView(view)](#M-Caliburn-Micro-XamlPlatformProvider-GetFirstNonGeneratedView-System-Object- 'Caliburn.Micro.XamlPlatformProvider.GetFirstNonGeneratedView(System.Object)')
  - [GetViewCloseAction(viewModel,views,dialogResult)](#M-Caliburn-Micro-XamlPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}- 'Caliburn.Micro.XamlPlatformProvider.GetViewCloseAction(System.Object,System.Collections.Generic.ICollection{System.Object},System.Nullable{System.Boolean})')
  - [OnUIThread(action)](#M-Caliburn-Micro-XamlPlatformProvider-OnUIThread-System-Action- 'Caliburn.Micro.XamlPlatformProvider.OnUIThread(System.Action)')
  - [OnUIThreadAsync(action)](#M-Caliburn-Micro-XamlPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}- 'Caliburn.Micro.XamlPlatformProvider.OnUIThreadAsync(System.Func{System.Threading.Tasks.Task})')

<a name='T-Caliburn-Micro-Action'></a>
## Action `type`

##### Namespace

Caliburn.Micro

##### Summary

A host for action related attached properties.

<a name='F-Caliburn-Micro-Action-TargetProperty'></a>
### TargetProperty `constants`

##### Summary

A property definition representing the target of an [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') . The DataContext of the element will be set to this instance.

<a name='F-Caliburn-Micro-Action-TargetWithoutContextProperty'></a>
### TargetWithoutContextProperty `constants`

##### Summary

A property definition representing the target of an [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') . The DataContext of the element is not set to this instance.

<a name='M-Caliburn-Micro-Action-GetTarget-Windows-UI-Xaml-DependencyObject-'></a>
### GetTarget(d) `method`

##### Summary

Gets the target for instances of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') .

##### Returns

The target for instances of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to which the target is attached. |

<a name='M-Caliburn-Micro-Action-GetTargetWithoutContext-Windows-UI-Xaml-DependencyObject-'></a>
### GetTargetWithoutContext(d) `method`

##### Summary

Gets the target for instances of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') .

##### Returns

The target for instances of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to which the target is attached. |

<a name='M-Caliburn-Micro-Action-HasTargetSet-Windows-UI-Xaml-DependencyObject-'></a>
### HasTargetSet(element) `method`

##### Summary

Checks if the [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') -Target was set.

##### Returns

True if Target or TargetWithoutContext was set on `element`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | DependencyObject to check |

<a name='M-Caliburn-Micro-Action-Invoke-System-Object,System-String,Windows-UI-Xaml-DependencyObject,Windows-UI-Xaml-FrameworkElement,System-Object,System-Object[]-'></a>
### Invoke(target,methodName,view,source,eventArgs,parameters) `method`

##### Summary

Uses the action pipeline to invoke the method.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object instance to invoke the method on. |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method to invoke. |
| view | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The view. |
| source | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The source of the invocation. |
| eventArgs | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event args. |
| parameters | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The method parameters. |

<a name='M-Caliburn-Micro-Action-SetTarget-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### SetTarget(d,target) `method`

##### Summary

Sets the target of the [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') .

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to attach the target to. |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The target for instances of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') . |

<a name='M-Caliburn-Micro-Action-SetTargetWithoutContext-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### SetTargetWithoutContext(d,target) `method`

##### Summary

Sets the target of the [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') .

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to attach the target to. |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The target for instances of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') . |

##### Remarks

The DataContext will not be set.

<a name='T-Caliburn-Micro-ActionExecutionContext'></a>
## ActionExecutionContext `type`

##### Namespace

Caliburn.Micro

##### Summary

The context used during the execution of an Action or its guard.

<a name='F-Caliburn-Micro-ActionExecutionContext-CanExecute'></a>
### CanExecute `constants`

##### Summary

Determines whether the action can execute.

##### Remarks

Returns true if the action can execute, false otherwise.

<a name='F-Caliburn-Micro-ActionExecutionContext-EventArgs'></a>
### EventArgs `constants`

##### Summary

Any event arguments associated with the action's invocation.

<a name='F-Caliburn-Micro-ActionExecutionContext-Method'></a>
### Method `constants`

##### Summary

The actual method info to be invoked.

<a name='P-Caliburn-Micro-ActionExecutionContext-Item-System-String-'></a>
### Item `property`

##### Summary

Gets or sets additional data needed to invoke the action.

##### Returns

Custom data associated with the context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The data key. |

<a name='P-Caliburn-Micro-ActionExecutionContext-Message'></a>
### Message `property`

##### Summary

The message being executed.

<a name='P-Caliburn-Micro-ActionExecutionContext-Source'></a>
### Source `property`

##### Summary

The source from which the message originates.

<a name='P-Caliburn-Micro-ActionExecutionContext-Target'></a>
### Target `property`

##### Summary

The instance on which the action is invoked.

<a name='P-Caliburn-Micro-ActionExecutionContext-View'></a>
### View `property`

##### Summary

The view associated with the target.

<a name='M-Caliburn-Micro-ActionExecutionContext-Dispose'></a>
### Dispose() `method`

##### Summary

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-ActionMessage'></a>
## ActionMessage `type`

##### Namespace

Caliburn.Micro

##### Summary

Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.

<a name='M-Caliburn-Micro-ActionMessage-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates an instance of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage').

##### Parameters

This constructor has no parameters.

<a name='F-Caliburn-Micro-ActionMessage-ApplyAvailabilityEffect'></a>
### ApplyAvailabilityEffect `constants`

##### Summary

Applies an availability effect, such as IsEnabled, to an element.

##### Remarks

Returns a value indicating whether or not the action is available.

<a name='F-Caliburn-Micro-ActionMessage-BuildPossibleGuardNames'></a>
### BuildPossibleGuardNames `constants`

##### Summary

Returns the list of possible names of guard methods / properties for the given method.

<a name='F-Caliburn-Micro-ActionMessage-EnforceGuardsDuringInvocation'></a>
### EnforceGuardsDuringInvocation `constants`

##### Summary

Causes the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.

##### Remarks

This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.

<a name='F-Caliburn-Micro-ActionMessage-GetTargetMethod'></a>
### GetTargetMethod `constants`

##### Summary

Finds the method on the target matching the specified message.

##### Returns

The matching method, if available.

<a name='F-Caliburn-Micro-ActionMessage-InvokeAction'></a>
### InvokeAction `constants`

##### Summary

Invokes the action using the specified [ActionExecutionContext](#T-Caliburn-Micro-ActionExecutionContext 'Caliburn.Micro.ActionExecutionContext')

<a name='F-Caliburn-Micro-ActionMessage-MethodNameProperty'></a>
### MethodNameProperty `constants`

##### Summary

Represents the method name of an action message.

<a name='F-Caliburn-Micro-ActionMessage-ParametersProperty'></a>
### ParametersProperty `constants`

##### Summary

Represents the parameters of an action message.

<a name='F-Caliburn-Micro-ActionMessage-PrepareContext'></a>
### PrepareContext `constants`

##### Summary

Prepares the action execution context for use.

<a name='F-Caliburn-Micro-ActionMessage-SetMethodBinding'></a>
### SetMethodBinding `constants`

##### Summary

Sets the target, method and view on the context. Uses a bubbling strategy by default.

<a name='F-Caliburn-Micro-ActionMessage-ThrowsExceptions'></a>
### ThrowsExceptions `constants`

##### Summary

Causes the action to throw if it cannot locate the target or the method at invocation time.

##### Remarks

True by default.

<a name='P-Caliburn-Micro-ActionMessage-MethodName'></a>
### MethodName `property`

##### Summary

Gets or sets the name of the method to be invoked on the presentation model class.

<a name='P-Caliburn-Micro-ActionMessage-Parameters'></a>
### Parameters `property`

##### Summary

Gets the parameters to pass as part of the method invocation.

<a name='M-Caliburn-Micro-ActionMessage-Invoke-System-Object-'></a>
### Invoke(eventArgs) `method`

##### Summary

Invokes the action.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventArgs | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference. |

<a name='M-Caliburn-Micro-ActionMessage-OnAttached'></a>
### OnAttached() `method`

##### Summary

Called after the action is attached to an AssociatedObject.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-ActionMessage-OnDetaching'></a>
### OnDetaching() `method`

##### Summary

Called when the action is being detached from its AssociatedObject, but before it has actually occurred.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-ActionMessage-ToString'></a>
### ToString() `method`

##### Summary

Returns a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that represents the current [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object').

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that represents the current [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object').

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-ActionMessage-TryFindGuardMethod-Caliburn-Micro-ActionExecutionContext,System-Collections-Generic-IEnumerable{System-String}-'></a>
### TryFindGuardMethod(context,possibleGuardNames) `method`

##### Summary

Try to find a candidate for guard function, having: 
    - a name matching any of `possibleGuardNames`
    - no generic parameters
    - a bool return type
    - no parameters or a set of parameters corresponding to the action method

##### Returns

A MethodInfo, if found; null otherwise

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.ActionExecutionContext](#T-Caliburn-Micro-ActionExecutionContext 'Caliburn.Micro.ActionExecutionContext') | The execution context |
| possibleGuardNames | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | Method names to look for. |

<a name='M-Caliburn-Micro-ActionMessage-UpdateAvailability'></a>
### UpdateAvailability() `method`

##### Summary

Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-AttachedCollection`1'></a>
## AttachedCollection\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

A collection that can exist as part of a behavior.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of item in the attached collection. |

<a name='M-Caliburn-Micro-AttachedCollection`1-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates an instance of [AttachedCollection\`1](#T-Caliburn-Micro-AttachedCollection`1 'Caliburn.Micro.AttachedCollection`1')

##### Parameters

This constructor has no parameters.

<a name='P-Caliburn-Micro-AttachedCollection`1-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

The currently attached object.

<a name='M-Caliburn-Micro-AttachedCollection`1-Attach-Windows-UI-Xaml-DependencyObject-'></a>
### Attach(dependencyObject) `method`

##### Summary

Attaches the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The dependency object to attach the collection to. |

<a name='M-Caliburn-Micro-AttachedCollection`1-Detach'></a>
### Detach() `method`

##### Summary

Detaches the collection.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-AttachedCollection`1-OnItemAdded-Windows-UI-Xaml-DependencyObject-'></a>
### OnItemAdded(item) `method`

##### Summary

Called when an item is added from the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The item that was added. |

<a name='M-Caliburn-Micro-AttachedCollection`1-OnItemRemoved-Windows-UI-Xaml-DependencyObject-'></a>
### OnItemRemoved(item) `method`

##### Summary

Called when an item is removed from the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The item that was removed. |

<a name='T-Caliburn-Micro-Bind'></a>
## Bind `type`

##### Namespace

Caliburn.Micro

##### Summary

Hosts dependency properties for binding.

<a name='F-Caliburn-Micro-Bind-AtDesignTimeProperty'></a>
### AtDesignTimeProperty `constants`

##### Summary

Allows application of conventions at design-time.

<a name='F-Caliburn-Micro-Bind-ModelProperty'></a>
### ModelProperty `constants`

##### Summary

Allows binding on an existing view. Use this on root UserControls, Pages and Windows; not in a DataTemplate.

<a name='F-Caliburn-Micro-Bind-ModelWithoutContextProperty'></a>
### ModelWithoutContextProperty `constants`

##### Summary

Allows binding on an existing view without setting the data context. Use this from within a DataTemplate.

<a name='M-Caliburn-Micro-Bind-GetAtDesignTime-Windows-UI-Xaml-DependencyObject-'></a>
### GetAtDesignTime(dependencyObject) `method`

##### Summary

Gets whether or not conventions are being applied at design-time.

##### Returns

Whether or not conventions are applied.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The ui to apply conventions to. |

<a name='M-Caliburn-Micro-Bind-GetModel-Windows-UI-Xaml-DependencyObject-'></a>
### GetModel(dependencyObject) `method`

##### Summary

Gets the model to bind to.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The dependency object to bind to. |

<a name='M-Caliburn-Micro-Bind-GetModelWithoutContext-Windows-UI-Xaml-DependencyObject-'></a>
### GetModelWithoutContext(dependencyObject) `method`

##### Summary

Gets the model to bind to.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The dependency object to bind to. |

<a name='M-Caliburn-Micro-Bind-SetAtDesignTime-Windows-UI-Xaml-DependencyObject,System-Boolean-'></a>
### SetAtDesignTime(dependencyObject,value) `method`

##### Summary

Sets whether or not do bind conventions at design-time.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The ui to apply conventions to. |
| value | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether or not to apply conventions. |

<a name='M-Caliburn-Micro-Bind-SetModel-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### SetModel(dependencyObject,value) `method`

##### Summary

Sets the model to bind to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The dependency object to bind to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='M-Caliburn-Micro-Bind-SetModelWithoutContext-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### SetModelWithoutContext(dependencyObject,value) `method`

##### Summary

Sets the model to bind to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The dependency object to bind to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='T-Caliburn-Micro-BindingScope'></a>
## BindingScope `type`

##### Namespace

Caliburn.Micro

##### Summary

Provides methods for searching a given scope for named elements.

<a name='F-Caliburn-Micro-BindingScope-FindNamedDescendants'></a>
### FindNamedDescendants `constants`

##### Summary

Finds a set of named [FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') instances in each hop in a [ScopeNamingRoute](#T-Caliburn-Micro-BindingScope-ScopeNamingRoute 'Caliburn.Micro.BindingScope.ScopeNamingRoute').

##### Remarks

Searches all the elements in the [ScopeNamingRoute](#T-Caliburn-Micro-BindingScope-ScopeNamingRoute 'Caliburn.Micro.BindingScope.ScopeNamingRoute') parameter as well as the visual children of 
each of these elements, the [Content](#P-Windows-UI-Xaml-Controls-ContentControl-Content 'Windows.UI.Xaml.Controls.ContentControl.Content'), the `HeaderedContentControl.Header`,
the [Items](#P-Windows-UI-Xaml-Controls-ItemsControl-Items 'Windows.UI.Xaml.Controls.ItemsControl.Items'), or the `HeaderedItemsControl.Header`, if any are found.

<a name='F-Caliburn-Micro-BindingScope-FindScopeNamingRoute'></a>
### FindScopeNamingRoute `constants`

##### Summary

Finds a path of dependency objects which traces through visual anscestry until a root which is `null`,
a [UserControl](#T-Windows-UI-Xaml-Controls-UserControl 'Windows.UI.Xaml.Controls.UserControl'), a `Page` with a dependency object `Page.ContentProperty` value, 
a dependency object with [IsScopeRootProperty](#F-Caliburn-Micro-View-IsScopeRootProperty 'Caliburn.Micro.View.IsScopeRootProperty') set to `true`. [ContentPresenter](#T-Windows-UI-Xaml-Controls-ContentPresenter 'Windows.UI.Xaml.Controls.ContentPresenter')
and [ItemsPresenter](#T-Windows-UI-Xaml-Controls-ItemsPresenter 'Windows.UI.Xaml.Controls.ItemsPresenter') are included in the resulting [ScopeNamingRoute](#T-Caliburn-Micro-BindingScope-ScopeNamingRoute 'Caliburn.Micro.BindingScope.ScopeNamingRoute') in order to track which item
in an items control we are scoped to.

<a name='F-Caliburn-Micro-BindingScope-GetNamedElements'></a>
### GetNamedElements `constants`

##### Summary

Gets all the [FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') instances with names in the scope.

##### Returns

Named [FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') instances in the provided scope.

##### Remarks

Pass in a [DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') and receive a list of named [FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') instances in the same scope.

<a name='F-Caliburn-Micro-BindingScope-GetVisualParent'></a>
### GetVisualParent `constants`

##### Summary

Gets the parent of the given object in the Visual Tree.

##### Returns

The parent of the given object in the Visual Tree

<a name='M-Caliburn-Micro-BindingScope-AddChildResolver-System-Func{System-Type,System-Boolean},System-Func{Windows-UI-Xaml-DependencyObject,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}-'></a>
### AddChildResolver(filter,resolver) `method`

##### Summary

Adds a child resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| filter | [System.Func{System.Type,System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Type,System.Boolean}') | The type filter. |
| resolver | [System.Func{Windows.UI.Xaml.DependencyObject,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{Windows.UI.Xaml.DependencyObject,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}') | The resolver. |

<a name='M-Caliburn-Micro-BindingScope-AddChildResolver``1-System-Func{``0,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}-'></a>
### AddChildResolver\`\`1(resolver) `method`

##### Summary

Adds a child resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolver | [System.Func{\`\`0,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}') | The resolver. |

<a name='M-Caliburn-Micro-BindingScope-FindName-System-Collections-Generic-IEnumerable{Windows-UI-Xaml-FrameworkElement},System-String-'></a>
### FindName(elementsToSearch,name) `method`

##### Summary

Searches through the list of named elements looking for a case-insensitive match.

##### Returns

The named element or null if not found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| elementsToSearch | [System.Collections.Generic.IEnumerable{Windows.UI.Xaml.FrameworkElement}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{Windows.UI.Xaml.FrameworkElement}') | The named elements to search through. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name to search for. |

<a name='M-Caliburn-Micro-BindingScope-RemoveChildResolver-Caliburn-Micro-ChildResolver-'></a>
### RemoveChildResolver(resolver) `method`

##### Summary

Removes a child resolver.

##### Returns

true, when the resolver was (found and) removed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolver | [Caliburn.Micro.ChildResolver](#T-Caliburn-Micro-ChildResolver 'Caliburn.Micro.ChildResolver') | The resolver to remove. |

<a name='T-Caliburn-Micro-BooleanToVisibilityConverter'></a>
## BooleanToVisibilityConverter `type`

##### Namespace

Caliburn.Micro

##### Summary

An [IValueConverter](#T-Windows-UI-Xaml-Data-IValueConverter 'Windows.UI.Xaml.Data.IValueConverter') which converts [Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') to [Visibility](#T-Windows-UI-Xaml-Visibility 'Windows.UI.Xaml.Visibility').

<a name='M-Caliburn-Micro-BooleanToVisibilityConverter-Convert-System-Object,System-Type,System-Object,System-String-'></a>
### Convert(value,targetType,parameter,language) `method`

##### Summary

Converts a boolean value to a [Visibility](#T-Windows-UI-Xaml-Visibility 'Windows.UI.Xaml.Visibility') value.

##### Returns

A converted value. If the method returns null, the valid null value is used.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The value produced by the binding source. |
| targetType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the binding target property. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The converter parameter to use. |
| language | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The language to use in the converter. |

<a name='M-Caliburn-Micro-BooleanToVisibilityConverter-ConvertBack-System-Object,System-Type,System-Object,System-String-'></a>
### ConvertBack(value,targetType,parameter,language) `method`

##### Summary

Converts a value [Visibility](#T-Windows-UI-Xaml-Visibility 'Windows.UI.Xaml.Visibility') value to a boolean value.

##### Returns

A converted value. If the method returns null, the valid null value is used.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The value that is produced by the binding target. |
| targetType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type to convert to. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The converter parameter to use. |
| language | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The language to use in the converter. |

<a name='T-Caliburn-Micro-CachingFrameAdapter'></a>
## CachingFrameAdapter `type`

##### Namespace

Caliburn.Micro

##### Summary

A basic implementation of [INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') designed to adapt the [Frame](#T-Windows-UI-Xaml-Controls-Frame 'Windows.UI.Xaml.Controls.Frame') control.

<a name='M-Caliburn-Micro-CachingFrameAdapter-#ctor-Windows-UI-Xaml-Controls-Frame,System-Boolean-'></a>
### #ctor(frame,treatViewAsLoaded) `constructor`

##### Summary

Creates an instance of [FrameAdapter](#T-Caliburn-Micro-FrameAdapter 'Caliburn.Micro.FrameAdapter').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| frame | [Windows.UI.Xaml.Controls.Frame](#T-Windows-UI-Xaml-Controls-Frame 'Windows.UI.Xaml.Controls.Frame') | The frame to represent as a [INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService'). |
| treatViewAsLoaded | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called.
This is necessary when using the TransitionFrame. |

<a name='P-Caliburn-Micro-CachingFrameAdapter-BackStack'></a>
### BackStack `property`

##### Summary

Gets a collection of PageStackEntry instances representing the backward navigation history of the Frame.

<a name='P-Caliburn-Micro-CachingFrameAdapter-ForwardStack'></a>
### ForwardStack `property`

##### Summary

Gets a collection of PageStackEntry instances representing the forward navigation history of the Frame.

<a name='M-Caliburn-Micro-CachingFrameAdapter-OnNavigated-System-Object,Windows-UI-Xaml-Navigation-NavigationEventArgs-'></a>
### OnNavigated(sender,e) `method`

##### Summary

Occurs after navigation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event sender. |
| e | [Windows.UI.Xaml.Navigation.NavigationEventArgs](#T-Windows-UI-Xaml-Navigation-NavigationEventArgs 'Windows.UI.Xaml.Navigation.NavigationEventArgs') | The event args. |

<a name='M-Caliburn-Micro-CachingFrameAdapter-OnNavigating-System-Object,Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs-'></a>
### OnNavigating(sender,e) `method`

##### Summary

Occurs before navigation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event sender. |
| e | [Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs](#T-Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs 'Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs') | The event args. |

<a name='T-Caliburn-Micro-CaliburnApplication'></a>
## CaliburnApplication `type`

##### Namespace

Caliburn.Micro

##### Summary

Encapsulates the app and its available services.

<a name='P-Caliburn-Micro-CaliburnApplication-RootFrame'></a>
### RootFrame `property`

##### Summary

The root frame of the application.

<a name='M-Caliburn-Micro-CaliburnApplication-BuildUp-System-Object-'></a>
### BuildUp(instance) `method`

##### Summary

Override this to provide an IoC specific implementation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| instance | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to perform injection on. |

<a name='M-Caliburn-Micro-CaliburnApplication-Configure'></a>
### Configure() `method`

##### Summary

Override to configure the framework and setup your IoC container.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-CaliburnApplication-CreateApplicationFrame'></a>
### CreateApplicationFrame() `method`

##### Summary

Creates the root frame used by the application.

##### Returns

The frame.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-CaliburnApplication-DisplayRootView-System-Type,System-Object-'></a>
### DisplayRootView(viewType,paramter) `method`

##### Summary

Creates the root frame and navigates to the specified view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The view type to navigate to. |
| paramter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object parameter to pass to the target. |

<a name='M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync-System-Type,System-Threading-CancellationToken-'></a>
### DisplayRootViewForAsync(viewModelType,cancellationToken) `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The view model type. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync-System-Type-'></a>
### DisplayRootViewForAsync(viewModelType) `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The view model type. |

<a name='M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync``1-System-Threading-CancellationToken-'></a>
### DisplayRootViewForAsync\`\`1(cancellationToken) `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view model type. |

<a name='M-Caliburn-Micro-CaliburnApplication-DisplayRootViewForAsync``1'></a>
### DisplayRootViewForAsync\`\`1() `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view model type. |

<a name='M-Caliburn-Micro-CaliburnApplication-DisplayRootView``1-System-Object-'></a>
### DisplayRootView\`\`1(parameter) `method`

##### Summary

Creates the root frame and navigates to the specified view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object parameter to pass to the target. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view type to navigate to. |

<a name='M-Caliburn-Micro-CaliburnApplication-GetAllInstances-System-Type-'></a>
### GetAllInstances(service) `method`

##### Summary

Override this to provide an IoC specific implementation

##### Returns

The located services.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service to locate. |

<a name='M-Caliburn-Micro-CaliburnApplication-GetInstance-System-Type,System-String-'></a>
### GetInstance(service,key) `method`

##### Summary

Override this to provide an IoC specific implementation.

##### Returns

The located service.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service to locate. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key to locate. |

<a name='M-Caliburn-Micro-CaliburnApplication-Initialize'></a>
### Initialize() `method`

##### Summary

Start the framework.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-CaliburnApplication-OnResuming-System-Object,System-Object-'></a>
### OnResuming(sender,e) `method`

##### Summary

Override this to add custom behavior when the application transitions from Suspended state to Running state.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The sender. |
| e | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event args. |

<a name='M-Caliburn-Micro-CaliburnApplication-OnSuspending-System-Object,Windows-ApplicationModel-SuspendingEventArgs-'></a>
### OnSuspending(sender,e) `method`

##### Summary

Override this to add custom behavior when the application transitions to Suspended state from some other state.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The sender. |
| e | [Windows.ApplicationModel.SuspendingEventArgs](#T-Windows-ApplicationModel-SuspendingEventArgs 'Windows.ApplicationModel.SuspendingEventArgs') | The event args. |

<a name='M-Caliburn-Micro-CaliburnApplication-OnUnhandledException-System-Object,Windows-UI-Xaml-UnhandledExceptionEventArgs-'></a>
### OnUnhandledException(sender,e) `method`

##### Summary

Override this to add custom behavior for unhandled exceptions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The sender. |
| e | [Windows.UI.Xaml.UnhandledExceptionEventArgs](#T-Windows-UI-Xaml-UnhandledExceptionEventArgs 'Windows.UI.Xaml.UnhandledExceptionEventArgs') | The event args. |

<a name='M-Caliburn-Micro-CaliburnApplication-OnWindowCreated-Windows-UI-Xaml-WindowCreatedEventArgs-'></a>
### OnWindowCreated(args) `method`

##### Summary

Invoked when the application creates a window.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [Windows.UI.Xaml.WindowCreatedEventArgs](#T-Windows-UI-Xaml-WindowCreatedEventArgs 'Windows.UI.Xaml.WindowCreatedEventArgs') | Event data for the event. |

<a name='M-Caliburn-Micro-CaliburnApplication-PrepareApplication'></a>
### PrepareApplication() `method`

##### Summary

Provides an opportunity to hook into the application object.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-CaliburnApplication-PrepareViewFirst'></a>
### PrepareViewFirst() `method`

##### Summary

Allows you to trigger the creation of the RootFrame from Configure if necessary.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-CaliburnApplication-PrepareViewFirst-Windows-UI-Xaml-Controls-Frame-'></a>
### PrepareViewFirst(rootFrame) `method`

##### Summary

Override this to register a navigation service.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rootFrame | [Windows.UI.Xaml.Controls.Frame](#T-Windows-UI-Xaml-Controls-Frame 'Windows.UI.Xaml.Controls.Frame') | The root frame of the application. |

<a name='M-Caliburn-Micro-CaliburnApplication-SelectAssemblies'></a>
### SelectAssemblies() `method`

##### Summary

Override to tell the framework where to find assemblies to inspect for views, etc.

##### Returns

A list of assemblies to inspect.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-CaliburnApplication-StartDesignTime'></a>
### StartDesignTime() `method`

##### Summary

Called by the bootstrapper's constructor at design time to start the framework.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-CaliburnApplication-StartRuntime'></a>
### StartRuntime() `method`

##### Summary

Called by the bootstrapper's constructor at runtime to start the framework.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-ChildResolver'></a>
## ChildResolver `type`

##### Namespace

Caliburn.Micro

##### Summary

Represents a resolver that takes a control and returns it's children

<a name='M-Caliburn-Micro-ChildResolver-#ctor-System-Func{System-Type,System-Boolean},System-Func{Windows-UI-Xaml-DependencyObject,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}-'></a>
### #ctor(filter,resolver) `constructor`

##### Summary

Creates the ChildResolver using the given anonymous methods.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| filter | [System.Func{System.Type,System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Type,System.Boolean}') | The filter |
| resolver | [System.Func{Windows.UI.Xaml.DependencyObject,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{Windows.UI.Xaml.DependencyObject,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}') | The resolver |

<a name='M-Caliburn-Micro-ChildResolver-CanResolve-System-Type-'></a>
### CanResolve(type) `method`

##### Summary

Can this resolve appy to the given type.

##### Returns

Returns true if this resolver applies.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The visual tree type. |

<a name='M-Caliburn-Micro-ChildResolver-Resolve-Windows-UI-Xaml-DependencyObject-'></a>
### Resolve(obj) `method`

##### Summary

The element from the visual tree for the children to resolve.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') |  |

<a name='T-Caliburn-Micro-ChildResolver`1'></a>
## ChildResolver\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Generic strongly typed child resolver

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type to filter on |

<a name='M-Caliburn-Micro-ChildResolver`1-#ctor-System-Func{`0,System-Collections-Generic-IEnumerable{Windows-UI-Xaml-DependencyObject}}-'></a>
### #ctor(resolver) `constructor`

##### Summary

Creates a

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolver | [System.Func{\`0,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{`0,System.Collections.Generic.IEnumerable{Windows.UI.Xaml.DependencyObject}}') |  |

<a name='T-Caliburn-Micro-ConventionManager'></a>
## ConventionManager `type`

##### Namespace

Caliburn.Micro

##### Summary

Used to configure the conventions used by the framework to apply bindings and create actions.

<a name='F-Caliburn-Micro-ConventionManager-ApplyBindingMode'></a>
### ApplyBindingMode `constants`

##### Summary

Applies the appropriate binding mode to the binding.

<a name='F-Caliburn-Micro-ConventionManager-ApplyStringFormat'></a>
### ApplyStringFormat `constants`

##### Summary

Determines whether a custom string format is needed and applies it to the binding.

<a name='F-Caliburn-Micro-ConventionManager-ApplyUpdateSourceTrigger'></a>
### ApplyUpdateSourceTrigger `constants`

##### Summary

Determines whether a custom update source trigger should be applied to the binding.

<a name='F-Caliburn-Micro-ConventionManager-ApplyValidation'></a>
### ApplyValidation `constants`

##### Summary

Determines whether or not and what type of validation to enable on the binding.

<a name='F-Caliburn-Micro-ConventionManager-ApplyValueConverter'></a>
### ApplyValueConverter `constants`

##### Summary

Determines whether a value converter is is needed and applies one to the binding.

<a name='F-Caliburn-Micro-ConventionManager-BooleanToVisibilityConverter'></a>
### BooleanToVisibilityConverter `constants`

##### Summary

Converters [Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') to/from [Visibility](#T-Windows-UI-Xaml-Visibility 'Windows.UI.Xaml.Visibility').

<a name='F-Caliburn-Micro-ConventionManager-ConfigureSelectedItem'></a>
### ConfigureSelectedItem `constants`

##### Summary

Configures the selected item convention.

<a name='F-Caliburn-Micro-ConventionManager-ConfigureSelectedItemBinding'></a>
### ConfigureSelectedItemBinding `constants`

##### Summary

Configures the SelectedItem binding for matched selection path.

<a name='F-Caliburn-Micro-ConventionManager-DefaultHeaderTemplate'></a>
### DefaultHeaderTemplate `constants`

##### Summary

The default DataTemplate used for Headered controls when required.

<a name='F-Caliburn-Micro-ConventionManager-DefaultItemTemplate'></a>
### DefaultItemTemplate `constants`

##### Summary

The default DataTemplate used for ItemsControls when required.

<a name='F-Caliburn-Micro-ConventionManager-DerivePotentialSelectionNames'></a>
### DerivePotentialSelectionNames `constants`

##### Summary

Derives the SelectedItem property name.

<a name='F-Caliburn-Micro-ConventionManager-IncludeStaticProperties'></a>
### IncludeStaticProperties `constants`

##### Summary

Indicates whether or not static properties should be included during convention name matching.

##### Remarks

False by default.

<a name='F-Caliburn-Micro-ConventionManager-OverwriteContent'></a>
### OverwriteContent `constants`

##### Summary

Indicates whether or not the Content of ContentControls should be overwritten by conventional bindings.

##### Remarks

False by default.

<a name='F-Caliburn-Micro-ConventionManager-SetBinding'></a>
### SetBinding `constants`

##### Summary

Creates a binding and sets it on the element, applying the appropriate conventions.

<a name='F-Caliburn-Micro-ConventionManager-Singularize'></a>
### Singularize `constants`

##### Summary

Changes the provided word from a plural form to a singular form.

<a name='M-Caliburn-Micro-ConventionManager-AddElementConvention-Caliburn-Micro-ElementConvention-'></a>
### AddElementConvention(convention) `method`

##### Summary

Adds an element convention.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| convention | [Caliburn.Micro.ElementConvention](#T-Caliburn-Micro-ElementConvention 'Caliburn.Micro.ElementConvention') |  |

<a name='M-Caliburn-Micro-ConventionManager-AddElementConvention``1-Windows-UI-Xaml-DependencyProperty,System-String,System-String-'></a>
### AddElementConvention\`\`1(bindableProperty,parameterProperty,eventName) `method`

##### Summary

Adds an element convention.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bindableProperty | [Windows.UI.Xaml.DependencyProperty](#T-Windows-UI-Xaml-DependencyProperty 'Windows.UI.Xaml.DependencyProperty') | The default property for binding conventions. |
| parameterProperty | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The default property for action parameters. |
| eventName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The default event to trigger actions. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of element. |

<a name='M-Caliburn-Micro-ConventionManager-ApplyHeaderTemplate-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-DependencyProperty,Windows-UI-Xaml-DependencyProperty,System-Type-'></a>
### ApplyHeaderTemplate(element,headerTemplateProperty,headerTemplateSelectorProperty,viewModelType) `method`

##### Summary

Applies a header template based on [IHaveDisplayName](#T-Caliburn-Micro-IHaveDisplayName 'Caliburn.Micro.IHaveDisplayName')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The element to apply the header template to. |
| headerTemplateProperty | [Windows.UI.Xaml.DependencyProperty](#T-Windows-UI-Xaml-DependencyProperty 'Windows.UI.Xaml.DependencyProperty') | The depdendency property for the hdeader. |
| headerTemplateSelectorProperty | [Windows.UI.Xaml.DependencyProperty](#T-Windows-UI-Xaml-DependencyProperty 'Windows.UI.Xaml.DependencyProperty') | The selector dependency property. |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the view model. |

<a name='M-Caliburn-Micro-ConventionManager-ApplyItemTemplate-Windows-UI-Xaml-Controls-ItemsControl,System-Reflection-PropertyInfo-'></a>
### ApplyItemTemplate(itemsControl,property) `method`

##### Summary

Attempts to apply the default item template to the items control.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| itemsControl | [Windows.UI.Xaml.Controls.ItemsControl](#T-Windows-UI-Xaml-Controls-ItemsControl 'Windows.UI.Xaml.Controls.ItemsControl') | The items control. |
| property | [System.Reflection.PropertyInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.PropertyInfo 'System.Reflection.PropertyInfo') | The collection property. |

<a name='M-Caliburn-Micro-ConventionManager-GetElementConvention-System-Type-'></a>
### GetElementConvention(elementType) `method`

##### Summary

Gets an element convention for the provided element type.

##### Returns

The convention if found, null otherwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| elementType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of element to locate the convention for. |

##### Remarks

Searches the class hierarchy for conventions.

<a name='M-Caliburn-Micro-ConventionManager-GetPropertyCaseInsensitive-System-Type,System-String-'></a>
### GetPropertyCaseInsensitive(type,propertyName) `method`

##### Summary

Gets a property by name, ignoring case and searching all interfaces.

##### Returns

The property or null if not found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type to inspect. |
| propertyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The property to search for. |

<a name='M-Caliburn-Micro-ConventionManager-HasBinding-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-DependencyProperty-'></a>
### HasBinding() `method`

##### Summary

Determines whether a particular dependency property already has a binding on the provided element.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-ConventionManager-SetBindingWithoutBindingOrValueOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Windows-UI-Xaml-FrameworkElement,Caliburn-Micro-ElementConvention,Windows-UI-Xaml-DependencyProperty-'></a>
### SetBindingWithoutBindingOrValueOverwrite(viewModelType,path,property,element,convention,bindableProperty) `method`

##### Summary

Creates a binding and set it on the element, guarding against pre-existing bindings and pre-existing values.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| property | [System.Reflection.PropertyInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.PropertyInfo 'System.Reflection.PropertyInfo') |  |
| element | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') |  |
| convention | [Caliburn.Micro.ElementConvention](#T-Caliburn-Micro-ElementConvention 'Caliburn.Micro.ElementConvention') |  |
| bindableProperty | [Windows.UI.Xaml.DependencyProperty](#T-Windows-UI-Xaml-DependencyProperty 'Windows.UI.Xaml.DependencyProperty') |  |

<a name='M-Caliburn-Micro-ConventionManager-SetBindingWithoutBindingOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Windows-UI-Xaml-FrameworkElement,Caliburn-Micro-ElementConvention,Windows-UI-Xaml-DependencyProperty-'></a>
### SetBindingWithoutBindingOverwrite() `method`

##### Summary

Creates a binding and sets it on the element, guarding against pre-existing bindings.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-DependencyPropertyHelper'></a>
## DependencyPropertyHelper `type`

##### Namespace

Caliburn.Micro

##### Summary

Class that abstracts the differences in creating a DepedencyProperty / BindableProperty on the different platforms.

<a name='M-Caliburn-Micro-DependencyPropertyHelper-Register-System-String,System-Type,System-Type,System-Object,Windows-UI-Xaml-PropertyChangedCallback-'></a>
### Register(name,propertyType,ownerType,defaultValue,propertyChangedCallback) `method`

##### Summary

Register a dependency / bindable property

##### Returns

The registred dependecy property

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The property name |
| propertyType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The property type |
| ownerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The owner type |
| defaultValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The default value |
| propertyChangedCallback | [Windows.UI.Xaml.PropertyChangedCallback](#T-Windows-UI-Xaml-PropertyChangedCallback 'Windows.UI.Xaml.PropertyChangedCallback') | Callback to executed on property changed |

<a name='M-Caliburn-Micro-DependencyPropertyHelper-RegisterAttached-System-String,System-Type,System-Type,System-Object,Windows-UI-Xaml-PropertyChangedCallback-'></a>
### RegisterAttached(name,propertyType,ownerType,defaultValue,propertyChangedCallback) `method`

##### Summary

Register an attached dependency / bindable property

##### Returns

The registred attached dependecy property

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The property name |
| propertyType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The property type |
| ownerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The owner type |
| defaultValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The default value |
| propertyChangedCallback | [Windows.UI.Xaml.PropertyChangedCallback](#T-Windows-UI-Xaml-PropertyChangedCallback 'Windows.UI.Xaml.PropertyChangedCallback') | Callback to executed on property changed |

<a name='T-Caliburn-Micro-DispatcherTaskExtensions'></a>
## DispatcherTaskExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

DispatcherTaskExtensions class.

##### Remarks

Contains helper functions to run  tasks asynchronously on a [CoreDispatcher](#T-Windows-UI-Core-CoreDispatcher 'Windows.UI.Core.CoreDispatcher').

<a name='M-Caliburn-Micro-DispatcherTaskExtensions-RunTaskAsync-Windows-UI-Core-CoreDispatcher,System-Func{System-Threading-Tasks-Task},Windows-UI-Core-CoreDispatcherPriority-'></a>
### RunTaskAsync(dispatcher,func,priority) `method`

##### Summary

Runs a task asynchronously on the specified [CoreDispatcher](#T-Windows-UI-Core-CoreDispatcher 'Windows.UI.Core.CoreDispatcher').

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dispatcher | [Windows.UI.Core.CoreDispatcher](#T-Windows-UI-Core-CoreDispatcher 'Windows.UI.Core.CoreDispatcher') | The dispatcher on which to run the task. |
| func | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') | The function that returns the task to be run. |
| priority | [Windows.UI.Core.CoreDispatcherPriority](#T-Windows-UI-Core-CoreDispatcherPriority 'Windows.UI.Core.CoreDispatcherPriority') | The priority with which the task should be run. |

##### Remarks

There is no [TaskCompletionSource\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.TaskCompletionSource`1 'System.Threading.Tasks.TaskCompletionSource`1') so a [Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') is used and discarded.

<a name='M-Caliburn-Micro-DispatcherTaskExtensions-RunTaskAsync``1-Windows-UI-Core-CoreDispatcher,System-Func{System-Threading-Tasks-Task{``0}},Windows-UI-Core-CoreDispatcherPriority-'></a>
### RunTaskAsync\`\`1(dispatcher,func,priority) `method`

##### Summary

Runs a task asynchronously on the specified [CoreDispatcher](#T-Windows-UI-Core-CoreDispatcher 'Windows.UI.Core.CoreDispatcher').

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dispatcher | [Windows.UI.Core.CoreDispatcher](#T-Windows-UI-Core-CoreDispatcher 'Windows.UI.Core.CoreDispatcher') | The dispatcher on which to run the task. |
| func | [System.Func{System.Threading.Tasks.Task{\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task{``0}}') | The function that returns the task to be run. |
| priority | [Windows.UI.Core.CoreDispatcherPriority](#T-Windows-UI-Core-CoreDispatcherPriority 'Windows.UI.Core.CoreDispatcherPriority') | The priority with which the task should be run. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the result produced by the task. |

<a name='T-Caliburn-Micro-ElementConvention'></a>
## ElementConvention `type`

##### Namespace

Caliburn.Micro

##### Summary

Represents the conventions for a particular element type.

<a name='F-Caliburn-Micro-ElementConvention-ApplyBinding'></a>
### ApplyBinding `constants`

##### Summary

Applies custom conventions for elements of this type.

##### Remarks

Pass the view model type, property path, property instance, framework element and its convention.

<a name='F-Caliburn-Micro-ElementConvention-CreateTrigger'></a>
### CreateTrigger `constants`

##### Summary

The default trigger to be used when wiring actions on this element.

<a name='F-Caliburn-Micro-ElementConvention-ElementType'></a>
### ElementType `constants`

##### Summary

The type of element to which the conventions apply.

<a name='F-Caliburn-Micro-ElementConvention-GetBindableProperty'></a>
### GetBindableProperty `constants`

##### Summary

Gets the default property to be used in binding conventions.

<a name='F-Caliburn-Micro-ElementConvention-ParameterProperty'></a>
### ParameterProperty `constants`

##### Summary

The default property to be used for parameters of this type in actions.

<a name='T-Caliburn-Micro-FrameAdapter'></a>
## FrameAdapter `type`

##### Namespace

Caliburn.Micro

##### Summary

A basic implementation of [INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') designed to adapt the [Frame](#T-Windows-UI-Xaml-Controls-Frame 'Windows.UI.Xaml.Controls.Frame') control.

<a name='M-Caliburn-Micro-FrameAdapter-#ctor-Windows-UI-Xaml-Controls-Frame,System-Boolean-'></a>
### #ctor(frame,treatViewAsLoaded) `constructor`

##### Summary

Creates an instance of [FrameAdapter](#T-Caliburn-Micro-FrameAdapter 'Caliburn.Micro.FrameAdapter').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| frame | [Windows.UI.Xaml.Controls.Frame](#T-Windows-UI-Xaml-Controls-Frame 'Windows.UI.Xaml.Controls.Frame') | The frame to represent as a [INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService'). |
| treatViewAsLoaded | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called.
This is necessary when using the TransitionFrame. |

<a name='P-Caliburn-Micro-FrameAdapter-BackStack'></a>
### BackStack `property`

##### Summary

Gets a collection of PageStackEntry instances representing the backward navigation history of the Frame.

<a name='P-Caliburn-Micro-FrameAdapter-CanGoBack'></a>
### CanGoBack `property`

##### Summary

Indicates whether the navigator can navigate back.

<a name='P-Caliburn-Micro-FrameAdapter-CanGoForward'></a>
### CanGoForward `property`

##### Summary

Indicates whether the navigator can navigate forward.

<a name='P-Caliburn-Micro-FrameAdapter-CurrentParameter'></a>
### CurrentParameter `property`

##### Summary

The parameter to the current view

<a name='P-Caliburn-Micro-FrameAdapter-CurrentSourcePageType'></a>
### CurrentSourcePageType `property`

##### Summary

Gets the data type of the content that is currently displayed.

<a name='P-Caliburn-Micro-FrameAdapter-ForwardStack'></a>
### ForwardStack `property`

##### Summary

Gets a collection of PageStackEntry instances representing the forward navigation history of the Frame.

<a name='P-Caliburn-Micro-FrameAdapter-SourcePageType'></a>
### SourcePageType `property`

##### Summary

Gets or sets the data type of the current content, or the content that should be navigated to.

<a name='M-Caliburn-Micro-FrameAdapter-BindViewModel-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### BindViewModel(view,viewModel) `method`

##### Summary

Binds the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The view. |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model. |

<a name='M-Caliburn-Micro-FrameAdapter-CanCloseOnNavigating-System-Object,Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs-'></a>
### CanCloseOnNavigating(sender,e) `method`

##### Summary

Called to check whether or not to close current instance on navigating.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event sender from OnNavigating event. |
| e | [Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs](#T-Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs 'Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs') | The event args from OnNavigating event. |

<a name='M-Caliburn-Micro-FrameAdapter-Dispose'></a>
### Dispose() `method`

##### Summary

Disposes the FrameAdapter instance, detaching event handlers to prevent memory leaks.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-FrameAdapter-GoBack'></a>
### GoBack() `method`

##### Summary

Navigates back.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-FrameAdapter-GoForward'></a>
### GoForward() `method`

##### Summary

Navigates forward.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-FrameAdapter-Navigate-System-Type-'></a>
### Navigate(sourcePageType) `method`

##### Summary

Navigates to the specified content.

##### Returns

Whether or not navigation succeeded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sourcePageType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The [Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') to navigate to. |

<a name='M-Caliburn-Micro-FrameAdapter-Navigate-System-Type,System-Object-'></a>
### Navigate(sourcePageType,parameter) `method`

##### Summary

Navigates to the specified content.

##### Returns

Whether or not navigation succeeded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sourcePageType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The [Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') to navigate to. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object parameter to pass to the target. |

<a name='M-Caliburn-Micro-FrameAdapter-OnBackRequested-Windows-UI-Core-BackRequestedEventArgs-'></a>
### OnBackRequested(e) `method`

##### Summary

Occurs when the user presses the hardware Back button. Allows the handlers to cancel the default behavior.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| e | [Windows.UI.Core.BackRequestedEventArgs](#T-Windows-UI-Core-BackRequestedEventArgs 'Windows.UI.Core.BackRequestedEventArgs') | The event arguments |

<a name='M-Caliburn-Micro-FrameAdapter-OnNavigated-System-Object,Windows-UI-Xaml-Navigation-NavigationEventArgs-'></a>
### OnNavigated(sender,e) `method`

##### Summary

Occurs after navigation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event sender. |
| e | [Windows.UI.Xaml.Navigation.NavigationEventArgs](#T-Windows-UI-Xaml-Navigation-NavigationEventArgs 'Windows.UI.Xaml.Navigation.NavigationEventArgs') | The event args. |

<a name='M-Caliburn-Micro-FrameAdapter-OnNavigating-System-Object,Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs-'></a>
### OnNavigating(sender,e) `method`

##### Summary

Occurs before navigation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event sender. |
| e | [Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs](#T-Windows-UI-Xaml-Navigation-NavigatingCancelEventArgs 'Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs') | The event args. |

<a name='M-Caliburn-Micro-FrameAdapter-ResumeStateAsync'></a>
### ResumeStateAsync() `method`

##### Summary

Tries to restore the frame navigation state from local settings.

##### Returns

Whether the restoration of successful.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-FrameAdapter-SuspendState'></a>
### SuspendState() `method`

##### Summary

Stores the frame navigation state in local settings if it can.

##### Returns

Whether the suspension was sucessful

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-FrameAdapter-TryInjectParameters-System-Object,System-Object-'></a>
### TryInjectParameters(viewModel,parameter) `method`

##### Summary

Attempts to inject query string parameters from the view into the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The parameter. |

<a name='T-Caliburn-Micro-IAttachedObject'></a>
## IAttachedObject `type`

##### Namespace

Caliburn.Micro

##### Summary

Interaface usually from the Interactivity SDK's included here for completeness.

<a name='P-Caliburn-Micro-IAttachedObject-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

The currently attached object.

<a name='M-Caliburn-Micro-IAttachedObject-Attach-Windows-UI-Xaml-DependencyObject-'></a>
### Attach(dependencyObject) `method`

##### Summary

Attached the specified dependency object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') |  |

<a name='M-Caliburn-Micro-IAttachedObject-Detach'></a>
### Detach() `method`

##### Summary

Detach from the previously attached object.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-IHaveParameters'></a>
## IHaveParameters `type`

##### Namespace

Caliburn.Micro

##### Summary

Indicates that a message is parameterized.

<a name='P-Caliburn-Micro-IHaveParameters-Parameters'></a>
### Parameters `property`

##### Summary

Represents the parameters of a message.

<a name='T-Caliburn-Micro-INavigationService'></a>
## INavigationService `type`

##### Namespace

Caliburn.Micro

##### Summary

Implemented by services that provide ([Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') based) navigation.

<a name='P-Caliburn-Micro-INavigationService-BackStack'></a>
### BackStack `property`

##### Summary

Gets a collection of PageStackEntry instances representing the backward navigation history of the Frame.

<a name='P-Caliburn-Micro-INavigationService-CanGoBack'></a>
### CanGoBack `property`

##### Summary

Indicates whether the navigator can navigate back.

<a name='P-Caliburn-Micro-INavigationService-CanGoForward'></a>
### CanGoForward `property`

##### Summary

Indicates whether the navigator can navigate forward.

<a name='P-Caliburn-Micro-INavigationService-CurrentSourcePageType'></a>
### CurrentSourcePageType `property`

##### Summary

Gets the data type of the content that is currently displayed.

<a name='P-Caliburn-Micro-INavigationService-ForwardStack'></a>
### ForwardStack `property`

##### Summary

Gets a collection of PageStackEntry instances representing the forward navigation history of the Frame.

<a name='P-Caliburn-Micro-INavigationService-SourcePageType'></a>
### SourcePageType `property`

##### Summary

Gets or sets the data type of the current content, or the content that should be navigated to.

<a name='M-Caliburn-Micro-INavigationService-GoBack'></a>
### GoBack() `method`

##### Summary

Navigates back.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-INavigationService-GoForward'></a>
### GoForward() `method`

##### Summary

Navigates forward.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-INavigationService-Navigate-System-Type-'></a>
### Navigate(sourcePageType) `method`

##### Summary

Navigates to the specified content.

##### Returns

Whether or not navigation succeeded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sourcePageType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The [Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') to navigate to. |

<a name='M-Caliburn-Micro-INavigationService-Navigate-System-Type,System-Object-'></a>
### Navigate(sourcePageType,parameter) `method`

##### Summary

Navigates to the specified content.

##### Returns

Whether or not navigation succeeded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sourcePageType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The [Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') to navigate to. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object parameter to pass to the target. |

<a name='M-Caliburn-Micro-INavigationService-ResumeStateAsync'></a>
### ResumeStateAsync() `method`

##### Summary

Tries to restore the frame navigation state from local settings.

##### Returns

Whether the restoration of successful.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-INavigationService-SuspendState'></a>
### SuspendState() `method`

##### Summary

Stores the frame navigation state in local settings if it can.

##### Returns

Whether the suspension was sucessful

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-ISharingService'></a>
## ISharingService `type`

##### Namespace

Caliburn.Micro

##### Summary

Service that handles sharing data with the Share Charm.

<a name='M-Caliburn-Micro-ISharingService-ShowShareUI'></a>
### ShowShareUI() `method`

##### Summary

Programmatically initiates the user interface for sharing content with another app.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-ISupportSharing'></a>
## ISupportSharing `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes a class which is aware of sharing data with the Share charm.

<a name='M-Caliburn-Micro-ISupportSharing-OnShareRequested-Windows-ApplicationModel-DataTransfer-DataRequest-'></a>
### OnShareRequested(dataRequest) `method`

##### Summary

Called when a share operation starts.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dataRequest | [Windows.ApplicationModel.DataTransfer.DataRequest](#T-Windows-ApplicationModel-DataTransfer-DataRequest 'Windows.ApplicationModel.DataTransfer.DataRequest') | The data request. |

<a name='T-Caliburn-Micro-Message'></a>
## Message `type`

##### Namespace

Caliburn.Micro

##### Summary

Host's attached properties related to routed UI messaging.

<a name='F-Caliburn-Micro-Message-AttachProperty'></a>
### AttachProperty `constants`

##### Summary

A property definition representing attached triggers and messages.

<a name='M-Caliburn-Micro-Message-GetAttach-Windows-UI-Xaml-DependencyObject-'></a>
### GetAttach(d) `method`

##### Summary

Gets the attached triggers and messages.

##### Returns

The parsable attachment text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element that was attached to. |

<a name='M-Caliburn-Micro-Message-GetHandler-Windows-UI-Xaml-DependencyObject-'></a>
### GetHandler(d) `method`

##### Summary

Gets the message handler for this element.

##### Returns

The message handler.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element. |

<a name='M-Caliburn-Micro-Message-SetAttach-Windows-UI-Xaml-DependencyObject,System-String-'></a>
### SetAttach(d,attachText) `method`

##### Summary

Sets the attached triggers and messages.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to attach to. |
| attachText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The parsable attachment text. |

<a name='M-Caliburn-Micro-Message-SetHandler-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### SetHandler(d,value) `method`

##### Summary

Places a message handler on this element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message handler. |

<a name='T-Caliburn-Micro-MessageBinder'></a>
## MessageBinder `type`

##### Namespace

Caliburn.Micro

##### Summary

A service that is capable of properly binding values to a method's parameters and creating instances of [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult').

<a name='F-Caliburn-Micro-MessageBinder-CustomConverters'></a>
### CustomConverters `constants`

##### Summary

Custom converters used by the framework registered by destination type for which they will be selected.
The converter is passed the existing value to convert and a "context" object.

<a name='F-Caliburn-Micro-MessageBinder-EvaluateParameter'></a>
### EvaluateParameter `constants`

##### Summary

Transforms the textual parameter into the actual parameter.

<a name='F-Caliburn-Micro-MessageBinder-SpecialValues'></a>
### SpecialValues `constants`

##### Summary

The special parameter values recognized by the message binder along with their resolvers.
Parameter names are case insensitive so the specified names are unique and can be used with different case variations

<a name='M-Caliburn-Micro-MessageBinder-CoerceValue-System-Type,System-Object,System-Object-'></a>
### CoerceValue(destinationType,providedValue,context) `method`

##### Summary

Coerces the provided value to the destination type.

##### Returns

The coerced value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| destinationType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The destination type. |
| providedValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The provided value. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | An optional context value which can be used during conversion. |

<a name='M-Caliburn-Micro-MessageBinder-DetermineParameters-Caliburn-Micro-ActionExecutionContext,System-Reflection-ParameterInfo[]-'></a>
### DetermineParameters(context,requiredParameters) `method`

##### Summary

Determines the parameters that a method should be invoked with.

##### Returns

The actual parameter values.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.ActionExecutionContext](#T-Caliburn-Micro-ActionExecutionContext 'Caliburn.Micro.ActionExecutionContext') | The action execution context. |
| requiredParameters | [System.Reflection.ParameterInfo[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.ParameterInfo[] 'System.Reflection.ParameterInfo[]') | The parameters required to complete the invocation. |

<a name='M-Caliburn-Micro-MessageBinder-GetDefaultValue-System-Type-'></a>
### GetDefaultValue(type) `method`

##### Summary

Gets the default value for a type.

##### Returns

The default value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type. |

<a name='T-Caliburn-Micro-NavigateHelper`1'></a>
## NavigateHelper\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Builds a Uri in a strongly typed fashion, based on a ViewModel.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TViewModel |  |

<a name='M-Caliburn-Micro-NavigateHelper`1-AttachTo-Caliburn-Micro-INavigationService-'></a>
### AttachTo(navigationService) `method`

##### Summary

Attaches a navigation servies to this builder.

##### Returns

Itself

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') | The navigation service. |

<a name='M-Caliburn-Micro-NavigateHelper`1-BuildUri'></a>
### BuildUri() `method`

##### Summary

Builds the URI.

##### Returns

A uri constructed with the current configuration information.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-NavigateHelper`1-Navigate'></a>
### Navigate() `method`

##### Summary

Navigates to the Uri represented by this builder.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-NavigateHelper`1-WithParam``1-System-Linq-Expressions-Expression{System-Func{`0,``0}},``0-'></a>
### WithParam\`\`1(property,value) `method`

##### Summary

Adds a query string parameter to the Uri.

##### Returns

Itself

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| property | [System.Linq.Expressions.Expression{System.Func{\`0,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{`0,``0}}') | The property. |
| value | [\`\`0](#T-``0 '``0') | The property value. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TValue | The type of the value. |

<a name='T-Caliburn-Micro-NavigationExtensions'></a>
## NavigationExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods for [INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService')

<a name='M-Caliburn-Micro-NavigationExtensions-For``1-Caliburn-Micro-INavigationService-'></a>
### For\`\`1(navigationService) `method`

##### Summary

Creates a Uri builder based on a view model type.

##### Returns

The builder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') | The navigation service. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TViewModel | The type of the view model. |

<a name='M-Caliburn-Micro-NavigationExtensions-NavigateToViewModel-Caliburn-Micro-INavigationService,System-Type,System-Object-'></a>
### NavigateToViewModel(navigationService,viewModelType,parameter) `method`

##### Summary

Navigate to the specified model type.

##### Returns

Whether or not navigation succeeded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') | The navigation service. |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The model type to navigate to. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object parameter to pass to the target. |

<a name='M-Caliburn-Micro-NavigationExtensions-NavigateToViewModel``1-Caliburn-Micro-INavigationService,System-Object-'></a>
### NavigateToViewModel\`\`1(navigationService,parameter) `method`

##### Summary

Navigate to the specified model type.

##### Returns

Whether or not navigation succeeded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') | The navigation service. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object parameter to pass to the target. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The model type to navigate to. |

<a name='M-Caliburn-Micro-NavigationExtensions-Navigate``1-Caliburn-Micro-INavigationService,System-Object-'></a>
### Navigate\`\`1(navigationService,parameter) `method`

##### Summary

Navigates to the specified content.

##### Returns

Whether or not navigation succeeded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') | The navigation service. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object parameter to pass to the target. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The [Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') to navigate to. |

<a name='M-Caliburn-Micro-NavigationExtensions-UriFor``1-Caliburn-Micro-INavigationService-'></a>
### UriFor\`\`1(navigationService) `method`

##### Summary

Creates a Uri builder based on a view model type.

##### Returns

The builder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.INavigationService](#T-Caliburn-Micro-INavigationService 'Caliburn.Micro.INavigationService') | The navigation service. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TViewModel | The type of the view model. |

<a name='T-Caliburn-Micro-Parameter'></a>
## Parameter `type`

##### Namespace

Caliburn.Micro

##### Summary

Represents a parameter of an [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage').

<a name='F-Caliburn-Micro-Parameter-ValueProperty'></a>
### ValueProperty `constants`

##### Summary

A dependency property representing the parameter's value.

<a name='P-Caliburn-Micro-Parameter-Owner'></a>
### Owner `property`

##### Summary

Gets or sets the owner.

<a name='P-Caliburn-Micro-Parameter-Value'></a>
### Value `property`

##### Summary

Gets or sets the value of the parameter.

<a name='M-Caliburn-Micro-Parameter-MakeAwareOf-Caliburn-Micro-ActionMessage-'></a>
### MakeAwareOf(owner) `method`

##### Summary

Makes the parameter aware of the [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') that it's attached to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| owner | [Caliburn.Micro.ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') | The action message. |

<a name='T-Caliburn-Micro-Parser'></a>
## Parser `type`

##### Namespace

Caliburn.Micro

##### Summary

Parses text into a fully functional set of [IBehavior](#T-Microsoft-Xaml-Interactivity-IBehavior 'Microsoft.Xaml.Interactivity.IBehavior') instances with [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage').

<a name='F-Caliburn-Micro-Parser-CreateParameter'></a>
### CreateParameter `constants`

##### Summary

Function used to parse a string identified as a message parameter.

<a name='F-Caliburn-Micro-Parser-CreateTrigger'></a>
### CreateTrigger `constants`

##### Summary

The function used to generate a trigger.

##### Remarks

The parameters passed to the method are the the target of the trigger and string representing the trigger.

<a name='F-Caliburn-Micro-Parser-InterpretMessageText'></a>
### InterpretMessageText `constants`

##### Summary

Function used to parse a string identified as a message.

<a name='M-Caliburn-Micro-Parser-BindParameter-Windows-UI-Xaml-FrameworkElement,Caliburn-Micro-Parameter,System-String,System-String,Windows-UI-Xaml-Data-BindingMode-'></a>
### BindParameter(target,parameter,elementName,path,bindingMode) `method`

##### Summary

Creates a binding on a [Parameter](#T-Caliburn-Micro-Parameter 'Caliburn.Micro.Parameter').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The target to which the message is applied. |
| parameter | [Caliburn.Micro.Parameter](#T-Caliburn-Micro-Parameter 'Caliburn.Micro.Parameter') | The parameter object. |
| elementName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the element to bind to. |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path of the element to bind to. |
| bindingMode | [Windows.UI.Xaml.Data.BindingMode](#T-Windows-UI-Xaml-Data-BindingMode 'Windows.UI.Xaml.Data.BindingMode') | The binding mode to use. |

<a name='M-Caliburn-Micro-Parser-CreateMessage-Windows-UI-Xaml-DependencyObject,System-String-'></a>
### CreateMessage(target,messageText) `method`

##### Summary

Creates an instance of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') by parsing out the textual dsl.

##### Returns

The created message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The target of the message. |
| messageText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The textual message dsl. |

<a name='M-Caliburn-Micro-Parser-Parse-Windows-UI-Xaml-DependencyObject,System-String-'></a>
### Parse(target,text) `method`

##### Summary

Parses the specified message text.

##### Returns

The triggers parsed from the text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The target. |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message text. |

<a name='T-Caliburn-Micro-BindingScope-ScopeNamingRoute'></a>
## ScopeNamingRoute `type`

##### Namespace

Caliburn.Micro.BindingScope

##### Summary

Maintains a connection in the visual tree of dependency objects in order to record a route through it.

<a name='P-Caliburn-Micro-BindingScope-ScopeNamingRoute-Root'></a>
### Root `property`

##### Summary

Gets or sets the starting point of the route.

<a name='M-Caliburn-Micro-BindingScope-ScopeNamingRoute-AddHop-Windows-UI-Xaml-DependencyObject,Windows-UI-Xaml-DependencyObject-'></a>
### AddHop(from,to) `method`

##### Summary

Adds a segment to the route.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| from | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The source dependency object. |
| to | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The target dependency object. |

<a name='M-Caliburn-Micro-BindingScope-ScopeNamingRoute-TryGetHop-Windows-UI-Xaml-DependencyObject,Windows-UI-Xaml-DependencyObject@-'></a>
### TryGetHop(hopSource,hopTarget) `method`

##### Summary

Tries to get a target dependency object given a source.

##### Returns

`true` if `hopSource` had a target recorded; `false` otherwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hopSource | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The possible beginning of a route segment (hop). |
| hopTarget | [Windows.UI.Xaml.DependencyObject@](#T-Windows-UI-Xaml-DependencyObject@ 'Windows.UI.Xaml.DependencyObject@') | The target of a route segment (hop). |

<a name='T-Caliburn-Micro-SharingService'></a>
## SharingService `type`

##### Namespace

Caliburn.Micro

##### Summary

Service that handles the [](#E-Windows-ApplicationModel-DataTransfer-DataTransferManager-DataRequested 'Windows.ApplicationModel.DataTransfer.DataTransferManager.DataRequested') event.

<a name='M-Caliburn-Micro-SharingService-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [SharingService](#T-Caliburn-Micro-SharingService 'Caliburn.Micro.SharingService') class.

##### Parameters

This constructor has no parameters.

<a name='M-Caliburn-Micro-SharingService-GetCurrentView'></a>
### GetCurrentView() `method`

##### Summary

Determines the current view, checks for view first with frame and then view mode first with a shell view.

##### Returns

The current view

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-SharingService-OnDataRequested-Windows-ApplicationModel-DataTransfer-DataTransferManager,Windows-ApplicationModel-DataTransfer-DataRequestedEventArgs-'></a>
### OnDataRequested(sender,args) `method`

##### Summary

Accepts the share request and forwards it to the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [Windows.ApplicationModel.DataTransfer.DataTransferManager](#T-Windows-ApplicationModel-DataTransfer-DataTransferManager 'Windows.ApplicationModel.DataTransfer.DataTransferManager') | The sender. |
| args | [Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs](#T-Windows-ApplicationModel-DataTransfer-DataRequestedEventArgs 'Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs') | The [DataRequestedEventArgs](#T-Windows-ApplicationModel-DataTransfer-DataRequestedEventArgs 'Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs') instance containing the event data. |

<a name='M-Caliburn-Micro-SharingService-ShowShareUI'></a>
### ShowShareUI() `method`

##### Summary

Programmatically initiates the user interface for sharing content with another app.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-TriggerAction`1'></a>
## TriggerAction\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Represents an attachable object that encapsulates a unit of functionality.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='F-Caliburn-Micro-TriggerAction`1-AssociatedObjectProperty'></a>
### AssociatedObjectProperty `constants`

##### Summary

The associated object property.

<a name='P-Caliburn-Micro-TriggerAction`1-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

Gets or sets the object to which this [TriggerAction\`1](#T-Caliburn-Micro-TriggerAction`1 'Caliburn.Micro.TriggerAction`1') is attached.

<a name='M-Caliburn-Micro-TriggerAction`1-Execute-System-Object,System-Object-'></a>
### Execute(sender,parameter) `method`

##### Summary

Executes the action.

##### Returns

Returns the result of the action.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') that is passed to the action by the behavior. Generally this is or a target object. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The value of this parameter is determined by the caller. |

<a name='M-Caliburn-Micro-TriggerAction`1-Invoke-System-Object-'></a>
### Invoke(parmeter) `method`

##### Summary

Invokes the action.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parmeter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference. |

<a name='M-Caliburn-Micro-TriggerAction`1-OnAttached'></a>
### OnAttached() `method`

##### Summary

Called after the action is attached to an AssociatedObject.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-TriggerAction`1-OnDetaching'></a>
### OnDetaching() `method`

##### Summary

Called when the action is being detached from its AssociatedObject, but before it has actually occurred.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-View'></a>
## View `type`

##### Namespace

Caliburn.Micro

##### Summary

Hosts attached properties related to view models.

<a name='F-Caliburn-Micro-View-ApplyConventionsProperty'></a>
### ApplyConventionsProperty `constants`

##### Summary

A dependency property which allows the override of convention application behavior.

<a name='F-Caliburn-Micro-View-ContextProperty'></a>
### ContextProperty `constants`

##### Summary

A dependency property for assigning a context to a particular portion of the UI.

<a name='F-Caliburn-Micro-View-GetFirstNonGeneratedView'></a>
### GetFirstNonGeneratedView `constants`

##### Summary

Used to retrieve the root, non-framework-created view.

##### Remarks

In certain instances the services create UI elements.
For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.

<a name='F-Caliburn-Micro-View-IsGeneratedProperty'></a>
### IsGeneratedProperty `constants`

##### Summary

Used by the framework to indicate that this element was generated.

<a name='F-Caliburn-Micro-View-IsLoadedProperty'></a>
### IsLoadedProperty `constants`

##### Summary

A dependency property which allows the framework to track whether a certain element has already been loaded in certain scenarios.

<a name='F-Caliburn-Micro-View-IsScopeRootProperty'></a>
### IsScopeRootProperty `constants`

##### Summary

A dependency property which marks an element as a name scope root.

<a name='F-Caliburn-Micro-View-ModelProperty'></a>
### ModelProperty `constants`

##### Summary

A dependency property for attaching a model to the UI.

<a name='P-Caliburn-Micro-View-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Gets a value that indicates whether the process is running in design mode.

<a name='M-Caliburn-Micro-View-ExecuteOnLayoutUpdated-Windows-UI-Xaml-FrameworkElement,System-EventHandler{System-Object}-'></a>
### ExecuteOnLayoutUpdated(element,handler) `method`

##### Summary

Executes the handler the next time the elements's LayoutUpdated event fires.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The element. |
| handler | [System.EventHandler{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.EventHandler 'System.EventHandler{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-View-ExecuteOnLoad-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-RoutedEventHandler-'></a>
### ExecuteOnLoad(element,handler) `method`

##### Summary

Executes the handler immediately if the element is loaded, otherwise wires it to the Loaded event.

##### Returns

true if the handler was executed immediately; false otherwise

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The element. |
| handler | [Windows.UI.Xaml.RoutedEventHandler](#T-Windows-UI-Xaml-RoutedEventHandler 'Windows.UI.Xaml.RoutedEventHandler') | The handler. |

<a name='M-Caliburn-Micro-View-ExecuteOnUnload-Windows-UI-Xaml-FrameworkElement,Windows-UI-Xaml-RoutedEventHandler-'></a>
### ExecuteOnUnload(element,handler) `method`

##### Summary

Executes the handler when the element is unloaded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The element. |
| handler | [Windows.UI.Xaml.RoutedEventHandler](#T-Windows-UI-Xaml-RoutedEventHandler 'Windows.UI.Xaml.RoutedEventHandler') | The handler. |

<a name='M-Caliburn-Micro-View-GetApplyConventions-Windows-UI-Xaml-DependencyObject-'></a>
### GetApplyConventions(d) `method`

##### Summary

Gets the convention application behavior.

##### Returns

Whether or not to apply conventions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element the property is attached to. |

<a name='M-Caliburn-Micro-View-GetContext-Windows-UI-Xaml-DependencyObject-'></a>
### GetContext(d) `method`

##### Summary

Gets the context.

##### Returns

The context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element the context is attached to. |

<a name='M-Caliburn-Micro-View-GetModel-Windows-UI-Xaml-DependencyObject-'></a>
### GetModel(d) `method`

##### Summary

Gets the model.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element the model is attached to. |

<a name='M-Caliburn-Micro-View-IsElementLoaded-Windows-UI-Xaml-FrameworkElement-'></a>
### IsElementLoaded(element) `method`

##### Summary

Determines whether the specified `element` is loaded.

##### Returns

true if the element is loaded; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The element. |

<a name='M-Caliburn-Micro-View-SetApplyConventions-Windows-UI-Xaml-DependencyObject,System-Nullable{System-Boolean}-'></a>
### SetApplyConventions(d,value) `method`

##### Summary

Sets the convention application behavior.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to attach the property to. |
| value | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Whether or not to apply conventions. |

<a name='M-Caliburn-Micro-View-SetContext-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### SetContext(d,value) `method`

##### Summary

Sets the context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to attach the context to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The context. |

<a name='M-Caliburn-Micro-View-SetModel-Windows-UI-Xaml-DependencyObject,System-Object-'></a>
### SetModel(d,value) `method`

##### Summary

Sets the model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Windows.UI.Xaml.DependencyObject](#T-Windows-UI-Xaml-DependencyObject 'Windows.UI.Xaml.DependencyObject') | The element to attach the model to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='T-Caliburn-Micro-ViewLocator'></a>
## ViewLocator `type`

##### Namespace

Caliburn.Micro

##### Summary

A strategy for determining which view to use for a given model.

<a name='F-Caliburn-Micro-ViewLocator-ContextSeparator'></a>
### ContextSeparator `constants`

##### Summary

Separator used when resolving View names for context instances.

<a name='F-Caliburn-Micro-ViewLocator-DeterminePackUriFromType'></a>
### DeterminePackUriFromType `constants`

##### Summary

Transforms a view type into a pack uri.

<a name='F-Caliburn-Micro-ViewLocator-GetOrCreateViewType'></a>
### GetOrCreateViewType `constants`

##### Summary

Retrieves the view from the IoC container or tries to create it if not found.

##### Remarks

Pass the type of view as a parameter and recieve an instance of the view.

<a name='F-Caliburn-Micro-ViewLocator-LocateForModel'></a>
### LocateForModel `constants`

##### Summary

Locates the view for the specified model instance.

##### Returns

The view.

##### Remarks

Pass the model instance, display location (or null) and the context (or null) as parameters and receive a view instance.

<a name='F-Caliburn-Micro-ViewLocator-LocateForModelType'></a>
### LocateForModelType `constants`

##### Summary

Locates the view for the specified model type.

##### Returns

The view.

##### Remarks

Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view instance.

<a name='F-Caliburn-Micro-ViewLocator-LocateTypeForModelType'></a>
### LocateTypeForModelType `constants`

##### Summary

Locates the view type based on the specified model type.

##### Returns

The view.

##### Remarks

Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.

<a name='F-Caliburn-Micro-ViewLocator-ModifyModelTypeAtDesignTime'></a>
### ModifyModelTypeAtDesignTime `constants`

##### Summary

Modifies the name of the type to be used at design time.

<a name='F-Caliburn-Micro-ViewLocator-NameTransformer'></a>
### NameTransformer `constants`

##### Summary

Used to transform names.

<a name='F-Caliburn-Micro-ViewLocator-TransformName'></a>
### TransformName `constants`

##### Summary

Transforms a ViewModel type name into all of its possible View type names. Optionally accepts an instance
of context object

##### Returns

Enumeration of transformed names

##### Remarks

Arguments:
typeName = The name of the ViewModel type being resolved to its companion View.
context = An instance of the context or null.

<a name='M-Caliburn-Micro-ViewLocator-AddDefaultTypeMapping-System-String-'></a>
### AddDefaultTypeMapping(viewSuffix) `method`

##### Summary

Adds a default type mapping using the standard namespace mapping convention

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewLocator-AddNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Namespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewLocator-AddNamespaceMapping-System-String,System-String,System-String-'></a>
### AddNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewLocator-AddSubNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Subnamespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewLocator-AddSubNamespaceMapping-System-String,System-String,System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewLocator-AddTypeMapping-System-String,System-String,System-String[],System-String-'></a>
### AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on namespace RegEx replace and filter patterns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSourceReplaceRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx replace pattern for source namespace |
| nsSourceFilterRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx filter pattern for source namespace |
| nsTargetsRegEx | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of RegEx replace values for target namespaces |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewLocator-AddTypeMapping-System-String,System-String,System-String,System-String-'></a>
### AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on namespace RegEx replace and filter patterns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSourceReplaceRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx replace pattern for source namespace |
| nsSourceFilterRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx filter pattern for source namespace |
| nsTargetRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx replace value for target namespace |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration-'></a>
### ConfigureTypeMappings(config) `method`

##### Summary

Specifies how type mappings are created, including default type mappings. Calling this method will
clear all existing name transformation rules and create new default type mappings according to the
configuration.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [Caliburn.Micro.TypeMappingConfiguration](#T-Caliburn-Micro-TypeMappingConfiguration 'Caliburn.Micro.TypeMappingConfiguration') | An instance of TypeMappingConfiguration that provides the settings for configuration |

<a name='M-Caliburn-Micro-ViewLocator-InitializeComponent-System-Object-'></a>
### InitializeComponent(element) `method`

##### Summary

When a view does not contain a code-behind file, we need to automatically call InitializeCompoent.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The element to initialize |

<a name='M-Caliburn-Micro-ViewLocator-RegisterViewSuffix-System-String-'></a>
### RegisterViewSuffix(viewSuffix) `method`

##### Summary

This method registers a View suffix or synonym so that View Context resolution works properly.
It is automatically called internally when calling AddNamespaceMapping(), AddDefaultTypeMapping(),
or AddTypeMapping(). It should not need to be called explicitly unless a rule that handles synonyms
is added directly through the NameTransformer.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". |

<a name='T-Caliburn-Micro-ViewModelBinder'></a>
## ViewModelBinder `type`

##### Namespace

Caliburn.Micro

##### Summary

Binds a view to a view model.

<a name='F-Caliburn-Micro-ViewModelBinder-ApplyConventionsByDefault'></a>
### ApplyConventionsByDefault `constants`

##### Summary

Gets or sets a value indicating whether to apply conventions by default.

<a name='F-Caliburn-Micro-ViewModelBinder-Bind'></a>
### Bind `constants`

##### Summary

Binds the specified viewModel to the view.

##### Remarks

Passes the the view model, view and creation context (or null for default) to use in applying binding.

<a name='F-Caliburn-Micro-ViewModelBinder-BindActions'></a>
### BindActions `constants`

##### Summary

Attaches instances of [ActionMessage](#T-Caliburn-Micro-ActionMessage 'Caliburn.Micro.ActionMessage') to the view's controls based on the provided methods.

##### Remarks

Parameters include the named elements to search through and the type of view model to determine conventions for. Returns unmatched elements.

<a name='F-Caliburn-Micro-ViewModelBinder-BindProperties'></a>
### BindProperties `constants`

##### Summary

Creates data bindings on the view's controls based on the provided properties.

##### Remarks

Parameters include named Elements to search through and the type of view model to determine conventions for. Returns unmatched elements.

<a name='F-Caliburn-Micro-ViewModelBinder-ConventionsAppliedProperty'></a>
### ConventionsAppliedProperty `constants`

##### Summary

Indicates whether or not the conventions have already been applied to the view.

<a name='F-Caliburn-Micro-ViewModelBinder-HandleUnmatchedElements'></a>
### HandleUnmatchedElements `constants`

##### Summary

Allows the developer to add custom handling of named elements which were not matched by any default conventions.

<a name='M-Caliburn-Micro-ViewModelBinder-ShouldApplyConventions-Windows-UI-Xaml-FrameworkElement-'></a>
### ShouldApplyConventions(view) `method`

##### Summary

Determines whether a view should have conventions applied to it.

##### Returns

Whether or not conventions should be applied to the view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Windows.UI.Xaml.FrameworkElement](#T-Windows-UI-Xaml-FrameworkElement 'Windows.UI.Xaml.FrameworkElement') | The view to check. |

<a name='T-Caliburn-Micro-ViewModelLocator'></a>
## ViewModelLocator `type`

##### Namespace

Caliburn.Micro

##### Summary

A strategy for determining which view model to use for a given view.

<a name='F-Caliburn-Micro-ViewModelLocator-InterfaceCaptureGroupName'></a>
### InterfaceCaptureGroupName `constants`

##### Summary

The name of the capture group used as a marker for rules that return interface types

<a name='F-Caliburn-Micro-ViewModelLocator-LocateForView'></a>
### LocateForView `constants`

##### Summary

Locates the view model for the specified view instance.

##### Returns

The view model.

##### Remarks

Pass the view instance as a parameters and receive a view model instance.

<a name='F-Caliburn-Micro-ViewModelLocator-LocateForViewType'></a>
### LocateForViewType `constants`

##### Summary

Locates the view model for the specified view type.

##### Returns

The view model.

##### Remarks

Pass the view type as a parameter and receive a view model instance.

<a name='F-Caliburn-Micro-ViewModelLocator-LocateTypeForViewType'></a>
### LocateTypeForViewType `constants`

##### Summary

Determines the view model type based on the specified view type.

##### Returns

The view model type.

##### Remarks

Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.

<a name='F-Caliburn-Micro-ViewModelLocator-NameTransformer'></a>
### NameTransformer `constants`

##### Summary

Used to transform names.

<a name='F-Caliburn-Micro-ViewModelLocator-TransformName'></a>
### TransformName `constants`

##### Summary

Transforms a View type name into all of its possible ViewModel type names. Accepts a flag
to include or exclude interface types.

##### Returns

Enumeration of transformed names

##### Remarks

Arguments:
typeName = The name of the View type being resolved to its companion ViewModel.
includeInterfaces = Flag to indicate if interface types are included

<a name='M-Caliburn-Micro-ViewModelLocator-AddDefaultTypeMapping-System-String-'></a>
### AddDefaultTypeMapping(viewSuffix) `method`

##### Summary

Adds a default type mapping using the standard namespace mapping convention

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewModelLocator-AddNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Namespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewModelLocator-AddNamespaceMapping-System-String,System-String,System-String-'></a>
### AddNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Subnamespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String,System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String[],System-String-'></a>
### AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on namespace RegEx replace and filter patterns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSourceReplaceRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx replace pattern for source namespace |
| nsSourceFilterRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx filter pattern for source namespace |
| nsTargetsRegEx | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of RegEx replace values for target namespaces |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String,System-String-'></a>
### AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on namespace RegEx replace and filter patterns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSourceReplaceRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx replace pattern for source namespace |
| nsSourceFilterRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx filter pattern for source namespace |
| nsTargetRegEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | RegEx replace value for target namespace |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-ViewModelLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration-'></a>
### ConfigureTypeMappings(config) `method`

##### Summary

Specifies how type mappings are created, including default type mappings. Calling this method will
clear all existing name transformation rules and create new default type mappings according to the
configuration.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [Caliburn.Micro.TypeMappingConfiguration](#T-Caliburn-Micro-TypeMappingConfiguration 'Caliburn.Micro.TypeMappingConfiguration') | An instance of TypeMappingConfiguration that provides the settings for configuration |

<a name='M-Caliburn-Micro-ViewModelLocator-MakeInterface-System-String-'></a>
### MakeInterface(typeName) `method`

##### Summary

Makes a type name into an interface name.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| typeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The part. |

<a name='T-Caliburn-Micro-WinRTContainer'></a>
## WinRTContainer `type`

##### Namespace

Caliburn.Micro

##### Summary

A custom IoC container which integrates with WinRT and properly registers all Caliburn.Micro services.

<a name='M-Caliburn-Micro-WinRTContainer-RegisterNavigationService-Windows-UI-Xaml-Controls-Frame,System-Boolean,System-Boolean-'></a>
### RegisterNavigationService(rootFrame,treatViewAsLoaded,cacheViewModels) `method`

##### Summary

Registers the Caliburn.Micro navigation service with the container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rootFrame | [Windows.UI.Xaml.Controls.Frame](#T-Windows-UI-Xaml-Controls-Frame 'Windows.UI.Xaml.Controls.Frame') | The application root frame. |
| treatViewAsLoaded | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` [treat view as loaded]. |
| cacheViewModels | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` then navigation service cache view models for resuse. |

<a name='M-Caliburn-Micro-WinRTContainer-RegisterSharingService'></a>
### RegisterSharingService() `method`

##### Summary

Registers the Caliburn.Micro sharing service with the container.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-WinRTContainer-RegisterWinRTServices'></a>
### RegisterWinRTServices() `method`

##### Summary

Registers the Caliburn.Micro WinRT services with the container.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-XamlMetadataProvider'></a>
## XamlMetadataProvider `type`

##### Namespace

Caliburn.Micro

##### Summary

Implements XAML schema context concepts that support XAML parsing.

<a name='M-Caliburn-Micro-XamlMetadataProvider-GetXamlType-System-Type-'></a>
### GetXamlType(type) `method`

##### Summary

Implements XAML schema context access to underlying type mapping, based on providing a helper value that describes a type.

##### Returns

The schema context's implementation of the [IXamlType](#T-Windows-UI-Xaml-Markup-IXamlType 'Windows.UI.Xaml.Markup.IXamlType') concept.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type as represented by the relevant type system or interop support type. |

<a name='M-Caliburn-Micro-XamlMetadataProvider-GetXamlType-System-String-'></a>
### GetXamlType(typeName) `method`

##### Summary

Implements XAML schema context access to underlying type mapping, based on specifying a full type name.

##### Returns

The schema context's implementation of the IXamlType concept.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| typeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the class for which to return a XAML type mapping. |

<a name='M-Caliburn-Micro-XamlMetadataProvider-GetXmlnsDefinitions'></a>
### GetXmlnsDefinitions() `method`

##### Summary

Gets the set of XMLNS (XAML namespace) definitions that apply to the context.

##### Returns

The set of XMLNS (XAML namespace) definitions.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-XamlPlatformProvider'></a>
## XamlPlatformProvider `type`

##### Namespace

Caliburn.Micro

##### Summary

A [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider') implementation for the XAML platfrom.

<a name='M-Caliburn-Micro-XamlPlatformProvider-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [XamlPlatformProvider](#T-Caliburn-Micro-XamlPlatformProvider 'Caliburn.Micro.XamlPlatformProvider') class.

##### Parameters

This constructor has no parameters.

<a name='P-Caliburn-Micro-XamlPlatformProvider-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Indicates whether or not the framework is in design-time mode.

<a name='P-Caliburn-Micro-XamlPlatformProvider-PropertyChangeNotificationsOnUIThread'></a>
### PropertyChangeNotificationsOnUIThread `property`

##### Summary

Whether or not classes should execute property change notications on the UI thread.

<a name='M-Caliburn-Micro-XamlPlatformProvider-BeginOnUIThread-System-Action-'></a>
### BeginOnUIThread(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-XamlPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnFirstLoad(view,handler) `method`

##### Summary

Executes the handler the fist time the view is loaded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-XamlPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnLayoutUpdated(view,handler) `method`

##### Summary

Executes the handler the next time the view's LayoutUpdated event fires.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-XamlPlatformProvider-GetFirstNonGeneratedView-System-Object-'></a>
### GetFirstNonGeneratedView(view) `method`

##### Summary

Used to retrieve the root, non-framework-created view.

##### Returns

The root element that was not created by the framework.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view to search. |

##### Remarks

In certain instances the services create UI elements.
For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.

<a name='M-Caliburn-Micro-XamlPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}-'></a>
### GetViewCloseAction(viewModel,views,dialogResult) `method`

##### Summary

Get the close action for the specified view model.

##### Returns

An [Action](#T-Caliburn-Micro-Action 'Caliburn.Micro.Action') to close the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model to close. |
| views | [System.Collections.Generic.ICollection{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.ICollection 'System.Collections.Generic.ICollection{System.Object}') | The associated views. |
| dialogResult | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | The dialog result. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='M-Caliburn-Micro-XamlPlatformProvider-OnUIThread-System-Action-'></a>
### OnUIThread(action) `method`

##### Summary

Executes the action on the UI thread.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='M-Caliburn-Micro-XamlPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}-'></a>
### OnUIThreadAsync(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') | The action to execute. |
