﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BackSaleOrderMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BackSaleOrderMngEntities : DbContext
    {
        public BackSaleOrderMngEntities()
            : base("name=BackSaleOrderMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BackSaleOrderMng_SaleOrderSearch_View> BackSaleOrderMng_SaleOrderSearch_View { get; set; }
        public virtual DbSet<BackSaleOrderMng_GoodsRemaining_View> BackSaleOrderMng_GoodsRemaining_View { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<OfferLine> OfferLine { get; set; }
        public virtual DbSet<OfferLineSparepart> OfferLineSparepart { get; set; }
        public virtual DbSet<OfferStatus> OfferStatus { get; set; }
        public virtual DbSet<SaleOrderDetail> SaleOrderDetail { get; set; }
        public virtual DbSet<SaleOrderDetailSparepart> SaleOrderDetailSparepart { get; set; }
        public virtual DbSet<BackSaleOrderMng_BackOrder_View> BackSaleOrderMng_BackOrder_View { get; set; }
        public virtual DbSet<BackSaleOrderMng_BackOrderDetail_View> BackSaleOrderMng_BackOrderDetail_View { get; set; }
        public virtual DbSet<BackOrderDetail> BackOrderDetail { get; set; }
        public virtual DbSet<SaleOrderStatus> SaleOrderStatus { get; set; }
        public virtual DbSet<BackOrder> BackOrder { get; set; }
        public virtual DbSet<SaleOrder> SaleOrder { get; set; }
    
        public virtual ObjectResult<BackSaleOrderMng_SaleOrderSearch_View> BackSaleOrderMng_function_SearchSaleOrder(string sortedBy, string sortedDirection, string season, string clientUD, string clientNM, Nullable<int> saleID, string proformaInvoiceNo, string orderType, string articleCode, string description, string clientArticleCode, string clientArticleName, string clientOrderNumber, string clientEANCode)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BackSaleOrderMng_SaleOrderSearch_View>("BackSaleOrderMng_function_SearchSaleOrder", sortedByParameter, sortedDirectionParameter, seasonParameter, clientUDParameter, clientNMParameter, saleIDParameter, proformaInvoiceNoParameter, orderTypeParameter, articleCodeParameter, descriptionParameter, clientArticleCodeParameter, clientArticleNameParameter, clientOrderNumberParameter, clientEANCodeParameter);
        }
    
        public virtual ObjectResult<BackSaleOrderMng_SaleOrderSearch_View> BackSaleOrderMng_function_SearchSaleOrder(string sortedBy, string sortedDirection, string season, string clientUD, string clientNM, Nullable<int> saleID, string proformaInvoiceNo, string orderType, string articleCode, string description, string clientArticleCode, string clientArticleName, string clientOrderNumber, string clientEANCode, MergeOption mergeOption)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BackSaleOrderMng_SaleOrderSearch_View>("BackSaleOrderMng_function_SearchSaleOrder", mergeOption, sortedByParameter, sortedDirectionParameter, seasonParameter, clientUDParameter, clientNMParameter, saleIDParameter, proformaInvoiceNoParameter, orderTypeParameter, articleCodeParameter, descriptionParameter, clientArticleCodeParameter, clientArticleNameParameter, clientOrderNumberParameter, clientEANCodeParameter);
        }
    
        public virtual ObjectResult<BackSaleOrderMng_GoodsRemaining_View> BackSaleOrderMng_function_SearchGoodsRemaining(string sortedBy, string sortedDirection, string season, string clientUD, string clientNM, Nullable<int> saleID, string proformaInvoiceNo, string orderType, string articleCode, string description, string clientArticleCode, string clientArticleName, string clientOrderNumber, string clientEANCode)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BackSaleOrderMng_GoodsRemaining_View>("BackSaleOrderMng_function_SearchGoodsRemaining", sortedByParameter, sortedDirectionParameter, seasonParameter, clientUDParameter, clientNMParameter, saleIDParameter, proformaInvoiceNoParameter, orderTypeParameter, articleCodeParameter, descriptionParameter, clientArticleCodeParameter, clientArticleNameParameter, clientOrderNumberParameter, clientEANCodeParameter);
        }
    
        public virtual ObjectResult<BackSaleOrderMng_GoodsRemaining_View> BackSaleOrderMng_function_SearchGoodsRemaining(string sortedBy, string sortedDirection, string season, string clientUD, string clientNM, Nullable<int> saleID, string proformaInvoiceNo, string orderType, string articleCode, string description, string clientArticleCode, string clientArticleName, string clientOrderNumber, string clientEANCode, MergeOption mergeOption)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BackSaleOrderMng_GoodsRemaining_View>("BackSaleOrderMng_function_SearchGoodsRemaining", mergeOption, sortedByParameter, sortedDirectionParameter, seasonParameter, clientUDParameter, clientNMParameter, saleIDParameter, proformaInvoiceNoParameter, orderTypeParameter, articleCodeParameter, descriptionParameter, clientArticleCodeParameter, clientArticleNameParameter, clientOrderNumberParameter, clientEANCodeParameter);
        }
    
        public virtual ObjectResult<string> OfferMng_function_GenerateOfferCode(Nullable<int> offerID, string season, Nullable<int> saleID, Nullable<int> clientID)
        {
            var offerIDParameter = offerID.HasValue ?
                new ObjectParameter("OfferID", offerID) :
                new ObjectParameter("OfferID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("OfferMng_function_GenerateOfferCode", offerIDParameter, seasonParameter, saleIDParameter, clientIDParameter);
        }
    
        public virtual ObjectResult<string> SaleOrderMng_function_GeneratePINo(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SaleOrderMng_function_GeneratePINo", userIDParameter);
        }
    }
}
