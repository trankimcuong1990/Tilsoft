using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoadingPlanMng : Lib.BLLBase2<DTO.LoadingPlanMng.SearchFormData, DTO.LoadingPlanMng.EditFormData,DTO.LoadingPlanMng.LoadingPlan>
    {
        private DAL.LoadingPlanMng.DataFactory factory;
        private Framework fwBLL = new Framework();
        public LoadingPlanMng(string tempFolder)
        {
            factory = new DAL.LoadingPlanMng.DataFactory(tempFolder);
        }

        public override DTO.LoadingPlanMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of loading plan");

            //assign userID
            filters["iRequesterID"] = iRequesterID;

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.LoadingPlanMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete booking: " + id.ToString());

            // query data
            return factory.DeleteData(iRequesterID, id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.LoadingPlanMng.LoadingPlan dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update loading plan: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.LoadingPlanMng.LoadingPlan dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.LoadingPlanMng.LoadingPlan dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.LoadingPlanMng.EditFormData GetData(int id, int factoryID, int bookingID, int parentLoadingPlanID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get loading plan: " + id.ToString());

            // query data
            return factory.GetData(iRequesterID, id, factoryID, bookingID, parentLoadingPlanID, out notification);
        }

        public DTO.LoadingPlanMng.InitFormData GetInitData(int iRequesterID , out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetInitData(iRequesterID, out notification);
        }
        
        public List<DTO.LoadingPlanMng.ProductSearchResult> QuicksearchProduct(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // query data
            return factory.QuicksearchProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        
        public List<DTO.LoadingPlanMng.SparepartSearchResult> QuicksearchSparepart(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // query data
            return factory.QuicksearchSparepart(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool SetLoadingPlanStatus(int id, int iRequesterID, int branchID, bool IsConfirmed, bool IsLoaded, bool IsShipped, out Library.DTO.Notification notification)
        {
            return factory.SetLoadingPlanStatus(branchID, id, iRequesterID, IsConfirmed, IsLoaded, IsShipped,out notification);
        }

        public IEnumerable<DTO.LoadingPlanMng.LoadingPlanSearchResult> QuickSearchLoadingPlan(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get list of loading plan");
            return factory.QuickSearchLoadingPlan(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string GetReportData(List<int> loadingPlanIDs, int print_type, int iRequesterID, out Library.DTO.Notification notification)
        {
            DAL.LoadingPlanMng.ReportDataFactory print_factory = new DAL.LoadingPlanMng.ReportDataFactory();
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get loading plan report");

            // query data
            return print_factory.GetReportData(loadingPlanIDs, print_type, iRequesterID, out notification);
        }

        public DTO.LoadingPlanMng.OverviewData GetViewData(int id, int factoryID, int bookingID, int parentLoadingPlanID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get loading plan: " + id.ToString());

            // query data
            return factory.GetViewData(iRequesterID, id, factoryID, bookingID, parentLoadingPlanID, out notification);
        }

        public DTO.LoadingPlanMng.LoadingPlanReportDTO GetReportHTML(int loadingPlanID, out Library.DTO.Notification notification)
        {
            DAL.LoadingPlanMng.ReportDataFactory report_factory = new DAL.LoadingPlanMng.ReportDataFactory();
            return report_factory.GetPrinOutHTML(loadingPlanID, out notification);
        }

        public List<DTO.LoadingPlanMng.SampleProductSearchResult> QuicksearchSampleProduct(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // query data
            return factory.QuicksearchSampleProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

    }
}
