﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.TransportationFeeMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TransportationFeeEntities : DbContext
    {
        public TransportationFeeEntities()
            : base("name=TransportationFeeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TransportationFeeMng_TransportationFee_View> TransportationFeeMng_TransportationFee_View { get; set; }
        public virtual DbSet<TransportationFeeMng_TransportationFeeSearchResult_View> TransportationFeeMng_TransportationFeeSearchResult_View { get; set; }
        public virtual DbSet<SaleOrder> SaleOrders { get; set; }
    
        public virtual ObjectResult<TransportationFeeMng_TransportationFeeSearchResult_View> TransportationFeeMng_function_SearchTransportationFee(Nullable<int> offerID, Nullable<decimal> transportation, Nullable<decimal> commision, string season, string sortedBy, string sortedDirection)
        {
            var offerIDParameter = offerID.HasValue ?
                new ObjectParameter("OfferID", offerID) :
                new ObjectParameter("OfferID", typeof(int));
    
            var transportationParameter = transportation.HasValue ?
                new ObjectParameter("Transportation", transportation) :
                new ObjectParameter("Transportation", typeof(decimal));
    
            var commisionParameter = commision.HasValue ?
                new ObjectParameter("Commision", commision) :
                new ObjectParameter("Commision", typeof(decimal));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TransportationFeeMng_TransportationFeeSearchResult_View>("TransportationFeeMng_function_SearchTransportationFee", offerIDParameter, transportationParameter, commisionParameter, seasonParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<TransportationFeeMng_TransportationFeeSearchResult_View> TransportationFeeMng_function_SearchTransportationFee(Nullable<int> offerID, Nullable<decimal> transportation, Nullable<decimal> commision, string season, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var offerIDParameter = offerID.HasValue ?
                new ObjectParameter("OfferID", offerID) :
                new ObjectParameter("OfferID", typeof(int));
    
            var transportationParameter = transportation.HasValue ?
                new ObjectParameter("Transportation", transportation) :
                new ObjectParameter("Transportation", typeof(decimal));
    
            var commisionParameter = commision.HasValue ?
                new ObjectParameter("Commision", commision) :
                new ObjectParameter("Commision", typeof(decimal));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TransportationFeeMng_TransportationFeeSearchResult_View>("TransportationFeeMng_function_SearchTransportationFee", mergeOption, offerIDParameter, transportationParameter, commisionParameter, seasonParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
