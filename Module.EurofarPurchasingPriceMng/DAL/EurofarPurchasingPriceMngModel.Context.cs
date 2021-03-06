﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.EurofarPurchasingPriceMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EurofarPurchasingPriceMngEntities : DbContext
    {
        public EurofarPurchasingPriceMngEntities()
            : base("name=EurofarPurchasingPriceMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Quotation> Quotation { get; set; }
        public virtual DbSet<QuotationDetail> QuotationDetail { get; set; }
        public virtual DbSet<QuotationOffer> QuotationOffer { get; set; }
        public virtual DbSet<QuotationOfferDetail> QuotationOfferDetail { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_OfferHistory_View> EurofarPurchasingPriceMng_OfferHistory_View { get; set; }
        public virtual DbSet<SupportMng_QuotationStatus_View> SupportMng_QuotationStatus_View { get; set; }
        public virtual DbSet<PriceDifference> PriceDifference { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_RelatedQuotationDetail_View> EurofarPurchasingPriceMng_RelatedQuotationDetail_View { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_GeneralBreakDown_View> EurofarPurchasingPriceMng_GeneralBreakDown_View { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_PALBreakDown_View> EurofarPurchasingPriceMng_PALBreakDown_View { get; set; }
        public virtual DbSet<SupportMng_OrderType_View> SupportMng_OrderType_View { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_Breakdown_View> EurofarPurchasingPriceMng_Breakdown_View { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_BreakdownCategory_View> EurofarPurchasingPriceMng_BreakdownCategory_View { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_BreakdownManagementFee_View> EurofarPurchasingPriceMng_BreakdownManagementFee_View { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_BreakdownCategoryOption_View> EurofarPurchasingPriceMng_BreakdownCategoryOption_View { get; set; }
        public virtual DbSet<EurofarPurchasingPriceMng_SimilarItem_View> EurofarPurchasingPriceMng_SimilarItem_View { get; set; }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_GetQuotaionConclusion_Result> EurofarPurchasingPriceMng_function_GetQuotaionConclusion(string season, string clientNM, string description, string factoryUD, Nullable<int> itemTypeID, Nullable<int> statusID, Nullable<int> quotationOfferDirectionID, Nullable<int> orderTypeID, string proformaInvoiceNo, Nullable<int> businessDataStatusID, Nullable<int> shippedStatus, string lDSFrom, string lDSTo, Nullable<int> cataloguePageNo)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var factoryUDParameter = factoryUD != null ?
                new ObjectParameter("FactoryUD", factoryUD) :
                new ObjectParameter("FactoryUD", typeof(string));
    
            var itemTypeIDParameter = itemTypeID.HasValue ?
                new ObjectParameter("ItemTypeID", itemTypeID) :
                new ObjectParameter("ItemTypeID", typeof(int));
    
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("StatusID", statusID) :
                new ObjectParameter("StatusID", typeof(int));
    
            var quotationOfferDirectionIDParameter = quotationOfferDirectionID.HasValue ?
                new ObjectParameter("QuotationOfferDirectionID", quotationOfferDirectionID) :
                new ObjectParameter("QuotationOfferDirectionID", typeof(int));
    
            var orderTypeIDParameter = orderTypeID.HasValue ?
                new ObjectParameter("OrderTypeID", orderTypeID) :
                new ObjectParameter("OrderTypeID", typeof(int));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var businessDataStatusIDParameter = businessDataStatusID.HasValue ?
                new ObjectParameter("BusinessDataStatusID", businessDataStatusID) :
                new ObjectParameter("BusinessDataStatusID", typeof(int));
    
            var shippedStatusParameter = shippedStatus.HasValue ?
                new ObjectParameter("ShippedStatus", shippedStatus) :
                new ObjectParameter("ShippedStatus", typeof(int));
    
            var lDSFromParameter = lDSFrom != null ?
                new ObjectParameter("LDSFrom", lDSFrom) :
                new ObjectParameter("LDSFrom", typeof(string));
    
            var lDSToParameter = lDSTo != null ?
                new ObjectParameter("LDSTo", lDSTo) :
                new ObjectParameter("LDSTo", typeof(string));
    
            var cataloguePageNoParameter = cataloguePageNo.HasValue ?
                new ObjectParameter("CataloguePageNo", cataloguePageNo) :
                new ObjectParameter("CataloguePageNo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_GetQuotaionConclusion_Result>("EurofarPurchasingPriceMng_function_GetQuotaionConclusion", seasonParameter, clientNMParameter, descriptionParameter, factoryUDParameter, itemTypeIDParameter, statusIDParameter, quotationOfferDirectionIDParameter, orderTypeIDParameter, proformaInvoiceNoParameter, businessDataStatusIDParameter, shippedStatusParameter, lDSFromParameter, lDSToParameter, cataloguePageNoParameter);
        }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation_Result> EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation(string season, string clientNM, string description, string factoryUD, Nullable<int> itemTypeID, Nullable<int> statusID, Nullable<int> quotationOfferDirectionID, Nullable<int> orderTypeID, string proformaInvoiceNo, Nullable<int> businessDataStatusID, Nullable<int> shippedStatus, string lDSFrom, string lDSTo, Nullable<int> cataloguePageNo)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var factoryUDParameter = factoryUD != null ?
                new ObjectParameter("FactoryUD", factoryUD) :
                new ObjectParameter("FactoryUD", typeof(string));
    
            var itemTypeIDParameter = itemTypeID.HasValue ?
                new ObjectParameter("ItemTypeID", itemTypeID) :
                new ObjectParameter("ItemTypeID", typeof(int));
    
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("StatusID", statusID) :
                new ObjectParameter("StatusID", typeof(int));
    
            var quotationOfferDirectionIDParameter = quotationOfferDirectionID.HasValue ?
                new ObjectParameter("QuotationOfferDirectionID", quotationOfferDirectionID) :
                new ObjectParameter("QuotationOfferDirectionID", typeof(int));
    
            var orderTypeIDParameter = orderTypeID.HasValue ?
                new ObjectParameter("OrderTypeID", orderTypeID) :
                new ObjectParameter("OrderTypeID", typeof(int));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var businessDataStatusIDParameter = businessDataStatusID.HasValue ?
                new ObjectParameter("BusinessDataStatusID", businessDataStatusID) :
                new ObjectParameter("BusinessDataStatusID", typeof(int));
    
            var shippedStatusParameter = shippedStatus.HasValue ?
                new ObjectParameter("ShippedStatus", shippedStatus) :
                new ObjectParameter("ShippedStatus", typeof(int));
    
            var lDSFromParameter = lDSFrom != null ?
                new ObjectParameter("LDSFrom", lDSFrom) :
                new ObjectParameter("LDSFrom", typeof(string));
    
            var lDSToParameter = lDSTo != null ?
                new ObjectParameter("LDSTo", lDSTo) :
                new ObjectParameter("LDSTo", typeof(string));
    
            var cataloguePageNoParameter = cataloguePageNo.HasValue ?
                new ObjectParameter("CataloguePageNo", cataloguePageNo) :
                new ObjectParameter("CataloguePageNo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation_Result>("EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation", seasonParameter, clientNMParameter, descriptionParameter, factoryUDParameter, itemTypeIDParameter, statusIDParameter, quotationOfferDirectionIDParameter, orderTypeIDParameter, proformaInvoiceNoParameter, businessDataStatusIDParameter, shippedStatusParameter, lDSFromParameter, lDSToParameter, cataloguePageNoParameter);
        }
    
        public virtual ObjectResult<MasterExchangeRateMng_function_GetExchangeRate_Result> MasterExchangeRateMng_function_GetExchangeRate(Nullable<System.DateTime> inputDate, string currency)
        {
            var inputDateParameter = inputDate.HasValue ?
                new ObjectParameter("InputDate", inputDate) :
                new ObjectParameter("InputDate", typeof(System.DateTime));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MasterExchangeRateMng_function_GetExchangeRate_Result>("MasterExchangeRateMng_function_GetExchangeRate", inputDateParameter, currencyParameter);
        }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_GetAdditionalInfo_Result> EurofarPurchasingPriceMng_function_GetAdditionalInfo(string season, string iDs)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var iDsParameter = iDs != null ?
                new ObjectParameter("IDs", iDs) :
                new ObjectParameter("IDs", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_GetAdditionalInfo_Result>("EurofarPurchasingPriceMng_function_GetAdditionalInfo", seasonParameter, iDsParameter);
        }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion_Result> EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion(string season)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion_Result>("EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion", seasonParameter);
        }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest_Result> EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest(string season)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest_Result>("EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest", seasonParameter);
        }
    
        public virtual int OfferSeasonQuotatonRequestMng_function_ChangeOfferSeasonDetailStatus(Nullable<int> updatedBy, string iDs)
        {
            var updatedByParameter = updatedBy.HasValue ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(int));
    
            var iDsParameter = iDs != null ?
                new ObjectParameter("IDs", iDs) :
                new ObjectParameter("IDs", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("OfferSeasonQuotatonRequestMng_function_ChangeOfferSeasonDetailStatus", updatedByParameter, iDsParameter);
        }
    
        public virtual int EurofarPurchasingPriceMng_function_RefreshCacheRow(Nullable<int> quotationDetailID, string season)
        {
            var quotationDetailIDParameter = quotationDetailID.HasValue ?
                new ObjectParameter("QuotationDetailID", quotationDetailID) :
                new ObjectParameter("QuotationDetailID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EurofarPurchasingPriceMng_function_RefreshCacheRow", quotationDetailIDParameter, seasonParameter);
        }
    
        public virtual int FW_function_UpdateOfferSeasonDetailPurchasingPriceFromQuotationConfirmed(Nullable<int> quotationDetailID, Nullable<int> userID)
        {
            var quotationDetailIDParameter = quotationDetailID.HasValue ?
                new ObjectParameter("QuotationDetailID", quotationDetailID) :
                new ObjectParameter("QuotationDetailID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FW_function_UpdateOfferSeasonDetailPurchasingPriceFromQuotationConfirmed", quotationDetailIDParameter, userIDParameter);
        }
    
        public virtual int EurofarPurchasingPriceMng_action_AddQuotationOffer(Nullable<int> quotationDetailID, string remark, Nullable<decimal> targetPrice, Nullable<int> updatedBy)
        {
            var quotationDetailIDParameter = quotationDetailID.HasValue ?
                new ObjectParameter("QuotationDetailID", quotationDetailID) :
                new ObjectParameter("QuotationDetailID", typeof(int));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            var targetPriceParameter = targetPrice.HasValue ?
                new ObjectParameter("TargetPrice", targetPrice) :
                new ObjectParameter("TargetPrice", typeof(decimal));
    
            var updatedByParameter = updatedBy.HasValue ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EurofarPurchasingPriceMng_action_AddQuotationOffer", quotationDetailIDParameter, remarkParameter, targetPriceParameter, updatedByParameter);
        }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge_Result> EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge_Result>("EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge");
        }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion_Result> EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion(string season)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion_Result>("EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion", seasonParameter);
        }
    
        public virtual ObjectResult<EurofarPurchasingPriceMng_function_SearchFactoryQuotation_Result> EurofarPurchasingPriceMng_function_SearchFactoryQuotation(string season, string clientNM, string description, string factoryUD, Nullable<int> itemTypeID, Nullable<int> statusID, Nullable<int> quotationOfferDirectionID, Nullable<int> orderTypeID, string proformaInvoiceNo, Nullable<int> businessDataStatusID, Nullable<int> shippedStatus, string lDSFrom, string lDSTo, Nullable<int> cataloguePageNo, Nullable<int> pricingTeamMemberID, string sortedBy, string sortedDirection)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var factoryUDParameter = factoryUD != null ?
                new ObjectParameter("FactoryUD", factoryUD) :
                new ObjectParameter("FactoryUD", typeof(string));
    
            var itemTypeIDParameter = itemTypeID.HasValue ?
                new ObjectParameter("ItemTypeID", itemTypeID) :
                new ObjectParameter("ItemTypeID", typeof(int));
    
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("StatusID", statusID) :
                new ObjectParameter("StatusID", typeof(int));
    
            var quotationOfferDirectionIDParameter = quotationOfferDirectionID.HasValue ?
                new ObjectParameter("QuotationOfferDirectionID", quotationOfferDirectionID) :
                new ObjectParameter("QuotationOfferDirectionID", typeof(int));
    
            var orderTypeIDParameter = orderTypeID.HasValue ?
                new ObjectParameter("OrderTypeID", orderTypeID) :
                new ObjectParameter("OrderTypeID", typeof(int));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var businessDataStatusIDParameter = businessDataStatusID.HasValue ?
                new ObjectParameter("BusinessDataStatusID", businessDataStatusID) :
                new ObjectParameter("BusinessDataStatusID", typeof(int));
    
            var shippedStatusParameter = shippedStatus.HasValue ?
                new ObjectParameter("ShippedStatus", shippedStatus) :
                new ObjectParameter("ShippedStatus", typeof(int));
    
            var lDSFromParameter = lDSFrom != null ?
                new ObjectParameter("LDSFrom", lDSFrom) :
                new ObjectParameter("LDSFrom", typeof(string));
    
            var lDSToParameter = lDSTo != null ?
                new ObjectParameter("LDSTo", lDSTo) :
                new ObjectParameter("LDSTo", typeof(string));
    
            var cataloguePageNoParameter = cataloguePageNo.HasValue ?
                new ObjectParameter("CataloguePageNo", cataloguePageNo) :
                new ObjectParameter("CataloguePageNo", typeof(int));
    
            var pricingTeamMemberIDParameter = pricingTeamMemberID.HasValue ?
                new ObjectParameter("PricingTeamMemberID", pricingTeamMemberID) :
                new ObjectParameter("PricingTeamMemberID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarPurchasingPriceMng_function_SearchFactoryQuotation_Result>("EurofarPurchasingPriceMng_function_SearchFactoryQuotation", seasonParameter, clientNMParameter, descriptionParameter, factoryUDParameter, itemTypeIDParameter, statusIDParameter, quotationOfferDirectionIDParameter, orderTypeIDParameter, proformaInvoiceNoParameter, businessDataStatusIDParameter, shippedStatusParameter, lDSFromParameter, lDSToParameter, cataloguePageNoParameter, pricingTeamMemberIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int FW_function_RefreshPriceCacheRow(string season, Nullable<int> quotationDetailID, Nullable<int> offerSeasonDetailID)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var quotationDetailIDParameter = quotationDetailID.HasValue ?
                new ObjectParameter("QuotationDetailID", quotationDetailID) :
                new ObjectParameter("QuotationDetailID", typeof(int));
    
            var offerSeasonDetailIDParameter = offerSeasonDetailID.HasValue ?
                new ObjectParameter("OfferSeasonDetailID", offerSeasonDetailID) :
                new ObjectParameter("OfferSeasonDetailID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FW_function_RefreshPriceCacheRow", seasonParameter, quotationDetailIDParameter, offerSeasonDetailIDParameter);
        }
    
        public virtual int EurofarPurchasingPriceMng_function_AfterUnConfirm(Nullable<int> quotationDetailID)
        {
            var quotationDetailIDParameter = quotationDetailID.HasValue ?
                new ObjectParameter("QuotationDetailID", quotationDetailID) :
                new ObjectParameter("QuotationDetailID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EurofarPurchasingPriceMng_function_AfterUnConfirm", quotationDetailIDParameter);
        }
    }
}
