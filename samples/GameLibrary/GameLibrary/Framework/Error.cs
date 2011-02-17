namespace GameLibrary.Framework {
    public class Error {
        public Error(object instance, string propertyName, string message) {
            Instance = instance;
            Key = propertyName;
            Message = message;
        }

        public object Instance { get; private set; }
        public string Key { get; private set; }
        public string Message { get; private set; }
    }
}