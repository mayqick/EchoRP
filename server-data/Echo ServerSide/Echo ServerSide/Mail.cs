using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using CitizenFX.Core.Native;
using CitizenFX.Core;
using System.Net;

namespace Echo_ServerSide 
{
    class Mail 
    {
        private const string SenderAdrees = "noreply@echorp.com";
        private const string SenderPassword = "5hiGw-Sdcrj3";
        private const string SenderServer = "piter21.dns-rus.net";

        // Метод асинхронной отправки письма. Аргументы: почта игрока, имя отправителя (заголовок письма при просмотре),
        // заголовок письма и текст письма.
        public static async void SendEmailAsync(string playerMail, string senderName, string title, string message)
        {
            try
            {

                MailAddress from = new MailAddress(SenderAdrees, senderName);
                MailAddress to = new MailAddress(playerMail);
                MailMessage m = new MailMessage(from, to);
                m.Subject = title;
                m.Body = message;
                SmtpClient smtp = new SmtpClient(SenderServer, 2083);
                smtp.Credentials = new NetworkCredential(SenderAdrees, SenderPassword);
                smtp.EnableSsl = true;
                smtp.SendMailAsync(m);
                Debug.WriteLine("Письмо отправлено на почтовый адрес {0}", playerMail);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
