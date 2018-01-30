using Acr.UserDialogs;
using Bagahe.Interfaces;
using Bagahe.ViewModels.Search;
using Bagahe.Entities;
using Bagahe.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace Bagahe.ViewModels.Login
{
    class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _service;
        private readonly IUserDialogs _udialog;
        private readonly IValidation _validation;

        public LoginViewModel(ILoginService service, IUserDialogs dialog, IValidation validation)
        {
            _service = service;
            _udialog = dialog;
            _validation = validation;
        }

        public void Init()
        {
            Username = "";
            Password = "";
        }

        public ICommand ShowMenuPageCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {

                    bool isValid = validateUserInput();
                    if (isValid)

                    {
                        using (_udialog.Loading("Logging in..."))
                        {
                            Dictionary<string, string> parameters = new Dictionary<string, string>();
                            parameters.Add("username", Username);
                            parameters.Add("password", Password);
                            parameters.Add("Station", "MNL");
                            parameters.Add("Device", "Android");
                            parameters.Add("Version", "0.1");
                            var restService = Mvx.Resolve<IRestService>();
                            restService.WebMethod = "AuthenticateUser";
                            restService.Parameters = parameters;
                            
                            string returnResponse = await restService.Consume();
                            AuthenticateUserResponse authUserResponse = JsonConvert.DeserializeObject<AuthenticateUserResponse>(returnResponse);
                            
                            if (authUserResponse.StatusCode == "0")
                            {

                                Application.Current.Properties["token"] = authUserResponse.Token;
                                Application.Current.Properties["name"] = authUserResponse.Name;
                                await Application.Current.SavePropertiesAsync();
                                ShowViewModel<TrackBaggagesViewModel>();
                                Password = string.Empty;
                                LoginErrorMsg = string.Empty;
                            }
                            else
                            {
                                LoginErrorMsg = "Incorrect Username or Password";
                            }
                        }
                    }
                });
            }
        }
        public ICommand ShowSignUpCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<SignupViewModel>();
                });
            }
        }

        public ICommand ShowForgotPasswordCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<ForgotPasswordViewModel>(new { action = "ForgotPassword" });
                });
            }
        }
        private bool validateUserInput()
        {
            bool isValid = false;
            bool isUsernameValid = false;
            bool isPasswordValid = false;

            LoginErrorMsg = "";
            UsernameErrorMsg = "Username is required.";
            PasswordErrorMsg = "Password is required.";

            //if (!("").Equals(Username) && Username != null)
            if(!string.IsNullOrEmpty(Username))
            {
                isUsernameValid = true;
                UsernameErrorMsg = "";
            }

            //if (!("").Equals(Password) && Password != null)
            if(!string.IsNullOrEmpty(Password))
            {
                isPasswordValid = true;
                PasswordErrorMsg = "";
            }

            if (isPasswordValid && isUsernameValid)
                isValid = true;

            return isValid;
        }
        
        private string _userName;
        public string Username
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => Username);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private string _loginErrorMsg;
        public string LoginErrorMsg
        {
            get { return _loginErrorMsg; }
            set
            {
                _loginErrorMsg = value;
                RaisePropertyChanged(() => LoginErrorMsg);
            }
        }

        private string _usernameErrorMsg;
        public string UsernameErrorMsg
        {
            get { return _usernameErrorMsg; }
            set
            {
                _usernameErrorMsg = value;
                RaisePropertyChanged(() => UsernameErrorMsg);
            }
        }

        private string _passwordErrorMsg;
        public string PasswordErrorMsg
        {
            get { return _passwordErrorMsg; }
            set
            {
                _passwordErrorMsg = value;
                RaisePropertyChanged(() => PasswordErrorMsg);
            }
        }
    }
}
