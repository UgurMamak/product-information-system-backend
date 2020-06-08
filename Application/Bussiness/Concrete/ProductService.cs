using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.Product;
using Application.Entities.Dtos.ProductCategory;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class ProductService : IProductService
    {
        private IProductDal _productDal;
        private IProductCategoryService _productCategoryService;
        public ProductService(IProductDal productDal,IProductCategoryService productCategoryService)
        {
            _productDal = productDal;
            _productCategoryService = productCategoryService;
        }
        public async Task<IDataResult<Product>> Add(ProductCreateDto productCreateDto)
        {
            var product = new Product
            {
                ProductName=productCreateDto.ProductName,
                ProductTypeId=productCreateDto.ProductTypeId,
                Title = productCreateDto.Title,
                Content = productCreateDto.Content,            
                UserId = productCreateDto.UserId,
                Created = DateTime.Now
            };
             await _productDal.Add(product);
           // var postSave = new SuccessDataResult<Product>(product, Messages.UserRegistered);
            var entity = new SuccessDataResult<Product>(product);
            foreach (var item in productCreateDto.Categories)
            {
               await _productCategoryService.Add(
                    new ProductCategoryCreateDto { ProductId=entity.Data.Id,CategoryId=item.CategoryId}
                    );
            }
            return new SuccessDataResult<Product>(product, Messages.ProductAdded);
        }
    }
}
