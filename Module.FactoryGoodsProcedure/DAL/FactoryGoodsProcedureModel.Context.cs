﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryGoodsProcedure.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FactoryGoodsProcedureEntities : DbContext
    {
        public FactoryGoodsProcedureEntities()
            : base("name=FactoryGoodsProcedureEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FactoryGoodsProcedure> FactoryGoodsProcedure { get; set; }
        public virtual DbSet<FactoryGoodsProcedureMng_FactoryGoodsProcedure_View> FactoryGoodsProcedureMng_FactoryGoodsProcedure_View { get; set; }
        public virtual DbSet<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View> FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View { get; set; }
        public virtual DbSet<FactoryGoodsProcedureMng_FactoryGoodsProcedureDetail_View> FactoryGoodsProcedureMng_FactoryGoodsProcedureDetail_View { get; set; }
        public virtual DbSet<FactoryGoodsProcedureDetail> FactoryGoodsProcedureDetail { get; set; }
    
        public virtual ObjectResult<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View> FactoryGoodsProcedureMng_function_SearchFactoryGoodsProcedure(string factoryGoodsProcedureNM, string factoryGoodsProcedureUD, string sortedBy, string sortedDirection)
        {
            var factoryGoodsProcedureNMParameter = factoryGoodsProcedureNM != null ?
                new ObjectParameter("FactoryGoodsProcedureNM", factoryGoodsProcedureNM) :
                new ObjectParameter("FactoryGoodsProcedureNM", typeof(string));
    
            var factoryGoodsProcedureUDParameter = factoryGoodsProcedureUD != null ?
                new ObjectParameter("FactoryGoodsProcedureUD", factoryGoodsProcedureUD) :
                new ObjectParameter("FactoryGoodsProcedureUD", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View>("FactoryGoodsProcedureMng_function_SearchFactoryGoodsProcedure", factoryGoodsProcedureNMParameter, factoryGoodsProcedureUDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View> FactoryGoodsProcedureMng_function_SearchFactoryGoodsProcedure(string factoryGoodsProcedureNM, string factoryGoodsProcedureUD, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var factoryGoodsProcedureNMParameter = factoryGoodsProcedureNM != null ?
                new ObjectParameter("FactoryGoodsProcedureNM", factoryGoodsProcedureNM) :
                new ObjectParameter("FactoryGoodsProcedureNM", typeof(string));
    
            var factoryGoodsProcedureUDParameter = factoryGoodsProcedureUD != null ?
                new ObjectParameter("FactoryGoodsProcedureUD", factoryGoodsProcedureUD) :
                new ObjectParameter("FactoryGoodsProcedureUD", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View>("FactoryGoodsProcedureMng_function_SearchFactoryGoodsProcedure", mergeOption, factoryGoodsProcedureNMParameter, factoryGoodsProcedureUDParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}