namespace Caliburn.Micro {
    using System;
    using Microsoft.Devices;

    /// <summary>
    ///   Allows applications to start and stop vibration on the device.
    /// </summary>
    public interface IVibrateController {
        /// <summary>
        ///   Starts vibration on the device.
        /// </summary>
        /// <param name="duration"> A TimeSpan object specifying the amount of time for which the phone vibrates. </param>
        void Start(TimeSpan duration);

        /// <summary>
        ///   Stops vibration on the device.
        /// </summary>
        void Stop();
    }

    /// <summary>
    ///   The default implementation of <see cref="IVibrateController" /> , using the system controller.
    /// </summary>
    public class SystemVibrateController : IVibrateController {
        /// <summary>
        ///   Starts vibration on the device.
        /// </summary>
        /// <param name="duration"> A TimeSpan object specifying the amount of time for which the phone vibrates. </param>
        public void Start(TimeSpan duration) {
            VibrateController.Default.Start(duration);
        }

        /// <summary>
        ///   Stops vibration on the device.
        /// </summary>
        public void Stop() {
            VibrateController.Default.Stop();
        }
    }
}