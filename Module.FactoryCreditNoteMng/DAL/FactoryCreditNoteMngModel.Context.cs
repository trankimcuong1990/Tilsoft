﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryCreditNoteMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FactoryCreditNoteMngEntities : DbContext
    {
        public FactoryCreditNoteMngEntities()
            : base("name=FactoryCreditNoteMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FactoryCreditNote> FactoryCreditNote { get; set; }
        public virtual DbSet<FactoryCreditNoteDetail> FactoryCreditNoteDetail { get; set; }
        public virtual DbSet<FactoryCreditNoteMng_FactoryCreditNote_View> FactoryCreditNoteMng_FactoryCreditNote_View { get; set; }
        public virtual DbSet<FactoryCreditNoteMng_FactoryCreditNoteDetail_View> FactoryCreditNoteMng_FactoryCreditNoteDetail_View { get; set; }
        public virtual DbSet<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View> FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View { get; set; }
        public virtual DbSet<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View> FactoryCreditNoteMng_FactoryInvoiceSearchResult_View { get; set; }
    
        public virtual ObjectResult<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View> FactoryCreditNoteMng_function_SearchFactoryInvoice(Nullable<int> userID, Nullable<int> supplierID, string season, string query)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var queryParameter = query != null ?
                new ObjectParameter("Query", query) :
                new ObjectParameter("Query", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View>("FactoryCreditNoteMng_function_SearchFactoryInvoice", userIDParameter, supplierIDParameter, seasonParameter, queryParameter);
        }
    
        public virtual ObjectResult<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View> FactoryCreditNoteMng_function_SearchFactoryInvoice(Nullable<int> userID, Nullable<int> supplierID, string season, string query, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var queryParameter = query != null ?
                new ObjectParameter("Query", query) :
                new ObjectParameter("Query", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View>("FactoryCreditNoteMng_function_SearchFactoryInvoice", mergeOption, userIDParameter, supplierIDParameter, seasonParameter, queryParameter);
        }
    
        public virtual ObjectResult<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View> FactoryCreditNoteMng_function_SearchCreditNote(string season, Nullable<int> supplierID, Nullable<int> userID, string creditNoteNo, string sortedBy, string sortedDirection)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var creditNoteNoParameter = creditNoteNo != null ?
                new ObjectParameter("CreditNoteNo", creditNoteNo) :
                new ObjectParameter("CreditNoteNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View>("FactoryCreditNoteMng_function_SearchCreditNote", seasonParameter, supplierIDParameter, userIDParameter, creditNoteNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View> FactoryCreditNoteMng_function_SearchCreditNote(string season, Nullable<int> supplierID, Nullable<int> userID, string creditNoteNo, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var creditNoteNoParameter = creditNoteNo != null ?
                new ObjectParameter("CreditNoteNo", creditNoteNo) :
                new ObjectParameter("CreditNoteNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View>("FactoryCreditNoteMng_function_SearchCreditNote", mergeOption, seasonParameter, supplierIDParameter, userIDParameter, creditNoteNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}