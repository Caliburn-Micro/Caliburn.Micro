using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro.Core;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class ConductWithTests
    {
        [Fact]
        public async Task Screen_ConductWithTests()
        {
            var root = new Screen();

            var child1 = new StateScreen
            {
                DisplayName = "screen1"
            };
            // simulate a long deactivation process 
            var child2 = new StateScreen(TimeSpan.FromSeconds(3))
            {
                DisplayName = "screen2"
            };

            var child3 = new StateScreen()
            {
                DisplayName = "screen3"
            };

            child1.ConductWith(root);
            child2.ConductWith(root);
            child3.ConductWith(root);

            await ScreenExtensions.TryActivateAsync(root).ConfigureAwait(false);

            await ScreenExtensions.TryDeactivateAsync(root, true).ConfigureAwait(false);

            Assert.True(child1.IsClosed, "child 1 should be closed");
            Assert.True(child2.IsClosed, "child 2 should be closed");
            Assert.True(child3.IsClosed, "child 3 should be closed");
        }

        [Fact]
        public async Task Conductor_ConductWithTests()
        {
            var root = new Conductor<StateScreen>.Collection.AllActive();

            var child1 = new StateScreen
            {
                DisplayName = "screen1"
            };
            var child2 = new StateScreen(TimeSpan.FromSeconds(3))
            {
                DisplayName = "screen2"
            };

            var child3 = new StateScreen()
            {
                DisplayName = "screen3"
            };

            root.Items.Add(child1);
            root.Items.Add(child2);
            root.Items.Add(child3);

            await ScreenExtensions.TryActivateAsync(root).ConfigureAwait(false);

            await ScreenExtensions.TryDeactivateAsync(root, true).ConfigureAwait(false);

            Assert.True(child1.IsClosed, "child 1 should be closed");
            Assert.True(child2.IsClosed, "child 2 should be closed");
            Assert.True(child3.IsClosed, "child 3 should be closed");
        }

        class StateScreen : Screen
        {
            public StateScreen()
            {
            }

            public StateScreen(TimeSpan? deactivationDelay)
            {
                this.deactivationDelay = deactivationDelay;
            }

            public bool IsClosed { get; private set; }
            public bool IsClosable { get; set; }

            public override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
            {
                return Task.FromResult(IsClosable);
            }

            protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken = default(CancellationToken))
            {
                await base.OnDeactivateAsync(close, cancellationToken).ConfigureAwait(false);

                if (deactivationDelay.HasValue)
                {
                    await Task.Delay(deactivationDelay.Value, cancellationToken).ConfigureAwait(false);
                }

                IsClosed = close;
            }

            private readonly TimeSpan? deactivationDelay;
        }
    }
}
