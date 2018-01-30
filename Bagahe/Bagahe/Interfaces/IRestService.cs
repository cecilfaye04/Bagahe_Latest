using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Entities;

namespace Bagahe.Interfaces
{
    public interface IRestService
    {
        Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserInput input);
        Task<TrackResponse> TrackBagScanPoints(BagTrackUserInput input);
        //Task<GetBagListReponse> GetBagList(BagTrackUserInput input);
        Task<GetBagListReponse> GetBagList(string PNR, string lastName);
        Task<String> Consume();

        Dictionary<String, String> Parameters { set; }
        String WebMethod { set; }
    }
}
