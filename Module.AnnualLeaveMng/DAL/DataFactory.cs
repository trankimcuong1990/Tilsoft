using Library;
using Library.DTO;
using Module.AnnualLeaveMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        //private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory(); 
        Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private AnnualLeaveMngEntities CreateContext()
        {
            return new AnnualLeaveMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AnnualLeaveMngModel"));
        }

        public DTO.SearchFormData GetDataWithFilter(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<AnnualLeaveSearchResultDTO> ();           

            totalRows = 0;

            string EmployeeNM = null;
            int? StatusID = null;
            string CompanyNM = null;
            string FromDate = null;
            string ToDate = null;
            bool? HasLeft = false;

            if (filters.ContainsKey("employeeNM") && filters["employeeNM"] != null && !string.IsNullOrEmpty(filters["employeeNM"].ToString()))
            {
                EmployeeNM = filters["employeeNM"].ToString();
            }
            if (filters.ContainsKey("statusID") && filters["statusID"] != null && !string.IsNullOrEmpty(filters["statusID"].ToString()))
            {
                StatusID = Convert.ToInt32(filters["statusID"].ToString());
            }
            if (filters.ContainsKey("companyNM") && filters["companyNM"] != null && !string.IsNullOrEmpty(filters["companyNM"].ToString()))
            {
                CompanyNM = filters["companyNM"].ToString();
            }
            if (filters.ContainsKey("fromDate") && filters["fromDate"] != null && !string.IsNullOrEmpty(filters["fromDate"].ToString()))
            {
                FromDate = filters["fromDate"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("toDate") && filters["toDate"] != null && !string.IsNullOrEmpty(filters["toDate"].ToString()))
            {
                ToDate = filters["toDate"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("HasLeft") && filters["HasLeft"] != null && !string.IsNullOrEmpty(filters["HasLeft"].ToString()))
            {
                HasLeft = (filters["HasLeft"].ToString() == "true") ? true : false;
            }

            DateTime? _fromDate = FromDate.ConvertStringToDateTime();
            DateTime? _toDate = ToDate.ConvertStringToDateTime();
            //try to get data
            try
            {
                using (AnnualLeaveMngEntities context = CreateContext())
                {
                    int? companyID = frameworkFactory.GetCompanyID(userID);
                    var result = context.AnnualLeaveMng_function_SearchAnnualLeave(EmployeeNM, StatusID, CompanyNM, _fromDate, _toDate, HasLeft, orderBy, orderDirection).Where(o=>o.CompanyID == companyID).ToList();
                    totalRows = result.Count();

                    data.Data = converter.DB2DTO_AnnualLeaveSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                   
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.AnnualLeaveTrackingDTO = new AnnualLeaveTrackingDTO();
            data.EmployeeDTOs = new List<EmployeeDTO>();
            data.AnnualLeaveTypeDTOs = new List<AnnualLeaveTypeDTO>();
            data.AnnualLeaveStatusDTOs = new List<AnnualLeaveStatusDTO>();
            
            //try to get data
            try
            {
                using (AnnualLeaveMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.AnnualLeaveTrackingDTO = converter.DB2DTO_AnnualLeaveTrackingDTO(context.AnnualLeaveMng_AnnualLeaveTracking_View
                            .Include("AnnualLeaveMng_AnnualLeaveTrackingDetail_View")
                            .FirstOrDefault(o => o.AnnualLeaveTrackingID == id));
                    }
                   
                    int? companyId = frameworkFactory.GetCompanyID(userId);
                    
                    data.EmployeeDTOs = converter.DB2DTO_EmployeeDTO(context.AnnualLeaveMng_Employee_View.Where(s=>s.CompanyID == companyId.Value).ToList());
                    data.AnnualLeaveTypeDTOs = converter.DB2DTO_AnnualLeaveTypeDTO(context.AnnualLeaveMng_AnnualLeaveType_View.ToList());
                    data.AnnualLeaveStatusDTOs = converter.DB2DTO_AnnualLeaveStatusDTO(context.AnnualLeaveMng_AnnualLeaveStatus_View.ToList());

                    if(id == 0)
                    {
                        data.AnnualLeaveTrackingDTO.StatusUpdatedBy = userId;
                        data.AnnualLeaveTrackingDTO.StatusUpdatedEmployeeIDBy = data.EmployeeDTOs.Where(s => s.UserID == userId).FirstOrDefault().EmployeeID;
                    }
                    else
                    {
                        data.AnnualLeaveTrackingDTO.StatusUpdatedEmployeeIDBy = data.EmployeeDTOs.Where(s => s.UserID == data.AnnualLeaveTrackingDTO.StatusUpdatedBy).FirstOrDefault().EmployeeID;
                    }

                    data.AnnualLeaveTrackingDTO.EmployeeLoginID = data.EmployeeDTOs.Where(s => s.UserID == userId).FirstOrDefault().EmployeeID;
                    //bind time range
                    var compnayInfo = context.AnnualLeaveMng_Company_View.Where(s => s.CompanyID == companyId.Value).FirstOrDefault();
                    data.FromTimeRange = new List<string>();
                    data.FromTimeRange.Add(compnayInfo.MorningShiftFrom);
                    data.FromTimeRange.Add(compnayInfo.AfternoonShiftFrom);
                    data.ToTimeRange = new List<string>();
                    data.ToTimeRange.Add(compnayInfo.MorningShiftTo);
                    data.ToTimeRange.Add(compnayInfo.AfternoonShiftTo);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {

            AnnualLeaveTrackingDTO annualLeaveTrackingDTO = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<AnnualLeaveTrackingDTO>();

            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    AnnualLeaveTracking dbItem;

                    if (id == 0)
                    {
                        dbItem = new AnnualLeaveTracking();
                        context.AnnualLeaveTrackings.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.AnnualLeaveTrackings.FirstOrDefault(o => o.AnnualLeaveTrackingID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return false;
                        }
                        else {
                            int? statusID = dbItem.StatusID;
                            if (statusID.HasValue && statusID == 3)
                            {
                                throw new Exception("Request has approved. You can not changed");
                            }
                        }
                    }
                    converter.DTO2DB_AnnualLeaveTracking(userId, annualLeaveTrackingDTO, ref dbItem);

                    context.AnnualLeaveTrackingDetail.Local.Where(o => o.AnnualLeaveTracking == null).ToList().ForEach(o => context.AnnualLeaveTrackingDetail.Remove(o));

                    context.SaveChanges();

                    //send notify
                    string emailBody = string.Empty;
                    string emailSubject = string.Empty;
                    string sendToEmail = string.Empty;

                    AnnualLeaveMng_Email_View info = context.AnnualLeaveMng_Email_View.Where(s => s.AnnualLeaveTrackingID == dbItem.AnnualLeaveTrackingID).FirstOrDefault();
                    if(dbItem.StatusID == 1 && !string.IsNullOrEmpty(info.EmailApprover)) // pending
                    {
                        sendToEmail = info.EmailApprover;
                        emailSubject = "Please check and approve the leave request from: " + info.RequestNM;
                        emailBody = "<p>" +
                                    "Dear Sir / Madam," +
                                    "<br/>" +
                                    "Could you please check and approve my leave request as below:" +
                                    "<br/>" +
                                    "       Request for: " + info.RequestNM +
                                    "<br/>" +
                                    "       Reason: " + info.Remark +
                                    "<br/>" +
                                    "       Requested Day Off: " + info.RequestedTimeOff +
                                    "<br/>" +
                                    "       From Day Off: " + info.FromDate + " To Date: " + info.ToDate +
                                    "<br/>" +
                                    "Could you please click here to approve: http://app.tilsoft.bg/AnnualLeaveMng/Edit/" + info.AnnualLeaveTrackingID +
                                    "<br/>" +
                                    "Have a nice day!" +
                                    "<br/>" +
                                    "This is an automated message, please do not reply!" +
                                    "</p>";

                        EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                        dbEmail.EmailBody = emailBody;
                        dbEmail.EmailSubject = emailSubject;
                        dbEmail.SendTo = sendToEmail;
                        context.EmailNotificationMessage.Add(dbEmail);

                        NotificationMessage notificationMessage = new NotificationMessage();
                        notificationMessage.UserID = info.ApproverUserID;
                        notificationMessage.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notificationMessage.NotificationMessageTitle = emailSubject;
                        notificationMessage.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notificationMessage);

                        context.SaveChanges();
                    }
                    else if(dbItem.StatusID == 3 && !string.IsNullOrEmpty(info.EmailRequest)) //approve
                    {
                        dbItem.ApproveBy = userId;
                        sendToEmail = info.EmailRequest;
                        emailSubject = "Your request has been Approved";
                        emailBody = "<p>" +
                                    "Dear," +
                                    "<br/>" +
                                    "Your leave request: http://app.tilsoft.bg/AnnualLeaveMng/Edit/" + info.AnnualLeaveTrackingID + " has been approved by manager, please check" +                                                                  
                                    "<br/>" +                                
                                    "Have a nice day!" +
                                    "<br/>" +
                                    "This is an automated message, please do not reply!" +
                                    "</p>";

                        EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                        dbEmail.EmailBody = emailBody;
                        dbEmail.EmailSubject = emailSubject;
                        dbEmail.SendTo = sendToEmail;
                        context.EmailNotificationMessage.Add(dbEmail);

                        NotificationMessage notificationMessage = new NotificationMessage();
                        notificationMessage.UserID = info.RequestUserID;
                        notificationMessage.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notificationMessage.NotificationMessageTitle = emailSubject;
                        notificationMessage.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notificationMessage);

                        context.SaveChanges();
                    }
                    else if (dbItem.StatusID == 4 && !string.IsNullOrEmpty(info.EmailRequest)) //reject
                    {
                        sendToEmail = info.EmailRequest;
                        emailSubject = "Your request has been Rejected:";
                        emailBody = "<p>" +
                                    "Dear," +
                                    "<br/>" +
                                    "Your leave request: http://app.tilsoft.bg/AnnualLeaveMng/Edit/" + info.AnnualLeaveTrackingID + " has been rejected: by manager, please check" +
                                    "<br/>" +
                                    "Have a nice day!" +
                                    "<br/>" +
                                    "This is an automated message, please do not reply!" +
                                    "</p>";

                        EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                        dbEmail.EmailBody = emailBody;
                        dbEmail.EmailSubject = emailSubject;
                        dbEmail.SendTo = sendToEmail;
                        context.EmailNotificationMessage.Add(dbEmail);

                        NotificationMessage notificationMessage = new NotificationMessage();
                        notificationMessage.UserID = info.RequestUserID;
                        notificationMessage.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notificationMessage.NotificationMessageTitle = emailSubject;
                        notificationMessage.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notificationMessage);

                        context.SaveChanges();
                    }

                    dtoItem = GetData(userId, dbItem.AnnualLeaveTrackingID, out notification);
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (var context = CreateContext())
            //    {
            //        AnnualLeaveTracking dbItem = context.AnnualLeaveTrackings.FirstOrDefault(o => o.AnnualLeaveTrackingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Annual Leave not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.StatusUpdatedBy = userId;                       
            //            dbItem.StatusUpdatedDate = DateTime.Now;
            //            dbItem.StatusID = 3; // Appoved
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }
      
        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //
        public SupportFormData GetInitData(int userID, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            SupportFormData data = new SupportFormData();
            data.EmployeeDTOs = new List<EmployeeDTO>();
            data.CompanyDTOs = new List<CompanyDTO>();
            data.AnnualLeaveStatusDTOs = new List<AnnualLeaveStatusDTO>();
            try
            {              
                using (AnnualLeaveMngEntities context = CreateContext())
                {
                    data.EmployeeDTOs = converter.DB2DTO_EmployeeDTO(context.AnnualLeaveMng_Employee_View.ToList());
                    data.AnnualLeaveStatusDTOs = converter.DB2DTO_AnnualLeaveStatusDTO(context.AnnualLeaveMng_AnnualLeaveStatus_View.ToList());
                    data.CompanyDTOs = converter.DB2DTO_CompanyDTO(context.AnnualLeaveMng_Company_View.ToList());
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return data;
            }
        }

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //DTO.SupportFormData data = new DTO.SupportFormData();
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return data;
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
