﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PriceList.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PriceListEntities : DbContext
    {
        public PriceListEntities()
            : base("name=PriceListEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PriceList> PriceList { get; set; }
        public virtual DbSet<PriceListMng_PriceList_View> PriceListMng_PriceList_View { get; set; }
    
        public virtual ObjectResult<PriceListMng_PriceList_View> PriceListMng_function_SearchPriceList(string sortedBy, string sortedDirection, string articleCode, string description)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PriceListMng_PriceList_View>("PriceListMng_function_SearchPriceList", sortedByParameter, sortedDirectionParameter, articleCodeParameter, descriptionParameter);
        }
    
        public virtual ObjectResult<PriceListMng_PriceList_View> PriceListMng_function_SearchPriceList(string sortedBy, string sortedDirection, string articleCode, string description, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PriceListMng_PriceList_View>("PriceListMng_function_SearchPriceList", mergeOption, sortedByParameter, sortedDirectionParameter, articleCodeParameter, descriptionParameter);
        }
    }
}