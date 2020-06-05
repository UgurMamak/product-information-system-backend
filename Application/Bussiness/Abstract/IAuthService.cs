using Application.Core.Utilities.Results;
using Application.Core.Utilities.Security.Jwt;
using Application.Entities.Dtos.Auth;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bussiness.Abstract
{
    public interface IAuthService
    {   
        IDataResult<User> Login(LoginDto LoginDto); 
        IResult UserExists(string email);     
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<User> Register(RegisterDto RegisterDto, string imgName);        
    }
}
