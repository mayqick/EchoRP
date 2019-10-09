using System;
using System.Net;
using System.Net.Mail;
using GTANetworkAPI;
namespace Wave.Global
{
    class Mail
    {
        private const string senderAdrees = "noreply@echorp.com";
        private const string senderPassword = "1Oc]GjmaB;H-";
        private const string senderName = "echorp.com";

        public static void SendMailToPlayer(string targetAdress, string msgTitle, string msg)
        {
            try
            {
                var fromAddress = new MailAddress(senderAdrees, senderName);
                var toAddress = new MailAddress(targetAdress, "");
                const string fromPassword = senderPassword;
                var subject = msgTitle;
                var body = msg;

                var smtp = new SmtpClient
                {
                    Host = "piter21.dns-rus.net",
                    Port = 465,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                    smtp.Send(message);
            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput(e.Message);
                throw;
            }
        }
    }
}
