﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryBreakdownMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FactoryBreakdownMngEntities : DbContext
    {
        public FactoryBreakdownMngEntities()
            : base("name=FactoryBreakdownMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FactoryBreakdown> FactoryBreakdown { get; set; }
        public virtual DbSet<FactoryBreakdownMng_FactoryBreakdown_View> FactoryBreakdownMng_FactoryBreakdown_View { get; set; }
        public virtual DbSet<FactoryBreakdownMng_FactoryBreakdownSearchResult_View> FactoryBreakdownMng_FactoryBreakdownSearchResult_View { get; set; }
        public virtual DbSet<Breakdown> Breakdown { get; set; }
        public virtual DbSet<FactoryBreakdownModel> FactoryBreakdownModel { get; set; }
        public virtual DbSet<FactoryBreakdownMng_FactoryBreakdownModel_View> FactoryBreakdownMng_FactoryBreakdownModel_View { get; set; }
        public virtual DbSet<FactoryBreakdownDetail> FactoryBreakdownDetail { get; set; }
        public virtual DbSet<FactoryBreakdownMng_FactoryBreakdownDetail_View> FactoryBreakdownMng_FactoryBreakdownDetail_View { get; set; }
    
        public virtual ObjectResult<FactoryBreakdownMng_FactoryBreakdownSearchResult_View> FactoryBreakdownMng_function_SearchFactoryBreakdown(Nullable<bool> isConfirmed, string clientUD, string sampleOrderUD, string articleDescription, string modelUD, string modelNM, string season, Nullable<int> userID, string sampleProductUD, Nullable<int> factoryID, string sortedBy, string sortedDirection)
        {
            var isConfirmedParameter = isConfirmed.HasValue ?
                new ObjectParameter("IsConfirmed", isConfirmed) :
                new ObjectParameter("IsConfirmed", typeof(bool));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var sampleOrderUDParameter = sampleOrderUD != null ?
                new ObjectParameter("SampleOrderUD", sampleOrderUD) :
                new ObjectParameter("SampleOrderUD", typeof(string));
    
            var articleDescriptionParameter = articleDescription != null ?
                new ObjectParameter("ArticleDescription", articleDescription) :
                new ObjectParameter("ArticleDescription", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("ModelUD", modelUD) :
                new ObjectParameter("ModelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("ModelNM", modelNM) :
                new ObjectParameter("ModelNM", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var sampleProductUDParameter = sampleProductUD != null ?
                new ObjectParameter("SampleProductUD", sampleProductUD) :
                new ObjectParameter("SampleProductUD", typeof(string));
    
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryBreakdownMng_FactoryBreakdownSearchResult_View>("FactoryBreakdownMng_function_SearchFactoryBreakdown", isConfirmedParameter, clientUDParameter, sampleOrderUDParameter, articleDescriptionParameter, modelUDParameter, modelNMParameter, seasonParameter, userIDParameter, sampleProductUDParameter, factoryIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactoryBreakdownMng_FactoryBreakdownSearchResult_View> FactoryBreakdownMng_function_SearchFactoryBreakdown(Nullable<bool> isConfirmed, string clientUD, string sampleOrderUD, string articleDescription, string modelUD, string modelNM, string season, Nullable<int> userID, string sampleProductUD, Nullable<int> factoryID, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var isConfirmedParameter = isConfirmed.HasValue ?
                new ObjectParameter("IsConfirmed", isConfirmed) :
                new ObjectParameter("IsConfirmed", typeof(bool));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var sampleOrderUDParameter = sampleOrderUD != null ?
                new ObjectParameter("SampleOrderUD", sampleOrderUD) :
                new ObjectParameter("SampleOrderUD", typeof(string));
    
            var articleDescriptionParameter = articleDescription != null ?
                new ObjectParameter("ArticleDescription", articleDescription) :
                new ObjectParameter("ArticleDescription", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("ModelUD", modelUD) :
                new ObjectParameter("ModelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("ModelNM", modelNM) :
                new ObjectParameter("ModelNM", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var sampleProductUDParameter = sampleProductUD != null ?
                new ObjectParameter("SampleProductUD", sampleProductUD) :
                new ObjectParameter("SampleProductUD", typeof(string));
    
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryBreakdownMng_FactoryBreakdownSearchResult_View>("FactoryBreakdownMng_function_SearchFactoryBreakdown", mergeOption, isConfirmedParameter, clientUDParameter, sampleOrderUDParameter, articleDescriptionParameter, modelUDParameter, modelNMParameter, seasonParameter, userIDParameter, sampleProductUDParameter, factoryIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FactoryBreakdownMng_function_CheckSamplePermission(Nullable<int> userID, Nullable<int> sampleProductID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var sampleProductIDParameter = sampleProductID.HasValue ?
                new ObjectParameter("SampleProductID", sampleProductID) :
                new ObjectParameter("SampleProductID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FactoryBreakdownMng_function_CheckSamplePermission", userIDParameter, sampleProductIDParameter);
        }
    
        public virtual int FactoryBreakdownMng_function_AddNewSampleProduct()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FactoryBreakdownMng_function_AddNewSampleProduct");
        }
    }
}
