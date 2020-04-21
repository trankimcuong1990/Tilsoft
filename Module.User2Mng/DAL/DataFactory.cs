using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.User2Mng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private User2MngEntities CreateContext()
        {
            return new User2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.User2MngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.UserSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    string UserUD = null;
                    string EmployeeNM = null;
                    string EmployeeFirstNM = null;
                    string UserName = null;
                    string Telephone = null;
                    string Email = null;
                    string SkypeID = null;
                    int? UserGroupID = null;
                    int? OfficeID = null;
                    bool? IsActivated = null;
                    int? CompanyID = null;
                    int? FactoryID = null;
                    int? DepartmentID = null;
                    string JobTitle = null;
                    bool? IsSuperUser = null;
                    int? JobLevelID = null;
                    int? LocationID = null;
                    if (filters.ContainsKey("UserUD") && !string.IsNullOrEmpty(filters["UserUD"].ToString()))
                    {
                        UserUD = filters["UserUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("EmployeeNM") && !string.IsNullOrEmpty(filters["EmployeeNM"].ToString()))
                    {
                        EmployeeNM = filters["EmployeeNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("EmployeeFirstNM") && !string.IsNullOrEmpty(filters["EmployeeFirstNM"].ToString()))
                    {
                        EmployeeFirstNM = filters["EmployeeFirstNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("UserName") && !string.IsNullOrEmpty(filters["UserName"].ToString()))
                    {
                        UserName = filters["UserName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Telephone") && !string.IsNullOrEmpty(filters["Telephone"].ToString()))
                    {
                        Telephone = filters["Telephone"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Email") && !string.IsNullOrEmpty(filters["Email"].ToString()))
                    {
                        Email = filters["Email"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("SkypeID") && !string.IsNullOrEmpty(filters["SkypeID"].ToString()))
                    {
                        SkypeID = filters["SkypeID"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("UserGroupID") && filters["UserGroupID"] != null && !string.IsNullOrEmpty(filters["UserGroupID"].ToString()))
                    {
                        UserGroupID = Convert.ToInt32(filters["UserGroupID"].ToString());
                    }
                    if (filters.ContainsKey("OfficeID") && filters["OfficeID"] != null && !string.IsNullOrEmpty(filters["OfficeID"].ToString()))
                    {
                        OfficeID = Convert.ToInt32(filters["OfficeID"].ToString());
                    }
                    //if (filters.ContainsKey("IsActivated") && filters["IsActivated"] != null && !string.IsNullOrEmpty(filters["IsActivated"].ToString()))
                    //{
                    //    switch (filters["IsActivated"].ToString().ToLower())
                    //    { 
                    //        case "true":
                    //            IsActivated = true;
                    //            break;

                    //        case "false":
                    //            IsActivated = false;
                    //            break;

                    //        default:
                    //            IsActivated = null;
                    //            break;
                    //    }
                    //}
                    if(filters.ContainsKey("IsActivated") && filters["IsActivated"] != null && !string.IsNullOrEmpty(filters["IsActivated"].ToString()))
                    {
                        IsActivated = (filters["IsActivated"].ToString() == "true") ? true : false;
                    }
                    if (filters.ContainsKey("CompanyID") && filters["CompanyID"] != null && !string.IsNullOrEmpty(filters["CompanyID"].ToString()))
                    {
                        CompanyID = Convert.ToInt32(filters["CompanyID"].ToString());
                    }
                    if (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("DepartmentID") && filters["DepartmentID"] != null && !string.IsNullOrEmpty(filters["DepartmentID"].ToString()))
                    {
                        DepartmentID = Convert.ToInt32(filters["DepartmentID"].ToString());
                    }
                    if (filters.ContainsKey("JobTitle") && !string.IsNullOrEmpty(filters["JobTitle"].ToString()))
                    {
                        JobTitle = filters["JobTitle"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsSuperUser") && filters["IsSuperUser"] != null && !string.IsNullOrEmpty(filters["IsSuperUser"].ToString()))
                    {
                        switch (filters["IsSuperUser"].ToString().ToLower())
                        {
                            case "true":
                                IsSuperUser = true;
                                break;

                            case "false":
                                IsSuperUser = false;
                                break;

                            default:
                                IsSuperUser = null;
                                break;
                        }
                    }
                    if (filters.ContainsKey("JobLevelID") && filters["JobLevelID"] != null && !string.IsNullOrEmpty(filters["JobLevelID"].ToString()))
                    {
                        JobLevelID = Convert.ToInt32(filters["JobLevelID"].ToString());
                    }
                    if (filters.ContainsKey("LocationID") && filters["LocationID"] != null && !string.IsNullOrEmpty(filters["LocationID"].ToString()))
                    {
                        LocationID = Convert.ToInt32(filters["LocationID"].ToString());
                    }
                    totalRows = context.User2Mng_function_SearchUserProfile(UserUD, EmployeeNM, EmployeeFirstNM, UserName, Telephone, Email, SkypeID, UserGroupID, OfficeID, IsActivated, CompanyID, FactoryID, DepartmentID, JobTitle, IsSuperUser, JobLevelID, LocationID, orderBy, orderDirection).Count();
                    var result = context.User2Mng_function_SearchUserProfile(UserUD, EmployeeNM, EmployeeFirstNM, UserName, Telephone, Email, SkypeID, UserGroupID, OfficeID, IsActivated, CompanyID, FactoryID, DepartmentID, JobTitle, IsSuperUser, JobLevelID, LocationID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_UserSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.UserProfile();
            data.Data.Permissions = new List<DTO.Permission>();
            data.Data.FactoryAccesses = new List<DTO.FactoryAccess>();
            data.Data.EmployeeFactories = new List<DTO.EmployeeFactory>();

            data.InternalCompanies = new List<Support.DTO.InternalCompany>();
            data.InternalDepartments = new List<Support.DTO.InternalDepartment>();
            data.Locations = new List<Support.DTO.Location>();
            data.UserGroups = new List<Support.DTO.UserGroup>();
            data.Managers = new List<Support.DTO.Employee>();
            data.JobLevels = new List<Support.DTO.JobLevel>();
            data.Factories = new List<Support.DTO.Factory>();

            //try to get data
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_UserProfile(context.User2Mng_UserProfile_View.FirstOrDefault(o => o.UserId == id));
                    
                    data.Data.Permissions = converter.DB2DTO_UserGroupPermissionList(context.User2Mng_UserGroupPermission_View.Where(o => o.UserGroupID == data.Data.UserGroupID).ToList());
                    List<DTO.Permission> permissions = converter.DB2DTO_UserPermissionList(context.User2Mng_UserPermission_View.Where(o => o.UserID == id).ToList());
                    DTO.Permission dtoTmpPermission;
                    foreach (DTO.Permission dtoPermission in permissions)
                    {
                        dtoTmpPermission = data.Data.Permissions.FirstOrDefault(o => o.ModuleID == dtoPermission.ModuleID);

                        if (dtoTmpPermission != null)
                        {
                            dtoTmpPermission.UserPermissionID = dtoPermission.UserPermissionID;

                            if (!dtoTmpPermission.CanCreate) dtoTmpPermission.CanCreateEditable = true;
                            if (dtoPermission.CanCreate) dtoTmpPermission.CanCreate = true;

                            if (!dtoTmpPermission.CanRead) dtoTmpPermission.CanReadEditable = true;
                            if (dtoPermission.CanRead) dtoTmpPermission.CanRead = true;

                            if (!dtoTmpPermission.CanUpdate) dtoTmpPermission.CanUpdateEditable = true;
                            if (dtoPermission.CanUpdate) dtoTmpPermission.CanUpdate = true;

                            if (!dtoTmpPermission.CanDelete) dtoTmpPermission.CanDeleteEditable = true;
                            if (dtoPermission.CanDelete) dtoTmpPermission.CanDelete = true;

                            if (!dtoTmpPermission.CanPrint) dtoTmpPermission.CanPrintEditable = true;
                            if (dtoPermission.CanPrint) dtoTmpPermission.CanPrint = true;

                            if (!dtoTmpPermission.CanApprove) dtoTmpPermission.CanApproveEditable = true;
                            if (dtoPermission.CanApprove) dtoTmpPermission.CanApprove = true;

                            if (!dtoTmpPermission.CanReset) dtoTmpPermission.CanResetEditable = true;
                            if (dtoPermission.CanReset) dtoTmpPermission.CanReset = true;
                        }
                        else
                        {
                            int moduleID = dtoPermission.ModuleID;//using to debug
                        }
                    }

                    data.Data.FactoryAccesses = converter.DB2DTO_FactoryAccessList(context.User2Mng_FactoryAccess_View.Where(o => o.UserID == id).ToList());
                    data.Data.EmployeeFactories = converter.DB2DTO_EmployeeFactoryList(context.User2Mng_EmployeeFactory_View.Where(o => o.EmployeeID == data.Data.EmployeeID).ToList());
                }

                // decorate
                int index = 0;
                foreach (DTO.Permission dtoPermission in data.Data.Permissions.Where(o => o.ParentID == 0).OrderBy(o => o.DisplayOrder))
                {
                    dtoPermission.DisplayOrder = index;
                    index++;
                    foreach (DTO.Permission dtoSubPermission in data.Data.Permissions.Where(o => o.ParentID == dtoPermission.ModuleID).OrderBy(o => o.DisplayOrder))
                    {
                        dtoSubPermission.DisplayText = "-----" + dtoSubPermission.DisplayText;
                        dtoSubPermission.DisplayOrder = index;
                        index++;
                    }
                }

                data.InternalCompanies = supportFactory.GetInternalCompany().ToList();
                data.InternalDepartments = supportFactory.GetInternalDepartment().ToList();
                data.Locations = supportFactory.GetLocation().ToList();
                data.UserGroups = supportFactory.GetUserGroup().ToList();
                data.Managers = supportFactory.GetEmployee().ToList();
                data.JobLevels = supportFactory.GetJobLevel().ToList();
                data.Factories = supportFactory.GetFactory().ToList();
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
            throw new NotImplementedException();
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
            data.Factories = new List<Support.DTO.Factory>();
            data.InternalCompanies = new List<Support.DTO.InternalCompany>();
            data.InternalDepartments = new List<Support.DTO.InternalDepartment>();
            data.JobLevels = new List<Support.DTO.JobLevel>();
            data.Locations = new List<Support.DTO.Location>();
            data.Offices = new List<Support.DTO.Office>();
            data.UserGroups = new List<Support.DTO.UserGroup>();

            //try to get data
            try
            {
                data.Factories = supportFactory.GetFactory().ToList();
                data.InternalCompanies = supportFactory.GetInternalCompany().ToList();
                data.InternalDepartments = supportFactory.GetInternalDepartment().ToList();
                data.JobLevels = supportFactory.GetJobLevel().ToList();
                data.Locations = supportFactory.GetLocation().ToList();
                data.Offices = supportFactory.GetOffice().ToList();
                data.UserGroups = supportFactory.GetUserGroup().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.UserGroups = new List<Support.DTO.UserGroup>();
            data.Employees = new List<DTO.NotYetMappedEmployee>();

            //try to get data
            try
            {
                data.UserGroups = supportFactory.GetUserGroup().ToList();
                using(User2MngEntities context = CreateContext())
                {
                    data.Employees = converter.DB2DTO_NotYetMappedEmployeeList(context.User2Mng_NotYetMappedEmployee_View.ToList());
                }                
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public int InitAccount(int userId, DTO.NewAccountData dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                UserProfile dbProfile;
                Employee dbEmployee;

                using (User2MngEntities context = CreateContext())
                {
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM UserProfile WITH (TABLOCKX, HOLDLOCK); SELECT * FROM Employee WITH (TABLOCKX, HOLDLOCK);");
                        try
                        {
                            dbProfile = new UserProfile();
                            dbProfile.CreatedBy = userId;
                            dbProfile.CreatedDate = DateTime.Now;
                            dbProfile.AspNetUsersId = dtoItem.AspNetUserId;
                            dbProfile.IsActivated = true;
                            dbProfile.UserGroupID = dtoItem.UserGroupID;
                            context.UserProfile.Add(dbProfile);
                            if (dtoItem.EmployeeID.HasValue)
                            {
                                dbEmployee = context.Employee.FirstOrDefault(o => o.EmployeeID == dtoItem.EmployeeID.Value);
                            }
                            else
                            {
                                dbEmployee = new Employee();
                                context.Employee.Add(dbEmployee);
                            }
                            context.SaveChanges();

                            context.User2Mng_function_InitAccount(dbProfile.UserId);
                            dbProfile.UserUD = dbProfile.UserId.ToString();
                            dbEmployee.EmployeeUD = dbEmployee.EmployeeID.ToString();
                            dbEmployee.UserID = dbProfile.UserId;
                            dbEmployee.UpdatedBy = userId;
                            dbEmployee.UpdatedDate = DateTime.Now;
                            context.SaveChanges();
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

                    return dbProfile.UserId;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return -1;
            }
        }

        public bool UpdateEmployee(int userId, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.UserProfile dtoProfile = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.UserProfile>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    Employee dbItem = context.Employee.FirstOrDefault(o => o.EmployeeID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Employee not found!");
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    converter.DTO2DB(dtoProfile, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                    dbItem.EmployeeFactory.ToList().ForEach(o => context.EmployeeFactory.Remove(o));
                    foreach (DTO.EmployeeFactory dtoFactory in dtoProfile.EmployeeFactories)
                    {
                        EmployeeFactory dbFactory = new EmployeeFactory();
                        dbItem.EmployeeFactory.Add(dbFactory);
                        dbFactory.FactoryID = dtoFactory.FactoryID;
                    }
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

        public bool UpdatePermission(int userId, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.Permission> dtoListPermission = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.Permission>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);
                    if (dbItem == null) 
                    {
                        throw new Exception("User not found!");
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    foreach (DTO.Permission dtoPermission in dtoListPermission)
                    {
                        UserPermission dbPermission = dbItem.UserPermission.FirstOrDefault(o => o.UserPermissionID == dtoPermission.UserPermissionID);
                        if (dtoPermission.CanReadEditable)
                        {
                            dbPermission.CanRead = dtoPermission.CanRead;
                        }
                        if (dtoPermission.CanCreateEditable)
                        {
                            dbPermission.CanCreate = dtoPermission.CanCreate;
                        }
                        if (dtoPermission.CanUpdateEditable)
                        {
                            dbPermission.CanUpdate = dtoPermission.CanUpdate;
                        }
                        if (dtoPermission.CanDeleteEditable)
                        {
                            dbPermission.CanDelete = dtoPermission.CanDelete;
                        }
                        if (dtoPermission.CanPrintEditable)
                        {
                            dbPermission.CanPrint = dtoPermission.CanPrint;
                        }
                        if (dtoPermission.CanApproveEditable)
                        {
                            dbPermission.CanApprove = dtoPermission.CanApprove;
                        }
                        if (dtoPermission.CanResetEditable)
                        {
                            dbPermission.CanReset = dtoPermission.CanReset;
                        }
                    }
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

        public bool UpdateAccount(int userId, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.UserData dtoUser = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.UserData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    UserProfile dbItem =  context.UserProfile.FirstOrDefault(o => o.UserId == id);                    
                    if (dbItem == null)
                    {
                        throw new Exception("User not found!");
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UserGroupID = dtoUser.UserGroupID;
                        dbItem.IsActivated = dtoUser.IsActivated;

                        Employee dbEmployee = context.Employee.FirstOrDefault(o => o.UserID == id);
                        if (dbEmployee != null)
                        {
                            dbEmployee.IsSuperUser = dtoUser.IsSuperUser;
                        }                        

                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool ForceChangePassword(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);
                    if (dbItem == null)
                    {
                        throw new Exception("User not found!");
                    }
                    else
                    {
                        dbItem.IsForcedToChangePassword = true;
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool UpdateFactoryAccess(int userId, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.FactoryAccess> dtoListFactoryAccess = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.FactoryAccess>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);
                    if (dbItem == null)
                    {
                        throw new Exception("User not found!");
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    foreach (DTO.FactoryAccess dtoFactoryAccess in dtoListFactoryAccess)
                    {
                        UserFactoryAccess dbFactoryAccess = dbItem.UserFactoryAccess.FirstOrDefault(o => o.UserFactoryAccessID == dtoFactoryAccess.UserFactoryAccessID);
                        dbFactoryAccess.CanAccess = dtoFactoryAccess.CanAccess;
                        dbFactoryAccess.CanReceiveNotification = dtoFactoryAccess.CanReceiveNotification;
                    }
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

        public bool DeleteUser(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);
                    if (dbItem == null)
                    {
                        throw new Exception("User not found!");
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.IsActivated = false;
                        dbItem.InActiveDate = DateTime.Now;
                        

                        Employee dbEmployee = context.Employee.FirstOrDefault(o => o.UserID.HasValue && o.UserID.Value == id);
                        if (dbEmployee != null)
                        {
                            dbEmployee.HasLeft = true;
                            dbEmployee.UpdatedBy = userId;
                            dbEmployee.UpdatedDate = DateTime.Now;
                            dbEmployee.LeftDate = DateTime.Now;
                        }

                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool RestoreUser(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);

                    if (dbItem == null)
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Not found id" };
                        return false;
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.IsActivated = true;
                        dbItem.InActiveDate = null;

                        Employee dbEmployee = context.Employee.FirstOrDefault(o => o.UserID.HasValue && o.UserID == id);
                        if (dbEmployee != null)
                        {
                            dbEmployee.HasLeft = false;
                            dbEmployee.LeftDate = null;
                            dbEmployee.UpdatedBy = userId;
                            dbEmployee.UpdatedDate = DateTime.Now;
                        }

                        context.SaveChanges();
                        return true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        public bool GetAPIKey(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (User2MngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);
                    if (dbItem == null)
                    {
                        throw new Exception("User not found!");
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.APIKey = System.Guid.NewGuid().ToString().ToUpper();
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return false;
            }
        }
    }
}
