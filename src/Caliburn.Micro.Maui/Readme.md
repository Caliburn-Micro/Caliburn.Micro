<a name='assembly'></a>
# Caliburn.Micro.Maui

## Contents

- [Action](#T-Caliburn-Micro-Maui-Action 'Caliburn.Micro.Maui.Action')
  - [TargetProperty](#F-Caliburn-Micro-Maui-Action-TargetProperty 'Caliburn.Micro.Maui.Action.TargetProperty')
  - [TargetWithoutContextProperty](#F-Caliburn-Micro-Maui-Action-TargetWithoutContextProperty 'Caliburn.Micro.Maui.Action.TargetWithoutContextProperty')
  - [GetTarget(d)](#M-Caliburn-Micro-Maui-Action-GetTarget-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Action.GetTarget(Microsoft.Maui.Controls.BindableObject)')
  - [GetTargetWithoutContext(d)](#M-Caliburn-Micro-Maui-Action-GetTargetWithoutContext-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Action.GetTargetWithoutContext(Microsoft.Maui.Controls.BindableObject)')
  - [HasTargetSet(element)](#M-Caliburn-Micro-Maui-Action-HasTargetSet-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Action.HasTargetSet(Microsoft.Maui.Controls.BindableObject)')
  - [Invoke(target,methodName,view,source,eventArgs,parameters)](#M-Caliburn-Micro-Maui-Action-Invoke-System-Object,System-String,Microsoft-Maui-Controls-BindableObject,Microsoft-Maui-Controls-VisualElement,System-Object,System-Object[]- 'Caliburn.Micro.Maui.Action.Invoke(System.Object,System.String,Microsoft.Maui.Controls.BindableObject,Microsoft.Maui.Controls.VisualElement,System.Object,System.Object[])')
  - [SetTarget(d,target)](#M-Caliburn-Micro-Maui-Action-SetTarget-Microsoft-Maui-Controls-BindableObject,System-Object- 'Caliburn.Micro.Maui.Action.SetTarget(Microsoft.Maui.Controls.BindableObject,System.Object)')
  - [SetTargetWithoutContext(d,target)](#M-Caliburn-Micro-Maui-Action-SetTargetWithoutContext-Microsoft-Maui-Controls-BindableObject,System-Object- 'Caliburn.Micro.Maui.Action.SetTargetWithoutContext(Microsoft.Maui.Controls.BindableObject,System.Object)')
- [ActionExecutionContext](#T-Caliburn-Micro-Maui-ActionExecutionContext 'Caliburn.Micro.Maui.ActionExecutionContext')
  - [CanExecute](#F-Caliburn-Micro-Maui-ActionExecutionContext-CanExecute 'Caliburn.Micro.Maui.ActionExecutionContext.CanExecute')
  - [EventArgs](#F-Caliburn-Micro-Maui-ActionExecutionContext-EventArgs 'Caliburn.Micro.Maui.ActionExecutionContext.EventArgs')
  - [Method](#F-Caliburn-Micro-Maui-ActionExecutionContext-Method 'Caliburn.Micro.Maui.ActionExecutionContext.Method')
  - [Item](#P-Caliburn-Micro-Maui-ActionExecutionContext-Item-System-String- 'Caliburn.Micro.Maui.ActionExecutionContext.Item(System.String)')
  - [Message](#P-Caliburn-Micro-Maui-ActionExecutionContext-Message 'Caliburn.Micro.Maui.ActionExecutionContext.Message')
  - [Source](#P-Caliburn-Micro-Maui-ActionExecutionContext-Source 'Caliburn.Micro.Maui.ActionExecutionContext.Source')
  - [Target](#P-Caliburn-Micro-Maui-ActionExecutionContext-Target 'Caliburn.Micro.Maui.ActionExecutionContext.Target')
  - [View](#P-Caliburn-Micro-Maui-ActionExecutionContext-View 'Caliburn.Micro.Maui.ActionExecutionContext.View')
  - [Dispose()](#M-Caliburn-Micro-Maui-ActionExecutionContext-Dispose 'Caliburn.Micro.Maui.ActionExecutionContext.Dispose')
- [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage')
  - [#ctor()](#M-Caliburn-Micro-Maui-ActionMessage-#ctor 'Caliburn.Micro.Maui.ActionMessage.#ctor')
  - [ApplyAvailabilityEffect](#F-Caliburn-Micro-Maui-ActionMessage-ApplyAvailabilityEffect 'Caliburn.Micro.Maui.ActionMessage.ApplyAvailabilityEffect')
  - [BuildPossibleGuardNames](#F-Caliburn-Micro-Maui-ActionMessage-BuildPossibleGuardNames 'Caliburn.Micro.Maui.ActionMessage.BuildPossibleGuardNames')
  - [EnforceGuardsDuringInvocation](#F-Caliburn-Micro-Maui-ActionMessage-EnforceGuardsDuringInvocation 'Caliburn.Micro.Maui.ActionMessage.EnforceGuardsDuringInvocation')
  - [GetTargetMethod](#F-Caliburn-Micro-Maui-ActionMessage-GetTargetMethod 'Caliburn.Micro.Maui.ActionMessage.GetTargetMethod')
  - [InvokeAction](#F-Caliburn-Micro-Maui-ActionMessage-InvokeAction 'Caliburn.Micro.Maui.ActionMessage.InvokeAction')
  - [PrepareContext](#F-Caliburn-Micro-Maui-ActionMessage-PrepareContext 'Caliburn.Micro.Maui.ActionMessage.PrepareContext')
  - [SetMethodBinding](#F-Caliburn-Micro-Maui-ActionMessage-SetMethodBinding 'Caliburn.Micro.Maui.ActionMessage.SetMethodBinding')
  - [ThrowsExceptions](#F-Caliburn-Micro-Maui-ActionMessage-ThrowsExceptions 'Caliburn.Micro.Maui.ActionMessage.ThrowsExceptions')
  - [Handler](#P-Caliburn-Micro-Maui-ActionMessage-Handler 'Caliburn.Micro.Maui.ActionMessage.Handler')
  - [MethodName](#P-Caliburn-Micro-Maui-ActionMessage-MethodName 'Caliburn.Micro.Maui.ActionMessage.MethodName')
  - [Parameters](#P-Caliburn-Micro-Maui-ActionMessage-Parameters 'Caliburn.Micro.Maui.ActionMessage.Parameters')
  - [Invoke(sender)](#M-Caliburn-Micro-Maui-ActionMessage-Invoke-Microsoft-Maui-Controls-VisualElement- 'Caliburn.Micro.Maui.ActionMessage.Invoke(Microsoft.Maui.Controls.VisualElement)')
  - [OnAttached()](#M-Caliburn-Micro-Maui-ActionMessage-OnAttached 'Caliburn.Micro.Maui.ActionMessage.OnAttached')
  - [OnDetaching()](#M-Caliburn-Micro-Maui-ActionMessage-OnDetaching 'Caliburn.Micro.Maui.ActionMessage.OnDetaching')
  - [ToString()](#M-Caliburn-Micro-Maui-ActionMessage-ToString 'Caliburn.Micro.Maui.ActionMessage.ToString')
  - [TryFindGuardMethod(context,possibleGuardNames)](#M-Caliburn-Micro-Maui-ActionMessage-TryFindGuardMethod-Caliburn-Micro-Maui-ActionExecutionContext,System-Collections-Generic-IEnumerable{System-String}- 'Caliburn.Micro.Maui.ActionMessage.TryFindGuardMethod(Caliburn.Micro.Maui.ActionExecutionContext,System.Collections.Generic.IEnumerable{System.String})')
  - [UpdateAvailability()](#M-Caliburn-Micro-Maui-ActionMessage-UpdateAvailability 'Caliburn.Micro.Maui.ActionMessage.UpdateAvailability')
- [ActivityEventArgs](#T-Caliburn-Micro-Maui-ActivityEventArgs 'Caliburn.Micro.Maui.ActivityEventArgs')
  - [#ctor(activity)](#M-Caliburn-Micro-Maui-ActivityEventArgs-#ctor-Android-App-Activity- 'Caliburn.Micro.Maui.ActivityEventArgs.#ctor(Android.App.Activity)')
  - [Activity](#P-Caliburn-Micro-Maui-ActivityEventArgs-Activity 'Caliburn.Micro.Maui.ActivityEventArgs.Activity')
- [ActivityLifecycleCallbackHandler](#T-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler')
  - [OnActivityCreated(activity,savedInstanceState)](#M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityCreated-Android-App-Activity,Android-OS-Bundle- 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler.OnActivityCreated(Android.App.Activity,Android.OS.Bundle)')
  - [OnActivityDestroyed(activity)](#M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityDestroyed-Android-App-Activity- 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler.OnActivityDestroyed(Android.App.Activity)')
  - [OnActivityPaused(activity)](#M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityPaused-Android-App-Activity- 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler.OnActivityPaused(Android.App.Activity)')
  - [OnActivityResumed(activity)](#M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityResumed-Android-App-Activity- 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler.OnActivityResumed(Android.App.Activity)')
  - [OnActivitySaveInstanceState(activity,outState)](#M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivitySaveInstanceState-Android-App-Activity,Android-OS-Bundle- 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler.OnActivitySaveInstanceState(Android.App.Activity,Android.OS.Bundle)')
  - [OnActivityStarted(activity)](#M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityStarted-Android-App-Activity- 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler.OnActivityStarted(Android.App.Activity)')
  - [OnActivityStopped(activity)](#M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityStopped-Android-App-Activity- 'Caliburn.Micro.Maui.ActivityLifecycleCallbackHandler.OnActivityStopped(Android.App.Activity)')
- [AndroidPlatformProvider](#T-Caliburn-Micro-Maui-AndroidPlatformProvider 'Caliburn.Micro.Maui.AndroidPlatformProvider')
  - [#ctor(application)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-#ctor-Android-App-Application- 'Caliburn.Micro.Maui.AndroidPlatformProvider.#ctor(Android.App.Application)')
  - [InDesignMode](#P-Caliburn-Micro-Maui-AndroidPlatformProvider-InDesignMode 'Caliburn.Micro.Maui.AndroidPlatformProvider.InDesignMode')
  - [PropertyChangeNotificationsOnUIThread](#P-Caliburn-Micro-Maui-AndroidPlatformProvider-PropertyChangeNotificationsOnUIThread 'Caliburn.Micro.Maui.AndroidPlatformProvider.PropertyChangeNotificationsOnUIThread')
  - [BeginOnUIThread(action)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-BeginOnUIThread-System-Action- 'Caliburn.Micro.Maui.AndroidPlatformProvider.BeginOnUIThread(System.Action)')
  - [ExecuteOnFirstLoad(view,handler)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}- 'Caliburn.Micro.Maui.AndroidPlatformProvider.ExecuteOnFirstLoad(System.Object,System.Action{System.Object})')
  - [ExecuteOnLayoutUpdated(view,handler)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}- 'Caliburn.Micro.Maui.AndroidPlatformProvider.ExecuteOnLayoutUpdated(System.Object,System.Action{System.Object})')
  - [GetFirstNonGeneratedView(view)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-GetFirstNonGeneratedView-System-Object- 'Caliburn.Micro.Maui.AndroidPlatformProvider.GetFirstNonGeneratedView(System.Object)')
  - [GetViewCloseAction(viewModel,views,dialogResult)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}- 'Caliburn.Micro.Maui.AndroidPlatformProvider.GetViewCloseAction(System.Object,System.Collections.Generic.ICollection{System.Object},System.Nullable{System.Boolean})')
  - [OnUIThread(action)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-OnUIThread-System-Action- 'Caliburn.Micro.Maui.AndroidPlatformProvider.OnUIThread(System.Action)')
  - [OnUIThreadAsync(action)](#M-Caliburn-Micro-Maui-AndroidPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}- 'Caliburn.Micro.Maui.AndroidPlatformProvider.OnUIThreadAsync(System.Func{System.Threading.Tasks.Task})')
- [AttachedCollection\`1](#T-Caliburn-Micro-Maui-AttachedCollection`1 'Caliburn.Micro.Maui.AttachedCollection`1')
  - [AssociatedObject](#P-Caliburn-Micro-Maui-AttachedCollection`1-AssociatedObject 'Caliburn.Micro.Maui.AttachedCollection`1.AssociatedObject')
  - [Attach(dependencyObject)](#M-Caliburn-Micro-Maui-AttachedCollection`1-Attach-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.AttachedCollection`1.Attach(Microsoft.Maui.Controls.BindableObject)')
  - [Detach()](#M-Caliburn-Micro-Maui-AttachedCollection`1-Detach 'Caliburn.Micro.Maui.AttachedCollection`1.Detach')
  - [OnCollectionChanged(e)](#M-Caliburn-Micro-Maui-AttachedCollection`1-OnCollectionChanged-System-Collections-Specialized-NotifyCollectionChangedEventArgs- 'Caliburn.Micro.Maui.AttachedCollection`1.OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs)')
  - [OnItemAdded(item)](#M-Caliburn-Micro-Maui-AttachedCollection`1-OnItemAdded-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.AttachedCollection`1.OnItemAdded(Microsoft.Maui.Controls.BindableObject)')
  - [OnItemRemoved(item)](#M-Caliburn-Micro-Maui-AttachedCollection`1-OnItemRemoved-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.AttachedCollection`1.OnItemRemoved(Microsoft.Maui.Controls.BindableObject)')
- [Bind](#T-Caliburn-Micro-Maui-Bind 'Caliburn.Micro.Maui.Bind')
  - [AtDesignTimeProperty](#F-Caliburn-Micro-Maui-Bind-AtDesignTimeProperty 'Caliburn.Micro.Maui.Bind.AtDesignTimeProperty')
  - [ModelProperty](#F-Caliburn-Micro-Maui-Bind-ModelProperty 'Caliburn.Micro.Maui.Bind.ModelProperty')
  - [ModelWithoutContextProperty](#F-Caliburn-Micro-Maui-Bind-ModelWithoutContextProperty 'Caliburn.Micro.Maui.Bind.ModelWithoutContextProperty')
  - [GetAtDesignTime(dependencyObject)](#M-Caliburn-Micro-Maui-Bind-GetAtDesignTime-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Bind.GetAtDesignTime(Microsoft.Maui.Controls.BindableObject)')
  - [GetModel(dependencyObject)](#M-Caliburn-Micro-Maui-Bind-GetModel-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Bind.GetModel(Microsoft.Maui.Controls.BindableObject)')
  - [GetModelWithoutContext(dependencyObject)](#M-Caliburn-Micro-Maui-Bind-GetModelWithoutContext-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Bind.GetModelWithoutContext(Microsoft.Maui.Controls.BindableObject)')
  - [SetAtDesignTime(dependencyObject,value)](#M-Caliburn-Micro-Maui-Bind-SetAtDesignTime-Microsoft-Maui-Controls-BindableObject,System-Boolean- 'Caliburn.Micro.Maui.Bind.SetAtDesignTime(Microsoft.Maui.Controls.BindableObject,System.Boolean)')
  - [SetModel(dependencyObject,value)](#M-Caliburn-Micro-Maui-Bind-SetModel-Microsoft-Maui-Controls-BindableObject,System-Object- 'Caliburn.Micro.Maui.Bind.SetModel(Microsoft.Maui.Controls.BindableObject,System.Object)')
  - [SetModelWithoutContext(dependencyObject,value)](#M-Caliburn-Micro-Maui-Bind-SetModelWithoutContext-Microsoft-Maui-Controls-BindableObject,System-Object- 'Caliburn.Micro.Maui.Bind.SetModelWithoutContext(Microsoft.Maui.Controls.BindableObject,System.Object)')
- [CaliburnApplication](#T-Caliburn-Micro-Maui-CaliburnApplication 'Caliburn.Micro.Maui.CaliburnApplication')
  - [#ctor(javaReference,transfer)](#M-Caliburn-Micro-Maui-CaliburnApplication-#ctor-System-IntPtr,Android-Runtime-JniHandleOwnership- 'Caliburn.Micro.Maui.CaliburnApplication.#ctor(System.IntPtr,Android.Runtime.JniHandleOwnership)')
  - [Configure()](#M-Caliburn-Micro-Maui-CaliburnApplication-Configure 'Caliburn.Micro.Maui.CaliburnApplication.Configure')
  - [Initialize()](#M-Caliburn-Micro-Maui-CaliburnApplication-Initialize 'Caliburn.Micro.Maui.CaliburnApplication.Initialize')
  - [SelectAssemblies()](#M-Caliburn-Micro-Maui-CaliburnApplication-SelectAssemblies 'Caliburn.Micro.Maui.CaliburnApplication.SelectAssemblies')
  - [StartDesignTime()](#M-Caliburn-Micro-Maui-CaliburnApplication-StartDesignTime 'Caliburn.Micro.Maui.CaliburnApplication.StartDesignTime')
  - [StartRuntime()](#M-Caliburn-Micro-Maui-CaliburnApplication-StartRuntime 'Caliburn.Micro.Maui.CaliburnApplication.StartRuntime')
- [ConventionManager](#T-Caliburn-Micro-Maui-ConventionManager 'Caliburn.Micro.Maui.ConventionManager')
  - [ApplyBindingMode](#F-Caliburn-Micro-Maui-ConventionManager-ApplyBindingMode 'Caliburn.Micro.Maui.ConventionManager.ApplyBindingMode')
  - [ApplyStringFormat](#F-Caliburn-Micro-Maui-ConventionManager-ApplyStringFormat 'Caliburn.Micro.Maui.ConventionManager.ApplyStringFormat')
  - [ApplyUpdateSourceTrigger](#F-Caliburn-Micro-Maui-ConventionManager-ApplyUpdateSourceTrigger 'Caliburn.Micro.Maui.ConventionManager.ApplyUpdateSourceTrigger')
  - [ApplyValidation](#F-Caliburn-Micro-Maui-ConventionManager-ApplyValidation 'Caliburn.Micro.Maui.ConventionManager.ApplyValidation')
  - [ApplyValueConverter](#F-Caliburn-Micro-Maui-ConventionManager-ApplyValueConverter 'Caliburn.Micro.Maui.ConventionManager.ApplyValueConverter')
  - [ConfigureSelectedItem](#F-Caliburn-Micro-Maui-ConventionManager-ConfigureSelectedItem 'Caliburn.Micro.Maui.ConventionManager.ConfigureSelectedItem')
  - [ConfigureSelectedItemBinding](#F-Caliburn-Micro-Maui-ConventionManager-ConfigureSelectedItemBinding 'Caliburn.Micro.Maui.ConventionManager.ConfigureSelectedItemBinding')
  - [DefaultHeaderTemplate](#F-Caliburn-Micro-Maui-ConventionManager-DefaultHeaderTemplate 'Caliburn.Micro.Maui.ConventionManager.DefaultHeaderTemplate')
  - [DefaultItemTemplate](#F-Caliburn-Micro-Maui-ConventionManager-DefaultItemTemplate 'Caliburn.Micro.Maui.ConventionManager.DefaultItemTemplate')
  - [DerivePotentialSelectionNames](#F-Caliburn-Micro-Maui-ConventionManager-DerivePotentialSelectionNames 'Caliburn.Micro.Maui.ConventionManager.DerivePotentialSelectionNames')
  - [IncludeStaticProperties](#F-Caliburn-Micro-Maui-ConventionManager-IncludeStaticProperties 'Caliburn.Micro.Maui.ConventionManager.IncludeStaticProperties')
  - [OverwriteContent](#F-Caliburn-Micro-Maui-ConventionManager-OverwriteContent 'Caliburn.Micro.Maui.ConventionManager.OverwriteContent')
  - [SetBinding](#F-Caliburn-Micro-Maui-ConventionManager-SetBinding 'Caliburn.Micro.Maui.ConventionManager.SetBinding')
  - [Singularize](#F-Caliburn-Micro-Maui-ConventionManager-Singularize 'Caliburn.Micro.Maui.ConventionManager.Singularize')
  - [AddElementConvention(convention)](#M-Caliburn-Micro-Maui-ConventionManager-AddElementConvention-Caliburn-Micro-Maui-ElementConvention- 'Caliburn.Micro.Maui.ConventionManager.AddElementConvention(Caliburn.Micro.Maui.ElementConvention)')
  - [AddElementConvention\`\`1(bindableProperty,parameterProperty,eventName)](#M-Caliburn-Micro-Maui-ConventionManager-AddElementConvention``1-Microsoft-Maui-Controls-BindableProperty,System-String,System-String- 'Caliburn.Micro.Maui.ConventionManager.AddElementConvention``1(Microsoft.Maui.Controls.BindableProperty,System.String,System.String)')
  - [ApplyHeaderTemplate(element,headerTemplateProperty,headerTemplateSelectorProperty,viewModelType)](#M-Caliburn-Micro-Maui-ConventionManager-ApplyHeaderTemplate-Microsoft-Maui-Controls-VisualElement,Microsoft-Maui-Controls-BindableProperty,Microsoft-Maui-Controls-BindableProperty,System-Type- 'Caliburn.Micro.Maui.ConventionManager.ApplyHeaderTemplate(Microsoft.Maui.Controls.VisualElement,Microsoft.Maui.Controls.BindableProperty,Microsoft.Maui.Controls.BindableProperty,System.Type)')
  - [ApplyItemTemplate\`\`1(itemsControl,property)](#M-Caliburn-Micro-Maui-ConventionManager-ApplyItemTemplate``1-Microsoft-Maui-Controls-ItemsView{``0},System-Reflection-PropertyInfo- 'Caliburn.Micro.Maui.ConventionManager.ApplyItemTemplate``1(Microsoft.Maui.Controls.ItemsView{``0},System.Reflection.PropertyInfo)')
  - [GetElementConvention(elementType)](#M-Caliburn-Micro-Maui-ConventionManager-GetElementConvention-System-Type- 'Caliburn.Micro.Maui.ConventionManager.GetElementConvention(System.Type)')
  - [GetPropertyCaseInsensitive(type,propertyName)](#M-Caliburn-Micro-Maui-ConventionManager-GetPropertyCaseInsensitive-System-Type,System-String- 'Caliburn.Micro.Maui.ConventionManager.GetPropertyCaseInsensitive(System.Type,System.String)')
  - [HasBinding()](#M-Caliburn-Micro-Maui-ConventionManager-HasBinding-Microsoft-Maui-Controls-VisualElement,Microsoft-Maui-Controls-BindableProperty- 'Caliburn.Micro.Maui.ConventionManager.HasBinding(Microsoft.Maui.Controls.VisualElement,Microsoft.Maui.Controls.BindableProperty)')
  - [SetBindingWithoutBindingOrValueOverwrite(viewModelType,path,property,element,convention,bindableProperty)](#M-Caliburn-Micro-Maui-ConventionManager-SetBindingWithoutBindingOrValueOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-ElementConvention,Microsoft-Maui-Controls-BindableProperty- 'Caliburn.Micro.Maui.ConventionManager.SetBindingWithoutBindingOrValueOverwrite(System.Type,System.String,System.Reflection.PropertyInfo,Microsoft.Maui.Controls.VisualElement,Caliburn.Micro.Maui.ElementConvention,Microsoft.Maui.Controls.BindableProperty)')
  - [SetBindingWithoutBindingOverwrite()](#M-Caliburn-Micro-Maui-ConventionManager-SetBindingWithoutBindingOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-ElementConvention,Microsoft-Maui-Controls-BindableProperty- 'Caliburn.Micro.Maui.ConventionManager.SetBindingWithoutBindingOverwrite(System.Type,System.String,System.Reflection.PropertyInfo,Microsoft.Maui.Controls.VisualElement,Caliburn.Micro.Maui.ElementConvention,Microsoft.Maui.Controls.BindableProperty)')
- [DependencyPropertyChangedEventArgs](#T-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs 'Caliburn.Micro.Maui.DependencyPropertyChangedEventArgs')
  - [#ctor(newValue,oldValue,property)](#M-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-#ctor-System-Object,System-Object,Microsoft-Maui-Controls-BindableProperty- 'Caliburn.Micro.Maui.DependencyPropertyChangedEventArgs.#ctor(System.Object,System.Object,Microsoft.Maui.Controls.BindableProperty)')
  - [NewValue](#P-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-NewValue 'Caliburn.Micro.Maui.DependencyPropertyChangedEventArgs.NewValue')
  - [OldValue](#P-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-OldValue 'Caliburn.Micro.Maui.DependencyPropertyChangedEventArgs.OldValue')
  - [Property](#P-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-Property 'Caliburn.Micro.Maui.DependencyPropertyChangedEventArgs.Property')
- [DependencyPropertyHelper](#T-Caliburn-Micro-Maui-DependencyPropertyHelper 'Caliburn.Micro.Maui.DependencyPropertyHelper')
  - [Register(name,propertyType,ownerType,defaultValue,propertyChangedCallback)](#M-Caliburn-Micro-Maui-DependencyPropertyHelper-Register-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Maui-PropertyChangedCallback- 'Caliburn.Micro.Maui.DependencyPropertyHelper.Register(System.String,System.Type,System.Type,System.Object,Caliburn.Micro.Maui.PropertyChangedCallback)')
  - [RegisterAttached(name,propertyType,ownerType,defaultValue,propertyChangedCallback)](#M-Caliburn-Micro-Maui-DependencyPropertyHelper-RegisterAttached-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Maui-PropertyChangedCallback- 'Caliburn.Micro.Maui.DependencyPropertyHelper.RegisterAttached(System.String,System.Type,System.Type,System.Object,Caliburn.Micro.Maui.PropertyChangedCallback)')
- [ElementConvention](#T-Caliburn-Micro-Maui-ElementConvention 'Caliburn.Micro.Maui.ElementConvention')
  - [ApplyBinding](#F-Caliburn-Micro-Maui-ElementConvention-ApplyBinding 'Caliburn.Micro.Maui.ElementConvention.ApplyBinding')
  - [CreateTrigger](#F-Caliburn-Micro-Maui-ElementConvention-CreateTrigger 'Caliburn.Micro.Maui.ElementConvention.CreateTrigger')
  - [ElementType](#F-Caliburn-Micro-Maui-ElementConvention-ElementType 'Caliburn.Micro.Maui.ElementConvention.ElementType')
  - [GetBindableProperty](#F-Caliburn-Micro-Maui-ElementConvention-GetBindableProperty 'Caliburn.Micro.Maui.ElementConvention.GetBindableProperty')
  - [ParameterProperty](#F-Caliburn-Micro-Maui-ElementConvention-ParameterProperty 'Caliburn.Micro.Maui.ElementConvention.ParameterProperty')
- [FormsPlatformProvider](#T-Caliburn-Micro-Maui-FormsPlatformProvider 'Caliburn.Micro.Maui.FormsPlatformProvider')
  - [#ctor(platformProvider)](#M-Caliburn-Micro-Maui-FormsPlatformProvider-#ctor-Caliburn-Micro-IPlatformProvider- 'Caliburn.Micro.Maui.FormsPlatformProvider.#ctor(Caliburn.Micro.IPlatformProvider)')
  - [InDesignMode](#P-Caliburn-Micro-Maui-FormsPlatformProvider-InDesignMode 'Caliburn.Micro.Maui.FormsPlatformProvider.InDesignMode')
  - [PropertyChangeNotificationsOnUIThread](#P-Caliburn-Micro-Maui-FormsPlatformProvider-PropertyChangeNotificationsOnUIThread 'Caliburn.Micro.Maui.FormsPlatformProvider.PropertyChangeNotificationsOnUIThread')
  - [BeginOnUIThread()](#M-Caliburn-Micro-Maui-FormsPlatformProvider-BeginOnUIThread-System-Action- 'Caliburn.Micro.Maui.FormsPlatformProvider.BeginOnUIThread(System.Action)')
  - [ExecuteOnFirstLoad()](#M-Caliburn-Micro-Maui-FormsPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}- 'Caliburn.Micro.Maui.FormsPlatformProvider.ExecuteOnFirstLoad(System.Object,System.Action{System.Object})')
  - [ExecuteOnLayoutUpdated()](#M-Caliburn-Micro-Maui-FormsPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}- 'Caliburn.Micro.Maui.FormsPlatformProvider.ExecuteOnLayoutUpdated(System.Object,System.Action{System.Object})')
  - [GetFirstNonGeneratedView()](#M-Caliburn-Micro-Maui-FormsPlatformProvider-GetFirstNonGeneratedView-System-Object- 'Caliburn.Micro.Maui.FormsPlatformProvider.GetFirstNonGeneratedView(System.Object)')
  - [GetViewCloseAction()](#M-Caliburn-Micro-Maui-FormsPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}- 'Caliburn.Micro.Maui.FormsPlatformProvider.GetViewCloseAction(System.Object,System.Collections.Generic.ICollection{System.Object},System.Nullable{System.Boolean})')
  - [OnUIThread()](#M-Caliburn-Micro-Maui-FormsPlatformProvider-OnUIThread-System-Action- 'Caliburn.Micro.Maui.FormsPlatformProvider.OnUIThread(System.Action)')
  - [OnUIThreadAsync()](#M-Caliburn-Micro-Maui-FormsPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}- 'Caliburn.Micro.Maui.FormsPlatformProvider.OnUIThreadAsync(System.Func{System.Threading.Tasks.Task})')
- [IAttachedObject](#T-Caliburn-Micro-Maui-IAttachedObject 'Caliburn.Micro.Maui.IAttachedObject')
  - [AssociatedObject](#P-Caliburn-Micro-Maui-IAttachedObject-AssociatedObject 'Caliburn.Micro.Maui.IAttachedObject.AssociatedObject')
  - [Attach(dependencyObject)](#M-Caliburn-Micro-Maui-IAttachedObject-Attach-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.IAttachedObject.Attach(Microsoft.Maui.Controls.BindableObject)')
  - [Detach()](#M-Caliburn-Micro-Maui-IAttachedObject-Detach 'Caliburn.Micro.Maui.IAttachedObject.Detach')
- [IHaveParameters](#T-Caliburn-Micro-Maui-IHaveParameters 'Caliburn.Micro.Maui.IHaveParameters')
  - [Parameters](#P-Caliburn-Micro-Maui-IHaveParameters-Parameters 'Caliburn.Micro.Maui.IHaveParameters.Parameters')
- [INavigationService](#T-Caliburn-Micro-Maui-INavigationService 'Caliburn.Micro.Maui.INavigationService')
  - [GoBackAsync(animated)](#M-Caliburn-Micro-Maui-INavigationService-GoBackAsync-System-Boolean- 'Caliburn.Micro.Maui.INavigationService.GoBackAsync(System.Boolean)')
  - [GoBackToRootAsync(animated)](#M-Caliburn-Micro-Maui-INavigationService-GoBackToRootAsync-System-Boolean- 'Caliburn.Micro.Maui.INavigationService.GoBackToRootAsync(System.Boolean)')
  - [NavigateToViewAsync(viewType,parameter,animated)](#M-Caliburn-Micro-Maui-INavigationService-NavigateToViewAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Maui.INavigationService.NavigateToViewAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Maui-INavigationService-NavigateToViewAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Maui.INavigationService.NavigateToViewAsync``1(System.Object,System.Boolean)')
  - [NavigateToViewModelAsync(viewModelType,parameter,animated)](#M-Caliburn-Micro-Maui-INavigationService-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Maui.INavigationService.NavigateToViewModelAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewModelAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Maui-INavigationService-NavigateToViewModelAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Maui.INavigationService.NavigateToViewModelAsync``1(System.Object,System.Boolean)')
- [MauiApplication](#T-Caliburn-Micro-Maui-MauiApplication 'Caliburn.Micro.Maui.MauiApplication')
  - [RootNavigationPage](#P-Caliburn-Micro-Maui-MauiApplication-RootNavigationPage 'Caliburn.Micro.Maui.MauiApplication.RootNavigationPage')
  - [BuildUp(instance)](#M-Caliburn-Micro-Maui-MauiApplication-BuildUp-System-Object- 'Caliburn.Micro.Maui.MauiApplication.BuildUp(System.Object)')
  - [Configure()](#M-Caliburn-Micro-Maui-MauiApplication-Configure 'Caliburn.Micro.Maui.MauiApplication.Configure')
  - [CreateApplicationPage()](#M-Caliburn-Micro-Maui-MauiApplication-CreateApplicationPage 'Caliburn.Micro.Maui.MauiApplication.CreateApplicationPage')
  - [DisplayRootView(viewType)](#M-Caliburn-Micro-Maui-MauiApplication-DisplayRootView-System-Type- 'Caliburn.Micro.Maui.MauiApplication.DisplayRootView(System.Type)')
  - [DisplayRootViewForAsync(viewModelType)](#M-Caliburn-Micro-Maui-MauiApplication-DisplayRootViewForAsync-System-Type- 'Caliburn.Micro.Maui.MauiApplication.DisplayRootViewForAsync(System.Type)')
  - [DisplayRootViewForAsync\`\`1()](#M-Caliburn-Micro-Maui-MauiApplication-DisplayRootViewForAsync``1 'Caliburn.Micro.Maui.MauiApplication.DisplayRootViewForAsync``1')
  - [DisplayRootView\`\`1()](#M-Caliburn-Micro-Maui-MauiApplication-DisplayRootView``1 'Caliburn.Micro.Maui.MauiApplication.DisplayRootView``1')
  - [GetAllInstances(service)](#M-Caliburn-Micro-Maui-MauiApplication-GetAllInstances-System-Type- 'Caliburn.Micro.Maui.MauiApplication.GetAllInstances(System.Type)')
  - [GetInstance(service,key)](#M-Caliburn-Micro-Maui-MauiApplication-GetInstance-System-Type,System-String- 'Caliburn.Micro.Maui.MauiApplication.GetInstance(System.Type,System.String)')
  - [Initialize()](#M-Caliburn-Micro-Maui-MauiApplication-Initialize 'Caliburn.Micro.Maui.MauiApplication.Initialize')
  - [PrepareViewFirst()](#M-Caliburn-Micro-Maui-MauiApplication-PrepareViewFirst 'Caliburn.Micro.Maui.MauiApplication.PrepareViewFirst')
  - [PrepareViewFirst(navigationPage)](#M-Caliburn-Micro-Maui-MauiApplication-PrepareViewFirst-Microsoft-Maui-Controls-NavigationPage- 'Caliburn.Micro.Maui.MauiApplication.PrepareViewFirst(Microsoft.Maui.Controls.NavigationPage)')
- [Message](#T-Caliburn-Micro-Maui-Message 'Caliburn.Micro.Maui.Message')
  - [AttachProperty](#F-Caliburn-Micro-Maui-Message-AttachProperty 'Caliburn.Micro.Maui.Message.AttachProperty')
  - [GetAttach(d)](#M-Caliburn-Micro-Maui-Message-GetAttach-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Message.GetAttach(Microsoft.Maui.Controls.BindableObject)')
  - [GetHandler(d)](#M-Caliburn-Micro-Maui-Message-GetHandler-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.Message.GetHandler(Microsoft.Maui.Controls.BindableObject)')
  - [SetAttach(d,attachText)](#M-Caliburn-Micro-Maui-Message-SetAttach-Microsoft-Maui-Controls-BindableObject,System-String- 'Caliburn.Micro.Maui.Message.SetAttach(Microsoft.Maui.Controls.BindableObject,System.String)')
  - [SetHandler(d,value)](#M-Caliburn-Micro-Maui-Message-SetHandler-Microsoft-Maui-Controls-BindableObject,System-Object- 'Caliburn.Micro.Maui.Message.SetHandler(Microsoft.Maui.Controls.BindableObject,System.Object)')
- [MessageBinder](#T-Caliburn-Micro-Maui-MessageBinder 'Caliburn.Micro.Maui.MessageBinder')
  - [CustomConverters](#F-Caliburn-Micro-Maui-MessageBinder-CustomConverters 'Caliburn.Micro.Maui.MessageBinder.CustomConverters')
  - [EvaluateParameter](#F-Caliburn-Micro-Maui-MessageBinder-EvaluateParameter 'Caliburn.Micro.Maui.MessageBinder.EvaluateParameter')
  - [SpecialValues](#F-Caliburn-Micro-Maui-MessageBinder-SpecialValues 'Caliburn.Micro.Maui.MessageBinder.SpecialValues')
  - [CoerceValue(destinationType,providedValue,context)](#M-Caliburn-Micro-Maui-MessageBinder-CoerceValue-System-Type,System-Object,System-Object- 'Caliburn.Micro.Maui.MessageBinder.CoerceValue(System.Type,System.Object,System.Object)')
  - [DetermineParameters(context,requiredParameters)](#M-Caliburn-Micro-Maui-MessageBinder-DetermineParameters-Caliburn-Micro-Maui-ActionExecutionContext,System-Reflection-ParameterInfo[]- 'Caliburn.Micro.Maui.MessageBinder.DetermineParameters(Caliburn.Micro.Maui.ActionExecutionContext,System.Reflection.ParameterInfo[])')
  - [GetDefaultValue(type)](#M-Caliburn-Micro-Maui-MessageBinder-GetDefaultValue-System-Type- 'Caliburn.Micro.Maui.MessageBinder.GetDefaultValue(System.Type)')
- [NavigateHelper\`1](#T-Caliburn-Micro-Maui-NavigateHelper`1 'Caliburn.Micro.Maui.NavigateHelper`1')
  - [AttachTo(navigationService)](#M-Caliburn-Micro-Maui-NavigateHelper`1-AttachTo-Caliburn-Micro-Maui-INavigationService- 'Caliburn.Micro.Maui.NavigateHelper`1.AttachTo(Caliburn.Micro.Maui.INavigationService)')
  - [Navigate()](#M-Caliburn-Micro-Maui-NavigateHelper`1-Navigate-System-Boolean- 'Caliburn.Micro.Maui.NavigateHelper`1.Navigate(System.Boolean)')
  - [WithParam\`\`1(property,value)](#M-Caliburn-Micro-Maui-NavigateHelper`1-WithParam``1-System-Linq-Expressions-Expression{System-Func{`0,``0}},``0- 'Caliburn.Micro.Maui.NavigateHelper`1.WithParam``1(System.Linq.Expressions.Expression{System.Func{`0,``0}},``0)')
- [NavigationExtensions](#T-Caliburn-Micro-Maui-NavigationExtensions 'Caliburn.Micro.Maui.NavigationExtensions')
  - [For\`\`1(navigationService)](#M-Caliburn-Micro-Maui-NavigationExtensions-For``1-Caliburn-Micro-Maui-INavigationService- 'Caliburn.Micro.Maui.NavigationExtensions.For``1(Caliburn.Micro.Maui.INavigationService)')
- [NavigationPageAdapter](#T-Caliburn-Micro-Maui-NavigationPageAdapter 'Caliburn.Micro.Maui.NavigationPageAdapter')
  - [#ctor(navigationPage)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-#ctor-Microsoft-Maui-Controls-NavigationPage- 'Caliburn.Micro.Maui.NavigationPageAdapter.#ctor(Microsoft.Maui.Controls.NavigationPage)')
  - [ActivateViewAsync(view)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-ActivateViewAsync-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.NavigationPageAdapter.ActivateViewAsync(Microsoft.Maui.Controls.BindableObject)')
  - [CreateContentPage(view,viewModel)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-CreateContentPage-Microsoft-Maui-Controls-ContentView,System-Object- 'Caliburn.Micro.Maui.NavigationPageAdapter.CreateContentPage(Microsoft.Maui.Controls.ContentView,System.Object)')
  - [DeactivateViewAsync(view)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-DeactivateViewAsync-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.NavigationPageAdapter.DeactivateViewAsync(Microsoft.Maui.Controls.BindableObject)')
  - [GoBackAsync(animated)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-GoBackAsync-System-Boolean- 'Caliburn.Micro.Maui.NavigationPageAdapter.GoBackAsync(System.Boolean)')
  - [GoBackToRootAsync(animated)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-GoBackToRootAsync-System-Boolean- 'Caliburn.Micro.Maui.NavigationPageAdapter.GoBackToRootAsync(System.Boolean)')
  - [NavigateToViewAsync(viewType,parameter,animated)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Maui.NavigationPageAdapter.NavigateToViewAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Maui.NavigationPageAdapter.NavigateToViewAsync``1(System.Object,System.Boolean)')
  - [NavigateToViewModelAsync(viewModelType,parameter,animated)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean- 'Caliburn.Micro.Maui.NavigationPageAdapter.NavigateToViewModelAsync(System.Type,System.Object,System.Boolean)')
  - [NavigateToViewModelAsync\`\`1(parameter,animated)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewModelAsync``1-System-Object,System-Boolean- 'Caliburn.Micro.Maui.NavigationPageAdapter.NavigateToViewModelAsync``1(System.Object,System.Boolean)')
  - [TryInjectParameters(viewModel,parameter)](#M-Caliburn-Micro-Maui-NavigationPageAdapter-TryInjectParameters-System-Object,System-Object- 'Caliburn.Micro.Maui.NavigationPageAdapter.TryInjectParameters(System.Object,System.Object)')
- [Parameter](#T-Caliburn-Micro-Maui-Parameter 'Caliburn.Micro.Maui.Parameter')
  - [ValueProperty](#F-Caliburn-Micro-Maui-Parameter-ValueProperty 'Caliburn.Micro.Maui.Parameter.ValueProperty')
  - [Owner](#P-Caliburn-Micro-Maui-Parameter-Owner 'Caliburn.Micro.Maui.Parameter.Owner')
  - [Value](#P-Caliburn-Micro-Maui-Parameter-Value 'Caliburn.Micro.Maui.Parameter.Value')
  - [MakeAwareOf(owner)](#M-Caliburn-Micro-Maui-Parameter-MakeAwareOf-Caliburn-Micro-Maui-ActionMessage- 'Caliburn.Micro.Maui.Parameter.MakeAwareOf(Caliburn.Micro.Maui.ActionMessage)')
- [Parser](#T-Caliburn-Micro-Maui-Parser 'Caliburn.Micro.Maui.Parser')
  - [CreateParameter](#F-Caliburn-Micro-Maui-Parser-CreateParameter 'Caliburn.Micro.Maui.Parser.CreateParameter')
  - [CreateTrigger](#F-Caliburn-Micro-Maui-Parser-CreateTrigger 'Caliburn.Micro.Maui.Parser.CreateTrigger')
  - [InterpretMessageText](#F-Caliburn-Micro-Maui-Parser-InterpretMessageText 'Caliburn.Micro.Maui.Parser.InterpretMessageText')
  - [BindParameter(target,parameter,elementName,path,bindingMode)](#M-Caliburn-Micro-Maui-Parser-BindParameter-Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-Parameter,System-String,System-String,Microsoft-Maui-Controls-BindingMode- 'Caliburn.Micro.Maui.Parser.BindParameter(Microsoft.Maui.Controls.VisualElement,Caliburn.Micro.Maui.Parameter,System.String,System.String,Microsoft.Maui.Controls.BindingMode)')
  - [CreateMessage(target,messageText)](#M-Caliburn-Micro-Maui-Parser-CreateMessage-Microsoft-Maui-Controls-BindableObject,System-String- 'Caliburn.Micro.Maui.Parser.CreateMessage(Microsoft.Maui.Controls.BindableObject,System.String)')
  - [Parse(target,text)](#M-Caliburn-Micro-Maui-Parser-Parse-Microsoft-Maui-Controls-BindableObject,System-String- 'Caliburn.Micro.Maui.Parser.Parse(Microsoft.Maui.Controls.BindableObject,System.String)')
- [PropertyChangedCallback](#T-Caliburn-Micro-Maui-PropertyChangedCallback 'Caliburn.Micro.Maui.PropertyChangedCallback')
- [RoutedEventArgs](#T-Caliburn-Micro-Maui-RoutedEventArgs 'Caliburn.Micro.Maui.RoutedEventArgs')
  - [OriginalSource](#P-Caliburn-Micro-Maui-RoutedEventArgs-OriginalSource 'Caliburn.Micro.Maui.RoutedEventArgs.OriginalSource')
- [RoutedEventHandler](#T-Caliburn-Micro-Maui-RoutedEventHandler 'Caliburn.Micro.Maui.RoutedEventHandler')
- [TriggerActionBase\`1](#T-Caliburn-Micro-Maui-TriggerActionBase`1 'Caliburn.Micro.Maui.TriggerActionBase`1')
  - [AssociatedObject](#P-Caliburn-Micro-Maui-TriggerActionBase`1-AssociatedObject 'Caliburn.Micro.Maui.TriggerActionBase`1.AssociatedObject')
  - [OnAttached()](#M-Caliburn-Micro-Maui-TriggerActionBase`1-OnAttached 'Caliburn.Micro.Maui.TriggerActionBase`1.OnAttached')
  - [OnDetaching()](#M-Caliburn-Micro-Maui-TriggerActionBase`1-OnDetaching 'Caliburn.Micro.Maui.TriggerActionBase`1.OnDetaching')
- [View](#T-Caliburn-Micro-Maui-View 'Caliburn.Micro.Maui.View')
  - [ApplyConventionsProperty](#F-Caliburn-Micro-Maui-View-ApplyConventionsProperty 'Caliburn.Micro.Maui.View.ApplyConventionsProperty')
  - [ContextProperty](#F-Caliburn-Micro-Maui-View-ContextProperty 'Caliburn.Micro.Maui.View.ContextProperty')
  - [GetFirstNonGeneratedView](#F-Caliburn-Micro-Maui-View-GetFirstNonGeneratedView 'Caliburn.Micro.Maui.View.GetFirstNonGeneratedView')
  - [IsGeneratedProperty](#F-Caliburn-Micro-Maui-View-IsGeneratedProperty 'Caliburn.Micro.Maui.View.IsGeneratedProperty')
  - [IsLoadedProperty](#F-Caliburn-Micro-Maui-View-IsLoadedProperty 'Caliburn.Micro.Maui.View.IsLoadedProperty')
  - [IsScopeRootProperty](#F-Caliburn-Micro-Maui-View-IsScopeRootProperty 'Caliburn.Micro.Maui.View.IsScopeRootProperty')
  - [ModelProperty](#F-Caliburn-Micro-Maui-View-ModelProperty 'Caliburn.Micro.Maui.View.ModelProperty')
  - [InDesignMode](#P-Caliburn-Micro-Maui-View-InDesignMode 'Caliburn.Micro.Maui.View.InDesignMode')
  - [ExecuteOnLoad(element,handler)](#M-Caliburn-Micro-Maui-View-ExecuteOnLoad-Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-RoutedEventHandler- 'Caliburn.Micro.Maui.View.ExecuteOnLoad(Microsoft.Maui.Controls.VisualElement,Caliburn.Micro.Maui.RoutedEventHandler)')
  - [ExecuteOnUnload(element,handler)](#M-Caliburn-Micro-Maui-View-ExecuteOnUnload-Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-RoutedEventHandler- 'Caliburn.Micro.Maui.View.ExecuteOnUnload(Microsoft.Maui.Controls.VisualElement,Caliburn.Micro.Maui.RoutedEventHandler)')
  - [GetApplyConventions(d)](#M-Caliburn-Micro-Maui-View-GetApplyConventions-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.View.GetApplyConventions(Microsoft.Maui.Controls.BindableObject)')
  - [GetContext(d)](#M-Caliburn-Micro-Maui-View-GetContext-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.View.GetContext(Microsoft.Maui.Controls.BindableObject)')
  - [GetModel(d)](#M-Caliburn-Micro-Maui-View-GetModel-Microsoft-Maui-Controls-BindableObject- 'Caliburn.Micro.Maui.View.GetModel(Microsoft.Maui.Controls.BindableObject)')
  - [SetApplyConventions(d,value)](#M-Caliburn-Micro-Maui-View-SetApplyConventions-Microsoft-Maui-Controls-BindableObject,System-Nullable{System-Boolean}- 'Caliburn.Micro.Maui.View.SetApplyConventions(Microsoft.Maui.Controls.BindableObject,System.Nullable{System.Boolean})')
  - [SetContext(d,value)](#M-Caliburn-Micro-Maui-View-SetContext-Microsoft-Maui-Controls-BindableObject,System-Object- 'Caliburn.Micro.Maui.View.SetContext(Microsoft.Maui.Controls.BindableObject,System.Object)')
  - [SetModel(d,value)](#M-Caliburn-Micro-Maui-View-SetModel-Microsoft-Maui-Controls-BindableObject,System-Object- 'Caliburn.Micro.Maui.View.SetModel(Microsoft.Maui.Controls.BindableObject,System.Object)')
- [ViewLocator](#T-Caliburn-Micro-Maui-ViewLocator 'Caliburn.Micro.Maui.ViewLocator')
  - [ContextSeparator](#F-Caliburn-Micro-Maui-ViewLocator-ContextSeparator 'Caliburn.Micro.Maui.ViewLocator.ContextSeparator')
  - [DeterminePackUriFromType](#F-Caliburn-Micro-Maui-ViewLocator-DeterminePackUriFromType 'Caliburn.Micro.Maui.ViewLocator.DeterminePackUriFromType')
  - [GetOrCreateViewType](#F-Caliburn-Micro-Maui-ViewLocator-GetOrCreateViewType 'Caliburn.Micro.Maui.ViewLocator.GetOrCreateViewType')
  - [LocateForModel](#F-Caliburn-Micro-Maui-ViewLocator-LocateForModel 'Caliburn.Micro.Maui.ViewLocator.LocateForModel')
  - [LocateForModelType](#F-Caliburn-Micro-Maui-ViewLocator-LocateForModelType 'Caliburn.Micro.Maui.ViewLocator.LocateForModelType')
  - [LocateTypeForModelType](#F-Caliburn-Micro-Maui-ViewLocator-LocateTypeForModelType 'Caliburn.Micro.Maui.ViewLocator.LocateTypeForModelType')
  - [ModifyModelTypeAtDesignTime](#F-Caliburn-Micro-Maui-ViewLocator-ModifyModelTypeAtDesignTime 'Caliburn.Micro.Maui.ViewLocator.ModifyModelTypeAtDesignTime')
  - [NameTransformer](#F-Caliburn-Micro-Maui-ViewLocator-NameTransformer 'Caliburn.Micro.Maui.ViewLocator.NameTransformer')
  - [TransformName](#F-Caliburn-Micro-Maui-ViewLocator-TransformName 'Caliburn.Micro.Maui.ViewLocator.TransformName')
  - [AddDefaultTypeMapping(viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-AddDefaultTypeMapping-System-String- 'Caliburn.Micro.Maui.ViewLocator.AddDefaultTypeMapping(System.String)')
  - [AddNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-AddNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Maui.ViewLocator.AddNamespaceMapping(System.String,System.String[],System.String)')
  - [AddNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-AddNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Maui.ViewLocator.AddNamespaceMapping(System.String,System.String,System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-AddSubNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Maui.ViewLocator.AddSubNamespaceMapping(System.String,System.String[],System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-AddSubNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Maui.ViewLocator.AddSubNamespaceMapping(System.String,System.String,System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-AddTypeMapping-System-String,System-String,System-String[],System-String- 'Caliburn.Micro.Maui.ViewLocator.AddTypeMapping(System.String,System.String,System.String[],System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-AddTypeMapping-System-String,System-String,System-String,System-String- 'Caliburn.Micro.Maui.ViewLocator.AddTypeMapping(System.String,System.String,System.String,System.String)')
  - [ConfigureTypeMappings(config)](#M-Caliburn-Micro-Maui-ViewLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration- 'Caliburn.Micro.Maui.ViewLocator.ConfigureTypeMappings(Caliburn.Micro.TypeMappingConfiguration)')
  - [InitializeComponent(element)](#M-Caliburn-Micro-Maui-ViewLocator-InitializeComponent-System-Object- 'Caliburn.Micro.Maui.ViewLocator.InitializeComponent(System.Object)')
  - [RegisterViewSuffix(viewSuffix)](#M-Caliburn-Micro-Maui-ViewLocator-RegisterViewSuffix-System-String- 'Caliburn.Micro.Maui.ViewLocator.RegisterViewSuffix(System.String)')
- [ViewModelBinder](#T-Caliburn-Micro-Maui-ViewModelBinder 'Caliburn.Micro.Maui.ViewModelBinder')
  - [ApplyConventionsByDefault](#F-Caliburn-Micro-Maui-ViewModelBinder-ApplyConventionsByDefault 'Caliburn.Micro.Maui.ViewModelBinder.ApplyConventionsByDefault')
  - [Bind](#F-Caliburn-Micro-Maui-ViewModelBinder-Bind 'Caliburn.Micro.Maui.ViewModelBinder.Bind')
  - [BindActions](#F-Caliburn-Micro-Maui-ViewModelBinder-BindActions 'Caliburn.Micro.Maui.ViewModelBinder.BindActions')
  - [BindProperties](#F-Caliburn-Micro-Maui-ViewModelBinder-BindProperties 'Caliburn.Micro.Maui.ViewModelBinder.BindProperties')
  - [ConventionsAppliedProperty](#F-Caliburn-Micro-Maui-ViewModelBinder-ConventionsAppliedProperty 'Caliburn.Micro.Maui.ViewModelBinder.ConventionsAppliedProperty')
  - [HandleUnmatchedElements](#F-Caliburn-Micro-Maui-ViewModelBinder-HandleUnmatchedElements 'Caliburn.Micro.Maui.ViewModelBinder.HandleUnmatchedElements')
  - [ShouldApplyConventions(view)](#M-Caliburn-Micro-Maui-ViewModelBinder-ShouldApplyConventions-Microsoft-Maui-Controls-VisualElement- 'Caliburn.Micro.Maui.ViewModelBinder.ShouldApplyConventions(Microsoft.Maui.Controls.VisualElement)')
- [ViewModelLocator](#T-Caliburn-Micro-Maui-ViewModelLocator 'Caliburn.Micro.Maui.ViewModelLocator')
  - [InterfaceCaptureGroupName](#F-Caliburn-Micro-Maui-ViewModelLocator-InterfaceCaptureGroupName 'Caliburn.Micro.Maui.ViewModelLocator.InterfaceCaptureGroupName')
  - [LocateForView](#F-Caliburn-Micro-Maui-ViewModelLocator-LocateForView 'Caliburn.Micro.Maui.ViewModelLocator.LocateForView')
  - [LocateForViewType](#F-Caliburn-Micro-Maui-ViewModelLocator-LocateForViewType 'Caliburn.Micro.Maui.ViewModelLocator.LocateForViewType')
  - [LocateTypeForViewType](#F-Caliburn-Micro-Maui-ViewModelLocator-LocateTypeForViewType 'Caliburn.Micro.Maui.ViewModelLocator.LocateTypeForViewType')
  - [NameTransformer](#F-Caliburn-Micro-Maui-ViewModelLocator-NameTransformer 'Caliburn.Micro.Maui.ViewModelLocator.NameTransformer')
  - [TransformName](#F-Caliburn-Micro-Maui-ViewModelLocator-TransformName 'Caliburn.Micro.Maui.ViewModelLocator.TransformName')
  - [AddDefaultTypeMapping(viewSuffix)](#M-Caliburn-Micro-Maui-ViewModelLocator-AddDefaultTypeMapping-System-String- 'Caliburn.Micro.Maui.ViewModelLocator.AddDefaultTypeMapping(System.String)')
  - [AddNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Maui-ViewModelLocator-AddNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Maui.ViewModelLocator.AddNamespaceMapping(System.String,System.String[],System.String)')
  - [AddNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Maui-ViewModelLocator-AddNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Maui.ViewModelLocator.AddNamespaceMapping(System.String,System.String,System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix)](#M-Caliburn-Micro-Maui-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String[],System-String- 'Caliburn.Micro.Maui.ViewModelLocator.AddSubNamespaceMapping(System.String,System.String[],System.String)')
  - [AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix)](#M-Caliburn-Micro-Maui-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String,System-String- 'Caliburn.Micro.Maui.ViewModelLocator.AddSubNamespaceMapping(System.String,System.String,System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetsRegEx,viewSuffix)](#M-Caliburn-Micro-Maui-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String[],System-String- 'Caliburn.Micro.Maui.ViewModelLocator.AddTypeMapping(System.String,System.String,System.String[],System.String)')
  - [AddTypeMapping(nsSourceReplaceRegEx,nsSourceFilterRegEx,nsTargetRegEx,viewSuffix)](#M-Caliburn-Micro-Maui-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String,System-String- 'Caliburn.Micro.Maui.ViewModelLocator.AddTypeMapping(System.String,System.String,System.String,System.String)')
  - [ConfigureTypeMappings(config)](#M-Caliburn-Micro-Maui-ViewModelLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration- 'Caliburn.Micro.Maui.ViewModelLocator.ConfigureTypeMappings(Caliburn.Micro.TypeMappingConfiguration)')
  - [MakeInterface(typeName)](#M-Caliburn-Micro-Maui-ViewModelLocator-MakeInterface-System-String- 'Caliburn.Micro.Maui.ViewModelLocator.MakeInterface(System.String)')

<a name='T-Caliburn-Micro-Maui-Action'></a>
## Action `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A host for action related attached properties.

<a name='F-Caliburn-Micro-Maui-Action-TargetProperty'></a>
### TargetProperty `constants`

##### Summary

A property definition representing the target of an [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') . The DataContext of the element will be set to this instance.

<a name='F-Caliburn-Micro-Maui-Action-TargetWithoutContextProperty'></a>
### TargetWithoutContextProperty `constants`

##### Summary

A property definition representing the target of an [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') . The DataContext of the element is not set to this instance.

<a name='M-Caliburn-Micro-Maui-Action-GetTarget-Microsoft-Maui-Controls-BindableObject-'></a>
### GetTarget(d) `method`

##### Summary

Gets the target for instances of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') .

##### Returns

The target for instances of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to which the target is attached. |

<a name='M-Caliburn-Micro-Maui-Action-GetTargetWithoutContext-Microsoft-Maui-Controls-BindableObject-'></a>
### GetTargetWithoutContext(d) `method`

##### Summary

Gets the target for instances of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') .

##### Returns

The target for instances of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to which the target is attached. |

<a name='M-Caliburn-Micro-Maui-Action-HasTargetSet-Microsoft-Maui-Controls-BindableObject-'></a>
### HasTargetSet(element) `method`

##### Summary

Checks if the [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') -Target was set.

##### Returns

True if Target or TargetWithoutContext was set on `element`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | DependencyObject to check |

<a name='M-Caliburn-Micro-Maui-Action-Invoke-System-Object,System-String,Microsoft-Maui-Controls-BindableObject,Microsoft-Maui-Controls-VisualElement,System-Object,System-Object[]-'></a>
### Invoke(target,methodName,view,source,eventArgs,parameters) `method`

##### Summary

Uses the action pipeline to invoke the method.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object instance to invoke the method on. |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method to invoke. |
| view | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The view. |
| source | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') | The source of the invocation. |
| eventArgs | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The event args. |
| parameters | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The method parameters. |

<a name='M-Caliburn-Micro-Maui-Action-SetTarget-Microsoft-Maui-Controls-BindableObject,System-Object-'></a>
### SetTarget(d,target) `method`

##### Summary

Sets the target of the [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') .

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to attach the target to. |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The target for instances of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') . |

<a name='M-Caliburn-Micro-Maui-Action-SetTargetWithoutContext-Microsoft-Maui-Controls-BindableObject,System-Object-'></a>
### SetTargetWithoutContext(d,target) `method`

##### Summary

Sets the target of the [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') .

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to attach the target to. |
| target | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The target for instances of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') . |

##### Remarks

The DataContext will not be set.

<a name='T-Caliburn-Micro-Maui-ActionExecutionContext'></a>
## ActionExecutionContext `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

The context used during the execution of an Action or its guard.

<a name='F-Caliburn-Micro-Maui-ActionExecutionContext-CanExecute'></a>
### CanExecute `constants`

##### Summary

Determines whether the action can execute.

##### Remarks

Returns true if the action can execute, false otherwise.

<a name='F-Caliburn-Micro-Maui-ActionExecutionContext-EventArgs'></a>
### EventArgs `constants`

##### Summary

Any event arguments associated with the action's invocation.

<a name='F-Caliburn-Micro-Maui-ActionExecutionContext-Method'></a>
### Method `constants`

##### Summary

The actual method info to be invoked.

<a name='P-Caliburn-Micro-Maui-ActionExecutionContext-Item-System-String-'></a>
### Item `property`

##### Summary

Gets or sets additional data needed to invoke the action.

##### Returns

Custom data associated with the context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The data key. |

<a name='P-Caliburn-Micro-Maui-ActionExecutionContext-Message'></a>
### Message `property`

##### Summary

The message being executed.

<a name='P-Caliburn-Micro-Maui-ActionExecutionContext-Source'></a>
### Source `property`

##### Summary

The source from which the message originates.

<a name='P-Caliburn-Micro-Maui-ActionExecutionContext-Target'></a>
### Target `property`

##### Summary

The instance on which the action is invoked.

<a name='P-Caliburn-Micro-Maui-ActionExecutionContext-View'></a>
### View `property`

##### Summary

The view associated with the target.

<a name='M-Caliburn-Micro-Maui-ActionExecutionContext-Dispose'></a>
### Dispose() `method`

##### Summary

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Maui-ActionMessage'></a>
## ActionMessage `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.

<a name='M-Caliburn-Micro-Maui-ActionMessage-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates an instance of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage').

##### Parameters

This constructor has no parameters.

<a name='F-Caliburn-Micro-Maui-ActionMessage-ApplyAvailabilityEffect'></a>
### ApplyAvailabilityEffect `constants`

##### Summary

Applies an availability effect, such as IsEnabled, to an element.

##### Remarks

Returns a value indicating whether or not the action is available.

<a name='F-Caliburn-Micro-Maui-ActionMessage-BuildPossibleGuardNames'></a>
### BuildPossibleGuardNames `constants`

##### Summary

Returns the list of possible names of guard methods / properties for the given method.

<a name='F-Caliburn-Micro-Maui-ActionMessage-EnforceGuardsDuringInvocation'></a>
### EnforceGuardsDuringInvocation `constants`

##### Summary

Causes the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.

##### Remarks

This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.

<a name='F-Caliburn-Micro-Maui-ActionMessage-GetTargetMethod'></a>
### GetTargetMethod `constants`

##### Summary

Finds the method on the target matching the specified message.

##### Returns

The matching method, if available.

<a name='F-Caliburn-Micro-Maui-ActionMessage-InvokeAction'></a>
### InvokeAction `constants`

##### Summary

Invokes the action using the specified [ActionExecutionContext](#T-Caliburn-Micro-Maui-ActionExecutionContext 'Caliburn.Micro.Maui.ActionExecutionContext')

<a name='F-Caliburn-Micro-Maui-ActionMessage-PrepareContext'></a>
### PrepareContext `constants`

##### Summary

Prepares the action execution context for use.

<a name='F-Caliburn-Micro-Maui-ActionMessage-SetMethodBinding'></a>
### SetMethodBinding `constants`

##### Summary

Sets the target, method and view on the context. Uses a bubbling strategy by default.

<a name='F-Caliburn-Micro-Maui-ActionMessage-ThrowsExceptions'></a>
### ThrowsExceptions `constants`

##### Summary

Causes the action to throw if it cannot locate the target or the method at invocation time.

##### Remarks

True by default.

<a name='P-Caliburn-Micro-Maui-ActionMessage-Handler'></a>
### Handler `property`

##### Summary

The handler for the action.

<a name='P-Caliburn-Micro-Maui-ActionMessage-MethodName'></a>
### MethodName `property`

##### Summary

Gets or sets the name of the method to be invoked on the presentation model class.

<a name='P-Caliburn-Micro-Maui-ActionMessage-Parameters'></a>
### Parameters `property`

##### Summary

Gets the parameters to pass as part of the method invocation.

<a name='M-Caliburn-Micro-Maui-ActionMessage-Invoke-Microsoft-Maui-Controls-VisualElement-'></a>
### Invoke(sender) `method`

##### Summary

Invokes the action.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') | The Visual Element invoking the event |

<a name='M-Caliburn-Micro-Maui-ActionMessage-OnAttached'></a>
### OnAttached() `method`

##### Summary

Called after the action is attached to an AssociatedObject.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-ActionMessage-OnDetaching'></a>
### OnDetaching() `method`

##### Summary

Called when the action is being detached from its AssociatedObject, but before it has actually occurred.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-ActionMessage-ToString'></a>
### ToString() `method`

##### Summary

Returns a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that represents the current [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object').

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that represents the current [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object').

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-ActionMessage-TryFindGuardMethod-Caliburn-Micro-Maui-ActionExecutionContext,System-Collections-Generic-IEnumerable{System-String}-'></a>
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
| context | [Caliburn.Micro.Maui.ActionExecutionContext](#T-Caliburn-Micro-Maui-ActionExecutionContext 'Caliburn.Micro.Maui.ActionExecutionContext') | The execution context |
| possibleGuardNames | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | Method names to look for. |

<a name='M-Caliburn-Micro-Maui-ActionMessage-UpdateAvailability'></a>
### UpdateAvailability() `method`

##### Summary

Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Maui-ActivityEventArgs'></a>
## ActivityEventArgs `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Arguments for activity events

<a name='M-Caliburn-Micro-Maui-ActivityEventArgs-#ctor-Android-App-Activity-'></a>
### #ctor(activity) `constructor`

##### Summary

Creates a new ActivityEventArgs.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity this event corresponds to. |

<a name='P-Caliburn-Micro-Maui-ActivityEventArgs-Activity'></a>
### Activity `property`

##### Summary

The activity this event corresponds to.

<a name='T-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler'></a>
## ActivityLifecycleCallbackHandler `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Handles callbacks for the activity lifecycle and exposes them as events

<a name='M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityCreated-Android-App-Activity,Android-OS-Bundle-'></a>
### OnActivityCreated(activity,savedInstanceState) `method`

##### Summary

Invokes the ActivityCreated event

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity |
| savedInstanceState | [Android.OS.Bundle](#T-Android-OS-Bundle 'Android.OS.Bundle') | The saved instance state |

<a name='M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityDestroyed-Android-App-Activity-'></a>
### OnActivityDestroyed(activity) `method`

##### Summary

Invokes the ActivityDestroyed event

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity |

<a name='M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityPaused-Android-App-Activity-'></a>
### OnActivityPaused(activity) `method`

##### Summary

Invokes the ActivityPaused event

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity |

<a name='M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityResumed-Android-App-Activity-'></a>
### OnActivityResumed(activity) `method`

##### Summary

Invokes the ActivityResumed event

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity |

<a name='M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivitySaveInstanceState-Android-App-Activity,Android-OS-Bundle-'></a>
### OnActivitySaveInstanceState(activity,outState) `method`

##### Summary

Invokes the ActivitySaveInstanceState event

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity |
| outState | [Android.OS.Bundle](#T-Android-OS-Bundle 'Android.OS.Bundle') | The output state |

<a name='M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityStarted-Android-App-Activity-'></a>
### OnActivityStarted(activity) `method`

##### Summary

Invokes the ActivityStarted event

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity |

<a name='M-Caliburn-Micro-Maui-ActivityLifecycleCallbackHandler-OnActivityStopped-Android-App-Activity-'></a>
### OnActivityStopped(activity) `method`

##### Summary

Invokes the ActivityStopped event

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activity | [Android.App.Activity](#T-Android-App-Activity 'Android.App.Activity') | The activity |

<a name='T-Caliburn-Micro-Maui-AndroidPlatformProvider'></a>
## AndroidPlatformProvider `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider') implementation for the Xamarin Android platfrom.

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-#ctor-Android-App-Application-'></a>
### #ctor(application) `constructor`

##### Summary

Creates an instance of [AndroidPlatformProvider](#T-Caliburn-Micro-Maui-AndroidPlatformProvider 'Caliburn.Micro.Maui.AndroidPlatformProvider').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| application | [Android.App.Application](#T-Android-App-Application 'Android.App.Application') | The Android Application |

<a name='P-Caliburn-Micro-Maui-AndroidPlatformProvider-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Indicates whether or not the framework is in design-time mode.

<a name='P-Caliburn-Micro-Maui-AndroidPlatformProvider-PropertyChangeNotificationsOnUIThread'></a>
### PropertyChangeNotificationsOnUIThread `property`

##### Summary

Whether or not classes should execute property change notications on the UI thread.

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-BeginOnUIThread-System-Action-'></a>
### BeginOnUIThread(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnFirstLoad(view,handler) `method`

##### Summary

Executes the handler the fist time the view is loaded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnLayoutUpdated(view,handler) `method`

##### Summary

Executes the handler the next time the view's LayoutUpdated event fires.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view. |
| handler | [System.Action{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Object}') | The handler. |

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-GetFirstNonGeneratedView-System-Object-'></a>
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

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}-'></a>
### GetViewCloseAction(viewModel,views,dialogResult) `method`

##### Summary

Get the close action for the specified view model.

##### Returns

An [Action](#T-Caliburn-Micro-Maui-Action 'Caliburn.Micro.Maui.Action') to close the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model to close. |
| views | [System.Collections.Generic.ICollection{System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.ICollection 'System.Collections.Generic.ICollection{System.Object}') | The associated views. |
| dialogResult | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | The dialog result. |

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-OnUIThread-System-Action-'></a>
### OnUIThread(action) `method`

##### Summary

Executes the action on the UI thread.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | The action to execute. |

<a name='M-Caliburn-Micro-Maui-AndroidPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}-'></a>
### OnUIThreadAsync(action) `method`

##### Summary

Executes the action on the UI thread asynchronously.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') | The action to execute. |

<a name='T-Caliburn-Micro-Maui-AttachedCollection`1'></a>
## AttachedCollection\`1 `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A collection that can exist as part of a behavior.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of item in the attached collection. |

<a name='P-Caliburn-Micro-Maui-AttachedCollection`1-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

The currently attached object.

<a name='M-Caliburn-Micro-Maui-AttachedCollection`1-Attach-Microsoft-Maui-Controls-BindableObject-'></a>
### Attach(dependencyObject) `method`

##### Summary

Attaches the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The dependency object to attach the collection to. |

<a name='M-Caliburn-Micro-Maui-AttachedCollection`1-Detach'></a>
### Detach() `method`

##### Summary

Detaches the collection.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-AttachedCollection`1-OnCollectionChanged-System-Collections-Specialized-NotifyCollectionChangedEventArgs-'></a>
### OnCollectionChanged(e) `method`

##### Summary

Raises the [](#E-System-Collections-ObjectModel-ObservableCollection`1-CollectionChanged 'System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged') event with the provided arguments.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| e | [System.Collections.Specialized.NotifyCollectionChangedEventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Specialized.NotifyCollectionChangedEventArgs 'System.Collections.Specialized.NotifyCollectionChangedEventArgs') | Arguments of the event being raised. |

<a name='M-Caliburn-Micro-Maui-AttachedCollection`1-OnItemAdded-Microsoft-Maui-Controls-BindableObject-'></a>
### OnItemAdded(item) `method`

##### Summary

Called when an item is added from the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The item that was added. |

<a name='M-Caliburn-Micro-Maui-AttachedCollection`1-OnItemRemoved-Microsoft-Maui-Controls-BindableObject-'></a>
### OnItemRemoved(item) `method`

##### Summary

Called when an item is removed from the collection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| item | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The item that was removed. |

<a name='T-Caliburn-Micro-Maui-Bind'></a>
## Bind `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Hosts dependency properties for binding.

<a name='F-Caliburn-Micro-Maui-Bind-AtDesignTimeProperty'></a>
### AtDesignTimeProperty `constants`

##### Summary

Allows application of conventions at design-time.

<a name='F-Caliburn-Micro-Maui-Bind-ModelProperty'></a>
### ModelProperty `constants`

##### Summary

Allows binding on an existing view. Use this on root UserControls, Pages and Windows; not in a DataTemplate.

<a name='F-Caliburn-Micro-Maui-Bind-ModelWithoutContextProperty'></a>
### ModelWithoutContextProperty `constants`

##### Summary

Allows binding on an existing view without setting the data context. Use this from within a DataTemplate.

<a name='M-Caliburn-Micro-Maui-Bind-GetAtDesignTime-Microsoft-Maui-Controls-BindableObject-'></a>
### GetAtDesignTime(dependencyObject) `method`

##### Summary

Gets whether or not conventions are being applied at design-time.

##### Returns

Whether or not conventions are applied.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The ui to apply conventions to. |

<a name='M-Caliburn-Micro-Maui-Bind-GetModel-Microsoft-Maui-Controls-BindableObject-'></a>
### GetModel(dependencyObject) `method`

##### Summary

Gets the model to bind to.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The dependency object to bind to. |

<a name='M-Caliburn-Micro-Maui-Bind-GetModelWithoutContext-Microsoft-Maui-Controls-BindableObject-'></a>
### GetModelWithoutContext(dependencyObject) `method`

##### Summary

Gets the model to bind to.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The dependency object to bind to. |

<a name='M-Caliburn-Micro-Maui-Bind-SetAtDesignTime-Microsoft-Maui-Controls-BindableObject,System-Boolean-'></a>
### SetAtDesignTime(dependencyObject,value) `method`

##### Summary

Sets whether or not do bind conventions at design-time.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The ui to apply conventions to. |
| value | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether or not to apply conventions. |

<a name='M-Caliburn-Micro-Maui-Bind-SetModel-Microsoft-Maui-Controls-BindableObject,System-Object-'></a>
### SetModel(dependencyObject,value) `method`

##### Summary

Sets the model to bind to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The dependency object to bind to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='M-Caliburn-Micro-Maui-Bind-SetModelWithoutContext-Microsoft-Maui-Controls-BindableObject,System-Object-'></a>
### SetModelWithoutContext(dependencyObject,value) `method`

##### Summary

Sets the model to bind to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The dependency object to bind to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='T-Caliburn-Micro-Maui-CaliburnApplication'></a>
## CaliburnApplication `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Encapsulates the app and its available services.

<a name='M-Caliburn-Micro-Maui-CaliburnApplication-#ctor-System-IntPtr,Android-Runtime-JniHandleOwnership-'></a>
### #ctor(javaReference,transfer) `constructor`

##### Summary

Creates an instance of [CaliburnApplication](#T-Caliburn-Micro-Maui-CaliburnApplication 'Caliburn.Micro.Maui.CaliburnApplication').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| javaReference | [System.IntPtr](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IntPtr 'System.IntPtr') | A [IntPtr](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IntPtr 'System.IntPtr') which contains the `java.lang.Class` JNI value corresponding to this type. |
| transfer | [Android.Runtime.JniHandleOwnership](#T-Android-Runtime-JniHandleOwnership 'Android.Runtime.JniHandleOwnership') | How to handle ownership |

<a name='M-Caliburn-Micro-Maui-CaliburnApplication-Configure'></a>
### Configure() `method`

##### Summary

Override to configure the framework and setup your IoC container.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-CaliburnApplication-Initialize'></a>
### Initialize() `method`

##### Summary

Start the framework.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-CaliburnApplication-SelectAssemblies'></a>
### SelectAssemblies() `method`

##### Summary

Override to tell the framework where to find assemblies to inspect for views, etc.

##### Returns

A list of assemblies to inspect.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-CaliburnApplication-StartDesignTime'></a>
### StartDesignTime() `method`

##### Summary

Called by the bootstrapper's constructor at design time to start the framework.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-CaliburnApplication-StartRuntime'></a>
### StartRuntime() `method`

##### Summary

Called by the bootstrapper's constructor at runtime to start the framework.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Maui-ConventionManager'></a>
## ConventionManager `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Used to configure the conventions used by the framework to apply bindings and create actions.

<a name='F-Caliburn-Micro-Maui-ConventionManager-ApplyBindingMode'></a>
### ApplyBindingMode `constants`

##### Summary

Applies the appropriate binding mode to the binding.

<a name='F-Caliburn-Micro-Maui-ConventionManager-ApplyStringFormat'></a>
### ApplyStringFormat `constants`

##### Summary

Determines whether a custom string format is needed and applies it to the binding.

<a name='F-Caliburn-Micro-Maui-ConventionManager-ApplyUpdateSourceTrigger'></a>
### ApplyUpdateSourceTrigger `constants`

##### Summary

Determines whether a custom update source trigger should be applied to the binding.

<a name='F-Caliburn-Micro-Maui-ConventionManager-ApplyValidation'></a>
### ApplyValidation `constants`

##### Summary

Determines whether or not and what type of validation to enable on the binding.

<a name='F-Caliburn-Micro-Maui-ConventionManager-ApplyValueConverter'></a>
### ApplyValueConverter `constants`

##### Summary

Determines whether a value converter is is needed and applies one to the binding.

<a name='F-Caliburn-Micro-Maui-ConventionManager-ConfigureSelectedItem'></a>
### ConfigureSelectedItem `constants`

##### Summary

Configures the selected item convention.

<a name='F-Caliburn-Micro-Maui-ConventionManager-ConfigureSelectedItemBinding'></a>
### ConfigureSelectedItemBinding `constants`

##### Summary

Configures the SelectedItem binding for matched selection path.

<a name='F-Caliburn-Micro-Maui-ConventionManager-DefaultHeaderTemplate'></a>
### DefaultHeaderTemplate `constants`

##### Summary

The default DataTemplate used for Headered controls when required.

<a name='F-Caliburn-Micro-Maui-ConventionManager-DefaultItemTemplate'></a>
### DefaultItemTemplate `constants`

##### Summary

The default DataTemplate used for ItemsControls when required.

<a name='F-Caliburn-Micro-Maui-ConventionManager-DerivePotentialSelectionNames'></a>
### DerivePotentialSelectionNames `constants`

##### Summary

Derives the SelectedItem property name.

<a name='F-Caliburn-Micro-Maui-ConventionManager-IncludeStaticProperties'></a>
### IncludeStaticProperties `constants`

##### Summary

Indicates whether or not static properties should be included during convention name matching.

##### Remarks

False by default.

<a name='F-Caliburn-Micro-Maui-ConventionManager-OverwriteContent'></a>
### OverwriteContent `constants`

##### Summary

Indicates whether or not the Content of ContentControls should be overwritten by conventional bindings.

##### Remarks

False by default.

<a name='F-Caliburn-Micro-Maui-ConventionManager-SetBinding'></a>
### SetBinding `constants`

##### Summary

Creates a binding and sets it on the element, applying the appropriate conventions.

<a name='F-Caliburn-Micro-Maui-ConventionManager-Singularize'></a>
### Singularize `constants`

##### Summary

Changes the provided word from a plural form to a singular form.

<a name='M-Caliburn-Micro-Maui-ConventionManager-AddElementConvention-Caliburn-Micro-Maui-ElementConvention-'></a>
### AddElementConvention(convention) `method`

##### Summary

Adds an element convention.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| convention | [Caliburn.Micro.Maui.ElementConvention](#T-Caliburn-Micro-Maui-ElementConvention 'Caliburn.Micro.Maui.ElementConvention') |  |

<a name='M-Caliburn-Micro-Maui-ConventionManager-AddElementConvention``1-Microsoft-Maui-Controls-BindableProperty,System-String,System-String-'></a>
### AddElementConvention\`\`1(bindableProperty,parameterProperty,eventName) `method`

##### Summary

Adds an element convention.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bindableProperty | [Microsoft.Maui.Controls.BindableProperty](#T-Microsoft-Maui-Controls-BindableProperty 'Microsoft.Maui.Controls.BindableProperty') | The default property for binding conventions. |
| parameterProperty | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The default property for action parameters. |
| eventName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The default event to trigger actions. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of element. |

<a name='M-Caliburn-Micro-Maui-ConventionManager-ApplyHeaderTemplate-Microsoft-Maui-Controls-VisualElement,Microsoft-Maui-Controls-BindableProperty,Microsoft-Maui-Controls-BindableProperty,System-Type-'></a>
### ApplyHeaderTemplate(element,headerTemplateProperty,headerTemplateSelectorProperty,viewModelType) `method`

##### Summary

Applies a header template based on [IHaveDisplayName](#T-Caliburn-Micro-IHaveDisplayName 'Caliburn.Micro.IHaveDisplayName')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') | The element to apply the header template to. |
| headerTemplateProperty | [Microsoft.Maui.Controls.BindableProperty](#T-Microsoft-Maui-Controls-BindableProperty 'Microsoft.Maui.Controls.BindableProperty') | The depdendency property for the hdeader. |
| headerTemplateSelectorProperty | [Microsoft.Maui.Controls.BindableProperty](#T-Microsoft-Maui-Controls-BindableProperty 'Microsoft.Maui.Controls.BindableProperty') | The selector dependency property. |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the view model. |

<a name='M-Caliburn-Micro-Maui-ConventionManager-ApplyItemTemplate``1-Microsoft-Maui-Controls-ItemsView{``0},System-Reflection-PropertyInfo-'></a>
### ApplyItemTemplate\`\`1(itemsControl,property) `method`

##### Summary

Attempts to apply the default item template to the items control.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| itemsControl | [Microsoft.Maui.Controls.ItemsView{\`\`0}](#T-Microsoft-Maui-Controls-ItemsView{``0} 'Microsoft.Maui.Controls.ItemsView{``0}') | The items control. |
| property | [System.Reflection.PropertyInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.PropertyInfo 'System.Reflection.PropertyInfo') | The collection property. |

<a name='M-Caliburn-Micro-Maui-ConventionManager-GetElementConvention-System-Type-'></a>
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

<a name='M-Caliburn-Micro-Maui-ConventionManager-GetPropertyCaseInsensitive-System-Type,System-String-'></a>
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

<a name='M-Caliburn-Micro-Maui-ConventionManager-HasBinding-Microsoft-Maui-Controls-VisualElement,Microsoft-Maui-Controls-BindableProperty-'></a>
### HasBinding() `method`

##### Summary

Determines whether a particular dependency property already has a binding on the provided element.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-ConventionManager-SetBindingWithoutBindingOrValueOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-ElementConvention,Microsoft-Maui-Controls-BindableProperty-'></a>
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
| element | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') |  |
| convention | [Caliburn.Micro.Maui.ElementConvention](#T-Caliburn-Micro-Maui-ElementConvention 'Caliburn.Micro.Maui.ElementConvention') |  |
| bindableProperty | [Microsoft.Maui.Controls.BindableProperty](#T-Microsoft-Maui-Controls-BindableProperty 'Microsoft.Maui.Controls.BindableProperty') |  |

<a name='M-Caliburn-Micro-Maui-ConventionManager-SetBindingWithoutBindingOverwrite-System-Type,System-String,System-Reflection-PropertyInfo,Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-ElementConvention,Microsoft-Maui-Controls-BindableProperty-'></a>
### SetBindingWithoutBindingOverwrite() `method`

##### Summary

Creates a binding and sets it on the element, guarding against pre-existing bindings.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs'></a>
## DependencyPropertyChangedEventArgs `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Provides data for a PropertyChangedCallback implementation that is invoked when a dependency property changes its value. Also provides event data for the Control.IsEnabledChanged event and any other event that uses the DependencyPropertyChangedEventHandler delegate.

<a name='M-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-#ctor-System-Object,System-Object,Microsoft-Maui-Controls-BindableProperty-'></a>
### #ctor(newValue,oldValue,property) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| oldValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| property | [Microsoft.Maui.Controls.BindableProperty](#T-Microsoft-Maui-Controls-BindableProperty 'Microsoft.Maui.Controls.BindableProperty') |  |

<a name='P-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-NewValue'></a>
### NewValue `property`

##### Summary

Gets the value of the dependency property after the reported change.

##### Returns

The dependency property value after the change.

<a name='P-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-OldValue'></a>
### OldValue `property`

##### Summary

Gets the value of the dependency property before the reported change.

##### Returns

The dependency property value before the change.

<a name='P-Caliburn-Micro-Maui-DependencyPropertyChangedEventArgs-Property'></a>
### Property `property`

##### Summary

Gets the identifier for the dependency property where the value change occurred.

##### Returns

The identifier field of the dependency property where the value change occurred.

<a name='T-Caliburn-Micro-Maui-DependencyPropertyHelper'></a>
## DependencyPropertyHelper `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Class that abstracts the differences in creating a DepedencyProperty / BindableProperty on the different platforms.

<a name='M-Caliburn-Micro-Maui-DependencyPropertyHelper-Register-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Maui-PropertyChangedCallback-'></a>
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
| propertyChangedCallback | [Caliburn.Micro.Maui.PropertyChangedCallback](#T-Caliburn-Micro-Maui-PropertyChangedCallback 'Caliburn.Micro.Maui.PropertyChangedCallback') | Callback to executed on property changed |

<a name='M-Caliburn-Micro-Maui-DependencyPropertyHelper-RegisterAttached-System-String,System-Type,System-Type,System-Object,Caliburn-Micro-Maui-PropertyChangedCallback-'></a>
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
| propertyChangedCallback | [Caliburn.Micro.Maui.PropertyChangedCallback](#T-Caliburn-Micro-Maui-PropertyChangedCallback 'Caliburn.Micro.Maui.PropertyChangedCallback') | Callback to executed on property changed |

<a name='T-Caliburn-Micro-Maui-ElementConvention'></a>
## ElementConvention `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Represents the conventions for a particular element type.

<a name='F-Caliburn-Micro-Maui-ElementConvention-ApplyBinding'></a>
### ApplyBinding `constants`

##### Summary

Applies custom conventions for elements of this type.

##### Remarks

Pass the view model type, property path, property instance, framework element and its convention.

<a name='F-Caliburn-Micro-Maui-ElementConvention-CreateTrigger'></a>
### CreateTrigger `constants`

##### Summary

The default trigger to be used when wiring actions on this element.

<a name='F-Caliburn-Micro-Maui-ElementConvention-ElementType'></a>
### ElementType `constants`

##### Summary

The type of element to which the conventions apply.

<a name='F-Caliburn-Micro-Maui-ElementConvention-GetBindableProperty'></a>
### GetBindableProperty `constants`

##### Summary

Gets the default property to be used in binding conventions.

<a name='F-Caliburn-Micro-Maui-ElementConvention-ParameterProperty'></a>
### ParameterProperty `constants`

##### Summary

The default property to be used for parameters of this type in actions.

<a name='T-Caliburn-Micro-Maui-FormsPlatformProvider'></a>
## FormsPlatformProvider `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A [IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider') implementation for the Xamarin.Forms platfrom.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-#ctor-Caliburn-Micro-IPlatformProvider-'></a>
### #ctor(platformProvider) `constructor`

##### Summary

Creates an instance of [FormsPlatformProvider](#T-Caliburn-Micro-Maui-FormsPlatformProvider 'Caliburn.Micro.Maui.FormsPlatformProvider').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| platformProvider | [Caliburn.Micro.IPlatformProvider](#T-Caliburn-Micro-IPlatformProvider 'Caliburn.Micro.IPlatformProvider') | The existing platform provider (from the host platform) to encapsulate |

<a name='P-Caliburn-Micro-Maui-FormsPlatformProvider-InDesignMode'></a>
### InDesignMode `property`

##### Summary

*Inherit from parent.*

<a name='P-Caliburn-Micro-Maui-FormsPlatformProvider-PropertyChangeNotificationsOnUIThread'></a>
### PropertyChangeNotificationsOnUIThread `property`

##### Summary

Whether or not classes should execute property change notications on the UI thread.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-BeginOnUIThread-System-Action-'></a>
### BeginOnUIThread() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-ExecuteOnFirstLoad-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnFirstLoad() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-ExecuteOnLayoutUpdated-System-Object,System-Action{System-Object}-'></a>
### ExecuteOnLayoutUpdated() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-GetFirstNonGeneratedView-System-Object-'></a>
### GetFirstNonGeneratedView() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-GetViewCloseAction-System-Object,System-Collections-Generic-ICollection{System-Object},System-Nullable{System-Boolean}-'></a>
### GetViewCloseAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-OnUIThread-System-Action-'></a>
### OnUIThread() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-FormsPlatformProvider-OnUIThreadAsync-System-Func{System-Threading-Tasks-Task}-'></a>
### OnUIThreadAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Maui-IAttachedObject'></a>
## IAttachedObject `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Interaface usually from the Interactivity SDK's included here for completeness.

<a name='P-Caliburn-Micro-Maui-IAttachedObject-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

The currently attached object.

<a name='M-Caliburn-Micro-Maui-IAttachedObject-Attach-Microsoft-Maui-Controls-BindableObject-'></a>
### Attach(dependencyObject) `method`

##### Summary

Attached the specified dependency object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependencyObject | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') |  |

<a name='M-Caliburn-Micro-Maui-IAttachedObject-Detach'></a>
### Detach() `method`

##### Summary

Detach from the previously attached object.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Maui-IHaveParameters'></a>
## IHaveParameters `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Indicates that a message is parameterized.

<a name='P-Caliburn-Micro-Maui-IHaveParameters-Parameters'></a>
### Parameters `property`

##### Summary

Represents the parameters of a message.

<a name='T-Caliburn-Micro-Maui-INavigationService'></a>
## INavigationService `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Implemented by services that provide view and view model based navigation

<a name='M-Caliburn-Micro-Maui-INavigationService-GoBackAsync-System-Boolean-'></a>
### GoBackAsync(animated) `method`

##### Summary

Asynchronously removes the top [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') from the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Maui-INavigationService-GoBackToRootAsync-System-Boolean-'></a>
### GoBackToRootAsync(animated) `method`

##### Summary

Pops all but the root [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') off the navigation stack.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Maui-INavigationService-NavigateToViewAsync-System-Type,System-Object,System-Boolean-'></a>
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

<a name='M-Caliburn-Micro-Maui-INavigationService-NavigateToViewAsync``1-System-Object,System-Boolean-'></a>
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

<a name='M-Caliburn-Micro-Maui-INavigationService-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean-'></a>
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

<a name='M-Caliburn-Micro-Maui-INavigationService-NavigateToViewModelAsync``1-System-Object,System-Boolean-'></a>
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

<a name='T-Caliburn-Micro-Maui-MauiApplication'></a>
## MauiApplication `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A slimmed down version of the normal Caliburn Application for MAUI, used to register the navigation service and set up the initial view.

<a name='P-Caliburn-Micro-Maui-MauiApplication-RootNavigationPage'></a>
### RootNavigationPage `property`

##### Summary

The root frame of the application.

<a name='M-Caliburn-Micro-Maui-MauiApplication-BuildUp-System-Object-'></a>
### BuildUp(instance) `method`

##### Summary

Override this to provide an IoC specific implementation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| instance | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The instance to perform injection on. |

<a name='M-Caliburn-Micro-Maui-MauiApplication-Configure'></a>
### Configure() `method`

##### Summary

Override to configure the framework and setup your IoC container.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-MauiApplication-CreateApplicationPage'></a>
### CreateApplicationPage() `method`

##### Summary

Creates the root frame used by the application.

##### Returns

The frame.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-MauiApplication-DisplayRootView-System-Type-'></a>
### DisplayRootView(viewType) `method`

##### Summary

Creates the root frame and navigates to the specified view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The view type to navigate to. |

<a name='M-Caliburn-Micro-Maui-MauiApplication-DisplayRootViewForAsync-System-Type-'></a>
### DisplayRootViewForAsync(viewModelType) `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModelType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The view model type. |

<a name='M-Caliburn-Micro-Maui-MauiApplication-DisplayRootViewForAsync``1'></a>
### DisplayRootViewForAsync\`\`1() `method`

##### Summary

Locates the view model, locates the associate view, binds them and shows it as the root view.

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view model type. |

<a name='M-Caliburn-Micro-Maui-MauiApplication-DisplayRootView``1'></a>
### DisplayRootView\`\`1() `method`

##### Summary

Creates the root frame and navigates to the specified view.

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view type to navigate to. |

<a name='M-Caliburn-Micro-Maui-MauiApplication-GetAllInstances-System-Type-'></a>
### GetAllInstances(service) `method`

##### Summary

Override this to provide an IoC specific implementation

##### Returns

The located services.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The service to locate. |

<a name='M-Caliburn-Micro-Maui-MauiApplication-GetInstance-System-Type,System-String-'></a>
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

<a name='M-Caliburn-Micro-Maui-MauiApplication-Initialize'></a>
### Initialize() `method`

##### Summary

Start the framework.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-MauiApplication-PrepareViewFirst'></a>
### PrepareViewFirst() `method`

##### Summary

Allows you to trigger the creation of the RootFrame from Configure if necessary.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-MauiApplication-PrepareViewFirst-Microsoft-Maui-Controls-NavigationPage-'></a>
### PrepareViewFirst(navigationPage) `method`

##### Summary

Override this to register a navigation service.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationPage | [Microsoft.Maui.Controls.NavigationPage](#T-Microsoft-Maui-Controls-NavigationPage 'Microsoft.Maui.Controls.NavigationPage') | The root frame of the application. |

<a name='T-Caliburn-Micro-Maui-Message'></a>
## Message `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Host's attached properties related to routed UI messaging.

<a name='F-Caliburn-Micro-Maui-Message-AttachProperty'></a>
### AttachProperty `constants`

##### Summary

A property definition representing attached triggers and messages.

<a name='M-Caliburn-Micro-Maui-Message-GetAttach-Microsoft-Maui-Controls-BindableObject-'></a>
### GetAttach(d) `method`

##### Summary

Gets the attached triggers and messages.

##### Returns

The parsable attachment text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element that was attached to. |

<a name='M-Caliburn-Micro-Maui-Message-GetHandler-Microsoft-Maui-Controls-BindableObject-'></a>
### GetHandler(d) `method`

##### Summary

Gets the message handler for this element.

##### Returns

The message handler.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element. |

<a name='M-Caliburn-Micro-Maui-Message-SetAttach-Microsoft-Maui-Controls-BindableObject,System-String-'></a>
### SetAttach(d,attachText) `method`

##### Summary

Sets the attached triggers and messages.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to attach to. |
| attachText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The parsable attachment text. |

<a name='M-Caliburn-Micro-Maui-Message-SetHandler-Microsoft-Maui-Controls-BindableObject,System-Object-'></a>
### SetHandler(d,value) `method`

##### Summary

Places a message handler on this element.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The message handler. |

<a name='T-Caliburn-Micro-Maui-MessageBinder'></a>
## MessageBinder `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A service that is capable of properly binding values to a method's parameters and creating instances of [IResult](#T-Caliburn-Micro-IResult 'Caliburn.Micro.IResult').

<a name='F-Caliburn-Micro-Maui-MessageBinder-CustomConverters'></a>
### CustomConverters `constants`

##### Summary

Custom converters used by the framework registered by destination type for which they will be selected.
The converter is passed the existing value to convert and a "context" object.

<a name='F-Caliburn-Micro-Maui-MessageBinder-EvaluateParameter'></a>
### EvaluateParameter `constants`

##### Summary

Transforms the textual parameter into the actual parameter.

<a name='F-Caliburn-Micro-Maui-MessageBinder-SpecialValues'></a>
### SpecialValues `constants`

##### Summary

The special parameter values recognized by the message binder along with their resolvers.
Parameter names are case insensitive so the specified names are unique and can be used with different case variations

<a name='M-Caliburn-Micro-Maui-MessageBinder-CoerceValue-System-Type,System-Object,System-Object-'></a>
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

<a name='M-Caliburn-Micro-Maui-MessageBinder-DetermineParameters-Caliburn-Micro-Maui-ActionExecutionContext,System-Reflection-ParameterInfo[]-'></a>
### DetermineParameters(context,requiredParameters) `method`

##### Summary

Determines the parameters that a method should be invoked with.

##### Returns

The actual parameter values.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Caliburn.Micro.Maui.ActionExecutionContext](#T-Caliburn-Micro-Maui-ActionExecutionContext 'Caliburn.Micro.Maui.ActionExecutionContext') | The action execution context. |
| requiredParameters | [System.Reflection.ParameterInfo[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.ParameterInfo[] 'System.Reflection.ParameterInfo[]') | The parameters required to complete the invocation. |

<a name='M-Caliburn-Micro-Maui-MessageBinder-GetDefaultValue-System-Type-'></a>
### GetDefaultValue(type) `method`

##### Summary

Gets the default value for a type.

##### Returns

The default value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type. |

<a name='T-Caliburn-Micro-Maui-NavigateHelper`1'></a>
## NavigateHelper\`1 `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Builds a Uri in a strongly typed fashion, based on a ViewModel.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TViewModel |  |

<a name='M-Caliburn-Micro-Maui-NavigateHelper`1-AttachTo-Caliburn-Micro-Maui-INavigationService-'></a>
### AttachTo(navigationService) `method`

##### Summary

Attaches a navigation servies to this builder.

##### Returns

Itself

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.Maui.INavigationService](#T-Caliburn-Micro-Maui-INavigationService 'Caliburn.Micro.Maui.INavigationService') | The navigation service. |

<a name='M-Caliburn-Micro-Maui-NavigateHelper`1-Navigate-System-Boolean-'></a>
### Navigate() `method`

##### Summary

Navigates to the Uri represented by this builder.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-NavigateHelper`1-WithParam``1-System-Linq-Expressions-Expression{System-Func{`0,``0}},``0-'></a>
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

<a name='T-Caliburn-Micro-Maui-NavigationExtensions'></a>
## NavigationExtensions `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Extension methods for [INavigationService](#T-Caliburn-Micro-Maui-INavigationService 'Caliburn.Micro.Maui.INavigationService')

<a name='M-Caliburn-Micro-Maui-NavigationExtensions-For``1-Caliburn-Micro-Maui-INavigationService-'></a>
### For\`\`1(navigationService) `method`

##### Summary

Creates a Uri builder based on a view model type.

##### Returns

The builder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationService | [Caliburn.Micro.Maui.INavigationService](#T-Caliburn-Micro-Maui-INavigationService 'Caliburn.Micro.Maui.INavigationService') | The navigation service. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TViewModel | The type of the view model. |

<a name='T-Caliburn-Micro-Maui-NavigationPageAdapter'></a>
## NavigationPageAdapter `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Adapater around NavigationPage that implements INavigationService

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-#ctor-Microsoft-Maui-Controls-NavigationPage-'></a>
### #ctor(navigationPage) `constructor`

##### Summary

Instantiates new instance of NavigationPageAdapter

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| navigationPage | [Microsoft.Maui.Controls.NavigationPage](#T-Microsoft-Maui-Controls-NavigationPage 'Microsoft.Maui.Controls.NavigationPage') | The navigation page to adapat |

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-ActivateViewAsync-Microsoft-Maui-Controls-BindableObject-'></a>
### ActivateViewAsync(view) `method`

##### Summary

Apply logic to activate a view when it is popped onto the navigation stack

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | the view to activate |

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-CreateContentPage-Microsoft-Maui-Controls-ContentView,System-Object-'></a>
### CreateContentPage(view,viewModel) `method`

##### Summary

Allow Xamarin to navigate to a ViewModel backed by a view which is of type [ContentView](#T-Xamarin-Forms-ContentView 'Xamarin.Forms.ContentView') by adapting the result
to a [ContentPage](#T-Xamarin-Forms-ContentPage 'Xamarin.Forms.ContentPage').

##### Returns

The adapted ContentPage

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Microsoft.Maui.Controls.ContentView](#T-Microsoft-Maui-Controls-ContentView 'Microsoft.Maui.Controls.ContentView') | The view to be adapted |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model which is bound to the view |

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-DeactivateViewAsync-Microsoft-Maui-Controls-BindableObject-'></a>
### DeactivateViewAsync(view) `method`

##### Summary

Apply logic to deactivate the active view when it is popped off the navigation stack

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | the previously active view |

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-GoBackAsync-System-Boolean-'></a>
### GoBackAsync(animated) `method`

##### Summary

Asynchronously removes the top [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') from the navigation stack, with optional animation.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-GoBackToRootAsync-System-Boolean-'></a>
### GoBackToRootAsync(animated) `method`

##### Summary

Pops all but the root [Page](#T-Xamarin-Forms-Page 'Xamarin.Forms.Page') off the navigation stack.

##### Returns

The asynchrous task representing the transition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| animated | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Animate the transition |

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewAsync-System-Type,System-Object,System-Boolean-'></a>
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

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewAsync``1-System-Object,System-Boolean-'></a>
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

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewModelAsync-System-Type,System-Object,System-Boolean-'></a>
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

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-NavigateToViewModelAsync``1-System-Object,System-Boolean-'></a>
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

<a name='M-Caliburn-Micro-Maui-NavigationPageAdapter-TryInjectParameters-System-Object,System-Object-'></a>
### TryInjectParameters(viewModel,parameter) `method`

##### Summary

Attempts to inject query string parameters from the view into the view model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewModel | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The view model. |
| parameter | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The parameter. |

<a name='T-Caliburn-Micro-Maui-Parameter'></a>
## Parameter `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Represents a parameter of an [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage').

<a name='F-Caliburn-Micro-Maui-Parameter-ValueProperty'></a>
### ValueProperty `constants`

##### Summary

A dependency property representing the parameter's value.

<a name='P-Caliburn-Micro-Maui-Parameter-Owner'></a>
### Owner `property`

##### Summary

Gets or sets the owner.

<a name='P-Caliburn-Micro-Maui-Parameter-Value'></a>
### Value `property`

##### Summary

Gets or sets the value of the parameter.

<a name='M-Caliburn-Micro-Maui-Parameter-MakeAwareOf-Caliburn-Micro-Maui-ActionMessage-'></a>
### MakeAwareOf(owner) `method`

##### Summary

Makes the parameter aware of the [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') that it's attached to.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| owner | [Caliburn.Micro.Maui.ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') | The action message. |

<a name='T-Caliburn-Micro-Maui-Parser'></a>
## Parser `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Parses text into a fully functional set of [TriggerBase](#T-Microsoft-Maui-Controls-TriggerBase 'Microsoft.Maui.Controls.TriggerBase') instances with [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage').

<a name='F-Caliburn-Micro-Maui-Parser-CreateParameter'></a>
### CreateParameter `constants`

##### Summary

Function used to parse a string identified as a message parameter.

<a name='F-Caliburn-Micro-Maui-Parser-CreateTrigger'></a>
### CreateTrigger `constants`

##### Summary

The function used to generate a trigger.

##### Remarks

The parameters passed to the method are the the target of the trigger and string representing the trigger.

<a name='F-Caliburn-Micro-Maui-Parser-InterpretMessageText'></a>
### InterpretMessageText `constants`

##### Summary

Function used to parse a string identified as a message.

<a name='M-Caliburn-Micro-Maui-Parser-BindParameter-Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-Parameter,System-String,System-String,Microsoft-Maui-Controls-BindingMode-'></a>
### BindParameter(target,parameter,elementName,path,bindingMode) `method`

##### Summary

Creates a binding on a [Parameter](#T-Caliburn-Micro-Maui-Parameter 'Caliburn.Micro.Maui.Parameter').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') | The target to which the message is applied. |
| parameter | [Caliburn.Micro.Maui.Parameter](#T-Caliburn-Micro-Maui-Parameter 'Caliburn.Micro.Maui.Parameter') | The parameter object. |
| elementName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the element to bind to. |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path of the element to bind to. |
| bindingMode | [Microsoft.Maui.Controls.BindingMode](#T-Microsoft-Maui-Controls-BindingMode 'Microsoft.Maui.Controls.BindingMode') | The binding mode to use. |

<a name='M-Caliburn-Micro-Maui-Parser-CreateMessage-Microsoft-Maui-Controls-BindableObject,System-String-'></a>
### CreateMessage(target,messageText) `method`

##### Summary

Creates an instance of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') by parsing out the textual dsl.

##### Returns

The created message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The target of the message. |
| messageText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The textual message dsl. |

<a name='M-Caliburn-Micro-Maui-Parser-Parse-Microsoft-Maui-Controls-BindableObject,System-String-'></a>
### Parse(target,text) `method`

##### Summary

Parses the specified message text.

##### Returns

The triggers parsed from the text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The target. |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message text. |

<a name='T-Caliburn-Micro-Maui-PropertyChangedCallback'></a>
## PropertyChangedCallback `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Helper type for abstracting differences between dependency / bindable properties.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [T:Caliburn.Micro.Maui.PropertyChangedCallback](#T-T-Caliburn-Micro-Maui-PropertyChangedCallback 'T:Caliburn.Micro.Maui.PropertyChangedCallback') | The dependency object |

<a name='T-Caliburn-Micro-Maui-RoutedEventArgs'></a>
## RoutedEventArgs `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Helper class with abstracting Xamarin Forms.

<a name='P-Caliburn-Micro-Maui-RoutedEventArgs-OriginalSource'></a>
### OriginalSource `property`

##### Summary

Source of the event

<a name='T-Caliburn-Micro-Maui-RoutedEventHandler'></a>
## RoutedEventHandler `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Helper class with abstracting Xamarin Forms.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [T:Caliburn.Micro.Maui.RoutedEventHandler](#T-T-Caliburn-Micro-Maui-RoutedEventHandler 'T:Caliburn.Micro.Maui.RoutedEventHandler') | The sender of the event |

<a name='T-Caliburn-Micro-Maui-TriggerActionBase`1'></a>
## TriggerActionBase\`1 `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Helper class to try and abtract the differences in TriggerAction across platforms

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='P-Caliburn-Micro-Maui-TriggerActionBase`1-AssociatedObject'></a>
### AssociatedObject `property`

##### Summary

Gets or sets the object to which this [TriggerActionBase\`1](#T-Caliburn-Micro-Maui-TriggerActionBase`1 'Caliburn.Micro.Maui.TriggerActionBase`1') is attached.

<a name='M-Caliburn-Micro-Maui-TriggerActionBase`1-OnAttached'></a>
### OnAttached() `method`

##### Summary

Called after the action is attached to an AssociatedObject.

##### Parameters

This method has no parameters.

<a name='M-Caliburn-Micro-Maui-TriggerActionBase`1-OnDetaching'></a>
### OnDetaching() `method`

##### Summary

Called when the action is being detached from its AssociatedObject, but before it has actually occurred.

##### Parameters

This method has no parameters.

<a name='T-Caliburn-Micro-Maui-View'></a>
## View `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Hosts attached properties related to view models.

<a name='F-Caliburn-Micro-Maui-View-ApplyConventionsProperty'></a>
### ApplyConventionsProperty `constants`

##### Summary

A dependency property which allows the override of convention application behavior.

<a name='F-Caliburn-Micro-Maui-View-ContextProperty'></a>
### ContextProperty `constants`

##### Summary

A dependency property for assigning a context to a particular portion of the UI.

<a name='F-Caliburn-Micro-Maui-View-GetFirstNonGeneratedView'></a>
### GetFirstNonGeneratedView `constants`

##### Summary

Used to retrieve the root, non-framework-created view.

##### Remarks

In certain instances the services create UI elements.
For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.

<a name='F-Caliburn-Micro-Maui-View-IsGeneratedProperty'></a>
### IsGeneratedProperty `constants`

##### Summary

Used by the framework to indicate that this element was generated.

<a name='F-Caliburn-Micro-Maui-View-IsLoadedProperty'></a>
### IsLoadedProperty `constants`

##### Summary

A dependency property which allows the framework to track whether a certain element has already been loaded in certain scenarios.

<a name='F-Caliburn-Micro-Maui-View-IsScopeRootProperty'></a>
### IsScopeRootProperty `constants`

##### Summary

A dependency property which marks an element as a name scope root.

<a name='F-Caliburn-Micro-Maui-View-ModelProperty'></a>
### ModelProperty `constants`

##### Summary

A dependency property for attaching a model to the UI.

<a name='P-Caliburn-Micro-Maui-View-InDesignMode'></a>
### InDesignMode `property`

##### Summary

Gets a value that indicates whether the process is running in design mode.

<a name='M-Caliburn-Micro-Maui-View-ExecuteOnLoad-Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-RoutedEventHandler-'></a>
### ExecuteOnLoad(element,handler) `method`

##### Summary

Executes the handler immediately if the element is loaded, otherwise wires it to the Loaded event.

##### Returns

true if the handler was executed immediately; false otherwise

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') | The element. |
| handler | [Caliburn.Micro.Maui.RoutedEventHandler](#T-Caliburn-Micro-Maui-RoutedEventHandler 'Caliburn.Micro.Maui.RoutedEventHandler') | The handler. |

<a name='M-Caliburn-Micro-Maui-View-ExecuteOnUnload-Microsoft-Maui-Controls-VisualElement,Caliburn-Micro-Maui-RoutedEventHandler-'></a>
### ExecuteOnUnload(element,handler) `method`

##### Summary

Executes the handler when the element is unloaded.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') | The element. |
| handler | [Caliburn.Micro.Maui.RoutedEventHandler](#T-Caliburn-Micro-Maui-RoutedEventHandler 'Caliburn.Micro.Maui.RoutedEventHandler') | The handler. |

<a name='M-Caliburn-Micro-Maui-View-GetApplyConventions-Microsoft-Maui-Controls-BindableObject-'></a>
### GetApplyConventions(d) `method`

##### Summary

Gets the convention application behavior.

##### Returns

Whether or not to apply conventions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element the property is attached to. |

<a name='M-Caliburn-Micro-Maui-View-GetContext-Microsoft-Maui-Controls-BindableObject-'></a>
### GetContext(d) `method`

##### Summary

Gets the context.

##### Returns

The context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element the context is attached to. |

<a name='M-Caliburn-Micro-Maui-View-GetModel-Microsoft-Maui-Controls-BindableObject-'></a>
### GetModel(d) `method`

##### Summary

Gets the model.

##### Returns

The model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element the model is attached to. |

<a name='M-Caliburn-Micro-Maui-View-SetApplyConventions-Microsoft-Maui-Controls-BindableObject,System-Nullable{System-Boolean}-'></a>
### SetApplyConventions(d,value) `method`

##### Summary

Sets the convention application behavior.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to attach the property to. |
| value | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Whether or not to apply conventions. |

<a name='M-Caliburn-Micro-Maui-View-SetContext-Microsoft-Maui-Controls-BindableObject,System-Object-'></a>
### SetContext(d,value) `method`

##### Summary

Sets the context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to attach the context to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The context. |

<a name='M-Caliburn-Micro-Maui-View-SetModel-Microsoft-Maui-Controls-BindableObject,System-Object-'></a>
### SetModel(d,value) `method`

##### Summary

Sets the model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [Microsoft.Maui.Controls.BindableObject](#T-Microsoft-Maui-Controls-BindableObject 'Microsoft.Maui.Controls.BindableObject') | The element to attach the model to. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The model. |

<a name='T-Caliburn-Micro-Maui-ViewLocator'></a>
## ViewLocator `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A strategy for determining which view to use for a given model.

<a name='F-Caliburn-Micro-Maui-ViewLocator-ContextSeparator'></a>
### ContextSeparator `constants`

##### Summary

Separator used when resolving View names for context instances.

<a name='F-Caliburn-Micro-Maui-ViewLocator-DeterminePackUriFromType'></a>
### DeterminePackUriFromType `constants`

##### Summary

Transforms a view type into a pack uri.

<a name='F-Caliburn-Micro-Maui-ViewLocator-GetOrCreateViewType'></a>
### GetOrCreateViewType `constants`

##### Summary

Retrieves the view from the IoC container or tries to create it if not found.

##### Remarks

Pass the type of view as a parameter and recieve an instance of the view.

<a name='F-Caliburn-Micro-Maui-ViewLocator-LocateForModel'></a>
### LocateForModel `constants`

##### Summary

Locates the view for the specified model instance.

##### Returns

The view.

##### Remarks

Pass the model instance, display location (or null) and the context (or null) as parameters and receive a view instance.

<a name='F-Caliburn-Micro-Maui-ViewLocator-LocateForModelType'></a>
### LocateForModelType `constants`

##### Summary

Locates the view for the specified model type.

##### Returns

The view.

##### Remarks

Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view instance.

<a name='F-Caliburn-Micro-Maui-ViewLocator-LocateTypeForModelType'></a>
### LocateTypeForModelType `constants`

##### Summary

Locates the view type based on the specified model type.

##### Returns

The view.

##### Remarks

Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.

<a name='F-Caliburn-Micro-Maui-ViewLocator-ModifyModelTypeAtDesignTime'></a>
### ModifyModelTypeAtDesignTime `constants`

##### Summary

Modifies the name of the type to be used at design time.

<a name='F-Caliburn-Micro-Maui-ViewLocator-NameTransformer'></a>
### NameTransformer `constants`

##### Summary

Used to transform names.

<a name='F-Caliburn-Micro-Maui-ViewLocator-TransformName'></a>
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

<a name='M-Caliburn-Micro-Maui-ViewLocator-AddDefaultTypeMapping-System-String-'></a>
### AddDefaultTypeMapping(viewSuffix) `method`

##### Summary

Adds a default type mapping using the standard namespace mapping convention

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewLocator-AddNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Namespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewLocator-AddNamespaceMapping-System-String,System-String,System-String-'></a>
### AddNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewLocator-AddSubNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Subnamespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewLocator-AddSubNamespaceMapping-System-String,System-String,System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewLocator-AddTypeMapping-System-String,System-String,System-String[],System-String-'></a>
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

<a name='M-Caliburn-Micro-Maui-ViewLocator-AddTypeMapping-System-String,System-String,System-String,System-String-'></a>
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

<a name='M-Caliburn-Micro-Maui-ViewLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration-'></a>
### ConfigureTypeMappings(config) `method`

##### Summary

Specifies how type mappings are created, including default type mappings. Calling this method will
clear all existing name transformation rules and create new default type mappings according to the
configuration.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [Caliburn.Micro.TypeMappingConfiguration](#T-Caliburn-Micro-TypeMappingConfiguration 'Caliburn.Micro.TypeMappingConfiguration') | An instance of TypeMappingConfiguration that provides the settings for configuration |

<a name='M-Caliburn-Micro-Maui-ViewLocator-InitializeComponent-System-Object-'></a>
### InitializeComponent(element) `method`

##### Summary

When a view does not contain a code-behind file, we need to automatically call InitializeCompoent.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| element | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The element to initialize |

<a name='M-Caliburn-Micro-Maui-ViewLocator-RegisterViewSuffix-System-String-'></a>
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

<a name='T-Caliburn-Micro-Maui-ViewModelBinder'></a>
## ViewModelBinder `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

Binds a view to a view model.

<a name='F-Caliburn-Micro-Maui-ViewModelBinder-ApplyConventionsByDefault'></a>
### ApplyConventionsByDefault `constants`

##### Summary

Gets or sets a value indicating whether to apply conventions by default.

<a name='F-Caliburn-Micro-Maui-ViewModelBinder-Bind'></a>
### Bind `constants`

##### Summary

Binds the specified viewModel to the view.

##### Remarks

Passes the the view model, view and creation context (or null for default) to use in applying binding.

<a name='F-Caliburn-Micro-Maui-ViewModelBinder-BindActions'></a>
### BindActions `constants`

##### Summary

Attaches instances of [ActionMessage](#T-Caliburn-Micro-Maui-ActionMessage 'Caliburn.Micro.Maui.ActionMessage') to the view's controls based on the provided methods.

##### Remarks

Parameters include the named elements to search through and the type of view model to determine conventions for. Returns unmatched elements.

<a name='F-Caliburn-Micro-Maui-ViewModelBinder-BindProperties'></a>
### BindProperties `constants`

##### Summary

Creates data bindings on the view's controls based on the provided properties.

##### Remarks

Parameters include named Elements to search through and the type of view model to determine conventions for. Returns unmatched elements.

<a name='F-Caliburn-Micro-Maui-ViewModelBinder-ConventionsAppliedProperty'></a>
### ConventionsAppliedProperty `constants`

##### Summary

Indicates whether or not the conventions have already been applied to the view.

<a name='F-Caliburn-Micro-Maui-ViewModelBinder-HandleUnmatchedElements'></a>
### HandleUnmatchedElements `constants`

##### Summary

Allows the developer to add custom handling of named elements which were not matched by any default conventions.

<a name='M-Caliburn-Micro-Maui-ViewModelBinder-ShouldApplyConventions-Microsoft-Maui-Controls-VisualElement-'></a>
### ShouldApplyConventions(view) `method`

##### Summary

Determines whether a view should have conventions applied to it.

##### Returns

Whether or not conventions should be applied to the view.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| view | [Microsoft.Maui.Controls.VisualElement](#T-Microsoft-Maui-Controls-VisualElement 'Microsoft.Maui.Controls.VisualElement') | The view to check. |

<a name='T-Caliburn-Micro-Maui-ViewModelLocator'></a>
## ViewModelLocator `type`

##### Namespace

Caliburn.Micro.Maui

##### Summary

A strategy for determining which view model to use for a given view.

<a name='F-Caliburn-Micro-Maui-ViewModelLocator-InterfaceCaptureGroupName'></a>
### InterfaceCaptureGroupName `constants`

##### Summary

The name of the capture group used as a marker for rules that return interface types

<a name='F-Caliburn-Micro-Maui-ViewModelLocator-LocateForView'></a>
### LocateForView `constants`

##### Summary

Locates the view model for the specified view instance.

##### Returns

The view model.

##### Remarks

Pass the view instance as a parameters and receive a view model instance.

<a name='F-Caliburn-Micro-Maui-ViewModelLocator-LocateForViewType'></a>
### LocateForViewType `constants`

##### Summary

Locates the view model for the specified view type.

##### Returns

The view model.

##### Remarks

Pass the view type as a parameter and receive a view model instance.

<a name='F-Caliburn-Micro-Maui-ViewModelLocator-LocateTypeForViewType'></a>
### LocateTypeForViewType `constants`

##### Summary

Determines the view model type based on the specified view type.

##### Returns

The view model type.

##### Remarks

Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.

<a name='F-Caliburn-Micro-Maui-ViewModelLocator-NameTransformer'></a>
### NameTransformer `constants`

##### Summary

Used to transform names.

<a name='F-Caliburn-Micro-Maui-ViewModelLocator-TransformName'></a>
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

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-AddDefaultTypeMapping-System-String-'></a>
### AddDefaultTypeMapping(viewSuffix) `method`

##### Summary

Adds a default type mapping using the standard namespace mapping convention

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-AddNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Namespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-AddNamespaceMapping-System-String,System-String,System-String-'></a>
### AddNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping based on simple namespace mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Namespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String[],System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTargets,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTargets | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Subnamespaces of target type as an array |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-AddSubNamespaceMapping-System-String,System-String,System-String-'></a>
### AddSubNamespaceMapping(nsSource,nsTarget,viewSuffix) `method`

##### Summary

Adds a standard type mapping by substituting one subnamespace for another

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nsSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of source type |
| nsTarget | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Subnamespace of target type |
| viewSuffix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Suffix for type name. Should  be "View" or synonym of "View". (Optional) |

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String[],System-String-'></a>
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

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-AddTypeMapping-System-String,System-String,System-String,System-String-'></a>
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

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-ConfigureTypeMappings-Caliburn-Micro-TypeMappingConfiguration-'></a>
### ConfigureTypeMappings(config) `method`

##### Summary

Specifies how type mappings are created, including default type mappings. Calling this method will
clear all existing name transformation rules and create new default type mappings according to the
configuration.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [Caliburn.Micro.TypeMappingConfiguration](#T-Caliburn-Micro-TypeMappingConfiguration 'Caliburn.Micro.TypeMappingConfiguration') | An instance of TypeMappingConfiguration that provides the settings for configuration |

<a name='M-Caliburn-Micro-Maui-ViewModelLocator-MakeInterface-System-String-'></a>
### MakeInterface(typeName) `method`

##### Summary

Makes a type name into an interface name.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| typeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The part. |
