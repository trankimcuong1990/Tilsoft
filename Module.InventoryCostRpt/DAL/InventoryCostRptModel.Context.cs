﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.InventoryCostRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InventoryCostRptEntities : DbContext
    {
        public InventoryCostRptEntities()
            : base("name=InventoryCostRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<InventoryCostRpt_FactoryWarehouse_View> InventoryCostRpt_FactoryWarehouse_View { get; set; }
        public virtual DbSet<InventoryCostRpt_InventoryCost_View> InventoryCostRpt_InventoryCost_View { get; set; }
        public virtual DbSet<InventoryCostRpt_ProductionItemType_View> InventoryCostRpt_ProductionItemType_View { get; set; }
        public virtual DbSet<SupportMng_BaseEmployee_View> SupportMng_BaseEmployee_View { get; set; }
    
        public virtual ObjectResult<InventoryCostRpt_InventoryCost_View> InventoryCostRpt_function_InventoryCostSearchResult(Nullable<int> factoryWarehouseID, string startDate, string endDate, Nullable<int> branchID, Nullable<int> productionItemTypeID)
        {
            var factoryWarehouseIDParameter = factoryWarehouseID.HasValue ?
                new ObjectParameter("FactoryWarehouseID", factoryWarehouseID) :
                new ObjectParameter("FactoryWarehouseID", typeof(int));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(string));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("BranchID", branchID) :
                new ObjectParameter("BranchID", typeof(int));
    
            var productionItemTypeIDParameter = productionItemTypeID.HasValue ?
                new ObjectParameter("ProductionItemTypeID", productionItemTypeID) :
                new ObjectParameter("ProductionItemTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InventoryCostRpt_InventoryCost_View>("InventoryCostRpt_function_InventoryCostSearchResult", factoryWarehouseIDParameter, startDateParameter, endDateParameter, branchIDParameter, productionItemTypeIDParameter);
        }
    
        public virtual ObjectResult<InventoryCostRpt_InventoryCost_View> InventoryCostRpt_function_InventoryCostSearchResult(Nullable<int> factoryWarehouseID, string startDate, string endDate, Nullable<int> branchID, Nullable<int> productionItemTypeID, MergeOption mergeOption)
        {
            var factoryWarehouseIDParameter = factoryWarehouseID.HasValue ?
                new ObjectParameter("FactoryWarehouseID", factoryWarehouseID) :
                new ObjectParameter("FactoryWarehouseID", typeof(int));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(string));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("BranchID", branchID) :
                new ObjectParameter("BranchID", typeof(int));
    
            var productionItemTypeIDParameter = productionItemTypeID.HasValue ?
                new ObjectParameter("ProductionItemTypeID", productionItemTypeID) :
                new ObjectParameter("ProductionItemTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InventoryCostRpt_InventoryCost_View>("InventoryCostRpt_function_InventoryCostSearchResult", mergeOption, factoryWarehouseIDParameter, startDateParameter, endDateParameter, branchIDParameter, productionItemTypeIDParameter);
        }
    }
}
