using System;

using Android.App;

namespace Caliburn.Micro.Maui {
    /// <summary>
    /// Arguments for activity events.
    /// </summary>
    public class ActivityEventArgs : EventArgs {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityEventArgs"/> class.
        /// </summary>
        /// <param name="activity">The activity this event corresponds to.</param>
        public ActivityEventArgs(Activity activity)
            => Activity = activity;

        /// <summary>
        /// Gets the activity this event corresponds to.
        /// </summary>
        public Activity Activity { get; }
    }
}
