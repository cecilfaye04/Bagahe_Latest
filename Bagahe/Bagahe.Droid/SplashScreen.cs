using Android.App;
using Android.Content.PM;
using Android.Views;
using MvvmCross.Droid.Views;

namespace Bagahe.Droid
{
    [Activity(
       Label = "Bagahe"
       , MainLauncher = true
       , Icon = "@drawable/icon"
       , Theme = "@style/Theme.Splash"
       , NoHistory = true
       , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public override void InitializationComplete()
        {
            StartActivity(typeof(MvxNavigationActivity));
        }

    }
}