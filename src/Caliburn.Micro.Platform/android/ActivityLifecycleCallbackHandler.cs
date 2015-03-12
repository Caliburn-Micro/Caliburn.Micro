using System;
using Android.App;
using Android.OS;

namespace Caliburn.Micro
{
    public class ActivityLifecycleCallbackHandler : Java.Lang.Object, Application.IActivityLifecycleCallbacks {

        public event EventHandler<ActivityEventArgs> ActivityCreated = delegate { };
        public event EventHandler<ActivityEventArgs> ActivityDestoryed = delegate { };
        public event EventHandler<ActivityEventArgs> ActivityPaused = delegate { };
        public event EventHandler<ActivityEventArgs> ActivityResumed = delegate { };
        public event EventHandler<ActivityEventArgs> ActivitySaveInstanceState = delegate { };
        public event EventHandler<ActivityEventArgs> ActivityStarted = delegate { };
        public event EventHandler<ActivityEventArgs> ActivityStopped = delegate { };

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) 
        {
            ActivityCreated(this, new ActivityEventArgs(activity));
        }

        public void OnActivityDestroyed(Activity activity) 
        {
            ActivityDestoryed(this, new ActivityEventArgs(activity));
        }

        public void OnActivityPaused(Activity activity) 
        {
            ActivityPaused(this, new ActivityEventArgs(activity));
        }

        public void OnActivityResumed(Activity activity) 
        {
            ActivityResumed(this, new ActivityEventArgs(activity));
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) 
        {
            ActivitySaveInstanceState(this, new ActivityEventArgs(activity));
        }

        public void OnActivityStarted(Activity activity) 
        {
            ActivityStarted(this, new ActivityEventArgs(activity));
        }

        public void OnActivityStopped(Activity activity) 
        {
            ActivityStopped(this, new ActivityEventArgs(activity));
        }
    }
}