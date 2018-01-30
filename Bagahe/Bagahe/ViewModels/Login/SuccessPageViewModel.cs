using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bagahe.ViewModels.Login
{
    class SuccessPageViewModel : BaseViewModel
    {
        public void Init(string message)
        {
            _message = message;
        }

        public ICommand SignInCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<LoginViewModel>();
                });
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
        }
    }
}
