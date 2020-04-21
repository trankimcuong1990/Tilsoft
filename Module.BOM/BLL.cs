using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOM
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

        public DTO.EditFormData GetData(int iRequesterID, int id, int? workOrderID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get BOM" + id.ToString());

            return factory.GetData(iRequesterID, id, workOrderID, out notification);
        }

        public DTO.ProductionItemDTO GetProductionItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetProductionItem(iRequesterID, id, out notification);
        }
        public DTO.ProductionItemDTO UpdateProductionItem(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateProductionItem(iRequesterID, id, ref dtoItem, out notification);
        }

        public List<DTO.ProductionItemDTO> GetListProductionItem(int iRequesterID, object bomImportData, out Library.DTO.Notification notification)
        {
            return factory.GetListProductionItem(iRequesterID, bomImportData, out notification);
        }

        public string CreateImportTemplateGeneral(int iRequesterID, int workOrderID, out Library.DTO.Notification notification)
        {
            return factory.CreateImportTemplateGeneral(workOrderID, out notification);
        }

        public string CreateImportTemplate(int iRequesterID, int workOrderID, out Library.DTO.Notification notification)
        {
            return factory.CreateImportTemplate(workOrderID, out notification);
        }

        public DTO.ImportTemplate.ImportTemplateData CreateBOMTemplateData(int iRequesterID, int workOrderID, int? copyFromProductionProcessID, out Library.DTO.Notification notification)
        {
            return factory.CreateBOMTemplateData(iRequesterID, workOrderID, copyFromProductionProcessID, out notification);
        }

        public bool SetDefaultWorkOrder(int workOrderID, out Library.DTO.Notification notification)
        {
            return factory.SetDefaultWorkOrder(workOrderID, out notification);
        }

        public DTO.WorkOrderAndProductionProcessForm.ProductionProcessFormData GetProductionProcessFormData(int? modelID, out Library.DTO.Notification notification)
        {
            return factory.GetProductionProcessFormData(modelID, out notification);
        }
        public DTO.EditFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
        public string ExportBOMToExcel(int iRequesterID, int bomID, Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.ExportBOMToExcel(iRequesterID, bomID, param, out notification);
        }
    }
}
