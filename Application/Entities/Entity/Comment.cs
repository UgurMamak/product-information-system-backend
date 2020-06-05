using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class Comment:BaseEntity
    {
        public Comment()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public List<CommentLike> CommentLikes { get; set; }
    }
}
