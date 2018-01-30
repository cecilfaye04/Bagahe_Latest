using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Interfaces
{
    public interface IValidation
    {
        bool IsPNRValid(string PNR);

        bool IsBagtagValid(string bagtag);
    }
}
