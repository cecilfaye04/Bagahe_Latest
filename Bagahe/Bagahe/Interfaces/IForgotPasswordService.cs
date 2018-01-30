using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Interfaces
{
    interface IForgotPasswordService
    {
        Task<bool> CheckEmail(string email);
        Task<bool> ChangePassword(string password);
    }
}
