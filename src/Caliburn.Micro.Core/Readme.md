<a name='assembly'></a>
# Caliburn.Micro.Core

## Contents

- [ActivateExtensions](#T-Caliburn-Micro-ActivateExtensions 'Caliburn.Micro.ActivateExtensions')
  - [ActivateAsync(activate)](#M-Caliburn-Micro-ActivateExtensions-ActivateAsync-Caliburn-Micro-IActivate- 'Caliburn.Micro.ActivateExtensions.ActivateAsync(Caliburn.Micro.IActivate)')
- [ActivationEventArgs](#T-Caliburn-Micro-ActivationEventArgs 'Caliburn.Micro.ActivationEventArgs')
  - [WasInitialized](#P-Caliburn-Micro-ActivationEventArgs-WasInitialized 'Caliburn.Micro.ActivationEventArgs.WasInitialized')
- [ActivationProcessedEventArgs](#T-Caliburn-Micro-ActivationProcessedEventArgs 'Caliburn.Micro.ActivationProcessedEventArgs')
  - [Item](#P-Caliburn-Micro-ActivationProcessedEventArgs-Item 'Caliburn.Micro.ActivationProcessedEventArgs.Item')
  - [Success](#P-Caliburn-Micro-ActivationProcessedEventArgs-Success 'Caliburn.Micro.ActivationProcessedEventArgs.Success')
- [AllActive](#T-Caliburn-Micro-Conductor`1-Collection-AllActive 'Caliburn.Micro.Conductor`1.Collection.AllActive')
  - [#ctor(openPublicItems)](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-#ctor-System-Boolean- 'Caliburn.Micro.Conductor`1.Collection.AllActive.#ctor(System.Boolean)')
  - [#ctor()](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-#ctor 'Caliburn.Micro.Conductor`1.Collection.AllActive.#ctor')
  - [Items](#P-Caliburn-Micro-Conductor`1-Collection-AllActive-Items 'Caliburn.Micro.Conductor`1.Collection.AllActive.Items')
  - [ActivateItemAsync(item,cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-ActivateItemAsync-`0,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.AllActive.ActivateItemAsync(`0,System.Threading.CancellationToken)')
  - [CanCloseAsync(cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-CanCloseAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.AllActive.CanCloseAsync(System.Threading.CancellationToken)')
  - [DeactivateItemAsync(item,close,cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.AllActive.DeactivateItemAsync(`0,System.Boolean,System.Threading.CancellationToken)')
  - [EnsureItem(newItem)](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-EnsureItem-`0- 'Caliburn.Micro.Conductor`1.Collection.AllActive.EnsureItem(`0)')
  - [GetChildren()](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-GetChildren 'Caliburn.Micro.Conductor`1.Collection.AllActive.GetChildren')
  - [OnActivatedAsync()](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-OnActivatedAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.AllActive.OnActivatedAsync(System.Threading.CancellationToken)')
  - [OnDeactivateAsync(close,cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.AllActive.OnDeactivateAsync(System.Boolean,System.Threading.CancellationToken)')
  - [OnInitializedAsync()](#M-Caliburn-Micro-Conductor`1-Collection-AllActive-OnInitializedAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.AllActive.OnInitializedAsync(System.Threading.CancellationToken)')
- [AsyncEventHandlerExtensions](#T-Caliburn-Micro-AsyncEventHandlerExtensions 'Caliburn.Micro.AsyncEventHandlerExtensions')
  - [GetHandlers\`\`1(handler)](#M-Caliburn-Micro-AsyncEventHandlerExtensions-GetHandlers``1-Caliburn-Micro-AsyncEventHandler{``0}- 'Caliburn.Micro.AsyncEventHandlerExtensions.GetHandlers``1(Caliburn.Micro.AsyncEventHandler{``0})')
  - [InvokeAllAsync\`\`1(handler,sender,e)](#M-Caliburn-Micro-AsyncEventHandlerExtensions-InvokeAllAsync``1-Caliburn-Micro-AsyncEventHandler{``0},System-Object,``0- 'Caliburn.Micro.AsyncEventHandlerExtensions.InvokeAllAsync``1(Caliburn.Micro.AsyncEventHandler{``0},System.Object,``0)')
- [AsyncEventHandler\`1](#T-Caliburn-Micro-AsyncEventHandler`1 'Caliburn.Micro.AsyncEventHandler`1')
- [BindableCollection\`1](#T-Caliburn-Micro-BindableCollection`1 'Caliburn.Micro.BindableCollection`1')
  - [#ctor()](#M-Caliburn-Micro-BindableCollection`1-#ctor 'Caliburn.Micro.BindableCollection`1.#ctor')
  - [#ctor(collection)](#M-Caliburn-Micro-BindableCollection`1-#ctor-System-Collections-Generic-IEnumerable{`0}- 'Caliburn.Micro.BindableCollection`1.#ctor(System.Collections.Generic.IEnumerable{`0})')
  - [IsNotifying](#P-Caliburn-Micro-BindableCollection`1-IsNotifying 'Caliburn.Micro.BindableCollection`1.IsNotifying')
  - [AddRange(items)](#M-Caliburn-Micro-BindableCollection`1-AddRange-System-Collections-Generic-IEnumerable{`0}- 'Caliburn.Micro.BindableCollection`1.AddRange(System.Collections.Generic.IEnumerable{`0})')
  - [ClearItems()](#M-Caliburn-Micro-BindableCollection`1-ClearItems 'Caliburn.Micro.BindableCollection`1.ClearItems')
  - [ClearItemsBase()](#M-Caliburn-Micro-BindableCollection`1-ClearItemsBase 'Caliburn.Micro.BindableCollection`1.ClearItemsBase')
  - [InsertItem(index,item)](#M-Caliburn-Micro-BindableCollection`1-InsertItem-System-Int32,`0- 'Caliburn.Micro.BindableCollection`1.InsertItem(System.Int32,`0)')
  - [InsertItemBase(index,item)](#M-Caliburn-Micro-BindableCollection`1-InsertItemBase-System-Int32,`0- 'Caliburn.Micro.BindableCollection`1.InsertItemBase(System.Int32,`0)')
  - [NotifyOfPropertyChange(propertyName)](#M-Caliburn-Micro-BindableCollection`1-NotifyOfPropertyChange-System-String- 'Caliburn.Micro.BindableCollection`1.NotifyOfPropertyChange(System.String)')
  - [OnCollectionChanged(e)](#M-Caliburn-Micro-BindableCollection`1-OnCollectionChanged-System-Collections-Specialized-NotifyCollectionChangedEventArgs- 'Caliburn.Micro.BindableCollection`1.OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs)')
  - [OnPropertyChanged(e)](#M-Caliburn-Micro-BindableCollection`1-OnPropertyChanged-System-ComponentModel-PropertyChangedEventArgs- 'Caliburn.Micro.BindableCollection`1.OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs)')
  - [OnUIThread(action)](#M-Caliburn-Micro-BindableCollection`1-OnUIThread-System-Action- 'Caliburn.Micro.BindableCollection`1.OnUIThread(System.Action)')
  - [Refresh()](#M-Caliburn-Micro-BindableCollection`1-Refresh 'Caliburn.Micro.BindableCollection`1.Refresh')
  - [RemoveItem(index)](#M-Caliburn-Micro-BindableCollection`1-RemoveItem-System-Int32- 'Caliburn.Micro.BindableCollection`1.RemoveItem(System.Int32)')
  - [RemoveItemBase(index)](#M-Caliburn-Micro-BindableCollection`1-RemoveItemBase-System-Int32- 'Caliburn.Micro.BindableCollection`1.RemoveItemBase(System.Int32)')
  - [RemoveRange(items)](#M-Caliburn-Micro-BindableCollection`1-RemoveRange-System-Collections-Generic-IEnumerable{`0}- 'Caliburn.Micro.BindableCollection`1.RemoveRange(System.Collections.Generic.IEnumerable{`0})')
  - [SetItem(index,item)](#M-Caliburn-Micro-BindableCollection`1-SetItem-System-Int32,`0- 'Caliburn.Micro.BindableCollection`1.SetItem(System.Int32,`0)')
  - [SetItemBase(index,item)](#M-Caliburn-Micro-BindableCollection`1-SetItemBase-System-Int32,`0- 'Caliburn.Micro.BindableCollection`1.SetItemBase(System.Int32,`0)')
- [CloseResult\`1](#T-Caliburn-Micro-CloseResult`1 'Caliburn.Micro.CloseResult`1')
  - [#ctor(closeCanOccur,children)](#M-Caliburn-Micro-CloseResult`1-#ctor-System-Boolean,System-Collections-Generic-IEnumerable{`0}- 'Caliburn.Micro.CloseResult`1.#ctor(System.Boolean,System.Collections.Generic.IEnumerable{`0})')
  - [Children](#P-Caliburn-Micro-CloseResult`1-Children 'Caliburn.Micro.CloseResult`1.Children')
  - [CloseCanOccur](#P-Caliburn-Micro-CloseResult`1-CloseCanOccur 'Caliburn.Micro.CloseResult`1.CloseCanOccur')
- [Collection](#T-Caliburn-Micro-Conductor`1-Collection 'Caliburn.Micro.Conductor`1.Collection')
- [ConductorBaseWithActiveItem\`1](#T-Caliburn-Micro-ConductorBaseWithActiveItem`1 'Caliburn.Micro.ConductorBaseWithActiveItem`1')
  - [ActiveItem](#P-Caliburn-Micro-ConductorBaseWithActiveItem`1-ActiveItem 'Caliburn.Micro.ConductorBaseWithActiveItem`1.ActiveItem')
  - [Caliburn#Micro#IHaveActiveItem#ActiveItem](#P-Caliburn-Micro-ConductorBaseWithActiveItem`1-Caliburn#Micro#IHaveActiveItem#ActiveItem 'Caliburn.Micro.ConductorBaseWithActiveItem`1.Caliburn#Micro#IHaveActiveItem#ActiveItem')
  - [ChangeActiveItemAsync(newItem,closePrevious,cancellationToken)](#M-Caliburn-Micro-ConductorBaseWithActiveItem`1-ChangeActiveItemAsync-`0,System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.ConductorBaseWithActiveItem`1.ChangeActiveItemAsync(`0,System.Boolean,System.Threading.CancellationToken)')
  - [ChangeActiveItemAsync(newItem,closePrevious)](#M-Caliburn-Micro-ConductorBaseWithActiveItem`1-ChangeActiveItemAsync-`0,System-Boolean- 'Caliburn.Micro.ConductorBaseWithActiveItem`1.ChangeActiveItemAsync(`0,System.Boolean)')
- [ConductorBase\`1](#T-Caliburn-Micro-ConductorBase`1 'Caliburn.Micro.ConductorBase`1')
  - [CloseStrategy](#P-Caliburn-Micro-ConductorBase`1-CloseStrategy 'Caliburn.Micro.ConductorBase`1.CloseStrategy')
  - [ActivateItemAsync(item,cancellationToken)](#M-Caliburn-Micro-ConductorBase`1-ActivateItemAsync-`0,System-Threading-CancellationToken- 'Caliburn.Micro.ConductorBase`1.ActivateItemAsync(`0,System.Threading.CancellationToken)')
  - [DeactivateItemAsync(item,close,cancellationToken)](#M-Caliburn-Micro-ConductorBase`1-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.ConductorBase`1.DeactivateItemAsync(`0,System.Boolean,System.Threading.CancellationToken)')
  - [EnsureItem(newItem)](#M-Caliburn-Micro-ConductorBase`1-EnsureItem-`0- 'Caliburn.Micro.ConductorBase`1.EnsureItem(`0)')
  - [GetChildren()](#M-Caliburn-Micro-ConductorBase`1-GetChildren 'Caliburn.Micro.ConductorBase`1.GetChildren')
  - [OnActivationProcessed(item,success)](#M-Caliburn-Micro-ConductorBase`1-OnActivationProcessed-`0,System-Boolean- 'Caliburn.Micro.ConductorBase`1.OnActivationProcessed(`0,System.Boolean)')
- [ConductorExtensions](#T-Caliburn-Micro-ConductorExtensions 'Caliburn.Micro.ConductorExtensions')
  - [ActivateItemAsync(conductor,item)](#M-Caliburn-Micro-ConductorExtensions-ActivateItemAsync-Caliburn-Micro-IConductor,System-Object- 'Caliburn.Micro.ConductorExtensions.ActivateItemAsync(Caliburn.Micro.IConductor,System.Object)')
- [Conductor\`1](#T-Caliburn-Micro-Conductor`1 'Caliburn.Micro.Conductor`1')
  - [ActivateItemAsync()](#M-Caliburn-Micro-Conductor`1-ActivateItemAsync-`0,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.ActivateItemAsync(`0,System.Threading.CancellationToken)')
  - [CanCloseAsync(cancellationToken)](#M-Caliburn-Micro-Conductor`1-CanCloseAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.CanCloseAsync(System.Threading.CancellationToken)')
  - [DeactivateItemAsync(item,close,cancellationToken)](#M-Caliburn-Micro-Conductor`1-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.DeactivateItemAsync(`0,System.Boolean,System.Threading.CancellationToken)')
  - [GetChildren()](#M-Caliburn-Micro-Conductor`1-GetChildren 'Caliburn.Micro.Conductor`1.GetChildren')
  - [OnActivatedAsync()](#M-Caliburn-Micro-Conductor`1-OnActivatedAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.OnActivatedAsync(System.Threading.CancellationToken)')
  - [OnDeactivateAsync(close,cancellationToken)](#M-Caliburn-Micro-Conductor`1-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.OnDeactivateAsync(System.Boolean,System.Threading.CancellationToken)')
- [ContainerExtensions](#T-Caliburn-Micro-ContainerExtensions 'Caliburn.Micro.ContainerExtensions')
  - [AllTypesOf\`\`1(container,assembly,filter)](#M-Caliburn-Micro-ContainerExtensions-AllTypesOf``1-Caliburn-Micro-SimpleContainer,System-Reflection-Assembly,System-Func{System-Type,System-Boolean}- 'Caliburn.Micro.ContainerExtensions.AllTypesOf``1(Caliburn.Micro.SimpleContainer,System.Reflection.Assembly,System.Func{System.Type,System.Boolean})')
  - [GetAllInstances\`\`1(container,key)](#M-Caliburn-Micro-ContainerExtensions-GetAllInstances``1-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.GetAllInstances``1(Caliburn.Micro.SimpleContainer,System.String)')
  - [GetInstance\`\`1(container,key)](#M-Caliburn-Micro-ContainerExtensions-GetInstance``1-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.GetInstance``1(Caliburn.Micro.SimpleContainer,System.String)')
  - [Handler\`\`1(container,handler)](#M-Caliburn-Micro-ContainerExtensions-Handler``1-Caliburn-Micro-SimpleContainer,System-Func{Caliburn-Micro-SimpleContainer,System-Object}- 'Caliburn.Micro.ContainerExtensions.Handler``1(Caliburn.Micro.SimpleContainer,System.Func{Caliburn.Micro.SimpleContainer,System.Object})')
  - [HasHandler\`\`1(container,key)](#M-Caliburn-Micro-ContainerExtensions-HasHandler``1-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.HasHandler``1(Caliburn.Micro.SimpleContainer,System.String)')
  - [Instance\`\`1(container,instance)](#M-Caliburn-Micro-ContainerExtensions-Instance``1-Caliburn-Micro-SimpleContainer,``0- 'Caliburn.Micro.ContainerExtensions.Instance``1(Caliburn.Micro.SimpleContainer,``0)')
  - [PerRequest\`\`1(container,key)](#M-Caliburn-Micro-ContainerExtensions-PerRequest``1-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.PerRequest``1(Caliburn.Micro.SimpleContainer,System.String)')
  - [PerRequest\`\`2(container,key)](#M-Caliburn-Micro-ContainerExtensions-PerRequest``2-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.PerRequest``2(Caliburn.Micro.SimpleContainer,System.String)')
  - [Singleton\`\`1(container,key)](#M-Caliburn-Micro-ContainerExtensions-Singleton``1-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.Singleton``1(Caliburn.Micro.SimpleContainer,System.String)')
  - [Singleton\`\`2(container,key)](#M-Caliburn-Micro-ContainerExtensions-Singleton``2-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.Singleton``2(Caliburn.Micro.SimpleContainer,System.String)')
  - [UnregisterHandler\`\`1(container,key)](#M-Caliburn-Micro-ContainerExtensions-UnregisterHandler``1-Caliburn-Micro-SimpleContainer,System-String- 'Caliburn.Micro.ContainerExtensions.UnregisterHandler``1(Caliburn.Micro.SimpleContainer,System.String)')
- [ContinueResultDecorator](#T-Caliburn-Micro-ContinueResultDecorator 'Caliburn.Micro.ContinueResultDecorator')
  - [#ctor(result,coroutine)](#M-Caliburn-Micro-ContinueResultDecorator-#ctor-Caliburn-Micro-IResult,System-Func{Caliburn-Micro-IResult}- 'Caliburn.Micro.ContinueResultDecorator.#ctor(Caliburn.Micro.IResult,System.Func{Caliburn.Micro.IResult})')
  - [OnInnerResultCompleted(context,innerResult,args)](#M-Caliburn-Micro-ContinueResultDecorator-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs- 'Caliburn.Micro.ContinueResultDecorator.OnInnerResultCompleted(Caliburn.Micro.CoroutineExecutionContext,Caliburn.Micro.IResult,Caliburn.Micro.ResultCompletionEventArgs)')
- [Coroutine](#T-Caliburn-Micro-Coroutine 'Caliburn.Micro.Coroutine')
  - [CreateParentEnumerator](#F-Caliburn-Micro-Coroutine-CreateParentEnumerator 'Caliburn.Micro.Coroutine.CreateParentEnumerator')
  - [BeginExecute(coroutine,context,callback)](#M-Caliburn-Micro-Coroutine-BeginExecute-System-Collections-Generic-IEnumerator{Caliburn-Micro-IResult},Caliburn-Micro-CoroutineExecutionContext,System-EventHandler{Caliburn-Micro-ResultCompletionEventArgs}- 'Caliburn.Micro.Coroutine.BeginExecute(System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult},Caliburn.Micro.CoroutineExecutionContext,System.EventHandler{Caliburn.Micro.ResultCompletionEventArgs})')
  - [ExecuteAsync(coroutine,context)](#M-Caliburn-Micro-Coroutine-ExecuteAsync-System-Collections-Generic-IEnumerator{Caliburn-Micro-IResult},Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.Coroutine.ExecuteAsync(System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult},Caliburn.Micro.CoroutineExecutionContext)')
- [CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext')
  - [Source](#P-Caliburn-Micro-CoroutineExecutionContext-Source 'Caliburn.Micro.CoroutineExecutionContext.Source')
  - [Target](#P-Caliburn-Micro-CoroutineExecutionContext-Target 'Caliburn.Micro.CoroutineExecutionContext.Target')
  - [View](#P-Caliburn-Micro-CoroutineExecutionContext-View 'Caliburn.Micro.CoroutineExecutionContext.View')
- [DeactivateExtensions](#T-Caliburn-Micro-DeactivateExtensions 'Caliburn.Micro.DeactivateExtensions')
  - [DeactivateAsync(deactivate,close)](#M-Caliburn-Micro-DeactivateExtensions-DeactivateAsync-Caliburn-Micro-IDeactivate,System-Boolean- 'Caliburn.Micro.DeactivateExtensions.DeactivateAsync(Caliburn.Micro.IDeactivate,System.Boolean)')
- [DeactivationEventArgs](#T-Caliburn-Micro-DeactivationEventArgs 'Caliburn.Micro.DeactivationEventArgs')
  - [WasClosed](#P-Caliburn-Micro-DeactivationEventArgs-WasClosed 'Caliburn.Micro.DeactivationEventArgs.WasClosed')
- [DebugLog](#T-Caliburn-Micro-DebugLog 'Caliburn.Micro.DebugLog')
  - [#ctor(type)](#M-Caliburn-Micro-DebugLog-#ctor-System-Type- 'Caliburn.Micro.DebugLog.#ctor(System.Type)')
  - [Error(exception)](#M-Caliburn-Micro-DebugLog-Error-System-Exception- 'Caliburn.Micro.DebugLog.Error(System.Exception)')
  - [Info(format,args)](#M-Caliburn-Micro-DebugLog-Info-System-String,System-Object[]- 'Caliburn.Micro.DebugLog.Info(System.String,System.Object[])')
  - [Warn(format,args)](#M-Caliburn-Micro-DebugLog-Warn-System-String,System-Object[]- 'Caliburn.Micro.DebugLog.Warn(System.String,System.Object[])')
- [DefaultCloseStrategy\`1](#T-Caliburn-Micro-DefaultCloseStrategy`1 'Caliburn.Micro.DefaultCloseStrategy`1')
  - [#ctor(closeConductedItemsWhenConductorCannotClose)](#M-Caliburn-Micro-DefaultCloseStrategy`1-#ctor-System-Boolean- 'Caliburn.Micro.DefaultCloseStrategy`1.#ctor(System.Boolean)')
  - [ExecuteAsync()](#M-Caliburn-Micro-DefaultCloseStrategy`1-ExecuteAsync-System-Collections-Generic-IEnumerable{`0},System-Threading-CancellationToken- 'Caliburn.Micro.DefaultCloseStrategy`1.ExecuteAsync(System.Collections.Generic.IEnumerable{`0},System.Threading.CancellationToken)')
- [DefaultPlatformProvider](#T-Caliburn-Micro-DefaultPlatformProvider 'Caliburn.Micro.DefaultPlatformProvider')
  - [InDesignMode](#P-Caliburn-Micro-DefaultPlatformProvider-InDesignMode 'Caliburn.Micro.DefaultPlatformProvider.InDesignMode')
  - [PropertyChangeNotificationsOnUIThread](#P-Caliburn-Micro-DefaultPlatformProvider-PropertyChangeNotificationsOnUIThread 'Caliburn.Micro.DefaultPlatformProvider.PropertyChangeNotificationsOnUIThread')
  - [BeginOnUIThread(action)](#M-Caliburn-Micro-DefaultPlatformProvider-BeginOnUIThread-System-Action- 'Caliburn.Micro.DefaultPlatformProvider.BeginOnUIThread(System.Action)')
  - [ExecuteOnFirstLoad(view,handler)](#M-Caliburn-Micro-DefaultPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}- 'Caliburn.Micro.DefaultPlatformProvider.ExecuteOnFirstLoad(System.Object,System.Action{System.Object})')
  - [ExecuteOnLayoutUpdated(view,handler)](#M-Caliburn-Micro-DefaultPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}- 'Caliburn.Micro.DefaultPlatformProvider.ExecuteOnLayoutUpdated(System.Object,System.Action{System.Object})')
  - [GetFirstNonGeneratedView(view)](#M-Caliburn-Micro-DefaultPlatformProvider-GetFirstNonGeneratedView-System-Object- 'Caliburn.Micro.DefaultPlatformProvider.GetFirstNonGeneratedView(System.Object)')
  - [GetViewCloseAction(viewModel,views,dialogResult)](#M-Caliburn-Micro-DefaultPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}- 'Caliburn.Micro.DefaultPlatformProvider.GetViewCloseAction(System.Object,System.Collections.Generic.ICollection{System.Object},System.Nullable{System.Boolean})')
  - [OnUIThread(action)](#M-Caliburn-Micro-DefaultPlatformProvider-OnUIThread-System-Action- 'Caliburn.Micro.DefaultPlatformProvider.OnUIThread(System.Action)')
  - [OnUIThreadAsync(action)](#M-Caliburn-Micro-DefaultPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}- 'Caliburn.Micro.DefaultPlatformProvider.OnUIThreadAsync(System.Func{System.Threading.Tasks.Task})')
- [DelegateResult](#T-Caliburn-Micro-DelegateResult 'Caliburn.Micro.DelegateResult')
  - [#ctor(action)](#M-Caliburn-Micro-DelegateResult-#ctor-System-Action- 'Caliburn.Micro.DelegateResult.#ctor(System.Action)')
  - [Execute(context)](#M-Caliburn-Micro-DelegateResult-Execute-Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.DelegateResult.Execute(Caliburn.Micro.CoroutineExecutionContext)')
- [DelegateResult\`1](#T-Caliburn-Micro-DelegateResult`1 'Caliburn.Micro.DelegateResult`1')
  - [#ctor(action)](#M-Caliburn-Micro-DelegateResult`1-#ctor-System-Func{`0}- 'Caliburn.Micro.DelegateResult`1.#ctor(System.Func{`0})')
  - [Result](#P-Caliburn-Micro-DelegateResult`1-Result 'Caliburn.Micro.DelegateResult`1.Result')
  - [Execute(context)](#M-Caliburn-Micro-DelegateResult`1-Execute-Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.DelegateResult`1.Execute(Caliburn.Micro.CoroutineExecutionContext)')
- [EnumerableExtensions](#T-Caliburn-Micro-EnumerableExtensions 'Caliburn.Micro.EnumerableExtensions')
  - [Apply\`\`1(enumerable,action)](#M-Caliburn-Micro-EnumerableExtensions-Apply``1-System-Collections-Generic-IEnumerable{``0},System-Action{``0}- 'Caliburn.Micro.EnumerableExtensions.Apply``1(System.Collections.Generic.IEnumerable{``0},System.Action{``0})')
- [EventAggregator](#T-Caliburn-Micro-EventAggregator 'Caliburn.Micro.EventAggregator')
  - [HandlerExistsFor()](#M-Caliburn-Micro-EventAggregator-HandlerExistsFor-System-Type- 'Caliburn.Micro.EventAggregator.HandlerExistsFor(System.Type)')
  - [PublishAsync()](#M-Caliburn-Micro-EventAggregator-PublishAsync-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task},System-Threading-CancellationToken- 'Caliburn.Micro.EventAggregator.PublishAsync(System.Object,System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task},System.Threading.CancellationToken)')
  - [Subscribe()](#M-Caliburn-Micro-EventAggregator-Subscribe-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task}- 'Caliburn.Micro.EventAggregator.Subscribe(System.Object,System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task})')
  - [Unsubscribe()](#M-Caliburn-Micro-EventAggregator-Unsubscribe-System-Object- 'Caliburn.Micro.EventAggregator.Unsubscribe(System.Object)')
- [EventAggregatorExtensions](#T-Caliburn-Micro-EventAggregatorExtensions 'Caliburn.Micro.EventAggregatorExtensions')
  - [PublishOnBackgroundThreadAsync(eventAggregator,message,cancellationToken)](#M-Caliburn-Micro-EventAggregatorExtensions-PublishOnBackgroundThreadAsync-Caliburn-Micro-IEventAggregator,System-Object,System-Threading-CancellationToken- 'Caliburn.Micro.EventAggregatorExtensions.PublishOnBackgroundThreadAsync(Caliburn.Micro.IEventAggregator,System.Object,System.Threading.CancellationToken)')
  - [PublishOnBackgroundThreadAsync(eventAggregator,message)](#M-Caliburn-Micro-EventAggregatorExtensions-PublishOnBackgroundThreadAsync-Caliburn-Micro-IEventAggregator,System-Object- 'Caliburn.Micro.EventAggregatorExtensions.PublishOnBackgroundThreadAsync(Caliburn.Micro.IEventAggregator,System.Object)')
  - [PublishOnCurrentThreadAsync(eventAggregator,message,cancellationToken)](#M-Caliburn-Micro-EventAggregatorExtensions-PublishOnCurrentThreadAsync-Caliburn-Micro-IEventAggregator,System-Object,System-Threading-CancellationToken- 'Caliburn.Micro.EventAggregatorExtensions.PublishOnCurrentThreadAsync(Caliburn.Micro.IEventAggregator,System.Object,System.Threading.CancellationToken)')
  - [PublishOnCurrentThreadAsync(eventAggregator,message)](#M-Caliburn-Micro-EventAggregatorExtensions-PublishOnCurrentThreadAsync-Caliburn-Micro-IEventAggregator,System-Object- 'Caliburn.Micro.EventAggregatorExtensions.PublishOnCurrentThreadAsync(Caliburn.Micro.IEventAggregator,System.Object)')
  - [PublishOnUIThreadAsync(eventAggregator,message,cancellationToken)](#M-Caliburn-Micro-EventAggregatorExtensions-PublishOnUIThreadAsync-Caliburn-Micro-IEventAggregator,System-Object,System-Threading-CancellationToken- 'Caliburn.Micro.EventAggregatorExtensions.PublishOnUIThreadAsync(Caliburn.Micro.IEventAggregator,System.Object,System.Threading.CancellationToken)')
  - [PublishOnUIThreadAsync(eventAggregator,message)](#M-Caliburn-Micro-EventAggregatorExtensions-PublishOnUIThreadAsync-Caliburn-Micro-IEventAggregator,System-Object- 'Caliburn.Micro.EventAggregatorExtensions.PublishOnUIThreadAsync(Caliburn.Micro.IEventAggregator,System.Object)')
  - [Subscribe(eventAggregator,subscriber)](#M-Caliburn-Micro-EventAggregatorExtensions-Subscribe-Caliburn-Micro-IEventAggregator,System-Object- 'Caliburn.Micro.EventAggregatorExtensions.Subscribe(Caliburn.Micro.IEventAggregator,System.Object)')
  - [SubscribeOnBackgroundThread(eventAggregator,subscriber)](#M-Caliburn-Micro-EventAggregatorExtensions-SubscribeOnBackgroundThread-Caliburn-Micro-IEventAggregator,System-Object- 'Caliburn.Micro.EventAggregatorExtensions.SubscribeOnBackgroundThread(Caliburn.Micro.IEventAggregator,System.Object)')
  - [SubscribeOnPublishedThread(eventAggregator,subscriber)](#M-Caliburn-Micro-EventAggregatorExtensions-SubscribeOnPublishedThread-Caliburn-Micro-IEventAggregator,System-Object- 'Caliburn.Micro.EventAggregatorExtensions.SubscribeOnPublishedThread(Caliburn.Micro.IEventAggregator,System.Object)')
  - [SubscribeOnUIThread(eventAggregator,subscriber)](#M-Caliburn-Micro-EventAggregatorExtensions-SubscribeOnUIThread-Caliburn-Micro-IEventAggregator,System-Object- 'Caliburn.Micro.EventAggregatorExtensions.SubscribeOnUIThread(Caliburn.Micro.IEventAggregator,System.Object)')
- [Execute](#T-Caliburn-Micro-Execute 'Caliburn.Micro.Execute')
  - [InDesignMode](#P-Caliburn-Micro-Execute-InDesignMode 'Caliburn.Micro.Execute.InDesignMode')
  - [BeginOnUIThread(action)](#M-Caliburn-Micro-Execute-BeginOnUIThread-System-Action- 'Caliburn.Micro.Execute.BeginOnUIThread(System.Action)')
  - [OnUIThread(action)](#M-Caliburn-Micro-Execute-OnUIThread-System-Action- 'Caliburn.Micro.Execute.OnUIThread(System.Action)')
  - [OnUIThreadAsync(action)](#M-Caliburn-Micro-Execute-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}- 'Caliburn.Micro.Execute.OnUIThreadAsync(System.Func{System.Threading.Tasks.Task})')
- [ExpressionExtensions](#T-Caliburn-Micro-ExpressionExtensions 'Caliburn.Micro.ExpressionExtensions')
  - [GetMemberInfo(expression)](#M-Caliburn-Micro-ExpressionExtensions-GetMemberInfo-System-Linq-Expressions-Expression- 'Caliburn.Micro.ExpressionExtensions.GetMemberInfo(System.Linq.Expressions.Expression)')
- [IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate')
  - [IsActive](#P-Caliburn-Micro-IActivate-IsActive 'Caliburn.Micro.IActivate.IsActive')
  - [ActivateAsync(cancellationToken)](#M-Caliburn-Micro-IActivate-ActivateAsync-System-Threading-CancellationToken- 'Caliburn.Micro.IActivate.ActivateAsync(System.Threading.CancellationToken)')
- [IChild](#T-Caliburn-Micro-IChild 'Caliburn.Micro.IChild')
  - [Parent](#P-Caliburn-Micro-IChild-Parent 'Caliburn.Micro.IChild.Parent')
- [IChild\`1](#T-Caliburn-Micro-IChild`1 'Caliburn.Micro.IChild`1')
  - [Parent](#P-Caliburn-Micro-IChild`1-Parent 'Caliburn.Micro.IChild`1.Parent')
- [IClose](#T-Caliburn-Micro-IClose 'Caliburn.Micro.IClose')
  - [TryCloseAsync(dialogResult)](#M-Caliburn-Micro-IClose-TryCloseAsync-System-Nullable{System-Boolean}- 'Caliburn.Micro.IClose.TryCloseAsync(System.Nullable{System.Boolean})')
- [ICloseResult\`1](#T-Caliburn-Micro-ICloseResult`1 'Caliburn.Micro.ICloseResult`1')
  - [Children](#P-Caliburn-Micro-ICloseResult`1-Children 'Caliburn.Micro.ICloseResult`1.Children')
  - [CloseCanOccur](#P-Caliburn-Micro-ICloseResult`1-CloseCanOccur 'Caliburn.Micro.ICloseResult`1.CloseCanOccur')
- [ICloseStrategy\`1](#T-Caliburn-Micro-ICloseStrategy`1 'Caliburn.Micro.ICloseStrategy`1')
  - [ExecuteAsync(toClose,cancellationToken)](#M-Caliburn-Micro-ICloseStrategy`1-ExecuteAsync-System-Collections-Generic-IEnumerable{`0},System-Threading-CancellationToken- 'Caliburn.Micro.ICloseStrategy`1.ExecuteAsync(System.Collections.Generic.IEnumerable{`0},System.Threading.CancellationToken)')
- [IConductActiveItem](#T-Caliburn-Micro-IConductActiveItem 'Caliburn.Micro.IConductActiveItem')
- [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor')
  - [ActivateItemAsync(item,cancellationToken)](#M-Caliburn-Micro-IConductor-ActivateItemAsync-System-Object,System-Threading-CancellationToken- 'Caliburn.Micro.IConductor.ActivateItemAsync(System.Object,System.Threading.CancellationToken)')
  - [DeactivateItemAsync(item,close,cancellationToken)](#M-Caliburn-Micro-IConductor-DeactivateItemAsync-System-Object,System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.IConductor.DeactivateItemAsync(System.Object,System.Boolean,System.Threading.CancellationToken)')
- [IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate')
  - [DeactivateAsync(close,cancellationToken)](#M-Caliburn-Micro-IDeactivate-DeactivateAsync-System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.IDeactivate.DeactivateAsync(System.Boolean,System.Threading.CancellationToken)')
- [IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator')
  - [HandlerExistsFor(messageType)](#M-Caliburn-Micro-IEventAggregator-HandlerExistsFor-System-Type- 'Caliburn.Micro.IEventAggregator.HandlerExistsFor(System.Type)')
  - [PublishAsync(message,marshal,cancellationToken)](#M-Caliburn-Micro-IEventAggregator-PublishAsync-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task},System-Threading-CancellationToken- 'Caliburn.Micro.IEventAggregator.PublishAsync(System.Object,System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task},System.Threading.CancellationToken)')
  - [Subscribe(subscriber,marshal)](#M-Caliburn-Micro-IEventAggregator-Subscribe-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task}- 'Caliburn.Micro.IEventAggregator.Subscribe(System.Object,System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task})')
  - [Unsubscribe(subscriber)](#M-Caliburn-Micro-IEventAggregator-Unsubscribe-System-Object- 'Caliburn.Micro.IEventAggregator.Unsubscribe(System.Object)')
- [IGuardClose](#T-Caliburn-Micro-IGuardClose 'Caliburn.Micro.IGuardClose')
  - [CanCloseAsync(cancellationToken)](#M-Caliburn-Micro-IGuardClose-CanCloseAsync-System-Threading-CancellationToken- 'Caliburn.Micro.IGuardClose.CanCloseAsync(System.Threading.CancellationToken)')
- [IHandle\`1](#T-Caliburn-Micro-IHandle`1 'Caliburn.Micro.IHandle`1')
  - [HandleAsync(message,cancellationToken)](#M-Caliburn-Micro-IHandle`1-HandleAsync-`0,System-Threading-CancellationToken- 'Caliburn.Micro.IHandle`1.HandleAsync(`0,System.Threading.CancellationToken)')
- [IHaveActiveItem](#T-Caliburn-Micro-IHaveActiveItem 'Caliburn.Micro.IHaveActiveItem')
  - [ActiveItem](#P-Caliburn-Micro-IHaveActiveItem-ActiveItem 'Caliburn.Micro.IHaveActiveItem.ActiveItem')
- [IHaveDisplayName](#T-Caliburn-Micro-IHaveDisplayName 'Caliburn.Micro.IHaveDisplayName')
  - [DisplayName](#P-Caliburn-Micro-IHaveDisplayName-DisplayName 'Caliburn.Micro.IHaveDisplayName.DisplayName')
- [ILog](#T-Caliburn-Micro-ILog 'Caliburn.Micro.ILog')
  - [Error(exception)](#M-Caliburn-Micro-ILog-Error-System-Exception- 'Caliburn.Micro.ILog.Error(System.Exception)')
  - [Info(format,args)](#M-Caliburn-Micro-ILog-Info-System-String,System-Object[]- 'Caliburn.Micro.ILog.Info(System.String,System.Object[])')
  - [Warn(format,args)](#M-Caliburn-Micro-ILog-Warn-System-String,System-Object[]- 'Caliburn.Micro.ILog.Warn(System.String,System.Object[])')
- [INotifyPropertyChangedEx](#T-Caliburn-Micro-INotifyPropertyChangedEx 'Caliburn.Micro.INotifyPropertyChangedEx')
  - [IsNotifying](#P-Caliburn-Micro-INotifyPropertyChangedEx-IsNotifying 'Caliburn.Micro.INotifyPropertyChangedEx.IsNotifying')
  - [NotifyOfPropertyChange(propertyName)](#M-Caliburn-Micro-INotifyPropertyChangedEx-NotifyOfPropertyChange-System-String- 'Caliburn.Micro.INotifyPropertyChangedEx.NotifyOfPropertyChange(System.String)')
  - [Refresh()](#M-Caliburn-Micro-INotifyPropertyChangedEx-Refresh 'Caliburn.Micro.INotifyPropertyChangedEx.Refresh')
- [IObservableCollection\`1](#T-Caliburn-Micro-IObservableCollection`1 'Caliburn.Micro.IObservableCollection`1')
  - [AddRange(items)](#M-Caliburn-Micro-IObservableCollection`1-AddRange-System-Collections-Generic-IEnumerable{`0}- 'Caliburn.Micro.IObservableCollection`1.AddRange(System.Collections.Generic.IEnumerable{`0})')
  - [RemoveRange(items)](#M-Caliburn-Micro-IObservableCollection`1-RemoveRange-System-Collections-Generic-IEnumerable{`0}- 'Caliburn.Micro.IObservableCollection`1.RemoveRange(System.Collections.Generic.IEnumerable{`0})')
- [IParent](#T-Caliburn-Micro-IParent 'Caliburn.Micro.IParent')
  - [GetChildren()](#M-Caliburn-Micro-IParent-GetChildren 'Caliburn.Micro.IParent.GetChildren')
- [IParent\`1](#T-Caliburn-Micro-IParent`1 'Caliburn.Micro.IParent`1')
  - [GetChildren()](#M-Caliburn-Micro-IParent`1-GetChildren 'Caliburn.Micro.IParent`1.GetChildren')
- [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider')
  - [InDesignMode](#P-Caliburn-Micro-IPlatformProvider-InDesignMode 'Caliburn.Micro.IPlatformProvider.InDesignMode')
  - [PropertyChangeNotificationsOnUIThread](#P-Caliburn-Micro-IPlatformProvider-PropertyChangeNotificationsOnUIThread 'Caliburn.Micro.IPlatformProvider.PropertyChangeNotificationsOnUIThread')
  - [BeginOnUIThread(action)](#M-Caliburn-Micro-IPlatformProvider-BeginOnUIThread-System-Action- 'Caliburn.Micro.IPlatformProvider.BeginOnUIThread(System.Action)')
  - [ExecuteOnFirstLoad(view,handler)](#M-Caliburn-Micro-IPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}- 'Caliburn.Micro.IPlatformProvider.ExecuteOnFirstLoad(System.Object,System.Action{System.Object})')
  - [ExecuteOnLayoutUpdated(view,handler)](#M-Caliburn-Micro-IPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}- 'Caliburn.Micro.IPlatformProvider.ExecuteOnLayoutUpdated(System.Object,System.Action{System.Object})')
  - [GetFirstNonGeneratedView(view)](#M-Caliburn-Micro-IPlatformProvider-GetFirstNonGeneratedView-System-Object- 'Caliburn.Micro.IPlatformProvider.GetFirstNonGeneratedView(System.Object)')
  - [GetViewCloseAction(viewModel,views,dialogResult)](#M-Caliburn-Micro-IPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}- 'Caliburn.Micro.IPlatformProvider.GetViewCloseAction(System.Object,System.Collections.Generic.ICollection{System.Object},System.Nullable{System.Boolean})')
  - [OnUIThread(action)](#M-Caliburn-Micro-IPlatformProvider-OnUIThread-System-Action- 'Caliburn.Micro.IPlatformProvider.OnUIThread(System.Action)')
  - [OnUIThreadAsync(action)](#M-Caliburn-Micro-IPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}- 'Caliburn.Micro.IPlatformProvider.OnUIThreadAsync(System.Func{System.Threading.Tasks.Task})')
- [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult')
  - [Execute(context)](#M-Caliburn-Micro-IResult-Execute-Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.IResult.Execute(Caliburn.Micro.CoroutineExecutionContext)')
- [IResult\`1](#T-Caliburn-Micro-IResult`1 'Caliburn.Micro.IResult`1')
  - [Result](#P-Caliburn-Micro-IResult`1-Result 'Caliburn.Micro.IResult`1.Result')
- [IScreen](#T-Caliburn-Micro-IScreen 'Caliburn.Micro.IScreen')
- [IViewAware](#T-Caliburn-Micro-IViewAware 'Caliburn.Micro.IViewAware')
  - [AttachView(view,context)](#M-Caliburn-Micro-IViewAware-AttachView-System-Object,System-Object- 'Caliburn.Micro.IViewAware.AttachView(System.Object,System.Object)')
  - [GetView(context)](#M-Caliburn-Micro-IViewAware-GetView-System-Object- 'Caliburn.Micro.IViewAware.GetView(System.Object)')
- [IoC](#T-Caliburn-Micro-IoC 'Caliburn.Micro.IoC')
  - [BuildUp](#F-Caliburn-Micro-IoC-BuildUp 'Caliburn.Micro.IoC.BuildUp')
  - [GetAllInstances](#F-Caliburn-Micro-IoC-GetAllInstances 'Caliburn.Micro.IoC.GetAllInstances')
  - [GetInstance](#F-Caliburn-Micro-IoC-GetInstance 'Caliburn.Micro.IoC.GetInstance')
  - [GetAll\`\`1()](#M-Caliburn-Micro-IoC-GetAll``1 'Caliburn.Micro.IoC.GetAll``1')
  - [Get\`\`1(key)](#M-Caliburn-Micro-IoC-Get``1-System-String- 'Caliburn.Micro.IoC.Get``1(System.String)')
- [LogManager](#T-Caliburn-Micro-LogManager 'Caliburn.Micro.LogManager')
  - [GetLog](#F-Caliburn-Micro-LogManager-GetLog 'Caliburn.Micro.LogManager.GetLog')
- [OneActive](#T-Caliburn-Micro-Conductor`1-Collection-OneActive 'Caliburn.Micro.Conductor`1.Collection.OneActive')
  - [#ctor()](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-#ctor 'Caliburn.Micro.Conductor`1.Collection.OneActive.#ctor')
  - [Items](#P-Caliburn-Micro-Conductor`1-Collection-OneActive-Items 'Caliburn.Micro.Conductor`1.Collection.OneActive.Items')
  - [ActivateItemAsync(item,cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-ActivateItemAsync-`0,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.OneActive.ActivateItemAsync(`0,System.Threading.CancellationToken)')
  - [CanCloseAsync(cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-CanCloseAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.OneActive.CanCloseAsync(System.Threading.CancellationToken)')
  - [DeactivateItemAsync(item,close,cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.OneActive.DeactivateItemAsync(`0,System.Boolean,System.Threading.CancellationToken)')
  - [DetermineNextItemToActivate(list,lastIndex)](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-DetermineNextItemToActivate-System-Collections-Generic-IList{`0},System-Int32- 'Caliburn.Micro.Conductor`1.Collection.OneActive.DetermineNextItemToActivate(System.Collections.Generic.IList{`0},System.Int32)')
  - [EnsureItem(newItem)](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-EnsureItem-`0- 'Caliburn.Micro.Conductor`1.Collection.OneActive.EnsureItem(`0)')
  - [GetChildren()](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-GetChildren 'Caliburn.Micro.Conductor`1.Collection.OneActive.GetChildren')
  - [OnActivatedAsync(cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-OnActivatedAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.OneActive.OnActivatedAsync(System.Threading.CancellationToken)')
  - [OnDeactivateAsync(close,cancellationToken)](#M-Caliburn-Micro-Conductor`1-Collection-OneActive-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.Conductor`1.Collection.OneActive.OnDeactivateAsync(System.Boolean,System.Threading.CancellationToken)')
- [OverrideCancelResultDecorator](#T-Caliburn-Micro-OverrideCancelResultDecorator 'Caliburn.Micro.OverrideCancelResultDecorator')
  - [#ctor(result)](#M-Caliburn-Micro-OverrideCancelResultDecorator-#ctor-Caliburn-Micro-IResult- 'Caliburn.Micro.OverrideCancelResultDecorator.#ctor(Caliburn.Micro.IResult)')
  - [OnInnerResultCompleted(context,innerResult,args)](#M-Caliburn-Micro-OverrideCancelResultDecorator-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs- 'Caliburn.Micro.OverrideCancelResultDecorator.OnInnerResultCompleted(Caliburn.Micro.CoroutineExecutionContext,Caliburn.Micro.IResult,Caliburn.Micro.ResultCompletionEventArgs)')
- [PlatformProvider](#T-Caliburn-Micro-PlatformProvider 'Caliburn.Micro.PlatformProvider')
  - [Current](#P-Caliburn-Micro-PlatformProvider-Current 'Caliburn.Micro.PlatformProvider.Current')
- [PropertyChangedBase](#T-Caliburn-Micro-PropertyChangedBase 'Caliburn.Micro.PropertyChangedBase')
  - [#ctor()](#M-Caliburn-Micro-PropertyChangedBase-#ctor 'Caliburn.Micro.PropertyChangedBase.#ctor')
  - [IsNotifying](#P-Caliburn-Micro-PropertyChangedBase-IsNotifying 'Caliburn.Micro.PropertyChangedBase.IsNotifying')
  - [NotifyOfPropertyChange(propertyName)](#M-Caliburn-Micro-PropertyChangedBase-NotifyOfPropertyChange-System-String- 'Caliburn.Micro.PropertyChangedBase.NotifyOfPropertyChange(System.String)')
  - [NotifyOfPropertyChange\`\`1(property)](#M-Caliburn-Micro-PropertyChangedBase-NotifyOfPropertyChange``1-System-Linq-Expressions-Expression{System-Func{``0}}- 'Caliburn.Micro.PropertyChangedBase.NotifyOfPropertyChange``1(System.Linq.Expressions.Expression{System.Func{``0}})')
  - [OnPropertyChanged(e)](#M-Caliburn-Micro-PropertyChangedBase-OnPropertyChanged-System-ComponentModel-PropertyChangedEventArgs- 'Caliburn.Micro.PropertyChangedBase.OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs)')
  - [OnUIThread(action)](#M-Caliburn-Micro-PropertyChangedBase-OnUIThread-System-Action- 'Caliburn.Micro.PropertyChangedBase.OnUIThread(System.Action)')
  - [Refresh()](#M-Caliburn-Micro-PropertyChangedBase-Refresh 'Caliburn.Micro.PropertyChangedBase.Refresh')
  - [Set\`\`1(oldValue,newValue,propertyName)](#M-Caliburn-Micro-PropertyChangedBase-Set``1-``0@,``0,System-String- 'Caliburn.Micro.PropertyChangedBase.Set``1(``0@,``0,System.String)')
- [RescueResultDecorator\`1](#T-Caliburn-Micro-RescueResultDecorator`1 'Caliburn.Micro.RescueResultDecorator`1')
  - [#ctor(result,coroutine,cancelResult)](#M-Caliburn-Micro-RescueResultDecorator`1-#ctor-Caliburn-Micro-IResult,System-Func{`0,Caliburn-Micro-IResult},System-Boolean- 'Caliburn.Micro.RescueResultDecorator`1.#ctor(Caliburn.Micro.IResult,System.Func{`0,Caliburn.Micro.IResult},System.Boolean)')
  - [OnInnerResultCompleted(context,innerResult,args)](#M-Caliburn-Micro-RescueResultDecorator`1-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs- 'Caliburn.Micro.RescueResultDecorator`1.OnInnerResultCompleted(Caliburn.Micro.CoroutineExecutionContext,Caliburn.Micro.IResult,Caliburn.Micro.ResultCompletionEventArgs)')
- [ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs')
  - [Error](#F-Caliburn-Micro-ResultCompletionEventArgs-Error 'Caliburn.Micro.ResultCompletionEventArgs.Error')
  - [WasCancelled](#F-Caliburn-Micro-ResultCompletionEventArgs-WasCancelled 'Caliburn.Micro.ResultCompletionEventArgs.WasCancelled')
- [ResultDecoratorBase](#T-Caliburn-Micro-ResultDecoratorBase 'Caliburn.Micro.ResultDecoratorBase')
  - [#ctor(result)](#M-Caliburn-Micro-ResultDecoratorBase-#ctor-Caliburn-Micro-IResult- 'Caliburn.Micro.ResultDecoratorBase.#ctor(Caliburn.Micro.IResult)')
  - [Execute(context)](#M-Caliburn-Micro-ResultDecoratorBase-Execute-Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.ResultDecoratorBase.Execute(Caliburn.Micro.CoroutineExecutionContext)')
  - [OnCompleted(args)](#M-Caliburn-Micro-ResultDecoratorBase-OnCompleted-Caliburn-Micro-ResultCompletionEventArgs- 'Caliburn.Micro.ResultDecoratorBase.OnCompleted(Caliburn.Micro.ResultCompletionEventArgs)')
  - [OnInnerResultCompleted(context,innerResult,args)](#M-Caliburn-Micro-ResultDecoratorBase-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs- 'Caliburn.Micro.ResultDecoratorBase.OnInnerResultCompleted(Caliburn.Micro.CoroutineExecutionContext,Caliburn.Micro.IResult,Caliburn.Micro.ResultCompletionEventArgs)')
- [ResultExtensions](#T-Caliburn-Micro-ResultExtensions 'Caliburn.Micro.ResultExtensions')
  - [OverrideCancel(result)](#M-Caliburn-Micro-ResultExtensions-OverrideCancel-Caliburn-Micro-IResult- 'Caliburn.Micro.ResultExtensions.OverrideCancel(Caliburn.Micro.IResult)')
  - [Rescue(result,rescue,cancelResult)](#M-Caliburn-Micro-ResultExtensions-Rescue-Caliburn-Micro-IResult,System-Func{System-Exception,Caliburn-Micro-IResult},System-Boolean- 'Caliburn.Micro.ResultExtensions.Rescue(Caliburn.Micro.IResult,System.Func{System.Exception,Caliburn.Micro.IResult},System.Boolean)')
  - [Rescue\`\`1(result,rescue,cancelResult)](#M-Caliburn-Micro-ResultExtensions-Rescue``1-Caliburn-Micro-IResult,System-Func{``0,Caliburn-Micro-IResult},System-Boolean- 'Caliburn.Micro.ResultExtensions.Rescue``1(Caliburn.Micro.IResult,System.Func{``0,Caliburn.Micro.IResult},System.Boolean)')
  - [WhenCancelled(result,coroutine)](#M-Caliburn-Micro-ResultExtensions-WhenCancelled-Caliburn-Micro-IResult,System-Func{Caliburn-Micro-IResult}- 'Caliburn.Micro.ResultExtensions.WhenCancelled(Caliburn.Micro.IResult,System.Func{Caliburn.Micro.IResult})')
- [Screen](#T-Caliburn-Micro-Screen 'Caliburn.Micro.Screen')
  - [#ctor()](#M-Caliburn-Micro-Screen-#ctor 'Caliburn.Micro.Screen.#ctor')
  - [DisplayName](#P-Caliburn-Micro-Screen-DisplayName 'Caliburn.Micro.Screen.DisplayName')
  - [IsActive](#P-Caliburn-Micro-Screen-IsActive 'Caliburn.Micro.Screen.IsActive')
  - [IsInitialized](#P-Caliburn-Micro-Screen-IsInitialized 'Caliburn.Micro.Screen.IsInitialized')
  - [Parent](#P-Caliburn-Micro-Screen-Parent 'Caliburn.Micro.Screen.Parent')
  - [CanCloseAsync(cancellationToken)](#M-Caliburn-Micro-Screen-CanCloseAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Screen.CanCloseAsync(System.Threading.CancellationToken)')
  - [OnActivateAsync()](#M-Caliburn-Micro-Screen-OnActivateAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Screen.OnActivateAsync(System.Threading.CancellationToken)')
  - [OnActivatedAsync()](#M-Caliburn-Micro-Screen-OnActivatedAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Screen.OnActivatedAsync(System.Threading.CancellationToken)')
  - [OnDeactivateAsync(close,cancellationToken)](#M-Caliburn-Micro-Screen-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.Screen.OnDeactivateAsync(System.Boolean,System.Threading.CancellationToken)')
  - [OnInitializeAsync()](#M-Caliburn-Micro-Screen-OnInitializeAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Screen.OnInitializeAsync(System.Threading.CancellationToken)')
  - [OnInitializedAsync()](#M-Caliburn-Micro-Screen-OnInitializedAsync-System-Threading-CancellationToken- 'Caliburn.Micro.Screen.OnInitializedAsync(System.Threading.CancellationToken)')
  - [TryCloseAsync(dialogResult)](#M-Caliburn-Micro-Screen-TryCloseAsync-System-Nullable{System-Boolean}- 'Caliburn.Micro.Screen.TryCloseAsync(System.Nullable{System.Boolean})')
- [ScreenExtensions](#T-Caliburn-Micro-ScreenExtensions 'Caliburn.Micro.ScreenExtensions')
  - [ActivateWith(child,parent)](#M-Caliburn-Micro-ScreenExtensions-ActivateWith-Caliburn-Micro-IActivate,Caliburn-Micro-IActivate- 'Caliburn.Micro.ScreenExtensions.ActivateWith(Caliburn.Micro.IActivate,Caliburn.Micro.IActivate)')
  - [CloseItemAsync(conductor,item)](#M-Caliburn-Micro-ScreenExtensions-CloseItemAsync-Caliburn-Micro-IConductor,System-Object- 'Caliburn.Micro.ScreenExtensions.CloseItemAsync(Caliburn.Micro.IConductor,System.Object)')
  - [CloseItemAsync(conductor,item,cancellationToken)](#M-Caliburn-Micro-ScreenExtensions-CloseItemAsync-Caliburn-Micro-IConductor,System-Object,System-Threading-CancellationToken- 'Caliburn.Micro.ScreenExtensions.CloseItemAsync(Caliburn.Micro.IConductor,System.Object,System.Threading.CancellationToken)')
  - [CloseItemAsync\`\`1(conductor,item)](#M-Caliburn-Micro-ScreenExtensions-CloseItemAsync``1-Caliburn-Micro-ConductorBase{``0},``0- 'Caliburn.Micro.ScreenExtensions.CloseItemAsync``1(Caliburn.Micro.ConductorBase{``0},``0)')
  - [CloseItemAsync\`\`1(conductor,item,cancellationToken)](#M-Caliburn-Micro-ScreenExtensions-CloseItemAsync``1-Caliburn-Micro-ConductorBase{``0},``0,System-Threading-CancellationToken- 'Caliburn.Micro.ScreenExtensions.CloseItemAsync``1(Caliburn.Micro.ConductorBase{``0},``0,System.Threading.CancellationToken)')
  - [ConductWith\`\`2(child,parent)](#M-Caliburn-Micro-ScreenExtensions-ConductWith``2-``0,``1- 'Caliburn.Micro.ScreenExtensions.ConductWith``2(``0,``1)')
  - [DeactivateWith(child,parent)](#M-Caliburn-Micro-ScreenExtensions-DeactivateWith-Caliburn-Micro-IDeactivate,Caliburn-Micro-IDeactivate- 'Caliburn.Micro.ScreenExtensions.DeactivateWith(Caliburn.Micro.IDeactivate,Caliburn.Micro.IDeactivate)')
  - [TryActivateAsync(potentialActivatable)](#M-Caliburn-Micro-ScreenExtensions-TryActivateAsync-System-Object- 'Caliburn.Micro.ScreenExtensions.TryActivateAsync(System.Object)')
  - [TryActivateAsync(potentialActivatable,cancellationToken)](#M-Caliburn-Micro-ScreenExtensions-TryActivateAsync-System-Object,System-Threading-CancellationToken- 'Caliburn.Micro.ScreenExtensions.TryActivateAsync(System.Object,System.Threading.CancellationToken)')
  - [TryDeactivateAsync(potentialDeactivatable,close)](#M-Caliburn-Micro-ScreenExtensions-TryDeactivateAsync-System-Object,System-Boolean- 'Caliburn.Micro.ScreenExtensions.TryDeactivateAsync(System.Object,System.Boolean)')
  - [TryDeactivateAsync(potentialDeactivatable,close,cancellationToken)](#M-Caliburn-Micro-ScreenExtensions-TryDeactivateAsync-System-Object,System-Boolean,System-Threading-CancellationToken- 'Caliburn.Micro.ScreenExtensions.TryDeactivateAsync(System.Object,System.Boolean,System.Threading.CancellationToken)')
- [SequentialResult](#T-Caliburn-Micro-SequentialResult 'Caliburn.Micro.SequentialResult')
  - [#ctor(enumerator)](#M-Caliburn-Micro-SequentialResult-#ctor-System-Collections-Generic-IEnumerator{Caliburn-Micro-IResult}- 'Caliburn.Micro.SequentialResult.#ctor(System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult})')
  - [Execute(context)](#M-Caliburn-Micro-SequentialResult-Execute-Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.SequentialResult.Execute(Caliburn.Micro.CoroutineExecutionContext)')
- [SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer')
  - [#ctor()](#M-Caliburn-Micro-SimpleContainer-#ctor 'Caliburn.Micro.SimpleContainer.#ctor')
  - [EnablePropertyInjection](#P-Caliburn-Micro-SimpleContainer-EnablePropertyInjection 'Caliburn.Micro.SimpleContainer.EnablePropertyInjection')
  - [ActivateInstance(type,args)](#M-Caliburn-Micro-SimpleContainer-ActivateInstance-System-Type,System-Object[]- 'Caliburn.Micro.SimpleContainer.ActivateInstance(System.Type,System.Object[])')
  - [BuildInstance(type)](#M-Caliburn-Micro-SimpleContainer-BuildInstance-System-Type- 'Caliburn.Micro.SimpleContainer.BuildInstance(System.Type)')
  - [BuildUp(instance)](#M-Caliburn-Micro-SimpleContainer-BuildUp-System-Object- 'Caliburn.Micro.SimpleContainer.BuildUp(System.Object)')
  - [CreateChildContainer()](#M-Caliburn-Micro-SimpleContainer-CreateChildContainer 'Caliburn.Micro.SimpleContainer.CreateChildContainer')
  - [GetAllInstances(service,key)](#M-Caliburn-Micro-SimpleContainer-GetAllInstances-System-Type,System-String- 'Caliburn.Micro.SimpleContainer.GetAllInstances(System.Type,System.String)')
  - [GetInstance(service,key)](#M-Caliburn-Micro-SimpleContainer-GetInstance-System-Type,System-String- 'Caliburn.Micro.SimpleContainer.GetInstance(System.Type,System.String)')
  - [HasHandler(service,key)](#M-Caliburn-Micro-SimpleContainer-HasHandler-System-Type,System-String- 'Caliburn.Micro.SimpleContainer.HasHandler(System.Type,System.String)')
  - [RegisterHandler(service,key,handler)](#M-Caliburn-Micro-SimpleContainer-RegisterHandler-System-Type,System-String,System-Func{Caliburn-Micro-SimpleContainer,System-Object}- 'Caliburn.Micro.SimpleContainer.RegisterHandler(System.Type,System.String,System.Func{Caliburn.Micro.SimpleContainer,System.Object})')
  - [RegisterInstance(service,key,implementation)](#M-Caliburn-Micro-SimpleContainer-RegisterInstance-System-Type,System-String,System-Object- 'Caliburn.Micro.SimpleContainer.RegisterInstance(System.Type,System.String,System.Object)')
  - [RegisterPerRequest(service,key,implementation)](#M-Caliburn-Micro-SimpleContainer-RegisterPerRequest-System-Type,System-String,System-Type- 'Caliburn.Micro.SimpleContainer.RegisterPerRequest(System.Type,System.String,System.Type)')
  - [RegisterSingleton(service,key,implementation)](#M-Caliburn-Micro-SimpleContainer-RegisterSingleton-System-Type,System-String,System-Type- 'Caliburn.Micro.SimpleContainer.RegisterSingleton(System.Type,System.String,System.Type)')
  - [UnregisterHandler(service,key)](#M-Caliburn-Micro-SimpleContainer-UnregisterHandler-System-Type,System-String- 'Caliburn.Micro.SimpleContainer.UnregisterHandler(System.Type,System.String)')
- [SimpleResult](#T-Caliburn-Micro-SimpleResult 'Caliburn.Micro.SimpleResult')
  - [Cancelled()](#M-Caliburn-Micro-SimpleResult-Cancelled 'Caliburn.Micro.SimpleResult.Cancelled')
  - [Execute(context)](#M-Caliburn-Micro-SimpleResult-Execute-Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.SimpleResult.Execute(Caliburn.Micro.CoroutineExecutionContext)')
  - [Failed()](#M-Caliburn-Micro-SimpleResult-Failed-System-Exception- 'Caliburn.Micro.SimpleResult.Failed(System.Exception)')
  - [Succeeded()](#M-Caliburn-Micro-SimpleResult-Succeeded 'Caliburn.Micro.SimpleResult.Succeeded')
- [TaskExtensions](#T-Caliburn-Micro-TaskExtensions 'Caliburn.Micro.TaskExtensions')
  - [AsResult(task)](#M-Caliburn-Micro-TaskExtensions-AsResult-System-Threading-Tasks-Task- 'Caliburn.Micro.TaskExtensions.AsResult(System.Threading.Tasks.Task)')
  - [AsResult\`\`1(task)](#M-Caliburn-Micro-TaskExtensions-AsResult``1-System-Threading-Tasks-Task{``0}- 'Caliburn.Micro.TaskExtensions.AsResult``1(System.Threading.Tasks.Task{``0})')
  - [ExecuteAsync(result,context)](#M-Caliburn-Micro-TaskExtensions-ExecuteAsync-Caliburn-Micro-IResult,Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.TaskExtensions.ExecuteAsync(Caliburn.Micro.IResult,Caliburn.Micro.CoroutineExecutionContext)')
  - [ExecuteAsync\`\`1(result,context)](#M-Caliburn-Micro-TaskExtensions-ExecuteAsync``1-Caliburn-Micro-IResult{``0},Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.TaskExtensions.ExecuteAsync``1(Caliburn.Micro.IResult{``0},Caliburn.Micro.CoroutineExecutionContext)')
- [TaskResult](#T-Caliburn-Micro-TaskResult 'Caliburn.Micro.TaskResult')
  - [#ctor(task)](#M-Caliburn-Micro-TaskResult-#ctor-System-Threading-Tasks-Task- 'Caliburn.Micro.TaskResult.#ctor(System.Threading.Tasks.Task)')
  - [Execute(context)](#M-Caliburn-Micro-TaskResult-Execute-Caliburn-Micro-CoroutineExecutionContext- 'Caliburn.Micro.TaskResult.Execute(Caliburn.Micro.CoroutineExecutionContext)')
  - [OnCompleted(task)](#M-Caliburn-Micro-TaskResult-OnCompleted-System-Threading-Tasks-Task- 'Caliburn.Micro.TaskResult.OnCompleted(System.Threading.Tasks.Task)')
- [TaskResult\`1](#T-Caliburn-Micro-TaskResult`1 'Caliburn.Micro.TaskResult`1')
  - [#ctor(task)](#M-Caliburn-Micro-TaskResult`1-#ctor-System-Threading-Tasks-Task{`0}- 'Caliburn.Micro.TaskResult`1.#ctor(System.Threading.Tasks.Task{`0})')
  - [Result](#P-Caliburn-Micro-TaskResult`1-Result 'Caliburn.Micro.TaskResult`1.Result')
  - [OnCompleted(task)](#M-Caliburn-Micro-TaskResult`1-OnCompleted-System-Threading-Tasks-Task- 'Caliburn.Micro.TaskResult`1.OnCompleted(System.Threading.Tasks.Task)')
- [ViewAttachedEventArgs](#T-Caliburn-Micro-ViewAttachedEventArgs 'Caliburn.Micro.ViewAttachedEventArgs')
  - [Context](#P-Caliburn-Micro-ViewAttachedEventArgs-Context 'Caliburn.Micro.ViewAttachedEventArgs.Context')
  - [View](#P-Caliburn-Micro-ViewAttachedEventArgs-View 'Caliburn.Micro.ViewAttachedEventArgs.View')
- [ViewAware](#T-Caliburn-Micro-ViewAware 'Caliburn.Micro.ViewAware')
  - [#ctor()](#M-Caliburn-Micro-ViewAware-#ctor 'Caliburn.Micro.ViewAware.#ctor')
  - [DefaultContext](#F-Caliburn-Micro-ViewAware-DefaultContext 'Caliburn.Micro.ViewAware.DefaultContext')
  - [Views](#P-Caliburn-Micro-ViewAware-Views 'Caliburn.Micro.ViewAware.Views')
  - [GetView(context)](#M-Caliburn-Micro-ViewAware-GetView-System-Object- 'Caliburn.Micro.ViewAware.GetView(System.Object)')
  - [OnViewAttached(view,context)](#M-Caliburn-Micro-ViewAware-OnViewAttached-System-Object,System-Object- 'Caliburn.Micro.ViewAware.OnViewAttached(System.Object,System.Object)')
  - [OnViewLoaded(view)](#M-Caliburn-Micro-ViewAware-OnViewLoaded-System-Object- 'Caliburn.Micro.ViewAware.OnViewLoaded(System.Object)')
  - [OnViewReady(view)](#M-Caliburn-Micro-ViewAware-OnViewReady-System-Object- 'Caliburn.Micro.ViewAware.OnViewReady(System.Object)')
- [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2')
  - [#ctor()](#M-Caliburn-Micro-WeakValueDictionary`2-#ctor 'Caliburn.Micro.WeakValueDictionary`2.#ctor')
  - [#ctor(dictionary)](#M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Collections-Generic-IDictionary{`0,`1}- 'Caliburn.Micro.WeakValueDictionary`2.#ctor(System.Collections.Generic.IDictionary{`0,`1})')
  - [#ctor(dictionary,comparer)](#M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Collections-Generic-IDictionary{`0,`1},System-Collections-Generic-IEqualityComparer{`0}- 'Caliburn.Micro.WeakValueDictionary`2.#ctor(System.Collections.Generic.IDictionary{`0,`1},System.Collections.Generic.IEqualityComparer{`0})')
  - [#ctor(comparer)](#M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Collections-Generic-IEqualityComparer{`0}- 'Caliburn.Micro.WeakValueDictionary`2.#ctor(System.Collections.Generic.IEqualityComparer{`0})')
  - [#ctor(capacity)](#M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Int32- 'Caliburn.Micro.WeakValueDictionary`2.#ctor(System.Int32)')
  - [#ctor(capacity,comparer)](#M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Int32,System-Collections-Generic-IEqualityComparer{`0}- 'Caliburn.Micro.WeakValueDictionary`2.#ctor(System.Int32,System.Collections.Generic.IEqualityComparer{`0})')
  - [Count](#P-Caliburn-Micro-WeakValueDictionary`2-Count 'Caliburn.Micro.WeakValueDictionary`2.Count')
  - [Item](#P-Caliburn-Micro-WeakValueDictionary`2-Item-`0- 'Caliburn.Micro.WeakValueDictionary`2.Item(`0)')
  - [Keys](#P-Caliburn-Micro-WeakValueDictionary`2-Keys 'Caliburn.Micro.WeakValueDictionary`2.Keys')
  - [Values](#P-Caliburn-Micro-WeakValueDictionary`2-Values 'Caliburn.Micro.WeakValueDictionary`2.Values')
  - [Add(key,value)](#M-Caliburn-Micro-WeakValueDictionary`2-Add-`0,`1- 'Caliburn.Micro.WeakValueDictionary`2.Add(`0,`1)')
  - [Clear()](#M-Caliburn-Micro-WeakValueDictionary`2-Clear 'Caliburn.Micro.WeakValueDictionary`2.Clear')
  - [ContainsKey(key)](#M-Caliburn-Micro-WeakValueDictionary`2-ContainsKey-`0- 'Caliburn.Micro.WeakValueDictionary`2.ContainsKey(`0)')
  - [GetEnumerator()](#M-Caliburn-Micro-WeakValueDictionary`2-GetEnumerator 'Caliburn.Micro.WeakValueDictionary`2.GetEnumerator')
  - [Remove(key)](#M-Caliburn-Micro-WeakValueDictionary`2-Remove-`0- 'Caliburn.Micro.WeakValueDictionary`2.Remove(`0)')
  - [TryGetValue(key,value)](#M-Caliburn-Micro-WeakValueDictionary`2-TryGetValue-`0,`1@- 'Caliburn.Micro.WeakValueDictionary`2.TryGetValue(`0,`1@)')

<a name='T-Caliburn-Micro-ActivateExtensions'></a>
## ActivateExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods for the [IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate') instance.

<a name='M-Caliburn-Micro-ActivateExtensions-ActivateAsync-Caliburn-Micro-IActivate-'></a>
### ActivateAsync(activate) `method`

##### Summary

Activates this instance.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activate | [Caliburn.Micro.IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate') | The instance to activate |

<a name='T-Caliburn-Micro-ActivationEventArgs'></a>
## ActivationEventArgs `type`

##### Namespace

Caliburn.Micro

##### Summary

EventArgs sent during activation.

<a name='P-Caliburn-Micro-ActivationEventArgs-WasInitialized'></a>
### WasInitialized `property`

##### Summary

Indicates whether the sender was initialized in addition to being activated.

<a name='T-Caliburn-Micro-ActivationProcessedEventArgs'></a>
## ActivationProcessedEventArgs `type`

##### Namespace

Caliburn.Micro

##### Summary

Contains details about the success or failure of an item's activation through an [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor').

<a name='P-Caliburn-Micro-ActivationProcessedEventArgs-Item'></a>
### Item `property`

##### Summary

The item whose activation was processed.

<a name='P-Caliburn-Micro-ActivationProcessedEventArgs-Success'></a>
### Success `property`

##### Summary

Gets or sets a value indicating whether the activation was a success.

<a name='T-Caliburn-Micro-Conductor`1-Collection-AllActive'></a>
## AllActive `type`

##### Namespace

Caliburn.Micro.Conductor`1.Collection

##### Summary

An implementation of [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') that holds on to many items which are all activated.

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-#ctor-System-Boolean-'></a>
### #ctor(openPublicItems) `constructor`

##### Summary

Initializes a new instance of the [AllActive](#T-Caliburn-Micro-Conductor`1-Collection-AllActive 'Caliburn.Micro.Conductor`1.Collection.AllActive') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| openPublicItems | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` opens public items that are properties of this class. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [AllActive](#T-Caliburn-Micro-Conductor`1-Collection-AllActive 'Caliburn.Micro.Conductor`1.Collection.AllActive') class.

##### Parameters

This constructor has no parameters.

<a name='P-Caliburn-Micro-Conductor`1-Collection-AllActive-Items'></a>
### Items `property`

##### Summary

Gets the items that are currently being conducted.

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-ActivateItemAsync-`0,System-Threading-CancellationToken-'></a>
### ActivateItemAsync(item,cancellationToken) `method`

##### Summary

Activates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item to activate. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-CanCloseAsync-System-Threading-CancellationToken-'></a>
### CanCloseAsync(cancellationToken) `method`

##### Summary

Called to check whether or not this instance can close.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken-'></a>
### DeactivateItemAsync(item,close,cancellationToken) `method`

##### Summary

Deactivates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item to close. |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the item after deactivating it. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-EnsureItem-`0-'></a>
### EnsureItem(newItem) `method`

##### Summary

Ensures that an item is ready to be activated.

##### Returns

The item to be activated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [\`0](#T-`0 '`0') | The item that is about to be activated. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-GetChildren'></a>
### GetChildren() `method`

##### Summary

Gets the children.

##### Returns

The collection of children.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-OnActivatedAsync-System-Threading-CancellationToken-'></a>
### OnActivatedAsync() `method`

##### Summary

Called when view has been activated.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken-'></a>
### OnDeactivateAsync(close,cancellationToken) `method`

##### Summary

Called when deactivating.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether this instance will be closed. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-AllActive-OnInitializedAsync-System-Threading-CancellationToken-'></a>
### OnInitializedAsync() `method`

##### Summary

Called when view has been initialized

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-AsyncEventHandlerExtensions'></a>
## AsyncEventHandlerExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

AsyncEventHandlerExtensions class.

##### Remarks

Contains helper functions to run Invoke methods asynchronously.

<a name='M-Caliburn-Micro-AsyncEventHandlerExtensions-GetHandlers``1-Caliburn-Micro-AsyncEventHandler{``0}-'></a>
### GetHandlers\`\`1(handler) `method`

##### Summary

Gets the invocation list of the specified async event handler.

##### Returns

An enumerable of async event handlers.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| handler | [Caliburn.Micro.AsyncEventHandler{\`\`0}](#T-Caliburn-Micro-AsyncEventHandler{``0} 'Caliburn.Micro.AsyncEventHandler{``0}') | The async event handler. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TEventArgs | The type of the event arguments. |

<a name='M-Caliburn-Micro-AsyncEventHandlerExtensions-InvokeAllAsync``1-Caliburn-Micro-AsyncEventHandler{``0},System-Object,``0-'></a>
### InvokeAllAsync\`\`1(handler,sender,e) `method`

##### Summary

Invokes all handlers of the specified async event handler asynchronously.

##### Returns

A task that represents the completion of all handler invocations.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| handler | [Caliburn.Micro.AsyncEventHandler{\`\`0}](#T-Caliburn-Micro-AsyncEventHandler{``0} 'Caliburn.Micro.AsyncEventHandler{``0}') | The async event handler. |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The source of the event. |
| e | [\`\`0](#T-``0 '``0') | The event data. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TEventArgs | The type of the event arguments. |

<a name='T-Caliburn-Micro-AsyncEventHandler`1'></a>
## AsyncEventHandler\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Represents an asynchronous event handler.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [T:Caliburn.Micro.AsyncEventHandler\`1](#T-T-Caliburn-Micro-AsyncEventHandler`1 'T:Caliburn.Micro.AsyncEventHandler`1') | The source of the event. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TEventArgs | The type of the event data generated by the event. |

<a name='T-Caliburn-Micro-BindableCollection`1'></a>
## BindableCollection\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

A base collection class that supports automatic UI thread marshalling.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of elements contained in the collection. |

<a name='M-Caliburn-Micro-BindableCollection`1-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [BindableCollection\`1](#T-Caliburn-Micro-BindableCollection`1 'Caliburn.Micro.BindableCollection`1') class.

##### Parameters

This constructor has no parameters.

<a name='M-Caliburn-Micro-BindableCollection`1-#ctor-System-Collections-Generic-IEnumerable{`0}-'></a>
### #ctor(collection) `constructor`

##### Summary

Initializes a new instance of the [BindableCollection\`1](#T-Caliburn-Micro-BindableCollection`1 'Caliburn.Micro.BindableCollection`1') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collection | [System.Collections.Generic.IEnumerable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{`0}') | The collection from which the elements are copied. |

<a name='P-Caliburn-Micro-BindableCollection`1-IsNotifying'></a>
### IsNotifying `property`

##### Summary

Enables/Disables property change notification.

<a name='M-Caliburn-Micro-BindableCollection`1-AddRange-System-Collections-Generic-IEnumerable{`0}-'></a>
### AddRange(items) `method`

##### Summary

Adds the range.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| items | [System.Collections.Generic.IEnumerable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{`0}') | The items. |

<a name='M-Caliburn-Micro-BindableCollection`1-ClearItems'></a>
### ClearItems() `method`

##### Summary

Clears the items contained by the collection.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-BindableCollection`1-ClearItemsBase'></a>
### ClearItemsBase() `method`

##### Summary

Exposes the base implementation of the [ClearItems](#M-Caliburn-Micro-BindableCollection`1-ClearItems 'Caliburn.Micro.BindableCollection`1.ClearItems') function.

##### Parameters

This method has no parameters.

##### Remarks

Used to avoid compiler warning regarding unverifiable code.

<a name='M-Caliburn-Micro-BindableCollection`1-InsertItem-System-Int32,`0-'></a>
### InsertItem(index,item) `method`

##### Summary

Inserts the item to the specified position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index to insert at. |
| item | [\`0](#T-`0 '`0') | The item to be inserted. |

<a name='M-Caliburn-Micro-BindableCollection`1-InsertItemBase-System-Int32,`0-'></a>
### InsertItemBase(index,item) `method`

##### Summary

Exposes the base implementation of the [InsertItem](#M-Caliburn-Micro-BindableCollection`1-InsertItem-System-Int32,`0- 'Caliburn.Micro.BindableCollection`1.InsertItem(System.Int32,`0)') function.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index. |
| item | [\`0](#T-`0 '`0') | The item. |

##### Remarks

Used to avoid compiler warning regarding unverifiable code.

<a name='M-Caliburn-Micro-BindableCollection`1-NotifyOfPropertyChange-System-String-'></a>
### NotifyOfPropertyChange(propertyName) `method`

##### Summary

Notifies subscribers of the property change.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| propertyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the property. |

<a name='M-Caliburn-Micro-BindableCollection`1-OnCollectionChanged-System-Collections-Specialized-NotifyCollectionChangedEventArgs-'></a>
### OnCollectionChanged(e) `method`

##### Summary

Raises the [](#E-System-Collections-ObjectModel-ObservableCollection`1-CollectionChanged 'System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged') event with the provided arguments.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| e | [System.Collections.Specialized.NotifyCollectionChangedEventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Specialized.NotifyCollectionChangedEventArgs 'System.Collections.Specialized.NotifyCollectionChangedEventArgs') | Arguments of the event being raised. |

<a name='M-Caliburn-Micro-BindableCollection`1-OnPropertyChanged-System-ComponentModel-PropertyChangedEventArgs-'></a>
### OnPropertyChanged(e) `method`

##### Summary

Raises the PropertyChanged event with the provided arguments.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| e | [System.ComponentModel.PropertyChangedEventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ComponentModel.PropertyChangedEventArgs 'System.ComponentModel.PropertyChangedEventArgs') | The event data to report in the event. |

<a name='M-Caliburn-Micro-BindableCollection`1-OnUIThread-System-Action-'></a>
### OnUIThread(action) `method`

##### Summary

Executes the given action on the UI thread

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') |  |

##### Remarks

An extension point for subclasses to customise how property change notifications are handled.

<a name='M-Caliburn-Micro-BindableCollection`1-Refresh'></a>
### Refresh() `method`

##### Summary

Raises a change notification indicating that all bindings should be refreshed.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-BindableCollection`1-RemoveItem-System-Int32-'></a>
### RemoveItem(index) `method`

##### Summary

Removes the item at the specified position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position used to identify the item to remove. |

<a name='M-Caliburn-Micro-BindableCollection`1-RemoveItemBase-System-Int32-'></a>
### RemoveItemBase(index) `method`

##### Summary

Exposes the base implementation of the [RemoveItem](#M-Caliburn-Micro-BindableCollection`1-RemoveItem-System-Int32- 'Caliburn.Micro.BindableCollection`1.RemoveItem(System.Int32)') function.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index. |

##### Remarks

Used to avoid compiler warning regarding unverifiable code.

<a name='M-Caliburn-Micro-BindableCollection`1-RemoveRange-System-Collections-Generic-IEnumerable{`0}-'></a>
### RemoveRange(items) `method`

##### Summary

Removes the range.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| items | [System.Collections.Generic.IEnumerable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{`0}') | The items. |

<a name='M-Caliburn-Micro-BindableCollection`1-SetItem-System-Int32,`0-'></a>
### SetItem(index,item) `method`

##### Summary

Sets the item at the specified position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index to set the item at. |
| item | [\`0](#T-`0 '`0') | The item to set. |

<a name='M-Caliburn-Micro-BindableCollection`1-SetItemBase-System-Int32,`0-'></a>
### SetItemBase(index,item) `method`

##### Summary

Exposes the base implementation of the [SetItem](#M-Caliburn-Micro-BindableCollection`1-SetItem-System-Int32,`0- 'Caliburn.Micro.BindableCollection`1.SetItem(System.Int32,`0)') function.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index. |
| item | [\`0](#T-`0 '`0') | The item. |

##### Remarks

Used to avoid compiler warning regarding unverifiable code.

<a name='T-Caliburn-Micro-CloseResult`1'></a>
## CloseResult\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

The result of a test whether an instance can be closed.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the children of the instance. |

<a name='M-Caliburn-Micro-CloseResult`1-#ctor-System-Boolean,System-Collections-Generic-IEnumerable{`0}-'></a>
### #ctor(closeCanOccur,children) `constructor`

##### Summary

Creates an instance of the [CloseResult\`1](#T-Caliburn-Micro-CloseResult`1 'Caliburn.Micro.CloseResult`1')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| closeCanOccur | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether of not a close operation should occur. |
| children | [System.Collections.Generic.IEnumerable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{`0}') | The children of the instance that can be closed. |

<a name='P-Caliburn-Micro-CloseResult`1-Children'></a>
### Children `property`

##### Summary

The children of the instance that can be closed.

<a name='P-Caliburn-Micro-CloseResult`1-CloseCanOccur'></a>
### CloseCanOccur `property`

##### Summary

Whether of not a close operation should occur.

<a name='T-Caliburn-Micro-Conductor`1-Collection'></a>
## Collection `type`

##### Namespace

Caliburn.Micro.Conductor`1

##### Summary

An implementation of [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') that holds on many items.

<a name='T-Caliburn-Micro-ConductorBaseWithActiveItem`1'></a>
## ConductorBaseWithActiveItem\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

A base class for various implementations of [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') that maintain an active item.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type that is being conducted. |

<a name='P-Caliburn-Micro-ConductorBaseWithActiveItem`1-ActiveItem'></a>
### ActiveItem `property`

##### Summary

The currently active item.

<a name='P-Caliburn-Micro-ConductorBaseWithActiveItem`1-Caliburn#Micro#IHaveActiveItem#ActiveItem'></a>
### Caliburn#Micro#IHaveActiveItem#ActiveItem `property`

##### Summary

The currently active item.

<a name='M-Caliburn-Micro-ConductorBaseWithActiveItem`1-ChangeActiveItemAsync-`0,System-Boolean,System-Threading-CancellationToken-'></a>
### ChangeActiveItemAsync(newItem,closePrevious,cancellationToken) `method`

##### Summary

Changes the active item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [\`0](#T-`0 '`0') | The new item to activate. |
| closePrevious | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the previous active item. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-ConductorBaseWithActiveItem`1-ChangeActiveItemAsync-`0,System-Boolean-'></a>
### ChangeActiveItemAsync(newItem,closePrevious) `method`

##### Summary

Changes the active item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [\`0](#T-`0 '`0') | The new item to activate. |
| closePrevious | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the previous active item. |

<a name='T-Caliburn-Micro-ConductorBase`1'></a>
## ConductorBase\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

A base class for various implementations of [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor').

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type that is being conducted. |

<a name='P-Caliburn-Micro-ConductorBase`1-CloseStrategy'></a>
### CloseStrategy `property`

##### Summary

Gets or sets the close strategy.

<a name='M-Caliburn-Micro-ConductorBase`1-ActivateItemAsync-`0,System-Threading-CancellationToken-'></a>
### ActivateItemAsync(item,cancellationToken) `method`

##### Summary

Activates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item to activate. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='M-Caliburn-Micro-ConductorBase`1-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken-'></a>
### DeactivateItemAsync(item,close,cancellationToken) `method`

##### Summary

Deactivates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item to close. |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the item after deactivating it. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-ConductorBase`1-EnsureItem-`0-'></a>
### EnsureItem(newItem) `method`

##### Summary

Ensures that an item is ready to be activated.

##### Returns

The item to be activated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [\`0](#T-`0 '`0') | The item that is about to be activated. |

<a name='M-Caliburn-Micro-ConductorBase`1-GetChildren'></a>
### GetChildren() `method`

##### Summary

Gets the children.

##### Returns

The collection of children.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-ConductorBase`1-OnActivationProcessed-`0,System-Boolean-'></a>
### OnActivationProcessed(item,success) `method`

##### Summary

Called by a subclass when an activation needs processing.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item on which activation was attempted. |
| success | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` activation was successful. |

<a name='T-Caliburn-Micro-ConductorExtensions'></a>
## ConductorExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods for the [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') instance.

<a name='M-Caliburn-Micro-ConductorExtensions-ActivateItemAsync-Caliburn-Micro-IConductor,System-Object-'></a>
### ActivateItemAsync(conductor,item) `method`

##### Summary

Activates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| conductor | [Caliburn.Micro.IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') | The conductor to activate the item with. |
| item | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The item to activate. |

<a name='T-Caliburn-Micro-Conductor`1'></a>
## Conductor\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

An implementation of [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') that holds on to and activates only one item at a time.

<a name='M-Caliburn-Micro-Conductor`1-ActivateItemAsync-`0,System-Threading-CancellationToken-'></a>
### ActivateItemAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Conductor`1-CanCloseAsync-System-Threading-CancellationToken-'></a>
### CanCloseAsync(cancellationToken) `method`

##### Summary

Called to check whether or not this instance can close.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken-'></a>
### DeactivateItemAsync(item,close,cancellationToken) `method`

##### Summary

Deactivates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item to close. |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the item after deactivating it. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-GetChildren'></a>
### GetChildren() `method`

##### Summary

Gets the children.

##### Returns

The collection of children.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Conductor`1-OnActivatedAsync-System-Threading-CancellationToken-'></a>
### OnActivatedAsync() `method`

##### Summary

Called when view has been activated.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Conductor`1-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken-'></a>
### OnDeactivateAsync(close,cancellationToken) `method`

##### Summary

Called when deactivating.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether this instance will be closed. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-ContainerExtensions'></a>
## ContainerExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods for the [SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer').

<a name='M-Caliburn-Micro-ContainerExtensions-AllTypesOf``1-Caliburn-Micro-SimpleContainer,System-Reflection-Assembly,System-Func{System-Type,System-Boolean}-'></a>
### AllTypesOf\`\`1(container,assembly,filter) `method`

##### Summary

Registers all specified types in an assembly as singleton in the container.

##### Returns

The container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| assembly | [System.Reflection.Assembly](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.Assembly 'System.Reflection.Assembly') | The assembly. |
| filter | [System.Func{System.Type,System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Type,System.Boolean}') | The type filter. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The type of the service. |

<a name='M-Caliburn-Micro-ContainerExtensions-GetAllInstances``1-Caliburn-Micro-SimpleContainer,System-String-'></a>
### GetAllInstances\`\`1(container,key) `method`

##### Summary

Gets all instances of a particular type and the given key (default null).

##### Returns

The resolved instances.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key shared by those instances |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The type to resolve. |

<a name='M-Caliburn-Micro-ContainerExtensions-GetInstance``1-Caliburn-Micro-SimpleContainer,System-String-'></a>
### GetInstance\`\`1(container,key) `method`

##### Summary

Requests an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The type of the service. |

<a name='M-Caliburn-Micro-ContainerExtensions-Handler``1-Caliburn-Micro-SimpleContainer,System-Func{Caliburn-Micro-SimpleContainer,System-Object}-'></a>
### Handler\`\`1(container,handler) `method`

##### Summary

Registers a custom service handler with the container.

##### Returns

The container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| handler | [System.Func{Caliburn.Micro.SimpleContainer,System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{Caliburn.Micro.SimpleContainer,System.Object}') | The handler. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The type of the service. |

<a name='M-Caliburn-Micro-ContainerExtensions-HasHandler``1-Caliburn-Micro-SimpleContainer,System-String-'></a>
### HasHandler\`\`1(container,key) `method`

##### Summary

Determines if a handler for the service/key has previously been registered.

##### Returns

True if a handler is registere; false otherwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The service type. |

<a name='M-Caliburn-Micro-ContainerExtensions-Instance``1-Caliburn-Micro-SimpleContainer,``0-'></a>
### Instance\`\`1(container,instance) `method`

##### Summary

Registers an instance with the container.

##### Returns

The container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| instance | [\`\`0](#T-``0 '``0') | The instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The type of the service. |

<a name='M-Caliburn-Micro-ContainerExtensions-PerRequest``1-Caliburn-Micro-SimpleContainer,System-String-'></a>
### PerRequest\`\`1(container,key) `method`

##### Summary

Registers an service to be created on each request.

##### Returns

The container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TImplementation | The type of the implementation. |

<a name='M-Caliburn-Micro-ContainerExtensions-PerRequest``2-Caliburn-Micro-SimpleContainer,System-String-'></a>
### PerRequest\`\`2(container,key) `method`

##### Summary

Registers an service to be created on each request.

##### Returns

The container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The type of the service. |
| TImplementation | The type of the implementation. |

<a name='M-Caliburn-Micro-ContainerExtensions-Singleton``1-Caliburn-Micro-SimpleContainer,System-String-'></a>
### Singleton\`\`1(container,key) `method`

##### Summary

Registers a singleton.

##### Returns

The container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TImplementation | The type of the implementation. |

<a name='M-Caliburn-Micro-ContainerExtensions-Singleton``2-Caliburn-Micro-SimpleContainer,System-String-'></a>
### Singleton\`\`2(container,key) `method`

##### Summary

Registers a singleton.

##### Returns

The container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The type of the service. |
| TImplementation | The type of the implementation. |

<a name='M-Caliburn-Micro-ContainerExtensions-UnregisterHandler``1-Caliburn-Micro-SimpleContainer,System-String-'></a>
### UnregisterHandler\`\`1(container,key) `method`

##### Summary

Unregisters any handlers for the service/key that have previously been registered.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [Caliburn.Micro.SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') | The container. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TService | The service type. |

<a name='T-Caliburn-Micro-ContinueResultDecorator'></a>
## ContinueResultDecorator `type`

##### Namespace

Caliburn.Micro

##### Summary

A result decorator which executes a coroutine when the wrapped result was cancelled.

<a name='M-Caliburn-Micro-ContinueResultDecorator-#ctor-Caliburn-Micro-IResult,System-Func{Caliburn-Micro-IResult}-'></a>
### #ctor(result,coroutine) `constructor`

##### Summary

Initializes a new instance of the [ContinueResultDecorator](#T-Caliburn-Micro-ContinueResultDecorator 'Caliburn.Micro.ContinueResultDecorator') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |
| coroutine | [System.Func{Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{Caliburn.Micro.IResult}') | The coroutine to execute when `result` was canceled. |

<a name='M-Caliburn-Micro-ContinueResultDecorator-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs-'></a>
### OnInnerResultCompleted(context,innerResult,args) `method`

##### Summary

Called when the execution of the decorated result has completed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |
| innerResult | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The decorated result. |
| args | [Caliburn.Micro.ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') | The [ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') instance containing the event data. |

<a name='T-Caliburn-Micro-Coroutine'></a>
## Coroutine `type`

##### Namespace

Caliburn.Micro

##### Summary

Manages coroutine execution.

<a name='F-Caliburn-Micro-Coroutine-CreateParentEnumerator'></a>
### CreateParentEnumerator `constants`

##### Summary

Creates the parent enumerator.

<a name='M-Caliburn-Micro-Coroutine-BeginExecute-System-Collections-Generic-IEnumerator{Caliburn-Micro-IResult},Caliburn-Micro-CoroutineExecutionContext,System-EventHandler{Caliburn-Micro-ResultCompletionEventArgs}-'></a>
### BeginExecute(coroutine,context,callback) `method`

##### Summary

Executes a coroutine.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| coroutine | [System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerator 'System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult}') | The coroutine to execute. |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context to execute the coroutine within. |
| callback | [System.EventHandler{Caliburn.Micro.ResultCompletionEventArgs}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.EventHandler 'System.EventHandler{Caliburn.Micro.ResultCompletionEventArgs}') | The completion callback for the coroutine. |

<a name='M-Caliburn-Micro-Coroutine-ExecuteAsync-System-Collections-Generic-IEnumerator{Caliburn-Micro-IResult},Caliburn-Micro-CoroutineExecutionContext-'></a>
### ExecuteAsync(coroutine,context) `method`

##### Summary

Executes a coroutine asynchronous.

##### Returns

A task that represents the asynchronous coroutine.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| coroutine | [System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerator 'System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult}') | The coroutine to execute. |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context to execute the coroutine within. |

<a name='T-Caliburn-Micro-CoroutineExecutionContext'></a>
## CoroutineExecutionContext `type`

##### Namespace

Caliburn.Micro

##### Summary

The context used during the execution of a Coroutine.

<a name='P-Caliburn-Micro-CoroutineExecutionContext-Source'></a>
### Source `property`

##### Summary

The source from which the message originates.

<a name='P-Caliburn-Micro-CoroutineExecutionContext-Target'></a>
### Target `property`

##### Summary

The instance on which the action is invoked.

<a name='P-Caliburn-Micro-CoroutineExecutionContext-View'></a>
### View `property`

##### Summary

The view associated with the target.

<a name='T-Caliburn-Micro-DeactivateExtensions'></a>
## DeactivateExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods for the [IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate') instance.

<a name='M-Caliburn-Micro-DeactivateExtensions-DeactivateAsync-Caliburn-Micro-IDeactivate,System-Boolean-'></a>
### DeactivateAsync(deactivate,close) `method`

##### Summary

Deactivates this instance.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| deactivate | [Caliburn.Micro.IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate') | The instance to deactivate |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not this instance is being closed. |

<a name='T-Caliburn-Micro-DeactivationEventArgs'></a>
## DeactivationEventArgs `type`

##### Namespace

Caliburn.Micro

##### Summary

EventArgs sent during deactivation.

<a name='P-Caliburn-Micro-DeactivationEventArgs-WasClosed'></a>
### WasClosed `property`

##### Summary

Indicates whether the sender was closed in addition to being deactivated.

<a name='T-Caliburn-Micro-DebugLog'></a>
## DebugLog `type`

##### Namespace

Caliburn.Micro

##### Summary

A simple logger thats logs everything to the debugger.

<a name='M-Caliburn-Micro-DebugLog-#ctor-System-Type-'></a>
### #ctor(type) `constructor`

##### Summary

Initializes a new instance of the [DebugLog](#T-Caliburn-Micro-DebugLog 'Caliburn.Micro.DebugLog') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type. |

<a name='M-Caliburn-Micro-DebugLog-Error-System-Exception-'></a>
### Error(exception) `method`

##### Summary

Logs the exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception. |

<a name='M-Caliburn-Micro-DebugLog-Info-System-String,System-Object[]-'></a>
### Info(format,args) `method`

##### Summary

Logs the message as info.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| format | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A formatted message. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Parameters to be injected into the formatted message. |

<a name='M-Caliburn-Micro-DebugLog-Warn-System-String,System-Object[]-'></a>
### Warn(format,args) `method`

##### Summary

Logs the message as a warning.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| format | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A formatted message. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Parameters to be injected into the formatted message. |

<a name='T-Caliburn-Micro-DefaultCloseStrategy`1'></a>
## DefaultCloseStrategy\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Used to gather the results from multiple child elements which may or may not prevent closing.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of child element. |

<a name='M-Caliburn-Micro-DefaultCloseStrategy`1-#ctor-System-Boolean-'></a>
### #ctor(closeConductedItemsWhenConductorCannotClose) `constructor`

##### Summary

Creates an instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| closeConductedItemsWhenConductorCannotClose | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates that even if all conducted items are not closable, those that are should be closed. The default is FALSE. |

<a name='M-Caliburn-Micro-DefaultCloseStrategy`1-ExecuteAsync-System-Collections-Generic-IEnumerable{`0},System-Threading-CancellationToken-'></a>
### ExecuteAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-DefaultPlatformProvider'></a>
## DefaultPlatformProvider `type`

##### Namespace

Caliburn.Micro

##### Summary

Default implementation for [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider') that does no platform enlightenment.

<a name='P-Caliburn-Micro-DefaultPlatformProvider-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Indicates whether or not the framework is in design-time mode.

<a name='P-Caliburn-Micro-DefaultPlatformProvider-PropertyChangeNotificationsOnUIThread'></a>
### PropertyChangeNotificationsOnUIThread `property`

##### Summary

Whether or not classes should execute property change notications on the UI thread.

<a name='M-Caliburn-Micro-DefaultPlatformProvider-BeginOnUIThread-System-Action-'></a>
### BeginOnUIThread(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-DefaultPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnFirstLoad(view,handler) `method`

##### Summary

Executes the handler the fist time the view is loaded.

##### Returns

true if the handler was executed immediately; false otherwise

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-DefaultPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnLayoutUpdated(view,handler) `method`

##### Summary

Executes the handler the next time the view's LayoutUpdated event fires.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-DefaultPlatformProvider-GetFirstNonGeneratedView-System-Object-'></a>
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

<a name='M-Caliburn-Micro-DefaultPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}-'></a>
### GetViewCloseAction(viewModel,views,dialogResult) `method`

##### Summary

Get the close action for the specified view model.

##### Returns

An [Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') to close the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model to close. |
| views | [System.Collections.Generic.ICollection{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.ICollection 'System.Collections.Generic.ICollection{System.Object}') | The associated views. |
| dialogResult | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | The dialog result. |

<a name='M-Caliburn-Micro-DefaultPlatformProvider-OnUIThread-System-Action-'></a>
### OnUIThread(action) `method`

##### Summary

Executes the action on the UI thread.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-DefaultPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}-'></a>
### OnUIThreadAsync(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') | The action to execute. |

<a name='T-Caliburn-Micro-DelegateResult'></a>
## DelegateResult `type`

##### Namespace

Caliburn.Micro

##### Summary

A result that executes an [Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action').

<a name='M-Caliburn-Micro-DelegateResult-#ctor-System-Action-'></a>
### #ctor(action) `constructor`

##### Summary

Initializes a new instance of the [DelegateResult](#T-Caliburn-Micro-DelegateResult 'Caliburn.Micro.DelegateResult') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action. |

<a name='M-Caliburn-Micro-DelegateResult-Execute-Caliburn-Micro-CoroutineExecutionContext-'></a>
### Execute(context) `method`

##### Summary

Executes the result using the specified context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |

<a name='T-Caliburn-Micro-DelegateResult`1'></a>
## DelegateResult\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

A result that executes a [Func\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func`1 'System.Func`1')

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TResult | The type of the result. |

<a name='M-Caliburn-Micro-DelegateResult`1-#ctor-System-Func{`0}-'></a>
### #ctor(action) `constructor`

##### Summary

Initializes a new instance of the [DelegateResult\`1](#T-Caliburn-Micro-DelegateResult`1 'Caliburn.Micro.DelegateResult`1') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Func{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{`0}') | The action. |

<a name='P-Caliburn-Micro-DelegateResult`1-Result'></a>
### Result `property`

##### Summary

Gets the result.

<a name='M-Caliburn-Micro-DelegateResult`1-Execute-Caliburn-Micro-CoroutineExecutionContext-'></a>
### Execute(context) `method`

##### Summary

Executes the result using the specified context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |

<a name='T-Caliburn-Micro-EnumerableExtensions'></a>
## EnumerableExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods for [IEnumerable\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable`1 'System.Collections.Generic.IEnumerable`1')

<a name='M-Caliburn-Micro-EnumerableExtensions-Apply``1-System-Collections-Generic-IEnumerable{``0},System-Action{``0}-'></a>
### Apply\`\`1(enumerable,action) `method`

##### Summary

Applies the action to each element in the list.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| enumerable | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | The elements to enumerate. |
| action | [System.Action{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{``0}') | The action to apply to each item in the list. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The enumerable item's type. |

<a name='T-Caliburn-Micro-EventAggregator'></a>
## EventAggregator `type`

##### Namespace

Caliburn.Micro

##### Summary

*Inherit from parent.*

<a name='M-Caliburn-Micro-EventAggregator-HandlerExistsFor-System-Type-'></a>
### HandlerExistsFor() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-EventAggregator-PublishAsync-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task},System-Threading-CancellationToken-'></a>
### PublishAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-EventAggregator-Subscribe-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task}-'></a>
### Subscribe() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-EventAggregator-Unsubscribe-System-Object-'></a>
### Unsubscribe() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-EventAggregatorExtensions'></a>
## EventAggregatorExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extensions for [IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator').

<a name='M-Caliburn-Micro-EventAggregatorExtensions-PublishOnBackgroundThreadAsync-Caliburn-Micro-IEventAggregator,System-Object,System-Threading-CancellationToken-'></a>
### PublishOnBackgroundThreadAsync(eventAggregator,message,cancellationToken) `method`

##### Summary

Publishes a message on a background thread (async).

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') | The event aggregator. |
| message | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message instance. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='M-Caliburn-Micro-EventAggregatorExtensions-PublishOnBackgroundThreadAsync-Caliburn-Micro-IEventAggregator,System-Object-'></a>
### PublishOnBackgroundThreadAsync(eventAggregator,message) `method`

##### Summary

Publishes a message on a background thread (async).

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') | The event aggregator. |
| message | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message instance. |

<a name='M-Caliburn-Micro-EventAggregatorExtensions-PublishOnCurrentThreadAsync-Caliburn-Micro-IEventAggregator,System-Object,System-Threading-CancellationToken-'></a>
### PublishOnCurrentThreadAsync(eventAggregator,message,cancellationToken) `method`

##### Summary

Publishes a message on the current thread (synchrone).

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') | The event aggregator. |
| message | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message instance. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='M-Caliburn-Micro-EventAggregatorExtensions-PublishOnCurrentThreadAsync-Caliburn-Micro-IEventAggregator,System-Object-'></a>
### PublishOnCurrentThreadAsync(eventAggregator,message) `method`

##### Summary

Publishes a message on the current thread (synchrone).

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') | The event aggregator. |
| message | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message instance. |

<a name='M-Caliburn-Micro-EventAggregatorExtensions-PublishOnUIThreadAsync-Caliburn-Micro-IEventAggregator,System-Object,System-Threading-CancellationToken-'></a>
### PublishOnUIThreadAsync(eventAggregator,message,cancellationToken) `method`

##### Summary

Publishes a message on the UI thread.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') | The event aggregator. |
| message | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message instance. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='M-Caliburn-Micro-EventAggregatorExtensions-PublishOnUIThreadAsync-Caliburn-Micro-IEventAggregator,System-Object-'></a>
### PublishOnUIThreadAsync(eventAggregator,message) `method`

##### Summary

Publishes a message on the UI thread.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') | The event aggregator. |
| message | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message instance. |

<a name='M-Caliburn-Micro-EventAggregatorExtensions-Subscribe-Caliburn-Micro-IEventAggregator,System-Object-'></a>
### Subscribe(eventAggregator,subscriber) `method`

##### Summary

Subscribes an instance to all events declared through implementations of [IHandle\`1](#T-Caliburn-Micro-IHandle`1 'Caliburn.Micro.IHandle`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') |  |
| subscriber | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to subscribe for event publication. |

##### Remarks

The subscription is invoked on the thread chosen by the publisher.

<a name='M-Caliburn-Micro-EventAggregatorExtensions-SubscribeOnBackgroundThread-Caliburn-Micro-IEventAggregator,System-Object-'></a>
### SubscribeOnBackgroundThread(eventAggregator,subscriber) `method`

##### Summary

Subscribes an instance to all events declared through implementations of [IHandle\`1](#T-Caliburn-Micro-IHandle`1 'Caliburn.Micro.IHandle`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') |  |
| subscriber | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to subscribe for event publication. |

##### Remarks

The subscription is invoked on a new background thread.

<a name='M-Caliburn-Micro-EventAggregatorExtensions-SubscribeOnPublishedThread-Caliburn-Micro-IEventAggregator,System-Object-'></a>
### SubscribeOnPublishedThread(eventAggregator,subscriber) `method`

##### Summary

Subscribes an instance to all events declared through implementations of [IHandle\`1](#T-Caliburn-Micro-IHandle`1 'Caliburn.Micro.IHandle`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') |  |
| subscriber | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to subscribe for event publication. |

##### Remarks

The subscription is invoked on the thread chosen by the publisher.

<a name='M-Caliburn-Micro-EventAggregatorExtensions-SubscribeOnUIThread-Caliburn-Micro-IEventAggregator,System-Object-'></a>
### SubscribeOnUIThread(eventAggregator,subscriber) `method`

##### Summary

Subscribes an instance to all events declared through implementations of [IHandle\`1](#T-Caliburn-Micro-IHandle`1 'Caliburn.Micro.IHandle`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eventAggregator | [Caliburn.Micro.IEventAggregator](#T-Caliburn-Micro-IEventAggregator 'Caliburn.Micro.IEventAggregator') |  |
| subscriber | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to subscribe for event publication. |

##### Remarks

The subscription is invoked on the UI thread.

<a name='T-Caliburn-Micro-Execute'></a>
## Execute `type`

##### Namespace

Caliburn.Micro

##### Summary

Enables easy marshalling of code to the UI thread.

<a name='P-Caliburn-Micro-Execute-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Indicates whether or not the framework is in design-time mode.

<a name='M-Caliburn-Micro-Execute-BeginOnUIThread-System-Action-'></a>
### BeginOnUIThread(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-Execute-OnUIThread-System-Action-'></a>
### OnUIThread(action) `method`

##### Summary

Executes the action on the UI thread.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-Execute-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}-'></a>
### OnUIThreadAsync(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') | The action to execute. |

<a name='T-Caliburn-Micro-ExpressionExtensions'></a>
## ExpressionExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension for [Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression').

<a name='M-Caliburn-Micro-ExpressionExtensions-GetMemberInfo-System-Linq-Expressions-Expression-'></a>
### GetMemberInfo(expression) `method`

##### Summary

Converts an expression into a [MemberInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.MemberInfo 'System.Reflection.MemberInfo').

##### Returns

The member info.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The expression to convert. |

<a name='T-Caliburn-Micro-IActivate'></a>
## IActivate `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an instance which requires activation.

<a name='P-Caliburn-Micro-IActivate-IsActive'></a>
### IsActive `property`

##### Summary

Indicates whether or not this instance is active.

<a name='M-Caliburn-Micro-IActivate-ActivateAsync-System-Threading-CancellationToken-'></a>
### ActivateAsync(cancellationToken) `method`

##### Summary

Activates this instance.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-IChild'></a>
## IChild `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes a node within a parent/child hierarchy.

<a name='P-Caliburn-Micro-IChild-Parent'></a>
### Parent `property`

##### Summary

Gets or Sets the Parent

<a name='T-Caliburn-Micro-IChild`1'></a>
## IChild\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes a node within a parent/child hierarchy.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TParent | The type of parent. |

<a name='P-Caliburn-Micro-IChild`1-Parent'></a>
### Parent `property`

##### Summary

Gets or Sets the Parent

<a name='T-Caliburn-Micro-IClose'></a>
## IClose `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an object that can be closed.

<a name='M-Caliburn-Micro-IClose-TryCloseAsync-System-Nullable{System-Boolean}-'></a>
### TryCloseAsync(dialogResult) `method`

##### Summary

Tries to close this instance.
Also provides an opportunity to pass a dialog result to it's corresponding view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dialogResult | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | The dialog result. |

<a name='T-Caliburn-Micro-ICloseResult`1'></a>
## ICloseResult\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Results from the close strategy.

<a name='P-Caliburn-Micro-ICloseResult`1-Children'></a>
### Children `property`

##### Summary

Indicates which children shbould close if the parent cannot.

<a name='P-Caliburn-Micro-ICloseResult`1-CloseCanOccur'></a>
### CloseCanOccur `property`

##### Summary

Indicates whether a close can occur

<a name='T-Caliburn-Micro-ICloseStrategy`1'></a>
## ICloseStrategy\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Used to gather the results from multiple child elements which may or may not prevent closing.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of child element. |

<a name='M-Caliburn-Micro-ICloseStrategy`1-ExecuteAsync-System-Collections-Generic-IEnumerable{`0},System-Threading-CancellationToken-'></a>
### ExecuteAsync(toClose,cancellationToken) `method`

##### Summary

Executes the strategy.

##### Returns

A task that represents the asynchronous operation and contains the result of the strategy.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toClose | [System.Collections.Generic.IEnumerable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{`0}') | Items that are requesting close. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-IConductActiveItem'></a>
## IConductActiveItem `type`

##### Namespace

Caliburn.Micro

##### Summary

An [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') that also implements [IHaveActiveItem](#T-Caliburn-Micro-IHaveActiveItem 'Caliburn.Micro.IHaveActiveItem').

<a name='T-Caliburn-Micro-IConductor'></a>
## IConductor `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an instance which conducts other objects by managing an ActiveItem and maintaining a strict lifecycle.

##### Remarks

Conducted instances can optin to the lifecycle by impelenting any of the follosing [IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate'), [IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate'), [IGuardClose](#T-Caliburn-Micro-IGuardClose 'Caliburn.Micro.IGuardClose').

<a name='M-Caliburn-Micro-IConductor-ActivateItemAsync-System-Object,System-Threading-CancellationToken-'></a>
### ActivateItemAsync(item,cancellationToken) `method`

##### Summary

Activates the specified item.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The item to activate. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='M-Caliburn-Micro-IConductor-DeactivateItemAsync-System-Object,System-Boolean,System-Threading-CancellationToken-'></a>
### DeactivateItemAsync(item,close,cancellationToken) `method`

##### Summary

Deactivates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The item to close. |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the item after deactivating it. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-IDeactivate'></a>
## IDeactivate `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an instance which requires deactivation.

<a name='M-Caliburn-Micro-IDeactivate-DeactivateAsync-System-Boolean,System-Threading-CancellationToken-'></a>
### DeactivateAsync(close,cancellationToken) `method`

##### Summary

Deactivates this instance.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not this instance is being closed. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-IEventAggregator'></a>
## IEventAggregator `type`

##### Namespace

Caliburn.Micro

##### Summary

Enables loosely-coupled publication of and subscription to events.

<a name='M-Caliburn-Micro-IEventAggregator-HandlerExistsFor-System-Type-'></a>
### HandlerExistsFor(messageType) `method`

##### Summary

Searches the subscribed handlers to check if we have a handler for
the message type supplied.

##### Returns

True if any handler is found, false if not.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| messageType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The message type to check with |

<a name='M-Caliburn-Micro-IEventAggregator-PublishAsync-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task},System-Threading-CancellationToken-'></a>
### PublishAsync(message,marshal,cancellationToken) `method`

##### Summary

Publishes a message.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message instance. |
| marshal | [System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task}') | Allows the publisher to provide a custom thread marshaller for the message publication. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-IEventAggregator-Subscribe-System-Object,System-Func{System-Func{System-Threading-Tasks-Task},System-Threading-Tasks-Task}-'></a>
### Subscribe(subscriber,marshal) `method`

##### Summary

Subscribes an instance to all events declared through implementations of [IHandle\`1](#T-Caliburn-Micro-IHandle`1 'Caliburn.Micro.IHandle`1')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| subscriber | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to subscribe for event publication. |
| marshal | [System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Func{System.Threading.Tasks.Task},System.Threading.Tasks.Task}') | Allows the subscriber to provide a custom thread marshaller for the message subscription. |

<a name='M-Caliburn-Micro-IEventAggregator-Unsubscribe-System-Object-'></a>
### Unsubscribe(subscriber) `method`

##### Summary

Unsubscribes the instance from all events.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| subscriber | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to unsubscribe. |

<a name='T-Caliburn-Micro-IGuardClose'></a>
## IGuardClose `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an instance which may prevent closing.

<a name='M-Caliburn-Micro-IGuardClose-CanCloseAsync-System-Threading-CancellationToken-'></a>
### CanCloseAsync(cancellationToken) `method`

##### Summary

Called to check whether or not this instance can close.

##### Returns

A task that represents the asynchronous operation and contains the result of the close.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-IHandle`1'></a>
## IHandle\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes a class which can handle a particular type of message.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TMessage | The type of message to handle. |

<a name='M-Caliburn-Micro-IHandle`1-HandleAsync-`0,System-Threading-CancellationToken-'></a>
### HandleAsync(message,cancellationToken) `method`

##### Summary

Handles the message.

##### Returns

A task that represents the asynchronous coroutine.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [\`0](#T-`0 '`0') | The message. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='T-Caliburn-Micro-IHaveActiveItem'></a>
## IHaveActiveItem `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an instance which maintains an active item.

<a name='P-Caliburn-Micro-IHaveActiveItem-ActiveItem'></a>
### ActiveItem `property`

##### Summary

The currently active item.

<a name='T-Caliburn-Micro-IHaveDisplayName'></a>
## IHaveDisplayName `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an instance which has a display name.

<a name='P-Caliburn-Micro-IHaveDisplayName-DisplayName'></a>
### DisplayName `property`

##### Summary

Gets or Sets the Display Name

<a name='T-Caliburn-Micro-ILog'></a>
## ILog `type`

##### Namespace

Caliburn.Micro

##### Summary

A logger.

<a name='M-Caliburn-Micro-ILog-Error-System-Exception-'></a>
### Error(exception) `method`

##### Summary

Logs the exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception. |

<a name='M-Caliburn-Micro-ILog-Info-System-String,System-Object[]-'></a>
### Info(format,args) `method`

##### Summary

Logs the message as info.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| format | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A formatted message. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Parameters to be injected into the formatted message. |

<a name='M-Caliburn-Micro-ILog-Warn-System-String,System-Object[]-'></a>
### Warn(format,args) `method`

##### Summary

Logs the message as a warning.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| format | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A formatted message. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Parameters to be injected into the formatted message. |

<a name='T-Caliburn-Micro-INotifyPropertyChangedEx'></a>
## INotifyPropertyChangedEx `type`

##### Namespace

Caliburn.Micro

##### Summary

Extends [INotifyPropertyChanged](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ComponentModel.INotifyPropertyChanged 'System.ComponentModel.INotifyPropertyChanged') such that the change event can be raised by external parties.

<a name='P-Caliburn-Micro-INotifyPropertyChangedEx-IsNotifying'></a>
### IsNotifying `property`

##### Summary

Enables/Disables property change notification.

<a name='M-Caliburn-Micro-INotifyPropertyChangedEx-NotifyOfPropertyChange-System-String-'></a>
### NotifyOfPropertyChange(propertyName) `method`

##### Summary

Notifies subscribers of the property change.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| propertyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the property. |

<a name='M-Caliburn-Micro-INotifyPropertyChangedEx-Refresh'></a>
### Refresh() `method`

##### Summary

Raises a change notification indicating that all bindings should be refreshed.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-IObservableCollection`1'></a>
## IObservableCollection\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Represents a collection that is observable.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of elements contained in the collection. |

<a name='M-Caliburn-Micro-IObservableCollection`1-AddRange-System-Collections-Generic-IEnumerable{`0}-'></a>
### AddRange(items) `method`

##### Summary

Adds the range.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| items | [System.Collections.Generic.IEnumerable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{`0}') | The items. |

<a name='M-Caliburn-Micro-IObservableCollection`1-RemoveRange-System-Collections-Generic-IEnumerable{`0}-'></a>
### RemoveRange(items) `method`

##### Summary

Removes the range.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| items | [System.Collections.Generic.IEnumerable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{`0}') | The items. |

<a name='T-Caliburn-Micro-IParent'></a>
## IParent `type`

##### Namespace

Caliburn.Micro

##### Summary

Interface used to define an object associated to a collection of children.

<a name='M-Caliburn-Micro-IParent-GetChildren'></a>
### GetChildren() `method`

##### Summary

Gets the children.

##### Returns

The collection of children.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-IParent`1'></a>
## IParent\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Interface used to define a specialized parent.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of children. |

<a name='M-Caliburn-Micro-IParent`1-GetChildren'></a>
### GetChildren() `method`

##### Summary

Gets the children.

##### Returns

The collection of children.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-IPlatformProvider'></a>
## IPlatformProvider `type`

##### Namespace

Caliburn.Micro

##### Summary

Interface for platform specific operations that need enlightenment.

<a name='P-Caliburn-Micro-IPlatformProvider-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Indicates whether or not the framework is in design-time mode.

<a name='P-Caliburn-Micro-IPlatformProvider-PropertyChangeNotificationsOnUIThread'></a>
### PropertyChangeNotificationsOnUIThread `property`

##### Summary

Whether or not classes should execute property change notications on the UI thread.

<a name='M-Caliburn-Micro-IPlatformProvider-BeginOnUIThread-System-Action-'></a>
### BeginOnUIThread(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-IPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnFirstLoad(view,handler) `method`

##### Summary

Executes the handler the fist time the view is loaded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-IPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnLayoutUpdated(view,handler) `method`

##### Summary

Executes the handler the next time the view's LayoutUpdated event fires.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-IPlatformProvider-GetFirstNonGeneratedView-System-Object-'></a>
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

<a name='M-Caliburn-Micro-IPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}-'></a>
### GetViewCloseAction(viewModel,views,dialogResult) `method`

##### Summary

Get the close action for the specified view model.

##### Returns

An [Func\`2](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func`2 'System.Func`2') to close the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model to close. |
| views | [System.Collections.Generic.ICollection{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.ICollection 'System.Collections.Generic.ICollection{System.Object}') | The associated views. |
| dialogResult | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | The dialog result. |

<a name='M-Caliburn-Micro-IPlatformProvider-OnUIThread-System-Action-'></a>
### OnUIThread(action) `method`

##### Summary

Executes the action on the UI thread.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-IPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}-'></a>
### OnUIThreadAsync(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') | The action to execute. |

<a name='T-Caliburn-Micro-IResult'></a>
## IResult `type`

##### Namespace

Caliburn.Micro

##### Summary

Allows custom code to execute after the return of a action.

<a name='M-Caliburn-Micro-IResult-Execute-Caliburn-Micro-CoroutineExecutionContext-'></a>
### Execute(context) `method`

##### Summary

Executes the result using the specified context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |

<a name='T-Caliburn-Micro-IResult`1'></a>
## IResult\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

Allows custom code to execute after the return of a action.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TResult | The type of the result. |

<a name='P-Caliburn-Micro-IResult`1-Result'></a>
### Result `property`

##### Summary

Gets the result of the asynchronous operation.

<a name='T-Caliburn-Micro-IScreen'></a>
## IScreen `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes an instance which implements [IHaveDisplayName](#T-Caliburn-Micro-IHaveDisplayName 'Caliburn.Micro.IHaveDisplayName'), [IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate'), 
[IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate'), [IGuardClose](#T-Caliburn-Micro-IGuardClose 'Caliburn.Micro.IGuardClose') and [INotifyPropertyChangedEx](#T-Caliburn-Micro-INotifyPropertyChangedEx 'Caliburn.Micro.INotifyPropertyChangedEx')

<a name='T-Caliburn-Micro-IViewAware'></a>
## IViewAware `type`

##### Namespace

Caliburn.Micro

##### Summary

Denotes a class which is aware of its view(s).

<a name='M-Caliburn-Micro-IViewAware-AttachView-System-Object,System-Object-'></a>
### AttachView(view,context) `method`

##### Summary

Attaches a view to this instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The context in which the view appears. |

<a name='M-Caliburn-Micro-IViewAware-GetView-System-Object-'></a>
### GetView(context) `method`

##### Summary

Gets a view previously attached to this instance.

##### Returns

The view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The context denoting which view to retrieve. |

<a name='T-Caliburn-Micro-IoC'></a>
## IoC `type`

##### Namespace

Caliburn.Micro

##### Summary

Used by the framework to pull instances from an IoC container and to inject dependencies into certain existing classes.

<a name='F-Caliburn-Micro-IoC-BuildUp'></a>
### BuildUp `constants`

##### Summary

Passes an existing instance to the IoC container to enable dependencies to be injected.

<a name='F-Caliburn-Micro-IoC-GetAllInstances'></a>
### GetAllInstances `constants`

##### Summary

Gets all instances of a particular type.

<a name='F-Caliburn-Micro-IoC-GetInstance'></a>
### GetInstance `constants`

##### Summary

Gets an instance by type and key.

<a name='M-Caliburn-Micro-IoC-GetAll``1'></a>
### GetAll\`\`1() `method`

##### Summary

Gets all instances of a particular type.

##### Returns

The resolved instances.

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type to resolve. |

<a name='M-Caliburn-Micro-IoC-Get``1-System-String-'></a>
### Get\`\`1(key) `method`

##### Summary

Gets an instance from the container.

##### Returns

The resolved instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key to look up. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type to resolve. |

<a name='T-Caliburn-Micro-LogManager'></a>
## LogManager `type`

##### Namespace

Caliburn.Micro

##### Summary

Used to manage logging.

<a name='F-Caliburn-Micro-LogManager-GetLog'></a>
### GetLog `constants`

##### Summary

Creates an [ILog](#T-Caliburn-Micro-ILog 'Caliburn.Micro.ILog') for the provided type.

<a name='T-Caliburn-Micro-Conductor`1-Collection-OneActive'></a>
## OneActive `type`

##### Namespace

Caliburn.Micro.Conductor`1.Collection

##### Summary

An implementation of [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') that holds on many items but only activates one at a time.

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [OneActive](#T-Caliburn-Micro-Conductor`1-Collection-OneActive 'Caliburn.Micro.Conductor`1.Collection.OneActive') class.

##### Parameters

This constructor has no parameters.

<a name='P-Caliburn-Micro-Conductor`1-Collection-OneActive-Items'></a>
### Items `property`

##### Summary

Gets the items that are currently being conducted.

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-ActivateItemAsync-`0,System-Threading-CancellationToken-'></a>
### ActivateItemAsync(item,cancellationToken) `method`

##### Summary

Activates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item to activate. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-CanCloseAsync-System-Threading-CancellationToken-'></a>
### CanCloseAsync(cancellationToken) `method`

##### Summary

Called to check whether or not this instance can close.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-DeactivateItemAsync-`0,System-Boolean,System-Threading-CancellationToken-'></a>
### DeactivateItemAsync(item,close,cancellationToken) `method`

##### Summary

Deactivates the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [\`0](#T-`0 '`0') | The item to close. |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the item after deactivating it. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-DetermineNextItemToActivate-System-Collections-Generic-IList{`0},System-Int32-'></a>
### DetermineNextItemToActivate(list,lastIndex) `method`

##### Summary

Determines the next item to activate based on the last active index.

##### Returns

The next item to activate.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| list | [System.Collections.Generic.IList{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{`0}') | The list of possible active items. |
| lastIndex | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index of the last active item. |

##### Remarks

Called after an active item is closed.

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-EnsureItem-`0-'></a>
### EnsureItem(newItem) `method`

##### Summary

Ensures that an item is ready to be activated.

##### Returns

The item to be activated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newItem | [\`0](#T-`0 '`0') | The item that is about to be activated. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-GetChildren'></a>
### GetChildren() `method`

##### Summary

Gets the children.

##### Returns

The collection of children.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-OnActivatedAsync-System-Threading-CancellationToken-'></a>
### OnActivatedAsync(cancellationToken) `method`

##### Summary

Called when view has been activated.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Conductor`1-Collection-OneActive-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken-'></a>
### OnDeactivateAsync(close,cancellationToken) `method`

##### Summary

Called when deactivating.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether this instance will be closed. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-OverrideCancelResultDecorator'></a>
## OverrideCancelResultDecorator `type`

##### Namespace

Caliburn.Micro

##### Summary

A result decorator that overrides [WasCancelled](#F-Caliburn-Micro-ResultCompletionEventArgs-WasCancelled 'Caliburn.Micro.ResultCompletionEventArgs.WasCancelled') of the decorated [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') instance.

<a name='M-Caliburn-Micro-OverrideCancelResultDecorator-#ctor-Caliburn-Micro-IResult-'></a>
### #ctor(result) `constructor`

##### Summary

Initializes a new instance of the [OverrideCancelResultDecorator](#T-Caliburn-Micro-OverrideCancelResultDecorator 'Caliburn.Micro.OverrideCancelResultDecorator') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |

<a name='M-Caliburn-Micro-OverrideCancelResultDecorator-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs-'></a>
### OnInnerResultCompleted(context,innerResult,args) `method`

##### Summary

Called when the execution of the decorated result has completed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |
| innerResult | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The decorated result. |
| args | [Caliburn.Micro.ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') | The [ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') instance containing the event data. |

<a name='T-Caliburn-Micro-PlatformProvider'></a>
## PlatformProvider `type`

##### Namespace

Caliburn.Micro

##### Summary

Access the current [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider').

<a name='P-Caliburn-Micro-PlatformProvider-Current'></a>
### Current `property`

##### Summary

Gets or sets the current [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider').

<a name='T-Caliburn-Micro-PropertyChangedBase'></a>
## PropertyChangedBase `type`

##### Namespace

Caliburn.Micro

##### Summary

A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.

<a name='M-Caliburn-Micro-PropertyChangedBase-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates an instance of [PropertyChangedBase](#T-Caliburn-Micro-PropertyChangedBase 'Caliburn.Micro.PropertyChangedBase').

##### Parameters

This constructor has no parameters.

<a name='P-Caliburn-Micro-PropertyChangedBase-IsNotifying'></a>
### IsNotifying `property`

##### Summary

Enables/Disables property change notification.
Virtualized in order to help with document oriented view models.

<a name='M-Caliburn-Micro-PropertyChangedBase-NotifyOfPropertyChange-System-String-'></a>
### NotifyOfPropertyChange(propertyName) `method`

##### Summary

Notifies subscribers of the property change.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| propertyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the property. |

<a name='M-Caliburn-Micro-PropertyChangedBase-NotifyOfPropertyChange``1-System-Linq-Expressions-Expression{System-Func{``0}}-'></a>
### NotifyOfPropertyChange\`\`1(property) `method`

##### Summary

Notifies subscribers of the property change.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| property | [System.Linq.Expressions.Expression{System.Func{\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{``0}}') | The property expression. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TProperty | The type of the property. |

<a name='M-Caliburn-Micro-PropertyChangedBase-OnPropertyChanged-System-ComponentModel-PropertyChangedEventArgs-'></a>
### OnPropertyChanged(e) `method`

##### Summary

Raises the [](#E-Caliburn-Micro-PropertyChangedBase-PropertyChanged 'Caliburn.Micro.PropertyChangedBase.PropertyChanged') event directly.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| e | [System.ComponentModel.PropertyChangedEventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ComponentModel.PropertyChangedEventArgs 'System.ComponentModel.PropertyChangedEventArgs') | The [PropertyChangedEventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ComponentModel.PropertyChangedEventArgs 'System.ComponentModel.PropertyChangedEventArgs') instance containing the event data. |

<a name='M-Caliburn-Micro-PropertyChangedBase-OnUIThread-System-Action-'></a>
### OnUIThread(action) `method`

##### Summary

Executes the given action on the UI thread

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') |  |

##### Remarks

An extension point for subclasses to customise how property change notifications are handled.

<a name='M-Caliburn-Micro-PropertyChangedBase-Refresh'></a>
### Refresh() `method`

##### Summary

Raises a change notification indicating that all bindings should be refreshed.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-PropertyChangedBase-Set``1-``0@,``0,System-String-'></a>
### Set\`\`1(oldValue,newValue,propertyName) `method`

##### Summary

Sets a backing field value and if it's changed raise a notification.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| oldValue | [\`\`0@](#T-``0@ '``0@') | A reference to the field to update. |
| newValue | [\`\`0](#T-``0 '``0') | The new value. |
| propertyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the property for change notifications. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the value being set. |

<a name='T-Caliburn-Micro-RescueResultDecorator`1'></a>
## RescueResultDecorator\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

A result decorator which rescues errors from the decorated result by executing a rescue coroutine.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TException | The type of the exception we want to perform the rescue on |

<a name='M-Caliburn-Micro-RescueResultDecorator`1-#ctor-Caliburn-Micro-IResult,System-Func{`0,Caliburn-Micro-IResult},System-Boolean-'></a>
### #ctor(result,coroutine,cancelResult) `constructor`

##### Summary

Initializes a new instance of the [RescueResultDecorator\`1](#T-Caliburn-Micro-RescueResultDecorator`1 'Caliburn.Micro.RescueResultDecorator`1') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |
| coroutine | [System.Func{\`0,Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{`0,Caliburn.Micro.IResult}') | The rescue coroutine. |
| cancelResult | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Set to true to cancel the result after executing rescue. |

<a name='M-Caliburn-Micro-RescueResultDecorator`1-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs-'></a>
### OnInnerResultCompleted(context,innerResult,args) `method`

##### Summary

Called when the execution of the decorated result has completed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |
| innerResult | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The decorated result. |
| args | [Caliburn.Micro.ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') | The [ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') instance containing the event data. |

<a name='T-Caliburn-Micro-ResultCompletionEventArgs'></a>
## ResultCompletionEventArgs `type`

##### Namespace

Caliburn.Micro

##### Summary

The event args for the Completed event of an [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult').

<a name='F-Caliburn-Micro-ResultCompletionEventArgs-Error'></a>
### Error `constants`

##### Summary

Gets or sets the error if one occurred.

<a name='F-Caliburn-Micro-ResultCompletionEventArgs-WasCancelled'></a>
### WasCancelled `constants`

##### Summary

Gets or sets a value indicating whether the result was cancelled.

<a name='T-Caliburn-Micro-ResultDecoratorBase'></a>
## ResultDecoratorBase `type`

##### Namespace

Caliburn.Micro

##### Summary

Base class for all [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') decorators.

<a name='M-Caliburn-Micro-ResultDecoratorBase-#ctor-Caliburn-Micro-IResult-'></a>
### #ctor(result) `constructor`

##### Summary

Initializes a new instance of the [ResultDecoratorBase](#T-Caliburn-Micro-ResultDecoratorBase 'Caliburn.Micro.ResultDecoratorBase') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |

<a name='M-Caliburn-Micro-ResultDecoratorBase-Execute-Caliburn-Micro-CoroutineExecutionContext-'></a>
### Execute(context) `method`

##### Summary

Executes the result using the specified context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |

<a name='M-Caliburn-Micro-ResultDecoratorBase-OnCompleted-Caliburn-Micro-ResultCompletionEventArgs-'></a>
### OnCompleted(args) `method`

##### Summary

Raises the [](#E-Caliburn-Micro-ResultDecoratorBase-Completed 'Caliburn.Micro.ResultDecoratorBase.Completed') event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [Caliburn.Micro.ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') | The [ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') instance containing the event data. |

<a name='M-Caliburn-Micro-ResultDecoratorBase-OnInnerResultCompleted-Caliburn-Micro-CoroutineExecutionContext,Caliburn-Micro-IResult,Caliburn-Micro-ResultCompletionEventArgs-'></a>
### OnInnerResultCompleted(context,innerResult,args) `method`

##### Summary

Called when the execution of the decorated result has completed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |
| innerResult | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The decorated result. |
| args | [Caliburn.Micro.ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') | The [ResultCompletionEventArgs](#T-Caliburn-Micro-ResultCompletionEventArgs 'Caliburn.Micro.ResultCompletionEventArgs') instance containing the event data. |

<a name='T-Caliburn-Micro-ResultExtensions'></a>
## ResultExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods for [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') instances.

<a name='M-Caliburn-Micro-ResultExtensions-OverrideCancel-Caliburn-Micro-IResult-'></a>
### OverrideCancel(result) `method`

##### Summary

Overrides [WasCancelled](#F-Caliburn-Micro-ResultCompletionEventArgs-WasCancelled 'Caliburn.Micro.ResultCompletionEventArgs.WasCancelled') of the decorated `result` instance.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |

<a name='M-Caliburn-Micro-ResultExtensions-Rescue-Caliburn-Micro-IResult,System-Func{System-Exception,Caliburn-Micro-IResult},System-Boolean-'></a>
### Rescue(result,rescue,cancelResult) `method`

##### Summary

Rescues any exception from the decorated `result` by executing a `rescue` coroutine.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |
| rescue | [System.Func{System.Exception,Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Exception,Caliburn.Micro.IResult}') | The rescue coroutine. |
| cancelResult | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Set to true to cancel the result after executing rescue. |

<a name='M-Caliburn-Micro-ResultExtensions-Rescue``1-Caliburn-Micro-IResult,System-Func{``0,Caliburn-Micro-IResult},System-Boolean-'></a>
### Rescue\`\`1(result,rescue,cancelResult) `method`

##### Summary

Rescues `TException` from the decorated `result` by executing a `rescue` coroutine.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |
| rescue | [System.Func{\`\`0,Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,Caliburn.Micro.IResult}') | The rescue coroutine. |
| cancelResult | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Set to true to cancel the result after executing rescue. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TException | The type of the exception we want to perform the rescue on. |

<a name='M-Caliburn-Micro-ResultExtensions-WhenCancelled-Caliburn-Micro-IResult,System-Func{Caliburn-Micro-IResult}-'></a>
### WhenCancelled(result,coroutine) `method`

##### Summary

Adds behavior to the result which is executed when the `result` was cancelled.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The result to decorate. |
| coroutine | [System.Func{Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{Caliburn.Micro.IResult}') | The coroutine to execute when `result` was canceled. |

<a name='T-Caliburn-Micro-Screen'></a>
## Screen `type`

##### Namespace

Caliburn.Micro

##### Summary

A base implementation of [IScreen](#T-Caliburn-Micro-IScreen 'Caliburn.Micro.IScreen').

<a name='M-Caliburn-Micro-Screen-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates an instance of the screen.

##### Parameters

This constructor has no parameters.

<a name='P-Caliburn-Micro-Screen-DisplayName'></a>
### DisplayName `property`

##### Summary

Gets or Sets the Display Name

<a name='P-Caliburn-Micro-Screen-IsActive'></a>
### IsActive `property`

##### Summary

Indicates whether or not this instance is currently active.
Virtualized in order to help with document oriented view models.

<a name='P-Caliburn-Micro-Screen-IsInitialized'></a>
### IsInitialized `property`

##### Summary

Indicates whether or not this instance is currently initialized.
Virtualized in order to help with document oriented view models.

<a name='P-Caliburn-Micro-Screen-Parent'></a>
### Parent `property`

##### Summary

Gets or Sets the Parent [IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor')

<a name='M-Caliburn-Micro-Screen-CanCloseAsync-System-Threading-CancellationToken-'></a>
### CanCloseAsync(cancellationToken) `method`

##### Summary

Called to check whether or not this instance can close.

##### Returns

A task that represents the asynchronous operation and holds the value of the close check..

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Screen-OnActivateAsync-System-Threading-CancellationToken-'></a>
### OnActivateAsync() `method`

##### Summary

Called when activating.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Screen-OnActivatedAsync-System-Threading-CancellationToken-'></a>
### OnActivatedAsync() `method`

##### Summary

Called when view has been activated.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Screen-OnDeactivateAsync-System-Boolean,System-Threading-CancellationToken-'></a>
### OnDeactivateAsync(close,cancellationToken) `method`

##### Summary

Called when deactivating.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether this instance will be closed. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-Screen-OnInitializeAsync-System-Threading-CancellationToken-'></a>
### OnInitializeAsync() `method`

##### Summary

Called when initializing.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Screen-OnInitializedAsync-System-Threading-CancellationToken-'></a>
### OnInitializedAsync() `method`

##### Summary

Called when view has been initialized

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Screen-TryCloseAsync-System-Nullable{System-Boolean}-'></a>
### TryCloseAsync(dialogResult) `method`

##### Summary

Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
Also provides an opportunity to pass a dialog result to it's corresponding view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dialogResult | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | The dialog result. |

<a name='T-Caliburn-Micro-ScreenExtensions'></a>
## ScreenExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Hosts extension methods for [IScreen](#T-Caliburn-Micro-IScreen 'Caliburn.Micro.IScreen') classes.

<a name='M-Caliburn-Micro-ScreenExtensions-ActivateWith-Caliburn-Micro-IActivate,Caliburn-Micro-IActivate-'></a>
### ActivateWith(child,parent) `method`

##### Summary

Activates a child whenever the specified parent is activated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| child | [Caliburn.Micro.IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate') | The child to activate. |
| parent | [Caliburn.Micro.IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate') | The parent whose activation triggers the child's activation. |

<a name='M-Caliburn-Micro-ScreenExtensions-CloseItemAsync-Caliburn-Micro-IConductor,System-Object-'></a>
### CloseItemAsync(conductor,item) `method`

##### Summary

Closes the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| conductor | [Caliburn.Micro.IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') | The conductor. |
| item | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The item to close. |

<a name='M-Caliburn-Micro-ScreenExtensions-CloseItemAsync-Caliburn-Micro-IConductor,System-Object,System-Threading-CancellationToken-'></a>
### CloseItemAsync(conductor,item,cancellationToken) `method`

##### Summary

Closes the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| conductor | [Caliburn.Micro.IConductor](#T-Caliburn-Micro-IConductor 'Caliburn.Micro.IConductor') | The conductor. |
| item | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The item to close. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-ScreenExtensions-CloseItemAsync``1-Caliburn-Micro-ConductorBase{``0},``0-'></a>
### CloseItemAsync\`\`1(conductor,item) `method`

##### Summary

Closes the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| conductor | [Caliburn.Micro.ConductorBase{\`\`0}](#T-Caliburn-Micro-ConductorBase{``0} 'Caliburn.Micro.ConductorBase{``0}') | The conductor. |
| item | [\`\`0](#T-``0 '``0') | The item to close. |

<a name='M-Caliburn-Micro-ScreenExtensions-CloseItemAsync``1-Caliburn-Micro-ConductorBase{``0},``0,System-Threading-CancellationToken-'></a>
### CloseItemAsync\`\`1(conductor,item,cancellationToken) `method`

##### Summary

Closes the specified item.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| conductor | [Caliburn.Micro.ConductorBase{\`\`0}](#T-Caliburn-Micro-ConductorBase{``0} 'Caliburn.Micro.ConductorBase{``0}') | The conductor. |
| item | [\`\`0](#T-``0 '``0') | The item to close. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-ScreenExtensions-ConductWith``2-``0,``1-'></a>
### ConductWith\`\`2(child,parent) `method`

##### Summary

Activates and Deactivates a child whenever the specified parent is Activated or Deactivated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| child | [\`\`0](#T-``0 '``0') | The child to activate/deactivate. |
| parent | [\`\`1](#T-``1 '``1') | The parent whose activation/deactivation triggers the child's activation/deactivation. |

<a name='M-Caliburn-Micro-ScreenExtensions-DeactivateWith-Caliburn-Micro-IDeactivate,Caliburn-Micro-IDeactivate-'></a>
### DeactivateWith(child,parent) `method`

##### Summary

Deactivates a child whenever the specified parent is deactivated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| child | [Caliburn.Micro.IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate') | The child to deactivate. |
| parent | [Caliburn.Micro.IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate') | The parent whose deactivation triggers the child's deactivation. |

<a name='M-Caliburn-Micro-ScreenExtensions-TryActivateAsync-System-Object-'></a>
### TryActivateAsync(potentialActivatable) `method`

##### Summary

Activates the item if it implements [IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate'), otherwise does nothing.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| potentialActivatable | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The potential activatable. |

<a name='M-Caliburn-Micro-ScreenExtensions-TryActivateAsync-System-Object,System-Threading-CancellationToken-'></a>
### TryActivateAsync(potentialActivatable,cancellationToken) `method`

##### Summary

Activates the item if it implements [IActivate](#T-Caliburn-Micro-IActivate 'Caliburn.Micro.IActivate'), otherwise does nothing.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| potentialActivatable | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The potential activatable. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='M-Caliburn-Micro-ScreenExtensions-TryDeactivateAsync-System-Object,System-Boolean-'></a>
### TryDeactivateAsync(potentialDeactivatable,close) `method`

##### Summary

Deactivates the item if it implements [IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate'), otherwise does nothing.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| potentialDeactivatable | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The potential deactivatable. |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the item after deactivating it. |

<a name='M-Caliburn-Micro-ScreenExtensions-TryDeactivateAsync-System-Object,System-Boolean,System-Threading-CancellationToken-'></a>
### TryDeactivateAsync(potentialDeactivatable,close,cancellationToken) `method`

##### Summary

Deactivates the item if it implements [IDeactivate](#T-Caliburn-Micro-IDeactivate 'Caliburn.Micro.IDeactivate'), otherwise does nothing.

##### Returns

A task that represents the asynchronous operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| potentialDeactivatable | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The potential deactivatable. |
| close | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indicates whether or not to close the item after deactivating it. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token to cancel operation. |

<a name='T-Caliburn-Micro-SequentialResult'></a>
## SequentialResult `type`

##### Namespace

Caliburn.Micro

##### Summary

An implementation of [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') that enables sequential execution of multiple results.

<a name='M-Caliburn-Micro-SequentialResult-#ctor-System-Collections-Generic-IEnumerator{Caliburn-Micro-IResult}-'></a>
### #ctor(enumerator) `constructor`

##### Summary

Initializes a new instance of the [SequentialResult](#T-Caliburn-Micro-SequentialResult 'Caliburn.Micro.SequentialResult') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| enumerator | [System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerator 'System.Collections.Generic.IEnumerator{Caliburn.Micro.IResult}') | The enumerator. |

<a name='M-Caliburn-Micro-SequentialResult-Execute-Caliburn-Micro-CoroutineExecutionContext-'></a>
### Execute(context) `method`

##### Summary

Executes the result using the specified context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |

<a name='T-Caliburn-Micro-SimpleContainer'></a>
## SimpleContainer `type`

##### Namespace

Caliburn.Micro

##### Summary

A simple IoC container.

<a name='M-Caliburn-Micro-SimpleContainer-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [SimpleContainer](#T-Caliburn-Micro-SimpleContainer 'Caliburn.Micro.SimpleContainer') class.

##### Parameters

This constructor has no parameters.

<a name='P-Caliburn-Micro-SimpleContainer-EnablePropertyInjection'></a>
### EnablePropertyInjection `property`

##### Summary

Whether to enable recursive property injection for all resolutions.

<a name='M-Caliburn-Micro-SimpleContainer-ActivateInstance-System-Type,System-Object[]-'></a>
### ActivateInstance(type,args) `method`

##### Summary

Creates an instance of the type with the specified constructor arguments.

##### Returns

The created instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The constructor args. |

<a name='M-Caliburn-Micro-SimpleContainer-BuildInstance-System-Type-'></a>
### BuildInstance(type) `method`

##### Summary

Actually does the work of creating the instance and satisfying it's constructor dependencies.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type. |

<a name='M-Caliburn-Micro-SimpleContainer-BuildUp-System-Object-'></a>
### BuildUp(instance) `method`

##### Summary

Pushes dependencies into an existing instance based on interface properties with setters.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| instance | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance. |

<a name='M-Caliburn-Micro-SimpleContainer-CreateChildContainer'></a>
### CreateChildContainer() `method`

##### Summary

Creates a child container.

##### Returns

A new container.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-SimpleContainer-GetAllInstances-System-Type,System-String-'></a>
### GetAllInstances(service,key) `method`

##### Summary

Requests all instances of a given type and the given key (default null).

##### Returns

All the instances or an empty enumerable if none are found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key shared by those instances |

<a name='M-Caliburn-Micro-SimpleContainer-GetInstance-System-Type,System-String-'></a>
### GetInstance(service,key) `method`

##### Summary

Requests an instance.

##### Returns

The instance, or null if a handler is not found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

<a name='M-Caliburn-Micro-SimpleContainer-HasHandler-System-Type,System-String-'></a>
### HasHandler(service,key) `method`

##### Summary

Determines if a handler for the service/key has previously been registered.

##### Returns

True if a handler is registere; false otherwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

<a name='M-Caliburn-Micro-SimpleContainer-RegisterHandler-System-Type,System-String,System-Func{Caliburn-Micro-SimpleContainer,System-Object}-'></a>
### RegisterHandler(service,key,handler) `method`

##### Summary

Registers a custom handler for serving requests from the container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |
| handler | [System.Func{Caliburn.Micro.SimpleContainer,System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{Caliburn.Micro.SimpleContainer,System.Object}') | The handler. |

<a name='M-Caliburn-Micro-SimpleContainer-RegisterInstance-System-Type,System-String,System-Object-'></a>
### RegisterInstance(service,key,implementation) `method`

##### Summary

Registers the instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |
| implementation | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The implementation. |

<a name='M-Caliburn-Micro-SimpleContainer-RegisterPerRequest-System-Type,System-String,System-Type-'></a>
### RegisterPerRequest(service,key,implementation) `method`

##### Summary

Registers the class so that a new instance is created on every request.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |
| implementation | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The implementation. |

<a name='M-Caliburn-Micro-SimpleContainer-RegisterSingleton-System-Type,System-String,System-Type-'></a>
### RegisterSingleton(service,key,implementation) `method`

##### Summary

Registers the class so that it is created once, on first request, and the same instance is returned to all requestors thereafter.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |
| implementation | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The implementation. |

<a name='M-Caliburn-Micro-SimpleContainer-UnregisterHandler-System-Type,System-String-'></a>
### UnregisterHandler(service,key) `method`

##### Summary

Unregisters any handlers for the service/key that have previously been registered.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

<a name='T-Caliburn-Micro-SimpleResult'></a>
## SimpleResult `type`

##### Namespace

Caliburn.Micro

##### Summary

A simple result.

<a name='M-Caliburn-Micro-SimpleResult-Cancelled'></a>
### Cancelled() `method`

##### Summary

A result that is always canceled.

##### Returns

The result.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-SimpleResult-Execute-Caliburn-Micro-CoroutineExecutionContext-'></a>
### Execute(context) `method`

##### Summary

Executes the result using the specified context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |

<a name='M-Caliburn-Micro-SimpleResult-Failed-System-Exception-'></a>
### Failed() `method`

##### Summary

A result that is always failed.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-SimpleResult-Succeeded'></a>
### Succeeded() `method`

##### Summary

A result that is always succeeded.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-TaskExtensions'></a>
## TaskExtensions `type`

##### Namespace

Caliburn.Micro

##### Summary

Extension methods to bring [Task](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task') and [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') together.

<a name='M-Caliburn-Micro-TaskExtensions-AsResult-System-Threading-Tasks-Task-'></a>
### AsResult(task) `method`

##### Summary

Encapsulates a task inside a couroutine.

##### Returns

The coroutine that encapsulates the task.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| task | [System.Threading.Tasks.Task](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task') | The task. |

<a name='M-Caliburn-Micro-TaskExtensions-AsResult``1-System-Threading-Tasks-Task{``0}-'></a>
### AsResult\`\`1(task) `method`

##### Summary

Encapsulates a task inside a couroutine.

##### Returns

The coroutine that encapsulates the task.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| task | [System.Threading.Tasks.Task{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task{``0}') | The task. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TResult | The type of the result. |

<a name='M-Caliburn-Micro-TaskExtensions-ExecuteAsync-Caliburn-Micro-IResult,Caliburn-Micro-CoroutineExecutionContext-'></a>
### ExecuteAsync(result,context) `method`

##### Summary

Executes an [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') asynchronous.

##### Returns

A task that represents the asynchronous coroutine.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult') | The coroutine to execute. |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context to execute the coroutine within. |

<a name='M-Caliburn-Micro-TaskExtensions-ExecuteAsync``1-Caliburn-Micro-IResult{``0},Caliburn-Micro-CoroutineExecutionContext-'></a>
### ExecuteAsync\`\`1(result,context) `method`

##### Summary

Executes an [IResult\`1](#T-Caliburn-Micro-IResult`1 'Caliburn.Micro.IResult`1') asynchronous.

##### Returns

A task that represents the asynchronous coroutine.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [Caliburn.Micro.IResult{\`\`0}](#T-Caliburn-Micro-IResult{``0} 'Caliburn.Micro.IResult{``0}') | The coroutine to execute. |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context to execute the coroutine within. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TResult | The type of the result. |

<a name='T-Caliburn-Micro-TaskResult'></a>
## TaskResult `type`

##### Namespace

Caliburn.Micro

##### Summary

A couroutine that encapsulates an [Task](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task').

<a name='M-Caliburn-Micro-TaskResult-#ctor-System-Threading-Tasks-Task-'></a>
### #ctor(task) `constructor`

##### Summary

Initializes a new instance of the [TaskResult](#T-Caliburn-Micro-TaskResult 'Caliburn.Micro.TaskResult') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| task | [System.Threading.Tasks.Task](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task') | The task. |

<a name='M-Caliburn-Micro-TaskResult-Execute-Caliburn-Micro-CoroutineExecutionContext-'></a>
### Execute(context) `method`

##### Summary

Executes the result using the specified context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.CoroutineExecutionContext](#T-Caliburn-Micro-CoroutineExecutionContext 'Caliburn.Micro.CoroutineExecutionContext') | The context. |

<a name='M-Caliburn-Micro-TaskResult-OnCompleted-System-Threading-Tasks-Task-'></a>
### OnCompleted(task) `method`

##### Summary

Called when the asynchronous task has completed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| task | [System.Threading.Tasks.Task](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task') | The completed task. |

<a name='T-Caliburn-Micro-TaskResult`1'></a>
## TaskResult\`1 `type`

##### Namespace

Caliburn.Micro

##### Summary

A couroutine that encapsulates an [Task\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task`1 'System.Threading.Tasks.Task`1').

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TResult | The type of the result. |

<a name='M-Caliburn-Micro-TaskResult`1-#ctor-System-Threading-Tasks-Task{`0}-'></a>
### #ctor(task) `constructor`

##### Summary

Initializes a new instance of the [TaskResult\`1](#T-Caliburn-Micro-TaskResult`1 'Caliburn.Micro.TaskResult`1') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| task | [System.Threading.Tasks.Task{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task{`0}') | The task. |

<a name='P-Caliburn-Micro-TaskResult`1-Result'></a>
### Result `property`

##### Summary

Gets the result of the asynchronous operation.

<a name='M-Caliburn-Micro-TaskResult`1-OnCompleted-System-Threading-Tasks-Task-'></a>
### OnCompleted(task) `method`

##### Summary

Called when the asynchronous task has completed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| task | [System.Threading.Tasks.Task](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task') | The completed task. |

<a name='T-Caliburn-Micro-ViewAttachedEventArgs'></a>
## ViewAttachedEventArgs `type`

##### Namespace

Caliburn.Micro

##### Summary

The event args for the [](#E-Caliburn-Micro-IViewAware-ViewAttached 'Caliburn.Micro.IViewAware.ViewAttached') event.

<a name='P-Caliburn-Micro-ViewAttachedEventArgs-Context'></a>
### Context `property`

##### Summary

The context.

<a name='P-Caliburn-Micro-ViewAttachedEventArgs-View'></a>
### View `property`

##### Summary

The view.

<a name='T-Caliburn-Micro-ViewAware'></a>
## ViewAware `type`

##### Namespace

Caliburn.Micro

##### Summary

A base implementation of [IViewAware](#T-Caliburn-Micro-IViewAware 'Caliburn.Micro.IViewAware') which is capable of caching views by context.

<a name='M-Caliburn-Micro-ViewAware-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates an instance of [ViewAware](#T-Caliburn-Micro-ViewAware 'Caliburn.Micro.ViewAware').

##### Parameters

This constructor has no parameters.

<a name='F-Caliburn-Micro-ViewAware-DefaultContext'></a>
### DefaultContext `constants`

##### Summary

The default view context.

<a name='P-Caliburn-Micro-ViewAware-Views'></a>
### Views `property`

##### Summary

The view chache for this instance.

<a name='M-Caliburn-Micro-ViewAware-GetView-System-Object-'></a>
### GetView(context) `method`

##### Summary

Gets a view previously attached to this instance.

##### Returns

The view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The context denoting which view to retrieve. |

<a name='M-Caliburn-Micro-ViewAware-OnViewAttached-System-Object,System-Object-'></a>
### OnViewAttached(view,context) `method`

##### Summary

Called when a view is attached.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The context in which the view appears. |

<a name='M-Caliburn-Micro-ViewAware-OnViewLoaded-System-Object-'></a>
### OnViewLoaded(view) `method`

##### Summary

Called when an attached view's Loaded event fires.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-Caliburn-Micro-ViewAware-OnViewReady-System-Object-'></a>
### OnViewReady(view) `method`

##### Summary

Called the first time the page's LayoutUpdated event fires after it is navigated to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='T-Caliburn-Micro-WeakValueDictionary`2'></a>
## WeakValueDictionary\`2 `type`

##### Namespace

Caliburn.Micro

##### Summary

A dictionary in which the values are weak references.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TKey | The type of keys in the dictionary. |
| TValue | The type of values in the dictionary. |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.

##### Parameters

This constructor has no parameters.

<a name='M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Collections-Generic-IDictionary{`0,`1}-'></a>
### #ctor(dictionary) `constructor`

##### Summary

Initializes a new instance of the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') class that contains elements copied from the specified [IDictionary\`2](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary`2 'System.Collections.Generic.IDictionary`2') and uses the default equality comparer for the key type.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dictionary | [System.Collections.Generic.IDictionary{\`0,\`1}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{`0,`1}') | The [IDictionary\`2](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary`2 'System.Collections.Generic.IDictionary`2') whose elements are copied to the new [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2'). |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Collections-Generic-IDictionary{`0,`1},System-Collections-Generic-IEqualityComparer{`0}-'></a>
### #ctor(dictionary,comparer) `constructor`

##### Summary

Initializes a new instance of the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') class that contains elements copied from the specified [IDictionary\`2](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary`2 'System.Collections.Generic.IDictionary`2') and uses the specified [IEqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer`1 'System.Collections.Generic.IEqualityComparer`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dictionary | [System.Collections.Generic.IDictionary{\`0,\`1}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{`0,`1}') | The [IDictionary\`2](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary`2 'System.Collections.Generic.IDictionary`2') whose elements are copied to the new [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2'). |
| comparer | [System.Collections.Generic.IEqualityComparer{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer 'System.Collections.Generic.IEqualityComparer{`0}') | The [IEqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer`1 'System.Collections.Generic.IEqualityComparer`1') implementation to use when comparing keys, or null to use the default [EqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.EqualityComparer`1 'System.Collections.Generic.EqualityComparer`1') for the type of the key. |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Collections-Generic-IEqualityComparer{`0}-'></a>
### #ctor(comparer) `constructor`

##### Summary

Initializes a new instance of the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') class that is empty, has the default initial capacity, and uses the specified [IEqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer`1 'System.Collections.Generic.IEqualityComparer`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| comparer | [System.Collections.Generic.IEqualityComparer{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer 'System.Collections.Generic.IEqualityComparer{`0}') | The [IEqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer`1 'System.Collections.Generic.IEqualityComparer`1') implementation to use when comparing keys, or null to use the default [EqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.EqualityComparer`1 'System.Collections.Generic.EqualityComparer`1') for the type of the key. |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Int32-'></a>
### #ctor(capacity) `constructor`

##### Summary

Initializes a new instance of the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| capacity | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The initial number of elements that the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') can contain. |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-#ctor-System-Int32,System-Collections-Generic-IEqualityComparer{`0}-'></a>
### #ctor(capacity,comparer) `constructor`

##### Summary

Initializes a new instance of the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') class that is empty, has the specified initial capacity, and uses the specified [IEqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer`1 'System.Collections.Generic.IEqualityComparer`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| capacity | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The initial number of elements that the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') can contain. |
| comparer | [System.Collections.Generic.IEqualityComparer{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer 'System.Collections.Generic.IEqualityComparer{`0}') | The [IEqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEqualityComparer`1 'System.Collections.Generic.IEqualityComparer`1') implementation to use when comparing keys, or null to use the default [EqualityComparer\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.EqualityComparer`1 'System.Collections.Generic.EqualityComparer`1') for the type of the key. |

<a name='P-Caliburn-Micro-WeakValueDictionary`2-Count'></a>
### Count `property`

##### Summary

Gets the number of key/value pairs contained in the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2').

##### Remarks

Since the items in the dictionary are held by weak reference, the count value
cannot be relied upon to guarantee the number of objects that would be discovered via
enumeration. Treat the Count as an estimate only.

<a name='P-Caliburn-Micro-WeakValueDictionary`2-Item-`0-'></a>
### Item `property`

##### Summary

Gets or sets the value associated with the specified key.

##### Returns

The value associated with the specified key. If the specified key is not found, a get operation throws a [KeyNotFoundException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException'), 
and a set operation creates a new element with the specified key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [\`0](#T-`0 '`0') | The key of the value to get or set. |

<a name='P-Caliburn-Micro-WeakValueDictionary`2-Keys'></a>
### Keys `property`

##### Summary

Gets a collection containing the keys in the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2').

<a name='P-Caliburn-Micro-WeakValueDictionary`2-Values'></a>
### Values `property`

##### Summary

Gets a collection containing the values in the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2').

<a name='M-Caliburn-Micro-WeakValueDictionary`2-Add-`0,`1-'></a>
### Add(key,value) `method`

##### Summary

Adds the specified key and value to the dictionary.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [\`0](#T-`0 '`0') | The key of the element to add. |
| value | [\`1](#T-`1 '`1') | The value of the element to add. The value can be null for reference types. |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-Clear'></a>
### Clear() `method`

##### Summary

Removes all keys and values from the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2').

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-WeakValueDictionary`2-ContainsKey-`0-'></a>
### ContainsKey(key) `method`

##### Summary

Determines whether the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') contains the specified key.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [\`0](#T-`0 '`0') | The key to locate in the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2'). |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

Returns an enumerator that iterates through the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2').

##### Returns

The enumerator.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-WeakValueDictionary`2-Remove-`0-'></a>
### Remove(key) `method`

##### Summary

Removes the value with the specified key from the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2').

##### Returns

true if the element is successfully found and removed; otherwise, false. This method returns false if key is not found in the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [\`0](#T-`0 '`0') | The key of the element to remove. |

<a name='M-Caliburn-Micro-WeakValueDictionary`2-TryGetValue-`0,`1@-'></a>
### TryGetValue(key,value) `method`

##### Summary

Gets the value associated with the specified key.

##### Returns

true if the [WeakValueDictionary\`2](#T-Caliburn-Micro-WeakValueDictionary`2 'Caliburn.Micro.WeakValueDictionary`2') contains an element with the specified key; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [\`0](#T-`0 '`0') | The key of the value to get. |
| value | [\`1@](#T-`1@ '`1@') | When this method returns, contains the value associated with the specified key, 
if the key is found; otherwise, the default value for the type of the value parameter.
This parameter is passed uninitialized. |
