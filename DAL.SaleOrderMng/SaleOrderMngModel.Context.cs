﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SaleOrderMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SaleOrderMngEntities : DbContext
    {
        public SaleOrderMngEntities()
            : base("name=SaleOrderMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SaleOrderDetail> SaleOrderDetail { get; set; }
        public virtual DbSet<SaleOrderDetailExtend> SaleOrderDetailExtend { get; set; }
        public virtual DbSet<SaleOrderExtend> SaleOrderExtend { get; set; }
        public virtual DbSet<SaleOrderStatus> SaleOrderStatus { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetail_View> SaleOrderMng_SaleOrderDetail_View { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetailExtend_View> SaleOrderMng_SaleOrderDetailExtend_View { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderExtend_View> SaleOrderMng_SaleOrderExtend_View { get; set; }
        public virtual DbSet<SaleOrderHistory> SaleOrderHistory { get; set; }
        public virtual DbSet<SaleOrderHistoryDetail> SaleOrderHistoryDetail { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetailSearch_View> SaleOrderMng_SaleOrderDetailSearch_View { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderSearch_View> SaleOrderMng_SaleOrderSearch_View { get; set; }
        public virtual DbSet<SaleOrder_ReportView> SaleOrderMng_SaleOrder_ReportView { get; set; }
        public virtual DbSet<SaleOrderDetail_ReportView> SaleOrderMng_SaleOrderDetail_ReportView { get; set; }
        public virtual DbSet<SaleOrderDetailExtend_ReportView> SaleOrderMng_SaleOrderDetailExtend_ReportView { get; set; }
        public virtual DbSet<SaleOrderExtend_ReportView> SaleOrderMng_SaleOrderExtend_ReportView { get; set; }
        public virtual DbSet<SaleOrderMng_Offer_View> SaleOrderMng_Offer_View { get; set; }
        public virtual DbSet<SaleOrderMng_OfferSearch_View> SaleOrderMng_OfferSearch_View { get; set; }
        public virtual DbSet<SaleOrderMng_Product_View> SaleOrderMng_Product_View { get; set; }
        public virtual DbSet<SaleOrder> SaleOrder { get; set; }
        public virtual DbSet<SaleOrderDetailSparepart> SaleOrderDetailSparepart { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetailSparepart_View> SaleOrderMng_SaleOrderDetailSparepart_View { get; set; }
        public virtual DbSet<SaleOrderDetailSparepart_ReportView> SaleOrderMng_SaleOrderDetailSparepart_ReportView { get; set; }
        public virtual DbSet<List_PaymentStatus_View> List_PaymentStatus_View { get; set; }
        public virtual DbSet<SaleOrderMng_LoadingPlanDetail_View> SaleOrderMng_LoadingPlanDetail_View { get; set; }
        public virtual DbSet<SaleOrderMng_LoadingPlan_View> SaleOrderMng_LoadingPlan_View { get; set; }
        public virtual DbSet<SaleOrderMng_LoadingPlanSparepartDetail_View> SaleOrderMng_LoadingPlanSparepartDetail_View { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderProductReturn_View> SaleOrderMng_SaleOrderProductReturn_View { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderSparepartReturn_View> SaleOrderMng_SaleOrderSparepartReturn_View { get; set; }
        public virtual DbSet<SaleOrderMng_StockOverview_View> SaleOrderMng_StockOverview_View { get; set; }
        public virtual DbSet<SaleOrderMng_WarehouseImport_View> SaleOrderMng_WarehouseImport_View { get; set; }
        public virtual DbSet<OfferStatus> OfferStatus { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetail_OverView> SaleOrderMng_SaleOrderDetail_OverView { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetailExtend_OverView> SaleOrderMng_SaleOrderDetailExtend_OverView { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetailSparepart_OverView> SaleOrderMng_SaleOrderDetailSparepart_OverView { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderExtend_OverView> SaleOrderMng_SaleOrderExtend_OverView { get; set; }
        public virtual DbSet<SaleOrderMng_WarehouseImport_OverView> SaleOrderMng_WarehouseImport_OverView { get; set; }
        public virtual DbSet<SaleOrderMng_ApprovedOffer_View> SaleOrderMng_ApprovedOffer_View { get; set; }
        public virtual DbSet<SaleOrderMng_OfferLine_View> SaleOrderMng_OfferLine_View { get; set; }
        public virtual DbSet<SaleOrderMng_OfferLineSparepart_View> SaleOrderMng_OfferLineSparepart_View { get; set; }
        public virtual DbSet<SaleOrderMng_GetClientIDByOfferID_View> SaleOrderMng_GetClientIDByOfferID_View { get; set; }
        public virtual DbSet<List_TrackingStatus> List_TrackingStatus { get; set; }
        public virtual DbSet<SaleOrderMng_OfferLineSample_View> SaleOrderMng_OfferLineSample_View { get; set; }
        public virtual DbSet<SaleOrderDetailSample_ReportView> SaleOrderMng_SaleOrderDetailSample_ReportView { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrder_OverView> SaleOrderMng_SaleOrder_OverView { get; set; }
        public virtual DbSet<SaleOrderDetailSample> SaleOrderDetailSample { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrderDetailSample_View> SaleOrderMng_SaleOrderDetailSample_View { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<OfferLine> OfferLine { get; set; }
        public virtual DbSet<OfferSeason> OfferSeason { get; set; }
        public virtual DbSet<OfferSeasonDetail> OfferSeasonDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<LDSClient> LDSClient { get; set; }
        public virtual DbSet<SaleOrderMng_LDSClient_View> SaleOrderMng_LDSClient_View { get; set; }
        public virtual DbSet<SaleOrderMng_SaleOrder_View> SaleOrderMng_SaleOrder_View { get; set; }
        public virtual DbSet<SaleOrderMng_FactoryOrderDetail_OverView> SaleOrderMng_FactoryOrderDetail_OverView { get; set; }
        public virtual DbSet<SaleOrderMng_FactoryOrderDetailQCReport_OverView> SaleOrderMng_FactoryOrderDetailQCReport_OverView { get; set; }
    
        public virtual int SaleOrderMng_function_DeleteSaleOrder(Nullable<int> saleOrderID, Nullable<int> deletedBy)
        {
            var saleOrderIDParameter = saleOrderID.HasValue ?
                new ObjectParameter("SaleOrderID", saleOrderID) :
                new ObjectParameter("SaleOrderID", typeof(int));
    
            var deletedByParameter = deletedBy.HasValue ?
                new ObjectParameter("DeletedBy", deletedBy) :
                new ObjectParameter("DeletedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaleOrderMng_function_DeleteSaleOrder", saleOrderIDParameter, deletedByParameter);
        }
    
        public virtual ObjectResult<SaleOrderMng_OfferSearch_View> SaleOrderMng_function_SearchOffer(string sortedBy, string sortedDirection, string season, string offerUD, string clientUD, string clientNM, Nullable<int> saleID, string proformaInvoiceNo, string orderType, string v4PINo, string articleCode, string description, string clientArticleCode, string clientArticleName, string clientOrderNumber, string clientEANCode)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var offerUDParameter = offerUD != null ?
                new ObjectParameter("OfferUD", offerUD) :
                new ObjectParameter("OfferUD", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var orderTypeParameter = orderType != null ?
                new ObjectParameter("OrderType", orderType) :
                new ObjectParameter("OrderType", typeof(string));
    
            var v4PINoParameter = v4PINo != null ?
                new ObjectParameter("V4PINo", v4PINo) :
                new ObjectParameter("V4PINo", typeof(string));
    
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var clientArticleCodeParameter = clientArticleCode != null ?
                new ObjectParameter("ClientArticleCode", clientArticleCode) :
                new ObjectParameter("ClientArticleCode", typeof(string));
    
            var clientArticleNameParameter = clientArticleName != null ?
                new ObjectParameter("ClientArticleName", clientArticleName) :
                new ObjectParameter("ClientArticleName", typeof(string));
    
            var clientOrderNumberParameter = clientOrderNumber != null ?
                new ObjectParameter("ClientOrderNumber", clientOrderNumber) :
                new ObjectParameter("ClientOrderNumber", typeof(string));
    
            var clientEANCodeParameter = clientEANCode != null ?
                new ObjectParameter("ClientEANCode", clientEANCode) :
                new ObjectParameter("ClientEANCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SaleOrderMng_OfferSearch_View>("SaleOrderMng_function_SearchOffer", sortedByParameter, sortedDirectionParameter, seasonParameter, offerUDParameter, clientUDParameter, clientNMParameter, saleIDParameter, proformaInvoiceNoParameter, orderTypeParameter, v4PINoParameter, articleCodeParameter, descriptionParameter, clientArticleCodeParameter, clientArticleNameParameter, clientOrderNumberParameter, clientEANCodeParameter);
        }
    
        public virtual ObjectResult<SaleOrderMng_OfferSearch_View> SaleOrderMng_function_SearchOffer(string sortedBy, string sortedDirection, string season, string offerUD, string clientUD, string clientNM, Nullable<int> saleID, string proformaInvoiceNo, string orderType, string v4PINo, string articleCode, string description, string clientArticleCode, string clientArticleName, string clientOrderNumber, string clientEANCode, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var offerUDParameter = offerUD != null ?
                new ObjectParameter("OfferUD", offerUD) :
                new ObjectParameter("OfferUD", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var orderTypeParameter = orderType != null ?
                new ObjectParameter("OrderType", orderType) :
                new ObjectParameter("OrderType", typeof(string));
    
            var v4PINoParameter = v4PINo != null ?
                new ObjectParameter("V4PINo", v4PINo) :
                new ObjectParameter("V4PINo", typeof(string));
    
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var clientArticleCodeParameter = clientArticleCode != null ?
                new ObjectParameter("ClientArticleCode", clientArticleCode) :
                new ObjectParameter("ClientArticleCode", typeof(string));
    
            var clientArticleNameParameter = clientArticleName != null ?
                new ObjectParameter("ClientArticleName", clientArticleName) :
                new ObjectParameter("ClientArticleName", typeof(string));
    
            var clientOrderNumberParameter = clientOrderNumber != null ?
                new ObjectParameter("ClientOrderNumber", clientOrderNumber) :
                new ObjectParameter("ClientOrderNumber", typeof(string));
    
            var clientEANCodeParameter = clientEANCode != null ?
                new ObjectParameter("ClientEANCode", clientEANCode) :
                new ObjectParameter("ClientEANCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SaleOrderMng_OfferSearch_View>("SaleOrderMng_function_SearchOffer", mergeOption, sortedByParameter, sortedDirectionParameter, seasonParameter, offerUDParameter, clientUDParameter, clientNMParameter, saleIDParameter, proformaInvoiceNoParameter, orderTypeParameter, v4PINoParameter, articleCodeParameter, descriptionParameter, clientArticleCodeParameter, clientArticleNameParameter, clientOrderNumberParameter, clientEANCodeParameter);
        }
    
        public virtual int SaleOrderMng_function_CreateReservation(Nullable<int> saleOrderID, Nullable<int> updatedBy)
        {
            var saleOrderIDParameter = saleOrderID.HasValue ?
                new ObjectParameter("SaleOrderID", saleOrderID) :
                new ObjectParameter("SaleOrderID", typeof(int));
    
            var updatedByParameter = updatedBy.HasValue ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaleOrderMng_function_CreateReservation", saleOrderIDParameter, updatedByParameter);
        }
    
        public virtual ObjectResult<string> SaleOrderMng_function_GeneratePINo(Nullable<int> userID, Nullable<int> offerID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var offerIDParameter = offerID.HasValue ?
                new ObjectParameter("OfferID", offerID) :
                new ObjectParameter("OfferID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SaleOrderMng_function_GeneratePINo", userIDParameter, offerIDParameter);
        }
    
        public virtual int SaleOrderMng_function_FrozenSaleOrderPrintoutData(Nullable<int> saleOrderID)
        {
            var saleOrderIDParameter = saleOrderID.HasValue ?
                new ObjectParameter("SaleOrderID", saleOrderID) :
                new ObjectParameter("SaleOrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaleOrderMng_function_FrozenSaleOrderPrintoutData", saleOrderIDParameter);
        }
    
        public virtual int FW_Quotation_function_AddFactoryOrderItem(Nullable<int> factoryOrderID)
        {
            var factoryOrderIDParameter = factoryOrderID.HasValue ?
                new ObjectParameter("FactoryOrderID", factoryOrderID) :
                new ObjectParameter("FactoryOrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FW_Quotation_function_AddFactoryOrderItem", factoryOrderIDParameter);
        }
    }
}
