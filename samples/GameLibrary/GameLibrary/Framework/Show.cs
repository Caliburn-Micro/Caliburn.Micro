namespace GameLibrary.Framework {
    using Caliburn.Micro;

    public static class Show {
        public static OpenChildResult<TChild> Child<TChild>()
            where TChild : IScreen
        {
            return new OpenChildResult<TChild>();
        }

        public static IResult Busy() {
            return new BusyResult(false);
        }

        public static IResult NotBusy() {
            return new BusyResult(true);
        }
    }
}