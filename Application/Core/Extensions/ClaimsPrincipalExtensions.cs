using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //Kullanıcının rolerine erişebilmek için yazıldı. role ve claimlere ulaşmak için yazdık.
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            //kullanıcı login yapmış mı diye kontrol ettik. (claimtype var mı diye baktık.)
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        //kullanıcı rollere erişmek istediğinde ClaimsRoles metosu ile erişebilecek.
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        { //kullanıcının rolleri listelemş olacak.
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
