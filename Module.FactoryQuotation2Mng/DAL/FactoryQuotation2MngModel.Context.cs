﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryQuotation2Mng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FactoryQuotation2MngEntities : DbContext
    {
        public FactoryQuotation2MngEntities()
            : base("name=FactoryQuotation2MngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PriceDifference> PriceDifference { get; set; }
        public virtual DbSet<Quotation> Quotation { get; set; }
        public virtual DbSet<QuotationDetail> QuotationDetail { get; set; }
        public virtual DbSet<QuotationOffer> QuotationOffer { get; set; }
        public virtual DbSet<QuotationOfferDetail> QuotationOfferDetail { get; set; }
        public virtual DbSet<FactoryQuotation2Mng_OfferHistory_View> FactoryQuotation2Mng_OfferHistory_View { get; set; }
        public virtual DbSet<SupportMng_QuotationStatus_View> SupportMng_QuotationStatus_View { get; set; }
        public virtual DbSet<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View> FactoryQuotation2Mng_GetWaitForFactoryConclusion_View { get; set; }
        public virtual DbSet<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View> FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View { get; set; }
        public virtual DbSet<FactoryQuotation2Mng_FactoryQuotationSearchResult_View> FactoryQuotation2Mng_FactoryQuotationSearchResult_View { get; set; }
        public virtual DbSet<FactoryQuotation2Mng_SimilarItem_View> FactoryQuotation2Mng_SimilarItem_View { get; set; }
        public virtual DbSet<FactoryQuotation2Mng_ClientAdditionalCondition_View> FactoryQuotation2Mng_ClientAdditionalCondition_View { get; set; }
    
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
    
        public virtual ObjectResult<FactoryQuotation2Mng_function_GetQuotaionConclusion_Result> FactoryQuotation2Mng_function_GetQuotaionConclusion(string season, string clientNM, string description, string factoryUD, Nullable<int> itemTypeID, Nullable<int> statusID, Nullable<int> quotationOfferDirectionID, string proformaInvoiceNo, Nullable<int> businessDataStatusID)
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
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var businessDataStatusIDParameter = businessDataStatusID.HasValue ?
                new ObjectParameter("BusinessDataStatusID", businessDataStatusID) :
                new ObjectParameter("BusinessDataStatusID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryQuotation2Mng_function_GetQuotaionConclusion_Result>("FactoryQuotation2Mng_function_GetQuotaionConclusion", seasonParameter, clientNMParameter, descriptionParameter, factoryUDParameter, itemTypeIDParameter, statusIDParameter, quotationOfferDirectionIDParameter, proformaInvoiceNoParameter, businessDataStatusIDParameter);
        }
    
        public virtual int FactoryQuotation2Mng_action_AddQuotationOffer(Nullable<int> quotationDetailID, Nullable<decimal> purchasingPrice, string newComment, Nullable<int> updatedBy)
        {
            var quotationDetailIDParameter = quotationDetailID.HasValue ?
                new ObjectParameter("QuotationDetailID", quotationDetailID) :
                new ObjectParameter("QuotationDetailID", typeof(int));
    
            var purchasingPriceParameter = purchasingPrice.HasValue ?
                new ObjectParameter("PurchasingPrice", purchasingPrice) :
                new ObjectParameter("PurchasingPrice", typeof(decimal));
    
            var newCommentParameter = newComment != null ?
                new ObjectParameter("NewComment", newComment) :
                new ObjectParameter("NewComment", typeof(string));
    
            var updatedByParameter = updatedBy.HasValue ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FactoryQuotation2Mng_action_AddQuotationOffer", quotationDetailIDParameter, purchasingPriceParameter, newCommentParameter, updatedByParameter);
        }
    
        public virtual ObjectResult<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View> FactoryQuotation2Mng_function_GetWaitForFactoryConclusion(string season, Nullable<int> userID)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View>("FactoryQuotation2Mng_function_GetWaitForFactoryConclusion", seasonParameter, userIDParameter);
        }
    
        public virtual ObjectResult<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View> FactoryQuotation2Mng_function_GetWaitForFactoryConclusion(string season, Nullable<int> userID, MergeOption mergeOption)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View>("FactoryQuotation2Mng_function_GetWaitForFactoryConclusion", mergeOption, seasonParameter, userIDParameter);
        }
    
        public virtual ObjectResult<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View> FactoryQuotation2Mng_function_GetWaitForFactoryProductionConclusion(string season, Nullable<int> userID)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View>("FactoryQuotation2Mng_function_GetWaitForFactoryProductionConclusion", seasonParameter, userIDParameter);
        }
    
        public virtual ObjectResult<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View> FactoryQuotation2Mng_function_GetWaitForFactoryProductionConclusion(string season, Nullable<int> userID, MergeOption mergeOption)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View>("FactoryQuotation2Mng_function_GetWaitForFactoryProductionConclusion", mergeOption, seasonParameter, userIDParameter);
        }
    
        public virtual ObjectResult<FactoryQuotation2Mng_FactoryQuotationSearchResult_View> FactoryQuotation2Mng_function_SearchFactoryQuotation(string season, string clientUD, string description, string factoryUD, Nullable<int> itemTypeID, Nullable<int> statusID, Nullable<int> quotationOfferDirectionID, string proformaInvoiceNo, Nullable<int> businessDataStatusID, Nullable<int> userID, Nullable<bool> isDeadline, Nullable<bool> isRepeatItem, string sortedBy, string sortedDirection)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
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
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var businessDataStatusIDParameter = businessDataStatusID.HasValue ?
                new ObjectParameter("BusinessDataStatusID", businessDataStatusID) :
                new ObjectParameter("BusinessDataStatusID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var isDeadlineParameter = isDeadline.HasValue ?
                new ObjectParameter("IsDeadline", isDeadline) :
                new ObjectParameter("IsDeadline", typeof(bool));
    
            var isRepeatItemParameter = isRepeatItem.HasValue ?
                new ObjectParameter("IsRepeatItem", isRepeatItem) :
                new ObjectParameter("IsRepeatItem", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryQuotation2Mng_FactoryQuotationSearchResult_View>("FactoryQuotation2Mng_function_SearchFactoryQuotation", seasonParameter, clientUDParameter, descriptionParameter, factoryUDParameter, itemTypeIDParameter, statusIDParameter, quotationOfferDirectionIDParameter, proformaInvoiceNoParameter, businessDataStatusIDParameter, userIDParameter, isDeadlineParameter, isRepeatItemParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactoryQuotation2Mng_FactoryQuotationSearchResult_View> FactoryQuotation2Mng_function_SearchFactoryQuotation(string season, string clientUD, string description, string factoryUD, Nullable<int> itemTypeID, Nullable<int> statusID, Nullable<int> quotationOfferDirectionID, string proformaInvoiceNo, Nullable<int> businessDataStatusID, Nullable<int> userID, Nullable<bool> isDeadline, Nullable<bool> isRepeatItem, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
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
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var businessDataStatusIDParameter = businessDataStatusID.HasValue ?
                new ObjectParameter("BusinessDataStatusID", businessDataStatusID) :
                new ObjectParameter("BusinessDataStatusID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var isDeadlineParameter = isDeadline.HasValue ?
                new ObjectParameter("IsDeadline", isDeadline) :
                new ObjectParameter("IsDeadline", typeof(bool));
    
            var isRepeatItemParameter = isRepeatItem.HasValue ?
                new ObjectParameter("IsRepeatItem", isRepeatItem) :
                new ObjectParameter("IsRepeatItem", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryQuotation2Mng_FactoryQuotationSearchResult_View>("FactoryQuotation2Mng_function_SearchFactoryQuotation", mergeOption, seasonParameter, clientUDParameter, descriptionParameter, factoryUDParameter, itemTypeIDParameter, statusIDParameter, quotationOfferDirectionIDParameter, proformaInvoiceNoParameter, businessDataStatusIDParameter, userIDParameter, isDeadlineParameter, isRepeatItemParameter, sortedByParameter, sortedDirectionParameter);
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
    
        public virtual int FactoryQuotation2Mng_function_AfterUnConfirm(Nullable<int> quotationDetailID)
        {
            var quotationDetailIDParameter = quotationDetailID.HasValue ?
                new ObjectParameter("QuotationDetailID", quotationDetailID) :
                new ObjectParameter("QuotationDetailID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FactoryQuotation2Mng_function_AfterUnConfirm", quotationDetailIDParameter);
        }
    }
}