using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class ActionMessageTests
    {
        [Fact]
        public void GetMethodPicksOverLoadStructParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = 1 });
            am.Parameters.Add(new Parameter() { Value = 1 });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded(1, 1));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadStructStringParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = 1 });
            am.Parameters.Add(new Parameter() { Value = 1 });
            am.Parameters.Add(new Parameter() { Value = "" });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded(1, 1, ""));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadStringParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = "" });
            am.Parameters.Add(new Parameter() { Value = "" });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded("", ""));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadStringStructParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = "" });
            am.Parameters.Add(new Parameter() { Value = "" });
            am.Parameters.Add(new Parameter() { Value = 1 });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded("", "", 1));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadTwoDifferntParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = new Foo() });
            am.Parameters.Add(new Parameter() { Value = new Bar() });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded(new Foo(), new Bar()));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadTwoBaseParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = new Foo() });
            am.Parameters.Add(new Parameter() { Value = new Foo() });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded(new Foo(), new Foo()));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadTwoDerivedParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = new Bar() });
            am.Parameters.Add(new Parameter() { Value = new Bar() });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded(new Bar(), new Bar()));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadEnumParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = OverloadEnum.One });
            var obj = new Overloads();

            var expected = MethodInfoHelper.GetMethodInfo<Overloads>(o => o.Overloaded(OverloadEnum.One));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadDerivedOnlyParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = new Bar() });
            am.Parameters.Add(new Parameter() { Value = new Bar() });
            var obj = new OverloadsDerivedOnly();

            var expected = MethodInfoHelper.GetMethodInfo<OverloadsDerivedOnly>(o => o.Overloaded(new Bar(), new Bar()));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadDerivedOnlyBaseParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = new Foo() });
            am.Parameters.Add(new Parameter() { Value = new Bar() });
            var obj = new OverloadsDerivedOnly();

            MethodInfo expected = null;

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }


        [Fact]
        public void GetMethodPicksOverLoadBaseOnlyParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = new Bar() });
            am.Parameters.Add(new Parameter() { Value = new Bar() });
            var obj = new OverloadsBaseOnly();

            var expected = MethodInfoHelper.GetMethodInfo<OverloadsBaseOnly>(o => o.Overloaded(new Foo(), new Foo()));

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetMethodPicksOverLoadBaseOnlyBaseParameter()
        {
            //Arrange
            var am = new ActionMessage();
            am.MethodName = "Overloaded";

            am.Parameters.Add(new Parameter() { Value = new Foo() });
            am.Parameters.Add(new Parameter() { Value = new Bar() });
            var obj = new OverloadsBaseOnly();

            MethodInfo expected =  MethodInfoHelper.GetMethodInfo<OverloadsBaseOnly>(o => o.Overloaded(new Foo(), new Foo()));
            ;

            //Act
            var result = ActionMessage.GetTargetMethod(am, obj);

            //Assert
            Assert.Equal(result, expected);
        }

        enum OverloadEnum
        {
            One,Two,Three
        }

        class OverloadsBaseOnly
        {
            public void Overloaded(Foo f)
            {

            }

            public void Overloaded(Foo f, Foo f2)
            {

            }
        }

        class OverloadsDerivedOnly
        {
            public void Overloaded(Bar b)
            {

            }

            public void Overloaded(Bar b, Bar b2)
            {

            }
        }

        class Overloads
        {
            public void Overloaded(int i)
            {

            }

            public void Overloaded(int i, int i2)
            {

            }

            public void Overloaded(int i, int i2, string s)
            {

            }

            public void Overloaded(string s)
            {

            }

            public void Overloaded(string s, string s2)
            {

            }

            public void Overloaded(string s, string s2, int i)
            {

            }

            public void Overloaded(Foo F)
            {

            }

            public void Overloaded(Bar B)
            {

            }

            public void Overloaded(OverloadEnum E)
            {

            }

            public void Overloaded(Foo s, Foo f)
            {

            }

            public void Overloaded(Foo s, Bar b)
            {

            }

            public void Overloaded(Bar s, Bar b)
            {

            }

            public void Overloaded(Bar s, Foo f)
            {

            }
        }

        class Foo
        {

        }

        class Bar : Foo
        {

        }
    }

    public static class MethodInfoHelper
    {
        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
        {
            var member = expression.Body as MethodCallExpression;

            if (member != null)
                return member.Method;

            throw new ArgumentException("Expression is not a method", "expression");
        }

    }
}
