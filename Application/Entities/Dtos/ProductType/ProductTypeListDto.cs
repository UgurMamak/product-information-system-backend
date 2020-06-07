using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.ProductType
{
    public class ProductTypeListDto:IDto
    {
        public string Id { get; set; }
        public string TypeName { get; set; }
    }
}
