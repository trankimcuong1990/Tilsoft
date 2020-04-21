using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.StockStatusQnt
{
    public class MainTask : NotificationBase.ITask
    {
        public void ExecuteTask()
        {
            using (StockStatusQntNotificationEntities context = new StockStatusQntNotificationEntities(Library.Helper.CreateEntityConnectionString("StockStatusQntNotificationModel")))
            {
                string subject = string.Empty;
                string body = string.Empty;
                string toAddresses = string.Empty;
                List<StockStatusQntRpt_StockStatusQnt_View> items = context.StockStatusQntRpt_StockStatusQnt_View.Where(o => o.StockQnt <= o.Average).ToList();
                List<Notification_StockStatusQntUser_View> users = context.Notification_StockStatusQntUser_View.ToList();
                List<ProductionItemMng_ProductionItemGroup_View> itemGroups = context.ProductionItemMng_ProductionItemGroup_View.ToList();
                foreach (ProductionItemMng_ProductionItemGroup_View itemGroup in itemGroups)
                {
                    if (users.Where(o => o.ProductionItemGroupID == itemGroup.ProductionItemGroupID).ToList().Count() > 0 && items.Where(o => o.ProductionItemGroupID == itemGroup.ProductionItemGroupID).ToList().Count() > 0)
                    {
                        subject = string.Empty;
                        body = string.Empty;
                        toAddresses = string.Empty;
                        foreach (Notification_StockStatusQntUser_View user in users.Where(o => o.ProductionItemGroupID == itemGroup.ProductionItemGroupID).ToList())
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

                        subject = "Notification for stock status quantity of material " + itemGroup.ProductionItemGroupNM + " - (Date: " + DateTime.Now.ToString("dd/MMM/yyyy") + ")";
                        body += "<p><b>Check date: </b>" + DateTime.Now.ToString("dd/MMM/yyyy") + "</p>";
                        body += "<p><b>Material group: </b>" + itemGroup.ProductionItemGroupNM + "</p>";
                        body += "<table border='1' style = 'border-collapse: collapse'>";
                        body += "   <tr style = 'background-color:yellow'>";
                        body += "       <th>Code</th>";
                        body += "       <th>Name</th>";
                        body += "       <th>Unit</th>";
                        body += "       <th>Stock QNT</th>";
                        body += "       <th>Min QNT</th>";
                        body += "       <th>Average QNT</th>";
                        body += "       <th>Max QNT</th>";
                        body += "       <th>Alert Level</th>";
                        body += "   </tr>";
                        foreach (StockStatusQntRpt_StockStatusQnt_View item in items.Where(o => o.ProductionItemGroupID == itemGroup.ProductionItemGroupID).ToList())
                        {
                            body += "<tr>";
                            body += "   <td style='vertical-align: top;'>" + item.ProductionItemUD + "</td>";
                            body += "   <td style='vertical-align: top;'>" + item.ProductionItemNM + "</td>";
                            body += "   <td style='vertical-align: top;'>" + item.Unit + "</td>";
                            body += "   <td style='vertical-align: top;'>" + item.StockQnt + "</td>";
                            body += "   <td style='vertical-align: top;'>" + item.Minimum + "</td>";
                            body += "   <td style='vertical-align: top;'>" + item.Average + "</td>";
                            body += "   <td style='vertical-align: top;'>" + item.Maximum + "</td>";
                            if (item.StockQnt <= item.Minimum && item.Minimum > 0)
                            {
                                body += "   <td style='vertical-align: top;'>Minimum</td>";
                            }
                            else if (item.StockQnt > item.Minimum && item.StockQnt <= item.Average && item.Average > 0)
                            {
                                body += "   <td style='vertical-align: top;'>Average</td>";
                            }
                            body += "</tr>";
                        }
                        body += "</table>";

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
        }

        public string GetDescription()
        {
            return "Sending notification for stock status quantity of material";
        }
    }
}
