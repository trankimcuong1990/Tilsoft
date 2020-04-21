using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserGroupMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.UserGroupMng.SearchFormData, DTO.UserGroupMng.EditFormData, DTO.UserGroupMng.UserGroup>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        private UserGroupMngEntities CreateContext()
        {
            return new UserGroupMngEntities(DALBase.Helper.CreateEntityConnectionString("UserGroupMngModel"));
        }

        public override DTO.UserGroupMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.UserGroupMng.SearchFormData data = new DTO.UserGroupMng.SearchFormData();
            data.Data = new List<DTO.UserGroupMng.UserGroupSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (UserGroupMngEntities context = CreateContext())
                {
                    totalRows = context.UserGroupMng_function_SearchUserGroup(orderBy, orderDirection).Count();
                    var result = context.UserGroupMng_function_SearchUserGroup(orderBy, orderDirection);
                    data.Data = converter.DB2DTO_UserGroupSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.UserGroupMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.UserGroupMng.EditFormData data = new DTO.UserGroupMng.EditFormData();
            data.Data = new DTO.UserGroupMng.UserGroup();
            data.Data.Permissions = new List<DTO.UserGroupMng.UserGroupPermission>();

            //try to get data
            try
            {
                using (UserGroupMngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        foreach (DTO.Support.Module mModule in supportFactory.GetModule())
                        {
                            data.Data.Permissions.Add(new DTO.UserGroupMng.UserGroupPermission() { ModuleID = mModule.ModuleID, ParentID = mModule.ParentID, DisplayText = mModule.DisplayText, CanCreate = false, CanRead = false, CanUpdate = false, CanDelete = false, CanPrint = false, CanApprove = false, CanReset = false });
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_UserGroup(context.UserGroupMng_UserGroup_View.Include("UserGroupMng_UserGroupPermission_View").FirstOrDefault(o => o.UserGroupID == id));
                    }

                    // decorate permission list 
                    int index = 1;
                    foreach (DTO.UserGroupMng.UserGroupPermission dtoPermission in data.Data.Permissions.Where(o => o.ParentID == 0).OrderBy(o => o.DisplayOrder))
                    {
                        dtoPermission.DisplayOrder = index;
                        index++;
                        foreach (DTO.UserGroupMng.UserGroupPermission dtoSubPermission in data.Data.Permissions.Where(o => o.ParentID == dtoPermission.ModuleID).OrderBy(o => o.DisplayOrder))
                        {
                            dtoSubPermission.DisplayOrder = index;
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

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (UserGroupMngEntities context = CreateContext())
                {
                    UserGroup dbItem = context.UserGroup.FirstOrDefault(o => o.UserGroupID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "User group not found!";
                        return false;
                    }
                    else
                    {
                        context.UserGroup.Remove(dbItem);
                        context.SaveChanges();

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

        public override bool UpdateData(int id, ref DTO.UserGroupMng.UserGroup dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (UserGroupMngEntities context = CreateContext())
                {
                    UserGroup dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new UserGroup();
                        context.UserGroup.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.UserGroup.FirstOrDefault(o => o.UserGroupID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "User group not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.UserGroupID, out notification).Data;

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

        public override bool Approve(int id, ref DTO.UserGroupMng.UserGroup dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.UserGroupMng.UserGroup dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
