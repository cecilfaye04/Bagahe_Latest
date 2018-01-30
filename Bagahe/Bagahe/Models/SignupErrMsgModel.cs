using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Models
{
    class SignupErrMsg : BaseViewModel
    {
        private string _firstNameErrMsg;
        public string FirstNameErrMsg
        {
            get { return _firstNameErrMsg; }
            set
            {
                _firstNameErrMsg = value;
                RaisePropertyChanged(() => FirstNameErrMsg);
            }
        }

        private string _lastNameErrMsg;
        public string LastNameErrMsg
        {
            get { return _lastNameErrMsg; }
            set
            {
                _lastNameErrMsg = value;
                RaisePropertyChanged(() => LastNameErrMsg);
            }
        }

        private string _usernameErrMsg;
        public string UsernameErrMsg
        {
            get { return _usernameErrMsg; }
            set
            {
                _usernameErrMsg = value;
                RaisePropertyChanged(() => UsernameErrMsg);
            }
        }

        private string _passwordErrMsg;
        public string PasswordErrMsg
        {
            get { return _passwordErrMsg; }
            set
            {
                _passwordErrMsg = value;
                RaisePropertyChanged(() => PasswordErrMsg);
            }
        }

        private string _rePasswordErrMsg;
        public string RePasswordErrMsg
        {
            get { return _rePasswordErrMsg; }
            set
            {
                _rePasswordErrMsg = value;
                RaisePropertyChanged(() => RePasswordErrMsg);
            }
        }

        private string _emailErrMsg;
        public string EmailErrMsg
        {
            get { return _emailErrMsg; }
            set
            {
                _emailErrMsg = value;
                RaisePropertyChanged(() => EmailErrMsg);
            }
        }

        public void Init()
        {
            this.FirstNameErrMsg = "";
            this.LastNameErrMsg = "";
            this.UsernameErrMsg = "";
            this.PasswordErrMsg = "";
            this.RePasswordErrMsg = "";
            this.EmailErrMsg = "";
        }
    }
}
