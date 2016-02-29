using System;
using Android.App;

namespace Caliburn.Micro
{
    /// <summary>
    /// Arguments for activity events
    /// </summary>
    public class ActivityEventArgs : EventArgs
    {
        private readonly Activity activity;

        /// <summary>
        /// Creates a new ActivityEventArgs.
        /// </summary>
        /// <param name="activity">The activity this event corresponds to.</param>
        public ActivityEventArgs(Activity activity) {
            this.activity = activity;
        }

        /// <summary>
        /// The activity this event corresponds to.
        /// </summary>
        public Activity Activity {
            get { return activity; }
        }
    }
}