using System.Threading;
using System.Threading.Tasks;

namespace PushDemp.Services
{
    public interface IPushNotificationHandler
    {
        string Token { get; }

        Task InitializeAsync(string notificationHubName, string connectionString, CancellationToken cancellationToken);
    }
}
