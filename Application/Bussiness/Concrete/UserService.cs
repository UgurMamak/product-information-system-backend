using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.Auth;
using Application.Entities.Dtos.User;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class UserService : IUserService
    {
        private IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task Add(User user)
        {
           await _userDal.Add(user);
        }

        public async Task<User> GetByMail(string email)
        {
            return await _userDal.Get(u => u.Email == email);
        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            return await _userDal.GetClaims(user);
        }
      
        public async Task<IDataResult<IList<UserGetAllDto>>> UserGetAll()
        {          
            return new SuccessDataResult<IList<UserGetAllDto>>(await _userDal.GetAllUser());
        }

        public async Task<IDataResult<IList<UserListDto>>> GetById(string userId)
        {
            var entity = await _userDal.GetList(s => s.Id == userId);
            var data = new List<UserListDto>(entity.Select(se => new UserListDto {
                Id = se.Id,
                Email = se.Email,
                ImageName = se.ImageName,
                FirstName = se.FirstName,
                LastName = se.LastName,
            })).ToList();
            return new SuccessDataResult<List<UserListDto>>(data);
        }

        public async Task<IDataResult<User>> Update(UserUpdateDto userUpdateDto)
        {
             await _userDal.UserUpdate(userUpdateDto);
            return new SuccessDataResult<User>(Messages.UserUpdated);
        }
    }
}
