using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.FactoryCertificate
{
    public class MainTask : NotificationBase.ITask
    {
        public void ExecuteTask()
        {
            using (EmailNotificationEntities context = new EmailNotificationEntities(Library.Helper.CreateEntityConnectionString("EmailNotificationModel")))
            {
                // Run add data check valid condition.
                context.EmailNotificationMessage_AutoCheckFactoryCertificateValidUntil();
            }
        }

        public string GetDescription()
        {
            return "Sending email factory certificate";
        }
    }
}
