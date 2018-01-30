using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Bagahe.Interfaces;
using Acr.UserDialogs;

namespace Bagahe.ViewModels.Login
{
    class ForgotPasswordViewModel : BaseViewModel
    {
        private readonly IForgotPasswordService _service;
        private readonly IUserDialogs _udialog;

        public ForgotPasswordViewModel(IForgotPasswordService service, IUserDialogs dialog)
        {
            _service = service;
            _udialog = dialog;
        }
        public void Init(string action, string email, string code)
        {
            _action = action;
            EmailAdd = email;
            randomString = code;
            initDisplay();
        }

        private string randomString;
        public ICommand ResetPasswordCommand
        {
            get
            {
                return new MvxCommand(async () => {
                    if (isValidEmail())
                    {
                        bool isExist = false;
                        //check if the email exists in the DB
                        using (_udialog.Loading("Checking email..."))
                        {
                            isExist = await _service.CheckEmail(EmailAdd);
                        }

                        if (!isExist)
                            ErrorMsg = "The email you have entered is not yet registered.";
                        else
                        {
                            //Send email
                            using (_udialog.Loading("Sending email..."))
                            {
                                await Task.Delay(2000);
                            }
                            //Change the way of generating random code.
                            randomString = RandomString(8);
                            //Redirect
                            //ShowViewModel<FgtPwEnterCodeVeiwModel>();
                            //ErrorMsg = "Valid Email " + randomString;
                            try
                            {
                                ShowViewModel<VerifyCodeViewModel>(new { code = randomString, email = EmailAdd, from = "ForgotPwViewModel" });
                            }
                            catch(Exception e)
                            {
                                return;
                            }
                        }
                            
                    }
                });
            }

        }
        private void initDisplay()
        {

            IsVisibleEmailEntry = false;
            IsVisibleEmailLabel = false;

            if (_action.Equals("ResendCode"))
            {
                Header = "Resend Code";
                Description = "We will send a security code to the email address below.";
                IsVisibleEmailLabel = true;
            } else
            {
                Header = "Forgot Password?";
                Description = "We just need your registered email to send you a security code.";
                IsVisibleEmailEntry = true;
            }
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string _action;
        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(() => {
                    if (_action.Equals("ForgotPassword"))
                        ShowViewModel<LoginViewModel>();
                    else if (_action.Equals("ResendCode"))
                        ShowViewModel<VerifyCodeViewModel>(new { code = randomString, email = EmailAdd });
                    else
                        ErrorMsg = "Unknown error has occured.";
                });
               
            }
        }

        private bool isValidEmail()
        {
            bool isValid = true;
            if (("").Equals(EmailAdd) || EmailAdd == null)
            {
                ErrorMsg = "The email address is required.";
                isValid = false;
            }
            else
            {
                string regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                bool isEmail = Regex.IsMatch(EmailAdd, regex, RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    ErrorMsg = "The email address you entered is not valid.";
                    isValid = false;
                }
            }

            return isValid;
        }

        private string _emailAdd;
        public string EmailAdd
        {
            get { return _emailAdd; }
            set
            {
                _emailAdd = value;
                RaisePropertyChanged(() => EmailAdd);
            }
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                RaisePropertyChanged(() => ErrorMsg);
            }
        }

        //For display
        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged(() => Header);
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }
        private bool _isVisibleEmailEntry;
        public bool IsVisibleEmailEntry
        {
            get { return _isVisibleEmailEntry; }
            set
            {
                _isVisibleEmailEntry = value;
                RaisePropertyChanged(() => IsVisibleEmailEntry);
            }
        }
        private bool _isVisibleEmailLabel;
        public bool IsVisibleEmailLabel
        {
            get { return _isVisibleEmailLabel; }
            set
            {
                _isVisibleEmailLabel = value;
                RaisePropertyChanged(() => IsVisibleEmailLabel);
            }
        }
    }
}
