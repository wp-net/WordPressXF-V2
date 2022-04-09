using Android.App;
using Android.OS;
using Android.Views;

namespace WordPressXF.Droid
{
    [Activity(Label = "WordPressXF", Icon = "@mipmap/appicon", RoundIcon = "@mipmap/appiconround", Theme = "@style/Theme.Splash", MainLauncher = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#004d40"));

            StartActivity(typeof(MainActivity));
        }
    }
}