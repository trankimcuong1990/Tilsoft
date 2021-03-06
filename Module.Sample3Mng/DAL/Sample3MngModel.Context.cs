﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample3Mng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Sample3MngEntities : DbContext
    {
        public Sample3MngEntities()
            : base("name=Sample3MngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SampleOrder> SampleOrder { get; set; }
        public virtual DbSet<SampleProduct> SampleProduct { get; set; }
        public virtual DbSet<SampleProductItem> SampleProductItem { get; set; }
        public virtual DbSet<SampleProductLocation> SampleProductLocation { get; set; }
        public virtual DbSet<SampleProductMinute> SampleProductMinute { get; set; }
        public virtual DbSet<SampleProductMinuteImage> SampleProductMinuteImage { get; set; }
        public virtual DbSet<SampleProductSubFactory> SampleProductSubFactory { get; set; }
        public virtual DbSet<SampleProgress> SampleProgress { get; set; }
        public virtual DbSet<SampleProgressImage> SampleProgressImage { get; set; }
        public virtual DbSet<SampleQARemark> SampleQARemark { get; set; }
        public virtual DbSet<SampleQARemarkImage> SampleQARemarkImage { get; set; }
        public virtual DbSet<SampleReferenceImage> SampleReferenceImage { get; set; }
        public virtual DbSet<SampleRemark> SampleRemark { get; set; }
        public virtual DbSet<SampleRemarkImage> SampleRemarkImage { get; set; }
        public virtual DbSet<SampleTechnicalDrawing> SampleTechnicalDrawing { get; set; }
        public virtual DbSet<SampleTechnicalDrawingFile> SampleTechnicalDrawingFile { get; set; }
        public virtual DbSet<SampleWorkFlow> SampleWorkFlow { get; set; }
        public virtual DbSet<Sample3Mng_SampleOrderSearchResult_View> Sample3Mng_SampleOrderSearchResult_View { get; set; }
        public virtual DbSet<Sample3Mng_MonitorUser_View> Sample3Mng_MonitorUser_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleOrder_View> Sample3Mng_SampleOrder_View { get; set; }
        public virtual DbSet<Sample3Mng_Client_View> Sample3Mng_Client_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleMonitor_View> Sample3Mng_SampleMonitor_View { get; set; }
        public virtual DbSet<Sample3Mng_UserWithClient_View> Sample3Mng_UserWithClient_View { get; set; }
        public virtual DbSet<SampleMonitor> SampleMonitor { get; set; }
        public virtual DbSet<Sample3Mng_SampleOrderOverview_Order_View> Sample3Mng_SampleOrderOverview_Order_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleOrderOverview_Product_View> Sample3Mng_SampleOrderOverview_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleOrderOverview_ProductLocation_View> Sample3Mng_SampleOrderOverview_ProductLocation_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleOrderOverview_ProductSubFactory_View> Sample3Mng_SampleOrderOverview_ProductSubFactory_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleOrderOverview_Monitor_View> Sample3Mng_SampleOrderOverview_Monitor_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_InternalRemark_View> Sample3Mng_SampleProductOverview_InternalRemark_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_InternalRemarkImage_View> Sample3Mng_SampleProductOverview_InternalRemarkImage_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_ItemLocation_View> Sample3Mng_SampleProductOverview_ItemLocation_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_Product_View> Sample3Mng_SampleProductOverview_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_Progress_View> Sample3Mng_SampleProductOverview_Progress_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_ProgressImage_View> Sample3Mng_SampleProductOverview_ProgressImage_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_QARemark_View> Sample3Mng_SampleProductOverview_QARemark_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_QARemarkImage_View> Sample3Mng_SampleProductOverview_QARemarkImage_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_ReferenceImage_View> Sample3Mng_SampleProductOverview_ReferenceImage_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_SubFactory_View> Sample3Mng_SampleProductOverview_SubFactory_View { get; set; }
        public virtual DbSet<Sample3Mng_SampleProductOverview_TechnicalDrawing_View> Sample3Mng_SampleProductOverview_TechnicalDrawing_View { get; set; }
        public virtual DbSet<Sample3Mng_FactoryAssignment_Product_View> Sample3Mng_FactoryAssignment_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_FactoryAssignment_SubFactory_View> Sample3Mng_FactoryAssignment_SubFactory_View { get; set; }
        public virtual DbSet<Sample3Mng_InternalRemark_Product_View> Sample3Mng_InternalRemark_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_InternalRemark_Remark_View> Sample3Mng_InternalRemark_Remark_View { get; set; }
        public virtual DbSet<Sample3Mng_InternalRemark_RemarkImage_View> Sample3Mng_InternalRemark_RemarkImage_View { get; set; }
        public virtual DbSet<Sample3Mng_QARemark_Product_View> Sample3Mng_QARemark_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_QARemark_Remark_View> Sample3Mng_QARemark_Remark_View { get; set; }
        public virtual DbSet<Sample3Mng_QARemark_RemarkImage_View> Sample3Mng_QARemark_RemarkImage_View { get; set; }
        public virtual DbSet<Sample3Mng_BuildingProcess_Product_View> Sample3Mng_BuildingProcess_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_BuildingProcess_Progress_View> Sample3Mng_BuildingProcess_Progress_View { get; set; }
        public virtual DbSet<Sample3Mng_BuildingProcess_ProgressImage_View> Sample3Mng_BuildingProcess_ProgressImage_View { get; set; }
        public virtual DbSet<Sample3Mng_ItemData_Product_View> Sample3Mng_ItemData_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_ProductInfo_Product_View> Sample3Mng_ProductInfo_Product_View { get; set; }
        public virtual DbSet<Sample3Mng_ProductInfo_ReferenceImage_View> Sample3Mng_ProductInfo_ReferenceImage_View { get; set; }
    
        public virtual ObjectResult<Sample3Mng_SampleOrderSearchResult_View> Sample3Mng_function_SearchSampleOrder(string sampleOrderUD, string season, string clientUD, string clientNM, Nullable<int> purposeID, Nullable<int> transportTypeID, Nullable<int> sampleOrderStatusID, string sortedBy, string sortedDirection)
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
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sample3Mng_SampleOrderSearchResult_View>("Sample3Mng_function_SearchSampleOrder", sampleOrderUDParameter, seasonParameter, clientUDParameter, clientNMParameter, purposeIDParameter, transportTypeIDParameter, sampleOrderStatusIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<Sample3Mng_SampleOrderSearchResult_View> Sample3Mng_function_SearchSampleOrder(string sampleOrderUD, string season, string clientUD, string clientNM, Nullable<int> purposeID, Nullable<int> transportTypeID, Nullable<int> sampleOrderStatusID, string sortedBy, string sortedDirection, MergeOption mergeOption)
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
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sample3Mng_SampleOrderSearchResult_View>("Sample3Mng_function_SearchSampleOrder", mergeOption, sampleOrderUDParameter, seasonParameter, clientUDParameter, clientNMParameter, purposeIDParameter, transportTypeIDParameter, sampleOrderStatusIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
