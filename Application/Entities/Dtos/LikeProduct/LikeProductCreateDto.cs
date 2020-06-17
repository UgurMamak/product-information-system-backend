using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.LikeProduct
{
   public class LikeProductCreateDto:IDto
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }//postu beğeenen kullanıcı
        public bool LikeStatus { get; set; }
    }
}
