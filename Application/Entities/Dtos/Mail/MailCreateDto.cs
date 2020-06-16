using Application.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Mail
{
  public  class MailCreateDto:IDto
    {
        public string Mail { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
