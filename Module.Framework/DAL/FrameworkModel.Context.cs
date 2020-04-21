﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Framework.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FrameworkEntities : DbContext
    {
        public FrameworkEntities()
            : base("name=FrameworkEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<FW_UserProfile_View> FW_UserProfile_View { get; set; }
        public virtual DbSet<FW_UserGroupPermission_View> FW_UserGroupPermission_View { get; set; }
        public virtual DbSet<FW_UserPermission_View> FW_UserPermission_View { get; set; }
        public virtual DbSet<FW_UserFactoryAccess_View> FW_UserFactoryAccess_View { get; set; }
        public virtual DbSet<FW_DPBalance_View> FW_DPBalance_View { get; set; }
        public virtual DbSet<ApplicationUsage> ApplicationUsage { get; set; }
        public virtual DbSet<DataTracking> DataTracking { get; set; }
        public virtual DbSet<DataTrackingAction> DataTrackingAction { get; set; }
        public virtual DbSet<DataTrackingDetail> DataTrackingDetail { get; set; }
        public virtual DbSet<FW_ModuleSpecialPermissionAccess_View> FW_ModuleSpecialPermissionAccess_View { get; set; }
        public virtual DbSet<FW_UserCompanyAccess_View> FW_UserCompanyAccess_View { get; set; }
        public virtual DbSet<SystemSetting> SystemSetting { get; set; }
        public virtual DbSet<FW_SystemSetting_View> FW_SystemSetting_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<NotificationMessage> NotificationMessage { get; set; }
        public virtual DbSet<FW_NotificationGroupMember_View> FW_NotificationGroupMember_View { get; set; }
        public virtual DbSet<FW_NotificationGroupStatus_View> FW_NotificationGroupStatus_View { get; set; }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckBookingPermission(Nullable<int> userID, Nullable<int> bookingID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var bookingIDParameter = bookingID.HasValue ?
                new ObjectParameter("BookingID", bookingID) :
                new ObjectParameter("BookingID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckBookingPermission", userIDParameter, bookingIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryOrderDetailPermission(Nullable<int> userID, Nullable<int> factoryOrderDetailID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryOrderDetailIDParameter = factoryOrderDetailID.HasValue ?
                new ObjectParameter("FactoryOrderDetailID", factoryOrderDetailID) :
                new ObjectParameter("FactoryOrderDetailID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryOrderDetailPermission", userIDParameter, factoryOrderDetailIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryOrderSparepartDetailPermission(Nullable<int> userID, Nullable<int> factoryOrderSparepartDetailID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryOrderSparepartDetailIDParameter = factoryOrderSparepartDetailID.HasValue ?
                new ObjectParameter("FactoryOrderSparepartDetailID", factoryOrderSparepartDetailID) :
                new ObjectParameter("FactoryOrderSparepartDetailID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryOrderSparepartDetailPermission", userIDParameter, factoryOrderSparepartDetailIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckLoadingPlanPermission(Nullable<int> userID, Nullable<int> loadingPlanID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var loadingPlanIDParameter = loadingPlanID.HasValue ?
                new ObjectParameter("LoadingPlanID", loadingPlanID) :
                new ObjectParameter("LoadingPlanID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckLoadingPlanPermission", userIDParameter, loadingPlanIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckPLCPermission(Nullable<int> userID, Nullable<int> pLCID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var pLCIDParameter = pLCID.HasValue ?
                new ObjectParameter("PLCID", pLCID) :
                new ObjectParameter("PLCID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckPLCPermission", userIDParameter, pLCIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckQuotationPermission(Nullable<int> userID, Nullable<int> quotationID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var quotationIDParameter = quotationID.HasValue ?
                new ObjectParameter("QuotationID", quotationID) :
                new ObjectParameter("QuotationID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckQuotationPermission", userIDParameter, quotationIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryInvoicePermission(Nullable<int> userID, Nullable<int> factoryInvoiceID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryInvoiceIDParameter = factoryInvoiceID.HasValue ?
                new ObjectParameter("FactoryInvoiceID", factoryInvoiceID) :
                new ObjectParameter("FactoryInvoiceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryInvoicePermission", userIDParameter, factoryInvoiceIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckSupplierPermission(Nullable<int> userID, Nullable<int> supplierID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckSupplierPermission", userIDParameter, supplierIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryCreditNotePermission(Nullable<int> userID, Nullable<int> factoryCreditNoteID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryCreditNoteIDParameter = factoryCreditNoteID.HasValue ?
                new ObjectParameter("FactoryCreditNoteID", factoryCreditNoteID) :
                new ObjectParameter("FactoryCreditNoteID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryCreditNotePermission", userIDParameter, factoryCreditNoteIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryPayment2Permission(Nullable<int> userID, Nullable<int> factoryPayment2ID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryPayment2IDParameter = factoryPayment2ID.HasValue ?
                new ObjectParameter("FactoryPayment2ID", factoryPayment2ID) :
                new ObjectParameter("FactoryPayment2ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryPayment2Permission", userIDParameter, factoryPayment2IDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryProformaInvoicePermission(Nullable<int> userID, Nullable<int> factoryProformaInvoiceID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryProformaInvoiceIDParameter = factoryProformaInvoiceID.HasValue ?
                new ObjectParameter("FactoryProformaInvoiceID", factoryProformaInvoiceID) :
                new ObjectParameter("FactoryProformaInvoiceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryProformaInvoicePermission", userIDParameter, factoryProformaInvoiceIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryPermission(Nullable<int> userID, Nullable<int> factoryID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryPermission", userIDParameter, factoryIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckFactoryOrderPermission(Nullable<int> userID, Nullable<int> factoryOrderID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryOrderIDParameter = factoryOrderID.HasValue ?
                new ObjectParameter("FactoryOrderID", factoryOrderID) :
                new ObjectParameter("FactoryOrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckFactoryOrderPermission", userIDParameter, factoryOrderIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckPurchasingInvoicePermission(Nullable<int> userID, Nullable<int> purchasingInvoiceID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var purchasingInvoiceIDParameter = purchasingInvoiceID.HasValue ?
                new ObjectParameter("PurchasingInvoiceID", purchasingInvoiceID) :
                new ObjectParameter("PurchasingInvoiceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckPurchasingInvoicePermission", userIDParameter, purchasingInvoiceIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckPurchasingCreditNotePermission(Nullable<int> userID, Nullable<int> purchasingCreditNoteID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var purchasingCreditNoteIDParameter = purchasingCreditNoteID.HasValue ?
                new ObjectParameter("PurchasingCreditNoteID", purchasingCreditNoteID) :
                new ObjectParameter("PurchasingCreditNoteID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckPurchasingCreditNotePermission", userIDParameter, purchasingCreditNoteIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckDPBalancePermission(Nullable<int> userID, Nullable<int> factoryPayment2BalanceID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var factoryPayment2BalanceIDParameter = factoryPayment2BalanceID.HasValue ?
                new ObjectParameter("FactoryPayment2BalanceID", factoryPayment2BalanceID) :
                new ObjectParameter("FactoryPayment2BalanceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckDPBalancePermission", userIDParameter, factoryPayment2BalanceIDParameter);
        }
    
        public virtual ObjectResult<FW_DPBalance_View> FW_function_getDPBalance(Nullable<int> userID, string season, Nullable<int> supplierID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FW_DPBalance_View>("FW_function_getDPBalance", userIDParameter, seasonParameter, supplierIDParameter);
        }
    
        public virtual ObjectResult<FW_DPBalance_View> FW_function_getDPBalance(Nullable<int> userID, string season, Nullable<int> supplierID, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FW_DPBalance_View>("FW_function_getDPBalance", mergeOption, userIDParameter, seasonParameter, supplierIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckModelPermission(Nullable<int> userID, Nullable<int> modelID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckModelPermission", userIDParameter, modelIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckPackingListPermission(Nullable<int> userID, Nullable<int> packingListID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var packingListIDParameter = packingListID.HasValue ?
                new ObjectParameter("PackingListID", packingListID) :
                new ObjectParameter("PackingListID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckPackingListPermission", userIDParameter, packingListIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckIfUserIsFromFactory(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckIfUserIsFromFactory", userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> FW_function_CheckModelPricePermission(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("FW_function_CheckModelPricePermission", userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> Framework_function_GetPermissionByModule(string moduleUDs, Nullable<int> userID)
        {
            var moduleUDsParameter = moduleUDs != null ?
                new ObjectParameter("ModuleUDs", moduleUDs) :
                new ObjectParameter("ModuleUDs", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("Framework_function_GetPermissionByModule", moduleUDsParameter, userIDParameter);
        }
    }
}
