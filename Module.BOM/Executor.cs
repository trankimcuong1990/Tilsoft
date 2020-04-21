using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOM
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
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
            int workOrderID;
            int modelID;
            int productionItemID;
            switch (identifier.Trim())
            {
                case "GetData":
                    int id = Convert.ToInt32(filters["id"]);
                    int? woID = null;
                    if (filters["workOrderID"] != null)
                    {
                        woID = Convert.ToInt32(filters["workOrderID"]);
                    }
                    return bll.GetData(userId, id, woID, out notification);

                case "GetProductionItem":
                    productionItemID = Convert.ToInt32(filters["productionItemID"]);
                    return bll.GetProductionItem(userId, productionItemID, out notification);

                case "UpdateProductionItem":
                    productionItemID = Convert.ToInt32(filters["productionItemID"]);
                    object objdtoItem = filters["dtoItem"];

                    string tempFolder = this.identifier;
                    bll = new BLL(tempFolder);
                    return bll.UpdateProductionItem(userId, productionItemID, ref objdtoItem, out notification);

                case "GetListProductionItem":
                    object bomImportData = filters["bomImportData"];
                    return bll.GetListProductionItem(userId, bomImportData, out notification);

                case "CreateImportTemplateGeneral":
                    workOrderID = Convert.ToInt32(filters["workOrderID"]);
                    return bll.CreateImportTemplateGeneral(userId, workOrderID, out notification);

                case "CreateImportTemplate":
                    workOrderID = Convert.ToInt32(filters["workOrderID"]);
                    return bll.CreateImportTemplate(userId, workOrderID, out notification);

                case "CreateBOMTemplateData":
                    workOrderID = Convert.ToInt32(filters["workOrderID"]);
                    int? copyFromProductionProcessID = null;

                    if (filters["copyFromProductionProcessID"] != null)
                    {
                        copyFromProductionProcessID = Convert.ToInt32(filters["copyFromProductionProcessID"]);
                    }
                    return bll.CreateBOMTemplateData(userId, workOrderID, copyFromProductionProcessID, out notification);

                case "SetDefaultWorkOrder":
                    workOrderID = Convert.ToInt32(filters["workOrderID"]);
                    return bll.SetDefaultWorkOrder(workOrderID, out notification);

                case "GetProductionProcessFormData":
                    modelID = Convert.ToInt32(filters["modelID"]);
                    return bll.GetProductionProcessFormData(modelID, out notification);

                case "ExportBOMToExcel":
                    int bomID = Convert.ToInt32(filters["bomID"]);
                    return bll.ExportBOMToExcel(userId, bomID, filters, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
