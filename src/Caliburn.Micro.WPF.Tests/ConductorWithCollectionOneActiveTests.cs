using System;
using Xunit;

namespace Caliburn.Micro.WPF.Tests
{
    public class ConductorWithCollectionOneActiveTests
    {
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
        public void ChildrenAreActivatedIfConductorIsActive()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            ((IActivate)conductor).Activate();
            conductor.ActivateItem(conducted);
            Assert.True(conducted.IsActive);
            Assert.Equal(conducted, conductor.ActiveItem);
        }

        [Fact(Skip = "ActiveItem currently set regardless of IsActive value. See [discussion:276374]")]
        public void ChildrenAreNotActivatedIfConductorIsNotActive()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            conductor.ActivateItem(conducted);
            Assert.False(conducted.IsActive);
            Assert.NotEqual(conducted, conductor.ActiveItem);
        }

        [Fact(Skip = "Parent value not currently updated when conducted item is removed. See [discussion:276374]")]
        public void ParentItemIsUnsetOnRemovedConductedItem()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            var conducted = new Screen();
            conductor.Items.Add(conducted);
            conductor.Items.RemoveAt(0);
            Assert.NotEqual(conductor, conducted.Parent);
        }

        [Fact(Skip = "Behavior currently allowed; under investigation. See [discussion:276373]")]
        public void ConductorCannotConductSelf()
        {
            var conductor = new Conductor<IScreen>.Collection.OneActive();
            Assert.Throws<InvalidOperationException>(() => conductor.Items.Add(conductor));
        }
    }
}
