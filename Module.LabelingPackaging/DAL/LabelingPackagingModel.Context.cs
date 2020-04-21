﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LabelingPackaging.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LabelingPackagingEntities : DbContext
    {
        public LabelingPackagingEntities()
            : base("name=LabelingPackagingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SaleOrderDetail> SaleOrderDetail { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail> LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail> LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail { get; set; }
        public virtual DbSet<LabelingPackagingRemark> LabelingPackagingRemark { get; set; }
        public virtual DbSet<LabelingPackagingSparepartDetail> LabelingPackagingSparepartDetail { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackagingRemark_View> LabelingPackagingMng_LabelingPackagingRemark_View { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackagingSparepartDetail_View> LabelingPackagingMng_LabelingPackagingSparepartDetail_View { get; set; }
        public virtual DbSet<LabelingPackagingMng_EmployeeEmail_View> LabelingPackagingMng_EmployeeEmail_View { get; set; }
        public virtual DbSet<SupportMng_NotificationGroup_View> SupportMng_NotificationGroup_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackaging_SearchView> LabelingPackagingMng_LabelingPackaging_SearchView { get; set; }
        public virtual DbSet<LabelingPackaging> LabelingPackaging { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackaging_View> LabelingPackagingMng_LabelingPackaging_View { get; set; }
        public virtual DbSet<LabelingPackagingOtherFile> LabelingPackagingOtherFile { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackagingOtherFile_View> LabelingPackagingMng_LabelingPackagingOtherFile_View { get; set; }
        public virtual DbSet<LabelingPackagingRejection> LabelingPackagingRejection { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackagingRejection_View> LabelingPackagingMng_LabelingPackagingRejection_View { get; set; }
        public virtual DbSet<LabelingPackagingMng_SendEmailToTeamVN_View> LabelingPackagingMng_SendEmailToTeamVN_View { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackagingFileMonitor_View> LabelingPackagingMng_LabelingPackagingFileMonitor_View { get; set; }
        public virtual DbSet<LabelingPackagingFileMonitor> LabelingPackagingFileMonitor { get; set; }
        public virtual DbSet<LabelingPackagingDetail> LabelingPackagingDetail { get; set; }
        public virtual DbSet<LabelingPackagingMng_LabelingPackagingDetail_View> LabelingPackagingMng_LabelingPackagingDetail_View { get; set; }
        public virtual DbSet<NotificationMessage> NotificationMessage { get; set; }
    
        public virtual ObjectResult<LabelingPackagingMng_LabelingPackaging_SearchView> LabelingPackagingMng_function_SearchLabelingPackaging(string sortedBy, string sortedDirection, Nullable<int> userID, string season, Nullable<int> saleID, string proformaInvoiceNo, string factoryUD, string clientUD, Nullable<int> lPStatusID, string productionStatus)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var factoryUDParameter = factoryUD != null ?
                new ObjectParameter("FactoryUD", factoryUD) :
                new ObjectParameter("FactoryUD", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var lPStatusIDParameter = lPStatusID.HasValue ?
                new ObjectParameter("LPStatusID", lPStatusID) :
                new ObjectParameter("LPStatusID", typeof(int));
    
            var productionStatusParameter = productionStatus != null ?
                new ObjectParameter("ProductionStatus", productionStatus) :
                new ObjectParameter("ProductionStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LabelingPackagingMng_LabelingPackaging_SearchView>("LabelingPackagingMng_function_SearchLabelingPackaging", sortedByParameter, sortedDirectionParameter, userIDParameter, seasonParameter, saleIDParameter, proformaInvoiceNoParameter, factoryUDParameter, clientUDParameter, lPStatusIDParameter, productionStatusParameter);
        }
    
        public virtual ObjectResult<LabelingPackagingMng_LabelingPackaging_SearchView> LabelingPackagingMng_function_SearchLabelingPackaging(string sortedBy, string sortedDirection, Nullable<int> userID, string season, Nullable<int> saleID, string proformaInvoiceNo, string factoryUD, string clientUD, Nullable<int> lPStatusID, string productionStatus, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var saleIDParameter = saleID.HasValue ?
                new ObjectParameter("SaleID", saleID) :
                new ObjectParameter("SaleID", typeof(int));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var factoryUDParameter = factoryUD != null ?
                new ObjectParameter("FactoryUD", factoryUD) :
                new ObjectParameter("FactoryUD", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var lPStatusIDParameter = lPStatusID.HasValue ?
                new ObjectParameter("LPStatusID", lPStatusID) :
                new ObjectParameter("LPStatusID", typeof(int));
    
            var productionStatusParameter = productionStatus != null ?
                new ObjectParameter("ProductionStatus", productionStatus) :
                new ObjectParameter("ProductionStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LabelingPackagingMng_LabelingPackaging_SearchView>("LabelingPackagingMng_function_SearchLabelingPackaging", mergeOption, sortedByParameter, sortedDirectionParameter, userIDParameter, seasonParameter, saleIDParameter, proformaInvoiceNoParameter, factoryUDParameter, clientUDParameter, lPStatusIDParameter, productionStatusParameter);
        }
    
        public virtual int LabelingPackagingMng_function_AutoAddFile(Nullable<int> chkPosition, string articleCode, Nullable<int> labelingPackagingDetailID)
        {
            var chkPositionParameter = chkPosition.HasValue ?
                new ObjectParameter("ChkPosition", chkPosition) :
                new ObjectParameter("ChkPosition", typeof(int));
    
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var labelingPackagingDetailIDParameter = labelingPackagingDetailID.HasValue ?
                new ObjectParameter("LabelingPackagingDetailID", labelingPackagingDetailID) :
                new ObjectParameter("LabelingPackagingDetailID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LabelingPackagingMng_function_AutoAddFile", chkPositionParameter, articleCodeParameter, labelingPackagingDetailIDParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> LabelingPackagingMng_function_InsertFactoryOrderDetail()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("LabelingPackagingMng_function_InsertFactoryOrderDetail");
        }
    
        public virtual int LabelingPackagingMng_function_AutoAddFileWhenUserConfig(Nullable<int> labelingPackagingID)
        {
            var labelingPackagingIDParameter = labelingPackagingID.HasValue ?
                new ObjectParameter("LabelingPackagingID", labelingPackagingID) :
                new ObjectParameter("LabelingPackagingID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LabelingPackagingMng_function_AutoAddFileWhenUserConfig", labelingPackagingIDParameter);
        }
    }
}