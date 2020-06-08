using Application.Core.DataAccess;
using Application.Entities.Dtos.Auth;
using Application.Entities.Dtos.User;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);

        Task<IList<UserGetAllDto>> GetAllUser();
       Task UserUpdate(UserUpdateDto userUpdateDto);
    }
}
