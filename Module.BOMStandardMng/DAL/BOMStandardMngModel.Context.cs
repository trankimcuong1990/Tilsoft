﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BOMStandardMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BOMStandardMngEntities : DbContext
    {
        public BOMStandardMngEntities()
            : base("name=BOMStandardMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BOMStandard> BOMStandard { get; set; }
        public virtual DbSet<ProductionProcess> ProductionProcess { get; set; }
        public virtual DbSet<BOMStandardMng_BOMStandard_View> BOMStandardMng_BOMStandard_View { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductionItem> ProductionItem { get; set; }
        public virtual DbSet<ProductionItemWarehouse> ProductionItemWarehouse { get; set; }
        public virtual DbSet<BOMStandardMng_ProductionItem_View> BOMStandardMng_ProductionItem_View { get; set; }
        public virtual DbSet<WorkCenter> WorkCenter { get; set; }
        public virtual DbSet<BOMStandardMng_CreateImportTemplate_BOMStandard_View> BOMStandardMng_CreateImportTemplate_BOMStandard_View { get; set; }
        public virtual DbSet<BOMStandardMng_ProductionProcess_SearchView> BOMStandardMng_ProductionProcess_SearchView { get; set; }
        public virtual DbSet<BOMStandardMng_ProductionProcess_View> BOMStandardMng_ProductionProcess_View { get; set; }
        public virtual DbSet<BOMStandardMng_ExportToExcel_View> BOMStandardMng_ExportToExcel_View { get; set; }
        public virtual DbSet<BOMStandardMng_ProductionItemUnit_View> BOMStandardMng_ProductionItemUnit_View { get; set; }
        public virtual DbSet<BOMStandardMng_ProductionItemUnitInBOMStandard_View> BOMStandardMng_ProductionItemUnitInBOMStandard_View { get; set; }
        public virtual DbSet<BOMStandardMng_CreateImportTemplate_ProductionItemUnit_View> BOMStandardMng_CreateImportTemplate_ProductionItemUnit_View { get; set; }
        public virtual DbSet<BOMStandardMng_DateOfPrice_View> BOMStandardMng_DateOfPrice_View { get; set; }
        public virtual DbSet<BOMStandardMng_ProductionPrice_View> BOMStandardMng_ProductionPrice_View { get; set; }
        public virtual DbSet<BOMStandardMng_BOMStandardView_View> BOMStandardMng_BOMStandardView_View { get; set; }
        public virtual DbSet<BOMStandardMng_WorkCenter_View> BOMStandardMng_WorkCenter_View { get; set; }
        public virtual DbSet<BOMStandardMng_WorkOrderApply_View> BOMStandardMng_WorkOrderApply_View { get; set; }
        public virtual DbSet<BOMStandardMng_WorkOrder_View> BOMStandardMng_WorkOrder_View { get; set; }
        public virtual DbSet<BreakdownMng_BOMProductOption_View> BreakdownMng_BOMProductOption_View { get; set; }
    
        public virtual int ProductMng_function_CreateProductPiece(Nullable<int> productID)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("ProductID", productID) :
                new ObjectParameter("ProductID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ProductMng_function_CreateProductPiece", productIDParameter);
        }
    
        public virtual ObjectResult<BOMStandardMng_ProductionProcess_SearchView> BOMStandardMng_function_SearchProductionProcess(string sortedBy, string sortedDirection, Nullable<int> companyID, string productArticleCode, string productDescription, string modelUD, string sampleProductUD, string sampleArticleDescription, string oPSequenceNM)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var productArticleCodeParameter = productArticleCode != null ?
                new ObjectParameter("ProductArticleCode", productArticleCode) :
                new ObjectParameter("ProductArticleCode", typeof(string));
    
            var productDescriptionParameter = productDescription != null ?
                new ObjectParameter("ProductDescription", productDescription) :
                new ObjectParameter("ProductDescription", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("ModelUD", modelUD) :
                new ObjectParameter("ModelUD", typeof(string));
    
            var sampleProductUDParameter = sampleProductUD != null ?
                new ObjectParameter("SampleProductUD", sampleProductUD) :
                new ObjectParameter("SampleProductUD", typeof(string));
    
            var sampleArticleDescriptionParameter = sampleArticleDescription != null ?
                new ObjectParameter("SampleArticleDescription", sampleArticleDescription) :
                new ObjectParameter("SampleArticleDescription", typeof(string));
    
            var oPSequenceNMParameter = oPSequenceNM != null ?
                new ObjectParameter("OPSequenceNM", oPSequenceNM) :
                new ObjectParameter("OPSequenceNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BOMStandardMng_ProductionProcess_SearchView>("BOMStandardMng_function_SearchProductionProcess", sortedByParameter, sortedDirectionParameter, companyIDParameter, productArticleCodeParameter, productDescriptionParameter, modelUDParameter, sampleProductUDParameter, sampleArticleDescriptionParameter, oPSequenceNMParameter);
        }
    
        public virtual ObjectResult<BOMStandardMng_ProductionProcess_SearchView> BOMStandardMng_function_SearchProductionProcess(string sortedBy, string sortedDirection, Nullable<int> companyID, string productArticleCode, string productDescription, string modelUD, string sampleProductUD, string sampleArticleDescription, string oPSequenceNM, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var productArticleCodeParameter = productArticleCode != null ?
                new ObjectParameter("ProductArticleCode", productArticleCode) :
                new ObjectParameter("ProductArticleCode", typeof(string));
    
            var productDescriptionParameter = productDescription != null ?
                new ObjectParameter("ProductDescription", productDescription) :
                new ObjectParameter("ProductDescription", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("ModelUD", modelUD) :
                new ObjectParameter("ModelUD", typeof(string));
    
            var sampleProductUDParameter = sampleProductUD != null ?
                new ObjectParameter("SampleProductUD", sampleProductUD) :
                new ObjectParameter("SampleProductUD", typeof(string));
    
            var sampleArticleDescriptionParameter = sampleArticleDescription != null ?
                new ObjectParameter("SampleArticleDescription", sampleArticleDescription) :
                new ObjectParameter("SampleArticleDescription", typeof(string));
    
            var oPSequenceNMParameter = oPSequenceNM != null ?
                new ObjectParameter("OPSequenceNM", oPSequenceNM) :
                new ObjectParameter("OPSequenceNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BOMStandardMng_ProductionProcess_SearchView>("BOMStandardMng_function_SearchProductionProcess", mergeOption, sortedByParameter, sortedDirectionParameter, companyIDParameter, productArticleCodeParameter, productDescriptionParameter, modelUDParameter, sampleProductUDParameter, sampleArticleDescriptionParameter, oPSequenceNMParameter);
        }
    
        public virtual int BOMStandardMng_function_DeleteBOMStandard(Nullable<int> bOMStandardID)
        {
            var bOMStandardIDParameter = bOMStandardID.HasValue ?
                new ObjectParameter("BOMStandardID", bOMStandardID) :
                new ObjectParameter("BOMStandardID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BOMStandardMng_function_DeleteBOMStandard", bOMStandardIDParameter);
        }
    
        public virtual int BOMStandardMng_function_DeleteBOMStandardIsDeleted()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BOMStandardMng_function_DeleteBOMStandardIsDeleted");
        }
    
        public virtual int BOMStandardMng_function_AddNewApplyBOM(Nullable<int> bomStandardID, string workOrderIDs, string workCenterIDs, Nullable<int> applyTypeID)
        {
            var bomStandardIDParameter = bomStandardID.HasValue ?
                new ObjectParameter("bomStandardID", bomStandardID) :
                new ObjectParameter("bomStandardID", typeof(int));
    
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var workCenterIDsParameter = workCenterIDs != null ?
                new ObjectParameter("workCenterIDs", workCenterIDs) :
                new ObjectParameter("workCenterIDs", typeof(string));
    
            var applyTypeIDParameter = applyTypeID.HasValue ?
                new ObjectParameter("applyTypeID", applyTypeID) :
                new ObjectParameter("applyTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BOMStandardMng_function_AddNewApplyBOM", bomStandardIDParameter, workOrderIDsParameter, workCenterIDsParameter, applyTypeIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> BOMStandardMng_function_CheckDocumentNoteCreated(Nullable<int> bomStandardID, string workOrderIDs, string workCenterIDs, Nullable<int> applyTypeID)
        {
            var bomStandardIDParameter = bomStandardID.HasValue ?
                new ObjectParameter("bomStandardID", bomStandardID) :
                new ObjectParameter("bomStandardID", typeof(int));
    
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var workCenterIDsParameter = workCenterIDs != null ?
                new ObjectParameter("workCenterIDs", workCenterIDs) :
                new ObjectParameter("workCenterIDs", typeof(string));
    
            var applyTypeIDParameter = applyTypeID.HasValue ?
                new ObjectParameter("applyTypeID", applyTypeID) :
                new ObjectParameter("applyTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("BOMStandardMng_function_CheckDocumentNoteCreated", bomStandardIDParameter, workOrderIDsParameter, workCenterIDsParameter, applyTypeIDParameter);
        }
    
        public virtual int BOMStandardMng_function_ReplaceApplyBOM(Nullable<int> bomStandardID, string workOrderIDs, string workCenterIDs, Nullable<int> applyTypeID)
        {
            var bomStandardIDParameter = bomStandardID.HasValue ?
                new ObjectParameter("bomStandardID", bomStandardID) :
                new ObjectParameter("bomStandardID", typeof(int));
    
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var workCenterIDsParameter = workCenterIDs != null ?
                new ObjectParameter("workCenterIDs", workCenterIDs) :
                new ObjectParameter("workCenterIDs", typeof(string));
    
            var applyTypeIDParameter = applyTypeID.HasValue ?
                new ObjectParameter("applyTypeID", applyTypeID) :
                new ObjectParameter("applyTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BOMStandardMng_function_ReplaceApplyBOM", bomStandardIDParameter, workOrderIDsParameter, workCenterIDsParameter, applyTypeIDParameter);
        }
    
        public virtual int BOMStandardMng_function_UpdateApplyBOM(Nullable<int> bomStandardID, string workOrderIDs, string workCenterIDs, Nullable<int> applyTypeID)
        {
            var bomStandardIDParameter = bomStandardID.HasValue ?
                new ObjectParameter("bomStandardID", bomStandardID) :
                new ObjectParameter("bomStandardID", typeof(int));
    
            var workOrderIDsParameter = workOrderIDs != null ?
                new ObjectParameter("workOrderIDs", workOrderIDs) :
                new ObjectParameter("workOrderIDs", typeof(string));
    
            var workCenterIDsParameter = workCenterIDs != null ?
                new ObjectParameter("workCenterIDs", workCenterIDs) :
                new ObjectParameter("workCenterIDs", typeof(string));
    
            var applyTypeIDParameter = applyTypeID.HasValue ?
                new ObjectParameter("applyTypeID", applyTypeID) :
                new ObjectParameter("applyTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BOMStandardMng_function_UpdateApplyBOM", bomStandardIDParameter, workOrderIDsParameter, workCenterIDsParameter, applyTypeIDParameter);
        }
    }
}
