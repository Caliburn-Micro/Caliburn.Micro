namespace Caliburn.Micro.HelloScreens.Orders {
    using System.ComponentModel.Composition;
    using Framework;

    [Export(typeof(OrderViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    
    public class OrderViewModel : DocumentBase {
        public void Save() {
            IsDirty = false;
            Dialogs.ShowMessageBox("Your data has been successfully saved.", "Data Saved");
        }
    }
}