using Xunit;

namespace Caliburn.Micro.Core.Tests {
    public class SimpleContainer_Find_Constructor_Tests {
        [Fact]
        public void Container_Finds_Single_Constructor() {
            var container = new SimpleContainer();
            container.Singleton<SingleEmptyConstructorType>();
            container.GetInstance(typeof(SingleEmptyConstructorType), null);
        }

        [Fact]
        public void Container_No_EmptyConstructor() {
            var container = new SimpleContainer();
            container.Singleton<SingleNonEmptyConstructorType>();
            container.GetInstance(typeof(SingleNonEmptyConstructorType), null);
        }

        [Fact]
        public void Container_SingleIntConstructor() {
            var container = new SimpleContainer();
            container.Singleton<SingleIntConstructor>();
            container.RegisterInstance(typeof(int), "x", 4);
            var inst = (SingleIntConstructor)container.GetInstance(typeof(SingleIntConstructor), null);
            Assert.Equal(4, inst.Value);
        }

        [Fact]
        public void Container_ChooseConstructorWithRegisteredParameter() {
            var container = new SimpleContainer();
            container.Singleton<TwoConstructors>();
            container.RegisterInstance(typeof(int), null, 23);
            var inst = (TwoConstructors)container.GetInstance(typeof(TwoConstructors), null);
            Assert.Equal(23, inst.Value);
        }

        [Fact]
        public void Container_ChooseEmptyConstructorWithoutRegisteredParameter() {
            var container = new SimpleContainer();
            container.Singleton<TwoConstructors>();
            var inst = (TwoConstructors)container.GetInstance(typeof(TwoConstructors), null);
            Assert.Equal(42, inst.Value);
        }

        private sealed class SingleNonEmptyConstructorType {
            public SingleNonEmptyConstructorType(SingleEmptyConstructorType type)
                => _ = type;
        }

        private sealed class SingleEmptyConstructorType {
            public SingleEmptyConstructorType() {
            }
        }

        private sealed class SingleIntConstructor {
            public SingleIntConstructor(int x)
                => Value = x;

            public int Value { get; private set; }
        }

        private sealed class TwoConstructors {
            public TwoConstructors()
                => Value = 42;

            public TwoConstructors(int value)
                => Value = value;

            public int Value { get; set; }
        }
    }
}
