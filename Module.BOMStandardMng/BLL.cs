using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.BOMStandardMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public BLL(string tempFolder)
        {
            factory = new DAL.DataFactory(tempFolder);
        }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get BOM list");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete BOM" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update BOM" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get BOM Standard" + id.ToString());

            return factory.GetData(iRequesterID, id, param, out notification);
        }

        public DTO.ViewFormData GetViewData(int iRequesterID, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get BOM Standard" + id.ToString());

            return factory.GetViewData(iRequesterID, id, param, out notification);
        }

        public List<DTO.ProductionItemDTO> GetListProductionItem(int iRequesterID, object bomImportData, out Library.DTO.Notification notification)
        {
            return factory.GetListProductionItem(iRequesterID, bomImportData, out notification);
        }

        public string CreateImportTemplate(int iRequesterID, int productionProcessID, out Library.DTO.Notification notification)
        {
            return factory.CreateImportTemplate(productionProcessID, out notification);
        }

        public DTO.ImportTemplate.ImportTemplateData CreateBOMTemplateData(int iRequesterID, int productionProcessID, int? copyFromproductionProcessID, out Library.DTO.Notification notification)
        {
            return factory.CreateBOMTemplateData(iRequesterID, productionProcessID, copyFromproductionProcessID, out notification);
        }

        public bool SetBOMStandardDefault(int productionProcessID, out Library.DTO.Notification notification)
        {
            return factory.SetBOMStandardDefault(productionProcessID, out notification);
        }

        public List<DTO.ProductionProcessSearchDTO> GetProductionProcessByModel(int? modelID, out Library.DTO.Notification notification)
        {
            return factory.GetProductionProcessByModel(modelID, out notification);
        }

        public DTO.EditProductionProcessFormData GetProductionProcessData(int userId, int productionProcessID, out Library.DTO.Notification notification)
        {
            return factory.GetProductionProcessData(userId, productionProcessID, out notification);
        }
        public int UpdateProductionProcess(int userId, int productionProcessID, ref object dtoData, out Library.DTO.Notification notification)
        {
            return factory.UpdateProductionProcess(userId, productionProcessID, dtoData, out  notification);
        }
        public DTO.SearchProductionProcessFormData SearchProductionProcess(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.SearchProductionProcess(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string ExportBOMStandardToExcel(int iRequesterID, int bomStandardID, out Library.DTO.Notification notification)
        {
            return factory.ExportBOMStandardToExcel(iRequesterID, bomStandardID, out notification);
        }
        public DTO.EditFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public object GetWorkOrderByBOMStandardID(int userID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetWorkOrderByBOMStandardID(id, out notification);
        }

        public object GetWorkOrderApplyByBOMStandardID(int userID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetWorkOrderApplyByBOMStandardID(id, out notification);
        }

        public object GetWorkCenterByBOMStandardID(int userID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetWorkCenterByBOMStandardID(id, out notification);
        }

        public object ApplyBOMStandard(int userID, int id, string workOrderIDs, string workCenterIDs, int applyTypeID, out Library.DTO.Notification notification)
        {
            return factory.UpdateWorkOrderApply(userID, id, workOrderIDs, workCenterIDs, applyTypeID, out notification);
        }
    }
}
