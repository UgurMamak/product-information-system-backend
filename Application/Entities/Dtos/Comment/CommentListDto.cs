using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Comment
{
    public class CommentListDto:IDto
    {
        public string Id { get; set; }//comment Id
        public string Content { get; set; }
        public string UserId { get; set; }//yorumu yapan Id
        public string FirstName { get; set; }//yorumu yapan 
        public string LastName { get; set; }//yorumu yapan 
        public string ImageName { get; set; }//yorumu yapan 
        public string ProductId { get; set; }
        //public DateTime created { get; set; }
        public string created { get; set; }

        public string TrueNumber { get; set; }//**
        public string FalseNumber { get; set; }//**

        //public string status { get; set; }//like dislike sayısı için 

    }
}
