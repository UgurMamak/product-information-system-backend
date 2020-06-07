using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class ProductType:BaseEntity
    {
        public ProductType()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string ProductTypeName { get; set; }
        public Product Product { get; set; }
    }
}
