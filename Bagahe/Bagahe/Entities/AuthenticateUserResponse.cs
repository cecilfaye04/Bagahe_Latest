using System.Collections.Generic;

namespace Bagahe.Entities
{
    public class AuthenticateUserResponse : BaseResponse
    {
        public string Name { get; set; }

        public List<string> Applications { get; set; }

        public string Token { get; set; }
    }
}
