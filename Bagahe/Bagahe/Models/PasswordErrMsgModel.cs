using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Models
{
    class PasswordErrMsgModel : BaseViewModel
    {
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

        public void Init()
        {
            this.RePasswordErrMsg = "";
            this.PasswordErrMsg = "";
        }
    }
}
