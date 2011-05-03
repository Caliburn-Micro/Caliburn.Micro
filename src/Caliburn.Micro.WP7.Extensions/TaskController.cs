namespace Caliburn.Micro {
    using System;
    using System.Threading;
    using Microsoft.Phone.Shell;
    using Microsoft.Phone.Tasks;

    public class TaskController : IHandle<TaskExecutionRequested> {
        const string TaskTypeKey = "Caliburn.Micro.TaskType";
        const string TaskIdKey = "Caliburn.Micro.TaskId";
        readonly IEventAggregator events;
        TaskExecutionRequested request;
        bool isResurrecting;

        public TaskController(PhoneBootstrapper bootstrapper, IEventAggregator events) {
            bootstrapper.Tombstoning += OnTombstone;
            bootstrapper.Resurrecting += OnResurrect;

            this.events = events;
            this.events.Subscribe(this);
        }

        public void Handle(TaskExecutionRequested message) {
            var taskType = message.Task.GetType();
            var @event = taskType.GetEvent("Completed");

            if(@event != null) {
                request = message;
                @event.AddEventHandler(message.Task, Delegate.CreateDelegate(@event.EventHandlerType, this, "OnTaskComplete"));
            }

            var showMethod = taskType.GetMethod("Show");
            showMethod.Invoke(message.Task, null);
        }

        void OnTombstone() {
            if(request == null)
                return;

            PhoneApplicationService.Current.State[TaskTypeKey] = request.Task.GetType().FullName;
            PhoneApplicationService.Current.State[TaskIdKey] = request.Id ?? string.Empty;
        }

        void OnResurrect() {
            if(!PhoneApplicationService.Current.State.ContainsKey(TaskTypeKey))
                return;

            isResurrecting = true;

            var taskTypeName = (string)PhoneApplicationService.Current.State[TaskTypeKey];
            PhoneApplicationService.Current.State.Remove(TaskTypeKey);

            object sourceId;
            if(!PhoneApplicationService.Current.State.TryGetValue(TaskIdKey, out sourceId))
                sourceId = string.Empty;
            else
                PhoneApplicationService.Current.State.Remove(TaskIdKey);

            var taskType = typeof(TaskEventArgs).Assembly.GetType(taskTypeName);
            var taskInstance = Activator.CreateInstance(taskType);

            request = new TaskExecutionRequested
            {
                Id = (string)sourceId,
                Task = taskInstance
            };

            var @event = taskType.GetEvent("Completed");
            @event.AddEventHandler(taskInstance, Delegate.CreateDelegate(@event.EventHandlerType, this, "OnTaskComplete"));
        }

        void OnTaskComplete(object sender, EventArgs e) {
            var genericMessageType = typeof(TaskCompleted<>);
            var argsType = e.GetType();
            var messageType = genericMessageType.MakeGenericType(argsType);
            var message = Activator.CreateInstance(messageType);

            if(!string.IsNullOrEmpty(request.Id))
                messageType.GetField("Id").SetValue(message, request.Id);

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
            else events.Publish(message);
        }
    }
}