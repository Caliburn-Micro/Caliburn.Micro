namespace Caliburn.Micro.HelloEventAggregator {
    using System.ComponentModel.Composition;

    [Export(typeof(IShell))]
    public class ShellViewModel : IShell {

        [ImportingConstructor]
        public ShellViewModel(LeftViewModel left, RightViewModel right) {
            Left = left;
            Right = right;
        }

        public LeftViewModel Left { get; private set; }
        public RightViewModel Right { get; private set; }
    }
}