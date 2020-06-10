using Application.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
    public class ProductImageDto : IDto
    {
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
    }
}
