using Android.App;
using Android.Content.PM;
using Android.OS;
using XamarinPushDemo.Droid.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace XamarinPushDemo.Droid
{
    [Activity(Label = "XamarinPushDemo", Icon = "@drawable/icon", RoundIcon = "@drawable/icon_round", Theme = "@style/MainTheme",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);

            LoadApplication(new App(new AndroidPushNotificationHandler(this)));
        }
    }
}
