using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.OnlineFileMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private OnlineFileMngEntities CreateContext()
        {
            return new OnlineFileMngEntities(Library.Helper.CreateEntityConnectionString("DAL.OnlineFileMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData()
            {
                Data = new List<DTO.OnlineFile>(),
                Users = new List<DTO.User2>()
            };            
            totalRows = 0;

            //try to get data
            try
            {
                using (OnlineFileMngEntities context = CreateContext())
                {
                    int UserID = -1;
                    if (filters.ContainsKey("UserID") && filters["UserID"] != null && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    var ownedFiles = context.OnlineFileMng_OnlineFile_View
                        .Include("OnlineFileMng_OnlineFilePermission_View")
                        .Where(o=>o.UpdatedBy == UserID);

                    int?[] sharedIDs = context.OnlineFilePermission.Where(o => o.UserID == UserID).Select(o => o.OnlineFileID).ToArray();
                    var sharedFiles = context.OnlineFileMng_OnlineFile_View
                        .Include("OnlineFileMng_OnlineFilePermission_View")
                        .Where(o => sharedIDs.Contains(o.OnlineFileID));

                    var result = ownedFiles.Union(sharedFiles);
                    data.Data = converter.DB2DTO_OnlineFileList(result.ToList());
                    data.Users = converter.DB2DTO_OnlineFileUserList(context.OnLineFileMng_User2_View.ToList());
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
                using (OnlineFileMngEntities context = CreateContext())
                {
                    // check if can delete
                    OnlineFile dbItem = context.OnlineFile.FirstOrDefault(o => o.OnlineFileID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("File not found");
                    }

                    // everything ok, delete
                    // remove attached file
                    if (!string.IsNullOrEmpty(dbItem.FileUD))
                    {
                        fwFactory.RemoveFile(dbItem.FileUD);
                    }
                    context.OnlineFile.Remove(dbItem);
                    context.OnlineFilePermission.Local.Where(o => o.OnlineFile == null).ToList().ForEach(o => context.OnlineFilePermission.Remove(o));
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
            DTO.OnlineFile dtoFile = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OnlineFile>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OnlineFileMngEntities context = CreateContext())
                {
                    OnlineFile dbItem = context.OnlineFile.FirstOrDefault(o => o.OnlineFileID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "File not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        converter.DTO2DB(dtoFile, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
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
        public bool UploadFile(int userId, object data, out Library.DTO.Notification notification)
        {
            List<DTO.OnlineFile> files = ((Newtonsoft.Json.Linq.JArray)data).ToObject<List<DTO.OnlineFile>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OnlineFileMngEntities context = CreateContext())
                {
                    foreach (DTO.OnlineFile dtoFile in files)
                    {
                        OnlineFile dbFile = new OnlineFile();
                        context.OnlineFile.Add(dbFile);
                        dbFile.UpdatedBy = userId;
                        dbFile.UpdatedDate = DateTime.Now;
                        converter.DTO2DB(dtoFile, ref dbFile, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                    }
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }
    }
}
