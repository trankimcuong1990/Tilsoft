﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PreOrderPlanningOverviewRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PreOrderPlanningOverviewRptEntities : DbContext
    {
        public PreOrderPlanningOverviewRptEntities()
            : base("name=PreOrderPlanningOverviewRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<WeekInfo> WeekInfo { get; set; }
        public virtual DbSet<FactoryPlanningSetting> FactoryPlanningSetting { get; set; }
    
        public virtual ObjectResult<PreOrderPlanningOverviewRpt_function_GetWeeklyOverview_Result> PreOrderPlanningOverviewRpt_function_GetWeeklyOverview(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PreOrderPlanningOverviewRpt_function_GetWeeklyOverview_Result>("PreOrderPlanningOverviewRpt_function_GetWeeklyOverview", startDateParameter, endDateParameter);
        }
    }
}