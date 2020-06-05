using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class LikeProduct : BaseEntity
    {
        public LikeProduct()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public bool LikeStatus { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
