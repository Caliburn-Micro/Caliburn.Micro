#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#else
namespace Caliburn.Micro
#endif 
{
    /// <summary>
    /// Indicates that a message is parameterized.
    /// </summary>
    public interface IHaveParameters {
        /// <summary>
        /// Represents the parameters of a message.
        /// </summary>
        AttachedCollection<Parameter> Parameters { get; }
    }
}