﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MIDeltaByClientRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MIDeltaByClientRptEntities : DbContext
    {
        public MIDeltaByClientRptEntities()
            : base("name=MIDeltaByClientRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MIDeltaByClientRpt_DeltaByClient_View> MIDeltaByClientRpt_DeltaByClient_View { get; set; }
        public virtual DbSet<SupportMng_AccountManager_View> SupportMng_AccountManager_View { get; set; }
    
        public virtual ObjectResult<MIDeltaByClientRpt_DeltaByClient_View> MIDeltaByClientRpt_function_SearchData(string season, Nullable<int> saleID, Nullable<int> clientID, string sortedBy, string sortedDirection)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MIDeltaByClientRpt_DeltaByClient_View>("MIDeltaByClientRpt_function_SearchData", seasonParameter, saleIDParameter, clientIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<MIDeltaByClientRpt_DeltaByClient_View> MIDeltaByClientRpt_function_SearchData(string season, Nullable<int> saleID, Nullable<int> clientID, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MIDeltaByClientRpt_DeltaByClient_View>("MIDeltaByClientRpt_function_SearchData", mergeOption, seasonParameter, saleIDParameter, clientIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}