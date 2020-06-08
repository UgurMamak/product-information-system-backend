using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Comment
{
    public class CommentCreateDto:IDto
    {
        public string Content { get; set; }//yorum
        public string UserId { get; set; }//yorumu yazan
        public string ProductId { get; set; }//yazdığı post
    }
}
