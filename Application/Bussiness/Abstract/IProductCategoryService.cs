using Application.Core.Utilities.Results;
using Application.Entities.Dtos.ProductCategory;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface IProductCategoryService
    {
       Task<IResult> Add(ProductCategoryCreateDto productCategoryCreateDto);
        Task<IResult> DeleteByProductId(string postId);
        Task<IResult> Delete(ProductCategory productCategory);//belli kayıtları silmek için
    }
}
