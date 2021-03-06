﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientPaymentMng.DAL
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
    
        public virtual DbSet<ClientPaymentMng_ClientPayment_View> ClientPaymentMng_ClientPayment_View { get; set; }
        public virtual DbSet<ClientPaymentMng_ClientPaymentDeduction_View> ClientPaymentMng_ClientPaymentDeduction_View { get; set; }
        public virtual DbSet<ClientPaymentMng_ClientPaymentDetail_View> ClientPaymentMng_ClientPaymentDetail_View { get; set; }
        public virtual DbSet<ClientPaymentMng_ClientPaymentSearchResult_View> ClientPaymentMng_ClientPaymentSearchResult_View { get; set; }
        public virtual DbSet<ClientPaymentMng_ECommercialInvoiceSearchResult_View> ClientPaymentMng_ECommercialInvoiceSearchResult_View { get; set; }
        public virtual DbSet<ClientPaymentMng_SaleOrderSearchResult_View> ClientPaymentMng_SaleOrderSearchResult_View { get; set; }
        public virtual DbSet<ClientPaymentMng_SaleOrderForDeductionSearchResult_View> ClientPaymentMng_SaleOrderForDeductionSearchResult_View { get; set; }
        public virtual DbSet<ClientPaymentMng_ClientBallace_View> ClientPaymentMng_ClientBallace_View { get; set; }
        public virtual DbSet<ClientPayment> ClientPayment { get; set; }
        public virtual DbSet<ClientPaymentBallance> ClientPaymentBallance { get; set; }
        public virtual DbSet<ClientPaymentDeduction> ClientPaymentDeduction { get; set; }
        public virtual DbSet<ClientPaymentDetail> ClientPaymentDetail { get; set; }
    
        public virtual ObjectResult<ClientPaymentMng_ClientPaymentSearchResult_View> ClientPaymentMng_function_SearchClientPayment(string clientPaymentUD, Nullable<int> clientPaymentMethodID, string clientUD, string proformaInvoiceNo, string invoiceNo, string transactionNo, string lCNo, string currency, string clientPaymentBallanceUD, Nullable<bool> isConfirmed, string remark, string sortedBy, string sortedDirection)
        {
            var clientPaymentUDParameter = clientPaymentUD != null ?
                new ObjectParameter("ClientPaymentUD", clientPaymentUD) :
                new ObjectParameter("ClientPaymentUD", typeof(string));
    
            var clientPaymentMethodIDParameter = clientPaymentMethodID.HasValue ?
                new ObjectParameter("ClientPaymentMethodID", clientPaymentMethodID) :
                new ObjectParameter("ClientPaymentMethodID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var transactionNoParameter = transactionNo != null ?
                new ObjectParameter("TransactionNo", transactionNo) :
                new ObjectParameter("TransactionNo", typeof(string));
    
            var lCNoParameter = lCNo != null ?
                new ObjectParameter("LCNo", lCNo) :
                new ObjectParameter("LCNo", typeof(string));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var clientPaymentBallanceUDParameter = clientPaymentBallanceUD != null ?
                new ObjectParameter("ClientPaymentBallanceUD", clientPaymentBallanceUD) :
                new ObjectParameter("ClientPaymentBallanceUD", typeof(string));
    
            var isConfirmedParameter = isConfirmed.HasValue ?
                new ObjectParameter("IsConfirmed", isConfirmed) :
                new ObjectParameter("IsConfirmed", typeof(bool));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_ClientPaymentSearchResult_View>("ClientPaymentMng_function_SearchClientPayment", clientPaymentUDParameter, clientPaymentMethodIDParameter, clientUDParameter, proformaInvoiceNoParameter, invoiceNoParameter, transactionNoParameter, lCNoParameter, currencyParameter, clientPaymentBallanceUDParameter, isConfirmedParameter, remarkParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_ClientPaymentSearchResult_View> ClientPaymentMng_function_SearchClientPayment(string clientPaymentUD, Nullable<int> clientPaymentMethodID, string clientUD, string proformaInvoiceNo, string invoiceNo, string transactionNo, string lCNo, string currency, string clientPaymentBallanceUD, Nullable<bool> isConfirmed, string remark, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var clientPaymentUDParameter = clientPaymentUD != null ?
                new ObjectParameter("ClientPaymentUD", clientPaymentUD) :
                new ObjectParameter("ClientPaymentUD", typeof(string));
    
            var clientPaymentMethodIDParameter = clientPaymentMethodID.HasValue ?
                new ObjectParameter("ClientPaymentMethodID", clientPaymentMethodID) :
                new ObjectParameter("ClientPaymentMethodID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var transactionNoParameter = transactionNo != null ?
                new ObjectParameter("TransactionNo", transactionNo) :
                new ObjectParameter("TransactionNo", typeof(string));
    
            var lCNoParameter = lCNo != null ?
                new ObjectParameter("LCNo", lCNo) :
                new ObjectParameter("LCNo", typeof(string));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var clientPaymentBallanceUDParameter = clientPaymentBallanceUD != null ?
                new ObjectParameter("ClientPaymentBallanceUD", clientPaymentBallanceUD) :
                new ObjectParameter("ClientPaymentBallanceUD", typeof(string));
    
            var isConfirmedParameter = isConfirmed.HasValue ?
                new ObjectParameter("IsConfirmed", isConfirmed) :
                new ObjectParameter("IsConfirmed", typeof(bool));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_ClientPaymentSearchResult_View>("ClientPaymentMng_function_SearchClientPayment", mergeOption, clientPaymentUDParameter, clientPaymentMethodIDParameter, clientUDParameter, proformaInvoiceNoParameter, invoiceNoParameter, transactionNoParameter, lCNoParameter, currencyParameter, clientPaymentBallanceUDParameter, isConfirmedParameter, remarkParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_ECommercialInvoiceSearchResult_View> ClientPaymentMng_function_SearchECommercialInvoice(Nullable<int> clientID, string currency)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_ECommercialInvoiceSearchResult_View>("ClientPaymentMng_function_SearchECommercialInvoice", clientIDParameter, currencyParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_ECommercialInvoiceSearchResult_View> ClientPaymentMng_function_SearchECommercialInvoice(Nullable<int> clientID, string currency, MergeOption mergeOption)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_ECommercialInvoiceSearchResult_View>("ClientPaymentMng_function_SearchECommercialInvoice", mergeOption, clientIDParameter, currencyParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_SaleOrderSearchResult_View> ClientPaymentMng_function_SearchSaleOrder(Nullable<int> clientID, string currency)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_SaleOrderSearchResult_View>("ClientPaymentMng_function_SearchSaleOrder", clientIDParameter, currencyParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_SaleOrderSearchResult_View> ClientPaymentMng_function_SearchSaleOrder(Nullable<int> clientID, string currency, MergeOption mergeOption)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_SaleOrderSearchResult_View>("ClientPaymentMng_function_SearchSaleOrder", mergeOption, clientIDParameter, currencyParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_SaleOrderForDeductionSearchResult_View> ClientPaymentMng_function_SearchSaleOrderForDeduction(Nullable<int> clientID, string currency)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_SaleOrderForDeductionSearchResult_View>("ClientPaymentMng_function_SearchSaleOrderForDeduction", clientIDParameter, currencyParameter);
        }
    
        public virtual ObjectResult<ClientPaymentMng_SaleOrderForDeductionSearchResult_View> ClientPaymentMng_function_SearchSaleOrderForDeduction(Nullable<int> clientID, string currency, MergeOption mergeOption)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientPaymentMng_SaleOrderForDeductionSearchResult_View>("ClientPaymentMng_function_SearchSaleOrderForDeduction", mergeOption, clientIDParameter, currencyParameter);
        }
    }
}
