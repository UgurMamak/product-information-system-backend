using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.CommetLike
{
   public  class CommentLikeDto:IDto
    {
        public CommentLikeDto() { }
        public int TrueNumber { get; set; }
        public int FalseNumber { get; set; }
        public string Message { get; set; }//hangi işlem yapıldığını dönecek.
    }
}
