using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.EmailNotification
{
    public class MainTask : NotificationBase.ITask
    {
        public string GetDescription()
        {
            return "Sending email notification";
        }

        public void ExecuteTask()
        {
            // get list of email to be sent
            List<EmailNotificationMessage> messages = new List<EmailNotificationMessage>();
            using (EmailNotificationEntities context = new EmailNotificationEntities(Library.Helper.CreateEntityConnectionString("EmailNotificationModel")))
            {
                messages = context.EmailNotificationMessage.Where(o => o.ErrorMessage == null && !string.IsNullOrEmpty(o.SendTo)     && (!o.IsSent.HasValue || !o.IsSent.Value)).Take(20).ToList();
                Console.WriteLine("Preparing to send " + messages.Count().ToString() + " emails: ");
            }

            // send email
            string subject = string.Empty;
            string body = string.Empty;
            List<string> toAddresses = new List<string>();
            List<EmailNotificationMessage> toBeRemove = new List<EmailNotificationMessage>();
            foreach (EmailNotificationMessage message in messages)
            {
                toAddresses.Clear();
                foreach (string emailAddress in message.SendTo.Split(';').Where(o => !string.IsNullOrEmpty(o)))
                {
                    toAddresses.Add(emailAddress);
                }
                body = message.EmailBody;
                subject = message.EmailSubject;

                try
                {
                    Console.Write("Sending mail: " + subject + "...");
                    Library.Common.Helper.SendMail(toAddresses, subject, body, new List<string>());
                    message.IsSent = true;
                    Console.WriteLine("done!");
                    //context.EmailNotificationMessage.Remove(message);
                    //toBeRemove.Add(message);
                }
                catch (Exception ex)
                {
                    message.LastAttempt = DateTime.Now;
                    message.ErrorMessage = ex.Message;
                    Console.WriteLine("");
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            // update status to database
            try
            {
                using (EmailNotificationEntities context = new EmailNotificationEntities(Library.Helper.CreateEntityConnectionString("EmailNotificationModel")))
                {
                    foreach (EmailNotificationMessage message in messages)
                    {
                        EmailNotificationMessage dbItem = context.EmailNotificationMessage.FirstOrDefault(o => o.EmailNotificationMessageID == message.EmailNotificationMessageID);
                        if (dbItem != null)
                        {
                            dbItem.IsSent = message.IsSent;
                            dbItem.LastAttempt = message.LastAttempt;
                            dbItem.ErrorMessage = message.ErrorMessage;
                        }

                    }
                    context.SaveChanges();
                }
            }
            catch { }
        }
    }
}
