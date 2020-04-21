using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        //
        // SAMPLE ORDER
        //
        public List<DTO.SampleOrderSearchResultDTO> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.SupportData GetOrderSearchFilter(out Library.DTO.Notification notification)
        {
            return factory.GetOrderSearchFilter(out notification);
        }
        public DTO.OrderEditFormData GetOrderData(int id, int clientId, string season, out Library.DTO.Notification notification)
        {
            return factory.GetOrderData(id, clientId, season, out notification);
        }
        public bool UpdateOrderData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateOrderData(userId, id, ref dtoItem, out notification);
        }
        public DTO.SampleOrderOverview.OrderDTO GetOrderOverviewData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetOrderOverviewData(id, out notification);
        }

        //
        // SAMPLE PRODUCT
        //
        public DTO.SampleProductOverview.ProductDTO GetProductOverviewData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetProductOverviewData(id, out notification);
        }


        //
        // FACTORY ASSIGNMENT
        //
        public DTO.FactoryAssignment.FormData GetFactoryAssignmentData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetFactoryAssignmentData(id, out notification);
        }
        public bool UpdateFactoryAssignmentData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateFactoryAssignmentData(userId, id, ref dtoItem, out notification);
        }

        #region INTERNAL REMARK

        public DTO.InternalRemark.FormData GetInternalRemarkData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetInternalRemarkData(id, out notification);
        }

        public bool UpdateInternalRemarkData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateInternalRemarkData(userId, id, ref dtoItem, out notification);
        }

        public bool DeleteInternalRemarkData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteInternalRemarkData(userId, id, out notification);
        }
        #endregion

        #region QA REMARK

        public DTO.QARemark.FormData GetQARemarkData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetQARemarkData(id, out notification);
        }

        public bool UpdateQARemarkData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateQARemarkData(userId, id, ref dtoItem, out notification);
        }

        public bool DeleteQARemarkData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteQARemarkData(userId, id, out notification);
        }
        #endregion

        #region BUILDING PROCESS

        public DTO.BuildingProcess.FormData GetBuildingProcessData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetBuildingProcessData(id, out notification);
        }

        public bool UpdateBuildingProcessData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateBuildingProcessData(userId, id, ref dtoItem, out notification);
        }

        public bool DeleteBuildingProcessData(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteBuildingProcessData(userId, id, out notification);
        }
        #endregion

        #region ITEM DATA

        public DTO.ItemData.FormData GetItemData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetItemData(id, out notification);
        }
        public bool UpdateItemData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateItemData(userId, id, ref dtoItem, out notification);
        }
        #endregion

        #region PRODUCT INFO

        public DTO.ProductInfo.FormData GetProductInfoData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetProductInfoData(id, out notification);
        }
        public bool UpdateProductInfoData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateProductInfoData(userId, id, ref dtoItem, out notification);
        }
        #endregion


        //
        // OTHER
        //
        public List<DTO.MonitorUserDTO> QuickSearchAVTUsers(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchAVTUsers(query, out notification);
        }
        public List<DTO.MonitorUserDTO> QuickSearchEurofarUsers(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchEurofarUsers(query, out notification);
        }
    }
}
