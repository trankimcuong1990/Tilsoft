﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SummaryOutWardRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SummaryOutWardRptEntities : DbContext
    {
        public SummaryOutWardRptEntities()
            : base("name=SummaryOutWardRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SummaryOutWardRpt_SummaryOutWard_View> SummaryOutWardRpt_SummaryOutWard_View { get; set; }
    
        public virtual ObjectResult<SummaryOutWardRpt_SummaryOutWard_View> SummaryOutWardRpt_function_GetReportData(Nullable<int> companyID, Nullable<int> month, Nullable<int> year, string status)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("Month", month) :
                new ObjectParameter("Month", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SummaryOutWardRpt_SummaryOutWard_View>("SummaryOutWardRpt_function_GetReportData", companyIDParameter, monthParameter, yearParameter, statusParameter);
        }
    
        public virtual ObjectResult<SummaryOutWardRpt_SummaryOutWard_View> SummaryOutWardRpt_function_GetReportData(Nullable<int> companyID, Nullable<int> month, Nullable<int> year, string status, MergeOption mergeOption)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("Month", month) :
                new ObjectParameter("Month", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SummaryOutWardRpt_SummaryOutWard_View>("SummaryOutWardRpt_function_GetReportData", mergeOption, companyIDParameter, monthParameter, yearParameter, statusParameter);
        }
    }
}
