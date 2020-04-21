using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private DevRequestMngEntities CreateContext()
        {
            return new DevRequestMngEntities(Library.Helper.CreateEntityConnectionString("DAL.DevRequestMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.DevRequestSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    int? DevRequestID = null;
                    int? DevRequestStatusID = null;
                    int? DevRequestProjectID = null;
                    int? DevRequestTypeID = null;
                    int? RequesterID = null;
                    int? AssignedPersonID = null;
                    int? Priority = null;
                    int? SpecialCriteria = null;
                    string Title = null;
                    if (filters.ContainsKey("DevRequestID") && filters["DevRequestID"] != null && !string.IsNullOrEmpty(filters["DevRequestID"].ToString()))
                    {
                        DevRequestID = Convert.ToInt32(filters["DevRequestID"].ToString());
                    }
                    if (filters.ContainsKey("DevRequestStatusID") && filters["DevRequestStatusID"] != null && !string.IsNullOrEmpty(filters["DevRequestStatusID"].ToString()))
                    {
                        DevRequestStatusID = Convert.ToInt32(filters["DevRequestStatusID"].ToString());
                    }
                    if (filters.ContainsKey("DevRequestProjectID") && filters["DevRequestProjectID"] != null && !string.IsNullOrEmpty(filters["DevRequestProjectID"].ToString()))
                    {
                        DevRequestProjectID = Convert.ToInt32(filters["DevRequestProjectID"].ToString());
                    }
                    if (filters.ContainsKey("DevRequestTypeID") && filters["DevRequestTypeID"] != null && !string.IsNullOrEmpty(filters["DevRequestTypeID"].ToString()))
                    {
                        DevRequestTypeID = Convert.ToInt32(filters["DevRequestTypeID"].ToString());
                    }
                    if (filters.ContainsKey("RequesterID") && filters["RequesterID"] != null && !string.IsNullOrEmpty(filters["RequesterID"].ToString()))
                    {
                        RequesterID = Convert.ToInt32(filters["RequesterID"].ToString());
                    }
                    if (filters.ContainsKey("AssignedPersonID") && filters["AssignedPersonID"] != null && !string.IsNullOrEmpty(filters["AssignedPersonID"].ToString()))
                    {
                        AssignedPersonID = Convert.ToInt32(filters["AssignedPersonID"].ToString());
                    }
                    if (filters.ContainsKey("Title") && !string.IsNullOrEmpty(filters["Title"].ToString()))
                    {
                        Title = filters["Title"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Priority") && filters["Priority"] != null && !string.IsNullOrEmpty(filters["Priority"].ToString()))
                    {
                        Priority = Convert.ToInt32(filters["Priority"].ToString());
                    }
                    if (filters.ContainsKey("SpecialCriteria") && filters["SpecialCriteria"] != null && !string.IsNullOrEmpty(filters["SpecialCriteria"].ToString()))
                    {
                        SpecialCriteria = Convert.ToInt32(filters["SpecialCriteria"].ToString());
                    }
                    totalRows = context.DevRequestMng_function_searchDevRequest(DevRequestID, DevRequestStatusID, DevRequestProjectID, DevRequestTypeID, RequesterID, Title, AssignedPersonID, Priority, SpecialCriteria, orderBy, orderDirection).Count();
                    var result = context.DevRequestMng_function_searchDevRequest(DevRequestID, DevRequestStatusID, DevRequestProjectID, DevRequestTypeID, RequesterID, Title, AssignedPersonID, Priority, SpecialCriteria, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_DevRequestSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    // add assignment
                    var assignments = context.DevRequestMng_function_searchDevRequestAssignment(DevRequestID, DevRequestStatusID, DevRequestProjectID, DevRequestTypeID, RequesterID, Title, AssignedPersonID, Priority).ToList();
                    foreach (DTO.DevRequestSearchResult dtoItem in data.Data)
                    {
                        dtoItem.DevRequestAssignments = new List<DTO.DevRequestAssignment>();
                        dtoItem.DevRequestAssignments.AddRange(converter.DB2DTO_DevRequestAssignmentList(assignments.Where(o => o.DevRequestID == dtoItem.DevRequestID).ToList()));
                    }
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.DevRequest();
            data.Data.DevRequestAssignments = new List<DTO.DevRequestAssignment>();
            data.Data.DevRequestHistories = new List<DTO.DevRequestHistory>();
            data.Data.DevRequestFiles = new List<DTO.DevRequestFile>();

            data.HistoryData = new DTO.DevRequestHistory();
            data.HistoryData.DevRequestCommentAttachedFiles = new List<DTO.DevRequestCommentAttachedFile>();

            data.DevRequestPriorities = new List<Support.DTO.DevRequestPriority>();
            data.DevRequestProjects = new List<Support.DTO.DevRequestProject>();
            data.DevRequestStatuses = new List<Support.DTO.DevRequestStatus>();
            data.DevRequestTypes = new List<Support.DTO.DevRequestType>();
            data.DevRequestPersons = new List<DTO.DevRequestPerson>();
            data.Requesters = new List<DTO.Requester>();

            //try to get data
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_DevRequest(context.DevRequestMng_DevRequest_View
                            .Include("DevRequestMng_DevRequestAssignment_View")
                            .Include("DevRequestMng_DevRequestHistory_View")
                            .Include("DevRequestMng_DevRequestHistory_View.DevRequestMng_DevRequestCommentAttachedFile_View")
                            .Include("DevRequestMng_DevRequestFile_View")
                            .FirstOrDefault(o => o.DevRequestID == id));
                    }

                    data.DevRequestPriorities = supportFactory.GetDevRequestPriority().ToList();
                    data.DevRequestProjects = supportFactory.GetDevRequestProject().ToList();
                    data.DevRequestTypes = supportFactory.GetDevRequestType().ToList();
                    data.DevRequestStatuses = supportFactory.GetDevRequestStatus().ToList();
                    data.DevRequestPersons = converter.DB2DTO_DevRequestPersonList(context.DevRequestMng_DevRequestPerson_View.ToList());
                    data.Requesters = converter.DB2DTO_RequesterList(context.DevRequestMng_Requester_View.ToList());
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
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.DevRequest dtoDevRequest = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.DevRequest>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequest dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new DevRequest();
                        context.DevRequest.Add(dbItem);
                        dbItem.DevRequestStatusID = 1; // PENDING
                    }
                    else 
                    {
                        notification.Message = "Dev request not found!";
                        return false;
                    }
                    converter.DTO2DB(dtoDevRequest, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                    // create history tracking
                    DevRequestHistory dbHistory = new DevRequestHistory();
                    dbHistory.UpdatedBy = userId;
                    dbHistory.UpdatedDate = DateTime.Now;
                    dbHistory.ActionDescription = "REQUEST CREATED";
                    dbHistory.DevRequestHistoryActionID = 1; // 1 = CREATE   
                    dbItem.DevRequestHistory.Add(dbHistory);

                    context.SaveChanges();

                    // send notify email to admin
                    string emailSubject = "TASK REQUEST: [CREATED] " + dbItem.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbItem.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbItem.Title + "<br/>";
                    emailBody += "REQUESTER: " +supportFactory.GetUsers().FirstOrDefault(o=>o.UserId == userId).FullName + "<br/>";
                    emailBody += "DESCRIPTION: " + dbItem.Description + "<br/>";
                    SendNotification(context, dbItem, emailSubject, emailBody);

                    dtoItem = GetData(dbItem.DevRequestID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.DevRequestProjects = new List<Support.DTO.DevRequestProject>();
            data.DevRequestStatuses = new List<Support.DTO.DevRequestStatus>();
            data.DevRequestTypes = new List<Support.DTO.DevRequestType>();
            data.Requesters = new List<DTO.Requester>();
            data.DevRequestPriorities = new List<Support.DTO.DevRequestPriority>();
            data.DevRequestPersons = new List<DTO.DevRequestPerson>();

            try
            {
                data.DevRequestProjects = supportFactory.GetDevRequestProject().ToList();
                data.DevRequestStatuses = supportFactory.GetDevRequestStatus().ToList();
                data.DevRequestTypes = supportFactory.GetDevRequestType().ToList();
                data.DevRequestPriorities = supportFactory.GetDevRequestPriority().ToList();
                using (DevRequestMngEntities context = CreateContext())
                {
                    data.DevRequestPersons = converter.DB2DTO_DevRequestPersonList(context.DevRequestMng_DevRequestPerson_View.ToList());
                    data.Requesters = converter.DB2DTO_RequesterList(context.DevRequestMng_Requester_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool AddComment(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.DevRequestHistory dtoDevRequestHistory = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.DevRequestHistory>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequestHistory dbItem = null;
                    DevRequest dbRequest = context.DevRequest.FirstOrDefault(o=>o.DevRequestID == id);
                    if (dbRequest != null)
                    {
                        dbItem = new DevRequestHistory();
                        dbRequest.DevRequestHistory.Add(dbItem);                        
                    }
                    else
                    {
                        notification.Message = "Dev request not found!";
                        return false;
                    }
                    dbItem.Comment = dtoDevRequestHistory.Comment;
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.ActionDescription = "ADD COMMENT";
                    dbItem.DevRequestHistoryActionID = 3; // 3 = ADD COMMENT

                    // request file
                    string TmpFile = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                    foreach (DTO.DevRequestCommentAttachedFile dtoFile in dtoDevRequestHistory.DevRequestCommentAttachedFiles.Where(o => o.HasChanged == true))
                    {
                        // add file
                        DevRequestCommentAttachedFile dbFile = new DevRequestCommentAttachedFile();
                        dbItem.DevRequestCommentAttachedFile.Add(dbFile);
                        AutoMapper.Mapper.Map<DTO.DevRequestCommentAttachedFile, DevRequestCommentAttachedFile>(dtoFile, dbFile);
                        dbFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.NewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                    }

                    // send notify email
                    string emailSubject = "TASK REQUEST: [COMMENT] " + dbRequest.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbRequest.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbRequest.Title + "<br/>";
                    emailBody += "UPDATOR: " + supportFactory.GetUsers().FirstOrDefault(o => o.UserId == userId).FullName + "<br/>";
                    emailBody += "COMMENT: " + dbItem.Comment + "<br/>";
                    SendNotification(context, dbRequest, emailSubject, emailBody);

                    context.SaveChanges();
                    dtoItem = GetData(id, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool Assign(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.AssignData dtoAssign = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AssignData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequestHistory dbHistory = null;
                    DevRequestAssignment dbAssign = null;
                    DevRequest dbRequest = context.DevRequest.FirstOrDefault(o => o.DevRequestID == id);
                    if (dbRequest != null)
                    {
                        dbHistory = new DevRequestHistory();
                        dbAssign = new DevRequestAssignment();
                        dbRequest.DevRequestHistory.Add(dbHistory);
                        dbRequest.DevRequestAssignment.Add(dbAssign);
                    }
                    else
                    {
                        notification.Message = "Dev request not found!";
                        return false;
                    }

                    if (dbRequest.DevRequestAssignment.FirstOrDefault(o => o.AssignedTo == dtoAssign.AssignedTo) != null) // person is already added
                    {
                        notification.Message = "Person is already in the list!";
                        return false;
                    }

                    Support.DTO.User dtoUser = supportFactory.GetUsers().FirstOrDefault(o => o.UserId == dtoAssign.AssignedTo);
                    if (dtoUser == null) // check if user is exists in the system
                    {
                        notification.Message = "Person not found!";
                        return false;
                    }

                    dbHistory.Comment = dtoAssign.Comment;
                    dbHistory.UpdatedBy = userId;
                    dbHistory.UpdatedDate = DateTime.Now;
                    dbHistory.DevRequestHistoryActionID = 2; // 2 : assign
                    dbHistory.ActionDescription = "ASSGIN TASK TO " + dtoUser.FullName + " ";
                    if (dtoAssign.IsPersonInCharge)
                    {
                        dbHistory.ActionDescription += "(PIC)";
                    }

                    dbAssign.AssignedTo = dtoAssign.AssignedTo;
                    if (dtoAssign.IsPersonInCharge)
                    {
                        dbRequest.DevRequestAssignment.ToList().ForEach(o => o.IsPersonInCharge = false);
                    }
                    dbAssign.IsPersonInCharge = dtoAssign.IsPersonInCharge;

                    // send notify email
                    string emailSubject = "TASK REQUEST: [ASSIGN] " + dbRequest.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbRequest.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbRequest.Title + "<br/>";
                    emailBody += "ASSIGNED BY: " + supportFactory.GetUsers().FirstOrDefault(o => o.UserId == userId).FullName + "<br/>";
                    emailBody += dbHistory.ActionDescription;
                    SendNotification(context, dbRequest, emailSubject, emailBody);

                    context.SaveChanges();
                    dtoItem = GetData(id, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool UnAssign(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.AssignData dtoAssign = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AssignData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequestHistory dbHistory = null;
                    DevRequest dbRequest = context.DevRequest.FirstOrDefault(o => o.DevRequestID == id);
                    if (dbRequest != null)
                    {
                        dbHistory = new DevRequestHistory();
                        dbRequest.DevRequestHistory.Add(dbHistory);
                    }
                    else
                    {
                        throw new Exception("Dev request not found!");
                    }

                    DevRequestAssignment dbAssignment = dbRequest.DevRequestAssignment.FirstOrDefault(o => o.AssignedTo == dtoAssign.AssignedTo);
                    if (dbAssignment != null) // person is already added
                    {
                        dbRequest.DevRequestAssignment.Remove(dbAssignment);
                        context.DevRequestAssignment.Remove(dbAssignment);
                    }
                    else
                    {
                        throw new Exception("Person is not assigned!");
                    }

                    Support.DTO.User dtoUser = supportFactory.GetUsers().FirstOrDefault(o => o.UserId == dtoAssign.AssignedTo);
                    if (dtoUser == null) // check if user is exists in the system
                    {
                        throw new Exception("Person not found!");
                    }

                    dbHistory.Comment = dtoAssign.Comment;
                    dbHistory.UpdatedBy = userId;
                    dbHistory.UpdatedDate = DateTime.Now;
                    dbHistory.DevRequestHistoryActionID = 7; // 7 : un-assign
                    dbHistory.ActionDescription = "REMOVE PERSON: " + dtoUser.FullName + " FROM TASK ASSIGNMENT";

                    // send notify email
                    string emailSubject = "TASK REQUEST: [UNASSIGN] " + dbRequest.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbRequest.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbRequest.Title + "<br/>";
                    emailBody += "UPDATED BY: " + supportFactory.GetUsers().FirstOrDefault(o => o.UserId == userId).FullName + "<br/>";
                    emailBody += dbHistory.ActionDescription;
                    SendNotification(context, dbRequest, emailSubject, emailBody);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool ChangePriority(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.PriorityData dtoPriority = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PriorityData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequestHistory dbHistory = null;
                    DevRequest dbRequest = context.DevRequest.FirstOrDefault(o => o.DevRequestID == id);
                    if (dbRequest != null)
                    {
                        dbHistory = new DevRequestHistory();
                        dbRequest.DevRequestHistory.Add(dbHistory);
                    }
                    else
                    {
                        notification.Message = "Dev request not found!";
                        return false;
                    }

                    Support.DTO.DevRequestPriority dtoSupportPriority = supportFactory.GetDevRequestPriority().FirstOrDefault(o => o.DevRequestPriorityID == dtoPriority.Priority);
                    if (dtoSupportPriority == null)
                    {
                        notification.Message = "Invalid priority!";
                        return false;
                    }
                    dbRequest.Priority = dtoPriority.Priority;
                    dbHistory.Comment = dtoPriority.Comment;
                    dbHistory.UpdatedBy = userId;
                    dbHistory.UpdatedDate = DateTime.Now;
                    dbHistory.DevRequestHistoryActionID = 5; // 5 : change priority
                    dbHistory.ActionDescription = "SET PRIORITY TO " + dtoSupportPriority.DevRequestPriorityNM;

                    // send notify email
                    string emailSubject = "TASK REQUEST: [PRIORITY] " + dbRequest.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbRequest.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbRequest.Title + "<br/>";
                    emailBody += "PRIORITY: " + dbHistory.ActionDescription;
                    emailBody += "UPDATOR: " + supportFactory.GetUsers().FirstOrDefault(o => o.UserId == userId).FullName + "<br/>";
                    emailBody += "COMMENT: " + dbHistory.Comment + "<br/>";
                    SendNotification(context, dbRequest, emailSubject, emailBody);

                    context.SaveChanges();
                    dtoItem = GetData(id, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool ChangeStatus(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.StatusData dtoStatus = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.StatusData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequestHistory dbHistory = null;
                    DevRequest dbRequest = context.DevRequest.FirstOrDefault(o => o.DevRequestID == id);
                    if (dbRequest != null)
                    {
                        dbHistory = new DevRequestHistory();
                        dbRequest.DevRequestHistory.Add(dbHistory);
                    }
                    else
                    {
                        notification.Message = "Dev request not found!";
                        return false;
                    }

                    // in case status = resolved (4), check if actual hour is exist
                    if (dtoStatus.DevRequestStatusID == 4)
                    {
                        foreach (DevRequestAssignment dbAssignment in dbRequest.DevRequestAssignment)
                        {
                            if (!dbAssignment.ActualWorkingHour.HasValue)
                            {
                                throw new Exception("Please fill in the actual working hour before mark request as resolved!");
                            }
                        }
                    }

                    Support.DTO.DevRequestStatus dtoSupportStatus = supportFactory.GetDevRequestStatus().FirstOrDefault(o => o.DevRequestStatusID == dtoStatus.DevRequestStatusID);
                    if (dtoSupportStatus == null)
                    {
                        notification.Message = "Invalid status!";
                        return false;
                    }
                    dbRequest.DevRequestStatusID = dtoStatus.DevRequestStatusID;
                    dbHistory.Comment = dtoStatus.Comment;
                    dbHistory.UpdatedBy = userId;
                    dbHistory.UpdatedDate = DateTime.Now;
                    dbHistory.DevRequestHistoryActionID = 4; // 4: change status
                    dbHistory.ActionDescription = "SET STATUS TO " + dtoSupportStatus.DevRequestStatusNM;

                    if (dtoStatus.DevRequestStatusID == 7) // progress
                    {
                        dbRequest.StartDate = DateTime.Now;
                    }

                    // send notify email
                    string emailSubject = "TASK REQUEST: [STATUS] " + dbRequest.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbRequest.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbRequest.Title + "<br/>";
                    emailBody += "UPDATOR: " + supportFactory.GetUsers().FirstOrDefault(o => o.UserId == userId).FullName + "<br/>";
                    emailBody += dbHistory.ActionDescription;
                    SendNotification(context, dbRequest, emailSubject, emailBody);

                    context.SaveChanges();
                    dtoItem = GetData(id, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool ChangeEstEndDate(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.EstEndDateData dtoEstEndDate = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.EstEndDateData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequestHistory dbHistory = null;
                    DevRequest dbRequest = context.DevRequest.FirstOrDefault(o => o.DevRequestID == id);
                    if (dbRequest != null)
                    {
                        dbHistory = new DevRequestHistory();
                        dbRequest.DevRequestHistory.Add(dbHistory);
                    }
                    else
                    {
                        notification.Message = "Dev request not found!";
                        return false;
                    }

                    DateTime tmpDate;
                    System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
                    if (!string.IsNullOrEmpty(dtoEstEndDate.EstEndDate))
                    {
                        if (DateTime.TryParse(dtoEstEndDate.EstEndDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbRequest.EstEndDate = tmpDate;
                        }
                        else
                        {
                            notification.Message = "Invalid date format!";
                            return false;
                        }
                    }
                    
                    dbHistory.Comment = dtoEstEndDate.Comment;
                    dbHistory.UpdatedBy = userId;
                    dbHistory.UpdatedDate = DateTime.Now;
                    dbHistory.DevRequestHistoryActionID = 6;  // 6 : set est end date
                    dbHistory.ActionDescription = "SET EST END DATE TO " + dtoEstEndDate.EstEndDate;

                    // send notify email
                    string emailSubject = "TASK REQUEST: [SET EST END DATE] " + dbRequest.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbRequest.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbRequest.Title + "<br/>";
                    emailBody += "UPDATOR: " + supportFactory.GetUsers().FirstOrDefault(o => o.UserId == userId).FullName + "<br/>";
                    emailBody += dbHistory.ActionDescription;
                    SendNotification(context, dbRequest, emailSubject, emailBody);

                    context.SaveChanges();
                    dtoItem = GetData(id, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool UpdateEstHourSpend(int userId, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.DevRequestAssignment> dtoAssignments = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.DevRequestAssignment>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DevRequestMngEntities context = CreateContext())
                {
                    DevRequestHistory dbHistory = null;
                    DevRequest dbRequest = context.DevRequest.FirstOrDefault(o => o.DevRequestID == id);
                    if (dbRequest != null)
                    {
                        dbHistory = new DevRequestHistory();
                        dbRequest.DevRequestHistory.Add(dbHistory);
                    }
                    else
                    {
                        notification.Message = "Dev request not found!";
                        return false;
                    }

                    if (dbRequest == null)
                    {
                        throw new Exception("Request not found!");
                    }

                    foreach (DTO.DevRequestAssignment dtoAssignment in dtoAssignments)
                    {
                        DevRequestAssignment dbAssignment = dbRequest.DevRequestAssignment.FirstOrDefault(o => o.DevRequestAssignmentID == dtoAssignment.DevRequestAssignmentID);
                        if (dbAssignment != null)
                        {
                            dbAssignment.EstWorkingHour = dtoAssignment.EstWorkingHour;
                            dbAssignment.ActualWorkingHour = dtoAssignment.ActualWorkingHour;
                        }
                    }

                    dbHistory.Comment = string.Empty;
                    dbHistory.UpdatedBy = userId;
                    dbHistory.UpdatedDate = DateTime.Now;
                    dbHistory.DevRequestHistoryActionID = 6;  // 6 : set est end date
                    dbHistory.ActionDescription = "SET HOUR SPEND (EST/ACTUAL)";

                    // send notify email
                    string emailSubject = "TASK REQUEST: [SET HOUR SPEND] " + dbRequest.Title;
                    string emailBody = "";
                    emailBody += "ID: " + dbRequest.DevRequestID.ToString() + "<br/>";
                    emailBody += "TITLE: " + dbRequest.Title + "<br/>";
                    emailBody += "UPDATOR: " + supportFactory.GetUsers().FirstOrDefault(o => o.UserId == userId).FullName + "<br/>";
                    emailBody += dbHistory.ActionDescription;
                    SendNotification(context, dbRequest, emailSubject, emailBody);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }


        private void SendNotification(DevRequestMngEntities context, DevRequest dbRequest, string emailSubject, string emailBody)
        {
            try
            {
                List<DevRequestMng_DevRequestPersonDetail_View> persons = context.DevRequestMng_DevRequestPersonDetail_View.ToList();
                List<string> emailAddress = new List<string>();

                // send to admin
                foreach (DevRequestMng_DevRequestPersonDetail_View dtoPerson in persons)
                {
                    if (dtoPerson.RoleID.HasValue && dtoPerson.RoleID.Value == 2) // 2 is admin
                    {
                        if (!emailAddress.Contains(dtoPerson.Email))
                        {
                            emailAddress.Add(dtoPerson.Email);
                        }
                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = dtoPerson.UserID;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                        notification.NotificationMessageTitle = emailSubject;
                        notification.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification);
                    }
                }

                // send to requester
                DevRequestMng_DevRequestPersonDetail_View dtoRequester = persons.FirstOrDefault(o => o.UserID == dbRequest.RequesterID);
                if (dtoRequester != null && !string.IsNullOrEmpty(dtoRequester.Email))
                {
                    if (!emailAddress.Contains(dtoRequester.Email))
                    {
                        emailAddress.Add(dtoRequester.Email);
                    }
                    // add to NotificationMessage table
                    NotificationMessage notification = new NotificationMessage();
                    notification.UserID = dtoRequester.UserID;
                    notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                    notification.NotificationMessageTitle = emailSubject;
                    notification.NotificationMessageContent = emailBody;
                    context.NotificationMessage.Add(notification);
                }

                // send to assigned person
                foreach (DevRequestAssignment dtoAssignment in dbRequest.DevRequestAssignment)
                {
                    dtoRequester = persons.FirstOrDefault(o => o.UserID == dtoAssignment.AssignedTo);
                    if (dtoRequester != null && !string.IsNullOrEmpty(dtoRequester.Email))
                    {
                        if (!emailAddress.Contains(dtoRequester.Email))
                        {
                            emailAddress.Add(dtoRequester.Email);
                        }
                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = dtoRequester.UserID;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                        notification.NotificationMessageTitle = emailSubject;
                        notification.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification);
                    }
                }

                string sendToEmail = string.Empty;
                foreach (string eAddress in emailAddress)
                {
                    if (string.IsNullOrEmpty(sendToEmail))
                    {
                        sendToEmail += eAddress;
                    }
                    else
                    {
                        sendToEmail += ";" + eAddress;
                    }
                }
                EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                dbEmail.EmailBody = emailBody;
                dbEmail.EmailSubject = emailSubject;
                dbEmail.SendTo = sendToEmail;
                context.EmailNotificationMessage.Add(dbEmail);

                context.SaveChanges();
            }
            catch { }            
        }
    }
}
