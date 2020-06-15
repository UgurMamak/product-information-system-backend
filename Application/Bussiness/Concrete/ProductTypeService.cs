using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.ProductType;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class ProductTypeService : IProductTypeService
    {
        private IProductTypeDal _productTypeDal;
        public ProductTypeService(IProductTypeDal productTypeDal)
        {
            _productTypeDal = productTypeDal;
        }
        public async Task<IResult> Add(ProductType productType)
        {
            await _productTypeDal.Add(productType);
            return new SuccessResult(Messages.ProductTypeAdded);

        }

        public async Task<IDataResult<IList<ProductTypeListDto>>> GetList()
        {
            var entity = await _productTypeDal.GetList();
            var data = new List<ProductTypeListDto>(
             entity.Select(se => new ProductTypeListDto { Id=se.Id, TypeName=se.ProductTypeName })
             ).OrderBy(x=>x.TypeName).ToList();
            return new SuccessDataResult<List<ProductTypeListDto>>(data);
        }

        public async Task<IResult> TypeExists(string typeName)
        {
            //yazılan kategori var mı yok mu kontrol edilir.
            var exist = await _productTypeDal.Get(x=>x.ProductTypeName.ToLower()==typeName.ToLower());

            if (exist != null)
            {
                return new ErrorResult(Messages.ProductTypeAlreadyExists);//eğer kategori varsa ErrorDataResult döndüreceğiz.
            }
            return new SuccessResult();
            
        }
    }
}
