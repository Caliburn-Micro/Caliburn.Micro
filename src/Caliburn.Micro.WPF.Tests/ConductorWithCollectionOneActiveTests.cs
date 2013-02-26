namespace Caliburn.Micro.WPF.Tests {
    using System;
    using System.Globalization;
    using System.Linq;
    using Xunit;

    public class ConductorWithCollectionOneActiveTests {
        [Fact]
        public void AddedItemAppearsInChildren() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            Assert.Contains(conducted, conductor.GetChildren());
        }

        [Fact]
        public void ParentItemIsSetOnAddedConductedItem() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            Assert.Equal(conductor, conducted.Parent);
        }

        [Fact]
        public void ParentItemIsSetOnReplacedConductedItem() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var originalConducted = new Screen();
            conductor.Items.Add(originalConducted);
            var newConducted = new Screen();
            conductor.Items[0] = newConducted;
            Assert.Equal(conductor, newConducted.Parent);
        }

        [Fact]
        public void ChildrenAreActivatedIfConductorIsActive() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            ((IActivate)conductor).Activate();
            conductor.ActivateItem(conducted);
            Assert.True(conducted.IsActive);
            Assert.Equal(conducted, conductor.ActiveItem);
        }

        [Fact]
        public void CanCloseIsTrueWhenItemsAreClosable() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new StateScreen { IsClosable = true };
            conductor.Items.Add(conducted);
            ((IActivate)conductor).Activate();
            conductor.CanClose(Assert.True);
            Assert.False(conducted.IsClosed);
        }

        [Fact(Skip = "Investigating close issue. http://caliburnmicro.codeplex.com/discussions/275824")]
        public void CanCloseIsTrueWhenItemsAreNotClosableAndCloseStrategyCloses() {
            var conductor = new Conductor<IScreen>.Collection.OneActive { CloseStrategy = new DefaultCloseStrategy<IScreen>(true) };
            var conducted = new StateScreen { IsClosable = true };
            conductor.Items.Add(conducted);
            ((IActivate)conductor).Activate();
            conductor.CanClose(Assert.True);
            Assert.True(conducted.IsClosed);
        }

        [Fact(Skip = "ActiveItem currently set regardless of IsActive value. See http://caliburnmicro.codeplex.com/discussions/276375")]
        public void ChildrenAreNotActivatedIfConductorIsNotActive() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            conductor.ActivateItem(conducted);
            Assert.False(conducted.IsActive);
            Assert.NotEqual(conducted, conductor.ActiveItem);
        }

        [Fact]
        public void ParentItemIsUnsetOnRemovedConductedItem() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            conductor.Items.RemoveAt(0);
            Assert.NotEqual(conductor, conducted.Parent);
        }

        [Fact(Skip = "This is not possible as we don't get the removed items in the event handler.")]
        public void ParentItemIsUnsetOnClear()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            conductor.Items.Clear();
            Assert.NotEqual(conductor, conducted.Parent);
        }

        [Fact]
        public void ParentItemIsUnsetOnReplaceConductedItem()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            var conducted2 = new Screen();
            conductor.Items[0] = conducted2;
            Assert.NotEqual(conductor, conducted.Parent);
            Assert.Equal(conductor, conducted2.Parent);
        }

        [Fact(Skip = "Behavior currently allowed; under investigation. See http://caliburnmicro.codeplex.com/discussions/276373")]
        public void ConductorCannotConductSelf() {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            Assert.Throws<InvalidOperationException>(() => conductor.Items.Add(conductor));
        }

        [Fact] // See http://caliburnmicro.codeplex.com/discussions/430917
        public void TryCloseStressTest()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = Enumerable.Range(0, 10000)
                .Select(i => new Screen {DisplayName = i.ToString(CultureInfo.InvariantCulture)});
            conductor.Items.AddRange(conducted);

            var defered1 = new DeferredCloseScreen {DisplayName = "d1", IsClosable = true};
            var defered2 = new DeferredCloseScreen {DisplayName = "d2", IsClosable = true};
            conductor.Items.Insert(0, defered1);
            conductor.Items.Insert(500, defered2);

            var finished = false;
            conductor.CanClose(canClose => {
                finished = true;
                Assert.True(canClose);
            });
            Assert.False(finished);

            defered1.TryClose();
            defered2.TryClose();
            Assert.True(finished);
        }

        class StateScreen : Screen {
            public Boolean IsClosed { get; private set; }
            public Boolean IsClosable { get; set; }

            public override void CanClose(Action<bool> callback) {
                callback(IsClosable);
            }

            protected override void OnDeactivate(bool close) {
                base.OnDeactivate(close);
                IsClosed = close;
            }
        }

        class DeferredCloseScreen : StateScreen {
            Action<bool> closeCallback;

            public override void CanClose(Action<bool> callback) {
                closeCallback = callback;
            }

            public override void TryClose() {
                if (closeCallback != null) {
                    closeCallback(IsClosable);
                }
            }
        }
    }
}
