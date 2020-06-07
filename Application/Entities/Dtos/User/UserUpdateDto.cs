using Application.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.User
{
    public class UserUpdateDto : IDto
    {
        public string Id { get; set; }//güncellemek için user Id bilgisine ihtiyaç var
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public string password { get; set; }
    }
}
