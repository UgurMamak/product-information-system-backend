using Application.Bussiness.Abstract;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.Auth;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bussiness.Concrete
{
    public class UserService : IUserService
    {
        private IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

       
    }
}
