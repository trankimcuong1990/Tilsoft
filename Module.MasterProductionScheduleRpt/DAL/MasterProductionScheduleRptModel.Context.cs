﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MasterProductionScheduleRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MasterProductionScheduleRptEntities : DbContext
    {
        public MasterProductionScheduleRptEntities()
            : base("name=MasterProductionScheduleRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MasterProductionScheduleRpt_MasterProductionSchedule_View> MasterProductionScheduleRpt_MasterProductionSchedule_View { get; set; }
    }
}