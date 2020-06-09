using Application.Core.Entities;
using Application.Entities.Dtos.ProductCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
   public class ProductCartDto:IDto
    {
        public string ProductId { get; set; }//Product 
        public string ProductName { get; set; }//Product
        public string ProductType { get; set; }//Product
        public DateTime Created { get; set; }//Product
        
        public string UserId { get; set; }//User
        public string FirstName { get; set; }//User 
        public string LastName { get; set; }//User
        public int CommentNumber { get; set; }//posta yapılan yorum sayısını tutmak için
     
        public List<ProductCategoryDto> productCategoryDtos { get; set; }
        public List<ProductImageListDto> productImageListDtos { get; set; }
    }
}
