using System;
using System.Net;
using System.Net.Mail;
using GTANetworkAPI;
using System.Threading.Tasks;
namespace Wave.Global
{
    class Mail
    {
        private const string SenderAdrees = "noreply@echorp.com";
        private const string SenderPassword = "n!W]0xTYia!F";
        /*private const string SenderName = "Echo Role Play";*/
        private const string SenderServer = "piter21.dns-rus.net";

        // Метод асинхронной отправки письма. Аргументы: почта игрока, имя отправителя (заголовок письма при просмотре),
        // заголовок письма и текст письма.
        public static void SendEmailAsync(string playerMail, string senderName, string title, string message)
        {
            try
            {

                MailAddress from = new MailAddress(SenderAdrees, senderName);
                MailAddress to = new MailAddress(playerMail);
                MailMessage m = new MailMessage(from, to);
                m.Subject = title;
                m.Body = message;
                SmtpClient smtp = new SmtpClient(SenderServer, 25);
                smtp.Credentials = new NetworkCredential(SenderAdrees, SenderPassword);
                smtp.EnableSsl = true;
                smtp.SendMailAsync(m);
                NAPI.Util.ConsoleOutput("Письмо отправлено на почтовый адрес {0}", playerMail);
            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput(e.Message);
            }
        }
    }
}
