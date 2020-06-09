using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.Product;
using Application.Entities.Dtos.ProductCategory;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class ProductService : IProductService
    {
        private IProductDal _productDal;
        private IProductCategoryService _productCategoryService;
        public ProductService(IProductDal productDal, IProductCategoryService productCategoryService)
        {
            _productDal = productDal;
            _productCategoryService = productCategoryService;
        }
        public async Task<IDataResult<Product>> Add(ProductCreateDto productCreateDto)
        {
            var product = new Product
            {
                ProductName = productCreateDto.ProductName,
                ProductTypeId = productCreateDto.ProductTypeId,
                Title = productCreateDto.Title,
                Content = productCreateDto.Content,
                UserId = productCreateDto.UserId,
                Created = DateTime.Now
            };
            await _productDal.Add(product);
            // var postSave = new SuccessDataResult<Product>(product, Messages.UserRegistered);
            if (productCreateDto.Categories != null)
            {
                var entity = new SuccessDataResult<Product>(product);
                foreach (var item in productCreateDto.Categories)
                {
                    await _productCategoryService.Add(
                         new ProductCategoryCreateDto { ProductId = entity.Data.Id, CategoryId = item }
                         );
                }
            }
            return new SuccessDataResult<Product>(product, Messages.ProductAdded);
        }

        public async Task<IResult> Delete(Product product)
        {           
         //  await _productCategoryService.DeleteByProductId(product.Id);
          //  _commentService.DeleteByPostId(post.Id);
          await  _productDal.DeleteById(w => w.Id == product.Id);
            return new SuccessResult(Messages.ProductDeleted);           
        }

        public async Task<IDataResult<IList<ProductCartDto>>> GetProductCart(Expression<Func<ProductCartDto, bool>> filter = null)
        {
            return new SuccessDataResult<IList<ProductCartDto>>(await _productDal.GetProductCart(filter));
        }

        public async Task<IDataResult<IList<ProductDetailDto>>> GetProductDetail(string productId)
        {
            return new SuccessDataResult<IList<ProductDetailDto>>(await _productDal.GetProductDetail(x=>x.ProductId==productId));
        }

        //PRODUCTPOİNT PROCCESS

        public async Task<IResult> PointAdd(ProductPoint productPoint)
        {
            await _productDal.PointAdd(productPoint);
            return new SuccessResult(Messages.ProductPointAdded);
        }
      
        public async Task<bool> ProductPointExists(ProductPoint productPoint)
        {
            //var isThere = _likePostDal.GetList(w => w.PostId == likePost.PostId && w.UserId == likePost.UserId).Count();
            var isThere =await _productDal.ProductPointExists(x=>x.ProductId==productPoint.ProductId && x.UserId==productPoint.UserId);

            if(isThere.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }      
        }

        public async Task<IResult> PointUpdate(ProductPoint productPoint)
        {
           await _productDal.PointUpdate(productPoint);
            return new SuccessResult("güncellendi");
        }

       
    }
}
