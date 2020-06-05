using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {    
        //Token işlemi için jwt dışında bir yöntem kullandığımızda tüm kod bloğu değişmesin diye interface kullandım.
        //User nesnesine ihtiyaç var user bilgisi dönecek ona göre token üretecek. Aynı zamanda kullanıcının rollerinin de gelmesini istioruz.
        //yani kullanıcı bilgisi ve rolleri vermiş olduk.
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
