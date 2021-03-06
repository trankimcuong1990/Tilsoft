﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FactoryMngEntities : DbContext
    {
        public FactoryMngEntities()
            : base("name=FactoryMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Factory> Factory { get; set; }
        public virtual DbSet<FactoryMng_Factory_View> FactoryMng_Factory_View { get; set; }
        public virtual DbSet<FactoryMng_FactorySearchResult_View> FactoryMng_FactorySearchResult_View { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
    
        public virtual ObjectResult<FactoryMng_FactorySearchResult_View> FactoryMng_function_SearchFactory(string sortedBy, string sortedDirection)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryMng_FactorySearchResult_View>("FactoryMng_function_SearchFactory", sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactoryMng_FactorySearchResult_View> FactoryMng_function_SearchFactory(string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryMng_FactorySearchResult_View>("FactoryMng_function_SearchFactory", mergeOption, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int SyncFactoryAndUserAccessFactory()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SyncFactoryAndUserAccessFactory");
        }
    }
}
