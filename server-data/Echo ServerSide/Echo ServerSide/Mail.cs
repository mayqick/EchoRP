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
        private const string SENDER_ADRESS = "noreply@echorp.com";
        private const string SENDER_PASSWORD = "5hiGw-Sdcrj3";
        private const string SENDER_SERVER = "piter21.dns-rus.net";

        // Метод асинхронной отправки письма. Аргументы: почта игрока, имя отправителя (заголовок письма при просмотре),
        // заголовок письма и текст письма.
        public static async Task SendEmailAsync(string playerMail, string senderName, string title, string message)
        {

            try
            {

                MailAddress from = new MailAddress(SENDER_ADRESS, senderName);
                MailAddress to = new MailAddress(playerMail);
                MailMessage m = new MailMessage(from, to);
                m.Subject = title;
                m.Body = message;
                SmtpClient smtp = new SmtpClient(SENDER_SERVER, 465);
                smtp.Credentials = new NetworkCredential(SENDER_ADRESS, SENDER_PASSWORD);
                smtp.EnableSsl = true;
                smtp.SendMailAsync(m);
                Debug.WriteLine("Send Mail to {0}", playerMail);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
