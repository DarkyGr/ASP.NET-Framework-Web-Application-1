using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ASP.NET_Web_App_NetFramework_1.Models;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MimeKit;
using ASP.NET_Web_App_NetFramework_1.Controllers;

namespace ASP.NET_Web_App_NetFramework_1.Services
{
    public static class EmailService
    {
        private static string _Host = "lsmtp.gamil.com";
        private static int _Port = 587;

        private static string _From = "Unknow 101";
        // This email is our
        private static string _Email = "[your email]";
        // The password is app password
        private static string _Password = "[your app password]";


        // Send Method
        public static bool Send(EmailDTO emailDTO)
        {
            try
            {
                var email = new MimeMessage();

                // From
                email.From.Add(new MailboxAddress(_From, _Email));
                // To
                email.To.Add(MailboxAddress.Parse(emailDTO.To));
                // Subject
                email.Subject = emailDTO.Subject;
                // Body
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailDTO.Body
                };

                var smtp = new SmtpClient();
                // Conect the SMTP with the host
                smtp.Connect(_Host, _Port, SecureSocketOptions.StartTls);

                // Authenticate
                smtp.Authenticate(_Email, _Password);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;

            }catch{ 
                return false;
            }
        }
    }
}