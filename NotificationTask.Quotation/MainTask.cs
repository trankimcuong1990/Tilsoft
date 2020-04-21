using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.Quotation
{
    public class MainTask : NotificationBase.ITask
    {
        public void ExecuteTask()
        {
            using (QuotationNotificationEntities context = new QuotationNotificationEntities(Library.Helper.CreateEntityConnectionString("QuotationNotificationModel")))
            {
                string subject = string.Empty;
                string body = string.Empty;
                string toAddresses = string.Empty;
                string a = context.Database.Connection.State.ToString();
                List<Notification_QuotationPrice_View> items = context.Notification_QuotationPrice_View.ToList();
                List<Notification_QuotationUser_View> users = context.Notification_QuotationUser_View.ToList();
                if (items.Count() > 0)
                {
                    toAddresses = string.Empty;
                    subject = "Notification for the price updating from factory" + "(Date: " + DateTime.Now.ToString("dd/MMM/yyyy") + ")";
                    body += "<p><b>Offer date: </b>" + DateTime.Now.ToString("dd/MMM/yyyy") + "</p>";
                    body += "<table border='1' style = 'border-collapse: collapse'>";
                    body += "   <tr style = 'background-color:yellow'>";
                    body += "       <th>Client</th>";
                    body += "       <th>PI</th>";
                    body += "       <th>Article Code</th>";
                    body += "       <th>Description</th>";
                    body += "       <th>Factory</th>";
                    body += "       <th>Price</th>";
                    body += "       <th>Update By</th>";
                    body += "   </tr>";
                    foreach (Notification_QuotationPrice_View item in items)
                    {
                        body += "<tr>";
                        body += "   <td style='vertical-align: top;'>" + item.Client + "</td>";
                        body += "   <td style='vertical-align: top;'>" + item.ProformaInvoiceNo + "</td>";
                        body += "   <td style='vertical-align: top;'>" + item.ArticleCode + "</td>";
                        body += "   <td style='vertical-align: top;'>" + item.Description + "</td>";
                        body += "   <td style='vertical-align: top;'>" + item.FactoryUD + "</td>";
                        body += "   <td style='vertical-align: top;'>" + item.Price + "</td>";
                        body += "   <td style='vertical-align: top;'>" + item.UpdateBy + "</td>";
                        body += "</tr>";
                    }
                    body += "</table>";

                    foreach (Notification_QuotationUser_View user in users)
                    {
                        if (!string.IsNullOrEmpty(toAddresses))
                        {
                            toAddresses += ";" + user.Email;
                        }
                        else
                        {
                            toAddresses += user.Email;
                        }

                    }

                    try
                    {
                        EmailNotificationMessage msg = new EmailNotificationMessage();
                        msg.EmailBody = body;
                        msg.SendTo = toAddresses;
                        msg.EmailSubject = subject;
                        context.EmailNotificationMessage.Add(msg);
                        context.SaveChanges();
                    }
                    catch { }
                }
            }
        }

        public string GetDescription()
        {
            return "Sending notification for the price updating from factory";
        }
    }
}
