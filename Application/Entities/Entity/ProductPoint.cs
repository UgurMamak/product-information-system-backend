using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class ProductPoint:BaseEntity
    {

        public ProductPoint()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string ProductId { get; set; }
        public string UserId { get; set; }
        public int Point { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
