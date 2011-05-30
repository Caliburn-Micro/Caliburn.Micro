namespace Caliburn.Micro.HelloEventAggregator {
    public class LeftEvent {
        public int Number;

        public override string ToString() {
            return "Left " + Number;
        }
    }
}