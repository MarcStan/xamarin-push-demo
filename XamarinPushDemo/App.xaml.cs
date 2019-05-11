
using XamarinPushDemo.Services;
using Xamarin.Forms;

namespace XamarinPushDemo
{
    public partial class App : Application
    {
        public App(IPushNotificationHandler pushNotificationHandler)
        {
            // get the details from your azure notification hub
            string hubName = "";
            string connectionString = "";

            pushNotificationHandler.InitializeAsync(hubName, connectionString);
            InitializeComponent();
        }
    }
}
