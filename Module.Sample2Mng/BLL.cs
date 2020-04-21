using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get sample list");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get sample order");
            return factory.GetData(id, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update sample order");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.EditFormData GetDataDetail(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get sample order");
            return factory.GetDataDetail(id, out notification);
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public bool UpdateOrderStatus(int iRequesterID, int id, int statusId, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update order status");
            return factory.UpdateOrderStatus(iRequesterID, id, statusId, out notification);
        }

        public bool UpdateProductStatus(int iRequesterID, int id, int statusId, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update product status");
            return factory.UpdateProductStatus(iRequesterID, id, statusId, out notification);
        }

        public bool UpdateProductStatus2(int iRequesterID, int id, int statusId, string file, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update product status");
            return factory.UpdateProductStatus2(iRequesterID, id, statusId, file, out notification);
        }

        public bool UpdateProgressData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update progress");
            return factory.UpdateProgressData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteProgress(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete progress");
            return factory.DeleteProgress(id, out notification);
        }

        public bool UpdateTechnicalDrawingData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update technical drawing");
            return factory.UpdateTechnicalDrawingData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool ApproveTechnicalDrawingData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "approve technical drawing");
            return factory.ApproveTechnicalDrawingData(iRequesterID, id, out notification);
        }

        public bool ResetTechnicalDrawingData(int iRequesterID, int id,out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "reset technical drawing");
            return factory.ResetTechnicalDrawingData(iRequesterID, id, out notification);
        }

        public bool DeleteTechnicalDrawing(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete technical drawing");
            return factory.DeleteTechnicalDrawing(id, out notification);
        }
        
        public bool UpdateRemarkData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update remark");
            return factory.UpdateRemarkData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteRemark(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete remark");
            return factory.DeleteRemark(id, out notification);
        }

        public bool UpdateQARemarkData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateQARemarkData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteQARemark(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteQARemark(id, out notification);
        }

        public bool UpdateItemInfo(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update item info");
            return factory.UpdateItemInfo(iRequesterID, id, ref dtoItem, out notification);
        }

        public object GetModelList(int userId, string param, out Notification notification)
        {
            return factory.GetModelList(param, out notification);
        }

        public object GetDataDetailFactoryBreakdown (int iRequesterID, int sampleProductID, out Notification notification)
        {
            return factory.GetDataDetailFactoryBreakdown(sampleProductID, out notification);
        }

        public string ExportExcelSampleOrder(int iRequesterID, string sampleOrderUD, string season, string clientUD, string clientNM, int? purposeID, int? transportTypeID, int? sampleOrderStatusID, int? factoryID, string sampleItemCode, string sampleItemName, int? accountManager, out Notification notification)
        {
            return factory.ExportExcelSampleOrder( sampleOrderUD,  season,  clientUD,  clientNM,   purposeID,  transportTypeID, sampleOrderStatusID, factoryID, sampleItemCode, sampleItemName, accountManager, out notification);
        }
    }
}
