using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bagahe.Interfaces;
using Bagahe.Entities;
using Bagahe.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;
using Acr.UserDialogs;
using Xamarin.Forms;
using Bagahe.Models;

namespace Bagahe.ViewModels.Search
{
    class TrackBaggagesViewModel : BaseViewModel
    {
        private readonly IValidation _iValidator;
        private readonly IMvxNavigationService _iNavigator;

        public TrackBaggagesViewModel(IValidation validator, IMvxNavigationService navigator)
        {
            _iValidator = validator;
            _iNavigator = navigator;
        }

        private string _bagtag;
        public string Bagtag
        {
            get { return _bagtag; }
            set
            {
                _bagtag = value;
                RaisePropertyChanged(() => Bagtag);
            }
        }

        private string _PNR;
        public string PNR
        {
            get { return _PNR; }
            set
            {
                _PNR = value;
                RaisePropertyChanged(() => PNR);
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        private string _inputErrorMsg;
        public string InputErrorMsg
        {
            get { return _inputErrorMsg; }
            set
            {
                _inputErrorMsg = value;
                RaisePropertyChanged(() => InputErrorMsg);
            }
        }

        public ICommand TrackResultCommand
        {
            get
            {

                return new MvxCommand(() =>
               {

                   try
                   {
                       bool validPNR = _iValidator.IsPNRValid(_PNR);
                       bool validBagtag = _iValidator.IsBagtagValid(_bagtag);
                       bool validLastName = IsLastNameValid();

                       if (validPNR && validBagtag && validLastName && Application.Current.Properties.ContainsKey("token"))
                       {
                           ShowViewModel<TrackBaggageResultViewModel>(new TrackBaggageResultViewModel.Params { Bagtag = Bagtag, PNR = PNR, LastName = LastName });

                       }
                       else if (validPNR && validLastName && string.IsNullOrEmpty(_bagtag))
                       {
                           ShowViewModel<TrackPnrLastnameResultViewModel>(new TrackPnrLastnameResultViewModel.Params { PNR = PNR, LastName = LastName });
                       }
                       else
                       {
                           InputErrorMsg = "Incorrect Bagtag or PNR Number or Lastname";
                       }

                   }
                   catch (Exception e)
                   {
                       Mvx.Resolve<IUserDialogs>().Toast(e.Message, null);
                   }
               });
            }
        }

        private bool IsLastNameValid()
        {
            bool ret = false;
            if (Application.Current.Properties.ContainsKey("name") && !string.IsNullOrEmpty(_lastName))
            {
                var name = Convert.ToString(Application.Current.Properties["name"]);
                string[] substrings = name.Split(' ');
                foreach (var substring in substrings)
                {
                    if (substring.ToLower() == _lastName.ToLower().Trim(' '))
                    {
                        ret = true;
                    }
                }

            }
            return ret;
        }

    }
}
