﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.EurofarSampleCollectionMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EurofarSampleCollectionMngEntities : DbContext
    {
        public EurofarSampleCollectionMngEntities()
            : base("name=EurofarSampleCollectionMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EurofarSampleCollectionMng_SampleProduct_View> EurofarSampleCollectionMng_SampleProduct_View { get; set; }
        public virtual DbSet<SampleProduct> SampleProduct { get; set; }
    
        public virtual ObjectResult<EurofarSampleCollectionMng_SampleProduct_View> EurofarSampleCollectionMng_function_SampleProduct(string sortedBy, string sortedDirection, string client, string season, string desciption, string sampleOrderUD)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientParameter = client != null ?
                new ObjectParameter("Client", client) :
                new ObjectParameter("Client", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var desciptionParameter = desciption != null ?
                new ObjectParameter("Desciption", desciption) :
                new ObjectParameter("Desciption", typeof(string));
    
            var sampleOrderUDParameter = sampleOrderUD != null ?
                new ObjectParameter("SampleOrderUD", sampleOrderUD) :
                new ObjectParameter("SampleOrderUD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarSampleCollectionMng_SampleProduct_View>("EurofarSampleCollectionMng_function_SampleProduct", sortedByParameter, sortedDirectionParameter, clientParameter, seasonParameter, desciptionParameter, sampleOrderUDParameter);
        }
    
        public virtual ObjectResult<EurofarSampleCollectionMng_SampleProduct_View> EurofarSampleCollectionMng_function_SampleProduct(string sortedBy, string sortedDirection, string client, string season, string desciption, string sampleOrderUD, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientParameter = client != null ?
                new ObjectParameter("Client", client) :
                new ObjectParameter("Client", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var desciptionParameter = desciption != null ?
                new ObjectParameter("Desciption", desciption) :
                new ObjectParameter("Desciption", typeof(string));
    
            var sampleOrderUDParameter = sampleOrderUD != null ?
                new ObjectParameter("SampleOrderUD", sampleOrderUD) :
                new ObjectParameter("SampleOrderUD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EurofarSampleCollectionMng_SampleProduct_View>("EurofarSampleCollectionMng_function_SampleProduct", mergeOption, sortedByParameter, sortedDirectionParameter, clientParameter, seasonParameter, desciptionParameter, sampleOrderUDParameter);
        }
    }
}
