﻿using Application.Core.Utilities.Results;
using Application.Entities.Dtos.Product;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
   public interface IProductService
    {
       Task<IDataResult<Product>> Add(ProductCreateDto postCreateDto);
    }
}

