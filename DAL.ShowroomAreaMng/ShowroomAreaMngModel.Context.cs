﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ShowroomAreaMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ShowroomAreaMngEntities : DbContext
    {
        public ShowroomAreaMngEntities()
            : base("name=ShowroomAreaMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ShowroomAreaMng_ShowroomArea_SearchView> ShowroomAreaMng_ShowroomArea_SearchView { get; set; }
        public virtual DbSet<ShowroomAreaMng_ShowroomArea_View> ShowroomAreaMng_ShowroomArea_View { get; set; }
        public virtual DbSet<ShowroomAreaMng_ShowroomAreaImage_View> ShowroomAreaMng_ShowroomAreaImage_View { get; set; }
        public virtual DbSet<ShowroomArea> ShowroomArea { get; set; }
        public virtual DbSet<ShowroomAreaImage> ShowroomAreaImage { get; set; }
    
        public virtual ObjectResult<ShowroomAreaMng_ShowroomArea_SearchView> ShowroomAreaMng_function_SearchShowroomArea(string sortedBy, string sortedDirection, string showroomAreaUD, string showroomAreaNM)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var showroomAreaUDParameter = showroomAreaUD != null ?
                new ObjectParameter("ShowroomAreaUD", showroomAreaUD) :
                new ObjectParameter("ShowroomAreaUD", typeof(string));
    
            var showroomAreaNMParameter = showroomAreaNM != null ?
                new ObjectParameter("ShowroomAreaNM", showroomAreaNM) :
                new ObjectParameter("ShowroomAreaNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ShowroomAreaMng_ShowroomArea_SearchView>("ShowroomAreaMng_function_SearchShowroomArea", sortedByParameter, sortedDirectionParameter, showroomAreaUDParameter, showroomAreaNMParameter);
        }
    
        public virtual ObjectResult<ShowroomAreaMng_ShowroomArea_SearchView> ShowroomAreaMng_function_SearchShowroomArea(string sortedBy, string sortedDirection, string showroomAreaUD, string showroomAreaNM, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var showroomAreaUDParameter = showroomAreaUD != null ?
                new ObjectParameter("ShowroomAreaUD", showroomAreaUD) :
                new ObjectParameter("ShowroomAreaUD", typeof(string));
    
            var showroomAreaNMParameter = showroomAreaNM != null ?
                new ObjectParameter("ShowroomAreaNM", showroomAreaNM) :
                new ObjectParameter("ShowroomAreaNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ShowroomAreaMng_ShowroomArea_SearchView>("ShowroomAreaMng_function_SearchShowroomArea", mergeOption, sortedByParameter, sortedDirectionParameter, showroomAreaUDParameter, showroomAreaNMParameter);
        }
    }
}