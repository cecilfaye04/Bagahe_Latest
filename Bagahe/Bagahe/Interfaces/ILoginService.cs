using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Interfaces
{
    public interface ILoginService
    {
        Task<bool> ValidateLogin(string username, string password);
    }
}