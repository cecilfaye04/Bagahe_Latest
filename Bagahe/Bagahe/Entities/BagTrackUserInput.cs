using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Entities
{
    public class BagTrackUserInput : BaseInput
    {
        public string Bagtag { get; set; }

        public string PNR { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }
    }
}
