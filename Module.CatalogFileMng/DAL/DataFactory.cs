using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.CatalogFileMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private CatalogFileMngEntities CreateContext()
        {
            return new CatalogFileMngEntities(Library.Helper.CreateEntityConnectionString("DAL.CatalogFileMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.CatalogFileSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (CatalogFileMngEntities context = CreateContext())
                {
                    string Season = null;
                    string FriendlyName = null;
                    string PLFriendlyName = null;
                    string UpdatorName = null;
                    if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FriendlyName") && !string.IsNullOrEmpty(filters["FriendlyName"].ToString()))
                    {
                        FriendlyName = filters["FriendlyName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("PLFriendlyName") && !string.IsNullOrEmpty(filters["PLFriendlyName"].ToString()))
                    {
                        PLFriendlyName = filters["PLFriendlyName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("UpdatorName") && !string.IsNullOrEmpty(filters["UpdatorName"].ToString()))
                    {
                        UpdatorName = filters["UpdatorName"].ToString().Replace("'", "''");
                    }
                    totalRows = context.CatalogFileMng_function_SearchCatalogFile(Season, FriendlyName, PLFriendlyName, UpdatorName, orderBy, orderDirection).Count();
                    var result = context.CatalogFileMng_function_SearchCatalogFile(Season, FriendlyName, PLFriendlyName, UpdatorName, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_CatalogFileSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.CatalogFileDTO();
            data.Seasons = new List<Support.DTO.Season>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (CatalogFileMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_CatalogFile(context.CatalogFileMng_CatalogFile_View.FirstOrDefault(o => o.CatalogFileID == id));
                    }
                }                

                data.Seasons = supportFactory.GetSeason().ToList();
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
                using (CatalogFileMngEntities context = CreateContext())
                {
                    CatalogFile dbItem = context.CatalogFile.FirstOrDefault(o => o.CatalogFileID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Catalog file not found!");
                    }

                    // remove file
                    if(!string.IsNullOrEmpty(dbItem.FileUD))
                    {
                        fwFactory.RemoveFile(dbItem.FileUD);
                    }
                    context.CatalogFile.Remove(dbItem);
                    context.SaveChanges();
                }                
            }
            catch(Exception ex) 
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.CatalogFileDTO dtoCatalogFile = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.CatalogFileDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (CatalogFileMngEntities context = CreateContext())
                {
                    CatalogFile dbItem = context.CatalogFile.FirstOrDefault(o => o.CatalogFileID == id);
                    if (id > 0)
                    {
                        dbItem = context.CatalogFile.FirstOrDefault(o => o.CatalogFileID == id);
                    }
                    else
                    {
                        dbItem = new CatalogFile();
                        context.CatalogFile.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        throw new Exception("Catalog file not found!");
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    converter.DTO2DB(dtoCatalogFile, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                    context.SaveChanges();
                    dtoItem = this.GetData(dbItem.CatalogFileID, out notification).Data;
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
            data.Seasons = new List<Support.DTO.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
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
