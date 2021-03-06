﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PriceListClientCharge.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PriceListClientChargeEntities : DbContext
    {
        public PriceListClientChargeEntities()
            : base("name=PriceListClientChargeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PriceListClientCharge_PriceListClientChargeSearch_View> PriceListClientCharge_PriceListClientChargeSearch_View { get; set; }
        public virtual DbSet<PriceListClientCharge_PriceListClientCharge_View> PriceListClientCharge_PriceListClientCharge_View { get; set; }
        public virtual DbSet<PriceListClientCharge_PriceListClientChargeDetail_View> PriceListClientCharge_PriceListClientChargeDetail_View { get; set; }
        public virtual DbSet<PriceListClientCharge> PriceListClientCharge { get; set; }
        public virtual DbSet<PriceListClientChargeDetail> PriceListClientChargeDetail { get; set; }
    
        public virtual ObjectResult<PriceListClientCharge_PriceListClientChargeSearch_View> PriceListClientCharge_function_PriceListClientChargeSearch(string startDate, string endDate, string clientUD, string chargeTypeID, string currencyTypeID, string sortedBy, string sortedDirection)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var chargeTypeIDParameter = chargeTypeID != null ?
                new ObjectParameter("ChargeTypeID", chargeTypeID) :
                new ObjectParameter("ChargeTypeID", typeof(string));
    
            var currencyTypeIDParameter = currencyTypeID != null ?
                new ObjectParameter("CurrencyTypeID", currencyTypeID) :
                new ObjectParameter("CurrencyTypeID", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PriceListClientCharge_PriceListClientChargeSearch_View>("PriceListClientCharge_function_PriceListClientChargeSearch", startDateParameter, endDateParameter, clientUDParameter, chargeTypeIDParameter, currencyTypeIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PriceListClientCharge_PriceListClientChargeSearch_View> PriceListClientCharge_function_PriceListClientChargeSearch(string startDate, string endDate, string clientUD, string chargeTypeID, string currencyTypeID, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var chargeTypeIDParameter = chargeTypeID != null ?
                new ObjectParameter("ChargeTypeID", chargeTypeID) :
                new ObjectParameter("ChargeTypeID", typeof(string));
    
            var currencyTypeIDParameter = currencyTypeID != null ?
                new ObjectParameter("CurrencyTypeID", currencyTypeID) :
                new ObjectParameter("CurrencyTypeID", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PriceListClientCharge_PriceListClientChargeSearch_View>("PriceListClientCharge_function_PriceListClientChargeSearch", mergeOption, startDateParameter, endDateParameter, clientUDParameter, chargeTypeIDParameter, currencyTypeIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
