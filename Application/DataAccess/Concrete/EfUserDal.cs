using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Dtos.Auth;
using Application.Entities.Dtos.User;
using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace Application.DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, ProductInformationContext>, IUserDal
    {
        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            using (var context = new ProductInformationContext())
            {
                var entity = context.Users.Where(u => u.Id == user.Id)
                    .Join(context.OperationClaims, u => u.RoleId, o => o.Id, (u, o) => new OperationClaim { Id = o.Id, RoleName = o.RoleName });
                return await entity.ToListAsync();
            }
        }



        public List<UserGetAllDto> GetAllUser()
        {
            using (var context = new ProductInformationContext())
            {
               
                   
                var entity = context.Users.Include(u => u.OperationClaim).Where(w=>w.Id)

                //var entity = context.UserOperationClaims.Include(u => u.User).Include(o => o.OperationClaim)

                    .Select(se => new UserGetAllDto
                    {

                        Id = se.UserId,
                        FirstName = se.User.FirstName,
                        LastName = se.User.LastName,
                        Role = se.OperationClaim.Name,
                        Email = se.User.Email

                    })
                    .ToList();
                return entity;
            }
        }

        Task<IList<UserGetAllDto>> IUserDal.GetAllUser()
        {
            throw new NotImplementedException();
        }
    }
}
