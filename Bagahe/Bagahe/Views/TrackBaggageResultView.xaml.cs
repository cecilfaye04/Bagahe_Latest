using Bagahe.ViewModels.Search;
using Bagahe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Bagahe.Views
{
    public partial class TrackBaggageResultView : ContentPage
    {
        public TrackBaggageResultView()
        {
            InitializeComponent();

            BindingContext = new TrackBaggageResultViewModel();
        }
    }
}
