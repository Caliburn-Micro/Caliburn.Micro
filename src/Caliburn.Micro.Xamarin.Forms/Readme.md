<a name='assembly'></a>
# Caliburn.Micro.Xamarin.Forms

## Contents

- [Action](#T-Caliburn-Micro-Xamarin-Forms-Action 'Caliburn.Micro.Xamarin.Forms.Action')
  - [TargetProperty](#F-Caliburn-Micro-Xamarin-Forms-Action-TargetProperty 'Caliburn.Micro.Xamarin.Forms.Action.TargetProperty')
  - [TargetWithoutContextProperty](#F-Caliburn-Micro-Xamarin-Forms-Action-TargetWithoutContextProperty 'Caliburn.Micro.Xamarin.Forms.Action.TargetWithoutContextProperty')
  - [GetTarget(d)](#M-Caliburn-Micro-Xamarin-Forms-Action-GetTarget-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Action.GetTarget(Xamarin.Forms.BindableObject)')
  - [GetTargetWithoutContext(d)](#M-Caliburn-Micro-Xamarin-Forms-Action-GetTargetWithoutContext-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Action.GetTargetWithoutContext(Xamarin.Forms.BindableObject)')
  - [HasTargetSet(element)](#M-Caliburn-Micro-Xamarin-Forms-Action-HasTargetSet-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Action.HasTargetSet(Xamarin.Forms.BindableObject)')
  - [SetTarget(d,target)](#M-Caliburn-Micro-Xamarin-Forms-Action-SetTarget-Xamarin-Forms-BindableObject,System-Object- 'Caliburn.Micro.Xamarin.Forms.Action.SetTarget(Xamarin.Forms.BindableObject,System.Object)')
  - [SetTargetWithoutContext(d,target)](#M-Caliburn-Micro-Xamarin-Forms-Action-SetTargetWithoutContext-Xamarin-Forms-BindableObject,System-Object- 'Caliburn.Micro.Xamarin.Forms.Action.SetTargetWithoutContext(Xamarin.Forms.BindableObject,System.Object)')
- [ActionExecutionContext](#T-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext')
  - [CanExecute](#F-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-CanExecute 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.CanExecute')
  - [EventArgs](#F-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-EventArgs 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.EventArgs')
  - [Method](#F-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Method 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.Method')
  - [Item](#P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Item-System-String- 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.Item(System.String)')
  - [Message](#P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Message 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.Message')
  - [Source](#P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Source 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.Source')
  - [Target](#P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Target 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.Target')
  - [View](#P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-View 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.View')
  - [Dispose()](#M-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Dispose 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext.Dispose')
- [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage')
  - [#ctor()](#M-Caliburn-Micro-Xamarin-Forms-ActionMessage-#ctor 'Caliburn.Micro.Xamarin.Forms.ActionMessage.#ctor')
  - [ApplyAvailabilityEffect](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-ApplyAvailabilityEffect 'Caliburn.Micro.Xamarin.Forms.ActionMessage.ApplyAvailabilityEffect')
  - [BuildPossibleGuardNames](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-BuildPossibleGuardNames 'Caliburn.Micro.Xamarin.Forms.ActionMessage.BuildPossibleGuardNames')
  - [EnforceGuardsDuringInvocation](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-EnforceGuardsDuringInvocation 'Caliburn.Micro.Xamarin.Forms.ActionMessage.EnforceGuardsDuringInvocation')
  - [GetTargetMethod](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-GetTargetMethod 'Caliburn.Micro.Xamarin.Forms.ActionMessage.GetTargetMethod')
  - [InvokeAction](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-InvokeAction 'Caliburn.Micro.Xamarin.Forms.ActionMessage.InvokeAction')
  - [PrepareContext](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-PrepareContext 'Caliburn.Micro.Xamarin.Forms.ActionMessage.PrepareContext')
  - [SetMethodBinding](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-SetMethodBinding 'Caliburn.Micro.Xamarin.Forms.ActionMessage.SetMethodBinding')
  - [ThrowsExceptions](#F-Caliburn-Micro-Xamarin-Forms-ActionMessage-ThrowsExceptions 'Caliburn.Micro.Xamarin.Forms.ActionMessage.ThrowsExceptions')
  - [Handler](#P-Caliburn-Micro-Xamarin-Forms-ActionMessage-Handler 'Caliburn.Micro.Xamarin.Forms.ActionMessage.Handler')
  - [MethodName](#P-Caliburn-Micro-Xamarin-Forms-ActionMessage-MethodName 'Caliburn.Micro.Xamarin.Forms.ActionMessage.MethodName')
  - [Parameters](#P-Caliburn-Micro-Xamarin-Forms-ActionMessage-Parameters 'Caliburn.Micro.Xamarin.Forms.ActionMessage.Parameters')
  - [Invoke(sender)](#M-Caliburn-Micro-Xamarin-Forms-ActionMessage-Invoke-Xamarin-Forms-VisualElement- 'Caliburn.Micro.Xamarin.Forms.ActionMessage.Invoke(Xamarin.Forms.VisualElement)')
  - [OnAttached()](#M-Caliburn-Micro-Xamarin-Forms-ActionMessage-OnAttached 'Caliburn.Micro.Xamarin.Forms.ActionMessage.OnAttached')
  - [OnDetaching()](#M-Caliburn-Micro-Xamarin-Forms-ActionMessage-OnDetaching 'Caliburn.Micro.Xamarin.Forms.ActionMessage.OnDetaching')
  - [ToString()](#M-Caliburn-Micro-Xamarin-Forms-ActionMessage-ToString 'Caliburn.Micro.Xamarin.Forms.ActionMessage.ToString')
  - [TryFindGuardMethod(context,possibleGuardNames)](#M-Caliburn-Micro-Xamarin-Forms-ActionMessage-TryFindGuardMethod-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext,System-Collections-Generic-IEnumerable{System-String}- 'Caliburn.Micro.Xamarin.Forms.ActionMessage.TryFindGuardMethod(Caliburn.Micro.Xamarin.Forms.ActionExecutionContext,System.Collections.Generic.IEnumerable{System.String})')
  - [UpdateAvailability()](#M-Caliburn-Micro-Xamarin-Forms-ActionMessage-UpdateAvailability 'Caliburn.Micro.Xamarin.Forms.ActionMessage.UpdateAvailability')
- [AttachedCollection\`1](#T-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1 'Caliburn.Micro.Xamarin.Forms.AttachedCollection`1')
  - [AssociatedObject](#P-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-AssociatedObject 'Caliburn.Micro.Xamarin.Forms.AttachedCollection`1.AssociatedObject')
  - [Attach(dependencyObject)](#M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-Attach-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.AttachedCollection`1.Attach(Xamarin.Forms.BindableObject)')
  - [Detach()](#M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-Detach 'Caliburn.Micro.Xamarin.Forms.AttachedCollection`1.Detach')
  - [OnCollectionChanged(e)](#M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-OnCollectionChanged-System-Collections-Specialized-NotifyCollectionChangedEventArgs- 'Caliburn.Micro.Xamarin.Forms.AttachedCollection`1.OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs)')
  - [OnItemAdded(item)](#M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-OnItemAdded-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.AttachedCollection`1.OnItemAdded(Xamarin.Forms.BindableObject)')
  - [OnItemRemoved(item)](#M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-OnItemRemoved-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.AttachedCollection`1.OnItemRemoved(Xamarin.Forms.BindableObject)')
- [Bind](#T-Caliburn-Micro-Xamarin-Forms-Bind 'Caliburn.Micro.Xamarin.Forms.Bind')
  - [AtDesignTimeProperty](#F-Caliburn-Micro-Xamarin-Forms-Bind-AtDesignTimeProperty 'Caliburn.Micro.Xamarin.Forms.Bind.AtDesignTimeProperty')
  - [ModelProperty](#F-Caliburn-Micro-Xamarin-Forms-Bind-ModelProperty 'Caliburn.Micro.Xamarin.Forms.Bind.ModelProperty')
  - [ModelWithoutContextProperty](#F-Caliburn-Micro-Xamarin-Forms-Bind-ModelWithoutContextProperty 'Caliburn.Micro.Xamarin.Forms.Bind.ModelWithoutContextProperty')
  - [GetAtDesignTime(dependencyObject)](#M-Caliburn-Micro-Xamarin-Forms-Bind-GetAtDesignTime-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Bind.GetAtDesignTime(Xamarin.Forms.BindableObject)')
  - [GetModel(dependencyObject)](#M-Caliburn-Micro-Xamarin-Forms-Bind-GetModel-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Bind.GetModel(Xamarin.Forms.BindableObject)')
  - [GetModelWithoutContext(dependencyObject)](#M-Caliburn-Micro-Xamarin-Forms-Bind-GetModelWithoutContext-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Bind.GetModelWithoutContext(Xamarin.Forms.BindableObject)')
  - [SetAtDesignTime(dependencyObject,value)](#M-Caliburn-Micro-Xamarin-Forms-Bind-SetAtDesignTime-Xamarin-Forms-BindableObject,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.Bind.SetAtDesignTime(Xamarin.Forms.BindableObject,System.Boolean)')
  - [SetModel(dependencyObject,value)](#M-Caliburn-Micro-Xamarin-Forms-Bind-SetModel-Xamarin-Forms-BindableObject,System-Object- 'Caliburn.Micro.Xamarin.Forms.Bind.SetModel(Xamarin.Forms.BindableObject,System.Object)')
  - [SetModelWithoutContext(dependencyObject,value)](#M-Caliburn-Micro-Xamarin-Forms-Bind-SetModelWithoutContext-Xamarin-Forms-BindableObject,System-Object- 'Caliburn.Micro.Xamarin.Forms.Bind.SetModelWithoutContext(Xamarin.Forms.BindableObject,System.Object)')
- [ConventionManager](#T-Caliburn-Micro-Xamarin-Forms-ConventionManager 'Caliburn.Micro.Xamarin.Forms.ConventionManager')
  - [ApplyBindingMode](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyBindingMode 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ApplyBindingMode')
  - [ApplyStringFormat](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyStringFormat 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ApplyStringFormat')
  - [ApplyUpdateSourceTrigger](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyUpdateSourceTrigger 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ApplyUpdateSourceTrigger')
  - [ApplyValidation](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyValidation 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ApplyValidation')
  - [ApplyValueConverter](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyValueConverter 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ApplyValueConverter')
  - [ConfigureSelectedItem](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ConfigureSelectedItem 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ConfigureSelectedItem')
  - [ConfigureSelectedItemBinding](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ConfigureSelectedItemBinding 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ConfigureSelectedItemBinding')
  - [DefaultHeaderTemplate](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-DefaultHeaderTemplate 'Caliburn.Micro.Xamarin.Forms.ConventionManager.DefaultHeaderTemplate')
  - [DefaultItemTemplate](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-DefaultItemTemplate 'Caliburn.Micro.Xamarin.Forms.ConventionManager.DefaultItemTemplate')
  - [DerivePotentialSelectionNames](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-DerivePotentialSelectionNames 'Caliburn.Micro.Xamarin.Forms.ConventionManager.DerivePotentialSelectionNames')
  - [IncludeStaticProperties](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-IncludeStaticProperties 'Caliburn.Micro.Xamarin.Forms.ConventionManager.IncludeStaticProperties')
  - [OverwriteContent](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-OverwriteContent 'Caliburn.Micro.Xamarin.Forms.ConventionManager.OverwriteContent')
  - [SetBinding](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-SetBinding 'Caliburn.Micro.Xamarin.Forms.ConventionManager.SetBinding')
  - [Singularize](#F-Caliburn-Micro-Xamarin-Forms-ConventionManager-Singularize 'Caliburn.Micro.Xamarin.Forms.ConventionManager.Singularize')
  - [AddElementConvention(convention)](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-AddElementConvention-Caliburn-Micro-Xamarin-Forms-ElementConvention- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.AddElementConvention(Caliburn.Micro.Xamarin.Forms.ElementConvention)')
  - [AddElementConvention\`\`1(bindableProperty,parameterProperty,eventName)](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-AddElementConvention``1-Xamarin-Forms-BindableProperty,System-String,System-String- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.AddElementConvention``1(Xamarin.Forms.BindableProperty,System.String,System.String)')
  - [ApplyHeaderTemplate(element,headerTemplateProperty,headerTemplateSelectorProperty,viewModelType)](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyHeaderTemplate-Xamarin-Forms-VisualElement,Xamarin-Forms-BindableProperty,Xamarin-Forms-BindableProperty,System-Type- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ApplyHeaderTemplate(Xamarin.Forms.VisualElement,Xamarin.Forms.BindableProperty,Xamarin.Forms.BindableProperty,System.Type)')
  - [ApplyItemTemplate\`\`1(itemsControl,property)](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyItemTemplate``1-Xamarin-Forms-ItemsView{``0},System-Reflection-PropertyInfo- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.ApplyItemTemplate``1(Xamarin.Forms.ItemsView{``0},System.Reflection.PropertyInfo)')
  - [GetElementConvention(elementType)](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-GetElementConvention-System-Type- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.GetElementConvention(System.Type)')
  - [GetPropertyCaseInsensitive(type,propertyName)](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-GetPropertyCaseInsensitive-System-Type,System-String- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.GetPropertyCaseInsensitive(System.Type,System.String)')
  - [HasBinding()](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-HasBinding-Xamarin-Forms-VisualElement,Xamarin-Forms-BindableProperty- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.HasBinding(Xamarin.Forms.VisualElement,Xamarin.Forms.BindableProperty)')
  - [SetBindingWithoutBindingOrValueOverwrite(viewModelType,path,property,element,convention,bindableProperty)](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-SetBindingWithoutBindingOrValueOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-ElementConvention,Xamarin-Forms-BindableProperty- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.SetBindingWithoutBindingOrValueOverwrite(System.Type,System.String,System.Reflection.PropertyInfo,Xamarin.Forms.VisualElement,Caliburn.Micro.Xamarin.Forms.ElementConvention,Xamarin.Forms.BindableProperty)')
  - [SetBindingWithoutBindingOverwrite()](#M-Caliburn-Micro-Xamarin-Forms-ConventionManager-SetBindingWithoutBindingOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-ElementConvention,Xamarin-Forms-BindableProperty- 'Caliburn.Micro.Xamarin.Forms.ConventionManager.SetBindingWithoutBindingOverwrite(System.Type,System.String,System.Reflection.PropertyInfo,Xamarin.Forms.VisualElement,Caliburn.Micro.Xamarin.Forms.ElementConvention,Xamarin.Forms.BindableProperty)')
- [DependencyPropertyChangedEventArgs](#T-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyChangedEventArgs')
  - [#ctor(newValue,oldValue,property)](#M-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-#ctor-System-Object,System-Object,Xamarin-Forms-BindableProperty- 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyChangedEventArgs.#ctor(System.Object,System.Object,Xamarin.Forms.BindableProperty)')
  - [NewValue](#P-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-NewValue 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyChangedEventArgs.NewValue')
  - [OldValue](#P-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-OldValue 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyChangedEventArgs.OldValue')
  - [Property](#P-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-Property 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyChangedEventArgs.Property')
- [DependencyPropertyHelper](#T-Caliburn-Micro-Xamarin-Forms-DependencyPropertyHelper 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyHelper')
  - [Register(name,propertyType,ownerType,defaultValue,propertyChangedCallback)](#M-Caliburn-Micro-Xamarin-Forms-DependencyPropertyHelper-Register-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback- 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyHelper.Register(System.String,System.Type,System.Type,System.Object,Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback)')
  - [RegisterAttached(name,propertyType,ownerType,defaultValue,propertyChangedCallback)](#M-Caliburn-Micro-Xamarin-Forms-DependencyPropertyHelper-RegisterAttached-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback- 'Caliburn.Micro.Xamarin.Forms.DependencyPropertyHelper.RegisterAttached(System.String,System.Type,System.Type,System.Object,Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback)')
- [ElementConvention](#T-Caliburn-Micro-Xamarin-Forms-ElementConvention 'Caliburn.Micro.Xamarin.Forms.ElementConvention')
  - [ApplyBinding](#F-Caliburn-Micro-Xamarin-Forms-ElementConvention-ApplyBinding 'Caliburn.Micro.Xamarin.Forms.ElementConvention.ApplyBinding')
  - [CreateTrigger](#F-Caliburn-Micro-Xamarin-Forms-ElementConvention-CreateTrigger 'Caliburn.Micro.Xamarin.Forms.ElementConvention.CreateTrigger')
  - [ElementType](#F-Caliburn-Micro-Xamarin-Forms-ElementConvention-ElementType 'Caliburn.Micro.Xamarin.Forms.ElementConvention.ElementType')
  - [GetBindableProperty](#F-Caliburn-Micro-Xamarin-Forms-ElementConvention-GetBindableProperty 'Caliburn.Micro.Xamarin.Forms.ElementConvention.GetBindableProperty')
  - [ParameterProperty](#F-Caliburn-Micro-Xamarin-Forms-ElementConvention-ParameterProperty 'Caliburn.Micro.Xamarin.Forms.ElementConvention.ParameterProperty')
- [FormsApplication](#T-Caliburn-Micro-Xamarin-Forms-FormsApplication 'Caliburn.Micro.Xamarin.Forms.FormsApplication')
  - [RootNavigationPage](#P-Caliburn-Micro-Xamarin-Forms-FormsApplication-RootNavigationPage 'Caliburn.Micro.Xamarin.Forms.FormsApplication.RootNavigationPage')
  - [CreateApplicationPage()](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-CreateApplicationPage 'Caliburn.Micro.Xamarin.Forms.FormsApplication.CreateApplicationPage')
  - [DisplayRootView(viewType)](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootView-System-Type- 'Caliburn.Micro.Xamarin.Forms.FormsApplication.DisplayRootView(System.Type)')
  - [DisplayRootViewForAsync(viewModelType)](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootViewForAsync-System-Type- 'Caliburn.Micro.Xamarin.Forms.FormsApplication.DisplayRootViewForAsync(System.Type)')
  - [DisplayRootViewForAsync\`\`1()](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootViewForAsync``1 'Caliburn.Micro.Xamarin.Forms.FormsApplication.DisplayRootViewForAsync``1')
  - [DisplayRootView\`\`1()](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootView``1 'Caliburn.Micro.Xamarin.Forms.FormsApplication.DisplayRootView``1')
  - [Initialize()](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-Initialize 'Caliburn.Micro.Xamarin.Forms.FormsApplication.Initialize')
  - [PrepareViewFirst()](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-PrepareViewFirst 'Caliburn.Micro.Xamarin.Forms.FormsApplication.PrepareViewFirst')
  - [PrepareViewFirst(navigationPage)](#M-Caliburn-Micro-Xamarin-Forms-FormsApplication-PrepareViewFirst-Xamarin-Forms-NavigationPage- 'Caliburn.Micro.Xamarin.Forms.FormsApplication.PrepareViewFirst(Xamarin.Forms.NavigationPage)')
- [FormsPlatformProvider](#T-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider')
  - [#ctor(platformProvider)](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-#ctor-Caliburn-Micro-IPlatformProvider- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.#ctor(Caliburn.Micro.IPlatformProvider)')
  - [InDesignMode](#P-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-InDesignMode 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.InDesignMode')
  - [PropertyChangeNotificationsOnUIThread](#P-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-PropertyChangeNotificationsOnUIThread 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.PropertyChangeNotificationsOnUIThread')
  - [BeginOnUIThread()](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-BeginOnUIThread-System-Action- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.BeginOnUIThread(System.Action)')
  - [ExecuteOnFirstLoad()](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.ExecuteOnFirstLoad(System.Object,System.Action{System.Object})')
  - [ExecuteOnLayoutUpdated()](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.ExecuteOnLayoutUpdated(System.Object,System.Action{System.Object})')
  - [GetFirstNonGeneratedView()](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-GetFirstNonGeneratedView-System-Object- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.GetFirstNonGeneratedView(System.Object)')
  - [GetViewCloseAction()](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.GetViewCloseAction(System.Object,System.Collections.Generic.ICollection{System.Object},System.Nullable{System.Boolean})')
  - [OnUIThread()](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-OnUIThread-System-Action- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.OnUIThread(System.Action)')
  - [OnUIThreadAsync()](#M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}- 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider.OnUIThreadAsync(System.Func{System.Threading.Tasks.Task})')
- [IAttachedObject](#T-Caliburn-Micro-Xamarin-Forms-IAttachedObject 'Caliburn.Micro.Xamarin.Forms.IAttachedObject')
  - [AssociatedObject](#P-Caliburn-Micro-Xamarin-Forms-IAttachedObject-AssociatedObject 'Caliburn.Micro.Xamarin.Forms.IAttachedObject.AssociatedObject')
  - [Attach(dependencyObject)](#M-Caliburn-Micro-Xamarin-Forms-IAttachedObject-Attach-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.IAttachedObject.Attach(Xamarin.Forms.BindableObject)')
  - [Detach()](#M-Caliburn-Micro-Xamarin-Forms-IAttachedObject-Detach 'Caliburn.Micro.Xamarin.Forms.IAttachedObject.Detach')
- [IHaveParameters](#T-Caliburn-Micro-Xamarin-Forms-IHaveParameters 'Caliburn.Micro.Xamarin.Forms.IHaveParameters')
  - [Parameters](#P-Caliburn-Micro-Xamarin-Forms-IHaveParameters-Parameters 'Caliburn.Micro.Xamarin.Forms.IHaveParameters.Parameters')
- [INavigationService](#T-Caliburn-Micro-Xamarin-Forms-INavigationService 'Caliburn.Micro.Xamarin.Forms.INavigationService')
  - [GoBackAsync(animated)](#M-Caliburn-Micro-Xamarin-Forms-INavigationService-GoBackAsync-System-Boolean- 'Caliburn.Micro.Xamarin.Forms.INavigationService.GoBackAsync(System.Boolean)')
  - [GoBackToRootAsync(animated)](#M-Caliburn-Micro-Xamarin-Forms-INavigationService-GoBackToRootAsync-System-Boolean- 'Caliburn.Micro.Xamarin.Forms.INavigationService.GoBackToRootAsync(System.Boolean)')
  - [NavigateToViewAsync(viewType,parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.INavigationService.NavigateToViewAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.INavigationService.NavigateToViewAsync``1(System.Object,System.Boolean)')
  - [NavigateToViewModelAsync(viewModelType,parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.INavigationService.NavigateToViewModelAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewModelAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewModelAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.INavigationService.NavigateToViewModelAsync``1(System.Object,System.Boolean)')
- [Message](#T-Caliburn-Micro-Xamarin-Forms-Message 'Caliburn.Micro.Xamarin.Forms.Message')
  - [AttachProperty](#F-Caliburn-Micro-Xamarin-Forms-Message-AttachProperty 'Caliburn.Micro.Xamarin.Forms.Message.AttachProperty')
  - [GetAttach(d)](#M-Caliburn-Micro-Xamarin-Forms-Message-GetAttach-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Message.GetAttach(Xamarin.Forms.BindableObject)')
  - [GetHandler(d)](#M-Caliburn-Micro-Xamarin-Forms-Message-GetHandler-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.Message.GetHandler(Xamarin.Forms.BindableObject)')
  - [SetAttach(d,attachText)](#M-Caliburn-Micro-Xamarin-Forms-Message-SetAttach-Xamarin-Forms-BindableObject,System-String- 'Caliburn.Micro.Xamarin.Forms.Message.SetAttach(Xamarin.Forms.BindableObject,System.String)')
  - [SetHandler(d,value)](#M-Caliburn-Micro-Xamarin-Forms-Message-SetHandler-Xamarin-Forms-BindableObject,System-Object- 'Caliburn.Micro.Xamarin.Forms.Message.SetHandler(Xamarin.Forms.BindableObject,System.Object)')
- [MessageBinder](#T-Caliburn-Micro-Xamarin-Forms-MessageBinder 'Caliburn.Micro.Xamarin.Forms.MessageBinder')
  - [CustomConverters](#F-Caliburn-Micro-Xamarin-Forms-MessageBinder-CustomConverters 'Caliburn.Micro.Xamarin.Forms.MessageBinder.CustomConverters')
  - [EvaluateParameter](#F-Caliburn-Micro-Xamarin-Forms-MessageBinder-EvaluateParameter 'Caliburn.Micro.Xamarin.Forms.MessageBinder.EvaluateParameter')
  - [SpecialValues](#F-Caliburn-Micro-Xamarin-Forms-MessageBinder-SpecialValues 'Caliburn.Micro.Xamarin.Forms.MessageBinder.SpecialValues')
  - [CoerceValue(destinationType,providedValue,context)](#M-Caliburn-Micro-Xamarin-Forms-MessageBinder-CoerceValue-System-Type,System-Object,System-Object- 'Caliburn.Micro.Xamarin.Forms.MessageBinder.CoerceValue(System.Type,System.Object,System.Object)')
  - [DetermineParameters(context,requiredParameters)](#M-Caliburn-Micro-Xamarin-Forms-MessageBinder-DetermineParameters-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext,System-Reflection-ParameterInfo[]- 'Caliburn.Micro.Xamarin.Forms.MessageBinder.DetermineParameters(Caliburn.Micro.Xamarin.Forms.ActionExecutionContext,System.Reflection.ParameterInfo[])')
  - [GetDefaultValue(type)](#M-Caliburn-Micro-Xamarin-Forms-MessageBinder-GetDefaultValue-System-Type- 'Caliburn.Micro.Xamarin.Forms.MessageBinder.GetDefaultValue(System.Type)')
- [NavigateHelper\`1](#T-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1 'Caliburn.Micro.Xamarin.Forms.NavigateHelper`1')
  - [AttachTo(navigationService)](#M-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1-AttachTo-Caliburn-Micro-Xamarin-Forms-INavigationService- 'Caliburn.Micro.Xamarin.Forms.NavigateHelper`1.AttachTo(Caliburn.Micro.Xamarin.Forms.INavigationService)')
  - [Navigate()](#M-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1-Navigate-System-Boolean- 'Caliburn.Micro.Xamarin.Forms.NavigateHelper`1.Navigate(System.Boolean)')
  - [WithParam\`\`1(property,value)](#M-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1-WithParam``1-System-Linq-Expressions-Expression{System-Func{`0,``0}},``0- 'Caliburn.Micro.Xamarin.Forms.NavigateHelper`1.WithParam``1(System.Linq.Expressions.Expression{System.Func{`0,``0}},``0)')
- [NavigationExtensions](#T-Caliburn-Micro-Xamarin-Forms-NavigationExtensions 'Caliburn.Micro.Xamarin.Forms.NavigationExtensions')
  - [For\`\`1(navigationService)](#M-Caliburn-Micro-Xamarin-Forms-NavigationExtensions-For``1-Caliburn-Micro-Xamarin-Forms-INavigationService- 'Caliburn.Micro.Xamarin.Forms.NavigationExtensions.For``1(Caliburn.Micro.Xamarin.Forms.INavigationService)')
- [NavigationPageAdapter](#T-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter')
  - [#ctor(navigationPage)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-#ctor-Xamarin-Forms-NavigationPage- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.#ctor(Xamarin.Forms.NavigationPage)')
  - [ActivateViewAsync(view)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-ActivateViewAsync-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.ActivateViewAsync(Xamarin.Forms.BindableObject)')
  - [CreateContentPage(view,viewModel)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-CreateContentPage-Xamarin-Forms-ContentView,System-Object- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.CreateContentPage(Xamarin.Forms.ContentView,System.Object)')
  - [DeactivateViewAsync(view)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-DeactivateViewAsync-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.DeactivateViewAsync(Xamarin.Forms.BindableObject)')
  - [GoBackAsync(animated)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-GoBackAsync-System-Boolean- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.GoBackAsync(System.Boolean)')
  - [GoBackToRootAsync(animated)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-GoBackToRootAsync-System-Boolean- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.GoBackToRootAsync(System.Boolean)')
  - [NavigateToViewAsync(viewType,parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.NavigateToViewAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.NavigateToViewAsync``1(System.Object,System.Boolean)')
  - [NavigateToViewModelAsync(viewModelType,parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.NavigateToViewModelAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewModelAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewModelAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.NavigateToViewModelAsync``1(System.Object,System.Boolean)')
  - [TryInjectParameters(viewModel,parameter)](#M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-TryInjectParameters-System-Object,System-Object- 'Caliburn.Micro.Xamarin.Forms.NavigationPageAdapter.TryInjectParameters(System.Object,System.Object)')
- [Parameter](#T-Caliburn-Micro-Xamarin-Forms-Parameter 'Caliburn.Micro.Xamarin.Forms.Parameter')
  - [ValueProperty](#F-Caliburn-Micro-Xamarin-Forms-Parameter-ValueProperty 'Caliburn.Micro.Xamarin.Forms.Parameter.ValueProperty')
  - [Owner](#P-Caliburn-Micro-Xamarin-Forms-Parameter-Owner 'Caliburn.Micro.Xamarin.Forms.Parameter.Owner')
  - [Value](#P-Caliburn-Micro-Xamarin-Forms-Parameter-Value 'Caliburn.Micro.Xamarin.Forms.Parameter.Value')
  - [MakeAwareOf(owner)](#M-Caliburn-Micro-Xamarin-Forms-Parameter-MakeAwareOf-Caliburn-Micro-Xamarin-Forms-ActionMessage- 'Caliburn.Micro.Xamarin.Forms.Parameter.MakeAwareOf(Caliburn.Micro.Xamarin.Forms.ActionMessage)')
- [Parser](#T-Caliburn-Micro-Xamarin-Forms-Parser 'Caliburn.Micro.Xamarin.Forms.Parser')
  - [CreateParameter](#F-Caliburn-Micro-Xamarin-Forms-Parser-CreateParameter 'Caliburn.Micro.Xamarin.Forms.Parser.CreateParameter')
  - [CreateTrigger](#F-Caliburn-Micro-Xamarin-Forms-Parser-CreateTrigger 'Caliburn.Micro.Xamarin.Forms.Parser.CreateTrigger')
  - [InterpretMessageText](#F-Caliburn-Micro-Xamarin-Forms-Parser-InterpretMessageText 'Caliburn.Micro.Xamarin.Forms.Parser.InterpretMessageText')
  - [BindParameter(target,parameter,elementName,path,bindingMode)](#M-Caliburn-Micro-Xamarin-Forms-Parser-BindParameter-Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-Parameter,System-String,System-String,Xamarin-Forms-BindingMode- 'Caliburn.Micro.Xamarin.Forms.Parser.BindParameter(Xamarin.Forms.VisualElement,Caliburn.Micro.Xamarin.Forms.Parameter,System.String,System.String,Xamarin.Forms.BindingMode)')
  - [CreateMessage(target,messageText)](#M-Caliburn-Micro-Xamarin-Forms-Parser-CreateMessage-Xamarin-Forms-BindableObject,System-String- 'Caliburn.Micro.Xamarin.Forms.Parser.CreateMessage(Xamarin.Forms.BindableObject,System.String)')
  - [Parse(target,text)](#M-Caliburn-Micro-Xamarin-Forms-Parser-Parse-Xamarin-Forms-BindableObject,System-String- 'Caliburn.Micro.Xamarin.Forms.Parser.Parse(Xamarin.Forms.BindableObject,System.String)')
- [PropertyChangedCallback](#T-Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback 'Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback')
- [RoutedEventArgs](#T-Caliburn-Micro-Xamarin-Forms-RoutedEventArgs 'Caliburn.Micro.Xamarin.Forms.RoutedEventArgs')
  - [OriginalSource](#P-Caliburn-Micro-Xamarin-Forms-RoutedEventArgs-OriginalSource 'Caliburn.Micro.Xamarin.Forms.RoutedEventArgs.OriginalSource')
- [RoutedEventHandler](#T-Caliburn-Micro-Xamarin-Forms-RoutedEventHandler 'Caliburn.Micro.Xamarin.Forms.RoutedEventHandler')
- [TriggerActionBase\`1](#T-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1 'Caliburn.Micro.Xamarin.Forms.TriggerActionBase`1')
  - [AssociatedObject](#P-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1-AssociatedObject 'Caliburn.Micro.Xamarin.Forms.TriggerActionBase`1.AssociatedObject')
  - [OnAttached()](#M-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1-OnAttached 'Caliburn.Micro.Xamarin.Forms.TriggerActionBase`1.OnAttached')
  - [OnDetaching()](#M-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1-OnDetaching 'Caliburn.Micro.Xamarin.Forms.TriggerActionBase`1.OnDetaching')
- [View](#T-Caliburn-Micro-Xamarin-Forms-View 'Caliburn.Micro.Xamarin.Forms.View')
  - [ApplyConventionsProperty](#F-Caliburn-Micro-Xamarin-Forms-View-ApplyConventionsProperty 'Caliburn.Micro.Xamarin.Forms.View.ApplyConventionsProperty')
  - [ContextProperty](#F-Caliburn-Micro-Xamarin-Forms-View-ContextProperty 'Caliburn.Micro.Xamarin.Forms.View.ContextProperty')
  - [GetFirstNonGeneratedView](#F-Caliburn-Micro-Xamarin-Forms-View-GetFirstNonGeneratedView 'Caliburn.Micro.Xamarin.Forms.View.GetFirstNonGeneratedView')
  - [IsGeneratedProperty](#F-Caliburn-Micro-Xamarin-Forms-View-IsGeneratedProperty 'Caliburn.Micro.Xamarin.Forms.View.IsGeneratedProperty')
  - [IsLoadedProperty](#F-Caliburn-Micro-Xamarin-Forms-View-IsLoadedProperty 'Caliburn.Micro.Xamarin.Forms.View.IsLoadedProperty')
  - [IsScopeRootProperty](#F-Caliburn-Micro-Xamarin-Forms-View-IsScopeRootProperty 'Caliburn.Micro.Xamarin.Forms.View.IsScopeRootProperty')
  - [ModelProperty](#F-Caliburn-Micro-Xamarin-Forms-View-ModelProperty 'Caliburn.Micro.Xamarin.Forms.View.ModelProperty')
  - [InDesignMode](#P-Caliburn-Micro-Xamarin-Forms-View-InDesignMode 'Caliburn.Micro.Xamarin.Forms.View.InDesignMode')
  - [ExecuteOnLoad(element,handler)](#M-Caliburn-Micro-Xamarin-Forms-View-ExecuteOnLoad-Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-RoutedEventHandler- 'Caliburn.Micro.Xamarin.Forms.View.ExecuteOnLoad(Xamarin.Forms.VisualElement,Caliburn.Micro.Xamarin.Forms.RoutedEventHandler)')
  - [ExecuteOnUnload(element,handler)](#M-Caliburn-Micro-Xamarin-Forms-View-ExecuteOnUnload-Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-RoutedEventHandler- 'Caliburn.Micro.Xamarin.Forms.View.ExecuteOnUnload(Xamarin.Forms.VisualElement,Caliburn.Micro.Xamarin.Forms.RoutedEventHandler)')
  - [GetApplyConventions(d)](#M-Caliburn-Micro-Xamarin-Forms-View-GetApplyConventions-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.View.GetApplyConventions(Xamarin.Forms.BindableObject)')
  - [GetContext(d)](#M-Caliburn-Micro-Xamarin-Forms-View-GetContext-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.View.GetContext(Xamarin.Forms.BindableObject)')
  - [GetModel(d)](#M-Caliburn-Micro-Xamarin-Forms-View-GetModel-Xamarin-Forms-BindableObject- 'Caliburn.Micro.Xamarin.Forms.View.GetModel(Xamarin.Forms.BindableObject)')
  - [SetApplyConventions(d,value)](#M-Caliburn-Micro-Xamarin-Forms-View-SetApplyConventions-Xamarin-Forms-BindableObject,System-Nullable{System-Boolean}- 'Caliburn.Micro.Xamarin.Forms.View.SetApplyConventions(Xamarin.Forms.BindableObject,System.Nullable{System.Boolean})')
  - [SetContext(d,value)](#M-Caliburn-Micro-Xamarin-Forms-View-SetContext-Xamarin-Forms-BindableObject,System-Object- 'Caliburn.Micro.Xamarin.Forms.View.SetContext(Xamarin.Forms.BindableObject,System.Object)')
  - [SetModel(d,value)](#M-Caliburn-Micro-Xamarin-Forms-View-SetModel-Xamarin-Forms-BindableObject,System-Object- 'Caliburn.Micro.Xamarin.Forms.View.SetModel(Xamarin.Forms.BindableObject,System.Object)')
- [ViewLocator](#T-Caliburn-Micro-Xamarin-Forms-ViewLocator 'Caliburn.Micro.Xamarin.Forms.ViewLocator')
  - [ContextSeparator](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-ContextSeparator 'Caliburn.Micro.Xamarin.Forms.ViewLocator.ContextSeparator')
  - [DeterminePackUriFromType](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-DeterminePackUriFromType 'Caliburn.Micro.Xamarin.Forms.ViewLocator.DeterminePackUriFromType')
  - [GetOrCreateViewType](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-GetOrCreateViewType 'Caliburn.Micro.Xamarin.Forms.ViewLocator.GetOrCreateViewType')
  - [LocateForModel](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-LocateForModel 'Caliburn.Micro.Xamarin.Forms.ViewLocator.LocateForModel')
  - [LocateForModelType](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-LocateForModelType 'Caliburn.Micro.Xamarin.Forms.ViewLocator.LocateForModelType')
  - [LocateTypeForModelType](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-LocateTypeForModelType 'Caliburn.Micro.Xamarin.Forms.ViewLocator.LocateTypeForModelType')
  - [ModifyModelTypeAtDesignTime](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-ModifyModelTypeAtDesignTime 'Caliburn.Micro.Xamarin.Forms.ViewLocator.ModifyModelTypeAtDesignTime')
  - [NameTransformer](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-NameTransformer 'Caliburn.Micro.Xamarin.Forms.ViewLocator.NameTransformer')
  - [TransformName](#F-Caliburn-Micro-Xamarin-Forms-ViewLocator-TransformName 'Caliburn.Micro.Xamarin.Forms.ViewLocator.TransformName')
  - [AddDefaultTypeMapping(viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddDefaultTypeMapping-System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.AddDefaultTypeMapping(System.String)')
  - [AddNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.AddNamespaceMapping(System.String,System.String[],System.String)')
  - [AddNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.AddNamespaceMapping(System.String,System.String,System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddSubNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.AddSubNamespaceMapping(System.String,System.String[],System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddSubNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.AddSubNamespaceMapping(System.String,System.String,System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddTypeMapping-System-String,System-String,System-String[],System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.AddTypeMapping(System.String,System.String,System.String[],System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddTypeMapping-System-String,System-String,System-String,System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.AddTypeMapping(System.String,System.String,System.String,System.String)')
  - [ConfigureTypeMappings(config)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.ConfigureTypeMappings(Caliburn.Micro.TypeMappingConfiguration)')
  - [InitializeComponent(element)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-InitializeComponent-System-Object- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.InitializeComponent(System.Object)')
  - [RegisterViewSuffix(viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewLocator-RegisterViewSuffix-System-String- 'Caliburn.Micro.Xamarin.Forms.ViewLocator.RegisterViewSuffix(System.String)')
- [ViewModelBinder](#T-Caliburn-Micro-Xamarin-Forms-ViewModelBinder 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder')
  - [ApplyConventionsByDefault](#F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-ApplyConventionsByDefault 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder.ApplyConventionsByDefault')
  - [Bind](#F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-Bind 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder.Bind')
  - [BindActions](#F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-BindActions 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder.BindActions')
  - [BindProperties](#F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-BindProperties 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder.BindProperties')
  - [ConventionsAppliedProperty](#F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-ConventionsAppliedProperty 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder.ConventionsAppliedProperty')
  - [HandleUnmatchedElements](#F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-HandleUnmatchedElements 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder.HandleUnmatchedElements')
  - [ShouldApplyConventions(view)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-ShouldApplyConventions-Xamarin-Forms-VisualElement- 'Caliburn.Micro.Xamarin.Forms.ViewModelBinder.ShouldApplyConventions(Xamarin.Forms.VisualElement)')
- [ViewModelLocator](#T-Caliburn-Micro-Xamarin-Forms-ViewModelLocator 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator')
  - [InterfaceCaptureGroupName](#F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-InterfaceCaptureGroupName 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.InterfaceCaptureGroupName')
  - [LocateForView](#F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-LocateForView 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.LocateForView')
  - [LocateForViewType](#F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-LocateForViewType 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.LocateForViewType')
  - [LocateTypeForViewType](#F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-LocateTypeForViewType 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.LocateTypeForViewType')
  - [NameTransformer](#F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-NameTransformer 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.NameTransformer')
  - [TransformName](#F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-TransformName 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.TransformName')
  - [AddDefaultTypeMapping(viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddDefaultTypeMapping-System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.AddDefaultTypeMapping(System.String)')
  - [AddNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.AddNamespaceMapping(System.String,System.String[],System.String)')
  - [AddNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.AddNamespaceMapping(System.String,System.String,System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.AddSubNamespaceMapping(System.String,System.String[],System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.AddSubNamespaceMapping(System.String,System.String,System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String[],System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.AddTypeMapping(System.String,System.String,System.String[],System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String,System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.AddTypeMapping(System.String,System.String,System.String,System.String)')
  - [ConfigureTypeMappings(config)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.ConfigureTypeMappings(Caliburn.Micro.TypeMappingConfiguration)')
  - [MakeInterface(typeName)](#M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-MakeInterface-System-String- 'Caliburn.Micro.Xamarin.Forms.ViewModelLocator.MakeInterface(System.String)')

<a name='T-Caliburn-Micro-Xamarin-Forms-Action'></a>
## Action `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

A host for action related attached properties.

<a name='F-Caliburn-Micro-Xamarin-Forms-Action-TargetProperty'></a>
### TargetProperty `constants`

##### Summary

A property definition representing the target of an [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') . The DataContext of the element will be set to this instance.

<a name='F-Caliburn-Micro-Xamarin-Forms-Action-TargetWithoutContextProperty'></a>
### TargetWithoutContextProperty `constants`

##### Summary

A property definition representing the target of an [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') . The DataContext of the element is not set to this instance.

<a name='M-Caliburn-Micro-Xamarin-Forms-Action-GetTarget-Xamarin-Forms-BindableObject-'></a>
### GetTarget(d) `method`

##### Summary

Gets the target for instances of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') .

##### Returns

The target for instances of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to which the target is attached. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Action-GetTargetWithoutContext-Xamarin-Forms-BindableObject-'></a>
### GetTargetWithoutContext(d) `method`

##### Summary

Gets the target for instances of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') .

##### Returns

The target for instances of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to which the target is attached. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Action-HasTargetSet-Xamarin-Forms-BindableObject-'></a>
### HasTargetSet(element) `method`

##### Summary

Checks if the [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') -Target was set.

##### Returns

True if Target or TargetWithoutContext was set on `element`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | DependencyObject to check |

<a name='M-Caliburn-Micro-Xamarin-Forms-Action-SetTarget-Xamarin-Forms-BindableObject,System-Object-'></a>
### SetTarget(d,target) `method`

##### Summary

Sets the target of the [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') .

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to attach the target to. |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The target for instances of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') . |

<a name='M-Caliburn-Micro-Xamarin-Forms-Action-SetTargetWithoutContext-Xamarin-Forms-BindableObject,System-Object-'></a>
### SetTargetWithoutContext(d,target) `method`

##### Summary

Sets the target of the [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') .

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to attach the target to. |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The target for instances of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') . |

##### Remarks

The DataContext will not be set.

<a name='T-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext'></a>
## ActionExecutionContext `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

The context used during the execution of an Action or its guard.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-CanExecute'></a>
### CanExecute `constants`

##### Summary

Determines whether the action can execute.

##### Remarks

Returns true if the action can execute, false otherwise.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-EventArgs'></a>
### EventArgs `constants`

##### Summary

Any event arguments associated with the action's invocation.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Method'></a>
### Method `constants`

##### Summary

The actual method info to be invoked.

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Item-System-String-'></a>
### Item `property`

##### Summary

Gets or sets additional data needed to invoke the action.

##### Returns

Custom data associated with the context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The data key. |

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Message'></a>
### Message `property`

##### Summary

The message being executed.

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Source'></a>
### Source `property`

##### Summary

The source from which the message originates.

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Target'></a>
### Target `property`

##### Summary

The instance on which the action is invoked.

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-View'></a>
### View `property`

##### Summary

The view associated with the target.

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext-Dispose'></a>
### Dispose() `method`

##### Summary

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Xamarin-Forms-ActionMessage'></a>
## ActionMessage `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionMessage-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates an instance of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage').

##### Parameters

This constructor has no parameters.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-ApplyAvailabilityEffect'></a>
### ApplyAvailabilityEffect `constants`

##### Summary

Applies an availability effect, such as IsEnabled, to an element.

##### Remarks

Returns a value indicating whether or not the action is available.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-BuildPossibleGuardNames'></a>
### BuildPossibleGuardNames `constants`

##### Summary

Returns the list of possible names of guard methods / properties for the given method.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-EnforceGuardsDuringInvocation'></a>
### EnforceGuardsDuringInvocation `constants`

##### Summary

Causes the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.

##### Remarks

This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-GetTargetMethod'></a>
### GetTargetMethod `constants`

##### Summary

Finds the method on the target matching the specified message.

##### Returns

The matching method, if available.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-InvokeAction'></a>
### InvokeAction `constants`

##### Summary

Invokes the action using the specified [ActionExecutionContext](#T-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext')

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-PrepareContext'></a>
### PrepareContext `constants`

##### Summary

Prepares the action execution context for use.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-SetMethodBinding'></a>
### SetMethodBinding `constants`

##### Summary

Sets the target, method and view on the context. Uses a bubbling strategy by default.

<a name='F-Caliburn-Micro-Xamarin-Forms-ActionMessage-ThrowsExceptions'></a>
### ThrowsExceptions `constants`

##### Summary

Causes the action to throw if it cannot locate the target or the method at invocation time.

##### Remarks

True by default.

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionMessage-Handler'></a>
### Handler `property`

##### Summary

The handler for the action.

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionMessage-MethodName'></a>
### MethodName `property`

##### Summary

Gets or sets the name of the method to be invoked on the presentation model class.

<a name='P-Caliburn-Micro-Xamarin-Forms-ActionMessage-Parameters'></a>
### Parameters `property`

##### Summary

Gets the parameters to pass as part of the method invocation.

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionMessage-Invoke-Xamarin-Forms-VisualElement-'></a>
### Invoke(sender) `method`

##### Summary

Invokes the action.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [Xamarin.Forms.VisualElement](#T-Xamarin-Forms-VisualElement 'Xamarin.Forms.VisualElement') | The Visual Element invoking the event |

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionMessage-OnAttached'></a>
### OnAttached() `method`

##### Summary

Called after the action is attached to an AssociatedObject.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionMessage-OnDetaching'></a>
### OnDetaching() `method`

##### Summary

Called when the action is being detached from its AssociatedObject, but before it has actually occurred.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionMessage-ToString'></a>
### ToString() `method`

##### Summary

Returns a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that represents the current [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object').

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that represents the current [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object').

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionMessage-TryFindGuardMethod-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext,System-Collections-Generic-IEnumerable{System-String}-'></a>
### TryFindGuardMethod(context,possibleGuardNames) `method`

##### Summary

Try to find a candidate for guard function, having:
		- a name in the form "CanXXX"
		- no generic parameters
		- a bool return type
		- no parameters or a set of parameters corresponding to the action method

##### Returns

A MethodInfo, if found; null otherwise

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.Xamarin.Forms.ActionExecutionContext](#T-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext') | The execution context |
| possibleGuardNames | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | Method names to look for. |

<a name='M-Caliburn-Micro-Xamarin-Forms-ActionMessage-UpdateAvailability'></a>
### UpdateAvailability() `method`

##### Summary

Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1'></a>
## AttachedCollection\`1 `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

A collection that can exist as part of a behavior.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of item in the attached collection. |

<a name='P-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

The currently attached object.

<a name='M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-Attach-Xamarin-Forms-BindableObject-'></a>
### Attach(dependencyObject) `method`

##### Summary

Attaches the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The dependency object to attach the collection to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-Detach'></a>
### Detach() `method`

##### Summary

Detaches the collection.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-OnCollectionChanged-System-Collections-Specialized-NotifyCollectionChangedEventArgs-'></a>
### OnCollectionChanged(e) `method`

##### Summary

Raises the [](#E-System-Collections-ObjectModel-ObservableCollection`1-CollectionChanged 'System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged') event with the provided arguments.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| e | [System.Collections.Specialized.NotifyCollectionChangedEventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Specialized.NotifyCollectionChangedEventArgs 'System.Collections.Specialized.NotifyCollectionChangedEventArgs') | Arguments of the event being raised. |

<a name='M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-OnItemAdded-Xamarin-Forms-BindableObject-'></a>
### OnItemAdded(item) `method`

##### Summary

Called when an item is added from the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The item that was added. |

<a name='M-Caliburn-Micro-Xamarin-Forms-AttachedCollection`1-OnItemRemoved-Xamarin-Forms-BindableObject-'></a>
### OnItemRemoved(item) `method`

##### Summary

Called when an item is removed from the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The item that was removed. |

<a name='T-Caliburn-Micro-Xamarin-Forms-Bind'></a>
## Bind `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Hosts dependency properties for binding.

<a name='F-Caliburn-Micro-Xamarin-Forms-Bind-AtDesignTimeProperty'></a>
### AtDesignTimeProperty `constants`

##### Summary

Allows application of conventions at design-time.

<a name='F-Caliburn-Micro-Xamarin-Forms-Bind-ModelProperty'></a>
### ModelProperty `constants`

##### Summary

Allows binding on an existing view. Use this on root UserControls, Pages and Windows; not in a DataTemplate.

<a name='F-Caliburn-Micro-Xamarin-Forms-Bind-ModelWithoutContextProperty'></a>
### ModelWithoutContextProperty `constants`

##### Summary

Allows binding on an existing view without setting the data context. Use this from within a DataTemplate.

<a name='M-Caliburn-Micro-Xamarin-Forms-Bind-GetAtDesignTime-Xamarin-Forms-BindableObject-'></a>
### GetAtDesignTime(dependencyObject) `method`

##### Summary

Gets whether or not conventions are being applied at design-time.

##### Returns

Whether or not conventions are applied.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The ui to apply conventions to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Bind-GetModel-Xamarin-Forms-BindableObject-'></a>
### GetModel(dependencyObject) `method`

##### Summary

Gets the model to bind to.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The dependency object to bind to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Bind-GetModelWithoutContext-Xamarin-Forms-BindableObject-'></a>
### GetModelWithoutContext(dependencyObject) `method`

##### Summary

Gets the model to bind to.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The dependency object to bind to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Bind-SetAtDesignTime-Xamarin-Forms-BindableObject,System-Boolean-'></a>
### SetAtDesignTime(dependencyObject,value) `method`

##### Summary

Sets whether or not do bind conventions at design-time.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The ui to apply conventions to. |
| value | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether or not to apply conventions. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Bind-SetModel-Xamarin-Forms-BindableObject,System-Object-'></a>
### SetModel(dependencyObject,value) `method`

##### Summary

Sets the model to bind to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The dependency object to bind to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Bind-SetModelWithoutContext-Xamarin-Forms-BindableObject,System-Object-'></a>
### SetModelWithoutContext(dependencyObject,value) `method`

##### Summary

Sets the model to bind to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The dependency object to bind to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='T-Caliburn-Micro-Xamarin-Forms-ConventionManager'></a>
## ConventionManager `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Used to configure the conventions used by the framework to apply bindings and create actions.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyBindingMode'></a>
### ApplyBindingMode `constants`

##### Summary

Applies the appropriate binding mode to the binding.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyStringFormat'></a>
### ApplyStringFormat `constants`

##### Summary

Determines whether a custom string format is needed and applies it to the binding.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyUpdateSourceTrigger'></a>
### ApplyUpdateSourceTrigger `constants`

##### Summary

Determines whether a custom update source trigger should be applied to the binding.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyValidation'></a>
### ApplyValidation `constants`

##### Summary

Determines whether or not and what type of validation to enable on the binding.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyValueConverter'></a>
### ApplyValueConverter `constants`

##### Summary

Determines whether a value converter is is needed and applies one to the binding.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ConfigureSelectedItem'></a>
### ConfigureSelectedItem `constants`

##### Summary

Configures the selected item convention.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-ConfigureSelectedItemBinding'></a>
### ConfigureSelectedItemBinding `constants`

##### Summary

Configures the SelectedItem binding for matched selection path.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-DefaultHeaderTemplate'></a>
### DefaultHeaderTemplate `constants`

##### Summary

The default DataTemplate used for Headered controls when required.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-DefaultItemTemplate'></a>
### DefaultItemTemplate `constants`

##### Summary

The default DataTemplate used for ItemsControls when required.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-DerivePotentialSelectionNames'></a>
### DerivePotentialSelectionNames `constants`

##### Summary

Derives the SelectedItem property name.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-IncludeStaticProperties'></a>
### IncludeStaticProperties `constants`

##### Summary

Indicates whether or not static properties should be included during convention name matching.

##### Remarks

False by default.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-OverwriteContent'></a>
### OverwriteContent `constants`

##### Summary

Indicates whether or not the Content of ContentControls should be overwritten by conventional bindings.

##### Remarks

False by default.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-SetBinding'></a>
### SetBinding `constants`

##### Summary

Creates a binding and sets it on the element, applying the appropriate conventions.

<a name='F-Caliburn-Micro-Xamarin-Forms-ConventionManager-Singularize'></a>
### Singularize `constants`

##### Summary

Changes the provided word from a plural form to a singular form.

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-AddElementConvention-Caliburn-Micro-Xamarin-Forms-ElementConvention-'></a>
### AddElementConvention(convention) `method`

##### Summary

Adds an element convention.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| convention | [Caliburn.Micro.Xamarin.Forms.ElementConvention](#T-Caliburn-Micro-Xamarin-Forms-ElementConvention 'Caliburn.Micro.Xamarin.Forms.ElementConvention') |  |

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-AddElementConvention``1-Xamarin-Forms-BindableProperty,System-String,System-String-'></a>
### AddElementConvention\`\`1(bindableProperty,parameterProperty,eventName) `method`

##### Summary

Adds an element convention.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bindableProperty | [Xamarin.Forms.BindableProperty](#T-Xamarin-Forms-BindableProperty 'Xamarin.Forms.BindableProperty') | The default property for binding conventions. |
| parameterProperty | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The default property for action parameters. |
| eventName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The default event to trigger actions. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of element. |

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyHeaderTemplate-Xamarin-Forms-VisualElement,Xamarin-Forms-BindableProperty,Xamarin-Forms-BindableProperty,System-Type-'></a>
### ApplyHeaderTemplate(element,headerTemplateProperty,headerTemplateSelectorProperty,viewModelType) `method`

##### Summary

Applies a header template based on [IHaveDisplayName](#T-Caliburn-Micro-IHaveDisplayName 'Caliburn.Micro.IHaveDisplayName')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Xamarin.Forms.VisualElement](#T-Xamarin-Forms-VisualElement 'Xamarin.Forms.VisualElement') | The element to apply the header template to. |
| headerTemplateProperty | [Xamarin.Forms.BindableProperty](#T-Xamarin-Forms-BindableProperty 'Xamarin.Forms.BindableProperty') | The depdendency property for the hdeader. |
| headerTemplateSelectorProperty | [Xamarin.Forms.BindableProperty](#T-Xamarin-Forms-BindableProperty 'Xamarin.Forms.BindableProperty') | The selector dependency property. |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the view model. |

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-ApplyItemTemplate``1-Xamarin-Forms-ItemsView{``0},System-Reflection-PropertyInfo-'></a>
### ApplyItemTemplate\`\`1(itemsControl,property) `method`

##### Summary

Attempts to apply the default item template to the items control.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| itemsControl | [Xamarin.Forms.ItemsView{\`\`0}](#T-Xamarin-Forms-ItemsView{``0} 'Xamarin.Forms.ItemsView{``0}') | The items control. |
| property | [System.Reflection.PropertyInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.PropertyInfo 'System.Reflection.PropertyInfo') | The collection property. |

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-GetElementConvention-System-Type-'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-GetPropertyCaseInsensitive-System-Type,System-String-'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-HasBinding-Xamarin-Forms-VisualElement,Xamarin-Forms-BindableProperty-'></a>
### HasBinding() `method`

##### Summary

Determines whether a particular dependency property already has a binding on the provided element.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-SetBindingWithoutBindingOrValueOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-ElementConvention,Xamarin-Forms-BindableProperty-'></a>
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
| element | [Xamarin.Forms.VisualElement](#T-Xamarin-Forms-VisualElement 'Xamarin.Forms.VisualElement') |  |
| convention | [Caliburn.Micro.Xamarin.Forms.ElementConvention](#T-Caliburn-Micro-Xamarin-Forms-ElementConvention 'Caliburn.Micro.Xamarin.Forms.ElementConvention') |  |
| bindableProperty | [Xamarin.Forms.BindableProperty](#T-Xamarin-Forms-BindableProperty 'Xamarin.Forms.BindableProperty') |  |

<a name='M-Caliburn-Micro-Xamarin-Forms-ConventionManager-SetBindingWithoutBindingOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-ElementConvention,Xamarin-Forms-BindableProperty-'></a>
### SetBindingWithoutBindingOverwrite() `method`

##### Summary

Creates a binding and sets it on the element, guarding against pre-existing bindings.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs'></a>
## DependencyPropertyChangedEventArgs `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Provides data for a PropertyChangedCallback implementation that is invoked when a dependency property changes its value. Also provides event data for the Control.IsEnabledChanged event and any other event that uses the DependencyPropertyChangedEventHandler delegate.

<a name='M-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-#ctor-System-Object,System-Object,Xamarin-Forms-BindableProperty-'></a>
### #ctor(newValue,oldValue,property) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| oldValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| property | [Xamarin.Forms.BindableProperty](#T-Xamarin-Forms-BindableProperty 'Xamarin.Forms.BindableProperty') |  |

<a name='P-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-NewValue'></a>
### NewValue `property`

##### Summary

Gets the value of the dependency property after the reported change.

##### Returns

The dependency property value after the change.

<a name='P-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-OldValue'></a>
### OldValue `property`

##### Summary

Gets the value of the dependency property before the reported change.

##### Returns

The dependency property value before the change.

<a name='P-Caliburn-Micro-Xamarin-Forms-DependencyPropertyChangedEventArgs-Property'></a>
### Property `property`

##### Summary

Gets the identifier for the dependency property where the value change occurred.

##### Returns

The identifier field of the dependency property where the value change occurred.

<a name='T-Caliburn-Micro-Xamarin-Forms-DependencyPropertyHelper'></a>
## DependencyPropertyHelper `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Class that abstracts the differences in creating a DepedencyProperty / BindableProperty on the different platforms.

<a name='M-Caliburn-Micro-Xamarin-Forms-DependencyPropertyHelper-Register-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback-'></a>
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
| propertyChangedCallback | [Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback](#T-Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback 'Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback') | Callback to executed on property changed |

<a name='M-Caliburn-Micro-Xamarin-Forms-DependencyPropertyHelper-RegisterAttached-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback-'></a>
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
| propertyChangedCallback | [Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback](#T-Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback 'Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback') | Callback to executed on property changed |

<a name='T-Caliburn-Micro-Xamarin-Forms-ElementConvention'></a>
## ElementConvention `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Represents the conventions for a particular element type.

<a name='F-Caliburn-Micro-Xamarin-Forms-ElementConvention-ApplyBinding'></a>
### ApplyBinding `constants`

##### Summary

Applies custom conventions for elements of this type.

##### Remarks

Pass the view model type, property path, property instance, framework element and its convention.

<a name='F-Caliburn-Micro-Xamarin-Forms-ElementConvention-CreateTrigger'></a>
### CreateTrigger `constants`

##### Summary

The default trigger to be used when wiring actions on this element.

<a name='F-Caliburn-Micro-Xamarin-Forms-ElementConvention-ElementType'></a>
### ElementType `constants`

##### Summary

The type of element to which the conventions apply.

<a name='F-Caliburn-Micro-Xamarin-Forms-ElementConvention-GetBindableProperty'></a>
### GetBindableProperty `constants`

##### Summary

Gets the default property to be used in binding conventions.

<a name='F-Caliburn-Micro-Xamarin-Forms-ElementConvention-ParameterProperty'></a>
### ParameterProperty `constants`

##### Summary

The default property to be used for parameters of this type in actions.

<a name='T-Caliburn-Micro-Xamarin-Forms-FormsApplication'></a>
## FormsApplication `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

A slimmed down version of the normal Caliburn Application for Xamarin Forms, used to register the navigation service and set up the initial view.

<a name='P-Caliburn-Micro-Xamarin-Forms-FormsApplication-RootNavigationPage'></a>
### RootNavigationPage `property`

##### Summary

The root frame of the application.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-CreateApplicationPage'></a>
### CreateApplicationPage() `method`

##### Summary

Creates the root frame used by the application.

##### Returns

The frame.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootView-System-Type-'></a>
### DisplayRootView(viewType) `method`

##### Summary

Creates the root frame and navigates to the specified view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The view type to navigate to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootViewForAsync-System-Type-'></a>
### DisplayRootViewForAsync(viewModelType) `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The view model type. |

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootViewForAsync``1'></a>
### DisplayRootViewForAsync\`\`1() `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view model type. |

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-DisplayRootView``1'></a>
### DisplayRootView\`\`1() `method`

##### Summary

Creates the root frame and navigates to the specified view.

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view type to navigate to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-Initialize'></a>
### Initialize() `method`

##### Summary

Start the framework.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-PrepareViewFirst'></a>
### PrepareViewFirst() `method`

##### Summary

Allows you to trigger the creation of the RootFrame from Configure if necessary.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsApplication-PrepareViewFirst-Xamarin-Forms-NavigationPage-'></a>
### PrepareViewFirst(navigationPage) `method`

##### Summary

Override this to register a navigation service.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationPage | [Xamarin.Forms.NavigationPage](#T-Xamarin-Forms-NavigationPage 'Xamarin.Forms.NavigationPage') | The root frame of the application. |

<a name='T-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider'></a>
## FormsPlatformProvider `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

A [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider') implementation for the Xamarin.Forms platfrom.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-#ctor-Caliburn-Micro-IPlatformProvider-'></a>
### #ctor(platformProvider) `constructor`

##### Summary

Creates an instance of [FormsPlatformProvider](#T-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider 'Caliburn.Micro.Xamarin.Forms.FormsPlatformProvider').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| platformProvider | [Caliburn.Micro.IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider') | The existing platform provider (from the host platform) to encapsulate |

<a name='P-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-InDesignMode'></a>
### InDesignMode `property`

##### Summary

*Inherit from parent.*

<a name='P-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-PropertyChangeNotificationsOnUIThread'></a>
### PropertyChangeNotificationsOnUIThread `property`

##### Summary

Whether or not classes should execute property change notications on the UI thread.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-BeginOnUIThread-System-Action-'></a>
### BeginOnUIThread() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnFirstLoad() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnLayoutUpdated() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-GetFirstNonGeneratedView-System-Object-'></a>
### GetFirstNonGeneratedView() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}-'></a>
### GetViewCloseAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-OnUIThread-System-Action-'></a>
### OnUIThread() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-FormsPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}-'></a>
### OnUIThreadAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Xamarin-Forms-IAttachedObject'></a>
## IAttachedObject `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Interaface usually from the Interactivity SDK's included here for completeness.

<a name='P-Caliburn-Micro-Xamarin-Forms-IAttachedObject-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

The currently attached object.

<a name='M-Caliburn-Micro-Xamarin-Forms-IAttachedObject-Attach-Xamarin-Forms-BindableObject-'></a>
### Attach(dependencyObject) `method`

##### Summary

Attached the specified dependency object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') |  |

<a name='M-Caliburn-Micro-Xamarin-Forms-IAttachedObject-Detach'></a>
### Detach() `method`

##### Summary

Detach from the previously attached object.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Xamarin-Forms-IHaveParameters'></a>
## IHaveParameters `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Indicates that a message is parameterized.

<a name='P-Caliburn-Micro-Xamarin-Forms-IHaveParameters-Parameters'></a>
### Parameters `property`

##### Summary

Represents the parameters of a message.

<a name='T-Caliburn-Micro-Xamarin-Forms-INavigationService'></a>
## INavigationService `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Implemented by services that provide view and view model based navigation

<a name='M-Caliburn-Micro-Xamarin-Forms-INavigationService-GoBackAsync-System-Boolean-'></a>
### GoBackAsync(animated) `method`

##### Summary

Asynchronously removes the top [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') from the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-INavigationService-GoBackToRootAsync-System-Boolean-'></a>
### GoBackToRootAsync(animated) `method`

##### Summary

Pops all but the root [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') off the navigation stack.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewAsync-System-Type,System-Object,System-Boolean-'></a>
### NavigateToViewAsync(viewType,parameter,animated) `method`

##### Summary

A task for asynchronously pushing a view onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the view |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewAsync``1-System-Object,System-Boolean-'></a>
### NavigateToViewAsync\`\`1(parameter,animated) `method`

##### Summary

A task for asynchronously pushing a view onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the view |

<a name='M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean-'></a>
### NavigateToViewModelAsync(viewModelType,parameter,animated) `method`

##### Summary

A task for asynchronously pushing a view for the given view model onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the view model |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-INavigationService-NavigateToViewModelAsync``1-System-Object,System-Boolean-'></a>
### NavigateToViewModelAsync\`\`1(parameter,animated) `method`

##### Summary

A task for asynchronously pushing a page for the given view model onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the view model |

<a name='T-Caliburn-Micro-Xamarin-Forms-Message'></a>
## Message `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Host's attached properties related to routed UI messaging.

<a name='F-Caliburn-Micro-Xamarin-Forms-Message-AttachProperty'></a>
### AttachProperty `constants`

##### Summary

A property definition representing attached triggers and messages.

<a name='M-Caliburn-Micro-Xamarin-Forms-Message-GetAttach-Xamarin-Forms-BindableObject-'></a>
### GetAttach(d) `method`

##### Summary

Gets the attached triggers and messages.

##### Returns

The parsable attachment text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element that was attached to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Message-GetHandler-Xamarin-Forms-BindableObject-'></a>
### GetHandler(d) `method`

##### Summary

Gets the message handler for this element.

##### Returns

The message handler.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Message-SetAttach-Xamarin-Forms-BindableObject,System-String-'></a>
### SetAttach(d,attachText) `method`

##### Summary

Sets the attached triggers and messages.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to attach to. |
| attachText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The parsable attachment text. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Message-SetHandler-Xamarin-Forms-BindableObject,System-Object-'></a>
### SetHandler(d,value) `method`

##### Summary

Places a message handler on this element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message handler. |

<a name='T-Caliburn-Micro-Xamarin-Forms-MessageBinder'></a>
## MessageBinder `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

A service that is capable of properly binding values to a method's parameters and creating instances of [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult').

<a name='F-Caliburn-Micro-Xamarin-Forms-MessageBinder-CustomConverters'></a>
### CustomConverters `constants`

##### Summary

Custom converters used by the framework registered by destination type for which they will be selected.
The converter is passed the existing value to convert and a "context" object.

<a name='F-Caliburn-Micro-Xamarin-Forms-MessageBinder-EvaluateParameter'></a>
### EvaluateParameter `constants`

##### Summary

Transforms the textual parameter into the actual parameter.

<a name='F-Caliburn-Micro-Xamarin-Forms-MessageBinder-SpecialValues'></a>
### SpecialValues `constants`

##### Summary

The special parameter values recognized by the message binder along with their resolvers.
Parameter names are case insensitive so the specified names are unique and can be used with different case variations

<a name='M-Caliburn-Micro-Xamarin-Forms-MessageBinder-CoerceValue-System-Type,System-Object,System-Object-'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-MessageBinder-DetermineParameters-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext,System-Reflection-ParameterInfo[]-'></a>
### DetermineParameters(context,requiredParameters) `method`

##### Summary

Determines the parameters that a method should be invoked with.

##### Returns

The actual parameter values.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.Xamarin.Forms.ActionExecutionContext](#T-Caliburn-Micro-Xamarin-Forms-ActionExecutionContext 'Caliburn.Micro.Xamarin.Forms.ActionExecutionContext') | The action execution context. |
| requiredParameters | [System.Reflection.ParameterInfo[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.ParameterInfo[] 'System.Reflection.ParameterInfo[]') | The parameters required to complete the invocation. |

<a name='M-Caliburn-Micro-Xamarin-Forms-MessageBinder-GetDefaultValue-System-Type-'></a>
### GetDefaultValue(type) `method`

##### Summary

Gets the default value for a type.

##### Returns

The default value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type. |

<a name='T-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1'></a>
## NavigateHelper\`1 `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Builds a Uri in a strongly typed fashion, based on a ViewModel.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TViewModel |  |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1-AttachTo-Caliburn-Micro-Xamarin-Forms-INavigationService-'></a>
### AttachTo(navigationService) `method`

##### Summary

Attaches a navigation servies to this builder.

##### Returns

Itself

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.Xamarin.Forms.INavigationService](#T-Caliburn-Micro-Xamarin-Forms-INavigationService 'Caliburn.Micro.Xamarin.Forms.INavigationService') | The navigation service. |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1-Navigate-System-Boolean-'></a>
### Navigate() `method`

##### Summary

Navigates to the Uri represented by this builder.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigateHelper`1-WithParam``1-System-Linq-Expressions-Expression{System-Func{`0,``0}},``0-'></a>
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

<a name='T-Caliburn-Micro-Xamarin-Forms-NavigationExtensions'></a>
## NavigationExtensions `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Extension methods for [INavigationService](#T-Caliburn-Micro-Xamarin-Forms-INavigationService 'Caliburn.Micro.Xamarin.Forms.INavigationService')

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationExtensions-For``1-Caliburn-Micro-Xamarin-Forms-INavigationService-'></a>
### For\`\`1(navigationService) `method`

##### Summary

Creates a Uri builder based on a view model type.

##### Returns

The builder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.Xamarin.Forms.INavigationService](#T-Caliburn-Micro-Xamarin-Forms-INavigationService 'Caliburn.Micro.Xamarin.Forms.INavigationService') | The navigation service. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TViewModel | The type of the view model. |

<a name='T-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter'></a>
## NavigationPageAdapter `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Adapater around NavigationPage that implements INavigationService

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-#ctor-Xamarin-Forms-NavigationPage-'></a>
### #ctor(navigationPage) `constructor`

##### Summary

Instantiates new instance of NavigationPageAdapter

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationPage | [Xamarin.Forms.NavigationPage](#T-Xamarin-Forms-NavigationPage 'Xamarin.Forms.NavigationPage') | The navigation page to adapat |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-ActivateViewAsync-Xamarin-Forms-BindableObject-'></a>
### ActivateViewAsync(view) `method`

##### Summary

Apply logic to activate a view when it is popped onto the navigation stack

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | the view to activate |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-CreateContentPage-Xamarin-Forms-ContentView,System-Object-'></a>
### CreateContentPage(view,viewModel) `method`

##### Summary

Allow Xamarin to navigate to a ViewModel backed by a view which is of type [ContentView](#T-Xamarin-Forms-ContentView 'Xamarin.Forms.ContentView') by adapting the result
to a [ContentPage](#T-Xamarin-Forms-ContentPage 'Xamarin.Forms.ContentPage').

##### Returns

The adapted ContentPage

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Xamarin.Forms.ContentView](#T-Xamarin-Forms-ContentView 'Xamarin.Forms.ContentView') | The view to be adapted |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model which is bound to the view |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-DeactivateViewAsync-Xamarin-Forms-BindableObject-'></a>
### DeactivateViewAsync(view) `method`

##### Summary

Apply logic to deactivate the active view when it is popped off the navigation stack

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | the previously active view |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-GoBackAsync-System-Boolean-'></a>
### GoBackAsync(animated) `method`

##### Summary

Asynchronously removes the top [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') from the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-GoBackToRootAsync-System-Boolean-'></a>
### GoBackToRootAsync(animated) `method`

##### Summary

Pops all but the root [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') off the navigation stack.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewAsync-System-Type,System-Object,System-Boolean-'></a>
### NavigateToViewAsync(viewType,parameter,animated) `method`

##### Summary

A task for asynchronously pushing a view onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the view |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewAsync``1-System-Object,System-Boolean-'></a>
### NavigateToViewAsync\`\`1(parameter,animated) `method`

##### Summary

A task for asynchronously pushing a view onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the view |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean-'></a>
### NavigateToViewModelAsync(viewModelType,parameter,animated) `method`

##### Summary

A task for asynchronously pushing a view for the given view model onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the view model |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-NavigateToViewModelAsync``1-System-Object,System-Boolean-'></a>
### NavigateToViewModelAsync\`\`1(parameter,animated) `method`

##### Summary

A task for asynchronously pushing a page for the given view model onto the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The paramter to pass to the view model |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the view model |

<a name='M-Caliburn-Micro-Xamarin-Forms-NavigationPageAdapter-TryInjectParameters-System-Object,System-Object-'></a>
### TryInjectParameters(viewModel,parameter) `method`

##### Summary

Attempts to inject query string parameters from the view into the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The parameter. |

<a name='T-Caliburn-Micro-Xamarin-Forms-Parameter'></a>
## Parameter `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Represents a parameter of an [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage').

<a name='F-Caliburn-Micro-Xamarin-Forms-Parameter-ValueProperty'></a>
### ValueProperty `constants`

##### Summary

A dependency property representing the parameter's value.

<a name='P-Caliburn-Micro-Xamarin-Forms-Parameter-Owner'></a>
### Owner `property`

##### Summary

Gets or sets the owner.

<a name='P-Caliburn-Micro-Xamarin-Forms-Parameter-Value'></a>
### Value `property`

##### Summary

Gets or sets the value of the parameter.

<a name='M-Caliburn-Micro-Xamarin-Forms-Parameter-MakeAwareOf-Caliburn-Micro-Xamarin-Forms-ActionMessage-'></a>
### MakeAwareOf(owner) `method`

##### Summary

Makes the parameter aware of the [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') that it's attached to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| owner | [Caliburn.Micro.Xamarin.Forms.ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') | The action message. |

<a name='T-Caliburn-Micro-Xamarin-Forms-Parser'></a>
## Parser `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Parses text into a fully functional set of [TriggerBase](#T-Xamarin-Forms-TriggerBase 'Xamarin.Forms.TriggerBase') instances with [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage').

<a name='F-Caliburn-Micro-Xamarin-Forms-Parser-CreateParameter'></a>
### CreateParameter `constants`

##### Summary

Function used to parse a string identified as a message parameter.

<a name='F-Caliburn-Micro-Xamarin-Forms-Parser-CreateTrigger'></a>
### CreateTrigger `constants`

##### Summary

The function used to generate a trigger.

##### Remarks

The parameters passed to the method are the the target of the trigger and string representing the trigger.

<a name='F-Caliburn-Micro-Xamarin-Forms-Parser-InterpretMessageText'></a>
### InterpretMessageText `constants`

##### Summary

Function used to parse a string identified as a message.

<a name='M-Caliburn-Micro-Xamarin-Forms-Parser-BindParameter-Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-Parameter,System-String,System-String,Xamarin-Forms-BindingMode-'></a>
### BindParameter(target,parameter,elementName,path,bindingMode) `method`

##### Summary

Creates a binding on a [Parameter](#T-Caliburn-Micro-Xamarin-Forms-Parameter 'Caliburn.Micro.Xamarin.Forms.Parameter').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Xamarin.Forms.VisualElement](#T-Xamarin-Forms-VisualElement 'Xamarin.Forms.VisualElement') | The target to which the message is applied. |
| parameter | [Caliburn.Micro.Xamarin.Forms.Parameter](#T-Caliburn-Micro-Xamarin-Forms-Parameter 'Caliburn.Micro.Xamarin.Forms.Parameter') | The parameter object. |
| elementName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the element to bind to. |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path of the element to bind to. |
| bindingMode | [Xamarin.Forms.BindingMode](#T-Xamarin-Forms-BindingMode 'Xamarin.Forms.BindingMode') | The binding mode to use. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Parser-CreateMessage-Xamarin-Forms-BindableObject,System-String-'></a>
### CreateMessage(target,messageText) `method`

##### Summary

Creates an instance of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') by parsing out the textual dsl.

##### Returns

The created message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The target of the message. |
| messageText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The textual message dsl. |

<a name='M-Caliburn-Micro-Xamarin-Forms-Parser-Parse-Xamarin-Forms-BindableObject,System-String-'></a>
### Parse(target,text) `method`

##### Summary

Parses the specified message text.

##### Returns

The triggers parsed from the text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The target. |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message text. |

<a name='T-Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback'></a>
## PropertyChangedCallback `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Helper type for abstracting differences between dependency / bindable properties.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [T:Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback](#T-T-Caliburn-Micro-Xamarin-Forms-PropertyChangedCallback 'T:Caliburn.Micro.Xamarin.Forms.PropertyChangedCallback') | The dependency object |

<a name='T-Caliburn-Micro-Xamarin-Forms-RoutedEventArgs'></a>
## RoutedEventArgs `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Helper class with abstracting Xamarin Forms.

<a name='P-Caliburn-Micro-Xamarin-Forms-RoutedEventArgs-OriginalSource'></a>
### OriginalSource `property`

##### Summary

Source of the event

<a name='T-Caliburn-Micro-Xamarin-Forms-RoutedEventHandler'></a>
## RoutedEventHandler `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Helper class with abstracting Xamarin Forms.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [T:Caliburn.Micro.Xamarin.Forms.RoutedEventHandler](#T-T-Caliburn-Micro-Xamarin-Forms-RoutedEventHandler 'T:Caliburn.Micro.Xamarin.Forms.RoutedEventHandler') | The sender of the event |

<a name='T-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1'></a>
## TriggerActionBase\`1 `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Helper class to try and abtract the differences in TriggerAction across platforms

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='P-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

Gets or sets the object to which this [TriggerActionBase\`1](#T-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1 'Caliburn.Micro.Xamarin.Forms.TriggerActionBase`1') is attached.

<a name='M-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1-OnAttached'></a>
### OnAttached() `method`

##### Summary

Called after the action is attached to an AssociatedObject.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Xamarin-Forms-TriggerActionBase`1-OnDetaching'></a>
### OnDetaching() `method`

##### Summary

Called when the action is being detached from its AssociatedObject, but before it has actually occurred.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Xamarin-Forms-View'></a>
## View `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Hosts attached properties related to view models.

<a name='F-Caliburn-Micro-Xamarin-Forms-View-ApplyConventionsProperty'></a>
### ApplyConventionsProperty `constants`

##### Summary

A dependency property which allows the override of convention application behavior.

<a name='F-Caliburn-Micro-Xamarin-Forms-View-ContextProperty'></a>
### ContextProperty `constants`

##### Summary

A dependency property for assigning a context to a particular portion of the UI.

<a name='F-Caliburn-Micro-Xamarin-Forms-View-GetFirstNonGeneratedView'></a>
### GetFirstNonGeneratedView `constants`

##### Summary

Used to retrieve the root, non-framework-created view.

##### Remarks

In certain instances the services create UI elements.
For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.

<a name='F-Caliburn-Micro-Xamarin-Forms-View-IsGeneratedProperty'></a>
### IsGeneratedProperty `constants`

##### Summary

Used by the framework to indicate that this element was generated.

<a name='F-Caliburn-Micro-Xamarin-Forms-View-IsLoadedProperty'></a>
### IsLoadedProperty `constants`

##### Summary

A dependency property which allows the framework to track whether a certain element has already been loaded in certain scenarios.

<a name='F-Caliburn-Micro-Xamarin-Forms-View-IsScopeRootProperty'></a>
### IsScopeRootProperty `constants`

##### Summary

A dependency property which marks an element as a name scope root.

<a name='F-Caliburn-Micro-Xamarin-Forms-View-ModelProperty'></a>
### ModelProperty `constants`

##### Summary

A dependency property for attaching a model to the UI.

<a name='P-Caliburn-Micro-Xamarin-Forms-View-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Gets a value that indicates whether the process is running in design mode.

<a name='M-Caliburn-Micro-Xamarin-Forms-View-ExecuteOnLoad-Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-RoutedEventHandler-'></a>
### ExecuteOnLoad(element,handler) `method`

##### Summary

Executes the handler immediately if the element is loaded, otherwise wires it to the Loaded event.

##### Returns

true if the handler was executed immediately; false otherwise

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Xamarin.Forms.VisualElement](#T-Xamarin-Forms-VisualElement 'Xamarin.Forms.VisualElement') | The element. |
| handler | [Caliburn.Micro.Xamarin.Forms.RoutedEventHandler](#T-Caliburn-Micro-Xamarin-Forms-RoutedEventHandler 'Caliburn.Micro.Xamarin.Forms.RoutedEventHandler') | The handler. |

<a name='M-Caliburn-Micro-Xamarin-Forms-View-ExecuteOnUnload-Xamarin-Forms-VisualElement,Caliburn-Micro-Xamarin-Forms-RoutedEventHandler-'></a>
### ExecuteOnUnload(element,handler) `method`

##### Summary

Executes the handler when the element is unloaded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Xamarin.Forms.VisualElement](#T-Xamarin-Forms-VisualElement 'Xamarin.Forms.VisualElement') | The element. |
| handler | [Caliburn.Micro.Xamarin.Forms.RoutedEventHandler](#T-Caliburn-Micro-Xamarin-Forms-RoutedEventHandler 'Caliburn.Micro.Xamarin.Forms.RoutedEventHandler') | The handler. |

<a name='M-Caliburn-Micro-Xamarin-Forms-View-GetApplyConventions-Xamarin-Forms-BindableObject-'></a>
### GetApplyConventions(d) `method`

##### Summary

Gets the convention application behavior.

##### Returns

Whether or not to apply conventions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element the property is attached to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-View-GetContext-Xamarin-Forms-BindableObject-'></a>
### GetContext(d) `method`

##### Summary

Gets the context.

##### Returns

The context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element the context is attached to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-View-GetModel-Xamarin-Forms-BindableObject-'></a>
### GetModel(d) `method`

##### Summary

Gets the model.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element the model is attached to. |

<a name='M-Caliburn-Micro-Xamarin-Forms-View-SetApplyConventions-Xamarin-Forms-BindableObject,System-Nullable{System-Boolean}-'></a>
### SetApplyConventions(d,value) `method`

##### Summary

Sets the convention application behavior.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to attach the property to. |
| value | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Whether or not to apply conventions. |

<a name='M-Caliburn-Micro-Xamarin-Forms-View-SetContext-Xamarin-Forms-BindableObject,System-Object-'></a>
### SetContext(d,value) `method`

##### Summary

Sets the context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to attach the context to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The context. |

<a name='M-Caliburn-Micro-Xamarin-Forms-View-SetModel-Xamarin-Forms-BindableObject,System-Object-'></a>
### SetModel(d,value) `method`

##### Summary

Sets the model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Xamarin.Forms.BindableObject](#T-Xamarin-Forms-BindableObject 'Xamarin.Forms.BindableObject') | The element to attach the model to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='T-Caliburn-Micro-Xamarin-Forms-ViewLocator'></a>
## ViewLocator `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

A strategy for determining which view to use for a given model.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-ContextSeparator'></a>
### ContextSeparator `constants`

##### Summary

Separator used when resolving View names for context instances.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-DeterminePackUriFromType'></a>
### DeterminePackUriFromType `constants`

##### Summary

Transforms a view type into a pack uri.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-GetOrCreateViewType'></a>
### GetOrCreateViewType `constants`

##### Summary

Retrieves the view from the IoC container or tries to create it if not found.

##### Remarks

Pass the type of view as a parameter and recieve an instance of the view.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-LocateForModel'></a>
### LocateForModel `constants`

##### Summary

Locates the view for the specified model instance.

##### Returns

The view.

##### Remarks

Pass the model instance, display location (or null) and the context (or null) as parameters and receive a view instance.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-LocateForModelType'></a>
### LocateForModelType `constants`

##### Summary

Locates the view for the specified model type.

##### Returns

The view.

##### Remarks

Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view instance.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-LocateTypeForModelType'></a>
### LocateTypeForModelType `constants`

##### Summary

Locates the view type based on the specified model type.

##### Returns

The view.

##### Remarks

Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-ModifyModelTypeAtDesignTime'></a>
### ModifyModelTypeAtDesignTime `constants`

##### Summary

Modifies the name of the type to be used at design time.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-NameTransformer'></a>
### NameTransformer `constants`

##### Summary

Used to transform names.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewLocator-TransformName'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddDefaultTypeMapping-System-String-'></a>
### AddDefaultTypeMapping(viewSuffix) `method`

##### Summary

Adds a default type mapping using the standard namespace mapping convention

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Namespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddNamespaceMapping-System-String,System-String,System-String-'></a>
### AddNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddSubNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Subnamespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddSubNamespaceMapping-System-String,System-String,System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddTypeMapping-System-String,System-String,System-String[],System-String-'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-AddTypeMapping-System-String,System-String,System-String,System-String-'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration-'></a>
### ConfigureTypeMappings(config) `method`

##### Summary

Specifies how type mappings are created, including default type mappings. Calling this method will
clear all existing name transformation rules and create new default type mappings according to the
configuration.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [Caliburn.Micro.TypeMappingConfiguration](#T-Caliburn-Micro-TypeMappingConfiguration 'Caliburn.Micro.TypeMappingConfiguration') | An instance of TypeMappingConfiguration that provides the settings for configuration |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-InitializeComponent-System-Object-'></a>
### InitializeComponent(element) `method`

##### Summary

When a view does not contain a code-behind file, we need to automatically call InitializeCompoent.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The element to initialize |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewLocator-RegisterViewSuffix-System-String-'></a>
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

<a name='T-Caliburn-Micro-Xamarin-Forms-ViewModelBinder'></a>
## ViewModelBinder `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

Binds a view to a view model.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-ApplyConventionsByDefault'></a>
### ApplyConventionsByDefault `constants`

##### Summary

Gets or sets a value indicating whether to apply conventions by default.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-Bind'></a>
### Bind `constants`

##### Summary

Binds the specified viewModel to the view.

##### Remarks

Passes the the view model, view and creation context (or null for default) to use in applying binding.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-BindActions'></a>
### BindActions `constants`

##### Summary

Attaches instances of [ActionMessage](#T-Caliburn-Micro-Xamarin-Forms-ActionMessage 'Caliburn.Micro.Xamarin.Forms.ActionMessage') to the view's controls based on the provided methods.

##### Remarks

Parameters include the named elements to search through and the type of view model to determine conventions for. Returns unmatched elements.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-BindProperties'></a>
### BindProperties `constants`

##### Summary

Creates data bindings on the view's controls based on the provided properties.

##### Remarks

Parameters include named Elements to search through and the type of view model to determine conventions for. Returns unmatched elements.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-ConventionsAppliedProperty'></a>
### ConventionsAppliedProperty `constants`

##### Summary

Indicates whether or not the conventions have already been applied to the view.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-HandleUnmatchedElements'></a>
### HandleUnmatchedElements `constants`

##### Summary

Allows the developer to add custom handling of named elements which were not matched by any default conventions.

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelBinder-ShouldApplyConventions-Xamarin-Forms-VisualElement-'></a>
### ShouldApplyConventions(view) `method`

##### Summary

Determines whether a view should have conventions applied to it.

##### Returns

Whether or not conventions should be applied to the view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Xamarin.Forms.VisualElement](#T-Xamarin-Forms-VisualElement 'Xamarin.Forms.VisualElement') | The view to check. |

<a name='T-Caliburn-Micro-Xamarin-Forms-ViewModelLocator'></a>
## ViewModelLocator `type`

##### Namespace

Caliburn.Micro.Xamarin.Forms

##### Summary

A strategy for determining which view model to use for a given view.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-InterfaceCaptureGroupName'></a>
### InterfaceCaptureGroupName `constants`

##### Summary

The name of the capture group used as a marker for rules that return interface types

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-LocateForView'></a>
### LocateForView `constants`

##### Summary

Locates the view model for the specified view instance.

##### Returns

The view model.

##### Remarks

Pass the view instance as a parameters and receive a view model instance.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-LocateForViewType'></a>
### LocateForViewType `constants`

##### Summary

Locates the view model for the specified view type.

##### Returns

The view model.

##### Remarks

Pass the view type as a parameter and receive a view model instance.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-LocateTypeForViewType'></a>
### LocateTypeForViewType `constants`

##### Summary

Determines the view model type based on the specified view type.

##### Returns

The view model type.

##### Remarks

Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-NameTransformer'></a>
### NameTransformer `constants`

##### Summary

Used to transform names.

<a name='F-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-TransformName'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddDefaultTypeMapping-System-String-'></a>
### AddDefaultTypeMapping(viewSuffix) `method`

##### Summary

Adds a default type mapping using the standard namespace mapping convention

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Namespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddNamespaceMapping-System-String,System-String,System-String-'></a>
### AddNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Subnamespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String,System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String[],System-String-'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String,System-String-'></a>
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

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration-'></a>
### ConfigureTypeMappings(config) `method`

##### Summary

Specifies how type mappings are created, including default type mappings. Calling this method will
clear all existing name transformation rules and create new default type mappings according to the
configuration.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [Caliburn.Micro.TypeMappingConfiguration](#T-Caliburn-Micro-TypeMappingConfiguration 'Caliburn.Micro.TypeMappingConfiguration') | An instance of TypeMappingConfiguration that provides the settings for configuration |

<a name='M-Caliburn-Micro-Xamarin-Forms-ViewModelLocator-MakeInterface-System-String-'></a>
### MakeInterface(typeName) `method`

##### Summary

Makes a type name into an interface name.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| typeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The part. |
