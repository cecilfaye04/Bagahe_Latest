using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using MvvmCross.Droid.Views;
using Android.Views;
using MvvmCross.Platform.IoC;
using Bagahe.Interfaces;

namespace Bagahe.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
           // Mvx.RegisterSingleton<ISqliteNewService>(new SqliteNewService());
            //Xamarin.Forms.DependencyService.Register<FileService>();
            return new CoreApp();
        }


        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxPagePresenter();
            Mvx.RegisterSingleton<IMvxPageNavigationHost>(presenter);
            return presenter;
        }
    }
}