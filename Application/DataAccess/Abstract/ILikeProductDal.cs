using Application.Core.DataAccess;
using Application.Entities.Dtos.LikeProduct;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Abstract
{
    public interface ILikeProductDal:IEntityRepository<LikeProduct>
    {
        Task<LikeProductNumberStatusDto> GetNumberStatus(string productId);
    }
}
