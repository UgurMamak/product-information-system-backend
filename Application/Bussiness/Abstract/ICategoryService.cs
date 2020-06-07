using Application.Core.Utilities.Results;
using Application.Entities.Dtos.Category;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
   public interface ICategoryService
    {
        Task<IResult>  Add(Category category);

        Task<IResult> CategoryExists(string categoryName);

        Task<IDataResult<IList<CategoryListDto>>> GetList();
    }
}
