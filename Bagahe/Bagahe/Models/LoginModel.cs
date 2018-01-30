using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Models
{
    public class LoginModel : BaseViewModel
    {
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
