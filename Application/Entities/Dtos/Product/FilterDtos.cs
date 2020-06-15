using Application.Core.Entities;
using Application.Entities.Dtos.ProductCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
    public class FilterDtos:IDto
    {
        public string[] ProductType { get; set; }
        //public string[] Categories { get; set; }
        public List<ProductCategoryDto> Categories { get; set; }
    }
}
