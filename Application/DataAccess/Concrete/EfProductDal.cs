using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using Application.Entities.Dtos.Product;
using Application.Entities.Dtos.ProductCategory;
using Application.Entities.Dtos.Comment;
using System.IO;


namespace Application.DataAccess.Concrete
{
    public class EfProductDal : EfEntityRepositoryBase<Product, ProductInformationContext>, IProductDal
    {
        public async Task PointAdd(ProductPoint productPoint)
        {

            using (var context = new ProductInformationContext())
            {
                var entity = context.ProductPoints.Add(productPoint);
                await context.SaveChangesAsync();
            }
        }
        public async Task<IList<ProductPoint>> ProductPointExists(Expression<Func<ProductPoint, bool>> filter = null)
        {
            using (var context = new ProductInformationContext())
            {
                var entity = await context.ProductPoints.Where(filter).ToListAsync();
                return entity;
            }
        }

        public async Task PointUpdate(ProductPoint productPoint)
        {
            using (var context = new ProductInformationContext())
            {
                var entity = await context.ProductPoints.SingleOrDefaultAsync(x => x.ProductId == productPoint.ProductId && x.UserId == productPoint.UserId);
                entity.Point = productPoint.Point;
                await context.SaveChangesAsync();
            }
        }
        public async Task<ProductPointDto> GetProductPoint(string productId)
        {
            using (var context=new ProductInformationContext())
            {
                var isthere = await context.ProductPoints.Where(x=>x.ProductId==productId).ToListAsync();
                if (isthere.Count == 0)
                { var result2 = new ProductPointDto { Point = 0.0 }; return result2; }
                var entity = await context.ProductPoints.Where(x=>x.ProductId==productId).AverageAsync(x=>x.Point);
                var result = new ProductPointDto { Point=entity};

                return result;
            }          
        }

        public async Task<IList<ProductCartDto>> GetProductCart(Expression<Func<ProductCartDto, bool>> filter = null)
        {
            using (var context = new ProductInformationContext())
            {
                if(filter==null)
                {
                    return await context.Products.Include(x => x.Images).Include(x => x.ProductCategories).Include(x => x.ProductType).Include(x => x.ProductCategories).ThenInclude(x => x.Category)
                    .Select(se => new ProductCartDto
                    {

                        ProductId = se.Id,
                        ProductName = se.ProductName,
                        ProductType = se.ProductType.ProductTypeName,
                       // Created = se.Created,
                        Created = se.Created.ToString("dd MMMM yyyy HH:mm"),
                        UserId = se.UserId,
                        FirstName = se.User.FirstName,
                        LastName = se.User.LastName,

                        CommentNumber = se.Comments.Count(x => x.ProductId == se.Id),

                        //LikeNumber = context.LikePosts.Where(w => w.PostId == se.Id && w.LikeStatus == true).Count()
                        LikeNumber=se.LikeProducts.Where(x=>x.ProductId==se.Id && x.LikeStatus==true).Count(),

                        productCategoryDtos = new List<ProductCategoryDto>(context.ProductCategories.Where(x => x.ProductId == se.Id).Select(se => new ProductCategoryDto { CategoryId = se.CategoryId, CategoryName = se.Category.CategoryName })),

                        productImageListDtos = new List<ProductImageListDto>(context.Images.Where(x => x.ProductId == se.Id).Select(se => new ProductImageListDto { Id = se.Id, ImageName = se.ImageName })),
                    
                        ProductPoint = se.ProductPoint.Where(x => x.ProductId == se.Id).Average(x => x.Point).ToString()

                    })
                    .ToListAsync();
                }
                return await context.Products.Include(x => x.Images).Include(x => x.ProductCategories).Include(x => x.ProductType).Include(x => x.ProductCategories).ThenInclude(x => x.Category)
                    .Select(se => new ProductCartDto
                    {

                        ProductId = se.Id,
                        ProductName = se.ProductName,
                        ProductType = se.ProductType.ProductTypeName,
                        //Created = se.Created,
                        Created = se.Created.ToString("dd MMMM yyyy HH:mm"),
                        UserId = se.UserId,
                        FirstName = se.User.FirstName,
                        LastName = se.User.LastName,

                        CommentNumber = se.Comments.Count(x => x.ProductId == se.Id),

                        productCategoryDtos = new List<ProductCategoryDto>(context.ProductCategories.Where(x => x.ProductId == se.Id).Select(se => new ProductCategoryDto { CategoryId = se.CategoryId, CategoryName = se.Category.CategoryName })),

                        productImageListDtos = new List<ProductImageListDto>(context.Images.Where(x => x.ProductId == se.Id).Select(se => new ProductImageListDto { Id = se.Id, ImageName = se.ImageName })),


                    ProductPoint = se.ProductPoint.Where(x => x.ProductId == se.Id).Average(x => x.Point).ToString()
                    })
                    .Where(filter).ToListAsync();
             
            }
        }

        public async Task<IList<ProductDetailDto>> GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            using (var context = new ProductInformationContext())
            {
                return await context.Products.Include(x => x.Images).Include(x => x.ProductCategories).Include(x => x.ProductCategories)
              .Select(se => new ProductDetailDto
              {
                  ProductId = se.Id,
                  ProductName = se.ProductName,
                  ProductType = se.ProductType.ProductTypeName,
                  Created = se.Created,
                  Content = se.Content,
                  UserId = se.UserId,
                  FirstName = se.User.FirstName,
                  LastName = se.User.LastName,
                  CommentNumber = se.Comments.Count(x => x.ProductId == se.Id),
                  productCategoryDtos = new List<ProductCategoryDto>(context.ProductCategories.Where(x => x.ProductId == se.Id).Select(se => new ProductCategoryDto { CategoryId = se.CategoryId, CategoryName = se.Category.CategoryName })),
                  productImageListDtos = new List<ProductImageListDto>(context.Images.Where(x => x.ProductId == se.Id).Select(se => new ProductImageListDto { Id = se.Id, ImageName = se.ImageName })),
                  CommentDtos = new List<CommentListDto>(context.Comments.Where(x => x.ProductId == se.Id).OrderByDescending(x=>x.Created).Select(se => new CommentListDto
                  { 
                      Id = se.Id,
                      Content = se.Content,
                      UserId = se.UserId,
                      FirstName = se.User.FirstName,
                      LastName = se.User.LastName,
                      ImageName = se.User.ImageName,
                    //  created = Convert.ToDateTime(se.Created.ToShortTimeString()),
                     // created = se.Created.ToString("MM/dd HH:mm"),
                      created = se.Created.ToString("dd MMMM yyyy HH:mm"),
                      ProductId = se.ProductId,
                     TrueNumber=se.CommentLikes.Where(w=>w.CommentId==se.Id && w.LikeStatus==true).Count().ToString(),
                     FalseNumber=se.CommentLikes.Where(w=>w.CommentId==se.Id && w.LikeStatus==false).Count().ToString(),

                  })),

                  ProductPoint = se.ProductPoint.Where(x => x.ProductId == se.Id).Average(x => x.Point).ToString()
              }
              ).Where(filter).ToListAsync();                                        
            }
        }

        public async Task ProductUpdate(ProductUpdateDto product)
        {
            using (var context=new ProductInformationContext())
            {
                var entity = await context.Products.SingleOrDefaultAsync(x=>x.Id==product.Id);

                if (product.ProductName != null) entity.ProductName = product.ProductName;
                if (product.Content != null) entity.Content = product.Content;
                if (product.Title != null) entity.Title = product.Title;
                if (product.ProductTypeId != null) entity.ProductTypeId = product.ProductTypeId;
                entity.Updated = DateTime.Now;
                await context.SaveChangesAsync();
            }
        }
    }
}
