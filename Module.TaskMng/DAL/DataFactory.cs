using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private TaskMngEntities CreateContext()
        {
            return new TaskMngEntities(Library.Helper.CreateEntityConnectionString("DAL.TaskMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.TaskSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (TaskMngEntities context = CreateContext())
                {
                    string TaskUD = null;
                    string TaskNM = null;
                    int? TaskStatusID = null;
                    int? TaskUserID = null;
                    int UserID = -1;
                    if (filters.ContainsKey("TaskUD") && !string.IsNullOrEmpty(filters["TaskUD"].ToString()))
                    {
                        TaskUD = filters["TaskUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("TaskNM") && !string.IsNullOrEmpty(filters["TaskNM"].ToString()))
                    {
                        TaskNM = filters["TaskNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("TaskStatusID") && filters["TaskStatusID"] != null && !string.IsNullOrEmpty(filters["TaskStatusID"].ToString()))
                    {
                        TaskStatusID = Convert.ToInt32(filters["TaskStatusID"].ToString());
                    }
                    if (filters.ContainsKey("TaskUserID") && filters["TaskUserID"] != null && !string.IsNullOrEmpty(filters["TaskUserID"].ToString()))
                    {
                        TaskUserID = Convert.ToInt32(filters["TaskUserID"].ToString());
                    }
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    totalRows = context.TaskMng_function_SearchTask(TaskUD, TaskNM, TaskStatusID, TaskUserID, UserID, orderBy, orderDirection).Count();
                    var result = context.TaskMng_function_SearchTask(TaskUD, TaskNM, TaskStatusID, TaskUserID, UserID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_TaskSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.Task();
            data.Data.TaskFiles = new List<DTO.TaskFile>();
            data.Data.TaskSteps = new List<DTO.TaskStep>();
            data.TaskStatuses = new List<Support.DTO.TaskStatus>();
            data.Users = new List<Support.DTO.User>();
            data.TaskRoles = new List<Support.DTO.TaskRole>();

            //try to get data
            try
            {
                // check permission

                ////

                if (id > 0)
                {
                    using (TaskMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_Task(context.TaskMng_Task_View
                            .Include("TaskMng_TaskFile_View")
                            .Include("TaskMng_TaskStep_View")
                            .Include("TaskMng_TaskStep_View.TaskMng_TaskStepUser_View")
                            .FirstOrDefault(o => o.TaskID == id));
                    }
                }               
                data.TaskStatuses = supportFactory.GetTaskStatus();
                data.Users = supportFactory.GetUsers();
                data.TaskRoles = supportFactory.GetTaskRole();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.ViewFormData GetViewData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ViewFormData data = new DTO.ViewFormData();
            data.Data = new DTO.Task();
            data.Data.TaskFiles = new List<DTO.TaskFile>();
            data.Data.TaskSteps = new List<DTO.TaskStep>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (TaskMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_Task(context.TaskMng_Task_View
                            .Include("TaskMng_TaskFile_View")
                            .Include("TaskMng_TaskStep_View")
                            .Include("TaskMng_TaskStep_View.TaskMng_TaskStepUser_View")
                            .FirstOrDefault(o => o.TaskID == id));
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

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                ////

                using (TaskMngEntities context = CreateContext())
                {
                    Task dbItem = context.Task.FirstOrDefault(o => o.TaskID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Task not found!");
                    }

                    // check if ok to delete
                    ////

                    // everything ok, delete the task
                    foreach (TaskFile dbFile in dbItem.TaskFile)
                    {
                        if (dbFile.FileUD != string.Empty) fwFactory.RemoveFile(dbFile.FileUD);
                    }
                    context.Task.Remove(dbItem);
                    SendNotification(context);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Task dtoTask = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Task>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                ////

                using (TaskMngEntities context = CreateContext())
                {
                    Task dbItem = null;
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM Task WITH (TABLOCKX, HOLDLOCK)");

                        try
                        {
                            if (id <= 0)
                            {
                                dbItem = new Task();
                                context.Task.Add(dbItem);
                            }
                            else
                            {
                                dbItem = context.Task.FirstOrDefault(o => o.TaskID == id);
                            }

                            if (dbItem == null)
                            {
                                notification.Message = "Task not found!";
                                return false;
                            }
                            else
                            {
                                // check concurrency
                                if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoTask.ConcurrencyFlag)))
                                {
                                    throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                                }
                                
                                converter.DTO2DB(dtoTask, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                                dbItem.UpdatedBy = userId;
                                dbItem.UpdatedDate = DateTime.Now;

                                // remove orphan
                                context.TaskFile.Local.Where(o => o.Task == null).ToList().ForEach(o => context.TaskFile.Remove(o));
                                context.TaskStep.Local.Where(o => o.Task == null).ToList().ForEach(o => context.TaskStep.Remove(o));
                                context.TaskStepUser.Local.Where(o => o.TaskStep == null).ToList().ForEach(o => context.TaskStepUser.Remove(o));

                                // notification
                                if (id <= 0)
                                {
                                    // new task
                                    string emailTitle, emailBody, sendTo;
                                    emailTitle = "Task: [" + dbItem.TaskNM + "] has been added";
                                    emailBody = "Task: [" + dbItem.TaskNM + "] has been added";
                                    sendTo = "";
                                    foreach (TaskStep step in dbItem.TaskStep)
                                    {
                                        foreach (TaskStepUser stepUser in step.TaskStepUser)
                                        {
                                            int _userId = stepUser.UserID.Value;
                                            AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == _userId);
                                            if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                            {
                                                sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                                
                                            }
                                            // add to NotificationMessage table
                                            NotificationMessage notification1 = new NotificationMessage();
                                            notification1.UserID = profile.UserId;
                                            notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                            notification1.NotificationMessageTitle = emailTitle;
                                            notification1.NotificationMessageContent = emailBody;
                                            context.NotificationMessage.Add(notification1);
                                        }
                                    }
                                    EmailNotificationMessage message = new EmailNotificationMessage();
                                    message.EmailBody = emailBody;
                                    message.EmailSubject = emailTitle;
                                    message.SendTo = sendTo;
                                    context.EmailNotificationMessage.Add(message);
                                }
                                else
                                {
                                    // update current task
                                    SendNotification(context);                                    
                                }                                
                                context.SaveChanges();

                                if (id <= 0)
                                {
                                    dbItem.TaskUD = Library.Common.Helper.formatIndex(dbItem.TaskID.ToString(), 5, "0");
                                    context.SaveChanges();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }
                    }

                    CalcPercent(dbItem.TaskID);
                    dtoItem = GetData(dbItem.TaskID, out notification).Data;
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
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.TaskStatuses = new List<Support.DTO.TaskStatus>();
            data.Users = new List<Support.DTO.User>();

            try
            {
                data.TaskStatuses = supportFactory.GetTaskStatus();
                data.Users = supportFactory.GetUsers();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.DetailFormData GetDetailData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DetailFormData data = new DTO.DetailFormData();
            data.Data = new DTO.TaskStep();
            data.Data.TaskStepUsers = new List<DTO.TaskStepUser>();
            data.TaskStatuses = new List<Support.DTO.TaskStatus>();
            data.TaskStepComments = new List<DTO.TaskStepComment>();

            //try to get data
            try
            {
                // check permission

                ////

                if (id > 0)
                {
                    using (TaskMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_TaskStep(context.TaskMng_TaskStep_View
                            .Include("TaskMng_TaskStepUser_View")
                            .FirstOrDefault(o => o.TaskStepID == id));

                        data.TaskStepComments = converter.DB2DTO_TaskStepCommentList(context.TaskMng_TaskStepComment_View
                            .Include("TaskMng_TaskStepCommentFile_View")
                            .Where(o => o.TaskStepID == id).ToList());
                    }
                }
                data.TaskStatuses = supportFactory.GetTaskStatus();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool UpdateStepStatus(int userId, int id, int statusId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                int? taskID = -1;
                using (TaskMngEntities context = CreateContext())
                {
                    TaskStep dbItem = context.TaskStep.FirstOrDefault(o => o.TaskStepID == id);
                    taskID = dbItem.TaskID;
                    if (dbItem == null)
                    {
                        throw new Exception("Task step not found!");
                    }
                    else
                    {
                        if (!dbItem.StepStatusID.HasValue || (dbItem.StepStatusID.HasValue && dbItem.StepStatusID.Value != statusId))
                        {
                            dbItem.StepStatusID = statusId;
                            SendNotification(context);
                            context.SaveChanges();
                        }
                    }
                }

                CalcPercent(taskID.Value);
                return true;
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool UpdateComment(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.TaskStepComment dtoTaskStepComment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.TaskStepComment>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                ////

                using (TaskMngEntities context = CreateContext())
                {
                    TaskStepComment dbItem = null;
                    try
                    {
                        if (id <= 0)
                        {
                            dbItem = new TaskStepComment();
                            context.TaskStepComment.Add(dbItem);
                        }
                        else
                        {
                            dbItem = context.TaskStepComment.FirstOrDefault(o => o.TaskStepCommentID == id);
                        }

                        if (dbItem == null)
                        {
                            notification.Message = "Comment not found!";
                            return false;
                        }
                        else
                        {
                            converter.DTO2DB_TaskStepComment(dtoTaskStepComment, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;

                            // remove orphan
                            context.TaskStepCommentFile.Local.Where(o => o.TaskStepComment == null).ToList().ForEach(o => context.TaskStepCommentFile.Remove(o));
                            SendNotification(context);
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool DeleteComment(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TaskMngEntities context = CreateContext())
                {
                    try
                    {
                        TaskStepComment dbItem = context.TaskStepComment.FirstOrDefault(o => o.TaskStepCommentID == id);
                        if (dbItem == null)
                        {
                            notification.Message = "Comment not found!";
                            return false;
                        }
                        else
                        {
                            // check for child rows deleted
                            foreach (TaskStepCommentFile dbFile in dbItem.TaskStepCommentFile.ToArray())
                            {
                                if (!string.IsNullOrEmpty(dbFile.FileUD))
                                {
                                    fwFactory.RemoveFile(dbFile.FileUD);
                                }
                            }
                            context.TaskStepComment.Remove(dbItem);
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        private void CalcPercent(int id)
        {
            using (TaskMngEntities context = CreateContext())
            {
                Task dbItem = context.Task.FirstOrDefault(o => o.TaskID == id);
                if (dbItem != null)
                {
                    dbItem.TaskStatusID = 1; // pending
                    dbItem.CompletedPercent = 0;

                    if (dbItem.TaskStep.Count() > 0)
                    {
                        decimal percent = 0;
                        decimal stepPercent = Math.Round(Convert.ToDecimal(100 / dbItem.TaskStep.Count()), 2, MidpointRounding.AwayFromZero);
                        int totalFinish = 0;
                        foreach (TaskStep dbStep in dbItem.TaskStep)
                        {
                            if (dbStep.StepStatusID.HasValue && dbStep.StepStatusID == 3)
                            {
                                percent += stepPercent;
                                totalFinish++;
                            }
                            else
                            {
                                if (dbStep.StepStatusID.HasValue && dbStep.StepStatusID == 2)
                                {
                                    dbItem.TaskStatusID = 2; // process
                                }
                            }
                        }
                        if (totalFinish == dbItem.TaskStep.Count())
                        {
                            percent = 100;
                            dbItem.TaskStatusID = 3;
                        }
                        dbItem.CompletedPercent = percent;                        
                    }
                    context.SaveChanges();
                }
            }
        }

        private void SendNotification(TaskMngEntities context)
        {
            List<Library.DTO.EmailMessage> messages = new List<Library.DTO.EmailMessage>();
            foreach (var entry in context.ChangeTracker.Entries().Where(o=>o.State != EntityState.Unchanged && o.State != EntityState.Detached))
            {
                string emailTitle = "";
                string emailBody = "";
                string sendTo = "";

                switch (entry.State)
                { 
                    case EntityState.Added:
                        if (entry.Entity is TaskStep)
                        {
                            // new step added
                            TaskStep taskStep = (TaskStep)entry.Entity;
                            emailTitle = "Step: " + entry.CurrentValues["StepIndex"].ToString() + " has been added to Task: " + taskStep.Task.TaskNM;
                            emailBody = "Step description: " + entry.CurrentValues["Description"].ToString();
                            sendTo = "";
                            foreach (TaskStep step in taskStep.Task.TaskStep)
                            {
                                foreach (TaskStepUser stepUser in step.TaskStepUser)
                                {
                                    int userId = stepUser.UserID.Value;
                                    AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                    {
                                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                    }
                                    // add to NotificationMessage table
                                    NotificationMessage notification1 = new NotificationMessage();
                                    notification1.UserID = profile.UserId;
                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                    notification1.NotificationMessageTitle = emailTitle;
                                    notification1.NotificationMessageContent = emailBody;
                                    context.NotificationMessage.Add(notification1);
                                }
                            }
                            messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                        }
                        else if (entry.Entity is TaskStepUser)
                        { 
                            // new step user added
                            TaskStepUser stepUser = (TaskStepUser)entry.Entity;
                            if (stepUser.TaskStep.TaskStepID > 0)
                            {
                                emailTitle = "You have been added to step: " + stepUser.TaskStep.StepIndex + ", task: " + stepUser.TaskStep.Task.TaskNM;
                                emailBody = "You have been added to step: " + stepUser.TaskStep.StepIndex + ", task: " + stepUser.TaskStep.Task.TaskNM;
                                int userId = stepUser.UserID.Value;
                                AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                {
                                    sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                }
                                // add to NotificationMessage table
                                NotificationMessage notification1 = new NotificationMessage();
                                notification1.UserID = profile.UserId;
                                notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                notification1.NotificationMessageTitle = emailTitle;
                                notification1.NotificationMessageContent = emailBody;
                                context.NotificationMessage.Add(notification1);

                                messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                            }
                        }
                        else if (entry.Entity is TaskStepComment)
                        {
                            // new step comment added
                            TaskStepComment taskStepComment = (TaskStepComment)entry.Entity;
                            int userId = taskStepComment.UpdatedBy.Value;
                            int taskStepID = taskStepComment.TaskStepID.Value;
                            TaskStep taskStep = context.TaskStep.FirstOrDefault(o => o.TaskStepID == taskStepID);
                            emailTitle = "Comment has been added to step: " + taskStep.StepIndex + ", task: " + taskStep.Task.TaskNM;
                            emailBody = "Comment: " + taskStepComment.Comment + "<br/>User: " + context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId).FullName;
                            sendTo = "";
                            foreach (TaskStep step in taskStep.Task.TaskStep)
                            {
                                foreach (TaskStepUser stepUser in step.TaskStepUser)
                                {
                                    userId = stepUser.UserID.Value;
                                    AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                    {
                                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                    }
                                    // add to NotificationMessage table
                                    NotificationMessage notification1 = new NotificationMessage();
                                    notification1.UserID = profile.UserId;
                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                    notification1.NotificationMessageTitle = emailTitle;
                                    notification1.NotificationMessageContent = emailBody;
                                    context.NotificationMessage.Add(notification1);
                                }
                            }
                            messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                        }
                        break;

                    case EntityState.Deleted:
                        if (entry.Entity is Task)
                        {
                            // delete task
                            Task task = (Task)entry.Entity;
                            emailTitle = "Task: " + task.TaskNM + " has been deleted";
                            emailBody = "Task: " + task.TaskNM + " has been deleted";
                            sendTo = "";
                            foreach (TaskStep step in task.TaskStep)
                            {
                                foreach (TaskStepUser stepUser in step.TaskStepUser)
                                {
                                    int userId = stepUser.UserID.Value;
                                    AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                    {
                                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                    }
                                }
                            }

                            // get user in deleted task
                            int taskID = Convert.ToInt32(entry.OriginalValues["TaskID"]);
                            if (taskID > 0)
                            {
                                foreach (TaskStep step in context.TaskStep.Where(o=>o.TaskID == taskID).ToList())
                                {
                                    foreach (TaskStepUser stepUser in step.TaskStepUser)
                                    {
                                        int userId = stepUser.UserID.Value;
                                        AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                        if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                        {
                                            sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                        }
                                        // add to NotificationMessage table
                                        NotificationMessage notification1 = new NotificationMessage();
                                        notification1.UserID = profile.UserId;
                                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                        notification1.NotificationMessageTitle = emailTitle;
                                        notification1.NotificationMessageContent = emailBody;
                                        context.NotificationMessage.Add(notification1);
                                    }
                                }
                            }
                            messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                        }
                        else if (entry.Entity is TaskStep)
                        {
                            // delete task step
                            int taskID = Convert.ToInt32(entry.OriginalValues["TaskID"]);
                            Task task = context.Task.FirstOrDefault(o => o.TaskID == taskID);
                            if (task.TaskID > 0)
                            {
                                emailTitle = "Step: " + entry.OriginalValues["StepIndex"].ToString() + " has been removed from Task: " + task.TaskNM;
                                emailBody = "Step: " + entry.OriginalValues["StepIndex"].ToString() + " has been removed from Task: " + task.TaskNM;
                                sendTo = "";
                                foreach (TaskStep step in task.TaskStep)
                                {
                                    foreach (TaskStepUser stepUser in step.TaskStepUser)
                                    {
                                        int userId = stepUser.UserID.Value;
                                        AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                        if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                        {
                                            sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                        }
                                    }
                                }

                                // get user in deleted taskstep
                                int taskStepID = Convert.ToInt32(entry.OriginalValues["TaskStepID"]);
                                if (taskStepID > 0)
                                {
                                    foreach (TaskStepUser stepUser in context.TaskStepUser.Where(o=>o.TaskStepID == taskStepID).ToList())
                                    {
                                        int userId = stepUser.UserID.Value;
                                        AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                        if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                        {
                                            sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                        }
                                        // add to NotificationMessage table
                                        NotificationMessage notification1 = new NotificationMessage();
                                        notification1.UserID = profile.UserId;
                                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                        notification1.NotificationMessageTitle = emailTitle;
                                        notification1.NotificationMessageContent = emailBody;
                                        context.NotificationMessage.Add(notification1);
                                    }
                                }
                                messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                            }                           
                        }
                        else if (entry.Entity is TaskStepUser)
                        {
                            // delete step user
                            int taskStepID = Convert.ToInt32(entry.OriginalValues["TaskStepID"]);
                            TaskStep taskStep = context.TaskStep.FirstOrDefault(o => o.TaskStepID == taskStepID);
                            if (taskStep.TaskStepID > 0)
                            {
                                emailTitle = "You have been removed from step: " + taskStep.StepIndex + ", task: " + taskStep.Task.TaskNM;
                                emailBody = "You have been removed from step: " + taskStep.StepIndex + ", task: " + taskStep.Task.TaskNM;
                                int userId = Convert.ToInt32(entry.OriginalValues["UserID"]);
                                AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                {
                                    sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                }

                                // add to NotificationMessage table
                                NotificationMessage notification1 = new NotificationMessage();
                                notification1.UserID = profile.UserId;
                                notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                notification1.NotificationMessageTitle = emailTitle;
                                notification1.NotificationMessageContent = emailBody;
                                context.NotificationMessage.Add(notification1);

                                messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                            }
                        }
                        else if (entry.Entity is TaskStepComment)
                        {
                            // new step comment added
                            int taskStepID = Convert.ToInt32(entry.OriginalValues["TaskStepID"]);
                            int userId = Convert.ToInt32(entry.OriginalValues["UpdatedBy"]);
                            TaskStep taskStep = context.TaskStep.FirstOrDefault(o => o.TaskStepID == taskStepID);
                            emailTitle = "Comment has been removed from step: " + taskStep.StepIndex + ", task: " + taskStep.Task.TaskNM;
                            emailBody = "Comment: " + entry.OriginalValues["Comment"].ToString() + "<br/>User: " + context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId).FullName;
                            sendTo = "";
                            foreach (TaskStep step in taskStep.Task.TaskStep)
                            {
                                foreach (TaskStepUser stepUser in step.TaskStepUser)
                                {
                                    userId = stepUser.UserID.Value;
                                    AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                    {
                                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                    }

                                    // add to NotificationMessage table
                                    NotificationMessage notification1 = new NotificationMessage();
                                    notification1.UserID = profile.UserId;
                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                    notification1.NotificationMessageTitle = emailTitle;
                                    notification1.NotificationMessageContent = emailBody;
                                    context.NotificationMessage.Add(notification1);
                                }
                            }
                            messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                        }
                        break;

                    case EntityState.Modified:
                        if (entry.Entity is Task)
                        {
                            foreach (var propName in entry.OriginalValues.PropertyNames)
                            {
                                var originalValue = entry.OriginalValues[propName];
                                var currentValue = entry.CurrentValues[propName];
                                bool hasChanged = false;

                                if (originalValue == null && currentValue != null)
                                {
                                    hasChanged = true;
                                }
                                else if (originalValue != null && currentValue == null)
                                {
                                    hasChanged = true;
                                }
                                else if (originalValue != null && currentValue != null)
                                {
                                    if (originalValue.ToString() != currentValue.ToString())
                                    {
                                        hasChanged = true;
                                    }
                                }

                                if (hasChanged && propName == "Description")
                                {
                                    // task description changed
                                    Task task = (Task)entry.Entity;
                                    emailTitle = "Description for task: [" + task.TaskNM + "] has changed";
                                    emailBody = "New description: " + task.Description;
                                    sendTo = "";
                                    foreach (TaskStep step in task.TaskStep)
                                    {
                                        foreach (TaskStepUser stepUser in step.TaskStepUser)
                                        {
                                            int userId = stepUser.UserID.Value;
                                            AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                            if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                            {
                                                sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                            }

                                            // add to NotificationMessage table
                                            NotificationMessage notification1 = new NotificationMessage();
                                            notification1.UserID = profile.UserId;
                                            notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                            notification1.NotificationMessageTitle = emailTitle;
                                            notification1.NotificationMessageContent = emailBody;
                                            context.NotificationMessage.Add(notification1);
                                        }
                                    }
                                    messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });

                                    break;
                                }
                            }
                        }
                        else if (entry.Entity is TaskStep)
                        {
                            foreach (var propName in entry.OriginalValues.PropertyNames)
                            {
                                var originalValue = entry.OriginalValues[propName];
                                var currentValue = entry.CurrentValues[propName];
                                bool hasChanged = false;

                                if (originalValue == null && currentValue != null)
                                {
                                    hasChanged = true;
                                }
                                else if (originalValue != null && currentValue == null)
                                {
                                    hasChanged = true;
                                }
                                else if (originalValue != null && currentValue != null)
                                {
                                    if (originalValue.ToString() != currentValue.ToString())
                                    {
                                        hasChanged = true;
                                    }
                                }

                                if (hasChanged)
                                {
                                    TaskStep taskStep = (TaskStep)entry.Entity;
                                    switch (propName)
                                    { 
                                        case "Description":
                                            // task step description changed                                            
                                            emailTitle = "Description for step: " + taskStep.StepIndex + ", task: [" + taskStep.Task.TaskNM + "] has been changed";
                                            emailBody = "New description: " + taskStep.Description;
                                            sendTo = "";
                                            foreach (TaskStep step in taskStep.Task.TaskStep)
                                            {
                                                foreach (TaskStepUser stepUser in step.TaskStepUser)
                                                {
                                                    int userId = stepUser.UserID.Value;
                                                    AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                                    {
                                                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                                        
                                                    }
                                                    // add to NotificationMessage table
                                                    NotificationMessage notification1 = new NotificationMessage();
                                                    notification1.UserID = profile.UserId;
                                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                                    notification1.NotificationMessageTitle = emailTitle;
                                                    notification1.NotificationMessageContent = emailBody;
                                                    context.NotificationMessage.Add(notification1);
                                                }
                                            }
                                            messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });

                                            break;

                                        case "StepStatusID":
                                            // task step status changed
                                            emailTitle = "Status for step: " + taskStep.StepIndex + ", task: [" + taskStep.Task.TaskNM + "] has been changed";
                                            emailBody = "New status: " + taskStep.StepStatusID.ToString();
                                            sendTo = "";
                                            foreach (TaskStep step in taskStep.Task.TaskStep)
                                            {
                                                foreach (TaskStepUser stepUser in step.TaskStepUser)
                                                {
                                                    int userId = stepUser.UserID.Value;
                                                    AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                                    {
                                                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                                    }
                                                    // add to NotificationMessage table
                                                    NotificationMessage notification1 = new NotificationMessage();
                                                    notification1.UserID = profile.UserId;
                                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                                    notification1.NotificationMessageTitle = emailTitle;
                                                    notification1.NotificationMessageContent = emailBody;
                                                    context.NotificationMessage.Add(notification1);
                                                }
                                            }
                                            messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                                            
                                            break;
                                    }
                                }
                            }
                        }
                        else if (entry.Entity is TaskStepComment)
                        {
                            // step comment changed
                            TaskStepComment taskStepComment = (TaskStepComment)entry.Entity;
                            int userId = taskStepComment.UpdatedBy.Value;
                            int taskStepID = taskStepComment.TaskStepID.Value;
                            TaskStep taskStep = context.TaskStep.FirstOrDefault(o => o.TaskStepID == taskStepID);
                            emailTitle = "Comment has been changed, step: " + taskStep.StepIndex + ", task: " + taskStep.Task.TaskNM;
                            emailBody = "Comment: " + taskStepComment.Comment + "<br/>User: " + context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId).FullName;
                            sendTo = "";
                            foreach (TaskStep step in taskStep.Task.TaskStep)
                            {
                                foreach (TaskStepUser stepUser in step.TaskStepUser)
                                {
                                    userId = stepUser.UserID.Value;
                                    AccountMng_UserProfile_View profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                                    {
                                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;
                                    }
                                    // add to NotificationMessage table
                                    NotificationMessage notification1 = new NotificationMessage();
                                    notification1.UserID = profile.UserId;
                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SYSTEMADMINISTRATION;
                                    notification1.NotificationMessageTitle = emailTitle;
                                    notification1.NotificationMessageContent = emailBody;
                                    context.NotificationMessage.Add(notification1);
                                }
                            }
                            messages.Add(new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo });
                        }
                        break;
                }
            }

            foreach (Library.DTO.EmailMessage message in messages)
            {
                if (!string.IsNullOrEmpty(message.SendTo))
                {
                    EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                    dbEmail.EmailBody = message.EmailBody;
                    dbEmail.EmailSubject = message.EmailSubject;
                    dbEmail.SendTo = message.SendTo;
                    context.EmailNotificationMessage.Add(dbEmail);
                }                
            }
        }
    }
}
