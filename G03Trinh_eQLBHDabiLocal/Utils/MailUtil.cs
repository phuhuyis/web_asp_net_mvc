using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using System.Configuration;

namespace G03Trinh_eQLBHDabiLocal.Utils
{
    public class MailUtil
    {
        public static bool sendMail(String subject, String body, String email)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(email);
            message.To.Add(email);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings.Get("mail.host"));
            smtpClient.Port = int.Parse(ConfigurationManager.AppSettings.Get("mail.port"));
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings.Get("mail.username"), ConfigurationManager.AppSettings.Get("mail.password"));
            smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(message);
                message.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}