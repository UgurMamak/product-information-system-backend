using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class Image : BaseEntity
    {
        public Image()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string ImageName { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }


    }
}
