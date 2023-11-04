using MVC.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace MVC.PL.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.ethereal.email",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("mack.crona38@ethereal.email", "HUYxzDz883EaG5CSVJ");
            client.Send("mohamed@gmail.com",email.To,email.Title,email.Body);
        }
    }
}
