using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
   public  class ProductImageListDto:IDto
    {
        public string Id { get; set; }
        public string ImageName { get; set; }
    }
}
