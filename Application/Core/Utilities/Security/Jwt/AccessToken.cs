using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Security.Jwt
{
    public class AccessToken
    {   //Return edilecek değerler
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
        public List<OperationClaim> Role { get; set; }
    }
}
