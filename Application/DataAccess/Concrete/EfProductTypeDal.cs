using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataAccess.Concrete
{
    public class EfProductTypeDal:EfEntityRepositoryBase<ProductType,ProductInformationContext>,IProductTypeDal
    {
    }
}
