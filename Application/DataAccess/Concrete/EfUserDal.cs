using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Dtos.Auth;
using Application.Entities.Dtos.User;
using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using Application.Core.Utilities.Security.Hashing;

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
                return await entity.AsNoTracking().ToListAsync();
            }
        }
        public async Task<IList<UserGetAllDto>> GetAllUser()
        {
            using (var context = new ProductInformationContext())
            {
                var entity =await context.Users.Include(u => u.OperationClaim)
                    .Select(se => new UserGetAllDto
                    {
                        Id = se.Id,
                        Email = se.Email,
                        FirstName = se.FirstName,
                        LastName = se.LastName,
                        Role = se.OperationClaim.RoleName

                    }).AsNoTracking().ToListAsync();
                return entity;
            }
        }

        public async Task UserUpdate(UserUpdateDto userUpdateDto)
        {
            using (var context = new ProductInformationContext())
            {
                var update =await context.Users.SingleOrDefaultAsync(w => w.Id == userUpdateDto.Id);

                if (userUpdateDto.FirstName != null) update.FirstName = userUpdateDto.FirstName;

                if (userUpdateDto.LastName != null) update.LastName = userUpdateDto.LastName;

                if (userUpdateDto.Image != null) update.ImageName = userUpdateDto.ImageName;

                if (userUpdateDto.Email != null) update.Email = userUpdateDto.Email;

                if (userUpdateDto.password != null)
                {
                    byte[] passwordHash, passwordSalt; //işlem bitince bunlar oluşacak
                    HashingHelper.CreatePasswordHash(userUpdateDto.password, out passwordHash, out passwordSalt);
                    update.PasswordHash = passwordHash;
                    update.PasswordSalt = passwordSalt;
                }
                update.Updated = DateTime.Now;
               await context.SaveChangesAsync();
            }
        
        }
    }
}
