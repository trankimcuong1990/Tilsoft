using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.UserMng.SearchFormData, DTO.UserMng.EditFormData, DTO.UserMng.UserProfile>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private string _TempFolder;

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private UserMngEntities CreateContext()
        {
            return new UserMngEntities(DALBase.Helper.CreateEntityConnectionString("UserMngModel"));
        }

        public override DTO.UserMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.UserMng.SearchFormData data = new DTO.UserMng.SearchFormData();
            data.Data = new List<DTO.UserMng.UserProfileSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (UserMngEntities context = CreateContext())
                {
                    string UserName = null;
                    string UserUD = null;
                    string FullName = null;
                    int? UserGroupID = null;
                    string PhoneNumber = null;
                    string Email = null;
                    string SkypeID = null;
                    int? OfficeID = null;
                    bool? IsActivated = null;

                    if (filters.ContainsKey("UserName") && !string.IsNullOrEmpty(filters["UserName"].ToString()))
                    {
                        UserName = filters["UserName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("UserUD") && !string.IsNullOrEmpty(filters["UserUD"].ToString()))
                    {
                        UserUD = filters["UserUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FullName") && !string.IsNullOrEmpty(filters["FullName"].ToString()))
                    {
                        FullName = filters["FullName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("PhoneNumber") && !string.IsNullOrEmpty(filters["PhoneNumber"].ToString()))
                    {
                        PhoneNumber = filters["PhoneNumber"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Email") && !string.IsNullOrEmpty(filters["Email"].ToString()))
                    {
                        Email = filters["Email"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("PhoneNumber") && !string.IsNullOrEmpty(filters["PhoneNumber"].ToString()))
                    {
                        PhoneNumber = filters["PhoneNumber"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("SkypeID") && !string.IsNullOrEmpty(filters["SkypeID"].ToString()))
                    {
                        SkypeID = filters["SkypeID"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("UserGroupID") && !string.IsNullOrEmpty(filters["UserGroupID"].ToString()))
                    {
                        UserGroupID = Convert.ToInt32(filters["UserGroupID"].ToString());
                    }
                    if (filters.ContainsKey("OfficeID") && !string.IsNullOrEmpty(filters["OfficeID"].ToString()))
                    {
                        OfficeID = Convert.ToInt32(filters["OfficeID"].ToString());
                    }
                    if (filters.ContainsKey("IsActivated") && filters["IsActivated"] != null && !string.IsNullOrEmpty(filters["IsActivated"].ToString()))
                    {
                        IsActivated = (filters["IsActivated"].ToString() == "true") ? true : false;
                    }

                    totalRows = context.UserMng_function_SearchUserProfile(UserName, UserUD, FullName, UserGroupID, PhoneNumber, Email, SkypeID, OfficeID, IsActivated, orderBy, orderDirection).Count();
                    var result = context.UserMng_function_SearchUserProfile(UserName, UserUD, FullName, UserGroupID, PhoneNumber, Email, SkypeID, OfficeID, IsActivated, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_UserProfileSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.UserMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.UserMng.EditFormData data = new DTO.UserMng.EditFormData();
            data.Data = new DTO.UserMng.UserProfile() { PersonalPhoto_DisplayURL = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg", SignatureImage_DisplayURL = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg" }; 
            data.Data.FactoryAccesses = new List<DTO.UserMng.FactoryAccess>();
            data.Data.UserPermissions = new List<DTO.UserMng.UserPermission>();
            data.Data.AffectivePermissions = new List<DTO.UserMng.UserPermission>();

            data.Offices = new List<DTO.Support.Office>();
            data.UserGroups = new List<DTO.Support.UserGroup>();

            //try to get data
            try
            {
                using (UserMngEntities context = CreateContext())
                {
                    int index = -1;

                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_UserProfile(context.UserMng_UserProfile_View
                            .Include("UserMng_UserFactoryAccess_View")
                            .Include("UserMng_UserPermission_View")
                            .Include("UserMng_UserGroupPermission_View")
                            .FirstOrDefault(o => o.UserId == id));
                    }
                    else
                    {
                        foreach (DTO.Support.Factory dtoFactory in supportFactory.GetFactory())
                        {
                            data.Data.FactoryAccesses.Add(new DTO.UserMng.FactoryAccess() {FactoryID=dtoFactory.FactoryID, FactoryUD = dtoFactory.FactoryUD, FactoryName = dtoFactory.FactoryName, CanAccess = false });
                        }

                        foreach (DTO.Support.Module mModule in supportFactory.GetModule())
                        {
                            data.Data.UserPermissions.Add(new DTO.UserMng.UserPermission() { ModuleID = mModule.ModuleID, ParentID = mModule.ParentID, DisplayText = mModule.DisplayText, CanCreate = false, CanRead = false, CanUpdate = false, CanDelete = false, CanPrint = false, CanApprove = false, CanReset = false });
                        }
                    }

                    // decorate permission list 
                    foreach (DTO.UserMng.UserPermission dtoPermission in data.Data.UserPermissions.Where(o => o.ParentID == 0).OrderBy(o => o.DisplayOrder))
                    {
                        dtoPermission.DisplayOrder = index;
                        index++;
                        foreach (DTO.UserMng.UserPermission dtoSubPermission in data.Data.UserPermissions.Where(o => o.ParentID == dtoPermission.ModuleID).OrderBy(o => o.DisplayOrder))
                        {
                            dtoSubPermission.DisplayOrder = index;
                            index++;
                        }
                    }

                    // decorate affective list list 
                    foreach (DTO.UserMng.UserPermission dtoPermission in data.Data.AffectivePermissions.Where(o => o.ParentID == 0).OrderBy(o => o.DisplayOrder))
                    {
                        dtoPermission.DisplayOrder = index;
                        index++;
                        foreach (DTO.UserMng.UserPermission dtoSubPermission in data.Data.AffectivePermissions.Where(o => o.ParentID == dtoPermission.ModuleID).OrderBy(o => o.DisplayOrder))
                        {
                            dtoSubPermission.DisplayOrder = index;
                            index++;
                        }
                    }

                    data.Offices = supportFactory.GetOffice();
                    data.UserGroups = supportFactory.GetUserGroup().ToList();
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
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (UserMngEntities context = CreateContext())
            //    {
            //        UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "User not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            if (!string.IsNullOrEmpty(dbItem.PersonalPhoto))
            //            {
            //                // remove image
            //                (new Framework.DataFactory()).RemoveImageFile(dbItem.PersonalPhoto);
            //            }
            //            if (!string.IsNullOrEmpty(dbItem.SignatureImage))
            //            {
            //                // remove image
            //                (new Framework.DataFactory()).RemoveImageFile(dbItem.SignatureImage);
            //            }

            //            context.UserProfile.Remove(dbItem);
            //            context.SaveChanges();

            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    return false;
            //}
            throw new NotImplementedException();
        }

        public override bool UpdateData(int id, ref DTO.UserMng.UserProfile dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (UserMngEntities context = CreateContext())
                {
                    UserProfile dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new UserProfile();
                        context.UserProfile.Add(dbItem);
                        dbItem.CreatedDate = DateTime.Now;
                        dbItem.CreatedBy = dtoItem.UpdatedBy;
                    }
                    else
                    {
                        dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "User not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        converter.DTO2DB(dtoItem, ref dbItem);

                        // remove orphans
                        context.UserPermission.Local.Where(o => o.UserProfile == null).ToList().ForEach(o => context.UserPermission.Remove(o));
                        context.UserFactoryAccess.Local.Where(o => o.UserProfile == null).ToList().ForEach(o => context.UserFactoryAccess.Remove(o));
                        context.SaveChanges();

                        dbItem.UserUD = dbItem.UserId.ToString();
                        context.SaveChanges();

                        // processing image
                        if (dtoItem.PersonalPhoto_HasChange)
                        {
                            dbItem.PersonalPhoto = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.PersonalPhoto_NewFile, dtoItem.PersonalPhoto);
                        }
                        if (dtoItem.SignatureImage_HasChange)
                        {
                            dbItem.SignatureImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.SignatureImage_NewFile, dtoItem.SignatureImage);
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.UserId, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2627)
                {
                    switch (indexName)
                    {
                        case "UserUDUnique":
                            notification.Message = "Duplicate user code";
                            break;

                        default:
                            notification.Message = dEx.Message;
                            break;
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.UserMng.UserProfile dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.UserMng.UserProfile dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.UserMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.UserMng.SearchFilterData data = new DTO.UserMng.SearchFilterData();
            data.Offices = new List<DTO.Support.Office>();
            data.UserGroups = new List<DTO.Support.UserGroup>();
            data.YesNoValues = new List<DTO.Support.YesNo>();

            //try to get data
            try
            {
                data.Offices = supportFactory.GetOffice();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
                data.UserGroups = supportFactory.GetUserGroup();
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
