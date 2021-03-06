﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample2Mng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Sample2MngEntities : DbContext
    {
        public Sample2MngEntities()
            : base("name=Sample2MngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Sample2Mng_SampleOrderSearchResult_View> Sample2Mng_SampleOrderSearchResult_View { get; set; }
        public virtual DbSet<SampleMonitor> SampleMonitor { get; set; }
        public virtual DbSet<SampleOrder> SampleOrder { get; set; }
        public virtual DbSet<SampleProduct> SampleProduct { get; set; }
        public virtual DbSet<SampleProductMinute> SampleProductMinute { get; set; }
        public virtual DbSet<SampleProductMinuteImage> SampleProductMinuteImage { get; set; }
        public virtual DbSet<Sample2Mng_SampleMonitor_View> Sample2Mng_SampleMonitor_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleOrder_View> Sample2Mng_SampleOrder_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProduct_View> Sample2Mng_SampleProduct_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProductMinute_View> Sample2Mng_SampleProductMinute_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProductMinuteImage_View> Sample2Mng_SampleProductMinuteImage_View { get; set; }
        public virtual DbSet<SampleReferenceImage> SampleReferenceImage { get; set; }
        public virtual DbSet<Sample2Mng_SampleReferenceImage_View> Sample2Mng_SampleReferenceImage_View { get; set; }
        public virtual DbSet<SampleProgress> SampleProgress { get; set; }
        public virtual DbSet<SampleProgressImage> SampleProgressImage { get; set; }
        public virtual DbSet<Sample2Mng_SampleProgress_View> Sample2Mng_SampleProgress_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProgressImage_View> Sample2Mng_SampleProgressImage_View { get; set; }
        public virtual DbSet<SampleRemark> SampleRemark { get; set; }
        public virtual DbSet<SampleRemarkImage> SampleRemarkImage { get; set; }
        public virtual DbSet<Sample2Mng_SampleRemark_View> Sample2Mng_SampleRemark_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleRemarkImage_View> Sample2Mng_SampleRemarkImage_View { get; set; }
        public virtual DbSet<SampleTechnicalDrawing> SampleTechnicalDrawing { get; set; }
        public virtual DbSet<SampleTechnicalDrawingFile> SampleTechnicalDrawingFile { get; set; }
        public virtual DbSet<Sample2Mng_SampleTechnicalDrawing_View> Sample2Mng_SampleTechnicalDrawing_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleTechnicalDrawingFile_View> Sample2Mng_SampleTechnicalDrawingFile_View { get; set; }
        public virtual DbSet<AccountMng_UserProfile_View> AccountMng_UserProfile_View { get; set; }
        public virtual DbSet<SupportMng_Client_View> SupportMng_Client_View { get; set; }
        public virtual DbSet<Sample2Mng_QAUser_View> Sample2Mng_QAUser_View { get; set; }
        public virtual DbSet<SampleProductSubFactory> SampleProductSubFactory { get; set; }
        public virtual DbSet<Sample2Mng_SampleProductSubFactory_View> Sample2Mng_SampleProductSubFactory_View { get; set; }
        public virtual DbSet<Sample2Mng_UserWithClient_View> Sample2Mng_UserWithClient_View { get; set; }
        public virtual DbSet<SampleQARemark> SampleQARemark { get; set; }
        public virtual DbSet<SampleQARemarkImage> SampleQARemarkImage { get; set; }
        public virtual DbSet<Sample2Mng_SampleQARemark_View> Sample2Mng_SampleQARemark_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleQARemarkImage_View> Sample2Mng_SampleQARemarkImage_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProductCode_View> Sample2Mng_SampleProductCode_View { get; set; }
        public virtual DbSet<SampleProductItem> SampleProductItem { get; set; }
        public virtual DbSet<Sample2Mng_SampleItemLocation_View> Sample2Mng_SampleItemLocation_View { get; set; }
        public virtual DbSet<Sample2Mng_FactoryUser_View> Sample2Mng_FactoryUser_View { get; set; }
        public virtual DbSet<Sample2Mng_Model_View> Sample2Mng_Model_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<SamplePackaging> SamplePackaging { get; set; }
        public virtual DbSet<Sample2Mng_SamplePackaging_View> Sample2Mng_SamplePackaging_View { get; set; }
        public virtual DbSet<Sample2Mng_Breakdown_View> Sample2Mng_Breakdown_View { get; set; }
        public virtual DbSet<FactoryBreakdown> FactoryBreakdown { get; set; }
        public virtual DbSet<FactoryBreakdownDetail> FactoryBreakdownDetail { get; set; }
        public virtual DbSet<SupportMng_NotificationGroup_View> SupportMng_NotificationGroup_View { get; set; }
        public virtual DbSet<List_PackagingMethod_View> List_PackagingMethod_View { get; set; }
        public virtual DbSet<SupportMng_AccountManager_View> SupportMng_AccountManager_View { get; set; }
        public virtual DbSet<SampleProductPackagingMaterial> SampleProductPackagingMaterial { get; set; }
        public virtual DbSet<Sample2Mng_SampleproductPackagingMaterialType_View> Sample2Mng_SampleproductPackagingMaterialType_View { get; set; }
        public virtual DbSet<Sample2Mng_Unit_View> Sample2Mng_Unit_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProductPackagingMaterial_View> Sample2Mng_SampleProductPackagingMaterial_View { get; set; }
        public virtual DbSet<Sample2Mng_DevelopmentType_View> Sample2Mng_DevelopmentType_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProductCartonBox_View> Sample2Mng_SampleProductCartonBox_View { get; set; }
        public virtual DbSet<Sample2Mng_SampleProductDimension_View> Sample2Mng_SampleProductDimension_View { get; set; }
        public virtual DbSet<SampleProductCartonBox> SampleProductCartonBox { get; set; }
        public virtual DbSet<SampleProductDimension> SampleProductDimension { get; set; }
        public virtual DbSet<NotificationMessage> NotificationMessage { get; set; }
    
        public virtual ObjectResult<Sample2Mng_SampleOrderSearchResult_View> Sample2Mng_function_SearchSampleOrder(string sampleOrderUD, string season, string clientUD, string clientNM, Nullable<int> purposeID, Nullable<int> transportTypeID, Nullable<int> sampleOrderStatusID, Nullable<int> factoryID, Nullable<int> accountManagerID, string sortedBy, string sortedDirection, string sampleItemCode, string sampleItemName)
        {
            var sampleOrderUDParameter = sampleOrderUD != null ?
                new ObjectParameter("SampleOrderUD", sampleOrderUD) :
                new ObjectParameter("SampleOrderUD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var purposeIDParameter = purposeID.HasValue ?
                new ObjectParameter("PurposeID", purposeID) :
                new ObjectParameter("PurposeID", typeof(int));
    
            var transportTypeIDParameter = transportTypeID.HasValue ?
                new ObjectParameter("TransportTypeID", transportTypeID) :
                new ObjectParameter("TransportTypeID", typeof(int));
    
            var sampleOrderStatusIDParameter = sampleOrderStatusID.HasValue ?
                new ObjectParameter("SampleOrderStatusID", sampleOrderStatusID) :
                new ObjectParameter("SampleOrderStatusID", typeof(int));
    
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            var accountManagerIDParameter = accountManagerID.HasValue ?
                new ObjectParameter("AccountManagerID", accountManagerID) :
                new ObjectParameter("AccountManagerID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var sampleItemCodeParameter = sampleItemCode != null ?
                new ObjectParameter("SampleItemCode", sampleItemCode) :
                new ObjectParameter("SampleItemCode", typeof(string));
    
            var sampleItemNameParameter = sampleItemName != null ?
                new ObjectParameter("SampleItemName", sampleItemName) :
                new ObjectParameter("SampleItemName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sample2Mng_SampleOrderSearchResult_View>("Sample2Mng_function_SearchSampleOrder", sampleOrderUDParameter, seasonParameter, clientUDParameter, clientNMParameter, purposeIDParameter, transportTypeIDParameter, sampleOrderStatusIDParameter, factoryIDParameter, accountManagerIDParameter, sortedByParameter, sortedDirectionParameter, sampleItemCodeParameter, sampleItemNameParameter);
        }
    
        public virtual ObjectResult<Sample2Mng_SampleOrderSearchResult_View> Sample2Mng_function_SearchSampleOrder(string sampleOrderUD, string season, string clientUD, string clientNM, Nullable<int> purposeID, Nullable<int> transportTypeID, Nullable<int> sampleOrderStatusID, Nullable<int> factoryID, Nullable<int> accountManagerID, string sortedBy, string sortedDirection, string sampleItemCode, string sampleItemName, MergeOption mergeOption)
        {
            var sampleOrderUDParameter = sampleOrderUD != null ?
                new ObjectParameter("SampleOrderUD", sampleOrderUD) :
                new ObjectParameter("SampleOrderUD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var purposeIDParameter = purposeID.HasValue ?
                new ObjectParameter("PurposeID", purposeID) :
                new ObjectParameter("PurposeID", typeof(int));
    
            var transportTypeIDParameter = transportTypeID.HasValue ?
                new ObjectParameter("TransportTypeID", transportTypeID) :
                new ObjectParameter("TransportTypeID", typeof(int));
    
            var sampleOrderStatusIDParameter = sampleOrderStatusID.HasValue ?
                new ObjectParameter("SampleOrderStatusID", sampleOrderStatusID) :
                new ObjectParameter("SampleOrderStatusID", typeof(int));
    
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            var accountManagerIDParameter = accountManagerID.HasValue ?
                new ObjectParameter("AccountManagerID", accountManagerID) :
                new ObjectParameter("AccountManagerID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var sampleItemCodeParameter = sampleItemCode != null ?
                new ObjectParameter("SampleItemCode", sampleItemCode) :
                new ObjectParameter("SampleItemCode", typeof(string));
    
            var sampleItemNameParameter = sampleItemName != null ?
                new ObjectParameter("SampleItemName", sampleItemName) :
                new ObjectParameter("SampleItemName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sample2Mng_SampleOrderSearchResult_View>("Sample2Mng_function_SearchSampleOrder", mergeOption, sampleOrderUDParameter, seasonParameter, clientUDParameter, clientNMParameter, purposeIDParameter, transportTypeIDParameter, sampleOrderStatusIDParameter, factoryIDParameter, accountManagerIDParameter, sortedByParameter, sortedDirectionParameter, sampleItemCodeParameter, sampleItemNameParameter);
        }
    
        public virtual ObjectResult<Sample2Mng_Model_View> Sample2Mng_function_GetModelList(string param)
        {
            var paramParameter = param != null ?
                new ObjectParameter("param", param) :
                new ObjectParameter("param", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sample2Mng_Model_View>("Sample2Mng_function_GetModelList", paramParameter);
        }
    
        public virtual ObjectResult<Sample2Mng_Model_View> Sample2Mng_function_GetModelList(string param, MergeOption mergeOption)
        {
            var paramParameter = param != null ?
                new ObjectParameter("param", param) :
                new ObjectParameter("param", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sample2Mng_Model_View>("Sample2Mng_function_GetModelList", mergeOption, paramParameter);
        }
    
        public virtual int FW_Quotation_function_AddSampleItem(Nullable<int> sampleOrderID, Nullable<int> sampleProductID)
        {
            var sampleOrderIDParameter = sampleOrderID.HasValue ?
                new ObjectParameter("SampleOrderID", sampleOrderID) :
                new ObjectParameter("SampleOrderID", typeof(int));
    
            var sampleProductIDParameter = sampleProductID.HasValue ?
                new ObjectParameter("SampleProductID", sampleProductID) :
                new ObjectParameter("SampleProductID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FW_Quotation_function_AddSampleItem", sampleOrderIDParameter, sampleProductIDParameter);
        }
    }
}
