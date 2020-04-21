using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.Employee
{
    public class MainTask : NotificationBase.ITask
    {
        public string GetDescription()
        {
            return " Sending employee contract period notification ";
        }
        public void ExecuteTask()
        {
            using (var context = new NotificationEmployee(Library.Helper.CreateEntityConnectionString("ModelNotificationEmployee")))
             {
                int? companyID;
                string subject = string.Empty;
                string body = string.Empty;
                string toAddresses = string.Empty;

                foreach(Notification_Company_View company in context.Notification_Company_View.ToList())
                {
                    companyID = company.CompanyID;
                    List<Notification_EmployeeList_View> employeeList = context.Notification_EmployeeList_View.Where(o=>o.CompanyID == companyID).ToList();
                    List<Notification_UserReviceMail_View> userReviceMails = context.Notification_UserReviceMail_View.Where(o => o.CompanyID == companyID).ToList();

                    if (employeeList.Count() > 0 && userReviceMails.Count() > 0)
                    {
                        subject = "Note the term of the employee's contract";

                        body = "<table border='1' border-collapse='collapse'>";
                        body += "   <tr>";
                        body += "       <th>Employee Name</th>";
                        body += "       <th>Email</th>";
                        body += "       <th>Department</th>";
                        body += "       <th>Company</th>";
                        body += "       <th>Type Of Contract</th>";
                        body += "       <th>Contract expiry date</th>";
                        body += "   </tr>";

                        foreach(Notification_EmployeeList_View employee in employeeList)
                        {
                            body += "<tr>";
                            body += "   <td style='vertical-align: top;'>" + employee.EmployeeNM + "</td>";
                            body += "   <td style='vertical-align: top;'>" + employee.Email + "</td>";
                            body += "   <td style='vertical-align: top;'>" + employee.Department + "</td>";
                            body += "   <td style='vertical-align: top;'>" + employee.Company + "</td>";
                            body += "   <td style='vertical-align: top;'>" + employee.TypeOfContract + "</td>";
                            body += "   <td style='vertical-align: top;'>" + employee.ContractPeriod + "</td>";
                            body += "   </td>";
                            body += "</tr>";
                        }

                        body += "</table>";


                        foreach (Notification_UserReviceMail_View userReviceMail in userReviceMails.Where(o => !string.IsNullOrEmpty(o.Email)))
                        {
                            if (string.IsNullOrEmpty(toAddresses))
                            {
                                toAddresses = userReviceMail.Email;
                            }
                            else
                            {
                                toAddresses += ";" + userReviceMail.Email;
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
        }
    }
}
