using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Models
{
    class PasswordFieldsModel : BaseViewModel
    {

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

        private string _rePassword;
        public string RePassword
        {
            get { return _rePassword; }
            set
            {
                _rePassword = value;
                RaisePropertyChanged(() => RePassword);
            }
        }

    }
}
