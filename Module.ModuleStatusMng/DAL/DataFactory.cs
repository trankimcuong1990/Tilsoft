using Library;
using Library.DTO;
using Module.ModuleStatusMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ModuleStatusMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private ModuleStatusMngEntities CreateContext()
        {
            return new ModuleStatusMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ModuleStatusMngModel"));
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                //int? companyID = null;
                string statusUD = null;
                string statusNM = null;
                int? moduleID = null;
                bool? isActived = null;

                if (filters.ContainsKey("statusUD") && !string.IsNullOrEmpty(filters["statusUD"].ToString()))
                {
                    statusUD = filters["statusUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("statusNM") && !string.IsNullOrEmpty(filters["statusNM"].ToString()))
                {
                    statusNM = filters["statusNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("moduleID") && !string.IsNullOrEmpty(filters["moduleID"].ToString()))
                {
                    moduleID = Convert.ToInt32(filters["moduleID"].ToString());
                }
                if (filters.ContainsKey("isActived") && filters["isActived"] != null && !string.IsNullOrEmpty(filters["isActived"].ToString()))
                {
                    isActived = (filters["isActived"].ToString() == "true") ? true : false;
                }
                using (ModuleStatusMngEntities context = CreateContext())
                {
                    totalRows = context.ModuleStatusMng_function_SearchModuleStatus(statusUD, moduleID, statusNM, isActived, orderBy, orderDirection).Count();
                    var result = context.ModuleStatusMng_function_SearchModuleStatus(statusUD, moduleID, statusNM, isActived, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }

                return searchFormData;
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
                return searchFormData;
            }
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new ModuleStatusDTO();

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.ModuleStatusMng_ModuleStatus_View.FirstOrDefault(o => o.ModuleStatusID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "không thể tìm thấy dữ liệu!";
                            return data;
                        }

                        data.Data = converter.DB2DTO_ModuleStatus(dbItem);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();          
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification()
            {
                Type = Library.DTO.NotificationType.Success
            };

            DTO.ModuleStatusDTO dataItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ModuleStatusDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    ModuleStatus dbItem;

                    if (id > 0)
                    {
                        dbItem = context.ModuleStatus.FirstOrDefault(s => s.ModuleStatusID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Couldn't find this module status";
                            return false;
                        }
                    }
                    else
                    {
                        var listItem = context.ModuleStatus.Where(s => s.ModuleID == dataItem.ModuleID).ToList();
                        if (listItem != null && listItem.Count != 0)
                        {
                            int? x = 0;
                            for (int i = 0; i < listItem.Count; i++)
                            {
                                var item = listItem[i];
                                if (item.StatusID > x)
                                {
                                    x = item.StatusID;
                                }
                            }
                            dataItem.StatusID = x + 1;
                        }
                        else
                        {
                            dataItem.StatusID = 1;
                        }

                        var listA = context.ModuleStatus.Where(s => s.ModuleID == dataItem.ModuleID && s.StatusUD == dataItem.StatusUD).ToList();
                        dbItem = listA.FirstOrDefault();
                        if (dbItem != null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "This Module status is existed" + dbItem.StatusNM;
                            return false;
                        }

                        dbItem = new ModuleStatus();
                        context.ModuleStatus.Add(dbItem);
                    }
                    converter.DTO2DB_Update(dataItem, ref dbItem);
                    context.SaveChanges();
                    dtoItem = GetData(dbItem.ModuleStatusID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
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

        //
        // CUSTOM FUNCTION HERE
        //

        public SupportFormData GetInitData(out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            SupportFormData data = new SupportFormData();

            try
            {
                using (ModuleStatusMngEntities context = CreateContext())
                {
                    data.Modules = converter.DB2DTO_Modules(context.ModuleStatusMng_Module_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
