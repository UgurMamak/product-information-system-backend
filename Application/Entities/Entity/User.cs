using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Entity
{
    public class User:BaseEntity
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
        public int RoleId { get; set; }
        public OperationClaim OperationClaim { get; set; }
        public List<Product> Products { get; set; }

       // public List<LikeProduct> LikeProducts { get; set; }
    }
}

