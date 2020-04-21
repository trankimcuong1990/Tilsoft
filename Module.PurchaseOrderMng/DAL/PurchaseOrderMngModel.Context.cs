﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseOrderMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PurchaseOrderMngEntities : DbContext
    {
        public PurchaseOrderMngEntities()
            : base("name=PurchaseOrderMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual DbSet<PurchaseOrderWorkOrderDetail> PurchaseOrderWorkOrderDetail { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseOrderDetail_View> PurchaseOrderMng_PurchaseOrderDetail_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View> PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_SearchPurchaseOrder_View> PurchaseOrderMng_SearchPurchaseOrder_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_FactoryRawMaterial_View> PurchaseOrderMng_FactoryRawMaterial_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseRequest_View> PurchaseOrderMng_PurchaseRequest_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseRequestDetailApproval_View> PurchaseOrderMng_PurchaseRequestDetailApproval_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View> PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View> PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View { get; set; }
        public virtual DbSet<PurchaseOrderDetailPriceHistory> PurchaseOrderDetailPriceHistory { get; set; }
        public virtual DbSet<PurchaseOrderDetailReceivingPlan> PurchaseOrderDetailReceivingPlan { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseRequestWorkOrder_View> PurchaseOrderMng_PurchaseRequestWorkOrder_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_SupplierPaymentTerm_View> PurchaseOrderMng_SupplierPaymentTerm_View { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseOrder_View> PurchaseOrderMng_PurchaseOrder_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_ReceivingNote_View> PurchaseOrderMng_ReceivingNote_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<SupportMng_NotificationGroup_View> SupportMng_NotificationGroup_View { get; set; }
        public virtual DbSet<ReceivingNoteDetail> ReceivingNoteDetail { get; set; }
        public virtual DbSet<PurchaseOrderMng_ReceivingNoteByPurchaseOrder_View> PurchaseOrderMng_ReceivingNoteByPurchaseOrder_View { get; set; }
        public virtual DbSet<PurchaseOrderMng_PurchaseQuotationAndSupplier_View> PurchaseOrderMng_PurchaseQuotationAndSupplier_View { get; set; }
        public virtual DbSet<NotificationMessage> NotificationMessage { get; set; }
    
        public virtual ObjectResult<PurchaseOrderMng_SearchPurchaseOrder_View> PurchaseOrderMng_function_SearchPurchaseOrder(string season, string purchaseOrderUD, Nullable<System.DateTime> purchaseOrderDate, string factoryRawMaterialShortNM, string purchaseRequestUD, Nullable<System.DateTime> eTA, string currency, string sortedBy, string sortedDirection, string remark, Nullable<int> pOStatus, string purchaseOrderIDs)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var purchaseOrderUDParameter = purchaseOrderUD != null ?
                new ObjectParameter("PurchaseOrderUD", purchaseOrderUD) :
                new ObjectParameter("PurchaseOrderUD", typeof(string));
    
            var purchaseOrderDateParameter = purchaseOrderDate.HasValue ?
                new ObjectParameter("PurchaseOrderDate", purchaseOrderDate) :
                new ObjectParameter("PurchaseOrderDate", typeof(System.DateTime));
    
            var factoryRawMaterialShortNMParameter = factoryRawMaterialShortNM != null ?
                new ObjectParameter("FactoryRawMaterialShortNM", factoryRawMaterialShortNM) :
                new ObjectParameter("FactoryRawMaterialShortNM", typeof(string));
    
            var purchaseRequestUDParameter = purchaseRequestUD != null ?
                new ObjectParameter("PurchaseRequestUD", purchaseRequestUD) :
                new ObjectParameter("PurchaseRequestUD", typeof(string));
    
            var eTAParameter = eTA.HasValue ?
                new ObjectParameter("ETA", eTA) :
                new ObjectParameter("ETA", typeof(System.DateTime));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            var pOStatusParameter = pOStatus.HasValue ?
                new ObjectParameter("POStatus", pOStatus) :
                new ObjectParameter("POStatus", typeof(int));
    
            var purchaseOrderIDsParameter = purchaseOrderIDs != null ?
                new ObjectParameter("PurchaseOrderIDs", purchaseOrderIDs) :
                new ObjectParameter("PurchaseOrderIDs", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderMng_SearchPurchaseOrder_View>("PurchaseOrderMng_function_SearchPurchaseOrder", seasonParameter, purchaseOrderUDParameter, purchaseOrderDateParameter, factoryRawMaterialShortNMParameter, purchaseRequestUDParameter, eTAParameter, currencyParameter, sortedByParameter, sortedDirectionParameter, remarkParameter, pOStatusParameter, purchaseOrderIDsParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderMng_SearchPurchaseOrder_View> PurchaseOrderMng_function_SearchPurchaseOrder(string season, string purchaseOrderUD, Nullable<System.DateTime> purchaseOrderDate, string factoryRawMaterialShortNM, string purchaseRequestUD, Nullable<System.DateTime> eTA, string currency, string sortedBy, string sortedDirection, string remark, Nullable<int> pOStatus, string purchaseOrderIDs, MergeOption mergeOption)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var purchaseOrderUDParameter = purchaseOrderUD != null ?
                new ObjectParameter("PurchaseOrderUD", purchaseOrderUD) :
                new ObjectParameter("PurchaseOrderUD", typeof(string));
    
            var purchaseOrderDateParameter = purchaseOrderDate.HasValue ?
                new ObjectParameter("PurchaseOrderDate", purchaseOrderDate) :
                new ObjectParameter("PurchaseOrderDate", typeof(System.DateTime));
    
            var factoryRawMaterialShortNMParameter = factoryRawMaterialShortNM != null ?
                new ObjectParameter("FactoryRawMaterialShortNM", factoryRawMaterialShortNM) :
                new ObjectParameter("FactoryRawMaterialShortNM", typeof(string));
    
            var purchaseRequestUDParameter = purchaseRequestUD != null ?
                new ObjectParameter("PurchaseRequestUD", purchaseRequestUD) :
                new ObjectParameter("PurchaseRequestUD", typeof(string));
    
            var eTAParameter = eTA.HasValue ?
                new ObjectParameter("ETA", eTA) :
                new ObjectParameter("ETA", typeof(System.DateTime));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            var pOStatusParameter = pOStatus.HasValue ?
                new ObjectParameter("POStatus", pOStatus) :
                new ObjectParameter("POStatus", typeof(int));
    
            var purchaseOrderIDsParameter = purchaseOrderIDs != null ?
                new ObjectParameter("PurchaseOrderIDs", purchaseOrderIDs) :
                new ObjectParameter("PurchaseOrderIDs", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderMng_SearchPurchaseOrder_View>("PurchaseOrderMng_function_SearchPurchaseOrder", mergeOption, seasonParameter, purchaseOrderUDParameter, purchaseOrderDateParameter, factoryRawMaterialShortNMParameter, purchaseRequestUDParameter, eTAParameter, currencyParameter, sortedByParameter, sortedDirectionParameter, remarkParameter, pOStatusParameter, purchaseOrderIDsParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderMng_PurchaseRequest_View> PurchaseOrderMng_function_PurchaseRequest(string searchString, Nullable<int> searchString2)
        {
            var searchStringParameter = searchString != null ?
                new ObjectParameter("SearchString", searchString) :
                new ObjectParameter("SearchString", typeof(string));
    
            var searchString2Parameter = searchString2.HasValue ?
                new ObjectParameter("SearchString2", searchString2) :
                new ObjectParameter("SearchString2", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderMng_PurchaseRequest_View>("PurchaseOrderMng_function_PurchaseRequest", searchStringParameter, searchString2Parameter);
        }
    
        public virtual ObjectResult<PurchaseOrderMng_PurchaseRequest_View> PurchaseOrderMng_function_PurchaseRequest(string searchString, Nullable<int> searchString2, MergeOption mergeOption)
        {
            var searchStringParameter = searchString != null ?
                new ObjectParameter("SearchString", searchString) :
                new ObjectParameter("SearchString", typeof(string));
    
            var searchString2Parameter = searchString2.HasValue ?
                new ObjectParameter("SearchString2", searchString2) :
                new ObjectParameter("SearchString2", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderMng_PurchaseRequest_View>("PurchaseOrderMng_function_PurchaseRequest", mergeOption, searchStringParameter, searchString2Parameter);
        }
    
        public virtual int PurchaseOrderMng_function_GeneratePurchaseOrderUD(string season, Nullable<int> supplierID, Nullable<int> purchaseOrderID)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            var purchaseOrderIDParameter = purchaseOrderID.HasValue ?
                new ObjectParameter("PurchaseOrderID", purchaseOrderID) :
                new ObjectParameter("PurchaseOrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PurchaseOrderMng_function_GeneratePurchaseOrderUD", seasonParameter, supplierIDParameter, purchaseOrderIDParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderMng_PurchaseQuotationAndSupplier_View> PurchaseOrderMng_function_GetListMaterial_PurchaseQuotation_Supplier(Nullable<int> factoryRawMaterialID, Nullable<System.DateTime> purchaseOrderDate)
        {
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var purchaseOrderDateParameter = purchaseOrderDate.HasValue ?
                new ObjectParameter("PurchaseOrderDate", purchaseOrderDate) :
                new ObjectParameter("PurchaseOrderDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderMng_PurchaseQuotationAndSupplier_View>("PurchaseOrderMng_function_GetListMaterial_PurchaseQuotation_Supplier", factoryRawMaterialIDParameter, purchaseOrderDateParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderMng_PurchaseQuotationAndSupplier_View> PurchaseOrderMng_function_GetListMaterial_PurchaseQuotation_Supplier(Nullable<int> factoryRawMaterialID, Nullable<System.DateTime> purchaseOrderDate, MergeOption mergeOption)
        {
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var purchaseOrderDateParameter = purchaseOrderDate.HasValue ?
                new ObjectParameter("PurchaseOrderDate", purchaseOrderDate) :
                new ObjectParameter("PurchaseOrderDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderMng_PurchaseQuotationAndSupplier_View>("PurchaseOrderMng_function_GetListMaterial_PurchaseQuotation_Supplier", mergeOption, factoryRawMaterialIDParameter, purchaseOrderDateParameter);
        }
    }
}