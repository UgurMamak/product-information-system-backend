using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Application.Entities.Entity
{
    public class Product:BaseEntity
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string ProductName { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public string UserId { get; set; }
        public string ProductTypeId { get; set; }
        public User User { get; set; }
        public List<Image> Images { get; set; }
        public List<Comment> Comments { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<LikeProduct> LikeProducts { get; set; }

        public ProductType ProductType { get; set; }
    }
}
