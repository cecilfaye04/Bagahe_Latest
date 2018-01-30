using Acr.UserDialogs;
using Bagahe.Interfaces;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bagahe.ViewModels.Login
{
    class VerifyCodeViewModel : BaseViewModel
    {
        private readonly IForgotPasswordService _service;
        private readonly IUserDialogs _udialog;

        public VerifyCodeViewModel(IForgotPasswordService service, IUserDialogs dialog)
        {
            _service = service;
            _udialog = dialog;
        }

        public ICommand ValidateCodeCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    //Validate input
                    if (Code.Equals(InputCode))
                    {
                        ShowViewModel<NewPasswordViewModel>();
                    }
                    else
                    {
                        ErrorMsg = "Invalid!";
                    }
                });
            }
        }

        public ICommand ResendCodeCommand
        {
            get
            {
                return new MvxCommand(() => {
                    ShowViewModel<ForgotPasswordViewModel>(new { action="ResendCode", email = Email, code = Code});
                });
            }
        }
        private string _code;
        private string _email;
        public async void Init(string code, string email, string from)
        {
            _code = code;
            _email = email;
            //Remove this if the function for sending email is working.
            DemoCode = "Demo Code: "+code;
            //--
            if ("ForgotPwViewModel".Equals(from))
                await Timer(30);

            else
            {
                ResendCodeButton = "Resend Code";
                IsEnabledResend = true;
                BGColorButton = "#2196f3";
            }
        }


        async Task Timer(int sec)
        {
            int count = 0;
            ResendCodeButton = "Resend Code (" + sec + ")";
            IsEnabledResend = false;
            BGColorButton = "#d9534f";
            while (sec + 1 > count)
            {
                await Task.Delay(1000);
                ResendCodeButton = "Resend Code (" + (sec - count) + ")";
                count++;
            }
            ResendCodeButton = "Resend Code";
            IsEnabledResend = true;
            BGColorButton = "#2196f3";
        }


        private string _resendCodeButton;
        public string ResendCodeButton
        {
            get { return _resendCodeButton; }
            set
            {
                _resendCodeButton = value;
                RaisePropertyChanged(() => ResendCodeButton);
            }
        }

        public string Email
        {
            get { return _email; }
        }
        public string Code
        {
            get { return _code; }
        }

        private string _inputCode;
        public string InputCode
        {
            get { return _inputCode; }
            set
            {
                _inputCode = value;
                RaisePropertyChanged(() => InputCode);
            }
        }
        //For display
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
        private bool _isEnabledResend;
        public bool IsEnabledResend
        {
            get { return _isEnabledResend; }
            set
            {
                _isEnabledResend = value;
                RaisePropertyChanged(() => IsEnabledResend);
            }
        }
        private string _bGColorButton;
        public string BGColorButton
        {
            get { return _bGColorButton; }
            set
            {
                _bGColorButton = value;
                RaisePropertyChanged(() => BGColorButton);
            }
        }
        //For demo purposes.
        //Displaying the code
        private string _demoCode;
        public string DemoCode
        {
            get { return _demoCode; }
            set
            {
                _demoCode = value;
                RaisePropertyChanged(() => DemoCode);
            }
        }
        
    }
}
