using Android.App;
using Android.Content;
using Firebase.Iid;

namespace XamarinPushDemo.Droid.PushNotification
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class IIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var token = FirebaseInstanceId.Instance.Token;
            // refreshed tokens must be re-registered with the notification hub
        }
    }
}
