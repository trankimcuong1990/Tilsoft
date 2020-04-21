﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ReportFreeToSale
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ReportFTSEntities : DbContext
    {
        public ReportFTSEntities()
            : base("name=ReportFTSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ReportMng_FreeToSaleOverview_View> ReportMng_FreeToSaleOverview_View { get; set; }
    
        public virtual ObjectResult<ReportMng_FreeToSaleOverview_View> ReportMng_action_GetSearchFreeToSale(string sortedBy, string sortedDirection, string articleCode, string description, string materialNM, string materialTypeNM, string materialColorNM, string cushionNM)
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
    
            var materialNMParameter = materialNM != null ?
                new ObjectParameter("MaterialNM", materialNM) :
                new ObjectParameter("MaterialNM", typeof(string));
    
            var materialTypeNMParameter = materialTypeNM != null ?
                new ObjectParameter("MaterialTypeNM", materialTypeNM) :
                new ObjectParameter("MaterialTypeNM", typeof(string));
    
            var materialColorNMParameter = materialColorNM != null ?
                new ObjectParameter("MaterialColorNM", materialColorNM) :
                new ObjectParameter("MaterialColorNM", typeof(string));
    
            var cushionNMParameter = cushionNM != null ?
                new ObjectParameter("CushionNM", cushionNM) :
                new ObjectParameter("CushionNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReportMng_FreeToSaleOverview_View>("ReportMng_action_GetSearchFreeToSale", sortedByParameter, sortedDirectionParameter, articleCodeParameter, descriptionParameter, materialNMParameter, materialTypeNMParameter, materialColorNMParameter, cushionNMParameter);
        }
    
        public virtual ObjectResult<ReportMng_FreeToSaleOverview_View> ReportMng_action_GetSearchFreeToSale(string sortedBy, string sortedDirection, string articleCode, string description, string materialNM, string materialTypeNM, string materialColorNM, string cushionNM, MergeOption mergeOption)
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
    
            var materialNMParameter = materialNM != null ?
                new ObjectParameter("MaterialNM", materialNM) :
                new ObjectParameter("MaterialNM", typeof(string));
    
            var materialTypeNMParameter = materialTypeNM != null ?
                new ObjectParameter("MaterialTypeNM", materialTypeNM) :
                new ObjectParameter("MaterialTypeNM", typeof(string));
    
            var materialColorNMParameter = materialColorNM != null ?
                new ObjectParameter("MaterialColorNM", materialColorNM) :
                new ObjectParameter("MaterialColorNM", typeof(string));
    
            var cushionNMParameter = cushionNM != null ?
                new ObjectParameter("CushionNM", cushionNM) :
                new ObjectParameter("CushionNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReportMng_FreeToSaleOverview_View>("ReportMng_action_GetSearchFreeToSale", mergeOption, sortedByParameter, sortedDirectionParameter, articleCodeParameter, descriptionParameter, materialNMParameter, materialTypeNMParameter, materialColorNMParameter, cushionNMParameter);
        }
    }
}
