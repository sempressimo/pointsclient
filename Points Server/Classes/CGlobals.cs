using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Points_Server
{
    class CGlobals
    {
        static public string AppKey = "481ed410-2ae9-484c-a435-0764c2280a73";

        static public int Count_New_Customers = 0;
        static public int Count_Updated_Customers = 0;

        static public int Count_New_RO = 0;
        static public int Count_Repeated_RO = 0;
        static public int Count_Rejected_RO = 0;

        static private string CreateEmailBody(string Customer, string Points)
        {
            return "this is my test email body";
        }

        static public string SendEmail(string To, string CustomerName, string Balance)
        {
            try
            {
                //MailMessage mail = new MailMessage("ismael.placa@gmail.com", r["email"].ToString());
                MailMessage mail = new MailMessage(Properties.Settings.Default.From, To);

                NetworkCredential basicCredential = new NetworkCredential("username", Properties.Settings.Default.Password);

                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(Properties.Settings.Default.Port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (Properties.Settings.Default.UseDefaultCredentials)
                {
                    client.UseDefaultCredentials = true;
                }
                else
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential;
                }

                client.Host = Properties.Settings.Default.Smtp;
                mail.Subject = "Utilice sus puntos en Autocentro Toyota";
                mail.Body = CreateEmailBody(CustomerName, Balance);

                client.Send(mail);

                return "Sent";
            }
            catch (Exception E)
            {
                return E.Message;                
            }
        }
    }
}