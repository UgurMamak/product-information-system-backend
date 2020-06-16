using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Concrete;
using Application.Entities.Dtos.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {

        //iletişim ekranı için yazıldı.
        [HttpPost("mailsend")]
        public async Task<IActionResult> SendMail([FromForm] MailCreateDto mailCreateDto)
        {
            try
            {
                string body = "<b>Ad Soyad:</b>" + mailCreateDto.Name + "<br/>" + "<b>Mail adresim:</b>" + mailCreateDto.Mail + "<br/>" + mailCreateDto.Content;
                mailCreateDto.Mail = "ugurmamak98@gmail.com";
                mailCreateDto.Name = "uğur mamak";
                SendMail send = new SendMail();
                send.Mail(mailCreateDto, body);
            }
            catch (Exception ex)
            {
                return BadRequest("Mesaj iletirken hata oluştu.");
                throw ex;
            }
            return Ok("Mesajınız iletildi");
        }
    }
}