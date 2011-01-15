namespace Caliburn.Micro.HelloScreens.Customers
{
    using System.ComponentModel.Composition;
    using Framework;

    [Export(typeof(CustomerViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerViewModel : DocumentBase
    {
        public void Save() {
            IsDirty = false;
            Dialogs.ShowMessageBox("Your data has been successfully saved.", "Data Saved");
        }

        public void EditAddress() {
            Dialogs.ShowDialog(new AddressViewModel());
        }
    }
}