using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Comment
{
    public class CommentUpdateDto:IDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
