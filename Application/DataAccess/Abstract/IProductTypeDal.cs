using Application.Core.DataAccess;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataAccess.Abstract
{
    public interface IProductTypeDal:IEntityRepository<ProductType>
    {
    }
}
