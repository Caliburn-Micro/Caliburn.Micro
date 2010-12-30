namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Navigation;
    using Microsoft.Phone.Shell;
    using Microsoft.Phone.Tasks;

    /// <summary>
    /// Designed to be plugged into an IoC Container, this class inspects instances to determine if they need access to the launcher/chooser mechanism.
    /// </summary>
    public class InstanceActivator
    {
        readonly Func<Type, object> getInstance;
        readonly List<ITaskManager> taskManagers = new List<ITaskManager>();
        readonly List<WeakReference> createdInstances = new List<WeakReference>();
        bool needsResurrection = true;

        /// <summary>
        /// Creates an instance of the <see cref="InstanceActivator"/>
        /// </summary>
        /// <param name="getInstance">A factory method which should take a <see cref="Type"/> and return an instance of that type from the IoC container.</param>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public InstanceActivator(PhoneBootstrapper bootstrapper, Func<Type, object> getInstance)
        {
            this.getInstance = getInstance;

            bootstrapper.PhoneService.Launching += delegate {
                needsResurrection = false;
                createdInstances.Clear();
            };

            bootstrapper.ResurrectionComplete += delegate {
                foreach (var reference in createdInstances) {
                    var instance = reference.Target;

                    if (instance != null)
                        taskManagers.Apply(x => x.CheckForTaskCompletion(instance));
                }

                needsResurrection = false;
                createdInstances.Clear();
            };
        }

        /// <summary>
        /// Tells the activator that the application should expect requests for a particular type of Chooser.
        /// </summary>
        /// <typeparam name="TChooser">The type of chooser to handle request for.</typeparam>
        /// <typeparam name="TResult">The type of result returned by the chooser.</typeparam>
        public void InstallChooser<TChooser, TResult>()
            where TChooser : ChooserBase<TResult>, new()
            where TResult : TaskEventArgs
        {
            var manager = new ChooserManager<TChooser, TResult>(this, (IPhoneService)getInstance(typeof(IPhoneService)));
            taskManagers.Add(manager);

            var navService = (INavigationService)getInstance(typeof(INavigationService));
            NavigatedEventHandler handler = null;
            handler = (s, e) =>{
                manager.PrepareToReceiveCallbacks();
                navService.Navigated -= handler;
            };
            navService.Navigated += handler;
        }

        /// <summary>
        /// Tells the activator that the application should expexct requests for a particular type of Task.
        /// </summary>
        /// <typeparam name="TLauncher">The type of task to handle the request for.</typeparam>
        public void InstallLauncher<TLauncher>()
            where TLauncher : new()
        {
            taskManagers.Add(new LauncherManager<TLauncher>());
        }

        /// <summary>
        /// Activates the specified instance by inspecting and optionally wiring for chooser/launcher requests. Additionally, inspected instances will be
        /// forwarded any Chooser results that are pending for them.
        /// </summary>
        /// <param name="instance">The instance to activate.</param>
        /// <returns>The instance that was activated.</returns>
        public object ActivateInstance(object instance)
        {
            TestForTaskLauncher(instance);

            if (needsResurrection)
                createdInstances.Add(new WeakReference(instance));

            return instance;
        }

        void TestForTaskLauncher(object instance)
        {
            var launcher = instance as ILaunchTask;
            if(launcher == null)
                return;

            launcher.TaskLaunchRequested += (s, e) =>{
                foreach(var taskManager in taskManagers)
                {
                    taskManager.TryLaunchTask(launcher, e);
                }
            };
        }

        protected virtual string GetKey(object instance, string qualifier)
        {
            var key = instance.GetType().AssemblyQualifiedName;

            var displayNamed = instance as IHaveDisplayName;
            if(displayNamed != null)
                key += "|" + displayNamed.DisplayName;

            if(!string.IsNullOrEmpty(qualifier))
                key += "|" + qualifier;

            return key;
        }

        interface ITaskManager
        {
            void TryLaunchTask(ILaunchTask launcher, TaskLaunchEventArgs args);
            void CheckForTaskCompletion(object potentialHandler);
        }

        class LauncherManager<TTask> : ITaskManager
            where TTask : new()
        {
            readonly MethodInfo showMethod;
            readonly TTask task;

            public LauncherManager()
            {
                task = new TTask();
                showMethod = typeof(TTask).GetMethod("Show", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            }

            public void TryLaunchTask(ILaunchTask launcher, TaskLaunchEventArgs args)
            {
                if (!typeof(TTask).IsAssignableFrom(args.TaskType))
                    return;

                var configurator = launcher as IConfigureTask<TTask>;
                if (configurator != null)
                    configurator.ConfigureTask(task);

                showMethod.Invoke(task, null);
            }

            public void CheckForTaskCompletion(object potentialHandler) { }
        }

        class ChooserManager<TChooser, TResult> : ITaskManager
            where TChooser : ChooserBase<TResult>, new()
            where TResult : TaskEventArgs
        {
            readonly InstanceActivator activator;
            readonly IPhoneService phoneService;
            readonly List<WeakReference> toCheck = new List<WeakReference>();
            TChooser chooser;
            bool eventHandled;
            bool eventRaised;
            TResult lastResult;
            bool handledActivate;

            public ChooserManager(InstanceActivator activator, IPhoneService phoneService)
            {
                this.activator = activator;
                this.phoneService = phoneService;
            }

            static string StateKey
            {
                get { return typeof(TChooser).AssemblyQualifiedName; }
            }

            public void TryLaunchTask(ILaunchTask launcher, TaskLaunchEventArgs args) {
                eventHandled = false;
                if(!typeof(TChooser).IsAssignableFrom(args.TaskType))
                    return;

                var configurator = launcher as IConfigureTask<TChooser>;
                if(configurator != null)
                    configurator.ConfigureTask(chooser);

                phoneService.State[StateKey] = activator.GetKey(launcher, string.Empty);
                toCheck.Clear();
                toCheck.Add(new WeakReference(launcher));
                chooser.Show();
            }

            public void CheckForTaskCompletion(object potentialHandler)
            {
                if(eventHandled)
                    return;

                var handler = potentialHandler as ILaunchChooser<TResult>;
                if(handler == null)
                    return;

                object storedClientId;
                if(!phoneService.State.TryGetValue(StateKey, out storedClientId))
                    return;

                if(eventRaised)
                    TryHandleAtLatestTime(handler, storedClientId);
                else toCheck.Add(new WeakReference(handler));
            }

            public void PrepareToReceiveCallbacks()
            {
                chooser = new TChooser();
                chooser.Completed += OnChooserCompleted;
            }

            void OnChooserCompleted(object sender, TResult e)
            {
                if (phoneService.StartupMode == StartupMode.Launch || handledActivate) {
                    toCheck.Select(weakReference => weakReference.Target)
                        .OfType<ILaunchChooser<TResult>>()
                        .First().Handle(e);
                    eventHandled = true;
                    toCheck.Clear();
                    return;
                }

                handledActivate = true;
                lastResult = e;
                eventRaised = true;

                object storedClientId;
                if(!phoneService.State.TryGetValue(StateKey, out storedClientId))
                    return;

                foreach(var weakReference in toCheck)
                {
                    var handler = weakReference.Target as ILaunchChooser<TResult>;
                    if(handler == null)
                        continue;

                    TryHandleAtLatestTime(handler, storedClientId);
                }

                toCheck.Clear();
            }

            void TryHandleAtLatestTime(ILaunchChooser<TResult> handler, object storedClientId)
            {
                var viewAware = handler as IViewAware;
                if(viewAware != null)
                {
                    var defaultView = viewAware.GetView(View.DefaultContext);

                    if(defaultView == null)
                        HandleViewAware(handler, viewAware, storedClientId);
                    else
                        HandleView(defaultView, storedClientId, handler);
                }
                else HandleActivatable(handler, storedClientId);
            }

            void HandleViewAware(ILaunchChooser<TResult> handler, IViewAware viewAware, object storedClientId)
            {
                EventHandler<ViewAttachedEventArgs> onViewAttached = null;
                onViewAttached = (s, e) =>{
                    HandleView(e.View, storedClientId, handler);
                    viewAware.ViewAttached -= onViewAttached;
                };
                viewAware.ViewAttached += onViewAttached;
            }

            void HandleView(object view, object storedClientId, ILaunchChooser<TResult> handler)
            {
                var frameworkElement = view as FrameworkElement;

                RoutedEventHandler onLoaded = null;
                onLoaded = (s, e) =>{
                    if(!eventHandled && string.Compare(storedClientId.ToString(), activator.GetKey(handler, string.Empty)) == 0)
                    {
                        handler.Handle(lastResult);
                        lastResult = null;
                        eventHandled = true;
                    }

                    frameworkElement.Loaded -= onLoaded;
                };

                if(frameworkElement != null)
                    frameworkElement.Loaded += onLoaded;
                else onLoaded(null, null);
            }

            void HandleActivatable(ILaunchChooser<TResult> handler, object storedClientId)
            {
                var activatable = handler as IActivate;
                EventHandler<ActivationEventArgs> onActivate = null;

                onActivate = (s, e) =>{
                    if(!eventHandled && e.WasInitialized && string.Compare(storedClientId.ToString(), activator.GetKey(handler, string.Empty)) == 0)
                    {
                        handler.Handle(lastResult);
                        lastResult = null;
                        eventHandled = true;
                    }

                    if(activatable != null)
                        activatable.Activated -= onActivate;
                };

                if(activatable != null && !activatable.IsActive)
                    activatable.Activated += onActivate;
                else
                {
                    onActivate(null, new ActivationEventArgs {
                        WasInitialized = true
                    });
                }
            }
        }
    }
}