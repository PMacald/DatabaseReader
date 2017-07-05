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
        
        public static void composeEmail(SpreadsheetFile sf, string receiver)
        {
            sf.datalist = sf.assembleData(sf);
            string emailBody = "Thermometer Data:\n\n";
            foreach (Record row in sf.datalist)
            {
                emailBody += $"{row.dateAndTime}\t{row.serialNo}\t{row.reading}\t{row.units}\n";
            }
            MailMessage mail = new MailMessage("Paul@otsys.co.uk", receiver, "Thermometer Data", emailBody);
            SmtpClient client = new SmtpClient("smtp.sparkpostmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("SMTP_Injection", "fae8840c80dd966210101be0dfdc9ea4e709014d");
            try {
                client.Send(mail);
                Console.WriteLine("Email Sent! Check your inbox!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Email failed to send due to this error: {e}");
            }
            
        }
    }
}
