namespace Caliburn.Micro.Coroutines
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(ScreenOneViewModel))]
    public class ScreenOneViewModel
    {
        public IEnumerable<IResult> GoForward()
        {
            yield return Loader.Show("Downloading...");
            yield return new LoadCatalog("Caliburn.Micro.Coroutines.External.xap");
            yield return Loader.Hide();
            yield return new ShowScreen("ExternalScreen");
        }
    }
}