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
using Newtonsoft.Json;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Bagahe.ViewModels.Search
{
    public class TrackPnrLastnameResultViewModel : BaseViewModel, IMvxNotifyPropertyChanged
    {

        private readonly IUserDialogs _udialog;
        private readonly IMvxNavigationService _iNavigator;
        private readonly IValidation _validation;

        public TrackPnrLastnameResultViewModel(IMvxNavigationService navigator, IUserDialogs dialog, IValidation validation)
        {
            _udialog = dialog;
            _iNavigator = navigator;
            _validation = validation;
        }

        public TrackPnrLastnameResultViewModel()
        {
        }

        private Params _params = new Params();
        public class Params
        {
            public string PNR { get; set; }

            public string LastName { get; set; }
        }

        public void Init(Params input)
        {
            _params = input;
            DisplayBagList();
        }

        public async void DisplayBagList()
        {
            try
            {
                using (_udialog.Loading("Displaying Bag List..."))
                {
                    //Dictionary<string, string> parameters = new Dictionary<string, string>();
                    //parameters.Add("pnr", _params.PNR);
                    //parameters.Add("lastname", _params.LastName);
                    var restService = Mvx.Resolve<IRestService>();
                    //restService.WebMethod = "GetBagList";
                   // restService.Parameters = parameters;

                    //string returnResponse = await restService.Consume();
                    GetBagListReponse bagListResponse = await restService.GetBagList(_params.PNR, _params.LastName.Trim(' '));
                    //GetBagListReponse bagListResponse = JsonConvert.DeserializeObject<GetBagListReponse>(returnResponse);

                    if (bagListResponse.StatusCode == "0")
                    {
                        Application.Current.Properties["token"] = bagListResponse.Token;

                        if (bagListResponse.Bags != null)
                        {
                            var newList = new ObservableCollection<Bagtag>();
                            foreach (var item in bagListResponse.Bags.ToList())
                            {
                                newList.Add(new Bagtag() { Bagtags = item });
                            }

                            BagList = newList;
                        }
                    }
                    else
                    {
                        ErrorMessage = bagListResponse.Message;
                    }
                    PNR = _params.PNR;
                    LastName = _params.LastName;

                }
            }
            catch (Exception e)
            {
                Mvx.Resolve<IUserDialogs>().Toast(e.Message, null);
            }

        }

        
                
        public class Bagtag
        {
            public string Bagtags { get; set; }
        }
        
        private Bagtag _selectedBag;
        public Bagtag SelectedBag
        {
            get { return _selectedBag; }
            set
            {
                if (_selectedBag != value)
                {
                    _selectedBag = value;
                    DisplaySelectedItem();
                    _selectedBag = null;
                }
                RaisePropertyChanged("SelectedBag");
                //base.RaisePropertyChanged();
                
            }
        }

        private void DisplaySelectedItem()
        {
            
            ShowViewModel<TrackBaggageResultViewModel>(new TrackBaggageResultViewModel.Params { Bagtag = _selectedBag.Bagtags, PNR = _PNR, LastName = _lastName });
        }

        private ObservableCollection<Bagtag> _bagList = new ObservableCollection<Bagtag>();
        public ObservableCollection<Bagtag> BagList
        {
            get { return _bagList; }
            set
            {
                _bagList = value;
                base.RaisePropertyChanged();
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
