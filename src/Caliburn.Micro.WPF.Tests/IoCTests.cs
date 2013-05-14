namespace Caliburn.Micro.WPF.Tests {
    using System;
    using Xunit;

    public class IoC_Get {
        [Fact]
        public void A_null_GetInstance_throws_an_NullRefrenceException() {
            IoC.GetInstance = null;
            
            Assert.Throws<NullReferenceException>(() => IoC.Get<Object>());
        }

        [Fact]
        public void A_valid_GetInstance_does_not_throw() {
            IoC.GetInstance = (type, s) => new object();
            
            Assert.DoesNotThrow(() => IoC.Get<Object>());
        }
    }
}