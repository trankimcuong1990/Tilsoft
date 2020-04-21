﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionItem.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ProductionItem> ProductionItem { get; set; }
        public virtual DbSet<ProductionItemWarehouse> ProductionItemWarehouse { get; set; }
        public virtual DbSet<ProductionItemMng_ProductionItem_SearchView> ProductionItemMng_ProductionItem_SearchView { get; set; }
        public virtual DbSet<ProductionItemMng_ProductionItem_View> ProductionItemMng_ProductionItem_View { get; set; }
    
        public virtual int ProductionItemMng_function_SearchProductionItem(string productionItemUD, string productionItemNM, Nullable<int> productionItemTypeID, Nullable<int> productionItemGroupID, string factoryWarehouseIDs, Nullable<bool> status, string sortedBy, string sortedDirection)
        {
            var productionItemUDParameter = productionItemUD != null ?
                new ObjectParameter("ProductionItemUD", productionItemUD) :
                new ObjectParameter("ProductionItemUD", typeof(string));
    
            var productionItemNMParameter = productionItemNM != null ?
                new ObjectParameter("ProductionItemNM", productionItemNM) :
                new ObjectParameter("ProductionItemNM", typeof(string));
    
            var productionItemTypeIDParameter = productionItemTypeID.HasValue ?
                new ObjectParameter("ProductionItemTypeID", productionItemTypeID) :
                new ObjectParameter("ProductionItemTypeID", typeof(int));
    
            var productionItemGroupIDParameter = productionItemGroupID.HasValue ?
                new ObjectParameter("ProductionItemGroupID", productionItemGroupID) :
                new ObjectParameter("ProductionItemGroupID", typeof(int));
    
            var factoryWarehouseIDsParameter = factoryWarehouseIDs != null ?
                new ObjectParameter("FactoryWarehouseIDs", factoryWarehouseIDs) :
                new ObjectParameter("FactoryWarehouseIDs", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ProductionItemMng_function_SearchProductionItem", productionItemUDParameter, productionItemNMParameter, productionItemTypeIDParameter, productionItemGroupIDParameter, factoryWarehouseIDsParameter, statusParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}