using Application.Core.Utilities.Results;
using Application.Entities.Dtos.Auth;
using Application.Entities.Dtos.User;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetClaims(User user);
        Task Add(User user);
        Task<User> GetByMail(string email);
        Task<IDataResult<IList<UserListDto>>> GetById(string userId);
        Task<IDataResult<IList<UserGetAllDto>>> UserGetAll();
        Task<IDataResult<User>> Update(UserUpdateDto userUpdateDto);

        Task<IResult> UpdateRole(UserGetAllDto userGetAllDto);



    }
}