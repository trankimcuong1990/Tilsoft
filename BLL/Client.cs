using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL
{
    public class ClientMng : Lib.BLLBase<DTO.ClientMng.ClientSearchResult, DTO.ClientMng.Client>
    {
        private DAL.ClientMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public ClientMng()
        {
            factory = new DAL.ClientMng.DataFactory();
        }
        public ClientMng(string tempFolder)
        {
            factory = new DAL.ClientMng.DataFactory(tempFolder);
        }
        public override IEnumerable<DTO.ClientMng.ClientSearchResult> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of client");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ClientMng.Client GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete client " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ClientMng.Client dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update client " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            dtoItem.UpdatedDate = DateTime.Now;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public bool SaveGIC(int id, List<DTO.ClientMng.ClientContactHistory> dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.SaveGIC(id, dtoItem, out notification);
        }

        public DTO.ClientMng.SearchFormData SearchClient(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            //fwBLL.WriteLog(iRequesterID, 0, "get list of client");
            filters["userID"] = iRequesterID;
            return factory.SearchClient(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.ClientMng.DataContainer GetEditData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            //fwBLL.WriteLog(iRequesterID, 0, "get client " + id.ToString());
            return factory.GetEditData(id, iRequesterID, out notification);
        }

        //
        // Change set custom code: MY001
        // Author: Nguyen The My
        // Created date: 09/04/2016
        // Description: print CIS report
        //
        public string PrintCIS(int ClientID, out Library.DTO.Notification notification)
        {
            return factory.PrintCIS(ClientID, out notification);
        }
        //
        // End custom change set: MY001
        //

        public string ExportExcelClient(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcelClient(iRequesterID, filters, out notification);
        }

        public DTO.ClientMng.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        // Custom function to get client overview
        public DTO.ClientMng.ClientOverview GetClientOverview(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client overview " + id.ToString());
            return factory.GetClientOverview(id, iRequesterID, out notification);
        }

        /// <summary>
        /// Get detail Offer, PLC, PI, ECI, SampleOrder, ClientComplaint.
        /// </summary>
        /// <param name="id"> ClientID </param>
        /// <param name="iRequesterID"> UserID </param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.ClientMng.ClientOverview2 GetDetailClientOverview(int id, string season, int iRequesterID, out Library.DTO.Notification notification)
        {
            // Write log.
            fwBLL.WriteLog(iRequesterID, 0, "GetDetailClientOverview " + id.ToString().Trim());
            return factory.GetDetailClientOverview(id, season, out notification);
        }

        public bool SetActivateStatus(int userId, int id, bool status, out Library.DTO.Notification notification)
        {
            return factory.SetActivateStatus(userId, id, status, out notification);
        }


        public DTO.ClientMng.ClientOverview2 GetOfferData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetOfferData(id, out notification);
        }

        public DTO.ClientMng.ClientOverview2 GetCIData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetCIData(id, out notification);
        }

        public DTO.ClientMng.ClientOverview2 GetPLCData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetPLCData(id, out notification);
        }

        public DTO.ClientMng.ClientOverview2 GetPIData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetPIData(id, out notification);
        }

        public DTO.ClientMng.ClientOverview2 GetClientComplainData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetClientComplainData(id, out notification);
        }

        public DTO.ClientMng.ClientOverview2 GetSampleOrderData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetSampleOrderData(id, out notification);
        }

        public List<DTO.ClientMng.OfferLine> GetOfferLine(int id, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLine(id, out notification);
        }

        public List<DTO.ClientMng.SaleOrderDetail> GetSaleOrderDetail(int id, out Library.DTO.Notification notification)
        {
            return factory.GetSaleOrderDetail(id, out notification);
        }

        public List<DTO.ClientMng.ECommercialInvoiceDetail> GetECommercialInvoiceDetail(int id, out Library.DTO.Notification notification)
        {
            return factory.GetECommercialInvoiceDetail(id, out notification);
        }

        public List<DTO.ClientMng.WarehouseInvoiceProductDetail> GetWarehouseInvoiceProductDetail(int id, out Library.DTO.Notification notification)
        {
            return factory.GetWarehouseInvoiceProductDetail(id, out notification);
        }

        public List<DTO.ClientMng.ECommercialInvoiceExtDetail> GetECommercialInvoiceExtDetail(int id, out Library.DTO.Notification notification)
        {
            return factory.GetECommercialInvoiceExtDetail(id, out notification);
        }

        public DTO.ClientMng.EurofarstockAccountDTO CreateEurofarstockAccount(int contactId, out Library.DTO.Notification notification)
        {
            return factory.CreateEurofarstockAccount(contactId, out notification);
        }

        public DTO.ClientMng.Overview.CIS.CISDataContainer GetCISData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetCISData(id, out notification);
        }
        public DTO.ClientMng.Overview.CIS.ShippingPerformanceDataContainer GetCISShippingPerformance(int id, string season, out Library.DTO.Notification notification)
        {
            return factory.GetCISShippingPerformance(id, season, out notification);
        }
        public DTO.ClientMng.Overview.CIS.ItemDataContainer GetCISItem(int id, string season, out Library.DTO.Notification notification)
        {
            return factory.GetCISItem(id, season, out notification);
        }
        public DTO.ClientMng.Overview.CIS.PriceComparisonDataContainer GetCISPriceComparison(int id, string season, string seasonToCompare, out Library.DTO.Notification notification)
        {
            return factory.GetCISPriceComparison(id, season, seasonToCompare, out notification);
        }
        public List<DTO.ClientMng.Overview.LoadingPlanDTO> GetLoadingPlan(int id, out Library.DTO.Notification notification)
        {
            return factory.GetLoadingPlan(id, out notification);
        }

        public List<DTO.ClientMng.Overview.PISearchResultDTO> QuickSearchPI_ByArticleCode(string articleCode, int clientId, string season, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchPI_ByArticleCode(articleCode, clientId, season, out notification);
        }
        public List<DTO.ClientMng.Overview.PISearchResultDTO> QuickSearchPI_ByCommercialInvoice(int commercialInvoiceId, int clientId, string season, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchPI_ByCommercialInvoice(commercialInvoiceId, clientId, season, out notification);
        }
        public List<DTO.ClientMng.Overview.PISearchResultDTO> QuickSearchPI_ByLoadingPlan(int loadingPlanId, int clientId, string season, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchPI_ByLoadingPlan(loadingPlanId, clientId, season, out notification);
        }

        public DTO.ClientMng.Overview.Delta.DataContainer GetDeltaData(int userId, int id, string season, out Library.DTO.Notification notification)
        {
            return factory.GetDeltaData(userId, id, season, out notification);
        }

        public DTO.ClientMng.Overview.Delta2.DataContainer GetDelta2Data(int userId, int id, string season, out Library.DTO.Notification notification)
        {
            return factory.GetDelta2Data(userId, id, season, out notification);
        }

        public DTO.ClientMng.Overview.Delta3.DataContainer GetDelta3Data(int userId, int clientID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetDelta3Data(userId, clientID, season, out notification);
        }


        public DTO.ClientMng.Overview.Breakdown.SearchFormDTO BreakdownGetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get list of client");
            filters["userID"] = iRequesterID;
            return factory.BreakdownGetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.ClientMng.Overview.Breakdown.SupportDataDTO BreakdownGetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            return factory.BreakdownGetSearchFilter(out notification);
        }

        public DTO.ClientMng.Overview.CIS.ItemDataContainer GetCISModel(int id, string season, out Library.DTO.Notification notification)
        {
            return factory.GetCISModel(id, season, out notification);
        }

        public List<DTO.ClientMng.ClientEstimatedAdditionalCostDTO> GetClientEstimatedAdditionalCost(int id, out Library.DTO.Notification notification)
        {
            return factory.GetClientEstimatedAdditionalCost(id, out notification);
        }

        public DTO.ClientMng.Overview.Offer.DataContainer GetOfferOverviewData(int id, string season, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetOfferOverviewData(id, season, out notification);
        }

        public List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO> GetOfferMarginDetail(int id, string season, int status, int clientID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data
            return factory.getOfferMarginDetail(id, season, status, clientID, out notification);
        }

        public string DetalExportExcel(int clientId, string season, out Library.DTO.Notification notification)
        {
            return factory.DetalExportExcel(clientId, season, out notification);
        }

        public string Detal2ExportExcel(int clientId, string season, out Library.DTO.Notification notification)
        {
            return factory.Detal2ExportExcel(clientId, season, out notification);
        }

        public string Detal3ExportExcel(int clientId, string season, out Library.DTO.Notification notification)
        {
            return factory.Detal3ExportExcel(clientId, season, out notification);
        }

        public bool UpdateOfferPotentialStatus(int userId, List<DTO.ClientMng.Overview.Offer.OfferMarginDTO> dtoItems, out Library.DTO.Notification notification)
        {
            return factory.UpdateOfferPotentialStatus(userId, dtoItems, out notification);
        }

        public List<DTO.ClientMng.PlaningPurchasingPriceDTO> GetPlaningPurchasingPrice(int userId, int offerLineID, out Library.DTO.Notification notification)
        {
            return factory.GetPlaningPurchasingPrice(userId, offerLineID, out notification);
        }

        public DTO.ClientMng.DataContainer GetDataClientAdditionalCondition(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetDataClientAdditionalCondition(out notification);
        }
    }
}
