﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ReceivingNote.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ReceivingNoteEntities : DbContext
    {
        public ReceivingNoteEntities()
            : base("name=ReceivingNoteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ReceivingNoteMng_WorkCenter_View> ReceivingNoteMng_WorkCenter_View { get; set; }
        public virtual DbSet<ReceivingNote> ReceivingNote { get; set; }
        public virtual DbSet<ReceivingNoteMng_ReceivingNote_SearchView> ReceivingNoteMng_ReceivingNote_SearchView { get; set; }
        public virtual DbSet<ReceivingNoteMng_ReceivingNote_View> ReceivingNoteMng_ReceivingNote_View { get; set; }
        public virtual DbSet<SupportMng_SubSupplierQuickSearch_View> SupportMng_SubSupplierQuickSearch_View { get; set; }
        public virtual DbSet<ProductionPrice> ProductionPrice { get; set; }
        public virtual DbSet<ReceivingNoteMng_ReceivingNoteWorkOrderDetail_View> ReceivingNoteMng_ReceivingNoteWorkOrderDetail_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_PurchaseOrder_View> ReceivingNoteMng_PurchaseOrder_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View> ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View { get; set; }
        public virtual DbSet<WorkOrder> WorkOrder { get; set; }
        public virtual DbSet<WorkCenter> WorkCenter { get; set; }
        public virtual DbSet<OPSequence> OPSequence { get; set; }
        public virtual DbSet<OPSequenceDetail> OPSequenceDetail { get; set; }
        public virtual DbSet<ReceivingNoteWorkOrderDetail> ReceivingNoteWorkOrderDetail { get; set; }
        public virtual DbSet<ReceivingNoteMng_ReceivingNoteDetailSubUnit_View> ReceivingNoteMng_ReceivingNoteDetailSubUnit_View { get; set; }
        public virtual DbSet<ProductionFrameWeight> ProductionFrameWeight { get; set; }
        public virtual DbSet<ProductionFrameWeightHistory> ProductionFrameWeightHistory { get; set; }
        public virtual DbSet<ReceivingNoteMng_WorkOrder_SearchView> ReceivingNoteMng_WorkOrder_SearchView { get; set; }
        public virtual DbSet<ProductionItem> ProductionItem { get; set; }
        public virtual DbSet<ReceivingNoteMng_FactoryWarehouse_View> ReceivingNoteMng_FactoryWarehouse_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_ProductionPrice_View> ReceivingNoteMng_ProductionPrice_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_WorkOrderItem_View> ReceivingNoteMng_WorkOrderItem_View { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<ReceivingNoteMng_TotalReceivingItem_View> ReceivingNoteMng_TotalReceivingItem_View { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual DbSet<ReceivingNoteMng_PurchaseOrderDetail_View> ReceivingNoteMng_PurchaseOrderDetail_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_SetContentDetail_View> ReceivingNoteMng_SetContentDetail_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_BOMProduction_View> ReceivingNoteMng_BOMProduction_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_TransferWorkOrder_View> ReceivingNoteMng_TransferWorkOrder_View { get; set; }
        public virtual DbSet<TransferWorkOrder> TransferWorkOrder { get; set; }
        public virtual DbSet<TransferWorkOrderDetail> TransferWorkOrderDetail { get; set; }
        public virtual DbSet<ReceivingNoteMng_NotificationGroupMember_View> ReceivingNoteMng_NotificationGroupMember_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<ReceivingNoteMng_TransportationService_View> ReceivingNoteMng_TransportationService_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_BOM_View> ReceivingNoteMng_BOM_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View> ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_ListModel_Set_View> ReceivingNoteMng_ListModel_Set_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_FactorySaleOrder_View> ReceivingNoteMng_FactorySaleOrder_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_FactorySaleOrderDetail_View> ReceivingNoteMng_FactorySaleOrderDetail_View { get; set; }
        public virtual DbSet<ReceivingNoteMng_ReceivingNoteDetail_View> ReceivingNoteMng_ReceivingNoteDetail_View { get; set; }
        public virtual DbSet<ReceivingNoteDetail> ReceivingNoteDetail { get; set; }
        public virtual DbSet<ReceivingNoteDetailSubUnit> ReceivingNoteDetailSubUnit { get; set; }
    
        public virtual ObjectResult<ReceivingNoteMng_ReceivingNote_SearchView> ReceivingNoteMng_function_SearchReceivingNote(Nullable<int> companyID, string sortedBy, string sortedDirection, string receivingNoteUD, string receivingNoteDate, string purchasingOrderNo, string workCenterNM, string fromProductionTeamNM, string workOrderUD, string modelUD, string modelNM, string productArticleCode, string productDescription, string description, string updatorName, string updatedDate, Nullable<int> receivingNoteTypeID, string workCenterAndDeliverName, string fromProductionTeamAndDeliverAddress, string fromReceivingNoteDate, string toReceivingNoteDate, Nullable<int> statusTypeID, string fromUpdatedDate, string toUpdatedDate, Nullable<int> workCenterID, Nullable<int> fromProductionTeamID)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var receivingNoteUDParameter = receivingNoteUD != null ?
                new ObjectParameter("ReceivingNoteUD", receivingNoteUD) :
                new ObjectParameter("ReceivingNoteUD", typeof(string));
    
            var receivingNoteDateParameter = receivingNoteDate != null ?
                new ObjectParameter("ReceivingNoteDate", receivingNoteDate) :
                new ObjectParameter("ReceivingNoteDate", typeof(string));
    
            var purchasingOrderNoParameter = purchasingOrderNo != null ?
                new ObjectParameter("PurchasingOrderNo", purchasingOrderNo) :
                new ObjectParameter("PurchasingOrderNo", typeof(string));
    
            var workCenterNMParameter = workCenterNM != null ?
                new ObjectParameter("WorkCenterNM", workCenterNM) :
                new ObjectParameter("WorkCenterNM", typeof(string));
    
            var fromProductionTeamNMParameter = fromProductionTeamNM != null ?
                new ObjectParameter("FromProductionTeamNM", fromProductionTeamNM) :
                new ObjectParameter("FromProductionTeamNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("WorkOrderUD", workOrderUD) :
                new ObjectParameter("WorkOrderUD", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("ModelUD", modelUD) :
                new ObjectParameter("ModelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("ModelNM", modelNM) :
                new ObjectParameter("ModelNM", typeof(string));
    
            var productArticleCodeParameter = productArticleCode != null ?
                new ObjectParameter("ProductArticleCode", productArticleCode) :
                new ObjectParameter("ProductArticleCode", typeof(string));
    
            var productDescriptionParameter = productDescription != null ?
                new ObjectParameter("ProductDescription", productDescription) :
                new ObjectParameter("ProductDescription", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var updatorNameParameter = updatorName != null ?
                new ObjectParameter("UpdatorName", updatorName) :
                new ObjectParameter("UpdatorName", typeof(string));
    
            var updatedDateParameter = updatedDate != null ?
                new ObjectParameter("UpdatedDate", updatedDate) :
                new ObjectParameter("UpdatedDate", typeof(string));
    
            var receivingNoteTypeIDParameter = receivingNoteTypeID.HasValue ?
                new ObjectParameter("ReceivingNoteTypeID", receivingNoteTypeID) :
                new ObjectParameter("ReceivingNoteTypeID", typeof(int));
    
            var workCenterAndDeliverNameParameter = workCenterAndDeliverName != null ?
                new ObjectParameter("WorkCenterAndDeliverName", workCenterAndDeliverName) :
                new ObjectParameter("WorkCenterAndDeliverName", typeof(string));
    
            var fromProductionTeamAndDeliverAddressParameter = fromProductionTeamAndDeliverAddress != null ?
                new ObjectParameter("FromProductionTeamAndDeliverAddress", fromProductionTeamAndDeliverAddress) :
                new ObjectParameter("FromProductionTeamAndDeliverAddress", typeof(string));
    
            var fromReceivingNoteDateParameter = fromReceivingNoteDate != null ?
                new ObjectParameter("FromReceivingNoteDate", fromReceivingNoteDate) :
                new ObjectParameter("FromReceivingNoteDate", typeof(string));
    
            var toReceivingNoteDateParameter = toReceivingNoteDate != null ?
                new ObjectParameter("ToReceivingNoteDate", toReceivingNoteDate) :
                new ObjectParameter("ToReceivingNoteDate", typeof(string));
    
            var statusTypeIDParameter = statusTypeID.HasValue ?
                new ObjectParameter("StatusTypeID", statusTypeID) :
                new ObjectParameter("StatusTypeID", typeof(int));
    
            var fromUpdatedDateParameter = fromUpdatedDate != null ?
                new ObjectParameter("FromUpdatedDate", fromUpdatedDate) :
                new ObjectParameter("FromUpdatedDate", typeof(string));
    
            var toUpdatedDateParameter = toUpdatedDate != null ?
                new ObjectParameter("ToUpdatedDate", toUpdatedDate) :
                new ObjectParameter("ToUpdatedDate", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var fromProductionTeamIDParameter = fromProductionTeamID.HasValue ?
                new ObjectParameter("FromProductionTeamID", fromProductionTeamID) :
                new ObjectParameter("FromProductionTeamID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_ReceivingNote_SearchView>("ReceivingNoteMng_function_SearchReceivingNote", companyIDParameter, sortedByParameter, sortedDirectionParameter, receivingNoteUDParameter, receivingNoteDateParameter, purchasingOrderNoParameter, workCenterNMParameter, fromProductionTeamNMParameter, workOrderUDParameter, modelUDParameter, modelNMParameter, productArticleCodeParameter, productDescriptionParameter, descriptionParameter, updatorNameParameter, updatedDateParameter, receivingNoteTypeIDParameter, workCenterAndDeliverNameParameter, fromProductionTeamAndDeliverAddressParameter, fromReceivingNoteDateParameter, toReceivingNoteDateParameter, statusTypeIDParameter, fromUpdatedDateParameter, toUpdatedDateParameter, workCenterIDParameter, fromProductionTeamIDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_ReceivingNote_SearchView> ReceivingNoteMng_function_SearchReceivingNote(Nullable<int> companyID, string sortedBy, string sortedDirection, string receivingNoteUD, string receivingNoteDate, string purchasingOrderNo, string workCenterNM, string fromProductionTeamNM, string workOrderUD, string modelUD, string modelNM, string productArticleCode, string productDescription, string description, string updatorName, string updatedDate, Nullable<int> receivingNoteTypeID, string workCenterAndDeliverName, string fromProductionTeamAndDeliverAddress, string fromReceivingNoteDate, string toReceivingNoteDate, Nullable<int> statusTypeID, string fromUpdatedDate, string toUpdatedDate, Nullable<int> workCenterID, Nullable<int> fromProductionTeamID, MergeOption mergeOption)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var receivingNoteUDParameter = receivingNoteUD != null ?
                new ObjectParameter("ReceivingNoteUD", receivingNoteUD) :
                new ObjectParameter("ReceivingNoteUD", typeof(string));
    
            var receivingNoteDateParameter = receivingNoteDate != null ?
                new ObjectParameter("ReceivingNoteDate", receivingNoteDate) :
                new ObjectParameter("ReceivingNoteDate", typeof(string));
    
            var purchasingOrderNoParameter = purchasingOrderNo != null ?
                new ObjectParameter("PurchasingOrderNo", purchasingOrderNo) :
                new ObjectParameter("PurchasingOrderNo", typeof(string));
    
            var workCenterNMParameter = workCenterNM != null ?
                new ObjectParameter("WorkCenterNM", workCenterNM) :
                new ObjectParameter("WorkCenterNM", typeof(string));
    
            var fromProductionTeamNMParameter = fromProductionTeamNM != null ?
                new ObjectParameter("FromProductionTeamNM", fromProductionTeamNM) :
                new ObjectParameter("FromProductionTeamNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("WorkOrderUD", workOrderUD) :
                new ObjectParameter("WorkOrderUD", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("ModelUD", modelUD) :
                new ObjectParameter("ModelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("ModelNM", modelNM) :
                new ObjectParameter("ModelNM", typeof(string));
    
            var productArticleCodeParameter = productArticleCode != null ?
                new ObjectParameter("ProductArticleCode", productArticleCode) :
                new ObjectParameter("ProductArticleCode", typeof(string));
    
            var productDescriptionParameter = productDescription != null ?
                new ObjectParameter("ProductDescription", productDescription) :
                new ObjectParameter("ProductDescription", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var updatorNameParameter = updatorName != null ?
                new ObjectParameter("UpdatorName", updatorName) :
                new ObjectParameter("UpdatorName", typeof(string));
    
            var updatedDateParameter = updatedDate != null ?
                new ObjectParameter("UpdatedDate", updatedDate) :
                new ObjectParameter("UpdatedDate", typeof(string));
    
            var receivingNoteTypeIDParameter = receivingNoteTypeID.HasValue ?
                new ObjectParameter("ReceivingNoteTypeID", receivingNoteTypeID) :
                new ObjectParameter("ReceivingNoteTypeID", typeof(int));
    
            var workCenterAndDeliverNameParameter = workCenterAndDeliverName != null ?
                new ObjectParameter("WorkCenterAndDeliverName", workCenterAndDeliverName) :
                new ObjectParameter("WorkCenterAndDeliverName", typeof(string));
    
            var fromProductionTeamAndDeliverAddressParameter = fromProductionTeamAndDeliverAddress != null ?
                new ObjectParameter("FromProductionTeamAndDeliverAddress", fromProductionTeamAndDeliverAddress) :
                new ObjectParameter("FromProductionTeamAndDeliverAddress", typeof(string));
    
            var fromReceivingNoteDateParameter = fromReceivingNoteDate != null ?
                new ObjectParameter("FromReceivingNoteDate", fromReceivingNoteDate) :
                new ObjectParameter("FromReceivingNoteDate", typeof(string));
    
            var toReceivingNoteDateParameter = toReceivingNoteDate != null ?
                new ObjectParameter("ToReceivingNoteDate", toReceivingNoteDate) :
                new ObjectParameter("ToReceivingNoteDate", typeof(string));
    
            var statusTypeIDParameter = statusTypeID.HasValue ?
                new ObjectParameter("StatusTypeID", statusTypeID) :
                new ObjectParameter("StatusTypeID", typeof(int));
    
            var fromUpdatedDateParameter = fromUpdatedDate != null ?
                new ObjectParameter("FromUpdatedDate", fromUpdatedDate) :
                new ObjectParameter("FromUpdatedDate", typeof(string));
    
            var toUpdatedDateParameter = toUpdatedDate != null ?
                new ObjectParameter("ToUpdatedDate", toUpdatedDate) :
                new ObjectParameter("ToUpdatedDate", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var fromProductionTeamIDParameter = fromProductionTeamID.HasValue ?
                new ObjectParameter("FromProductionTeamID", fromProductionTeamID) :
                new ObjectParameter("FromProductionTeamID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_ReceivingNote_SearchView>("ReceivingNoteMng_function_SearchReceivingNote", mergeOption, companyIDParameter, sortedByParameter, sortedDirectionParameter, receivingNoteUDParameter, receivingNoteDateParameter, purchasingOrderNoParameter, workCenterNMParameter, fromProductionTeamNMParameter, workOrderUDParameter, modelUDParameter, modelNMParameter, productArticleCodeParameter, productDescriptionParameter, descriptionParameter, updatorNameParameter, updatedDateParameter, receivingNoteTypeIDParameter, workCenterAndDeliverNameParameter, fromProductionTeamAndDeliverAddressParameter, fromReceivingNoteDateParameter, toReceivingNoteDateParameter, statusTypeIDParameter, fromUpdatedDateParameter, toUpdatedDateParameter, workCenterIDParameter, fromProductionTeamIDParameter);
        }
    
        public virtual int ReceivingNoteMng_function_GenerateReceivingNoteUD(Nullable<int> receivingNoteID, Nullable<int> companyID, Nullable<int> receivingYear, Nullable<int> receivingMonth)
        {
            var receivingNoteIDParameter = receivingNoteID.HasValue ?
                new ObjectParameter("ReceivingNoteID", receivingNoteID) :
                new ObjectParameter("ReceivingNoteID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var receivingYearParameter = receivingYear.HasValue ?
                new ObjectParameter("ReceivingYear", receivingYear) :
                new ObjectParameter("ReceivingYear", typeof(int));
    
            var receivingMonthParameter = receivingMonth.HasValue ?
                new ObjectParameter("ReceivingMonth", receivingMonth) :
                new ObjectParameter("ReceivingMonth", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ReceivingNoteMng_function_GenerateReceivingNoteUD", receivingNoteIDParameter, companyIDParameter, receivingYearParameter, receivingMonthParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_WorkCenter_View> ReceivingNoteMng_function_GetWorkCenter(string workOrderIDs)
        {
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_WorkCenter_View>("ReceivingNoteMng_function_GetWorkCenter", workOrderIDsParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_WorkCenter_View> ReceivingNoteMng_function_GetWorkCenter(string workOrderIDs, MergeOption mergeOption)
        {
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_WorkCenter_View>("ReceivingNoteMng_function_GetWorkCenter", mergeOption, workOrderIDsParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_function_GetProductionItem_Result> ReceivingNoteMng_function_GetProductionItem(string createdNoteDate, string searchProductionItem, Nullable<int> companyID, Nullable<int> branchID, string workOrderIDs, Nullable<int> workCenterID)
        {
            var createdNoteDateParameter = createdNoteDate != null ?
                new ObjectParameter("createdNoteDate", createdNoteDate) :
                new ObjectParameter("createdNoteDate", typeof(string));
    
            var searchProductionItemParameter = searchProductionItem != null ?
                new ObjectParameter("searchProductionItem", searchProductionItem) :
                new ObjectParameter("searchProductionItem", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("branchID", branchID) :
                new ObjectParameter("branchID", typeof(int));
    
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("workCenterID", workCenterID) :
                new ObjectParameter("workCenterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_function_GetProductionItem_Result>("ReceivingNoteMng_function_GetProductionItem", createdNoteDateParameter, searchProductionItemParameter, companyIDParameter, branchIDParameter, workOrderIDsParameter, workCenterIDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_SetContentDetail_View> ReceivingNoteMng_function_SetContentDetail(Nullable<int> item_id, Nullable<int> warehouse_id)
        {
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            var warehouse_idParameter = warehouse_id.HasValue ?
                new ObjectParameter("warehouse_id", warehouse_id) :
                new ObjectParameter("warehouse_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_SetContentDetail_View>("ReceivingNoteMng_function_SetContentDetail", item_idParameter, warehouse_idParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_SetContentDetail_View> ReceivingNoteMng_function_SetContentDetail(Nullable<int> item_id, Nullable<int> warehouse_id, MergeOption mergeOption)
        {
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            var warehouse_idParameter = warehouse_id.HasValue ?
                new ObjectParameter("warehouse_id", warehouse_id) :
                new ObjectParameter("warehouse_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_SetContentDetail_View>("ReceivingNoteMng_function_SetContentDetail", mergeOption, item_idParameter, warehouse_idParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_BOMProduction_View> ReceivingNoteMng_function_GetBOMProductionItem(string createdNoteDate, string searchProductionItem, Nullable<int> companyID, Nullable<int> branchID, string workOrderIDs, Nullable<int> workCenterID)
        {
            var createdNoteDateParameter = createdNoteDate != null ?
                new ObjectParameter("createdNoteDate", createdNoteDate) :
                new ObjectParameter("createdNoteDate", typeof(string));
    
            var searchProductionItemParameter = searchProductionItem != null ?
                new ObjectParameter("searchProductionItem", searchProductionItem) :
                new ObjectParameter("searchProductionItem", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("branchID", branchID) :
                new ObjectParameter("branchID", typeof(int));
    
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("workCenterID", workCenterID) :
                new ObjectParameter("workCenterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_BOMProduction_View>("ReceivingNoteMng_function_GetBOMProductionItem", createdNoteDateParameter, searchProductionItemParameter, companyIDParameter, branchIDParameter, workOrderIDsParameter, workCenterIDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_BOMProduction_View> ReceivingNoteMng_function_GetBOMProductionItem(string createdNoteDate, string searchProductionItem, Nullable<int> companyID, Nullable<int> branchID, string workOrderIDs, Nullable<int> workCenterID, MergeOption mergeOption)
        {
            var createdNoteDateParameter = createdNoteDate != null ?
                new ObjectParameter("createdNoteDate", createdNoteDate) :
                new ObjectParameter("createdNoteDate", typeof(string));
    
            var searchProductionItemParameter = searchProductionItem != null ?
                new ObjectParameter("searchProductionItem", searchProductionItem) :
                new ObjectParameter("searchProductionItem", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("branchID", branchID) :
                new ObjectParameter("branchID", typeof(int));
    
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("workCenterID", workCenterID) :
                new ObjectParameter("workCenterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_BOMProduction_View>("ReceivingNoteMng_function_GetBOMProductionItem", mergeOption, createdNoteDateParameter, searchProductionItemParameter, companyIDParameter, branchIDParameter, workOrderIDsParameter, workCenterIDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_TransferWorkOrder_View> ReceivingNoteMng_function_TransferNotePreToWork(string workOrderIDs, Nullable<int> workCenterID)
        {
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("WorkOrderIDs", workOrderIDs) :
                new ObjectParameter("WorkOrderIDs", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_TransferWorkOrder_View>("ReceivingNoteMng_function_TransferNotePreToWork", workOrderIDsParameter, workCenterIDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_TransferWorkOrder_View> ReceivingNoteMng_function_TransferNotePreToWork(string workOrderIDs, Nullable<int> workCenterID, MergeOption mergeOption)
        {
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("WorkOrderIDs", workOrderIDs) :
                new ObjectParameter("WorkOrderIDs", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_TransferWorkOrder_View>("ReceivingNoteMng_function_TransferNotePreToWork", mergeOption, workOrderIDsParameter, workCenterIDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_NotificationGroupMember_View> ReceivingNoteMng_function_NotificationGroupMember(string notificationGroupUD)
        {
            var notificationGroupUDParameter = notificationGroupUD != null ?
                new ObjectParameter("notificationGroupUD", notificationGroupUD) :
                new ObjectParameter("notificationGroupUD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_NotificationGroupMember_View>("ReceivingNoteMng_function_NotificationGroupMember", notificationGroupUDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_NotificationGroupMember_View> ReceivingNoteMng_function_NotificationGroupMember(string notificationGroupUD, MergeOption mergeOption)
        {
            var notificationGroupUDParameter = notificationGroupUD != null ?
                new ObjectParameter("notificationGroupUD", notificationGroupUD) :
                new ObjectParameter("notificationGroupUD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_NotificationGroupMember_View>("ReceivingNoteMng_function_NotificationGroupMember", mergeOption, notificationGroupUDParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_BOM_View> ReceivingNoteMng_function_GetBom(string workOrderIDs, Nullable<int> branchID, string receivingNoteDate)
        {
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("branchID", branchID) :
                new ObjectParameter("branchID", typeof(int));
    
            var receivingNoteDateParameter = receivingNoteDate != null ?
                new ObjectParameter("receivingNoteDate", receivingNoteDate) :
                new ObjectParameter("receivingNoteDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_BOM_View>("ReceivingNoteMng_function_GetBom", workOrderIDsParameter, branchIDParameter, receivingNoteDateParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_BOM_View> ReceivingNoteMng_function_GetBom(string workOrderIDs, Nullable<int> branchID, string receivingNoteDate, MergeOption mergeOption)
        {
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("branchID", branchID) :
                new ObjectParameter("branchID", typeof(int));
    
            var receivingNoteDateParameter = receivingNoteDate != null ?
                new ObjectParameter("receivingNoteDate", receivingNoteDate) :
                new ObjectParameter("receivingNoteDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_BOM_View>("ReceivingNoteMng_function_GetBom", mergeOption, workOrderIDsParameter, branchIDParameter, receivingNoteDateParameter);
        }
    
        public virtual int ReceivingNoteMng_function_UpdateFactoryProductionStatus(Nullable<int> factoryOrderDetailID, Nullable<int> factoryOrderID, Nullable<int> factoryID, string season, Nullable<decimal> qty, Nullable<int> weekNo)
        {
            var factoryOrderDetailIDParameter = factoryOrderDetailID.HasValue ?
                new ObjectParameter("FactoryOrderDetailID", factoryOrderDetailID) :
                new ObjectParameter("FactoryOrderDetailID", typeof(int));
    
            var factoryOrderIDParameter = factoryOrderID.HasValue ?
                new ObjectParameter("FactoryOrderID", factoryOrderID) :
                new ObjectParameter("FactoryOrderID", typeof(int));
    
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var qtyParameter = qty.HasValue ?
                new ObjectParameter("Qty", qty) :
                new ObjectParameter("Qty", typeof(decimal));
    
            var weekNoParameter = weekNo.HasValue ?
                new ObjectParameter("WeekNo", weekNo) :
                new ObjectParameter("WeekNo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ReceivingNoteMng_function_UpdateFactoryProductionStatus", factoryOrderDetailIDParameter, factoryOrderIDParameter, factoryIDParameter, seasonParameter, qtyParameter, weekNoParameter);
        }
    
        public virtual ObjectResult<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result> ReceivingNoteMng_Function_ProductionItemUnitByValidDate(Nullable<System.DateTime> validFrom, Nullable<int> productionItemID)
        {
            var validFromParameter = validFrom.HasValue ?
                new ObjectParameter("ValidFrom", validFrom) :
                new ObjectParameter("ValidFrom", typeof(System.DateTime));
    
            var productionItemIDParameter = productionItemID.HasValue ?
                new ObjectParameter("ProductionItemID", productionItemID) :
                new ObjectParameter("ProductionItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result>("ReceivingNoteMng_Function_ProductionItemUnitByValidDate", validFromParameter, productionItemIDParameter);
        }
    }
}
