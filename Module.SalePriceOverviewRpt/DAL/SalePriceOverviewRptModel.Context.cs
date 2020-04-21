﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SalePriceOverviewRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SalePriceOverviewRptEntities : DbContext
    {
        public SalePriceOverviewRptEntities()
            : base("name=SalePriceOverviewRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SalePriceOverviewRpt_SalePriceOverview_View> SalePriceOverviewRpt_SalePriceOverview_View { get; set; }
    
        public virtual ObjectResult<SalePriceOverviewRpt_SalePriceOverview_View> SalePriceOverviewRpt_function_GetReportData(string clientUD, string season, string modelNM, string sortedBy, string sortedDirection)
        {
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("ModelNM", modelNM) :
                new ObjectParameter("ModelNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SalePriceOverviewRpt_SalePriceOverview_View>("SalePriceOverviewRpt_function_GetReportData", clientUDParameter, seasonParameter, modelNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<SalePriceOverviewRpt_SalePriceOverview_View> SalePriceOverviewRpt_function_GetReportData(string clientUD, string season, string modelNM, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("ModelNM", modelNM) :
                new ObjectParameter("ModelNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SalePriceOverviewRpt_SalePriceOverview_View>("SalePriceOverviewRpt_function_GetReportData", mergeOption, clientUDParameter, seasonParameter, modelNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
