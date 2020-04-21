﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVFOutwardInvoiceMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AVFOutwardInvoiceMngEntities : DbContext
    {
        public AVFOutwardInvoiceMngEntities()
            : base("name=AVFOutwardInvoiceMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AVFOutwardInvoice> AVFOutwardInvoice { get; set; }
        public virtual DbSet<AVFOutwardInvoiceDetail> AVFOutwardInvoiceDetail { get; set; }
        public virtual DbSet<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View> AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View { get; set; }
        public virtual DbSet<AVFOutwardInvoiceMng_AVFOutwardInvoice_View> AVFOutwardInvoiceMng_AVFOutwardInvoice_View { get; set; }
        public virtual DbSet<AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View> AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View { get; set; }
    
        public virtual ObjectResult<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View> AVFOutwardInvoiceMng_function_SearchAVFOutwardInvoice(string creditorNM, string invoiceNo, string sortedBy, string sortedDirection)
        {
            var creditorNMParameter = creditorNM != null ?
                new ObjectParameter("CreditorNM", creditorNM) :
                new ObjectParameter("CreditorNM", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View>("AVFOutwardInvoiceMng_function_SearchAVFOutwardInvoice", creditorNMParameter, invoiceNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View> AVFOutwardInvoiceMng_function_SearchAVFOutwardInvoice(string creditorNM, string invoiceNo, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var creditorNMParameter = creditorNM != null ?
                new ObjectParameter("CreditorNM", creditorNM) :
                new ObjectParameter("CreditorNM", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View>("AVFOutwardInvoiceMng_function_SearchAVFOutwardInvoice", mergeOption, creditorNMParameter, invoiceNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}