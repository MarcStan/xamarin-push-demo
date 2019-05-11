using XamarinPushDemo.UWP.Services;

namespace XamarinPushDemo.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            App.MainPageInstance = this;
            LoadApplication(new XamarinPushDemo.App(new WindowsPushNotificationHandler()));
        }
    }
}
