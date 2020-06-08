using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
   public class ProductCategoryListDto:IDto
    {
        public string CategoryId { get; set; }
    }
}
