using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Entities;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;
using Xamarin.Forms;
using Bagahe.Interfaces;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Bagahe.ViewModels.Search
{
    public class TrackBaggageResultViewModel : BaseViewModel, IMvxNotifyPropertyChanged
    {
        private readonly IUserDialogs _udialog;
        private readonly IMvxNavigationService _iNavigator;
        private readonly IValidation _validation;

        public TrackBaggageResultViewModel(IMvxNavigationService navigator, IUserDialogs dialog, IValidation validation)
        {
            _udialog = dialog;
            _iNavigator = navigator;
            _validation = validation;
        }

        public TrackBaggageResultViewModel()
        {
        }

        private Params _params = new Params();
        public class Params
        {
            public string Bagtag { get; set; }

            public string PNR { get; set; }

            public string LastName { get; set; }
        }

        public void Init(Params input)
        {
            _params = input;
            DisplayBaggageLocation();
        }
        
        public async void DisplayBaggageLocation()
        {
            try
            {
                using (_udialog.Loading("Tracking..."))
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("bagtag", _params.Bagtag);
                    parameters.Add("pnr", _params.PNR);
                    parameters.Add("lastname", _params.LastName.Trim(' '));
                    var restService = Mvx.Resolve<IRestService>();
                    restService.WebMethod = "TrackBagScanPoints";
                    restService.Parameters = parameters;

                    string returnResponse = await restService.Consume();
                    TrackResponse trackResponse = JsonConvert.DeserializeObject<TrackResponse>(returnResponse);
                    if (trackResponse.StatusCode == "0")
                    {
                        Application.Current.Properties["token"] = trackResponse.Token;

                        if (trackResponse.ScanPoints != null)
                        {
                            var scanList = new ScanList();
                            foreach (var item in trackResponse.ScanPoints.ToList())
                            {
                                scanList.Add(new ScanPoints() { Station = item.Station, Location = item.Location, ScanTime = item.ScanTime.ToString("HH:mm MMM dd, yyyy") });
                            }
                            var list = new List<ScanList>() { scanList };
                            ScanPointList = list;
                        }
                    }
                    else
                    {
                        ErrorMessage = trackResponse.Message;
                    }
                    Bagtag = _params.Bagtag;
                    PNR = _params.PNR;
                    LastName = _params.LastName;
                    
                }
            }
            catch (Exception e)
            {
                Mvx.Resolve<IUserDialogs>().Toast(e.Message, null);
            }

        }
        
        public class ScanPoints
        {
            public string Location { get; set; }
            public string Station { get; set; }
            public string ScanTime { get; set; }
        }

        public class ScanList : List<ScanPoints>
        {
            public List<ScanPoints> ScanPoints => this;
        }

        private List<ScanList> _scanList;
        public List<ScanList> ScanPointList
        {
            get { return _scanList; }
            set
            {
                _scanList = value;
                base.RaisePropertyChanged();
            }
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
            }
        }
    }
}
