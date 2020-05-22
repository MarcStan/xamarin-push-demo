using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;
using System.Linq;
using Xamarin.Essentials;

namespace XamarinPushDemo.Droid.PushNotification
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MessagingService : FirebaseMessagingService
    {
        public static string Token => Preferences.Get("TOKEN", "");

        public override void OnNewToken(string token)
        {
            Preferences.Set("TOKEN", token);
            base.OnNewToken(token);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            // called for every received push notification

            string title, body;
            var notification = message.GetNotification();
            if (notification == null)
            {
                // azure uses different format (needed when testing from portal 
                // and when sending regular notifications through notification hub)
                var values = message.Data;
                body = values.FirstOrDefault(p => p.Key == "message").Value;
                title = values.FirstOrDefault(p => p.Key == "title").Value;
            }
            else
            {
                // firebase console notification (only needed when manually sending notifications via firebase)
                body = notification.Body;
                title = notification.Title;
            }

            var intent = new Intent(ApplicationContext, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            const int id = 1;
            var pIntent = PendingIntent.GetActivity(ApplicationContext, id, intent, PendingIntentFlags.Immutable);

            var builder = new NotificationCompat.Builder(ApplicationContext)
                .SetSmallIcon(Resource.Drawable.logo)
                .SetContentIntent(pIntent)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetAutoCancel(true);

            var notificationManager = (NotificationManager)ApplicationContext.GetSystemService(NotificationService);

            notificationManager.Notify(id, builder.Build());
        }
    }
}
