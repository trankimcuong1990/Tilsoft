﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PODMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PODMngEntities : DbContext
    {
        public PODMngEntities()
            : base("name=PODMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<POD> POD { get; set; }
        public virtual DbSet<PODMng_POD_View> PODMng_POD_View { get; set; }
        public virtual DbSet<PODMng_PODSearchResult_View> PODMng_PODSearchResult_View { get; set; }
    
        public virtual ObjectResult<PODMng_PODSearchResult_View> PODMng_function_SearchPOD(string poDUD, string poDName, string sortedBy, string sortedDirection)
        {
            var poDUDParameter = poDUD != null ?
                new ObjectParameter("PoDUD", poDUD) :
                new ObjectParameter("PoDUD", typeof(string));
    
            var poDNameParameter = poDName != null ?
                new ObjectParameter("PoDName", poDName) :
                new ObjectParameter("PoDName", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PODMng_PODSearchResult_View>("PODMng_function_SearchPOD", poDUDParameter, poDNameParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PODMng_PODSearchResult_View> PODMng_function_SearchPOD(string poDUD, string poDName, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var poDUDParameter = poDUD != null ?
                new ObjectParameter("PoDUD", poDUD) :
                new ObjectParameter("PoDUD", typeof(string));
    
            var poDNameParameter = poDName != null ?
                new ObjectParameter("PoDName", poDName) :
                new ObjectParameter("PoDName", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PODMng_PODSearchResult_View>("PODMng_function_SearchPOD", mergeOption, poDUDParameter, poDNameParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
