using Application.Core.Utilities.Results;
using Application.Entities.Dtos.ProductType;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface IProductTypeService
    {
        Task<IResult> Add(ProductType productType);

        Task<IResult> TypeExists(string typeName);

        Task<IDataResult<IList<ProductTypeListDto>>> GetList();

        Task<IResult> Delete(ProductType productType);
    }
}
