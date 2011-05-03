namespace Caliburn.Micro
{
    using System;
    using Microsoft.Phone.Shell;
    using Microsoft.Phone.Tasks;

    public class TaskManager : IHandle<TaskExecutionRequested> {
        const string TaskTypeKey = "Caliburn.Micro.TaskType";
        const string TaskIdKey = "Caliburn.Micro.TaskId";
        readonly IEventAggregator events;
        TaskExecutionRequested request;

        public TaskManager(IEventAggregator events) {
            this.events = events;
            this.events.Subscribe(this);
        }

        public void Handle(TaskExecutionRequested message) {
            var taskType = message.Task.GetType();
            var @event = taskType.GetEvent("Completed");

            if(@event != null) {
                request = message;
                @event.AddEventHandler(this, Delegate.CreateDelegate(typeof(EventHandler), this, "OnTaskComplete"));
            };

            var showMethod = taskType.GetMethod("Show");
            showMethod.Invoke(message.Task, null);
        }

        public void Tombstone() {
            if (request == null)
                return;

            PhoneApplicationService.Current.State[TaskTypeKey] = request.Task.GetType().FullName;
            PhoneApplicationService.Current.State[TaskIdKey] = request.Id ?? string.Empty;
        }

        public void Resurrect() {
            if (!PhoneApplicationService.Current.State.ContainsKey(TaskTypeKey))
                return;

            var taskTypeName = (string)PhoneApplicationService.Current.State[TaskTypeKey];
            PhoneApplicationService.Current.State.Remove(TaskTypeKey);

            object sourceId;
            if (!PhoneApplicationService.Current.State.TryGetValue(TaskIdKey, out sourceId))
                sourceId = string.Empty;
            else PhoneApplicationService.Current.State.Remove(TaskIdKey);

            var taskType = typeof(TaskEventArgs).Assembly.GetType(taskTypeName);
            var taskInstance = Activator.CreateInstance(taskType);

            request = new TaskExecutionRequested
            {
                Id = (string)sourceId,
                Task = taskInstance
            };

            var @event = taskType.GetEvent("Completed");
            @event.AddEventHandler(this, Delegate.CreateDelegate(typeof(EventHandler), this, "OnTaskComplete"));
        }

        public void OnTaskComplete(object sender, TaskEventArgs e) {
            var genericMessageType = typeof(TaskCompleted<>);
            var argsType = e.GetType();
            var messageType = genericMessageType.MakeGenericType(argsType);
            var message = Activator.CreateInstance(messageType);

            if(!string.IsNullOrEmpty(request.Id))
                messageType.GetField("Id").SetValue(message, request.Id);

            messageType.GetField("Result").SetValue(message, e);

            request = null;
            events.Publish(message); //if resurrecting, this will happen too soon...should wait until all resurrection is complete
        }
    }
}