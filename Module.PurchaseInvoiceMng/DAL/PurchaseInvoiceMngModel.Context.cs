﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseInvoiceMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PurchaseInvoiceMngEntities : DbContext
    {
        public PurchaseInvoiceMngEntities()
            : base("name=PurchaseInvoiceMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PurchaseInvoice> PurchaseInvoice { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_FactoryWarehouse_View> PurchaseInvoiceMng_FactoryWarehouse_View { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_GetFactoryRawMaterial_View> PurchaseInvoiceMng_GetFactoryRawMaterial_View { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_ProductionItem_View> PurchaseInvoiceMng_ProductionItem_View { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_PurchaseInvoice_SearchView> PurchaseInvoiceMng_PurchaseInvoice_SearchView { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_PurchaseInvoice_View> PurchaseInvoiceMng_PurchaseInvoice_View { get; set; }
        public virtual DbSet<SupportMng_PurchaseInvoiceStatus_View> SupportMng_PurchaseInvoiceStatus_View { get; set; }
        public virtual DbSet<SupportMng_PurchaseInvoiceType_View> SupportMng_PurchaseInvoiceType_View { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_ListPaymentNoteInvoice_View> PurchaseInvoiceMng_ListPaymentNoteInvoice_View { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_SupplierPaymentTerm_View> PurchaseInvoiceMng_SupplierPaymentTerm_View { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_PurchaseOrder_View> PurchaseInvoiceMng_PurchaseOrder_View { get; set; }
        public virtual DbSet<PurchaseInvoiceDetail> PurchaseInvoiceDetail { get; set; }
        public virtual DbSet<PurchaseInvoiceMng_PurchaseInvoiceDetail_View> PurchaseInvoiceMng_PurchaseInvoiceDetail_View { get; set; }
        public virtual DbSet<PaymentNoteInvoice> PaymentNoteInvoice { get; set; }
    
        public virtual int PurchaseInvoiceMng_function_GeneratePurchaseInvoiceUD(Nullable<int> purchaseInvoiceID, Nullable<int> purchaseInvoiceYear, Nullable<int> purchaseInvoiceMonth)
        {
            var purchaseInvoiceIDParameter = purchaseInvoiceID.HasValue ?
                new ObjectParameter("PurchaseInvoiceID", purchaseInvoiceID) :
                new ObjectParameter("PurchaseInvoiceID", typeof(int));
    
            var purchaseInvoiceYearParameter = purchaseInvoiceYear.HasValue ?
                new ObjectParameter("PurchaseInvoiceYear", purchaseInvoiceYear) :
                new ObjectParameter("PurchaseInvoiceYear", typeof(int));
    
            var purchaseInvoiceMonthParameter = purchaseInvoiceMonth.HasValue ?
                new ObjectParameter("PurchaseInvoiceMonth", purchaseInvoiceMonth) :
                new ObjectParameter("PurchaseInvoiceMonth", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PurchaseInvoiceMng_function_GeneratePurchaseInvoiceUD", purchaseInvoiceIDParameter, purchaseInvoiceYearParameter, purchaseInvoiceMonthParameter);
        }
    
        public virtual ObjectResult<PurchaseInvoiceMng_PurchaseInvoice_SearchView> PurchaseInvoiceMng_function_SearchPurchaseInvoice(string purchaseInvoiceUD, Nullable<System.DateTime> purchaseInvoiceDate, Nullable<System.DateTime> postingDate, Nullable<int> factoryRawMaterialID, Nullable<int> purchaseInvoiceStatusID, Nullable<int> purchaseInvoiceTypeID, string invoiceNo, string sortedBy, string sortedDirection)
        {
            var purchaseInvoiceUDParameter = purchaseInvoiceUD != null ?
                new ObjectParameter("PurchaseInvoiceUD", purchaseInvoiceUD) :
                new ObjectParameter("PurchaseInvoiceUD", typeof(string));
    
            var purchaseInvoiceDateParameter = purchaseInvoiceDate.HasValue ?
                new ObjectParameter("PurchaseInvoiceDate", purchaseInvoiceDate) :
                new ObjectParameter("PurchaseInvoiceDate", typeof(System.DateTime));
    
            var postingDateParameter = postingDate.HasValue ?
                new ObjectParameter("PostingDate", postingDate) :
                new ObjectParameter("PostingDate", typeof(System.DateTime));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var purchaseInvoiceStatusIDParameter = purchaseInvoiceStatusID.HasValue ?
                new ObjectParameter("PurchaseInvoiceStatusID", purchaseInvoiceStatusID) :
                new ObjectParameter("PurchaseInvoiceStatusID", typeof(int));
    
            var purchaseInvoiceTypeIDParameter = purchaseInvoiceTypeID.HasValue ?
                new ObjectParameter("PurchaseInvoiceTypeID", purchaseInvoiceTypeID) :
                new ObjectParameter("PurchaseInvoiceTypeID", typeof(int));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseInvoiceMng_PurchaseInvoice_SearchView>("PurchaseInvoiceMng_function_SearchPurchaseInvoice", purchaseInvoiceUDParameter, purchaseInvoiceDateParameter, postingDateParameter, factoryRawMaterialIDParameter, purchaseInvoiceStatusIDParameter, purchaseInvoiceTypeIDParameter, invoiceNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseInvoiceMng_PurchaseInvoice_SearchView> PurchaseInvoiceMng_function_SearchPurchaseInvoice(string purchaseInvoiceUD, Nullable<System.DateTime> purchaseInvoiceDate, Nullable<System.DateTime> postingDate, Nullable<int> factoryRawMaterialID, Nullable<int> purchaseInvoiceStatusID, Nullable<int> purchaseInvoiceTypeID, string invoiceNo, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var purchaseInvoiceUDParameter = purchaseInvoiceUD != null ?
                new ObjectParameter("PurchaseInvoiceUD", purchaseInvoiceUD) :
                new ObjectParameter("PurchaseInvoiceUD", typeof(string));
    
            var purchaseInvoiceDateParameter = purchaseInvoiceDate.HasValue ?
                new ObjectParameter("PurchaseInvoiceDate", purchaseInvoiceDate) :
                new ObjectParameter("PurchaseInvoiceDate", typeof(System.DateTime));
    
            var postingDateParameter = postingDate.HasValue ?
                new ObjectParameter("PostingDate", postingDate) :
                new ObjectParameter("PostingDate", typeof(System.DateTime));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var purchaseInvoiceStatusIDParameter = purchaseInvoiceStatusID.HasValue ?
                new ObjectParameter("PurchaseInvoiceStatusID", purchaseInvoiceStatusID) :
                new ObjectParameter("PurchaseInvoiceStatusID", typeof(int));
    
            var purchaseInvoiceTypeIDParameter = purchaseInvoiceTypeID.HasValue ?
                new ObjectParameter("PurchaseInvoiceTypeID", purchaseInvoiceTypeID) :
                new ObjectParameter("PurchaseInvoiceTypeID", typeof(int));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseInvoiceMng_PurchaseInvoice_SearchView>("PurchaseInvoiceMng_function_SearchPurchaseInvoice", mergeOption, purchaseInvoiceUDParameter, purchaseInvoiceDateParameter, postingDateParameter, factoryRawMaterialIDParameter, purchaseInvoiceStatusIDParameter, purchaseInvoiceTypeIDParameter, invoiceNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseInvoiceMng_PurchaseOrder_View> PurchaseInvoiceMng_function_SearchPurchaseOrderItem(string sortedBy, string sortedDirection, string purchaseOrderUD, Nullable<System.DateTime> purchaseOrderDate, string productionItemUD, string productionItemNM, Nullable<int> factoryRawMaterialID, string receivingNoteUD, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var purchaseOrderUDParameter = purchaseOrderUD != null ?
                new ObjectParameter("PurchaseOrderUD", purchaseOrderUD) :
                new ObjectParameter("PurchaseOrderUD", typeof(string));
    
            var purchaseOrderDateParameter = purchaseOrderDate.HasValue ?
                new ObjectParameter("PurchaseOrderDate", purchaseOrderDate) :
                new ObjectParameter("PurchaseOrderDate", typeof(System.DateTime));
    
            var productionItemUDParameter = productionItemUD != null ?
                new ObjectParameter("ProductionItemUD", productionItemUD) :
                new ObjectParameter("ProductionItemUD", typeof(string));
    
            var productionItemNMParameter = productionItemNM != null ?
                new ObjectParameter("ProductionItemNM", productionItemNM) :
                new ObjectParameter("ProductionItemNM", typeof(string));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var receivingNoteUDParameter = receivingNoteUD != null ?
                new ObjectParameter("ReceivingNoteUD", receivingNoteUD) :
                new ObjectParameter("ReceivingNoteUD", typeof(string));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseInvoiceMng_PurchaseOrder_View>("PurchaseInvoiceMng_function_SearchPurchaseOrderItem", sortedByParameter, sortedDirectionParameter, purchaseOrderUDParameter, purchaseOrderDateParameter, productionItemUDParameter, productionItemNMParameter, factoryRawMaterialIDParameter, receivingNoteUDParameter, fromDateParameter, toDateParameter);
        }
    
        public virtual ObjectResult<PurchaseInvoiceMng_PurchaseOrder_View> PurchaseInvoiceMng_function_SearchPurchaseOrderItem(string sortedBy, string sortedDirection, string purchaseOrderUD, Nullable<System.DateTime> purchaseOrderDate, string productionItemUD, string productionItemNM, Nullable<int> factoryRawMaterialID, string receivingNoteUD, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var purchaseOrderUDParameter = purchaseOrderUD != null ?
                new ObjectParameter("PurchaseOrderUD", purchaseOrderUD) :
                new ObjectParameter("PurchaseOrderUD", typeof(string));
    
            var purchaseOrderDateParameter = purchaseOrderDate.HasValue ?
                new ObjectParameter("PurchaseOrderDate", purchaseOrderDate) :
                new ObjectParameter("PurchaseOrderDate", typeof(System.DateTime));
    
            var productionItemUDParameter = productionItemUD != null ?
                new ObjectParameter("ProductionItemUD", productionItemUD) :
                new ObjectParameter("ProductionItemUD", typeof(string));
    
            var productionItemNMParameter = productionItemNM != null ?
                new ObjectParameter("ProductionItemNM", productionItemNM) :
                new ObjectParameter("ProductionItemNM", typeof(string));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var receivingNoteUDParameter = receivingNoteUD != null ?
                new ObjectParameter("ReceivingNoteUD", receivingNoteUD) :
                new ObjectParameter("ReceivingNoteUD", typeof(string));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseInvoiceMng_PurchaseOrder_View>("PurchaseInvoiceMng_function_SearchPurchaseOrderItem", mergeOption, sortedByParameter, sortedDirectionParameter, purchaseOrderUDParameter, purchaseOrderDateParameter, productionItemUDParameter, productionItemNMParameter, factoryRawMaterialIDParameter, receivingNoteUDParameter, fromDateParameter, toDateParameter);
        }
    }
}
