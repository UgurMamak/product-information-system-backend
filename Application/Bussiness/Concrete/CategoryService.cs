using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.Category;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class CategoryService : ICategoryService
    {

        private ICategoryDal _categoryDal;
        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }


        public async Task<IResult> Add(Category category)
        {
            await _categoryDal.Add(category);
            return  new SuccessResult(Messages.CategoryAdded);
        }

        public async Task<IResult> CategoryExists(string categoryName)
        {
            //yazılan kategori var mı yok mu kontrol edilir.
            var exist =await _categoryDal.Get(c => c.CategoryName.ToLower() == categoryName.ToLower());

            if (exist != null)
            {
                return new ErrorResult(Messages.CategoryAlreadyExists);//eğer kategori varsa ErrorDataResult döndüreceğiz.
            }
            return new SuccessResult();
        }

        public async Task<IDataResult<IList<CategoryListDto>>> GetList()
        {
            var entity = await _categoryDal.GetList();
            var data = new List<CategoryListDto>(
             entity.Select(se => new CategoryListDto { CategoryName = se.CategoryName, Id = se.Id })
             ).ToList();
            return new SuccessDataResult<List<CategoryListDto>>(data);
        }

        public async Task<IResult> Delete(Category category)
        {
           await _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

    }
}
