using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProfileMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private ProfileMngEntities CreateContext()
        {
            return new ProfileMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ProfileMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.UserProfile();
            data.Locations = new List<Support.DTO.Location>();

            //try to get data
            try
            {
                using (ProfileMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_UserProfile(context.ProfileMng_UserProfile_View.FirstOrDefault(o => o.UserId == id));
                }
                data.Locations = supportFactory.GetLocation().ToList();
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
        public bool UpdateEmployee(int userId, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.UserProfile dtoProfile = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.UserProfile>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProfileMngEntities context = CreateContext())
                {
                    UserProfile dbProfile = context.UserProfile.FirstOrDefault(o => o.UserId == id);
                    if (dbProfile == null)
                    {
                        throw new Exception("Employee not found!");
                    }
                    Employee dbItem = context.Employee.FirstOrDefault(o => o.UserID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Employee not found!");
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    converter.DTO2DB(dtoProfile, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
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
                using (ProfileMngEntities context = CreateContext())
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

        public bool GetAPIKey(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProfileMngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == userId);
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
