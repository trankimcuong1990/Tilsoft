using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SystemSettingMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.SystemSettingMng.SearchFormData, DTO.SystemSettingMng.EditFormData, DTO.SystemSettingMng.SystemSetting>
    {
        private DataConverter converter = new DataConverter();
        private SystemSettingMngEntities CreateContext()
        {
            return new SystemSettingMngEntities(DALBase.Helper.CreateEntityConnectionString("SystemSettingMngModel"));
        }

        public override DTO.SystemSettingMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SystemSettingMng.SearchFormData data = new DTO.SystemSettingMng.SearchFormData();
            data.Data = new List<DTO.SystemSettingMng.SystemSettingSearchResult>();
            totalRows = 0;

           
            string SettingKey = null;
            string SettingValue = null;
            if (filters.ContainsKey("SettingKey") && !string.IsNullOrEmpty(filters["SettingKey"].ToString()))
            {
                SettingKey = filters["SettingKey"].ToString().Replace("'", "''");
            }
            
            if (filters.ContainsKey("SettingValue") && !string.IsNullOrEmpty(filters["SettingValue"].ToString()))
            {
                SettingKey = filters["SettingValue"].ToString().Replace("'", "''");
            }
         



            //try to get data
            try
            {
                using (SystemSettingMngEntities context = CreateContext())

                {
                    totalRows = context.SystemSettingMng_function_SearchSystemSetting(SettingValue,SettingKey,orderBy,orderDirection).Count();
                    var result = context.SystemSettingMng_function_SearchSystemSetting(SettingValue, SettingKey, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SystemSettingSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.SystemSettingMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SystemSettingMng.EditFormData data = new DTO.SystemSettingMng.EditFormData();
            data.Data = new DTO.SystemSettingMng.SystemSetting();

            //try to get data
            try
            {
                using (SystemSettingMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_SystemSetting(context.FW_SystemSetting_View.FirstOrDefault(o => o.SystemSettingID == id));
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
                using (SystemSettingMngEntities context = CreateContext())
                {
                    SystemSetting dbItem = context.SystemSettings.FirstOrDefault(o => o.SystemSettingID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "SystemSetting not found!";
                        return false;
                    }
                    else
                    {
                        context.SystemSettings.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.SystemSettingMng.SystemSetting dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (SystemSettingMngEntities context = CreateContext())
                {
                    SystemSetting dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new SystemSetting();
                        context.SystemSettings.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SystemSettings.FirstOrDefault(o => o.SystemSettingID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "System Setting not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_SystemSetting(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.SystemSettingID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if (indexName == "SettingKeyUnique")
                    {
                        notification.Message = "Setting Key already exists!";
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

        public override bool Approve(int id, ref DTO.SystemSettingMng.SystemSetting dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SystemSettingMng.SystemSetting dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
 