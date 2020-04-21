using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.QCReportDefaultSettingMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        DataConverter converter = new DataConverter();

        QCReportDefaultSettingMngEntities CreateContext()
        {
            return new QCReportDefaultSettingMngEntities(Library.Helper.CreateEntityConnectionString("DAL.QCReportDefaultSettingMngModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using(var context = CreateContext())
                {
                    var dbItem = context.QCReportDefaultSetting.Where(o => o.QCReportDefaultSettingID == id).FirstOrDefault();
                    context.QCReportDefaultSetting.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if(ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        public override DTO.EditFormData GetData(int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.QCReportDefaultSettingDTO();

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.QCReportDefaultSettingMng_QCReportDefaultSetting_View.FirstOrDefault(o => o.QCReportDefaultSettingID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return data;
                        }

                        data.Data = converter.DB2DTO_QCReportDefaultSetting(dbItem);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification { Type = NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.QCReportDefaultSettingSearchResultDTO>();

            string QCReportSection = null;
            string Description = null;

            if(filters.ContainsKey("QCReportSection") && !string.IsNullOrEmpty(filters["QCReportSection"].ToString()))
            {
                QCReportSection = filters["QCReportSection"].ToString().Replace("'", "''");
            }
            if(filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            try
            {
                using (var context = CreateContext())
                {
                    totalRows = context.QCReportDefaultSettingMng_Function_SearchResult(QCReportSection, Description, orderBy, orderDirection).Count();
                    var result = context.QCReportDefaultSettingMng_Function_SearchResult(QCReportSection, Description, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SearchQCReportDefaultSetting(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch(Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object objItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.QCReportDefaultSettingDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)objItem).ToObject<DTO.QCReportDefaultSettingDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    QCReportDefaultSetting dbItem;

                    if (id == 0)
                    {
                        dbItem = new QCReportDefaultSetting();
                        context.QCReportDefaultSetting.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.QCReportDefaultSetting.FirstOrDefault(o => o.QCReportDefaultSettingID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return false;
                        }
                    }

                    converter.DTO2DB_QCReportDefaultSetting(dtoItem, ref dbItem);
                    context.SaveChanges();

                    objItem = GetData(dbItem.QCReportDefaultSettingID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return true;
        }
    }
}
