using System;
using Android.App;
using Android.OS;

namespace Caliburn.Micro
{
    /// <summary>
    /// Handles callbacks for the activity lifecycle and exposes them as events
    /// </summary>
    public class ActivityLifecycleCallbackHandler : Java.Lang.Object, Application.IActivityLifecycleCallbacks {

        /// <summary>
        /// Invoked when an activity is created
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivityCreated = delegate { };

        /// <summary>
        /// Invoked when an acitivty is destroyed
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivityDestoryed = delegate { };

        /// <summary>
        /// Invoked when an acitivty is paused
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivityPaused = delegate { };

        /// <summary>
        /// Invoked when an acitivty is resumed
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivityResumed = delegate { };

        /// <summary>
        /// Invoked when an acitities instance state is saved
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivitySaveInstanceState = delegate { };

        /// <summary>
        /// Invoked when an activity is started
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivityStarted = delegate { };

        /// <summary>
        /// Invoked when an activity is stopped
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivityStopped = delegate { };

        /// <summary>
        /// Invokes the ActivityCreated event
        /// </summary>
        /// <param name="activity">The activity</param>
        /// <param name="savedInstanceState">The saved instance state</param>
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) 
        {
            ActivityCreated(this, new ActivityEventArgs(activity));
        }

        /// <summary>
        /// Invokes the ActivityDestroyed event
        /// </summary>
        /// <param name="activity">The activity</param>
        public void OnActivityDestroyed(Activity activity) 
        {
            ActivityDestoryed(this, new ActivityEventArgs(activity));
        }

        /// <summary>
        /// Invokes the ActivityPaused event
        /// </summary>
        /// <param name="activity">The activity</param>
        public void OnActivityPaused(Activity activity) 
        {
            ActivityPaused(this, new ActivityEventArgs(activity));
        }

        /// <summary>
        /// Invokes the ActivityResumed event
        /// </summary>
        /// <param name="activity">The activity</param>
        public void OnActivityResumed(Activity activity) 
        {
            ActivityResumed(this, new ActivityEventArgs(activity));
        }

        /// <summary>
        /// Invokes the ActivitySaveInstanceState event
        /// </summary>
        /// <param name="activity">The activity</param>
        /// <param name="outState">The output state</param>
        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) 
        {
            ActivitySaveInstanceState(this, new ActivityEventArgs(activity));
        }

        /// <summary>
        /// Invokes the ActivityStarted event
        /// </summary>
        /// <param name="activity">The activity</param>
        public void OnActivityStarted(Activity activity) 
        {
            ActivityStarted(this, new ActivityEventArgs(activity));
        }

        /// <summary>
        /// Invokes the ActivityStopped event
        /// </summary>
        /// <param name="activity">The activity</param>
        public void OnActivityStopped(Activity activity) 
        {
            ActivityStopped(this, new ActivityEventArgs(activity));
        }
    }
}