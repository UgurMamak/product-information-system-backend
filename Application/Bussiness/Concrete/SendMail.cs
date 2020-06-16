using Application.Entities.Dtos.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Bussiness.Concrete
{
    public class SendMail
    {
        
        public void Mail(MailCreateDto mailCreateDto, string body)
        {
            MailMessage msg = new MailMessage();
            msg.Subject = mailCreateDto.Subject;
            msg.From = new MailAddress("ugurmamak98@gmail.com", "uğur mamak");//burası hep böyle kalmalı

            //msg.To.Add(new MailAddress("ugurmamak98@gmail.com", "uğur mamak"));//mesajın gittiği adres
            msg.To.Add(new MailAddress(mailCreateDto.Mail, mailCreateDto.Name));//mesajın gittiği adres

            msg.Body = body;

            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            if (mailCreateDto.File != null)
            {
                Attachment data = new Attachment(mailCreateDto.File.OpenReadStream(), mailCreateDto.File.FileName);
                msg.Attachments.Add(data);
            }
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
            NetworkCredential AccountInfo = new NetworkCredential("ugurmamak98@gmail.com", "134ugur2163");//alan kişi
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(msg);
        }
    }
}
