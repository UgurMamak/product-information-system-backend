using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
    public class ProductUpdateDto : IDto
    {
        public string Id { get; set; }//productId
        public string Title { get; set; }
        public string Content { get; set; } 
        public string ProductName { get; set; }
        public string ProductTypeId { get; set; }
        public DateTime Updated { get; set; }
        public List<ProductImageDto> ProductImages { get; set; } //Gelen resim bilgilerini güncellemek için

        // public string ImageName { get; set; }// eski image ismi
        //public IFormFile Image { get; set; } //yeni gelen  image

        //   public bool isActive { get; set; } //is active değerini güncellemek için
        // public bool isDeleted { get; set; } //is active değerini güncellemek için

        //  public string PostCategoryId { get; set; }// 
        //   public string CategoryId { get; set; }//postu yazan
        //  public string UserId { get; set; }//postu yazan
    }
}
