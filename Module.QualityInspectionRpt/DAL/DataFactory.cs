using Library;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt.DAL
{
    internal class DataFactory
    {
        private readonly DataConverter converter = new DataConverter();

        public object GetDataSupport(Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            string handleTypeName = (filters.ContainsKey("typeName") && filters["typeName"] != null && !string.IsNullOrEmpty(filters["typeName"].ToString().Trim())) ? filters["typeName"].ToString().Trim() : null;

            DTO.SupportFormData data;

            try
            {
                switch (handleTypeName)
                {
                    case "QualityInspectionType":
                        GetQualityInspectionType(out data);
                        return data;

                    case "QualityInspectionCorrectAction":
                        GetQualityInspectionCorrectAction(out data);
                        return data;

                    case "QualityInspectionDefaultSetting":
                        GetQualityInspectionDefaultSetting(out data);
                        return data;

                    default:
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find match identifier";
                        return null;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return null;
            }
        }

        public object UpdateDataSupport(Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            string handleTypeName = (filters.ContainsKey("typeName") && filters["typeName"] != null && !string.IsNullOrEmpty(filters["typeName"].ToString().Trim())) ? filters["typeName"].ToString().Trim() : null;

            try
            {
                switch (handleTypeName)
                {
                    case "QualityInspectionType":
                        UpdateQualityInspectionType(filters, ref notification);
                        return GetDataSupport(filters, out notification);

                    case "QualityInspectionCorrectAction":
                        UpdateQualityInspectionCorrectAction(filters, ref notification);
                        return GetDataSupport(filters, out notification);

                    case "QualityInspectionDefaultSetting":
                        UpdateQualityInspectionDefaultSetting(filters, ref notification);
                        return GetDataSupport(filters, out notification);

                    default:
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not get type to handle";
                        return null;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return null;
            }
        }

        public object DeleteDataSupport(Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            string handleTypeName = (filters.ContainsKey("typeName") && filters["typeName"] != null && !string.IsNullOrEmpty(filters["typeName"].ToString().Trim())) ? filters["typeName"].ToString().Trim() : null;

            try
            {
                switch (handleTypeName)
                {
                    case "QualityInspectionType":
                        DeleteQualityInspectionType(filters, ref notification);
                        return GetDataSupport(filters, out notification);

                    case "QualityInspectionCorrectAction":
                        DeleteQualityInspectionCorrectAction(filters, ref notification);
                        return GetDataSupport(filters, out notification);

                    case "QualityInspectionDefaultSetting":
                        DeleteQualityInspectionDefaultSetting(filters, ref notification);
                        return GetDataSupport(filters, out notification);

                    default:
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not get type to handle";
                        return null;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return null;
            }
        }

        public object GetInitData(out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.EditFormData data = new DTO.EditFormData();
            data.SupportSupplier = new List<Support.DTO.FactoryRawMaterialData>();
            data.SupportQualityInspectionType = new List<DTO.QualityInspectionTypeData>();
            data.SupportQualityInspectionCorrectAction = new List<DTO.QualityInspectionCorrectActionData>();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.SupportSupplier = supportFactory.GetSubSupplier(out notification);

                using (var context = CreateContext())
                {
                    data.SupportQualityInspectionType = converter.DB2DTO_QualityInspectionTypes(context.QualityInspectionRpt_QualityInspectionType_View.ToList());
                    data.SupportQualityInspectionCorrectAction = converter.DB2DTO_QualityInspectionCorrectActions(context.QualityInspectionRpt_QualityInspectionCorrectAction_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetData(Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            string handlerName = (filters.ContainsKey("typeName") && filters["typeName"] != null && !string.IsNullOrEmpty(filters["typeName"].ToString().Trim())) ? filters["typeName"].ToString().Trim() : null;

            DTO.EditFormData data = new DTO.EditFormData()
            {
                EditData = new DTO.QualityInspectionData(),
                SearchData = new List<DTO.QualityInspectionSearchResultData>(),
                SupportWickerColor = new List<DTO.SupportWickerColorData>(),
            };

            try
            {
                switch (handlerName)
                {
                    case "QualityInspection":
                        GetData(filters, ref data, ref notification);
                        return data;

                    default:
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find match identifier";
                        return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public object UpdateData(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.EditFormData data = new DTO.EditFormData();
            data.EditData = new DTO.QualityInspectionData();
            data.SearchData = new List<DTO.QualityInspectionSearchResultData>();

            var dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.QualityInspectionData>();

            try
            {
                using (var context = CreateContext())
                {
                    QualityInspection dbItem;

                    if (dtoItem.QualityInspectionID == 0)
                    {
                        dbItem = new QualityInspection();
                        context.QualityInspection.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.QualityInspection.FirstOrDefault(o => o.QualityInspectionID == dtoItem.QualityInspectionID);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find Quality Inspection";

                        return GetData(filters, out notification);
                    }

                    converter.DTO2DB_QualityInspection(dtoItem, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userID.ToString() + @"\", userID);

                    context.SaveChanges();

                    filters["id"] = 0;
                    filters["workOrderID"] = dbItem.WorkOrderID;
                    filters["clientID"] = dbItem.ClientID;
                    filters["productionItemID"] = dbItem.ProductionItemID;

                    GetData(filters, ref data, ref notification);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object DeleteData(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.EditFormData data = new DTO.EditFormData();
            data.EditData = new DTO.QualityInspectionData();
            data.SearchData = new List<DTO.QualityInspectionSearchResultData>();

            try
            {
                int id = filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim()) ? (int)filters["id"] : 0;

                if (id == 0)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find data";

                    return data;
                }

                using (var context = CreateContext())
                {
                    var dbItem = context.QualityInspection.FirstOrDefault(o => o.QualityInspectionID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data";

                        return data;
                    }

                    context.QualityInspection.Remove(dbItem);
                    context.SaveChanges();
                }

                filters["id"] = 0;

                GetData(filters, ref data, ref notification);
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        private void GetQualityInspectionType(out DTO.SupportFormData data)
        {
            data = new DTO.SupportFormData()
            {
                TypeData = new DTO.QualityInspectionTypeData(),
                TypeList = new List<DTO.QualityInspectionTypeData>()
            };

            using (var context = CreateContext())
            {
                data.TypeList = converter.DB2DTO_QualityInspectionTypes(context.QualityInspectionRpt_QualityInspectionType_View.ToList());
            }
        }

        private void GetQualityInspectionCorrectAction(out DTO.SupportFormData data)
        {
            data = new DTO.SupportFormData()
            {
                CorrectActionData = new DTO.QualityInspectionCorrectActionData(),
                CorrectActionList = new List<DTO.QualityInspectionCorrectActionData>()
            };

            using (var context = CreateContext())
            {
                data.CorrectActionList = converter.DB2DTO_QualityInspectionCorrectActions(context.QualityInspectionRpt_QualityInspectionCorrectAction_View.ToList());
            }
        }

        private void GetQualityInspectionDefaultSetting(out DTO.SupportFormData data)
        {
            data = new DTO.SupportFormData()
            {
                SupportWorkCenter = new List<Support.DTO.WorkCenter>(),
                DefaultSettingData = new DTO.QualityInspectionDefaultSettingData(),
                DefaultSettingList = new List<DTO.QualityInspectionDefaultSettingData>()
            };

            using (var context = CreateContext())
            {
                data.DefaultSettingList = converter.DB2DTO_QualityInspectionDefaultSettings(context.QualityInspectionRpt_QualityInspectionDefaultSetting_View.ToList());
            }

            Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
            data.SupportWorkCenter = supportFactory.GetWorkCenter();
        }

        private void UpdateQualityInspectionType(Hashtable filters, ref Notification notification)
        {
            var dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.QualityInspectionTypeData>();

            using (var context = CreateContext())
            {
                QualityInspectionType dbItem;

                if (dtoItem.QualityInspectionTypeID == 0)
                {
                    dbItem = new QualityInspectionType();
                    context.QualityInspectionType.Add(dbItem);
                }
                else
                {
                    dbItem = context.QualityInspectionType.FirstOrDefault(o => o.QualityInspectionTypeID == dtoItem.QualityInspectionTypeID);
                }

                if (dbItem == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find Quality Inspection Type";
                }

                converter.DTO2DB_QualityInspectionType(dtoItem, ref dbItem);
                context.SaveChanges();
            }
        }

        private void UpdateQualityInspectionCorrectAction(Hashtable filters, ref Notification notification)
        {
            var dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.QualityInspectionCorrectActionData>();

            using (var context = CreateContext())
            {
                QualityInspectionCorrectAction dbItem;

                if (dtoItem.QualityInspectionCorrectActionID == 0)
                {
                    dbItem = new QualityInspectionCorrectAction();
                    context.QualityInspectionCorrectAction.Add(dbItem);
                }
                else
                {
                    dbItem = context.QualityInspectionCorrectAction.FirstOrDefault(o => o.QualityInspectionCorrectActionID == dtoItem.QualityInspectionCorrectActionID);
                }

                if (dbItem == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find Quality Inspection Correct Action";
                }

                converter.DTO2DB_QualityInspectionCorrectAction(dtoItem, ref dbItem);
                context.SaveChanges();
            }
        }

        private void UpdateQualityInspectionDefaultSetting(Hashtable filters, ref Notification notification)
        {
            var dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.QualityInspectionDefaultSettingData>();

            using (var context = CreateContext())
            {
                QualityInspectionDefaultSetting dbItem;

                if (dtoItem.QualityInspectionDefaultSettingID == 0)
                {
                    dbItem = new QualityInspectionDefaultSetting();
                    context.QualityInspectionDefaultSetting.Add(dbItem);
                }
                else
                {
                    dbItem = context.QualityInspectionDefaultSetting.FirstOrDefault(o => o.QualityInspectionDefaultSettingID == dtoItem.QualityInspectionDefaultSettingID);
                }

                if (dbItem == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find Quality Inspection Default Setting";
                }

                converter.DTO2DB_QualityInspectionDefaultSetting(dtoItem, ref dbItem);
                context.SaveChanges();
            }
        }

        private void DeleteQualityInspectionType(Hashtable filters, ref Notification notification)
        {
            var dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.QualityInspectionTypeData>();

            using (var context = CreateContext())
            {
                var dbItem = context.QualityInspectionType.FirstOrDefault(o => o.QualityInspectionTypeID == dtoItem.QualityInspectionTypeID);

                if (dbItem == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find Quality Inspection Type";
                }
                else
                {
                    context.QualityInspectionType.Remove(dbItem);
                    context.SaveChanges();
                }
            }
        }

        private void DeleteQualityInspectionCorrectAction(Hashtable filters, ref Notification notification)
        {
            var dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.QualityInspectionCorrectActionData>();

            using (var context = CreateContext())
            {
                var dbItem = context.QualityInspectionCorrectAction.FirstOrDefault(o => o.QualityInspectionCorrectActionID == dtoItem.QualityInspectionCorrectActionID);

                if (dbItem == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find Quality Inspection Correct Action";
                }
                else
                {
                    context.QualityInspectionCorrectAction.Remove(dbItem);
                    context.SaveChanges();
                }
            }
        }

        private void DeleteQualityInspectionDefaultSetting(Hashtable filters, ref Notification notification)
        {
            var dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.QualityInspectionDefaultSettingData>();

            using (var context = CreateContext())
            {
                var dbItem = context.QualityInspectionDefaultSetting.FirstOrDefault(o => o.QualityInspectionDefaultSettingID == dtoItem.QualityInspectionDefaultSettingID);

                if (dbItem == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find Quality Inspection Default Setting";
                }
                else
                {
                    context.QualityInspectionDefaultSetting.Remove(dbItem);
                    context.SaveChanges();
                }
            }
        }

        private void GetData(Hashtable filters, ref DTO.EditFormData data, ref Notification notification)
        {
            int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? (int)filters["id"] : 0;
            int? workOrderID = (filters.ContainsKey("workOrderID") && filters["workOrderID"] != null && !string.IsNullOrEmpty(filters["workOrderID"].ToString().Trim())) ? (int?)filters["workOrderID"] : null;
            int? clientID = (filters.ContainsKey("clientID") && filters["clientID"] != null && !string.IsNullOrEmpty(filters["clientID"].ToString().Trim())) ? (int?)filters["clientID"] : null;
            int? productionItemID = (filters.ContainsKey("productionItemID") && filters["productionItemID"] != null && !string.IsNullOrEmpty(filters["productionItemID"].ToString().Trim())) ? (int?)filters["productionItemID"] : null;
            string workCenterNM = (filters.ContainsKey("workCenterNM") && filters["workCenterNM"] != null && !string.IsNullOrEmpty(filters["workCenterNM"].ToString().Trim())) ? filters["workCenterNM"].ToString().Trim() : null;

            using (var context = CreateContext())
            {
                if (id == 0)
                {
                    data.EditData = converter.DB2DTO_InitQualityInspection(context.QualityInspectionRpt_function_GetInitQualityInspection(workOrderID, clientID, productionItemID).FirstOrDefault());
                    data.SearchData = converter.DB2DTO_QualityInspections(context.QualityInspectionRpt_function_GetQualityInspectionSearchResult(workOrderID, clientID, productionItemID).ToList());

                    data.EditData.QualityInspectionCategories = converter.DB2DTO_InitQualityInspectionCategories(context.QualityInspectionRpt_QualityInspectionDefaultSetting_View.ToList());
                }
                else
                {
                    data.EditData = converter.DB2DTO_QualityInspection(context.QualityInspectionRpt_QualityInspection_View.FirstOrDefault(o => o.QualityInspectionID == id));
                    data.SearchData = converter.DB2DTO_QualityInspections(context.QualityInspectionRpt_QualityInspectionSearchResult_View.Where(o => o.WorkOrderID == workOrderID && o.ClientID == clientID && o.ProductionItemID == productionItemID).ToList());
                }

                data.SupportWickerColor = converter.DB2DTO_SupportWickerColor(context.QualityInspectionRpt_SupportWickerColor_View.ToList());
            }
        }

        protected QualityInspectionRptEntities CreateContext()
        {
            return new QualityInspectionRptEntities(Helper.CreateEntityConnectionString("DAL.QualityInspectionRptModel"));
        }
    }
}
