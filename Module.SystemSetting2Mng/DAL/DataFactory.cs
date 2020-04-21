using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SystemSetting2Mng.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();

        public SystemSetting2MngEntities CreateContext()
        {
            return new SystemSetting2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.SystemSetting2MngModel"));
        }

        public DTO.InitForm GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitForm initForm = new DTO.InitForm();
            initForm.Season = new List<Support.DTO.Season>();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

                initForm.Season = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return initForm;
        }
        public List<DTO.SystemSetting2> CoppySeassions(string Seassion, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            List<DTO.SystemSetting2> data = new List<DTO.SystemSetting2>();

            try
            {
                // Pre data param to get data ProductionItem
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                //int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    context.FW_function_ApplySystemSettingFromLastSeason(Seassion);
                    var dbItem = context.SystemSetting2Mng_SystemSetting_View.ToList();
                    data = AutoMapper.Mapper.Map<List<SystemSetting2Mng_SystemSetting_View>, List<DTO.SystemSetting2>>(dbItem.ToList());
                }

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.SearchForm GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm searchForm = new DTO.SearchForm();
            searchForm.SystemSetting = new List<DTO.SystemSetting2>();

            try
            {
                using (var context = CreateContext())
                {
                    string settingKey = filters.ContainsKey("settingKey") && filters["settingKey"] != null && !string.IsNullOrEmpty(filters["settingKey"].ToString().Replace("'", "''")) ? filters["settingKey"].ToString().Trim() : null;
                    string settingValue = filters.ContainsKey("settingValue") && filters["settingValue"] != null && !string.IsNullOrEmpty(filters["settingValue"].ToString().Replace("'", "''")) ? filters["settingValue"].ToString().Trim() : null;
                    string season = filters.ContainsKey("season") && filters["season"] != null && !string.IsNullOrEmpty(filters["season"].ToString().Replace("'", "''")) ? filters["season"].ToString().Trim() : null;

                    totalRows = context.SystemSetting2Mng_function_SystemSettingSearchResult(season, orderBy, orderDirection).Count();
                    searchForm.SystemSetting = converter.DB2DTO_SystemSettingSearchResult(context.SystemSetting2Mng_function_SystemSettingSearchResult(season, orderBy, orderDirection).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return searchForm;
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SystemSetting2 systemSettingDto = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SystemSetting2>();
            bool resultUpdate = false;

            // Check setting key is null.
            if (string.IsNullOrEmpty(systemSettingDto.SettingKey.Trim()))
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Please input Setting Key!";

                return resultUpdate;
            }

            // Check season is null.
            if (string.IsNullOrEmpty(systemSettingDto.Season.Trim()))
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Please select Season!";

                return resultUpdate;
            }

            try
            {
                using (var context = CreateContext())
                {
                    SystemSetting systemSetting = null;

                    if (id < 0)
                    {
                        // Check setting key exist.
                        systemSetting = context.SystemSetting.FirstOrDefault(o => o.SettingKey == systemSettingDto.SettingKey && o.Season == systemSettingDto.Season);
                        if (systemSetting != null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "System Setting already in database!";

                            return resultUpdate;
                        }
                        else
                        {
                            systemSetting = new SystemSetting();
                        }

                        context.SystemSetting.Add(systemSetting);
                    }
                    else
                    {
                        systemSetting = context.SystemSetting.FirstOrDefault(o => o.SystemSettingID == id);

                        if (systemSetting == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Cannot found data!";

                            return resultUpdate;
                        }
                    }

                    converter.DTO2DB_UpdateData(systemSettingDto, ref systemSetting);

                    systemSetting.SettingKey = systemSettingDto.SettingKey;
                    systemSetting.SettingValue = systemSettingDto.SettingValue;

                    context.SaveChanges();

                    dtoItem = converter.DB2DTO_SystemSetting(context.SystemSetting2Mng_SystemSetting_View.FirstOrDefault(o => o.SystemSettingID == systemSetting.SystemSettingID));

                    resultUpdate = true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return resultUpdate;
        }

        public bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            bool resultDelete = false;

            try
            {
                using (var context = CreateContext())
                {
                    SystemSetting systemSetting = context.SystemSetting.FirstOrDefault(o => o.SystemSettingID == id);

                    if (systemSetting == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Cannot found System Setting data!";

                        return resultDelete;
                    }

                    context.SystemSetting.Remove(systemSetting);
                    context.SaveChanges();

                    resultDelete = true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return resultDelete;
        }
    }
}
