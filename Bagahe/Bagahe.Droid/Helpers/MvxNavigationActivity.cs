using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using MvvmCross.Platform;
using Xamarin.Forms.Platform.Android;
using Acr.UserDialogs;
using Bagahe.Helpers;
using Bagahe.Views;

namespace Bagahe.Droid
{
    [Activity(Theme = "@style/Theme.MainBackground", Label = "BAGAHE",
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MvxNavigationActivity
    : FormsApplicationActivity, IMvxPageNavigationProvider
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            Mvx.Resolve<IMvxPageNavigationHost>().NavigationProvider = this;
            Mvx.Resolve<IMvxAppStart>().Start();

        }

        public async void Push(Page page)
        {
            if (MvxNavigationActivity.NavigationPage != null)
            {
                await MvxNavigationActivity.NavigationPage.PushAsync(page);
                return;
            }

            MvxNavigationActivity.NavigationPage = new NavigationPage(page);
            SetPage(MvxNavigationActivity.NavigationPage);
        }

        public async void Pop()
        {
            if (MvxNavigationActivity.NavigationPage == null)
                return;

            await MvxNavigationActivity.NavigationPage.PopAsync();
        }

        public static NavigationPage NavigationPage { get; set; }


    }
}