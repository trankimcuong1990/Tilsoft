﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.LabelingPackagingMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LabelingPackagingMngEntities : DbContext
    {
        public LabelingPackagingMngEntities()
            : base("name=LabelingPackagingMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ConstantEntry> ConstantEntry { get; set; }
        public virtual DbSet<LabelingPackaging> LabelingPackaging { get; set; }
        public virtual DbSet<LabelingPackagingAttachment> LabelingPackagingAttachment { get; set; }
        public virtual DbSet<LabelingPackagingDetail> LabelingPackagingDetail { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<OfferLine> OfferLine { get; set; }
        public virtual DbSet<PackagingMethod> PackagingMethod { get; set; }
        public virtual DbSet<SaleOrder> SaleOrder { get; set; }
        public virtual DbSet<SaleOrderDetail> SaleOrderDetail { get; set; }
        public virtual DbSet<GetLabalingPackagingProcessList_View> GetLabalingPackagingProcessList_View { get; set; }
        public virtual DbSet<PackagingMethodList_View> PackagingMethodList_View { get; set; }
        public virtual DbSet<ProformaInvoiceList_View> ProformaInvoiceList_View { get; set; }
        public virtual DbSet<GetPackagingTypes_View> GetPackagingTypes_View { get; set; }
        public virtual DbSet<ProformaInvoiceEdit_View> ProformaInvoiceEdit_View { get; set; }
    
        public virtual ObjectResult<ProformaInvoiceList_View> LabelinPackagingMng_function_Search(string proformaInvoiceNo, string clientUD, string clientNM, string sortedBy, string sortedDirection)
        {
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProformaInvoiceList_View>("LabelinPackagingMng_function_Search", proformaInvoiceNoParameter, clientUDParameter, clientNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ProformaInvoiceList_View> LabelinPackagingMng_function_Search(string proformaInvoiceNo, string clientUD, string clientNM, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProformaInvoiceList_View>("LabelinPackagingMng_function_Search", mergeOption, proformaInvoiceNoParameter, clientUDParameter, clientNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
