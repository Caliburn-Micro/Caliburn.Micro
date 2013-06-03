namespace Caliburn.Micro.WPF.Tests {
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class IoC_Get {
        class IoCReset : IDisposable {
            readonly Func<Type, string, object> getInstance;
            readonly Func<Type, IEnumerable<object>> getAllInstances;
            readonly Action<object> buildUp;

            private IoCReset() {
                getInstance = IoC.GetInstance;
                getAllInstances = IoC.GetAllInstances;
                buildUp = IoC.BuildUp;
            }

            public void Dispose() {
                IoC.GetInstance = getInstance;
                IoC.GetAllInstances = getAllInstances;
                IoC.BuildUp = buildUp;
            }

            public static IDisposable Create() {
                return new IoCReset();
            }
        }


        [Fact]
        public void A_not_initialized_GetInstance_throws_an_InvalidOperationException() {
            using (IoCReset.Create()) {
                Assert.Throws<InvalidOperationException>(() => IoC.Get<Object>());
            }
        }

        [Fact]
        public void A_not_initialized_GetAllInstances_throws_an_InvalidOperationException() {
            using (IoCReset.Create()) {
                Assert.Throws<InvalidOperationException>(() => IoC.GetAll<Object>());
            }
        }

        [Fact]
        public void A_not_initialized_BuildUp_throws_an_InvalidOperationException() {
            using (IoCReset.Create()) {
                Assert.Throws<InvalidOperationException>(() => IoC.BuildUp(new object()));
            }
        }

        [Fact]
        public void A_null_GetInstance_throws_a_NullRefrenceException() {
            using (IoCReset.Create()) {
                IoC.GetInstance = null;
                Assert.Throws<NullReferenceException>(() => IoC.Get<Object>());
            }
        }

        [Fact]
        public void A_valid_GetInstance_does_not_throw() {
            using (IoCReset.Create()) {
                IoC.GetInstance = (type, s) => new object();
                Assert.DoesNotThrow(() => IoC.Get<Object>());
            }
        }

        [Fact]
        public void A_valid_GetAll_does_not_throw() {
            using (IoCReset.Create()) {
                IoC.GetAllInstances = type => new object[] {"foo", "bar"};
                Assert.DoesNotThrow(() => IoC.GetAll<string>());
            }
        }
    }
}