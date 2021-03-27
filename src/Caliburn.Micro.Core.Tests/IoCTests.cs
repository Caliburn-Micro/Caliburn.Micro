using System;
using System.Collections.Generic;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class IoCGet
    {
        private class IoCReset : IDisposable
        {
            private readonly Action<object> _buildUp;
            private readonly Func<Type, IEnumerable<object>> _getAllInstances;
            private readonly Func<Type, string, object> _getInstance;

            private IoCReset()
            {
                _getInstance = IoC.GetInstance;
                _getAllInstances = IoC.GetAllInstances;
                _buildUp = IoC.BuildUp;
            }

            public void Dispose()
            {
                IoC.GetInstance = _getInstance;
                IoC.GetAllInstances = _getAllInstances;
                IoC.BuildUp = _buildUp;
            }

            public static IDisposable Create()
            {
                return new IoCReset();
            }
        }

        [Fact]
        public void A_not_initialized_BuildUp_throws_an_InvalidOperationException()
        {
            using (IoCReset.Create())
            {
                Assert.Throws<InvalidOperationException>(() => IoC.BuildUp(new object()));
            }
        }

        [Fact]
        public void A_not_initialized_GetAllInstances_throws_an_InvalidOperationException()
        {
            using (IoCReset.Create())
            {
                Assert.Throws<InvalidOperationException>(() => IoC.GetAll<object>());
            }
        }


        [Fact]
        public void A_not_initialized_GetInstance_throws_an_InvalidOperationException()
        {
            using (IoCReset.Create())
            {
                Assert.Throws<InvalidOperationException>(() => IoC.Get<object>());
            }
        }

        [Fact]
        public void A_null_GetInstance_throws_a_NullRefrenceException()
        {
            using (IoCReset.Create())
            {
                IoC.GetInstance = null;
                Assert.Throws<NullReferenceException>(() => IoC.Get<object>());
            }
        }

        [Fact]
        public void A_valid_GetAll_does_not_throw()
        {
            using (IoCReset.Create())
            {
                IoC.GetAllInstances = type => new object[] {"foo", "bar"};
                IoC.GetAll<string>();
            }
        }

        [Fact]
        public void A_valid_GetInstance_does_not_throw()
        {
            using (IoCReset.Create())
            {
                IoC.GetInstance = (type, s) => new object();
                IoC.Get<object>();
            }
        }
    }
}
