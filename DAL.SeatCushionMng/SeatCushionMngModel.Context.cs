﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SeatCushionMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SeatCushionMngEntities : DbContext
    {
        public SeatCushionMngEntities()
            : base("name=SeatCushionMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SeatCushion> SeatCushion { get; set; }
        public virtual DbSet<SeatCushionMng_SeatCushion_View> SeatCushionMng_SeatCushion_View { get; set; }
        public virtual DbSet<SeatCushionMng_SeatCushionSearchResult_View> SeatCushionMng_SeatCushionSearchResult_View { get; set; }
        public virtual DbSet<SeatCushionProductGroup> SeatCushionProductGroup { get; set; }
        public virtual DbSet<SeatCushionMng_SeatCushionProductGroup_View> SeatCushionMng_SeatCushionProductGroup_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<EmployeeMng_Employee_View> EmployeeMng_Employee_View { get; set; }
    
        public virtual ObjectResult<SeatCushionMng_SeatCushionSearchResult_View> SeatCushionMng_function_SearchSeatCushionMng(string seatCushionUD, string seatCushionNM, string season, Nullable<bool> isStandard, Nullable<bool> isEnabled, Nullable<int> productGroupID, string sortedBy, string sortedDirection)
        {
            var seatCushionUDParameter = seatCushionUD != null ?
                new ObjectParameter("SeatCushionUD", seatCushionUD) :
                new ObjectParameter("SeatCushionUD", typeof(string));
    
            var seatCushionNMParameter = seatCushionNM != null ?
                new ObjectParameter("SeatCushionNM", seatCushionNM) :
                new ObjectParameter("SeatCushionNM", typeof(string));
    
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SeatCushionMng_SeatCushionSearchResult_View>("SeatCushionMng_function_SearchSeatCushionMng", seatCushionUDParameter, seatCushionNMParameter, seasonParameter, isStandardParameter, isEnabledParameter, productGroupIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<SeatCushionMng_SeatCushionSearchResult_View> SeatCushionMng_function_SearchSeatCushionMng(string seatCushionUD, string seatCushionNM, string season, Nullable<bool> isStandard, Nullable<bool> isEnabled, Nullable<int> productGroupID, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var seatCushionUDParameter = seatCushionUD != null ?
                new ObjectParameter("SeatCushionUD", seatCushionUD) :
                new ObjectParameter("SeatCushionUD", typeof(string));
    
            var seatCushionNMParameter = seatCushionNM != null ?
                new ObjectParameter("SeatCushionNM", seatCushionNM) :
                new ObjectParameter("SeatCushionNM", typeof(string));
    
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SeatCushionMng_SeatCushionSearchResult_View>("SeatCushionMng_function_SearchSeatCushionMng", mergeOption, seatCushionUDParameter, seatCushionNMParameter, seasonParameter, isStandardParameter, isEnabledParameter, productGroupIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<string> SeatCushionMng_function_GenerateCode()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SeatCushionMng_function_GenerateCode");
        }
    }
}
