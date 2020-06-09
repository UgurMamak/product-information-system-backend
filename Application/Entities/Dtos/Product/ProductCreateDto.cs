using Application.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
    public class ProductCreateDto:IDto
    {       
        public string ProductName { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string ProductTypeId { get; set; }
       public IFormFile[] ProductImages { get; set; }
        //  public List<ProductImageDto> ProductImages { get; set; }

        public string[] Categories { get; set; }
        //public List<ProductCategoryListDto> Categories { get; set; }    
    }
}
