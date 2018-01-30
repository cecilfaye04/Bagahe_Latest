using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Entities
{
    public class GetBagListReponse : BaseResponse
    {
        public List<string> Bags { get; set; }

        public string Token { get; set; }
                
    }
}
