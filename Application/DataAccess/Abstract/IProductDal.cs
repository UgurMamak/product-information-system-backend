using Application.Core.DataAccess;
using Application.Entities.Dtos.Product;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        //ProductPoint
        Task PointAdd(ProductPoint productPoint);
        Task<IList<ProductPoint>> ProductPointExists(Expression<Func<ProductPoint, bool>> filter = null);
        Task PointUpdate(ProductPoint productPoint);

        //ProductCart
        Task<IList<ProductCartDto>> GetProductCart(Expression<Func<ProductCartDto, bool>> filter = null);

        //ProductDetail
        Task<IList<ProductDetailDto>> GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter = null);

    }
}
