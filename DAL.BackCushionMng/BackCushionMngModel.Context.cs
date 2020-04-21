﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BackCushionMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BackCushionMngEntities : DbContext
    {
        public BackCushionMngEntities()
            : base("name=BackCushionMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BackCushion> BackCushion { get; set; }
        public virtual DbSet<BackCushionMng_BackCushion_View> BackCushionMng_BackCushion_View { get; set; }
        public virtual DbSet<BackCushionMng_BackCushionSearchResult_View> BackCushionMng_BackCushionSearchResult_View { get; set; }
        public virtual DbSet<BackCushionProductGroup> BackCushionProductGroup { get; set; }
        public virtual DbSet<BackCushionMng_BackCushionProductGroup_View> BackCushionMng_BackCushionProductGroup_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<EmployeeMng_Employee_View> EmployeeMng_Employee_View { get; set; }
    
        public virtual ObjectResult<BackCushionMng_BackCushionSearchResult_View> BackCushionMng_function_SearchBackCushionMng(string backCushionUD, string backCushionNM, string season, Nullable<bool> isStandard, Nullable<bool> isEnabled, Nullable<int> productGroupID, string sortedBy, string sortedDirection)
        {
            var backCushionUDParameter = backCushionUD != null ?
                new ObjectParameter("BackCushionUD", backCushionUD) :
                new ObjectParameter("BackCushionUD", typeof(string));
    
            var backCushionNMParameter = backCushionNM != null ?
                new ObjectParameter("BackCushionNM", backCushionNM) :
                new ObjectParameter("BackCushionNM", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var isStandardParameter = isStandard.HasValue ?
                new ObjectParameter("IsStandard", isStandard) :
                new ObjectParameter("IsStandard", typeof(bool));
    
            var isEnabledParameter = isEnabled.HasValue ?
                new ObjectParameter("IsEnabled", isEnabled) :
                new ObjectParameter("IsEnabled", typeof(bool));
    
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BackCushionMng_BackCushionSearchResult_View>("BackCushionMng_function_SearchBackCushionMng", backCushionUDParameter, backCushionNMParameter, seasonParameter, isStandardParameter, isEnabledParameter, productGroupIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<BackCushionMng_BackCushionSearchResult_View> BackCushionMng_function_SearchBackCushionMng(string backCushionUD, string backCushionNM, string season, Nullable<bool> isStandard, Nullable<bool> isEnabled, Nullable<int> productGroupID, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var backCushionUDParameter = backCushionUD != null ?
                new ObjectParameter("BackCushionUD", backCushionUD) :
                new ObjectParameter("BackCushionUD", typeof(string));
    
            var backCushionNMParameter = backCushionNM != null ?
                new ObjectParameter("BackCushionNM", backCushionNM) :
                new ObjectParameter("BackCushionNM", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var isStandardParameter = isStandard.HasValue ?
                new ObjectParameter("IsStandard", isStandard) :
                new ObjectParameter("IsStandard", typeof(bool));
    
            var isEnabledParameter = isEnabled.HasValue ?
                new ObjectParameter("IsEnabled", isEnabled) :
                new ObjectParameter("IsEnabled", typeof(bool));
    
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BackCushionMng_BackCushionSearchResult_View>("BackCushionMng_function_SearchBackCushionMng", mergeOption, backCushionUDParameter, backCushionNMParameter, seasonParameter, isStandardParameter, isEnabledParameter, productGroupIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<string> BackCushionMng_function_GenerateCode()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("BackCushionMng_function_GenerateCode");
        }
    }
}