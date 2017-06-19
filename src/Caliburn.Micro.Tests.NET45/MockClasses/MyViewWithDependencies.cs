using System.Windows;

namespace Caliburn.Micro.Tests.NET45.MockClasses {
    public class MyViewWithDependencies : Window
    {
        public MyViewWithDependencies(MyViewModel viewModel) {
            
        }
    }
    
    public class MyViewModel { }
}