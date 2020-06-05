using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Dtos.Auth;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Application.DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, ProductInformationContext>, IUserDal
    {
      

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ProductInformationContext())
            {
                var entity = context.Users.Where(u => u.Id == user.Id)
                    .Join(context.OperationClaims, u => u.RoleId, o => o.Id, (u, o) => new OperationClaim { Id = o.Id, RoleName = o.RoleName });
                return entity.ToList();
            }
        }
    }
}
