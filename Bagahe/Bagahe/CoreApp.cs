using Acr.UserDialogs;
using Bagahe.ViewModels.Login;
using Bagahe.ViewModels.Search;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Async;
using Bagahe.Interfaces;
using Bagahe.Models;
using Bagahe.Services;

namespace Bagahe
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            //Mvx.RegisterSingleton<IInitializeSqliteService>(() => InitializeSqliteService.Instance);
            
            RegisterAppStart(new CustomAppStart());

        }
    }

    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public static SQLite.Net.Async.SQLiteAsyncConnection Connection { get; set; }
        public async void Start(object hint = null)
        {
            //Change code where you check if the user has previously logged in
            bool isLoggedIn = false;
            if (isLoggedIn)
            {
                //ShowViewModel<TrackBaggageViewModel>();
                ShowViewModel<TrackBaggagesViewModel>();
            }
            else
            {
                ShowViewModel<LoginViewModel>();
            }
        }
    }
}
