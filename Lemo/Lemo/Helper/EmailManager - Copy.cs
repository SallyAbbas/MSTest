using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;

namespace Lemo.Helper
{
    public class EmailManager
    {
        /// <summary>
        /// send email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Belal Salah El-Din at 19-2-2012</remarks>
        public static bool SendEmail(string subject, string body, string toEmail, bool isEnglish, bool toAdmin=false)
        {
            bool isMailSent = false;
            try
            {
                MailMessage mailMessage;
                if(!toAdmin)
                    mailMessage = new MailMessage(ConfigurationManager.AppSettings["AdminEmail"],
                                                              toEmail, subject, body);
                else
                    mailMessage = new MailMessage(ConfigurationManager.AppSettings["AdminEmail"],
                                                  ConfigurationManager.AppSettings["AdminEmail"], subject, body);
                mailMessage.IsBodyHtml = true;
                isMailSent = SendEmail(mailMessage);
                return isMailSent;
            }
            catch (Exception)
            {
                //mail did not sent
                return isMailSent;
            }
        }
        /// <summary>
        /// Send Mails To the User who fire the Import Process.
        /// </summary>
        private static bool SendEmail(MailMessage mailMessage)
        {
            try
            {
                //create a mail client
                SmtpClient mailClient = new SmtpClient("mail.Limoallover.net ", 25);
                //Int32 port;
                //Int32.TryParse(ConfigurationModel.SMTPMailServerPort, out port);
                //if (port > 0)
                //{
                //    mailClient = new SmtpClient(ConfigurationModel.SMTPMailServer, port);
                //}
                //else
                //{
                //    mailClient = new SmtpClient(ConfigurationModel.SMTPMailServer);
                //}
                //mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = new System.Net.NetworkCredential("info@limoallover.net", "Zyad#123456");
                mailClient.EnableSsl = false;

                //send the e-mail, but first make change Credentials to the One in the Configuration .
                
                mailClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                /* just log the issue */
                return false;
            }
        }   
    }
}