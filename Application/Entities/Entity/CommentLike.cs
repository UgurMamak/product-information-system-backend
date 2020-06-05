using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class CommentLike:BaseEntity
    {
        public CommentLike()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public bool LikeStatus { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
