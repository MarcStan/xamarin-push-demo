using XamarinPushDemo.UWP.Services;

namespace XamarinPushDemo.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new XamarinPushDemo.App(new WindowsPushNotificationHandler()));
        }
    }
}
