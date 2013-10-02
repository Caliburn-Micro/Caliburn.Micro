namespace Caliburn.Micro {
    using System;
    using System.Reflection;
    using System.Threading;
    using Microsoft.Phone.Shell;
    using Microsoft.Phone.Tasks;

    /// <summary>
    /// Handles <see cref="TaskExecutionRequested"/> messages and ensures that the property handler receives the completion message.
    /// </summary>
    public class TaskController : IHandle<TaskExecutionRequested> {
        const string TaskTypeKey = "Caliburn.Micro.TaskType";
        const string TaskSateKey = "Caliburn.Micro.TaskState";
        readonly IPhoneService phoneService;
        readonly IEventAggregator events;
        TaskExecutionRequested request;
        bool isResurrecting;
        object continueWithMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskController"/> class.
        /// </summary>
        /// <param name="phoneService">The phone service.</param>
        /// <param name="events">The event aggregator.</param>
        public TaskController(IPhoneService phoneService, IEventAggregator events) {
            this.phoneService = phoneService;
            this.events = events;
        }

        /// <summary>
        /// Starts monitoring for task requests and controlling completion messages.
        /// </summary>
        public void Start() {
            phoneService.Deactivated += OnDeactivated;
            phoneService.Resurrected += OnResurrected;
            phoneService.Continued += OnContinued;
            events.Subscribe(this);
        }

        /// <summary>
        /// Stops monitoring for task requests and controlling completion messages.
        /// </summary>
        public void Stop() {
            events.Unsubscribe(this);
            phoneService.Deactivated -= OnDeactivated;
            phoneService.Resurrected -= OnResurrected;
            phoneService.Continued -= OnContinued;
        }

        void IHandle<TaskExecutionRequested>.Handle(TaskExecutionRequested message) {
            var taskType = message.Task.GetType();

            var @event = taskType.GetEvent("Completed");
            if(@event != null) {
                request = message;
                @event.AddEventHandler(request.Task, CreateOnTaskCompletedDelegate(@event));
            }

            var showMethod = taskType.GetMethod("Show");
            showMethod.Invoke(message.Task, null);
        }

        void OnDeactivated(object sender, DeactivatedEventArgs e) {
            if (request == null) {
                return;
            }

            phoneService.State[TaskTypeKey] = request.Task.GetType().FullName;
            phoneService.State[TaskSateKey] = request.State ?? string.Empty;
        }

        void OnContinued() {
            if(continueWithMessage == null) {
                return;
            }

            events.Publish(continueWithMessage);
            continueWithMessage = null;
        }

        void OnResurrected() {
            if (!phoneService.State.ContainsKey(TaskTypeKey)) {
                return;
            }

            isResurrecting = true;

            var taskTypeName = (string)phoneService.State[TaskTypeKey];
            phoneService.State.Remove(TaskTypeKey);

            object taskState;
            if (phoneService.State.TryGetValue(TaskSateKey, out taskState))
                phoneService.State.Remove(TaskSateKey);

            var taskType = typeof(TaskEventArgs).Assembly.GetType(taskTypeName);
            var taskInstance = Activator.CreateInstance(taskType);

            request = new TaskExecutionRequested {
                State = taskState,
                Task = taskInstance
            };

            var @event = taskType.GetEvent("Completed");
            if(@event != null) {
                @event.AddEventHandler(request.Task, CreateOnTaskCompletedDelegate(@event));
            }
        }

        Delegate CreateOnTaskCompletedDelegate(EventInfo @event) {
            var methodInfo = typeof(TaskController).GetMethod("OnTaskComplete", BindingFlags.Instance | BindingFlags.NonPublic);
            var handler = Delegate.CreateDelegate(@event.EventHandlerType, this, methodInfo);
            return handler;
        }

        void OnTaskComplete(object sender, EventArgs e) {
            var genericMessageType = typeof(TaskCompleted<>);
            var argsType = e.GetType();
            var messageType = genericMessageType.MakeGenericType(argsType);
            var message = Activator.CreateInstance(messageType);

            if (request.State != null) {
                messageType.GetField("State").SetValue(message, request.State);
            }

            messageType.GetField("Result").SetValue(message, e);
            request = null;

            if(isResurrecting) {
                ThreadPool.QueueUserWorkItem(state => {
                    Thread.Sleep(500);
                    Execute.OnUIThread(() => {
                        events.Publish(message);
                        isResurrecting = false;
                    });
                });
            }
            else {
                continueWithMessage = message;
            }
        }
    }
}