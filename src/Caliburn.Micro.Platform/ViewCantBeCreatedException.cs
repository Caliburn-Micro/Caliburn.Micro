namespace Caliburn.Micro {
using System;

    public class ViewCantBeCreatedException: Exception {
        public ViewCantBeCreatedException(string message) : base(message) {
        }
    }
}