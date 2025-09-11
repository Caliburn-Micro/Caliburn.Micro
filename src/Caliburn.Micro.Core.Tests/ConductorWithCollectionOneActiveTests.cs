using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class ConductorWithCollectionOneActiveTests
    {
        private class StateScreen : Screen
        {
            public bool IsClosed { get; private set; }
            public bool IsClosable { get; set; }

            public override Task<bool> CanCloseAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(IsClosable);
            }

            protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
            {
                await base.OnDeactivateAsync(close, cancellationToken);
                IsClosed = close;
            }
        }

        private class ConductorExposeChangeActiveItem<T> : Conductor<T>.Collection.OneActive where T : Screen
        {
            public async Task ChangeActiveItemAsyncPublic(T newItem, bool closePrevious)
            {
                await ChangeActiveItemAsync(newItem, closePrevious);
            }
        }

        private class ActivateScreen : Screen
        {
            public event EventHandler RequestContinue;

            public int ActivateCalledCount { get; private set; }
            public bool AutoContinue { get; set; }

            [Obsolete("Please use OnActivatedAsync instead.")]
            protected override async Task OnActivateAsync(CancellationToken cancellationToken)
            {
                ActivateCalledCount++;
                await Task.Delay(100, cancellationToken);
                if (AutoContinue)
                    RequestContinue?.Invoke(this, EventArgs.Empty);
            }

            protected override async Task OnActivatedAsync(CancellationToken cancellationToken)
            {
                ActivateCalledCount++;
                await Task.Delay(100, cancellationToken);
                if (AutoContinue)
                    RequestContinue?.Invoke(this, EventArgs.Empty);
            }
        }

        private class ActivatedScreen : Screen
        {
            public event EventHandler RequestContinue;

            public int ActivateCalledCount { get; private set; }
            public bool AutoContinue { get; set; }

            protected override async Task OnActivatedAsync(CancellationToken cancellationToken)
            {
                ActivateCalledCount++;
                await Task.Delay(100, cancellationToken);
                if (AutoContinue)
                    RequestContinue?.Invoke(this, EventArgs.Empty);
            }
        }

        private class AsyncActivationScreen : Screen
        {
            private readonly bool _simulateAsyncOnActivate;

            private readonly bool _simulateAsyncOnDeactivate;

            private readonly TimeSpan _simulateAsyncTaskDuration;

            public AsyncActivationScreen(bool simulateAsyncOnActivate, bool simulateAsyncOnDeactivate,
                TimeSpan simulateAsyncTaskDuration)
            {
                _simulateAsyncOnActivate = simulateAsyncOnActivate;
                _simulateAsyncOnDeactivate = simulateAsyncOnDeactivate;
                _simulateAsyncTaskDuration = simulateAsyncTaskDuration;
            }

            protected override async Task OnActivatedAsync(CancellationToken cancellationToken)
            {
                await base.OnActivatedAsync(cancellationToken);

                if (_simulateAsyncOnActivate)
                {
                    // Task.Delay doesn't run within captured context
                    await Task.Delay(_simulateAsyncTaskDuration, cancellationToken);
                }
            }

            protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
            {
                if (_simulateAsyncOnDeactivate)
                {
                    // Task.Delay doesn't run within captured context
                    await Task.Delay(_simulateAsyncTaskDuration, cancellationToken);
                }

                await base.OnDeactivateAsync(close, cancellationToken);
            }
        }

        [Fact]
        public void AddedItemAppearsInChildren()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            Assert.Contains(conducted, conductor.GetChildren());
        }

        [Fact]
        public async Task CanCloseIsTrueWhenItemsAreClosable()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new StateScreen
            {
                IsClosable = true
            };
            conductor.Items.Add(conducted);

            await ((IActivate)conductor).ActivateAsync(CancellationToken.None);
            var canClose = await conductor.CanCloseAsync(CancellationToken.None);

            Assert.True(canClose);
            Assert.False(conducted.IsClosed);
        }

        [Fact(Skip = "Investigating close issue. http://caliburnmicro.codeplex.com/discussions/275824")]
        public async Task CanCloseIsTrueWhenItemsAreNotClosableAndCloseStrategyCloses()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive
            {
                CloseStrategy = new DefaultCloseStrategy<IScreen>(true)
            };
            var conducted = new StateScreen
            {
                IsClosable = true
            };
            conductor.Items.Add(conducted);
            await ((IActivate)conductor).ActivateAsync(CancellationToken.None);
            var canClose = await conductor.CanCloseAsync(CancellationToken.None);

            Assert.True(canClose);
            Assert.True(conducted.IsClosed);
        }

        [Fact]
        public async Task ChildrenAreActivatedIfConductorIsActive()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            await ((IActivate)conductor).ActivateAsync(CancellationToken.None);
            await conductor.ActivateItemAsync(conducted);
            Assert.True(conducted.IsActive);
            Assert.Equal(conducted, conductor.ActiveItem);
        }

        [Fact(Skip = "Please use OnActiveAsync carefully https://github.com/Caliburn-Micro/Caliburn.Micro/issues/789")]
        public async Task ActivateWhileActivateOneLevel()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var viewModel1 = new ActivateScreen()
            {
                DisplayName = "ViewModel 1",
                AutoContinue = true
            };
            conductor.Items.Add(viewModel1);
            var viewModel2 = new ActivateScreen() { DisplayName = "ViewModel 2" };
            conductor.Items.Add(viewModel2);
            viewModel1.RequestContinue += (sender, e) => { conductor.ActivateItemAsync(viewModel2).Wait(); };

            await conductor.ActivateAsync();
            await conductor.ActivateItemAsync(viewModel1);

            Assert.False(viewModel1.IsActive);
            Assert.Equal(1, viewModel1.ActivateCalledCount);
            Assert.True(viewModel2.IsActive);
            Assert.Equal(1, viewModel2.ActivateCalledCount);
        }

        [Fact]
        public async Task ActivatedWhileActivatedOneLevel()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var viewModel1 = new ActivatedScreen()
            {
                DisplayName = "ViewModel 1",
                AutoContinue = true
            };
            conductor.Items.Add(viewModel1);
            var viewModel2 = new ActivatedScreen() { DisplayName = "ViewModel 2" };
            conductor.Items.Add(viewModel2);
            viewModel1.RequestContinue += (sender, e) => { conductor.ActivateItemAsync(viewModel2).Wait(); };

            await conductor.ActivateAsync();
            await conductor.ActivateItemAsync(viewModel1);

            Assert.False(viewModel1.IsActive);
            Assert.Equal(1, viewModel1.ActivateCalledCount);
            Assert.True(viewModel2.IsActive);
            Assert.Equal(1, viewModel2.ActivateCalledCount);
        }

        [Fact]
        public async Task ActivateWhileActivateStackedLevels()
        {
            var outerConductor = new ConductorExposeChangeActiveItem<Screen>() { DisplayName = "Outer Conductor" };
            var somePage = new Screen();
            outerConductor.Items.Add(somePage);
            var innerConductor = new Conductor<IScreen>.Collection.OneActive() { DisplayName = "Inner Conductor" };
            outerConductor.Items.Add(innerConductor);
            var viewModel1 = new ActivatedScreen() { DisplayName = "ViewModel 1" };
            innerConductor.Items.Add(viewModel1);
            var viewModel2 = new ActivatedScreen() { DisplayName = "ViewModel 2" };
            innerConductor.Items.Add(viewModel2);
            viewModel1.RequestContinue += (sender, e) => { innerConductor.ActivateItemAsync(viewModel2).Wait(); };

            await outerConductor.ActivateAsync();
            await outerConductor.ActivateItemAsync(innerConductor);
            await innerConductor.ActivateItemAsync(viewModel1);

            await outerConductor.ChangeActiveItemAsyncPublic(somePage, false);
            Assert.True(somePage.IsActive);
            viewModel1.AutoContinue = true;
            await outerConductor.ChangeActiveItemAsyncPublic(innerConductor, true);

            Assert.True(innerConductor.IsActive);
            Assert.False(viewModel1.IsActive);
            Assert.Equal(2, viewModel1.ActivateCalledCount);
            Assert.True(viewModel2.IsActive);
            Assert.Equal(1, viewModel2.ActivateCalledCount);
        }

        [Fact(Skip = "ActiveItem currently set regardless of IsActive value. See http://caliburnmicro.codeplex.com/discussions/276375")]
        public async Task ChildrenAreNotActivatedIfConductorIsNotActive()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            await ((IActivate)conductor).ActivateAsync(CancellationToken.None);
            Assert.False(conducted.IsActive);
            Assert.NotEqual(conducted, conductor.ActiveItem);
        }

        [Fact(Skip = "Behavior currently allowed; under investigation. See http://caliburnmicro.codeplex.com/discussions/276373")]
        public void ConductorCannotConductSelf()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            Assert.Throws<InvalidOperationException>(() => conductor.Items.Add(conductor));
        }

        [Fact]
        public void ParentItemIsSetOnAddedConductedItem()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            Assert.Equal(conductor, conducted.Parent);
        }

        [Fact]
        public void ParentItemIsSetOnReplacedConductedItem()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var originalConducted = new Screen();
            conductor.Items.Add(originalConducted);
            var newConducted = new Screen();
            conductor.Items[0] = newConducted;
            Assert.Equal(conductor, newConducted.Parent);
        }

        [Fact]
        public void ParentItemIsUnsetOnClear()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            conductor.Items.Clear();
            Assert.NotEqual(conductor, conducted.Parent);
        }

        [Fact]
        public void ParentItemIsUnsetOnRemovedConductedItem()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            conductor.Items.RemoveAt(0);
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

        [Fact]
        public async Task ActiveItemSetterShouldSetThePropertySynchronouslyWhenOnActivateIsLongRunningTask()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new AsyncActivationScreen(true, false, TimeSpan.FromSeconds(1));
            conductor.Items.Add(conducted);
            await ((IActivate)conductor).ActivateAsync(CancellationToken.None);
            conductor.ActiveItem = conducted;
            Assert.NotNull(conductor.ActiveItem);
            Assert.Equal(conducted, conductor.ActiveItem);
        }

        [Fact]
        public async Task ActiveItemSetterShouldSetThePropertySynchronouslyWhenOnDeactivateIsLongRunningTask()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted1 = new AsyncActivationScreen(false, true, TimeSpan.FromSeconds(1));
            conductor.Items.Add(conducted1);
            var conducted2 = new Screen();
            conductor.Items.Add(conducted2);
            await ((IActivate)conductor).ActivateAsync(CancellationToken.None);
            conductor.ActiveItem = conducted1;
            conductor.ActiveItem = conducted2;
            Assert.NotNull(conductor.ActiveItem);
            Assert.NotEqual(conducted1, conductor.ActiveItem);
            Assert.Equal(conducted2, conductor.ActiveItem);
        }
    }
}
