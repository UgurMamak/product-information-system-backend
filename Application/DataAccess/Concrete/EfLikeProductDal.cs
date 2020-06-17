using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Application.Entities.Dtos.LikeProduct;
using System.Linq;

namespace Application.DataAccess.Concrete
{
    public class EfLikeProductDal : EfEntityRepositoryBase<LikeProduct, ProductInformationContext>, ILikeProductDal
    {
        public async Task<LikeProductNumberStatusDto> GetNumberStatus(string productId)
        {
            using (var context = new ProductInformationContext())
            {
                var trueNumber = await context.LikeProducts.Where(w => w.ProductId == productId && w.LikeStatus == true).CountAsync();
                var falseNumber = await context.LikeProducts.Where(w => w.ProductId == productId && w.LikeStatus == false).CountAsync();

                var entity = new LikeProductNumberStatusDto { FalseNumber = falseNumber, TrueNumber = trueNumber };
                return entity;
            }
        }
    }
}
