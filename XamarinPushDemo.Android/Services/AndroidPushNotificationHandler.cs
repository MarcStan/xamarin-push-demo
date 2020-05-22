using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Threading.Tasks;
using WindowsAzure.Messaging;
using XamarinPushDemo.Droid.PushNotification;
using XamarinPushDemo.Droid.Services;
using XamarinPushDemo.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidPushNotificationHandler))]
namespace XamarinPushDemo.Droid.Services
{
    public class AndroidPushNotificationHandler : IPushNotificationHandler
    {
        private NotificationHub _hub;
        private readonly MainActivity _activity;

        public AndroidPushNotificationHandler(MainActivity activity)
        {
            _activity = activity;
        }

        public string Token => MessagingService.Token;

        public Task InitializeAsync(string notificationHubName, string connectionString)
        {
            _hub = new NotificationHub(notificationHubName, connectionString, _activity);

            // Token will be null if platform does not support push notifications (e.g. no google services installed)
            if (!string.IsNullOrEmpty(Token))
            {
                var userVisibleChannelName = "Demo Channel";
                CreateNotificationChannel(userVisibleChannelName);
                var payload = GetTemplate();
                return Task.Run(() =>
                {
                    try
                    {
                        _hub.RegisterTemplate(Token, userVisibleChannelName, payload);
                        Task.Delay(100);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                });
            }
            return Task.CompletedTask;
        }

        private void CreateNotificationChannel(string channelName)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            // the channel will be visible to the user in the "app info" section of android
            // the user can then enable/disable each channel individually or modify the noise level (sound/vibrate)
            var channel = new NotificationChannel(channelName, channelName, NotificationImportance.Default)
            {
                Description = channelName
            };

            var notificationManager = (NotificationManager)_activity.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private string GetTemplate()
        {
            return
                "{" +
                "  \"data\": {" +
                "    \"title\": \"$(title)\"," +
                "    \"message\": \"$(message)\"," +
                "  }" +
                "}";
        }
    }
}
