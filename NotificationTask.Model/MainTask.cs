using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.Model
{
    public class MainTask : NotificationBase.ITask
    {
        public string GetDescription()
        {
            return "Sending model missing info notification";
        }

        public void ExecuteTask()
        {
            int monitorUserID = 346;

            using (ModelNotificationEntities context = new ModelNotificationEntities(Library.Helper.CreateEntityConnectionString("ModelNotificationModel")))
            {
                int factoryID;
                string subject = string.Empty;
                string body = string.Empty;
                string toAddresses = string.Empty;

                foreach (Notification_Factory_View factory in context.Notification_Factory_View.ToList())
                {
                    factoryID = factory.FactoryID;
                    List<Notification_MissionInfoModel_View> items = context.Notification_function_GetMissingInfoModel(factoryID).ToList();
                    List<Notification_FactoryUser_View> users = context.Notification_FactoryUser_View.Where(o => o.FactoryID == factoryID).ToList();
                    var validUserIDs = context.AccountMng_UserPermission_View.Where(o => o.ModuleUD == "ModelMng" && o.CanRead.HasValue && o.CanRead.Value && o.CanUpdate.HasValue && o.CanUpdate.Value).Select(i => i.UserId).Distinct().ToList();
                    if (items.Count() > 0 && users.Count() > 0)
                    {
                        toAddresses = string.Empty;
                        subject = "Missing info items for factory: " + factory.FactoryUD + "(Date: " + DateTime.Now.ToString("dd/MMM/yyyy") + ")";
                        body = "<table border='1' border-collapse='collapse'>";
                        body += "   <tr>";
                        body += "       <th>Code</th>";
                        body += "       <th>Name</th>";
                        body += "       <th>Missing Info</th>";
                        body += "   </tr>";

                        foreach (Notification_MissionInfoModel_View item in items)
                        {
                            body += "<tr>";
                            body += "   <td style='vertical-align: top;'>"+item.ModelUD+"</td>";
                            body += "   <td style='vertical-align: top;'>" + item.ModelNM + "</td>";
                            body += "   <td style='vertical-align: top;'>";

                            if (item.IsModelUDOK == 0) { body += "Code; "; }
                            if (item.IsModelNMOK == 0) { body += "Name; "; }
                            if (item.IsProductTypeIDOK == 0) { body += "Type; "; }
                            if (item.IsManufacturerCountryIDOK == 0) { body += "Country; "; }
                            if (item.IsRangeNameOK == 0) { body += "Range Name; "; }
                            if (item.IsCollectionOK == 0) { body += "Collection; "; }
                            if (item.IsModelStatusIDOK == 0) { body += "Status; "; }
                            if (item.IsSeasonOK == 0) { body += "Season; "; }
                            if (item.IsProductGroupIDOK == 0) { body += "Product Group; "; }
                            if (item.IsProductCategoryIDOK == 0) { body += "Category; "; }
                            if (item.IsDimensionOK == 0) { body += "Dimension; "; }
                            if (item.IsFreeScanOK == 0) { body += "Freescan; "; }
                            if (item.IsPackagingMethodOK == 0) { body += "Packaging Method; "; }
                            if (item.IsLoadAbilityOK == 0) { body += "Load Ability"; }

                            body += "   </td>";
                            body += "</tr>";
                        }

                        body += "</table>";

                        foreach (Notification_FactoryUser_View user in users.Where(o=> validUserIDs.Contains(o.UserID.Value)))
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

                // create notification base on creator
                foreach (int userId in context.Notification_MissionInfoModel_View.Where(o=>o.CreatedBy.HasValue).Select(o=>o.CreatedBy).Distinct().ToList())
                {
                    List<Notification_MissionInfoModel_View> items = context.Notification_MissionInfoModel_View.Where(o => o.CreatedBy.HasValue && o.CreatedBy == userId).ToList();
                    Notification_EmailContact_View dbEmail = context.Notification_EmailContact_View.FirstOrDefault(o => o.UserId == userId);
                    Notification_EmailContact_View dbMonitorEmail = context.Notification_EmailContact_View.FirstOrDefault(o => o.UserId == monitorUserID);
                    if (dbEmail != null)
                    {
                        toAddresses = dbEmail.Email1;
                        if (!toAddresses.ToLower().Contains(dbMonitorEmail.Email1.ToLower()))
                        {
                            toAddresses += ";" + dbMonitorEmail.Email1;
                        }

                        subject = "Missing info items which is created by you (Date: " + DateTime.Now.ToString("dd/MMM/yyyy") + ")";
                        body = "<table border='1' border-collapse='collapse'>";
                        body += "   <tr>";
                        body += "       <th>Code</th>";
                        body += "       <th>Name</th>";
                        body += "       <th>Missing Info</th>";
                        body += "   </tr>";

                        foreach (Notification_MissionInfoModel_View item in items)
                        {
                            body += "<tr>";
                            body += "   <td style='vertical-align: top;'>" + item.ModelUD + "</td>";
                            body += "   <td style='vertical-align: top;'>" + item.ModelNM + "</td>";
                            body += "   <td style='vertical-align: top;'>";

                            if (item.IsModelUDOK == 0) { body += "Code; "; }
                            if (item.IsModelNMOK == 0) { body += "Name; "; }
                            if (item.IsProductTypeIDOK == 0) { body += "Type; "; }
                            if (item.IsManufacturerCountryIDOK == 0) { body += "Country; "; }
                            if (item.IsRangeNameOK == 0) { body += "Range Name; "; }
                            if (item.IsCollectionOK == 0) { body += "Collection; "; }
                            if (item.IsModelStatusIDOK == 0) { body += "Status; "; }
                            if (item.IsSeasonOK == 0) { body += "Season; "; }
                            if (item.IsProductGroupIDOK == 0) { body += "Product Group; "; }
                            if (item.IsProductCategoryIDOK == 0) { body += "Category; "; }
                            if (item.IsDimensionOK == 0) { body += "Dimension; "; }
                            if (item.IsFreeScanOK == 0) { body += "Freescan; "; }
                            if (item.IsPackagingMethodOK == 0) { body += "Packaging Method; "; }
                            if (item.IsLoadAbilityOK == 0) { body += "Load Ability"; }

                            body += "   </td>";
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
    }
}
