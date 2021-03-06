﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehouseTransportMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WarehouseTransportMngEntities : DbContext
    {
        public WarehouseTransportMngEntities()
            : base("name=WarehouseTransportMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<WarehouseTransport> WarehouseTransport { get; set; }
        public virtual DbSet<WarehouseTransportProductDetail> WarehouseTransportProductDetail { get; set; }
        public virtual DbSet<WarehouseTransportSparepartDetail> WarehouseTransportSparepartDetail { get; set; }
        public virtual DbSet<WarehouseTransportMng_WarehouseTransport_SearchView> WarehouseTransportMng_WarehouseTransport_SearchView { get; set; }
        public virtual DbSet<WarehouseTransportMng_WarehouseTransport_View> WarehouseTransportMng_WarehouseTransport_View { get; set; }
        public virtual DbSet<WarehouseTransportMng_WarehouseTransportProductDetail_View> WarehouseTransportMng_WarehouseTransportProductDetail_View { get; set; }
        public virtual DbSet<WarehouseTransportMng_WarehouseTransportSparepartDetail_View> WarehouseTransportMng_WarehouseTransportSparepartDetail_View { get; set; }
        public virtual DbSet<WarehouseTransportMng_PhysicalStock_View> WarehouseTransportMng_PhysicalStock_View { get; set; }
        public virtual DbSet<WarehouseTransportMng_PhysicalStockByWarehouseArea_View> WarehouseTransportMng_PhysicalStockByWarehouseArea_View { get; set; }
    
        public virtual ObjectResult<WarehouseTransportMng_WarehouseTransport_SearchView> WarehouseTransportMng_function_SearchWarehouseTransport(string sortedBy, string sortedDirection, string receiptNo, string remark)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var receiptNoParameter = receiptNo != null ?
                new ObjectParameter("ReceiptNo", receiptNo) :
                new ObjectParameter("ReceiptNo", typeof(string));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseTransportMng_WarehouseTransport_SearchView>("WarehouseTransportMng_function_SearchWarehouseTransport", sortedByParameter, sortedDirectionParameter, receiptNoParameter, remarkParameter);
        }
    
        public virtual ObjectResult<WarehouseTransportMng_WarehouseTransport_SearchView> WarehouseTransportMng_function_SearchWarehouseTransport(string sortedBy, string sortedDirection, string receiptNo, string remark, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var receiptNoParameter = receiptNo != null ?
                new ObjectParameter("ReceiptNo", receiptNo) :
                new ObjectParameter("ReceiptNo", typeof(string));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseTransportMng_WarehouseTransport_SearchView>("WarehouseTransportMng_function_SearchWarehouseTransport", mergeOption, sortedByParameter, sortedDirectionParameter, receiptNoParameter, remarkParameter);
        }
    
        public virtual ObjectResult<WarehouseTransportMng_PhysicalStock_View> WarehouseTransportMng_action_QuickSearchProduct(string sortedBy, string sortedDirection, string productType, string searchText)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var productTypeParameter = productType != null ?
                new ObjectParameter("ProductType", productType) :
                new ObjectParameter("ProductType", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseTransportMng_PhysicalStock_View>("WarehouseTransportMng_action_QuickSearchProduct", sortedByParameter, sortedDirectionParameter, productTypeParameter, searchTextParameter);
        }
    
        public virtual ObjectResult<WarehouseTransportMng_PhysicalStock_View> WarehouseTransportMng_action_QuickSearchProduct(string sortedBy, string sortedDirection, string productType, string searchText, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var productTypeParameter = productType != null ?
                new ObjectParameter("ProductType", productType) :
                new ObjectParameter("ProductType", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseTransportMng_PhysicalStock_View>("WarehouseTransportMng_action_QuickSearchProduct", mergeOption, sortedByParameter, sortedDirectionParameter, productTypeParameter, searchTextParameter);
        }
    
        public virtual int WarehouseTransportMng_function_GenerateReceiptNo(Nullable<int> warehouseTransportID, string season)
        {
            var warehouseTransportIDParameter = warehouseTransportID.HasValue ?
                new ObjectParameter("WarehouseTransportID", warehouseTransportID) :
                new ObjectParameter("WarehouseTransportID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WarehouseTransportMng_function_GenerateReceiptNo", warehouseTransportIDParameter, seasonParameter);
        }
    }
}
