using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
namespace Application.Entities.Dtos.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string Role { get; set; }//***********
        public string processType { get; set; } //register işlemini yapan kişi admin mi normal kullanıcı mı kontrol ediyoruz.
    }
}
