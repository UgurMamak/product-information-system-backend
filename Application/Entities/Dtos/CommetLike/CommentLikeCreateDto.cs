using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.CommetLike
{
   public  class CommentLikeCreateDto:IDto
    {
        public string  CommentId { get; set; }
        public string  userId { get; set; }
        public bool LikeStatus { get; set; }
    }
}
