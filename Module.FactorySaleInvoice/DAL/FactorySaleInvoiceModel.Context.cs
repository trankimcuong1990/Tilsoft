﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactorySaleInvoice.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FactorySaleInvoiceEntities : DbContext
    {
        public FactorySaleInvoiceEntities()
            : base("name=FactorySaleInvoiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FactorySaleInvoiceDetail> FactorySaleInvoiceDetail { get; set; }
        public virtual DbSet<SupportMng_SubSupplier_View> SupportMng_SubSupplier_View { get; set; }
        public virtual DbSet<SupportMng_FactorySaleInvoiceStatus_View> SupportMng_FactorySaleInvoiceStatus_View { get; set; }
        public virtual DbSet<FactorySaleInvoice_FactorySaleInvoice_SeachView> FactorySaleInvoice_FactorySaleInvoice_SeachView { get; set; }
        public virtual DbSet<FactorySaleInvoice_FactorySaleInvoiceDetail_View> FactorySaleInvoice_FactorySaleInvoiceDetail_View { get; set; }
        public virtual DbSet<FactorySaleInvoice_ProductionItem_View> FactorySaleInvoice_ProductionItem_View { get; set; }
        public virtual DbSet<FactorySaleInvoice> FactorySaleInvoice { get; set; }
        public virtual DbSet<FactorySaleInvoice_FactorySaleOrderDetail_View> FactorySaleInvoice_FactorySaleOrderDetail_View { get; set; }
        public virtual DbSet<FactorySaleInvoice_SupplierPaymentTerm_View> FactorySaleInvoice_SupplierPaymentTerm_View { get; set; }
        public virtual DbSet<FactorySaleInvoice_FactorySaleInvoice_View> FactorySaleInvoice_FactorySaleInvoice_View { get; set; }
        public virtual DbSet<ReceiptNoteSaleInvoice> ReceiptNoteSaleInvoice { get; set; }
    
        public virtual ObjectResult<FactorySaleInvoice_FactorySaleInvoice_SeachView> FactorySaleInvoice_function_SearchFactorySaleInvoice(string docCode, Nullable<System.DateTime> postingDate, Nullable<int> factoryRawMaterialID, string invoiceStatus, Nullable<int> invoiceTypeID, string season, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, string sortedBy, string sortedDirection)
        {
            var docCodeParameter = docCode != null ?
                new ObjectParameter("DocCode", docCode) :
                new ObjectParameter("DocCode", typeof(string));
    
            var postingDateParameter = postingDate.HasValue ?
                new ObjectParameter("PostingDate", postingDate) :
                new ObjectParameter("PostingDate", typeof(System.DateTime));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var invoiceStatusParameter = invoiceStatus != null ?
                new ObjectParameter("InvoiceStatus", invoiceStatus) :
                new ObjectParameter("InvoiceStatus", typeof(string));
    
            var invoiceTypeIDParameter = invoiceTypeID.HasValue ?
                new ObjectParameter("InvoiceTypeID", invoiceTypeID) :
                new ObjectParameter("InvoiceTypeID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactorySaleInvoice_FactorySaleInvoice_SeachView>("FactorySaleInvoice_function_SearchFactorySaleInvoice", docCodeParameter, postingDateParameter, factoryRawMaterialIDParameter, invoiceStatusParameter, invoiceTypeIDParameter, seasonParameter, fromDateParameter, toDateParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactorySaleInvoice_FactorySaleInvoice_SeachView> FactorySaleInvoice_function_SearchFactorySaleInvoice(string docCode, Nullable<System.DateTime> postingDate, Nullable<int> factoryRawMaterialID, string invoiceStatus, Nullable<int> invoiceTypeID, string season, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var docCodeParameter = docCode != null ?
                new ObjectParameter("DocCode", docCode) :
                new ObjectParameter("DocCode", typeof(string));
    
            var postingDateParameter = postingDate.HasValue ?
                new ObjectParameter("PostingDate", postingDate) :
                new ObjectParameter("PostingDate", typeof(System.DateTime));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var invoiceStatusParameter = invoiceStatus != null ?
                new ObjectParameter("InvoiceStatus", invoiceStatus) :
                new ObjectParameter("InvoiceStatus", typeof(string));
    
            var invoiceTypeIDParameter = invoiceTypeID.HasValue ?
                new ObjectParameter("InvoiceTypeID", invoiceTypeID) :
                new ObjectParameter("InvoiceTypeID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactorySaleInvoice_FactorySaleInvoice_SeachView>("FactorySaleInvoice_function_SearchFactorySaleInvoice", mergeOption, docCodeParameter, postingDateParameter, factoryRawMaterialIDParameter, invoiceStatusParameter, invoiceTypeIDParameter, seasonParameter, fromDateParameter, toDateParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int FactorySaleInvoice_function_GenerateFactorySaleInvoiceUD(Nullable<int> factorySaleInvoiceID, Nullable<int> invoiceYear, Nullable<int> invoiceMonth)
        {
            var factorySaleInvoiceIDParameter = factorySaleInvoiceID.HasValue ?
                new ObjectParameter("FactorySaleInvoiceID", factorySaleInvoiceID) :
                new ObjectParameter("FactorySaleInvoiceID", typeof(int));
    
            var invoiceYearParameter = invoiceYear.HasValue ?
                new ObjectParameter("InvoiceYear", invoiceYear) :
                new ObjectParameter("InvoiceYear", typeof(int));
    
            var invoiceMonthParameter = invoiceMonth.HasValue ?
                new ObjectParameter("InvoiceMonth", invoiceMonth) :
                new ObjectParameter("InvoiceMonth", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FactorySaleInvoice_function_GenerateFactorySaleInvoiceUD", factorySaleInvoiceIDParameter, invoiceYearParameter, invoiceMonthParameter);
        }
    
        public virtual ObjectResult<FactorySaleInvoice_FactorySaleOrderDetail_View> FactorySaleInvoice_function_SearchFactorySaleOrderItem(string sortedBy, string sortedDirection, string factorySaleOrderUD, Nullable<System.DateTime> documentDate, string productionItemUD, string productionItemNM, Nullable<int> factoryRawMaterialID)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var factorySaleOrderUDParameter = factorySaleOrderUD != null ?
                new ObjectParameter("FactorySaleOrderUD", factorySaleOrderUD) :
                new ObjectParameter("FactorySaleOrderUD", typeof(string));
    
            var documentDateParameter = documentDate.HasValue ?
                new ObjectParameter("DocumentDate", documentDate) :
                new ObjectParameter("DocumentDate", typeof(System.DateTime));
    
            var productionItemUDParameter = productionItemUD != null ?
                new ObjectParameter("ProductionItemUD", productionItemUD) :
                new ObjectParameter("ProductionItemUD", typeof(string));
    
            var productionItemNMParameter = productionItemNM != null ?
                new ObjectParameter("ProductionItemNM", productionItemNM) :
                new ObjectParameter("ProductionItemNM", typeof(string));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactorySaleInvoice_FactorySaleOrderDetail_View>("FactorySaleInvoice_function_SearchFactorySaleOrderItem", sortedByParameter, sortedDirectionParameter, factorySaleOrderUDParameter, documentDateParameter, productionItemUDParameter, productionItemNMParameter, factoryRawMaterialIDParameter);
        }
    
        public virtual ObjectResult<FactorySaleInvoice_FactorySaleOrderDetail_View> FactorySaleInvoice_function_SearchFactorySaleOrderItem(string sortedBy, string sortedDirection, string factorySaleOrderUD, Nullable<System.DateTime> documentDate, string productionItemUD, string productionItemNM, Nullable<int> factoryRawMaterialID, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var factorySaleOrderUDParameter = factorySaleOrderUD != null ?
                new ObjectParameter("FactorySaleOrderUD", factorySaleOrderUD) :
                new ObjectParameter("FactorySaleOrderUD", typeof(string));
    
            var documentDateParameter = documentDate.HasValue ?
                new ObjectParameter("DocumentDate", documentDate) :
                new ObjectParameter("DocumentDate", typeof(System.DateTime));
    
            var productionItemUDParameter = productionItemUD != null ?
                new ObjectParameter("ProductionItemUD", productionItemUD) :
                new ObjectParameter("ProductionItemUD", typeof(string));
    
            var productionItemNMParameter = productionItemNM != null ?
                new ObjectParameter("ProductionItemNM", productionItemNM) :
                new ObjectParameter("ProductionItemNM", typeof(string));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactorySaleInvoice_FactorySaleOrderDetail_View>("FactorySaleInvoice_function_SearchFactorySaleOrderItem", mergeOption, sortedByParameter, sortedDirectionParameter, factorySaleOrderUDParameter, documentDateParameter, productionItemUDParameter, productionItemNMParameter, factoryRawMaterialIDParameter);
        }
    }
}
