using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class ProductCategory:BaseEntity
    {
        public ProductCategory()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string ProductId { get; set; }
        public string  CategoryId { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
