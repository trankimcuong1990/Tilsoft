﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DueColorMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DueColorMngEntities : DbContext
    {
        public DueColorMngEntities()
            : base("name=DueColorMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DueColor> DueColor { get; set; }
        public virtual DbSet<DueColorMng_DueColor_SearchView> DueColorMng_DueColor_SearchView { get; set; }
        public virtual DbSet<DueColorMng_DueColor_View> DueColorMng_DueColor_View { get; set; }
    
        public virtual ObjectResult<DueColorMng_DueColor_SearchView> DueColorMng_function_SearchDueColor(string dueColorNM, string sortedBy, string sortedDirection)
        {
            var dueColorNMParameter = dueColorNM != null ?
                new ObjectParameter("DueColorNM", dueColorNM) :
                new ObjectParameter("DueColorNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DueColorMng_DueColor_SearchView>("DueColorMng_function_SearchDueColor", dueColorNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<DueColorMng_DueColor_SearchView> DueColorMng_function_SearchDueColor(string dueColorNM, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var dueColorNMParameter = dueColorNM != null ?
                new ObjectParameter("DueColorNM", dueColorNM) :
                new ObjectParameter("DueColorNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DueColorMng_DueColor_SearchView>("DueColorMng_function_SearchDueColor", mergeOption, dueColorNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
