using Application.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.ProductImage
{
   public  class ImageDeleteDto:IDto
    {
    
       public ImageList[] imageLists { get; set; }
    }

    public class ImageList
    {
        public string Id { get; set; }//ImageId (Add ve delete işlemi için)
     //   public IFormFile Image { get; set; }//Add işlemi için
        public string ImageName { get; set; }//delete işlemi için
       // public string ProductId { get; set; } //add işlemi için
    }

}
