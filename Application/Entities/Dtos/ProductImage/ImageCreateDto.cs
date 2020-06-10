using Application.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.ProductImage
{
    public class ImageCreateDto:IDto
    {
        public IFormFile[] ProductImages { get; set; }
        public string  ProductId { get; set; }
    }
}
