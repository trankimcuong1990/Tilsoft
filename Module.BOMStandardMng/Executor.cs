using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.SearchProductionProcess(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int productionProcessID;
            int modelID;
            int bomStandardID;
            string workOrderIDs = string.Empty;
            string workCenterIDs = string.Empty;
            int id = 0;
            int applyTypeID = 0;

            switch (identifier.Trim())
            {
                case "GetData":
                    id = Convert.ToInt32(filters["id"]);
                    return bll.GetData(userId, id, filters, out notification);

                case "GetViewData":
                    int viewId = Convert.ToInt32(filters["id"]);
                    return bll.GetViewData(userId, viewId, filters, out notification);

                case "GetListProductionItem":
                    object bomImportData = filters["bomImportData"];
                    return bll.GetListProductionItem(userId, bomImportData, out notification);

                case "CreateImportTemplate":
                    productionProcessID = Convert.ToInt32(filters["productionProcessID"]);
                    return bll.CreateImportTemplate(userId, productionProcessID, out notification);

                case "CreateBOMTemplateData":
                    productionProcessID = Convert.ToInt32(filters["productionProcessID"]);
                    int? copyFromproductionProcessID = null;
                    if (filters["copyFromproductionProcessID"] != null)
                    {
                        copyFromproductionProcessID = Convert.ToInt32(filters["copyFromproductionProcessID"]);
                    }
                    return bll.CreateBOMTemplateData(userId, productionProcessID, copyFromproductionProcessID, out notification);

                case "SetBOMStandardDefault":
                    productionProcessID = Convert.ToInt32(filters["productionProcessID"]);
                    return bll.SetBOMStandardDefault(productionProcessID, out notification);

                case "GetProductionProcessByModel":
                    modelID = Convert.ToInt32(filters["modelID"]);
                    return bll.GetProductionProcessByModel(modelID, out notification);
                
                case "GetProductionProcessData":
                    productionProcessID = Convert.ToInt32(filters["productionProcessID"]);
                    return bll.GetProductionProcessData(userId, productionProcessID, out notification);

                case "UpdateProductionProcess":
                    productionProcessID = Convert.ToInt32(filters["productionProcessID"]);
                    object dtoData = filters["dtoData"];
                    return bll.UpdateProductionProcess(userId, productionProcessID, ref dtoData, out notification);

                case "ExportBOMStandardToExcel":
                    bomStandardID = Convert.ToInt32(filters["bomStandardID"]);
                    return bll.ExportBOMStandardToExcel(userId, bomStandardID, out notification);

                case "GetWorkOrderByBOMStandardID":
                    bomStandardID = Convert.ToInt32(filters["bomStandardID"]);
                    return bll.GetWorkOrderByBOMStandardID(userId, bomStandardID, out notification);

                case "GetWorkOrderApplyByBOMStandardID":
                    bomStandardID = Convert.ToInt32(filters["bomStandardID"]);
                    return bll.GetWorkOrderApplyByBOMStandardID(userId, bomStandardID, out notification);

                case "GetWorkCenterByBOMStandardID":
                    bomStandardID = Convert.ToInt32(filters["bomStandardID"]);
                    return bll.GetWorkCenterByBOMStandardID(userId, bomStandardID, out notification);

                case "applyBOMStandard":
                    if (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString()))
                    {
                        id = Convert.ToInt32(filters["id"].ToString());
                    }

                    if (filters.ContainsKey("workOrderIDs") && filters["workOrderIDs"] != null && !string.IsNullOrEmpty(filters["workOrderIDs"].ToString()))
                    {
                        workOrderIDs = filters["workOrderIDs"].ToString().Trim();
                    }

                    if (filters.ContainsKey("workCenterIDs") && filters["workCenterIDs"] != null && !string.IsNullOrEmpty(filters["workCenterIDs"].ToString()))
                    {
                        workCenterIDs = filters["workCenterIDs"].ToString().Trim();
                    }

                    if (filters.ContainsKey("applyTypeID") && filters["applyTypeID"] != null && !string.IsNullOrEmpty(filters["applyTypeID"].ToString()))
                    {
                        applyTypeID = Convert.ToInt32(filters["applyTypeID"].ToString());
                    }

                    return bll.ApplyBOMStandard(userId, id, workOrderIDs, workCenterIDs, applyTypeID, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
