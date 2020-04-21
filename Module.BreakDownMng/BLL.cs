using System;
using System.Collections;
using System.Collections.Generic;

namespace Module.BreakDownMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        public DTO.EditFormData GetData(int iRequesterID, int id, int modelId, int sampleProductId, int offerSeasonDetailID, int factoryId, out Library.DTO.Notification notification)
        {
            return factory.GetData(iRequesterID, id, modelId, sampleProductId, offerSeasonDetailID, factoryId, out notification);
        }

        public DTO.ProductOptionFormData GetProductOption(int iRequesterID, int categoryId, int modelId, int productGroupId, out Library.DTO.Notification notification)
        {
            return factory.GetProductOption(iRequesterID, categoryId, modelId, productGroupId, out notification);
        }
        public bool UpdateCategoryOption(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateCategoryOption(iRequesterID, id, ref dtoItem, out notification);
        }

        public List<DTO.ProductionItemDTO> QuickSearchProductionItem(string query, string type, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchProductionItem(query, type, out notification);
        }

        public object GetModel(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetModel(iRequesterID, filters, out notification);
        }

        public object GetSampleProduct(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetSampleProduct(iRequesterID, filters, out notification);
        }
        public string GetArticleCode(int offerSeasonDetailID, int modelID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.GetArticleCode(offerSeasonDetailID, modelID, dtoItem, out notification);
        }
        public DTO.GetPriceDetailDTO GetPriceData(int iRequesterID, object dtoItem, int id, string articleCode, out Library.DTO.Notification notification)
        {
            return factory.GetPriceData(iRequesterID, dtoItem, id, articleCode, out notification);
        }

        public bool UpdatePrice(int iRequesterID, object dtoItem, int id, string articleCode, out Library.DTO.Notification notification)
        {
            return factory.UpdatePrice(iRequesterID, dtoItem, id, articleCode, out notification);
        }

        public bool ApproveOption(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.ApproveOption(iRequesterID, id, out notification);
        }

        public bool UnApproveOption(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.UnApproveOption(iRequesterID, id, out notification);
        }

        public bool ApproveAllOption(int iRequesterID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.ApproveAllOption(iRequesterID, dtoItem, out notification);
        }

        public bool UnApproveAllOption(int iRequesterID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UnApproveAllOption(iRequesterID, dtoItem, out notification);
        }

        public bool UpdateSeasonSpecPercent(int iRequesterID, int id, decimal percent, out Library.DTO.Notification notification)
        {
            return factory.UpdateSeasonSpecPercent(iRequesterID, id, percent, out notification);
        }

        public List<DTO.ProductionItemDTO> GetDefaultProductionItem(int id, out Library.DTO.Notification notification)
        {
            return factory.GetDefaultProductionItem(id, out notification);
        }
        public DTO.SearchFormData GetCaculation(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as InitData");

            // query data
            return factory.GetCaculation(out notification);

        }

        //
        // import from BOM
        //
        public bool ImportBOMData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.ImportBOMData(iRequesterID, out notification);
        }

        public DTO.BOMImportResultDTO ImportBOMDataSingle(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.ImportBOMDataSingle(iRequesterID, id, out notification);
        }

        public string GetExcelReportBreakdown(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportBreakdown(filters, out notification);
        }

        public List<DTO.BreakdownPriceDTO> GetPurchasingPriceData(int userID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetPurchasingPriceData(userID, id, out notification);
        }

        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }
        public DTO.GetPriceDetailDTO GetPriceQuotation(int userID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.GetPriceQuotation(userID, dtoItem, out notification);
        }
    }
}
