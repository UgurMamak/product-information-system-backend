using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.ProductCategory;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
   public class ProductCategoryService:IProductCategoryService
    {
        private IProductCategoryDal _productCategoryDal;
        public ProductCategoryService(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }

        public async Task<IResult> Add(ProductCategoryCreateDto productCategoryCreateDto)
        {
            var entity = new ProductCategory { 
                CategoryId=productCategoryCreateDto.CategoryId,
                ProductId=productCategoryCreateDto.ProductId
            };
            await _productCategoryDal.Add(entity);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public async Task<IResult> DeleteByProductId(string productId)
        {
           await _productCategoryDal.DeleteById(w => w.ProductId == productId);
            return new SuccessResult(Messages.CommentDeleted);
        }
    }
}
