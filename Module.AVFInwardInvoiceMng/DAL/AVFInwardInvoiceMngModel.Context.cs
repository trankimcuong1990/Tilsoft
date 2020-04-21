﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVFInwardInvoiceMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AVFInwardInvoiceMngEntities : DbContext
    {
        public AVFInwardInvoiceMngEntities()
            : base("name=AVFInwardInvoiceMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View> AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View { get; set; }
        public virtual DbSet<AVFInwardInvoiceMng_AVFInwardInvoice_View> AVFInwardInvoiceMng_AVFInwardInvoice_View { get; set; }
    
        public virtual ObjectResult<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View> AVFInwardInvoiceMng_function_SearchAVFInwardInvoice(string supplierNM, string invoiceNo, string sortedBy, string sortedDirection)
        {
            var supplierNMParameter = supplierNM != null ?
                new ObjectParameter("SupplierNM", supplierNM) :
                new ObjectParameter("SupplierNM", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View>("AVFInwardInvoiceMng_function_SearchAVFInwardInvoice", supplierNMParameter, invoiceNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View> AVFInwardInvoiceMng_function_SearchAVFInwardInvoice(string supplierNM, string invoiceNo, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var supplierNMParameter = supplierNM != null ?
                new ObjectParameter("SupplierNM", supplierNM) :
                new ObjectParameter("SupplierNM", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View>("AVFInwardInvoiceMng_function_SearchAVFInwardInvoice", mergeOption, supplierNMParameter, invoiceNoParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
