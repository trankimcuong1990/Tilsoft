﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OfferReportRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OfferReportRptEntities : DbContext
    {
        public OfferReportRptEntities()
            : base("name=OfferReportRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OfferReportRpt_FOBItemOnly_ReportView> OfferReportRpt_FOBItemOnly_ReportView { get; set; }
    }
}
