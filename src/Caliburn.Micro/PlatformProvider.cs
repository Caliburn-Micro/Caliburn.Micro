using System;

namespace Caliburn.Micro {
    /// <summary>
    /// Access the current <see cref="IPlatformProvider"/>.
    /// </summary>
    public static class PlatformProvider {
        private static IPlatformProvider current;

        /// <summary>
        /// Gets or sets the current <see cref="IPlatformProvider"/>.
        /// </summary>
        public static IPlatformProvider Current {
            get
            {
                if (current == null)
                    throw new InvalidOperationException("PlatformProvider.Current is not set, ensure Caliburn.Micro is correctly initialized");
                return current;
            }
            set { current = value; }
        }
    }
}
