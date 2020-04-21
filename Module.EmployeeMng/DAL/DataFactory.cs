using Module.EmployeeMng.DTO;
using Module.Support.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Module.EmployeeMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private string moduleCode = "EmployeeMng";
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private EmployeeMngEntities CreateContext()
        {
            return new EmployeeMngEntities(Library.Helper.CreateEntityConnectionString("DAL.EmployeeMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.EmployeeSearchResult>();
            data.TilsoftUsage = new DTO.TilsoftUsage();
            totalRows = 0;

            //try to get data
            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    string EmployeeNM = null;
                    string EmployeeFirstNM = null;
                    string JobTitle = null;
                    int? JobLevelID = null;
                    int? DepartmentID = null;
                    int? CompanyID = null;
                    string FactoryIDs = "";
                    bool? IsSuperUser = null;
                    int? LocationID = null;
                    int? TilsoftUsageID = null;
                    bool? HasLeft = false;
                    int? BranchID = null;
                    // Check Factory permission
                    int UserID = -1;

                    if (filters.ContainsKey("EmployeeNM") && !string.IsNullOrEmpty(filters["EmployeeNM"].ToString()))
                    {
                        EmployeeNM = filters["EmployeeNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("EmployeeFirstNM") && !string.IsNullOrEmpty(filters["EmployeeFirstNM"].ToString()))
                    {
                        EmployeeFirstNM = filters["EmployeeFirstNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("JobTitle") && !string.IsNullOrEmpty(filters["JobTitle"].ToString()))
                    {
                        JobTitle = filters["JobTitle"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("JobLevelID") && !string.IsNullOrEmpty(filters["JobLevelID"].ToString()))
                    {
                        JobLevelID = Convert.ToInt32(filters["JobLevelID"]);
                    }

                    if (filters.ContainsKey("DepartmentID") && !string.IsNullOrEmpty(filters["DepartmentID"].ToString()))
                    {
                        DepartmentID = Convert.ToInt32(filters["DepartmentID"]);
                    }

                    if (filters.ContainsKey("CompanyID") && !string.IsNullOrEmpty(filters["CompanyID"].ToString()))
                    {
                        CompanyID = Convert.ToInt32(filters["CompanyID"]);
                    }

                    if (filters.ContainsKey("FactoryIDs") && filters["FactoryIDs"] != null)
                    {
                        //pare object to list string of factoryid
                        List<string> fID = ((Newtonsoft.Json.Linq.JArray)filters["FactoryIDs"]).ToObject<List<string>>();
                        foreach (var item in fID)
                        {
                            FactoryIDs += item + ",";
                        }
                    }

                    if (filters.ContainsKey("IsSuperUser") && !string.IsNullOrEmpty(filters["IsSuperUser"].ToString()))
                    {
                        IsSuperUser = Convert.ToBoolean(filters["IsSuperUser"].ToString());
                    }

                    if (filters.ContainsKey("LocationID") && !string.IsNullOrEmpty(filters["LocationID"].ToString()))
                    {
                        LocationID = Convert.ToInt32(filters["LocationID"]);
                    }

                    if (filters.ContainsKey("TilsoftUsageID") && !string.IsNullOrEmpty(filters["TilsoftUsageID"].ToString()))
                    {
                        TilsoftUsageID = Convert.ToInt32(filters["TilsoftUsageID"]);
                    }

                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }

                    if (filters.ContainsKey("HasLeft") && filters["HasLeft"] != null && !string.IsNullOrEmpty(filters["HasLeft"].ToString()))
                    {
                        HasLeft = (filters["HasLeft"].ToString() == "true") ? true : false;
                    }

                    if (filters.ContainsKey("BranchID") && filters["BranchID"] != null && !string.IsNullOrEmpty(filters["BranchID"].ToString()))
                    {
                        BranchID = Convert.ToInt32(filters["BranchID"].ToString());
                    }

                    if (orderBy == "tilsoftUsageID" || TilsoftUsageID != null)
                    {
                        var orderByDefault = "updatedDate";
                        totalRows = context.EmployeeMng_function_SearchEmployee(EmployeeNM, JobTitle, JobLevelID, DepartmentID, CompanyID, FactoryIDs, IsSuperUser, LocationID, EmployeeFirstNM, UserID, HasLeft, orderByDefault, orderDirection, BranchID).Count();
                        data.TotalAccount = context.EmployeeMng_function_SearchEmployee(EmployeeNM, JobTitle, JobLevelID, DepartmentID, CompanyID, FactoryIDs, IsSuperUser, LocationID, EmployeeFirstNM, UserID, HasLeft, orderByDefault, orderDirection, BranchID).Where(o => o.UserID != null).Count();
                        var result = context.EmployeeMng_function_SearchEmployee(EmployeeNM, JobTitle, JobLevelID, DepartmentID, CompanyID, FactoryIDs, IsSuperUser, LocationID, EmployeeFirstNM, UserID, HasLeft, orderByDefault, orderDirection, BranchID);
                        //data.Data = converter.DB2DTO_EmployeeSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                        data.Data = converter.DB2DTO_EmployeeSearchResultList(result.ToList());
                    }
                    else
                    {
                        totalRows = context.EmployeeMng_function_SearchEmployee(EmployeeNM, JobTitle, JobLevelID, DepartmentID, CompanyID, FactoryIDs, IsSuperUser, LocationID, EmployeeFirstNM, UserID, HasLeft, orderBy, orderDirection, BranchID).Count();
                        data.TotalAccount = context.EmployeeMng_function_SearchEmployee(EmployeeNM, JobTitle, JobLevelID, DepartmentID, CompanyID, FactoryIDs, IsSuperUser, LocationID, EmployeeFirstNM, UserID, HasLeft, orderBy, orderDirection, BranchID).Where(o => o.UserID != null).Count();
                        var result = context.EmployeeMng_function_SearchEmployee(EmployeeNM, JobTitle, JobLevelID, DepartmentID, CompanyID, FactoryIDs, IsSuperUser, LocationID, EmployeeFirstNM, UserID, HasLeft, orderBy, orderDirection, BranchID);
                        data.Data = converter.DB2DTO_EmployeeSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }

                    // Get Tilsoft Usage data
                    foreach (DTO.EmployeeSearchResult dtoResult in data.Data)
                    {
                        if (dtoResult.UserID == null)
                        {
                            dtoResult.TilsoftUsage = "NO ACCOUNT";
                            dtoResult.TilsoftUsageID = 0;
                        }
                        else
                        {
                            var TilsoftUsageData = context.EmployeeMng_function_TilsoftAverageUsage().FirstOrDefault(o => o.UserID == dtoResult.UserID);
                            if (TilsoftUsageData == null)
                            {
                                dtoResult.TilsoftUsage = "NONE";
                                dtoResult.TilsoftUsageID = 1;
                            }
                            else
                            {
                                data.TilsoftUsage = converter.DB2DTO_TilsoftUsage(TilsoftUsageData);
                                dtoResult.TilsoftUsage = data.TilsoftUsage.AppUsage;
                                dtoResult.TilsoftUsageID = data.TilsoftUsage.AppUsageID;
                            }
                        }
                    }

                    if (TilsoftUsageID != null)
                    {
                        data.Data = data.Data.Where(o => o.TilsoftUsageID.ToString().ToUpper().Contains(TilsoftUsageID.ToString().ToUpper())).ToList();
                        totalRows = data.Data.Count();
                        data.TotalAccount = data.Data.Count(o => o.UserID != null);
                        data.Data = data.Data.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                    }

                    if (orderBy == "tilsoftUsageID" && orderDirection == "desc")
                    {
                        data.Data = data.Data.OrderByDescending(o => o.TilsoftUsageID).ToList();
                        data.Data = data.Data.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                    }

                    if (orderBy == "tilsoftUsageID" && orderDirection == "asc")
                    {
                        data.Data = data.Data.OrderBy(o => o.TilsoftUsageID).ToList();
                        data.Data = data.Data.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
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
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    Employee dbItem = context.Employee.FirstOrDefault(o => o.EmployeeID == id);
                    int UserID = Convert.ToInt32(dbItem.UserID);
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.UserId == UserID);
                    if (dbItem == null)
                    {
                        notification.Message = "Employee not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.HasLeft = true;
                        dbItem.LeftDate = DateTime.Now;
                        if (dbUser != null)
                        {
                            dbUser.IsActivated = false;
                            dbUser.InActiveDate = DateTime.Now;
                        }
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Employee dtoEmployee = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Employee>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.Employee> EmployeeDTOs = new List<DTO.Employee>();
            List<EmployeeResponsibleForDTO> EmployeeResponsibleForDTOs = new List<EmployeeResponsibleForDTO>();
            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    Employee dbItem = null;
                    if (dtoEmployee.SaleUD != null && dtoEmployee.SaleUD != "")
                    {
                        // Check SaleUD exist in Database
                        var checkItem = context.Employee.FirstOrDefault(o => o.SaleUD == dtoEmployee.SaleUD);
                        if (checkItem != null && checkItem.EmployeeID != id)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = dtoEmployee.SaleUD + " is existed in Employee table";
                            return false;
                        }
                    }

                    if (id == 0)
                    {
                        dbItem = new Employee();
                        context.Employee.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Employee.FirstOrDefault(o => o.EmployeeID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Employee not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoEmployee.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // processing resume file pdf                        
                        int eurofarCompanyID = 4;
                        int currentUserCompanyID = fwBll.GetCompanyID(userId).Value;
                        int currentUserGroupID = fwBll.GetUserGroup(userId);
                        bool canUploadCV = false;
                        if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_EMPLOYEE_RESUME))
                        {
                            if (!dtoEmployee.CompanyID.HasValue || currentUserGroupID == 1 || currentUserCompanyID == eurofarCompanyID || currentUserCompanyID == dtoEmployee.CompanyID.Value)
                            {
                                canUploadCV = true;
                            }
                        }
                        else
                        {
                            canUploadCV = false;
                        }
                        //if (fwBll.CanPerformAction(userId, moduleCode, Library.DTO.ModuleAction.CanApprove))
                        //{
                        //    if (currentUserCompanyID == eurofarCompanyID)
                        //    {
                        //        canUploadCV = true;
                        //    }
                        //    else
                        //    {
                        //        if (dbItem.CompanyID.HasValue && dbItem.CompanyID.Value == eurofarCompanyID)
                        //        {
                        //            canUploadCV = false;
                        //        }
                        //        else
                        //        {
                        //            canUploadCV = true;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    canUploadCV = false;
                        //}

                        if (canUploadCV && dtoEmployee.ResumeFile_HasChange)
                        {
                            dtoEmployee.ResumeFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoEmployee.ResumeFile_NewFile, dtoEmployee.ResumeFile, dtoEmployee.ResumeFileFriendlyName);
                        }
                        if (dtoEmployee.Email1 != "")
                        {
                            var EmployeeDBs = context.EmployeeMng_Employee_View.Where(o => o.EmployeeID != id).ToList();
                            EmployeeDTOs = converter.DB2DTO_EmployeeList(EmployeeDBs);
                            foreach (DTO.Employee EmployeeData in EmployeeDTOs)
                            {
                                if (dtoEmployee.Email1 != null)
                                {
                                    if (dtoEmployee.Email1 == EmployeeData.Email1)
                                    {
                                        throw new Exception("Duplicate Email");
                                    }
                                }
                            }
                        }

                        //foreach(DTO.Employee )
                        converter.DTO2BD(dtoEmployee, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                        //remove orphan item
                        context.AnnualLeaveSetting.Local.Where(o => o.Employee == null).ToList().ForEach(o => context.AnnualLeaveSetting.Remove(o));

                        //remove orphan item
                        context.EmployeeFactory.Local.Where(o => o.Employee == null).ToList().ForEach(o => context.EmployeeFactory.Remove(o));

                        context.EmployeeFile.Local.Where(o => o.Employee == null).ToList().ForEach(o => context.EmployeeFile.Remove(o));
                        context.EmployeeContract.Local.Where(o => o.Employee == null).ToList().ForEach(o => context.EmployeeContract.Remove(o));

                        context.EmployeeResponsibleFor.Local.Where(o => o.Employee == null).ToList().ForEach(o => context.EmployeeResponsibleFor.Remove(o));

                        context.QAQCFactoryAccess.Local.Where(o => o.Employee == null).ToList().ForEach(o => context.QAQCFactoryAccess.Remove(o));
                        // processing image
                        if (dtoEmployee.PersonalPhoto_HasChange)
                        {
                            dbItem.PersonalPhoto = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoEmployee.PersonalPhoto_NewFile, dbItem.PersonalPhoto);
                        }

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;
                        if (dbItem.IsAccountManager == false)
                        {
                            dbItem.IsIncludeInDDC = false;
                        }
                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.EmployeeID, out notification).Data;

                        return true;
                    }
                }
            }

            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
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
        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.JobLevels = new List<Support.DTO.JobLevel>();
            data.Departments = new List<Support.DTO.InternalDepartment>();
            //data.Companies = new List<Support.DTO.InternalCompany>();
            data.Factories = new List<Module.Support.DTO.Factory>();
            data.YesNoValues = new List<Module.Support.DTO.YesNo>();
            data.Locations = new List<Module.Support.DTO.Location>();
            data.Companies = new List<DTO.CompanyDTO>();
            data.Branches = new List<DTO.BranchDTO>();

            try
            {
                data.JobLevels = supportFactory.GetJobLevel();
                data.Departments = supportFactory.GetInternalDepartment();
                //data.Companies = supportFactory.GetInternalCompany();
                data.Factories = supportFactory.GetFactory().OrderBy(o => o.FactoryUD).ToList();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
                data.Locations = supportFactory.GetLocation().ToList();

                using (var context = CreateContext())
                {
                    data.Companies = AutoMapper.Mapper.Map<List<EmployeeMng_Company_View>, List<DTO.CompanyDTO>>(context.EmployeeMng_Company_View.ToList());
                    data.Branches = AutoMapper.Mapper.Map<List<EmployeeMng_Branch_View>, List<DTO.BranchDTO>>(context.EmployeeMng_Branch_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public List<DTO.Director> GetListDirector()
        {
            List<DTO.Director> result = new List<DTO.Director>();
            using (EmployeeMngEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_Director(context.EmployeeMng_Director_View.ToList());
            }
            return result;
        }

        //
        // Custom function
        //
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();

            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Employee();

            data.Data.AnnualLeaveSettings = new List<DTO.AnnualLeaveSetting>();
            data.JobLevels = new List<Module.Support.DTO.JobLevel>();
            //data.Companies = new List<Module.Support.DTO.InternalCompany>();
            data.Departments = new List<Module.Support.DTO.InternalDepartment>();
            data.Locations = new List<Module.Support.DTO.Location>();
            data.Directors = new List<DTO.Director>();
            data.Factories = new List<Module.Support.DTO.Factory>();
            data.YesNoValues = new List<Module.Support.DTO.YesNo>();
            data.AccountManagerType = new List<DTO.AccountManagerTypeData>();

            data.TilsoftUsage = new DTO.TilsoftUsage();
            data.Data.typeOfContractDTOs = new List<DTO.TypeOfContractDTO>();
            data.Companies = new List<DTO.CompanyDTO>();
            data.Branches = new List<DTO.BranchDTO>();
            
            //try to get data
            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        var employee = context.EmployeeMng_Employee_View
                            .Include("EmployeeMng_EmployeeFactory_View")
                            .Include("EmployeeMng_EmployeeFile_View")
                            .FirstOrDefault(o => o.EmployeeID == id);

                        var responsibleFor = context.EmployeeMng_function_getResponsibleFor(id).ToList();
                           
                        if (employee == null)
                        {
                            throw new Exception("Can not found the employee to edit");
                        }

                        // check permission
                        // description: user only can see other user information if one of the below condition is met
                        // 1: user is belong to admin group
                        // 2: viewing data from AVT + EUR user or from the company which is the same company with the logged in user
                        int currentUserCompanyID = fwFactory.GetCompanyID(iRequesterID).Value;
                        int currentUserGroupID = fwFactory.GetUserGroup(iRequesterID);
                        List<int> publicCompanyIDs = new List<int>() { 3, 4 }; // 3: AVT; 4: EUROFAR
                        List<int> masterCompanyIDs = new List<int>() { 3, 4, 2, 13, 75 }; // 3: AVT; 4: EUROFAR; 2: AVS; 13: ORANGE PINE; 75: PURPLE TOMATO
                        if (employee.CompanyID != null && currentUserGroupID != 1 && !masterCompanyIDs.Contains(currentUserCompanyID)) // 1: admin user group
                        {
                            if (currentUserCompanyID != employee.CompanyID && publicCompanyIDs.Contains(employee.CompanyID.Value))
                            {
                                throw new Exception("Viewing user's information from other company - except for EUROFAR and AN VIET THINH - is not allowed!");
                            }
                        }

                        //Convert name to title case
                        employee.EmployeeNM = textInfo.ToTitleCase(employee.EmployeeNM.ToLower());
                        data.Data = converter.DB2DTO_Employee(employee, iRequesterID);

                        data.Data.EmployeeResponsibleForDTOs = new List<EmployeeResponsibleForDTO>();
                        foreach (var item in responsibleFor)
                        {
                            EmployeeResponsibleForDTO employeeResponsible = new EmployeeResponsibleForDTO()
                            {
                                EmployeeID = item.EmployeeID,
                                DisplayText = item.DisplayText,
                                ReposibleForValue=item.ReposibleForValue,
                                ResposibleForID=item.ResposibleForID
                            };
                            data.Data.EmployeeResponsibleForDTOs.Add(employeeResponsible);
                        }

                        // Get Usage data
                        if (employee.UserID == null)
                        {
                            data.Data.TilsoftUsage = "NO ACCOUNT";
                            data.Data.TilsoftUsageID = 0;
                        }
                        else
                        {
                            var TilsoftUsageData = context.EmployeeMng_function_TilsoftAverageUsage().FirstOrDefault(o => o.UserID == employee.UserID);
                            if (TilsoftUsageData == null)
                            {
                                data.Data.TilsoftUsage = "NONE";
                                data.Data.TilsoftUsageID = 1;
                            }
                            else
                            {
                                data.TilsoftUsage = converter.DB2DTO_TilsoftUsage(TilsoftUsageData);
                                data.Data.TilsoftUsage = data.TilsoftUsage.AppUsage;
                            }
                        }

                        // Get List of Unselected Tilsoft User
                        //int? UserID = 0;
                        if (data.Data.UserID == null)
                        {
                            int? UserID = 0;
                            var userList = context.EmployeeMng_function_SearchUnSelectedUser(UserID);
                            data.Users = converter.DB2DTO_UnSelectedUser(userList.ToList());
                            //data.Data.UserDetails = new List<DTO.UserDetail>();
                            //data.Data.UserDetailWeeklyDetails = new List<DTO.UserDetailWeeklyDetail>();
                            //data.Data.UserDetailWeeklyOverviews = new List<DTO.UserDetailWeeklyOverview>();
                        }
                        else
                        {
                            int? UserID = data.Data.UserID;
                            var userList = context.EmployeeMng_function_SearchUnSelectedUser(UserID);
                            data.Users = converter.DB2DTO_UnSelectedUser(userList.ToList());

                            // get usage tracking data
                            //data.Data.UserDetails = converter.DB2DTO_UserDetail(context.EmployeeMng_function_getUserActionDetail(data.Data.UserID, null, null, null).ToList());
                            //data.Data.UserDetailWeeklyDetails = converter.DB2DTO_UserDetailWeeklyDetaill(context.EmployeeMng_function_getUserActionDetailWeeklyDetail(data.Data.UserID, null, null, null).ToList());
                            //data.Data.UserDetailWeeklyOverviews = converter.DB2DTO_UserDetailWeeklyOverview(context.EmployeeMng_function_getUserActionDetailWeeklyOverview(data.Data.UserID, null, null, null).ToList());
                        }


                        // authentication - check if user has approve permission
                        //
                        // author: themy
                        // description: change authentication process
                        // if current user have special permission and belong to eurofar => he/she has access to all resume
                        // if current user have special permission but not from eurfoar => he/she has access to his/her college's data only (same company id)
                        //
                        int eurofarCompanyID = 4;
                        //int currentUserCompanyID = fwBll.GetCompanyID(iRequesterID).Value;
                        if (fwFactory.HasSpecialPermission(iRequesterID, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_EMPLOYEE_RESUME))
                        {
                            if (data.Data.CompanyID.HasValue && currentUserCompanyID != data.Data.CompanyID.Value && currentUserCompanyID != eurofarCompanyID && currentUserGroupID != 1) // 1: admin group
                            {
                                data.Data.ResumeFileFriendlyName = string.Empty;
                                data.Data.ResumeFileLocation = string.Empty;
                            }
                        }
                        else
                        {
                            data.Data.ResumeFileFriendlyName = string.Empty;
                            data.Data.ResumeFileLocation = string.Empty;
                        }

                        data.AccountManagerType = AutoMapper.Mapper.Map<List<SupportMng_AccountManagerType_View>, List<DTO.AccountManagerTypeData>>(context.SupportMng_AccountManagerType_View.ToList());
                        data.Companies = AutoMapper.Mapper.Map<List<EmployeeMng_Company_View>, List<DTO.CompanyDTO>>(context.EmployeeMng_Company_View.ToList());
                        data.Branches = AutoMapper.Mapper.Map<List<EmployeeMng_Branch_View>, List<DTO.BranchDTO>>(context.EmployeeMng_Branch_View.ToList());
                    }
                    else
                    {
                        data.Companies = AutoMapper.Mapper.Map<List<EmployeeMng_Company_View>, List<DTO.CompanyDTO>>(context.EmployeeMng_Company_View.ToList());
                        data.Branches = AutoMapper.Mapper.Map<List<EmployeeMng_Branch_View>, List<DTO.BranchDTO>>(context.EmployeeMng_Branch_View.ToList());
                    }
                }

                data.JobLevels = supportFactory.GetJobLevel().ToList();
                //data.Companies = supportFactory.GetInternalCompany().ToList();
                data.Departments = supportFactory.GetInternalDepartment().ToList();
                data.Locations = supportFactory.GetLocation().ToList();
                data.Directors = GetListDirector().Where(o => o.ApprovalID.HasValue).ToList();
                data.Factories = supportFactory.GetFactory().OrderBy(o => o.FactoryUD).ToList();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
                data.ResponsibleFor = supportFactory.GetConstantEntries(EntryGroupType.ResponsibleFor);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.EditFormData GetSensitiveData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Employee();
            try
            {
                if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_EMPLOYEE_MANAGER_NOTE))
                {
                    using (EmployeeMngEntities context = CreateContext())
                    {
                        EmployeeMng_Employee_View dbItem = context.EmployeeMng_Employee_View
                            .Include("EmployeeMng_EmployeeFile_View")
                            .Include("EmployeeMng_EmployeeContract_View")
                            .FirstOrDefault(o => o.EmployeeID == id);

                        data.Data.ManagerNote = dbItem.ManagerNote;
                        data.Data.EmployeeFileDTOs = AutoMapper.Mapper.Map<List<EmployeeMng_EmployeeFile_View>, List<DTO.EmployeeFileDTO>>(dbItem.EmployeeMng_EmployeeFile_View.ToList());
                        data.Data.EmployeeContractDTOs = AutoMapper.Mapper.Map<List<EmployeeMng_EmployeeContract_View>, List<DTO.EmployeeContractDTO>>(dbItem.EmployeeMng_EmployeeContract_View.ToList());
                    }
                }
                else
                {
                    throw new Exception("Not authorized!");
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        //
        // Get Employee Detail by Tilsoft acc
        //
        public DTO.EditFormData GetDetail(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Employee();

            data.TilsoftUsage = new DTO.TilsoftUsage();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (EmployeeMngEntities context = CreateContext())
                    {
                        var employee = context.EmployeeMng_Employee_View.Include("EmployeeMng_EmployeeFactory_View").FirstOrDefault(o => o.UserID == id);
                        if (employee == null)
                        {
                            throw new Exception("Can not found the employee to view");
                        }
                        //Convert name to title case
                        employee.EmployeeNM = textInfo.ToTitleCase(employee.EmployeeNM.ToLower());
                        data.Data = converter.DB2DTO_Employee(employee, userId);

                        // Get Usage data
                        if (employee.UserID == null)
                        {
                            data.Data.TilsoftUsage = "NO ACCOUNT";
                            data.Data.TilsoftUsageID = 0;
                        }
                        else
                        {
                            var TilsoftUsageData = context.EmployeeMng_function_TilsoftAverageUsage().FirstOrDefault(o => o.UserID == employee.UserID);
                            if (TilsoftUsageData == null)
                            {
                                data.Data.TilsoftUsage = "NONE";
                                data.Data.TilsoftUsageID = 1;
                            }
                            else
                            {
                                data.TilsoftUsage = converter.DB2DTO_TilsoftUsage(TilsoftUsageData);
                                data.Data.TilsoftUsage = data.TilsoftUsage.AppUsage;
                                data.Data.TilsoftUsageID = data.TilsoftUsage.AppUsageID;
                            }
                        }
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

        //
        // Get Employee Detail by Employee info
        //
        public DTO.EditFormData GetEmpDetail(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Employee();

            data.TilsoftUsage = new DTO.TilsoftUsage();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (EmployeeMngEntities context = CreateContext())
                    {
                        var employee = context.EmployeeMng_Employee_View.Include("EmployeeMng_EmployeeFactory_View").FirstOrDefault(o => o.EmployeeID == id);
                        if (employee == null)
                        {
                            throw new Exception("Can not found the employee to view");
                        }
                        //Convert name to title case
                        employee.EmployeeNM = textInfo.ToTitleCase(employee.EmployeeNM.ToLower());
                        data.Data = converter.DB2DTO_Employee(employee, userId);

                        // Get Usage data
                        if (employee.UserID == null)
                        {
                            data.Data.TilsoftUsage = "NO ACCOUNT";
                            data.Data.TilsoftUsageID = 0;
                        }
                        else
                        {
                            var TilsoftUsageData = context.EmployeeMng_function_TilsoftAverageUsage().FirstOrDefault(o => o.UserID == employee.UserID);
                            if (TilsoftUsageData == null)
                            {
                                data.Data.TilsoftUsage = "NONE";
                                data.Data.TilsoftUsageID = 1;
                            }
                            else
                            {
                                data.TilsoftUsage = converter.DB2DTO_TilsoftUsage(TilsoftUsageData);
                                data.Data.TilsoftUsage = data.TilsoftUsage.AppUsage;
                                data.Data.TilsoftUsageID = data.TilsoftUsage.AppUsageID;
                            }
                        }
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

        //
        // overview
        //
        public DTO.Overview.FormData GetOverviewData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.Overview.FormData data = new DTO.Overview.FormData();
            data.Data = new DTO.Overview.EmployeeDTO();
            data.SensitiveData = new DTO.Overview.ManagementData();
            data.UserDataRpt = new List<DTO.Overview.UserDataRpt>();
            data.HitCountDataRpt = new List<DTO.Overview.HitCountDataRpt>();

            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    EmployeeMng_Overview_Employee_View dbItem = context.EmployeeMng_Overview_Employee_View.FirstOrDefault(o => o.EmployeeID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Employee not found!");
                    }

                    data.Data = converter.DB2DTO_Overview_Employee(dbItem);

                    // User
                    var UserData = context.EmployeeMng_function_getUserData(Library.OMSHelper.Helper.GetCurrentSeason());
                    data.UserDataRpt = converter.DB2DTO_UserDataRpt(UserData.ToList());

                    // HitCount
                    data.HitCountDataRpt = converter.DB2DTO_HitCountDataRpt(context.EmployeeMng_HitCountDataRpt_View.ToList());

                    // Get Usage data
                    if (dbItem.UserID == null)
                    {
                        data.Data.TilsoftUsage = "NO ACCOUNT";
                    }
                    else
                    {
                        var TilsoftUsageData = context.EmployeeMng_function_TilsoftAverageUsage().FirstOrDefault(o => o.UserID == dbItem.UserID);
                        if (TilsoftUsageData == null)
                        {
                            data.Data.TilsoftUsage = "NONE";
                        }
                        else
                        {
                            data.Data.TilsoftUsage = converter.DB2DTO_TilsoftUsage(TilsoftUsageData).AppUsage;
                        }

                        // get usage tracking data
                        data.Data.UserDetailDTOs = converter.DB2DTO_Overview_UserDetail(context.EmployeeMng_Overview_function_getUserActionDetail(dbItem.UserID, null, null, null).ToList());
                        data.Data.UserDetailWeeklyDetailDTOs = converter.DB2DTO_Overview_UserDetailWeeklyDetaill(context.EmployeeMng_Overview_function_getUserActionDetailWeeklyDetail(dbItem.UserID, null, null, null).ToList());
                        data.Data.UserDetailWeeklyOverviewDTOs = converter.DB2DTO_Overview_UserDetailWeeklyOverview(context.EmployeeMng_Overview_function_getUserActionDetailWeeklyOverview(dbItem.UserID, null, null, null).ToList());
                    }

                    int eurofarCompanyID = 4;
                    int currentUserCompanyID = fwBll.GetCompanyID(userId).Value;
                    int currentUserGroupID = fwBll.GetUserGroup(userId);
                    if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_EMPLOYEE_RESUME)) // check special permission for accessing resume file
                    {
                        if (data.Data.CompanyID == 0 || currentUserGroupID == 1 || currentUserCompanyID == eurofarCompanyID || currentUserCompanyID == data.Data.CompanyID)
                        {
                            data.Data.ResumeFileFriendlyName = dbItem.ResumeFileFriendlyName;
                            data.Data.ResumeFileLocation = !string.IsNullOrEmpty(dbItem.ResumeFileLocation) ? FrameworkSetting.Setting.MediaFullSizeUrl + dbItem.ResumeFileLocation : "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.Overview.FormData GetOverviewSensitiveData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.Overview.FormData data = new DTO.Overview.FormData();
            data.Data = new DTO.Overview.EmployeeDTO();
            data.SensitiveData = new DTO.Overview.ManagementData();

            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    Employee dbItem = context.Employee.FirstOrDefault(o => o.EmployeeID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Employee not found!");
                    }

                    data.SensitiveData.ManagerNote = dbItem.ManagerNote;
                    data.SensitiveData.EmployeeFileDTOs = converter.DB2DTO_Overview_EmployeeFile(context.EmployeeMng_Overview_EmployeeFile_View.Where(o => o.EmployeeID == id).ToList());
                    data.SensitiveData.EmployeeContractDTOs = converter.DB2DTO_Overview_EmployeeContract(context.EmployeeMng_Overview_EmployeeContract_View.Where(o => o.EmployeeID == id).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        //Restore

        public bool Restore(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    Employee dbItem = context.Employee.FirstOrDefault(o => o.EmployeeID == id);
                    int UserID = Convert.ToInt32(dbItem.UserID);
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.UserId == UserID);

                    if (dbItem == null)
                    {
                        notification.Message = "Employee not found !";
                        return false;
                    }
                    else
                    {
                        dbItem.HasLeft = false;
                        dbItem.LeftDate = null;
                        if (dbUser != null)
                        {
                            dbUser.IsActivated = true;
                            dbUser.InActiveDate = null;
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

        public bool DeactiveAccount(int id, string hasLeftDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (EmployeeMngEntities context = CreateContext())
                {
                    Employee dbItem = context.Employee.FirstOrDefault(o => o.EmployeeID == id);
                    int UserID = Convert.ToInt32(dbItem.UserID);
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.UserId == UserID);
                    if (dbItem == null)
                    {
                        notification.Message = "Employee not found!";
                        return false;
                    }
                    else
                    {
                        System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
                        DateTime leftDate = new DateTime();
                        if (!string.IsNullOrEmpty(hasLeftDate))
                        {
                            if (DateTime.TryParse(hasLeftDate, nl, System.Globalization.DateTimeStyles.None, out DateTime tmpDate))
                            {
                                leftDate = tmpDate;
                            }
                        }

                        dbItem.HasLeft = true;
                        dbItem.LeftDate = leftDate;

                        if (dbUser != null)
                        {
                            dbUser.IsActivated = false;
                            dbUser.InActiveDate = DateTime.Now;
                        }

                        List<Employee> employees = context.Employee.Where(o => o.ReportToID == id).ToList();
                        foreach(var item in employees)
                        {
                            item.ReportToID = null;
                        }
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message; // fix to get error exception
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        public string ExportExcelEmployee(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            EmployeeMngObject ds = new EmployeeMngObject();

            int? JobLevelID = null;
            int? DepartmentID = null;
            int? CompanyID = null;
            int? LocationID = null;
            bool? HasLeft = null;
            int? BranchID = null;

            if (filters.ContainsKey("JobLevelID") && filters["JobLevelID"] != null && !string.IsNullOrEmpty(filters["JobLevelID"].ToString()))
            {
                JobLevelID = Convert.ToInt32(filters["JobLevelID"]);
            }
            if (filters.ContainsKey("DepartmentID") && filters["DepartmentID"] != null && !string.IsNullOrEmpty(filters["DepartmentID"].ToString()))
            {
                DepartmentID = Convert.ToInt32(filters["DepartmentID"]);
            }
            if (filters.ContainsKey("CompanyID") && filters["CompanyID"] != null && !string.IsNullOrEmpty(filters["CompanyID"].ToString()))
            {
                CompanyID = Convert.ToInt32(filters["CompanyID"]);
            }
            if (filters.ContainsKey("BranchID") && filters["BranchID"] != null && !string.IsNullOrEmpty(filters["BranchID"].ToString()))
            {
                BranchID = Convert.ToInt32(filters["BranchID"]);
            }
            if (filters.ContainsKey("LocationID") && filters["LocationID"] != null && !string.IsNullOrEmpty(filters["LocationID"].ToString()))
            {
                LocationID = Convert.ToInt32(filters["LocationID"]);
            }
            if (filters.ContainsKey("HasLeft") && filters["HasLeft"] != null && !string.IsNullOrEmpty(filters["HasLeft"].ToString()))
            {
                HasLeft = (filters["HasLeft"].ToString() == "true") ? true : false;
            }

            try
            {

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("EmployeeRpt_function_Employee", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@JobLevelID", JobLevelID);
                adap.SelectCommand.Parameters.AddWithValue("@InternalDepartmentID", DepartmentID);
                adap.SelectCommand.Parameters.AddWithValue("@InternalCompanyID", CompanyID);
                adap.SelectCommand.Parameters.AddWithValue("@LocationID", LocationID);
                adap.SelectCommand.Parameters.AddWithValue("@HasLeft", HasLeft);
                adap.SelectCommand.Parameters.AddWithValue("@BranchID", BranchID);

                adap.TableMappings.Add("Table", "EmployeeReport");
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "EmployeeRpt");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }
    }
}
