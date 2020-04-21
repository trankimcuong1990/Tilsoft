﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OutsourcingCostMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OutsourcingCostMngsEntities : DbContext
    {
        public OutsourcingCostMngsEntities()
            : base("name=OutsourcingCostMngsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OutsourcingCost> OutsourcingCost { get; set; }
        public virtual DbSet<OutsourcingCostMng_OutsourcingCost_SearchView> OutsourcingCostMng_OutsourcingCost_SearchView { get; set; }
        public virtual DbSet<OutsourcingCostMng_OutsourcingCost_View> OutsourcingCostMng_OutsourcingCost_View { get; set; }
        public virtual DbSet<OutsourcingCostMng_WorkCenter_View> OutsourcingCostMng_WorkCenter_View { get; set; }
    
        public virtual ObjectResult<OutsourcingCostMng_OutsourcingCost_SearchView> OutsourcingCostMng_function_SearchOutsourceCost(string outsourcingCostUD, string outsourcingCostNM, string outsourcingCostNMEN, Nullable<int> workCenterID, Nullable<bool> isAdditionalCost, string sortedBy, string sortedDirection)
        {
            var outsourcingCostUDParameter = outsourcingCostUD != null ?
                new ObjectParameter("OutsourcingCostUD", outsourcingCostUD) :
                new ObjectParameter("OutsourcingCostUD", typeof(string));
    
            var outsourcingCostNMParameter = outsourcingCostNM != null ?
                new ObjectParameter("OutsourcingCostNM", outsourcingCostNM) :
                new ObjectParameter("OutsourcingCostNM", typeof(string));
    
            var outsourcingCostNMENParameter = outsourcingCostNMEN != null ?
                new ObjectParameter("OutsourcingCostNMEN", outsourcingCostNMEN) :
                new ObjectParameter("OutsourcingCostNMEN", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var isAdditionalCostParameter = isAdditionalCost.HasValue ?
                new ObjectParameter("IsAdditionalCost", isAdditionalCost) :
                new ObjectParameter("IsAdditionalCost", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourcingCostMng_OutsourcingCost_SearchView>("OutsourcingCostMng_function_SearchOutsourceCost", outsourcingCostUDParameter, outsourcingCostNMParameter, outsourcingCostNMENParameter, workCenterIDParameter, isAdditionalCostParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<OutsourcingCostMng_OutsourcingCost_SearchView> OutsourcingCostMng_function_SearchOutsourceCost(string outsourcingCostUD, string outsourcingCostNM, string outsourcingCostNMEN, Nullable<int> workCenterID, Nullable<bool> isAdditionalCost, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var outsourcingCostUDParameter = outsourcingCostUD != null ?
                new ObjectParameter("OutsourcingCostUD", outsourcingCostUD) :
                new ObjectParameter("OutsourcingCostUD", typeof(string));
    
            var outsourcingCostNMParameter = outsourcingCostNM != null ?
                new ObjectParameter("OutsourcingCostNM", outsourcingCostNM) :
                new ObjectParameter("OutsourcingCostNM", typeof(string));
    
            var outsourcingCostNMENParameter = outsourcingCostNMEN != null ?
                new ObjectParameter("OutsourcingCostNMEN", outsourcingCostNMEN) :
                new ObjectParameter("OutsourcingCostNMEN", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var isAdditionalCostParameter = isAdditionalCost.HasValue ?
                new ObjectParameter("IsAdditionalCost", isAdditionalCost) :
                new ObjectParameter("IsAdditionalCost", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourcingCostMng_OutsourcingCost_SearchView>("OutsourcingCostMng_function_SearchOutsourceCost", mergeOption, outsourcingCostUDParameter, outsourcingCostNMParameter, outsourcingCostNMENParameter, workCenterIDParameter, isAdditionalCostParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}