using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.User
{
    public class UserListDto : IDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }//yorumu yapan
    }
}
