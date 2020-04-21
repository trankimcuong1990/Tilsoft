using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PermissionOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private PermissionOverviewRptEntities CreateContext()
        {
            return new PermissionOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.PermissionOverviewRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
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
        public List<DTO.UserPermission> GetReportDataDetail(int? moduleId, int? userId, int? userGroupId, int? officeId)
        {
            List<DTO.UserPermission> data = new List<DTO.UserPermission>();

            using (PermissionOverviewRptEntities context = CreateContext())
            {
                List<PermissionOverviewRpt_UserGroupPermission_View> dbUserGroupPermissions = context.PermissionOverviewRpt_function_getUserGroupPermission(moduleId, userId, userGroupId, officeId).ToList();
                List<PermissionOverviewRpt_UserPermission_View> dbUserPermissions = context.PermissionOverviewRpt_function_getUserPermission(moduleId, userId, userGroupId, officeId).ToList();

                foreach (PermissionOverviewRpt_UserGroupPermission_View dbGPermission in dbUserGroupPermissions.OrderBy(o => o.Fullname))
                {
                    int currentModuleID = dbGPermission.ModuleID.Value;
                    int currentUserID = dbGPermission.UserId.Value;
                    DTO.UserPermission permission = new DTO.UserPermission();
                    permission.ModuleID = dbGPermission.ModuleID.Value;
                    permission.UserId = dbGPermission.UserId.Value;
                    permission.Fullname = dbGPermission.Fullname;
                    permission.UserName = dbGPermission.UserName;
                    permission.UserGroupID = dbGPermission.UserGroupID.Value;
                    permission.UserGroupNM = dbGPermission.UserGroupNM;
                    permission.Email = dbGPermission.Email;
                    permission.OfficeID = dbGPermission.OfficeID.Value;
                    permission.OfficeNM = dbGPermission.OfficeNM;
                    permission.CanRead = dbGPermission.CanRead;
                    permission.CanCreate = dbGPermission.CanCreate;
                    permission.CanUpdate = dbGPermission.CanUpdate;
                    permission.CanDelete = dbGPermission.CanDelete;
                    permission.CanPrint = dbGPermission.CanPrint;
                    permission.CanApprove = dbGPermission.CanApprove;
                    permission.CanReset = dbGPermission.CanReset;

                    // check duplication
                    PermissionOverviewRpt_UserPermission_View dbUPermission = dbUserPermissions.FirstOrDefault(o => o.ModuleID == currentModuleID && o.UserId == currentUserID);
                    if (dbUPermission != null)
                    {
                        if (dbUPermission.CanRead)
                        {
                            permission.CanRead = true;
                        }
                        if (dbUPermission.CanCreate)
                        {
                            permission.CanCreate = true;
                        }
                        if (dbUPermission.CanUpdate)
                        {
                            permission.CanUpdate = true;
                        }
                        if (dbUPermission.CanDelete)
                        {
                            permission.CanDelete = true;
                        }
                        if (dbUPermission.CanPrint)
                        {
                            permission.CanPrint = true;
                        }
                        if (dbUPermission.CanApprove)
                        {
                            permission.CanApprove = true;
                        }
                        if (dbUPermission.CanReset)
                        {
                            permission.CanReset = true;
                        }
                    }

                    data.Add(permission);
                }
            }

            return data.Where(o => o.CanRead == true).ToList();
        }

        public List<DTO.Module> GetReportData(int? moduleId, int? userId, int? userGroupId, int? officeId)
        {
            List<DTO.UserPermission> permissionDetails = GetReportDataDetail(moduleId, userId, userGroupId, officeId);
            List<DTO.Module> data = new List<DTO.Module>();
            using (PermissionOverviewRptEntities context = CreateContext())
            {
                data = converter.DB2DTO_Module(context.PermissionOverviewRpt_function_getModule(moduleId).ToList());
            }
            foreach (int mID in permissionDetails.Select(o => o.ModuleID).Distinct())
            {
                DTO.Module mModule = data.FirstOrDefault(o => o.ModuleID == mID);
                if (mModule != null)
                {
                    mModule.CreateCount = permissionDetails.Count(o => o.ModuleID == mID && o.CanCreate);
                    mModule.ReadCount = permissionDetails.Count(o => o.ModuleID == mID && o.CanRead);
                    mModule.UpdateCount = permissionDetails.Count(o => o.ModuleID == mID && o.CanUpdate);
                    mModule.DeleteCount = permissionDetails.Count(o => o.ModuleID == mID && o.CanDelete);
                    mModule.PrintCount = permissionDetails.Count(o => o.ModuleID == mID && o.CanPrint);
                    mModule.ApproveCount = permissionDetails.Count(o => o.ModuleID == mID && o.CanApprove);
                    mModule.ResetCount = permissionDetails.Count(o => o.ModuleID == mID && o.CanReset);
                }
            }

            // decorate permission list 
            int index = 1;
            foreach (DTO.Module module in data.Where(o => o.ParentID == 0).OrderBy(o => o.DisplayOrder))
            {
                module.DisplayOrder = index;
                module.IsParent = true;
                index++;
                foreach (DTO.Module subModule in data.Where(o => o.ParentID == module.ModuleID).OrderBy(o => o.DisplayOrder))
                {
                    subModule.DisplayText = "-----" + subModule.DisplayText;
                    subModule.DisplayOrder = index;
                    subModule.IsParent = false;
                    index++;
                }
            }

            return data.Where(o=>o.ReadCount > 0).ToList();
        }

        public DTO.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.UserGroups = new List<Support.DTO.UserGroup>();
            data.Users = new List<Support.DTO.User>();
            data.Offices = new List<Support.DTO.InternalCompany>();
            data.Modules = new List<DTO.Module>();

            //try to get data
            try
            {
                data.UserGroups = supportFactory.GetUserGroup().ToList();
                data.Users = supportFactory.GetUsers().ToList();
                data.Offices = supportFactory.GetInternalCompany().ToList();
                using (PermissionOverviewRptEntities context = CreateContext())
                {
                    data.Modules = converter.DB2DTO_Module(context.PermissionOverviewRpt_Module_View.ToList());
                    
                    // decorate permission list 
                    int index = 1;
                    foreach (DTO.Module module in data.Modules.Where(o => o.ParentID == 0).OrderBy(o => o.DisplayOrder))
                    {
                        module.DisplayOrder = index;
                        module.IsParent = true;
                        index++;
                        foreach (DTO.Module subModule in data.Modules.Where(o => o.ParentID == module.ModuleID).OrderBy(o => o.DisplayOrder))
                        {
                            subModule.DisplayText = "-----" + subModule.DisplayText;
                            subModule.DisplayOrder = index;
                            subModule.IsParent = false;
                            index++;
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
    }
}
