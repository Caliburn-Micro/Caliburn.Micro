using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class ActionMessageTests
    {
        [Fact]
        public void GetTargetMethod_FindsMethod_WithMatchingNameAndParameterCount()
        {
            // Arrange
            var message = new ActionMessage();
            message.MethodName = "Foo"; // target method with zero parameters
            var target = new TestTarget();

            // Act
            var method = ActionMessage.GetTargetMethod(message, target);

            // Assert
            Assert.NotNull(method);
            Assert.Equal("Foo", method.Name);
            Assert.Empty(method.GetParameters());
        }

        [Fact]
        public void BuildPossibleGuardNames_ForAsyncMethod_IncludesCanAndNonAsyncVariant()
        {
            // Arrange
            var method = typeof(AsyncTarget).GetMethod(nameof(AsyncTarget.DoWorkAsync));

            // Act
            var guardNames = ActionMessage.BuildPossibleGuardNames(method).ToList();

            // Assert
            Assert.Contains("CanDoWorkAsync", guardNames);
            Assert.Contains("CanDoWork", guardNames);
        }

        [Fact]
        public void ApplyAvailabilityEffect_WithNullSource_ReturnsTrue()
        {
            // Arrange
            var ctx = new ActionExecutionContext
            {
                Source = null
            };

            // Act
            var result = ActionMessage.ApplyAvailabilityEffect(ctx);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InvokeAction_InvokesMethod_OnTarget()
        {
            // Arrange
            var target = new SideEffectTarget();
            var message = new ActionMessage();
            message.MethodName = nameof(SideEffectTarget.Increment);

            var ctx = new ActionExecutionContext
            {
                Message = message,
                Method = typeof(SideEffectTarget).GetMethod(nameof(SideEffectTarget.Increment)),
                Target = target,
                Source = null,
                View = null
            };

            // Act
            ActionMessage.InvokeAction(ctx);

            // Assert
            Assert.Equal(1, target.Count);
        }
        [StaFact]
        public void SetMethodBinding_UsesDataContext_When_NoHandlerInTree()
        {
            // Arrange
            var source = new Button();
            var target = new TargetWithNoArgs();
            source.DataContext = target;

            var message = new ActionMessage { MethodName = nameof(TargetWithNoArgs.Foo) };
            var ctx = new ActionExecutionContext
            {
                Message = message,
                Source = source
            };

            // Act
            ActionMessage.SetMethodBinding(ctx);

            // Assert
            Assert.NotNull(ctx.Target);
            Assert.Same(target, ctx.Target);
            Assert.NotNull(ctx.Method);
            Assert.Equal(nameof(TargetWithNoArgs.Foo), ctx.Method.Name);
            Assert.Same(source, ctx.View);
        }

        [StaFact]
        public void SetMethodBinding_FindsHandlerOnAncestor_InVisualTree()
        {
            // Arrange - create a parent container and a child element
            var parent = new StackPanel();
            var child = new Button();
            parent.Children.Add(child);

            // Attach a handler (target) to the parent using Action.SetTarget
            var target = new TargetWithArgs();
            Action.SetTarget(parent, target);

            // Message expects method 'Bar' with 1 parameter, but ActionMessage.Parameters.Count must match;
            // ActionMessage.GetTargetMethod uses Parameters.Count to match overloads.
            var message = new ActionMessage { MethodName = nameof(TargetWithArgs.Bar) };
            // add a dummy Parameter so parameter-count matches the method with one parameter
            message.Parameters.Add(new Parameter());

            var ctx = new ActionExecutionContext
            {
                Message = message,
                Source = child
            };

            // Act
            ActionMessage.SetMethodBinding(ctx);

            // Assert - SetMethodBinding should locate the handler on the parent and find the method
            Assert.NotNull(ctx.Target);
            Assert.Same(target, ctx.Target);
            Assert.NotNull(ctx.Method);
            Assert.Equal(nameof(TargetWithArgs.Bar), ctx.Method.Name);
            // View should be the element on which the handler was found (the parent)
            Assert.Same((object)parent, ctx.View);
        }

        [StaFact]
        public void SetMethodBinding_FindsHandlerOnAncestor_IsPopupe()
        {
            // Arrange - create a parent container and a child element
            var parent = new Popup();
            var child = new Button();
            parent.Child = child;

            // Attach a handler (target) to the parent using Action.SetTarget
            var target = new TargetWithArgs();
            Action.SetTarget(parent, target);

            // Message expects method 'Bar' with 1 parameter, but ActionMessage.Parameters.Count must match;
            // ActionMessage.GetTargetMethod uses Parameters.Count to match overloads.
            var message = new ActionMessage { MethodName = nameof(TargetWithArgs.Bar) };
            // add a dummy Parameter so parameter-count matches the method with one parameter
            message.Parameters.Add(new Parameter());

            var ctx = new ActionExecutionContext
            {
                Message = message,
                Source = child
            };

            // Act
            ActionMessage.SetMethodBinding(ctx);

            // Assert - SetMethodBinding should locate the handler on the parent and find the method
            Assert.NotNull(ctx.Target);
            Assert.Same(target, ctx.Target);
            Assert.NotNull(ctx.Method);
            Assert.Equal(nameof(TargetWithArgs.Bar), ctx.Method.Name);
            // View should be the element on which the handler was found (the parent)
            Assert.Same((object)child, ctx.View);
        }
        class TargetWithNoArgs
        {
            public void Foo() { }
        }

        class TargetWithArgs
        {
            public void Bar(int i) { }
        }

        // Helper types used by the tests
        class TestTarget
        {
            public void Foo() { }
            public void Foo(int i) { }
        }

        class AsyncTarget
        {
            public System.Threading.Tasks.Task DoWorkAsync() => System.Threading.Tasks.Task.CompletedTask;
        }

        class SideEffectTarget
        {
            public int Count;
            public void Increment() => Count++;
        }
    }
}
