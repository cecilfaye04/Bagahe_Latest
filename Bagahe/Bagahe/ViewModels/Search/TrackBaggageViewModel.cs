using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bagahe.ViewModels.Search
{
    class TrackBaggageViewModel : BaseViewModel
    {
        public TrackBaggageViewModel()
        {
            this.ScanBagTagCommand = new Command(() =>
            {
                IsBusy = true;
                //change this to a code that can scan a bag tag (or maybe install??)
                Task.Delay(5000).ContinueWith(_ =>
                {
                    BagTagNumber = "123456789";
                    IsBusy = false;
                });
            });

            this.TrackBagCommand = new Command(() =>
            {
                IsBusy = true;
                //change this to a code that can track the bag then redirect to result view
                Task.Delay(10000).ContinueWith(_ =>
                {
                    ShowViewModel<TrackBaggageResultViewModel>();
                    IsBusy = false;
                });
            });
        }

        private string _bagTagNumber;
        public string BagTagNumber
        {
            get { return _bagTagNumber; }
            set
            {
                SetProperty(ref _bagTagNumber, value);
            }
        }

        public ICommand ScanBagTagCommand { protected set; get; }

        private string _passengerName;
        public string PassengerName
        {
            set
            {
                _passengerName = value;
            }
        }

        public ICommand TrackBagCommand { protected set; get; }


        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

    }
}
