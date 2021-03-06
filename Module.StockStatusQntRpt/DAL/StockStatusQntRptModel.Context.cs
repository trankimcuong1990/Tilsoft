﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.StockStatusQntRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StockStatusQntRptEntities : DbContext
    {
        public StockStatusQntRptEntities()
            : base("name=StockStatusQntRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ProductionItemMng_ProductionItemGroup_View> ProductionItemMng_ProductionItemGroup_View { get; set; }
        public virtual DbSet<StockStatusQntRpt_StockStatusQnt_View> StockStatusQntRpt_StockStatusQnt_View { get; set; }
        public virtual DbSet<StockStatusQntRpt_FactoryWarehouse_View> StockStatusQntRpt_FactoryWarehouse_View { get; set; }
    
        public virtual ObjectResult<StockStatusQntRpt_StockStatusQnt_View> StockStatusQntRpt_function_GetDataStockStatusQnt(Nullable<int> alertLevel, Nullable<int> factoryWarehouseID, Nullable<int> materialGroupID, string sortedOrder, string sortedDirection)
        {
            var alertLevelParameter = alertLevel.HasValue ?
                new ObjectParameter("AlertLevel", alertLevel) :
                new ObjectParameter("AlertLevel", typeof(int));
    
            var factoryWarehouseIDParameter = factoryWarehouseID.HasValue ?
                new ObjectParameter("FactoryWarehouseID", factoryWarehouseID) :
                new ObjectParameter("FactoryWarehouseID", typeof(int));
    
            var materialGroupIDParameter = materialGroupID.HasValue ?
                new ObjectParameter("MaterialGroupID", materialGroupID) :
                new ObjectParameter("MaterialGroupID", typeof(int));
    
            var sortedOrderParameter = sortedOrder != null ?
                new ObjectParameter("SortedOrder", sortedOrder) :
                new ObjectParameter("SortedOrder", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StockStatusQntRpt_StockStatusQnt_View>("StockStatusQntRpt_function_GetDataStockStatusQnt", alertLevelParameter, factoryWarehouseIDParameter, materialGroupIDParameter, sortedOrderParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<StockStatusQntRpt_StockStatusQnt_View> StockStatusQntRpt_function_GetDataStockStatusQnt(Nullable<int> alertLevel, Nullable<int> factoryWarehouseID, Nullable<int> materialGroupID, string sortedOrder, string sortedDirection, MergeOption mergeOption)
        {
            var alertLevelParameter = alertLevel.HasValue ?
                new ObjectParameter("AlertLevel", alertLevel) :
                new ObjectParameter("AlertLevel", typeof(int));
    
            var factoryWarehouseIDParameter = factoryWarehouseID.HasValue ?
                new ObjectParameter("FactoryWarehouseID", factoryWarehouseID) :
                new ObjectParameter("FactoryWarehouseID", typeof(int));
    
            var materialGroupIDParameter = materialGroupID.HasValue ?
                new ObjectParameter("MaterialGroupID", materialGroupID) :
                new ObjectParameter("MaterialGroupID", typeof(int));
    
            var sortedOrderParameter = sortedOrder != null ?
                new ObjectParameter("SortedOrder", sortedOrder) :
                new ObjectParameter("SortedOrder", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StockStatusQntRpt_StockStatusQnt_View>("StockStatusQntRpt_function_GetDataStockStatusQnt", mergeOption, alertLevelParameter, factoryWarehouseIDParameter, materialGroupIDParameter, sortedOrderParameter, sortedDirectionParameter);
        }
    }
}
