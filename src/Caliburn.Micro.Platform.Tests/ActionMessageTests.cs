using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class ActionMessageTests
    {

        [Fact]
        public void GetMethodPicksOverLoad()
        {
            var am = new ActionMessage();
            am.MethodName = "Overloaded";
            am.Parameters.Add(new Parameter(){ Value = "2"});
            var obj = new Overloads(); 


            var result = ActionMessage.GetTargetMethod(am, obj);
        }

        class Overloads
        {
            public void Overloaded(int s)
            {

            }

            public void Overloaded(string s)
            {

            }

            public void Overloaded(Bar s)
            {

            }

            public void Overloaded(Foo s)
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
}
