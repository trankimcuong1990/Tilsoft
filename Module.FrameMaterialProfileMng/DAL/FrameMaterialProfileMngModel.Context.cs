﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FrameMaterialProfileMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FrameMaterialProfileMngEntities : DbContext
    {
        public FrameMaterialProfileMngEntities()
            : base("name=FrameMaterialProfileMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FrameMaterialProfileMng_FrameMaterialProfile_View> FrameMaterialProfileMng_FrameMaterialProfile_View { get; set; }
        public virtual DbSet<FrameMaterialProfileMng_FrameMaterialProfileFactory_View> FrameMaterialProfileMng_FrameMaterialProfileFactory_View { get; set; }
        public virtual DbSet<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View> FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View { get; set; }
        public virtual DbSet<FrameMaterialProfile> FrameMaterialProfile { get; set; }
        public virtual DbSet<FrameMaterialProfileFactory> FrameMaterialProfileFactory { get; set; }
    
        public virtual ObjectResult<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View> FrameMaterialProfileMng_function_SearchFrameMaterialProfile(string frameMaterialProfileUD, string description, Nullable<int> frameMaterialID, string factory, string sortedBy, string sortedDirection)
        {
            var frameMaterialProfileUDParameter = frameMaterialProfileUD != null ?
                new ObjectParameter("FrameMaterialProfileUD", frameMaterialProfileUD) :
                new ObjectParameter("FrameMaterialProfileUD", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var frameMaterialIDParameter = frameMaterialID.HasValue ?
                new ObjectParameter("FrameMaterialID", frameMaterialID) :
                new ObjectParameter("FrameMaterialID", typeof(int));
    
            var factoryParameter = factory != null ?
                new ObjectParameter("Factory", factory) :
                new ObjectParameter("Factory", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View>("FrameMaterialProfileMng_function_SearchFrameMaterialProfile", frameMaterialProfileUDParameter, descriptionParameter, frameMaterialIDParameter, factoryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View> FrameMaterialProfileMng_function_SearchFrameMaterialProfile(string frameMaterialProfileUD, string description, Nullable<int> frameMaterialID, string factory, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var frameMaterialProfileUDParameter = frameMaterialProfileUD != null ?
                new ObjectParameter("FrameMaterialProfileUD", frameMaterialProfileUD) :
                new ObjectParameter("FrameMaterialProfileUD", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var frameMaterialIDParameter = frameMaterialID.HasValue ?
                new ObjectParameter("FrameMaterialID", frameMaterialID) :
                new ObjectParameter("FrameMaterialID", typeof(int));
    
            var factoryParameter = factory != null ?
                new ObjectParameter("Factory", factory) :
                new ObjectParameter("Factory", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View>("FrameMaterialProfileMng_function_SearchFrameMaterialProfile", mergeOption, frameMaterialProfileUDParameter, descriptionParameter, frameMaterialIDParameter, factoryParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
