using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace TemperatureApp
{
    public class email
    {
        
        public static void composeEmail()
        {


            
            MailMessage mail = new MailMessage("SMTP_Injection@sparkpostmail.com", "peter.i.macaldowie@gmail.com", "this is a test email.", "this is my test email body");
            SmtpClient client = new SmtpClient("smtp.sparkpostmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("SMTP_Injection", "fae8840c80dd966210101be0dfdc9ea4e709014d");
            client.Send(mail);
        }
            
    }
}
