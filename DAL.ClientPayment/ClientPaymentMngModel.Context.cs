﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientPaymentMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ClientPaymentMngEntities : DbContext
    {
        public ClientPaymentMngEntities()
            : base("name=ClientPaymentMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClientPayment> ClientPayment { get; set; }
        public virtual DbSet<ClientPaymentMng_ClientPayment_View> ClientPaymentMng_ClientPayment_View { get; set; }
        public virtual DbSet<ClientPaymentDetail> ClientPaymentDetail { get; set; }
        public virtual DbSet<ClientPaymentMng_ClientPaymentDetail_View> ClientPaymentMng_ClientPaymentDetail_View { get; set; }
        public virtual DbSet<ClientPaymentMng_ClientPaymentSearch_View> ClientPaymentMng_ClientPaymentSearch_View { get; set; }
    
        public virtual ObjectResult<ClientPaymentMng_ClientPaymentSearch_View> ClientPaymentMng_function_SearchClientPayment(string sortedBy, string sortedDirection, string season, string proformaInvoiceNo, string clientUD, string clientNM, Nullable<int> saleID)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_ClientPaymentSearch_View>("ClientPaymentMng_function_SearchClientPayment", sortedByParameter, sortedDirectionParameter, seasonParameter, proformaInvoiceNoParameter, clientUDParameter, clientNMParameter, saleIDParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_ClientPaymentSearch_View> ClientPaymentMng_function_SearchClientPayment(string sortedBy, string sortedDirection, string season, string proformaInvoiceNo, string clientUD, string clientNM, Nullable<int> saleID, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_ClientPaymentSearch_View>("ClientPaymentMng_function_SearchClientPayment", mergeOption, sortedByParameter, sortedDirectionParameter, seasonParameter, proformaInvoiceNoParameter, clientUDParameter, clientNMParameter, saleIDParameter);
        }
    }
}
