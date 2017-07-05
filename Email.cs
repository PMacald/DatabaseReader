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



            MailMessage mail = new MailMessage("iidozx@gmail.com", "peter.i.macaldowie@gmail.com", "this is a test email.", "this is my test email body");
            SmtpClient client = new SmtpClient("smtp.google.com",465);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("iidozx@gmail.com", "Maximum12345");
            client.Send(mail);
        }
            
    }
}
