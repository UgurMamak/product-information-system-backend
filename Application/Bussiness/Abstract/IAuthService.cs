using Application.Core.Utilities.Results;
using Application.Core.Utilities.Security.Jwt;
using Application.Entities.Dtos.Auth;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface IAuthService
    {   
        Task<IDataResult<User>> Login(LoginDto LoginDto); 
        Task<IResult> UserExists(string email);     
        Task<IDataResult<AccessToken>> CreateAccessToken(User user);
        Task<IDataResult<User>> Register(RegisterDto RegisterDto, string imgName);        
    }
}
