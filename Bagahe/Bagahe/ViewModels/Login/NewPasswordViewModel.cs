using Acr.UserDialogs;
using Bagahe.Interfaces;
using Bagahe.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bagahe.ViewModels.Login
{
    class NewPasswordViewModel : BaseViewModel
    {
        private readonly IUserDialogs _udialog;
        private readonly IForgotPasswordService _service;


        public NewPasswordViewModel(IForgotPasswordService service, IUserDialogs dialog)
        {
            _udialog = dialog;
            _service = service;
        }

        public ICommand ChangePasswordCommand
        {
            get
            {
                return new MvxCommand(async () => {
                    bool isValidInput = validateInput();
                    if (isValidInput)
                    {
                        bool isChangedPw = false;
                        using (_udialog.Loading("Updating ..."))
                        {
                            isChangedPw = await _service.ChangePassword(PasswordField.Password);
                        }
                        string successMessage = "Your password has been changed successfully! Thank you.";
                        ShowViewModel<SuccessPageViewModel>(new { message = successMessage });
                    }
                });
            }
        }

        int countInvalidInput;
        private bool validateInput()
        {
            countInvalidInput = 0;
            bool result = true;
            PasswordErrMsg.Init();
            GeneralErrorMsg = "";

            isValidPassword(PasswordField.Password);

            if (isEmpty(PasswordField.RePassword))
            {
                PasswordErrMsg.RePasswordErrMsg = "Please re-enter your password.";
            }
            else
            {
                if (!PasswordField.RePassword.Equals(PasswordField.Password))
                {
                    PasswordErrMsg.RePasswordErrMsg = "The password you have entered doesn't match.";
                    countInvalidInput++;
                }
            }


            if (countInvalidInput != 0)
            {
                GeneralErrorMsg = "Please correct the " + countInvalidInput + " item";
                if (countInvalidInput != 1)
                {
                    GeneralErrorMsg += "s";
                }
                result = false;
            }

            return result;
        }

        private void isValidPassword(string password)
        {
            if (isEmpty(password))
            {
                PasswordErrMsg.PasswordErrMsg = "Password is required.";
            }
            else if (password.Length < 8)
            {
                PasswordErrMsg.PasswordErrMsg = "Passwords must be at least 8 characters long.";
                countInvalidInput++;
            }
            else
            {
                List<string> errors = new List<string>();

                if (!Regex.IsMatch(password, @"[A-Z]"))
                    errors.Add("uppercase letter");
                if (!Regex.IsMatch(password, @"[a-z]"))
                    errors.Add("lowercase letter");
                if (!Regex.IsMatch(password, @"[0-9]"))
                    errors.Add("number");

                if (errors.Count > 0)
                {
                    countInvalidInput++;
                    PasswordErrMsg.PasswordErrMsg = "Your password must contain at least one ";
                    int counter = 0;
                    foreach (var error in errors)
                    {
                        counter++;
                        PasswordErrMsg.PasswordErrMsg += error;
                        if (counter == errors.Count - 1)
                            PasswordErrMsg.PasswordErrMsg += " and ";
                        else if (errors.Count != counter)
                            PasswordErrMsg.PasswordErrMsg += ", ";
                        else
                            PasswordErrMsg.PasswordErrMsg += ".";
                    }
                }
            }
        }
        private bool isEmpty(string input)
        {
            bool result = false;
            if (("").Equals(input) || input == null)
            {
                countInvalidInput++;
                result = true;
            }
            return result;
        }

        private PasswordFieldsModel _passwordField = new PasswordFieldsModel();
        public PasswordFieldsModel PasswordField
        {
            get { return _passwordField; }
            set
            {
                _passwordField = value;
                RaisePropertyChanged(() => PasswordField);
            }
        }

        private PasswordErrMsgModel _passwordErrMsg = new PasswordErrMsgModel();
        public PasswordErrMsgModel PasswordErrMsg
        {
            get { return _passwordErrMsg; }
            set
            {
                _passwordErrMsg = value;
                RaisePropertyChanged(() => PasswordErrMsg);
            }
        }

        private string _generalErrorMsg;
        public string GeneralErrorMsg
        {
            get { return _generalErrorMsg; }
            set
            {
                _generalErrorMsg = value;
                RaisePropertyChanged(() => GeneralErrorMsg);
            }
        }
    }
}
