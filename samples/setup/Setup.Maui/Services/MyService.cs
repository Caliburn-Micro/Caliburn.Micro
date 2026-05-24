using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Maui.Services
{
    public class MyService : IService  // <--- This is the implementation
    {
        public string GetText()
        {
            return "Hello from MyService";
        }
    }

}
