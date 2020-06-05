using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class Category:BaseEntity
    {
        public Category()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string CategoryName { get; set; }
    }
}
