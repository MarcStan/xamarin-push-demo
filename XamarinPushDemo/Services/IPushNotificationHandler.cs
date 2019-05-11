using System.Threading.Tasks;

namespace XamarinPushDemo.Services
{
    /// <summary>
    /// Xamarin platform dependency that handles the platform specific parts of push notifications
    /// </summary>
    public interface IPushNotificationHandler
    {
        string Token { get; }

        Task InitializeAsync(string notificationHubName, string connectionString);
    }
}
