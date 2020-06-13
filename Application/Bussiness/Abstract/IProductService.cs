using Application.Core.Utilities.Results;
using Application.Entities.Dtos.Product;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
   public interface IProductService
    {
       Task<IDataResult<Product>> Add(ProductCreateDto postCreateDto);
       Task<IResult> Delete(Product product);

       Task<IDataResult<IList<ProductCartDto>>> GetProductCart(Expression<Func<ProductCartDto, bool>> filter = null);

        Task<IDataResult<IList<ProductDetailDto>>> GetProductDetail(string productId);

        Task<IResult> Update(ProductUpdateDto product);


        //Puan verme kontolü
        Task<IResult>  PointAdd(ProductPoint productPoint);
        Task<bool> ProductPointExists(ProductPoint productPoint);
        Task<IResult> PointUpdate(ProductPoint productPoint);
        Task<IDataResult<ProductPointDto>> GetProductPoint(string productId);
    }
}

