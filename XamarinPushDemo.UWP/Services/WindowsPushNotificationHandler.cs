using Microsoft.WindowsAzure.Messaging;
using XamarinPushDemo.Services;
using XamarinPushDemo.UWP.Services;
using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(WindowsPushNotificationHandler))]
namespace XamarinPushDemo.UWP.Services
{
    public class WindowsPushNotificationHandler : IPushNotificationHandler
    {
        private NotificationHub _hub;
        private PushNotificationChannel _channel;

        public string Token { get; private set; }

        public async Task InitializeAsync(string notificationHubName, string connectionString)
        {
            _channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            _hub = new NotificationHub(notificationHubName, connectionString);

            Token = _channel.Uri;

            _channel.PushNotificationReceived += OnPushNotificationReceived;

            // template allows to send notifications across all platforms by providing shared placeholders (title/message in this case)
            var template = GetTemplate();
            await _hub.RegisterTemplateAsync(Token, template, "myCustomTemplate");
        }

        private string GetTemplate()
        {
            return
                "<toast> " +
                "<visual>" +
                "<binding template=\"ToastGeneric\">" +
                "<text id=\"1\">$(title)</text>" +
                "<text id=\"2\">$(message)</text>" +
                "</binding>" +
                "</visual>" +
                "</toast>";
        }

        private void OnPushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            // notifications are automatically displayed to the user
            // while the app is open you can cancel notifications so they are not displayed
            e.Cancel = true;
        }
    }
}
