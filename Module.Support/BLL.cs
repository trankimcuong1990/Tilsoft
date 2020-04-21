using Library.DTO;
using Module.Support.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public List<DTO.ProductionItem> GetProductionItem(int userId, System.Collections.Hashtable filters)
        {
            return factory.GetProductionItem(userId, filters);
        }

        public List<DTO.Client> GetClient(Hashtable filters)
        {
            return factory.GetClient(filters);
        }

        public List<DTO.FactoryOrderDetail> GetFactoryOrderDetail(int userId, Hashtable filters)
        {
            return factory.GetFactoryOrderDetail(userId, filters);
        }

        public List<SampleProduct> GetSampleProduct(Hashtable filters)
        {
            return factory.GetSampleProduct(filters);
        }

        public List<Model> GetModel(Hashtable filters)
        {
            return factory.GetModel(filters);
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromTeamToTeam(int userId, System.Collections.Hashtable filters)
        {
            return factory.GetProductionItemToDeliveryFromTeamToTeam(userId, filters);
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromWarehouseToTeam(int userId, System.Collections.Hashtable filters)
        {
            return factory.GetProductionItemToDeliveryFromWarehouseToTeam(userId, filters);
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromTeamToTeamToAmend(int userId, System.Collections.Hashtable filters)
        {
            return factory.GetProductionItemToDeliveryFromTeamToTeamToAmend(userId, filters);
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromWarehouseToTeamToAmend(int userId, System.Collections.Hashtable filters)
        {
            return factory.GetProductionItemToDeliveryFromWarehouseToTeamToAmend(userId, filters);
        }

        public DTO.FactoryMaterials QuickSearchFactoryMaterial(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "quick search factory material");
            return factory.QuickSearchFactoryMaterial(filters, out notification);
        }

        public DTO.FactoryOrders QuickSearchFactoryOrder(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "quick search factory order");
            return factory.QuickSearchFactoryOrder(filters, out notification);
        }

        public List<DTO.Factory> GetFactory()
        {
            return factory.GetFactory();
        }

        public List<DTO.Factory> GetFactory(int userId)
        {
            return factory.GetFactory(userId);
        }

        public List<DTO.AVFSupplier> QuickSearchAVFSupplier(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "quick search avf supplier");
            return factory.QuickSearchAVFSupplier(filters, out notification);
        }

        public List<DTO.Employee> QuickSearchEmployee(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "quick search employee");
            return factory.QuickSearchEmployee(filters, out notification);
        }

        //
        // QUICK SEARCH FUNCTIONS
        //
        public List<DTO.Client> QuickSearchClient(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchClient(query, out notification);
        }

        public DTO.FactoryFinishedProducts QuickSearchFactoryFinishedProduct(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "quick search factory finished product");
            return factory.QuickSearchFactoryFinishedProduct(filters, out notification);
        }

        // QUICK SEARCH
        public List<DTO.QuickSearchResult> QuickSearchClient2(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchClient2(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchModel(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchModel(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchMaterial(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchMaterial(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchMaterialType(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchMaterialType(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchMaterialColor(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchMaterialColor(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchFrameMaterial(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchFrameMaterial(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchFrameMaterialColor(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchFrameMaterialColor(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchSubMaterial(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSubMaterial(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchSubMaterialColor(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSubMaterialColor(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchBackCushion(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchBackCushion(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchSeatCushion(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSeatCushion(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchCushionColor(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchCushionColor(query, out notification);
        }
        public List<DTO.QuickSearchResult> QuickSearchClient3(string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchClient3(query, out notification);
        }

        //
        // OTHER FUNCTION
        //

        // model image
        public DTO.ModelImage GetModelImage(int modelId, out Library.DTO.Notification notification)
        {
            return factory.GetModelImage(modelId, out notification);
        }

        // product info
        public DTO.ProductInfo GetProductInfo(string articleCode, out Library.DTO.Notification notification)
        {
            return factory.GetProductInfo(articleCode, out notification);
        }

        public List<DTO.Forwarder> QuickSearchForwarder(string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchForwarder(searchQuery, out notification);
        }

        public object GetClient2(int userId, Hashtable filters, out Notification notification)
        {
            return factory.GetClient2(filters, out notification);
        }

        public object GetFactory2(int userId, Hashtable filters, out Notification notification)
        {
            return factory.GetFactory2(userId, filters, out notification);
        }

        public List<DTO.QuickSearchProductionItem> GetProductionItem2(int userId, string query, out Library.DTO.Notification notification)
        {
            return factory.GetProductionItem2(userId, query, out notification);
        }

        public object GetProductionItemWithFilters(int iRequesterID, string query, int branchID, out Library.DTO.Notification notification)
        {
            return factory.GetProductionItemWithFilters(iRequesterID, query, branchID, out notification);
        }

        public object GetWorkOrderApproved(int userId, string searchString, out Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "Quick-search work order approved");
            return factory.GetWorkOrderApproved(userId, searchString, out notification);
        }

        public object GetPurchaseOrderApprove(int userId, string searchString, out Notification notification)
        {
            return factory.GetPurchaseOrderApprove(userId, searchString, out notification);
        }

        public object GetModel2(int userId, string searchString, out Notification notification)
        {
            return factory.GetModel2(searchString, out notification);
        }

        public object GetWorkOrder(int userId, string searchString, out Notification notification)
        {
            return factory.GetWorkOrder(searchString, out notification);
        }

        public object GetUserProfiles(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Get information user profile[quick-search]");
            return factory.GetUserProfiles(iRequesterID, filters, out notification);
        }
    }
}
