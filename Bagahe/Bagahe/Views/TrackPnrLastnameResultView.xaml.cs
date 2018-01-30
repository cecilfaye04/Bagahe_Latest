using Bagahe.ViewModels.Search;
using Bagahe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MvvmCross.Platform;
using Acr.UserDialogs;
using static Bagahe.ViewModels.Search.TrackPnrLastnameResultViewModel;

namespace Bagahe.Views
{
    public partial class TrackPnrLastnameResultView : ContentPage
    {
        public TrackPnrLastnameResultView()
        {
            BindingContext = new TrackPnrLastnameResultViewModel();

            InitializeComponent();
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            //  DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.

            var x = e.SelectedItem as Bagtag;
            //bag = e.SelectedItem;
            Mvx.Resolve<IUserDialogs>().Toast(x.Bagtags.ToString(), null);
            //ListView lst = (ListView)sender;
            //lst.SelectedItem = null;


        }
    }
}
