using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.ProductCategory
{
    public class ProductCategoryCreateDto:IDto
    {
        public string CategoryId { get; set; }
        public string ProductId { get; set; }
    }
}