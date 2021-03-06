﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LedgerAccountMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LedgerAccountMngEntities : DbContext
    {
        public LedgerAccountMngEntities()
            : base("name=LedgerAccountMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LedgerAccount> LedgerAccount { get; set; }
        public virtual DbSet<LedgerAccountMng_LedgerAccount_View> LedgerAccountMng_LedgerAccount_View { get; set; }
        public virtual DbSet<LedgerAccountMng_LedgerAccountSearchResult_View> LedgerAccountMng_LedgerAccountSearchResult_View { get; set; }
        public virtual DbSet<LedgerAccountMng_LedgerAccountDetailOverview_View> LedgerAccountMng_LedgerAccountDetailOverview_View { get; set; }
    
        public virtual ObjectResult<LedgerAccountMng_LedgerAccountSearchResult_View> LedgerAccountMng_function_SearchLedgerAccount(string accountNo, string vietnameseNM, string englishNM, string sortedBy, string sortedDirection)
        {
            var accountNoParameter = accountNo != null ?
                new ObjectParameter("AccountNo", accountNo) :
                new ObjectParameter("AccountNo", typeof(string));
    
            var vietnameseNMParameter = vietnameseNM != null ?
                new ObjectParameter("VietnameseNM", vietnameseNM) :
                new ObjectParameter("VietnameseNM", typeof(string));
    
            var englishNMParameter = englishNM != null ?
                new ObjectParameter("EnglishNM", englishNM) :
                new ObjectParameter("EnglishNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LedgerAccountMng_LedgerAccountSearchResult_View>("LedgerAccountMng_function_SearchLedgerAccount", accountNoParameter, vietnameseNMParameter, englishNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<LedgerAccountMng_LedgerAccountSearchResult_View> LedgerAccountMng_function_SearchLedgerAccount(string accountNo, string vietnameseNM, string englishNM, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var accountNoParameter = accountNo != null ?
                new ObjectParameter("AccountNo", accountNo) :
                new ObjectParameter("AccountNo", typeof(string));
    
            var vietnameseNMParameter = vietnameseNM != null ?
                new ObjectParameter("VietnameseNM", vietnameseNM) :
                new ObjectParameter("VietnameseNM", typeof(string));
    
            var englishNMParameter = englishNM != null ?
                new ObjectParameter("EnglishNM", englishNM) :
                new ObjectParameter("EnglishNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LedgerAccountMng_LedgerAccountSearchResult_View>("LedgerAccountMng_function_SearchLedgerAccount", mergeOption, accountNoParameter, vietnameseNMParameter, englishNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
