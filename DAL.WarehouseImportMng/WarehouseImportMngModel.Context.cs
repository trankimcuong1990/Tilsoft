﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehouseImportMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WarehouseImportMngEntities : DbContext
    {
        public WarehouseImportMngEntities()
            : base("name=WarehouseImportMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<WarehouseImportMng_WarehouseImport_View> WarehouseImportMng_WarehouseImport_View { get; set; }
        public virtual DbSet<WarehouseImportMng_WarehouseImportSearchResult_View> WarehouseImportMng_WarehouseImportSearchResult_View { get; set; }
        public virtual DbSet<WarehouseImport> WarehouseImport { get; set; }
        public virtual DbSet<WarehouseImportAreaDetail> WarehouseImportAreaDetail { get; set; }
        public virtual DbSet<WarehouseImportMng_CreditNoteProduct_View> WarehouseImportMng_CreditNoteProduct_View { get; set; }
        public virtual DbSet<WarehouseImportMng_CreditNoteSparepart_View> WarehouseImportMng_CreditNoteSparepart_View { get; set; }
        public virtual DbSet<WarehouseImportMng_OnSeaProduct_View> WarehouseImportMng_OnSeaProduct_View { get; set; }
        public virtual DbSet<WarehouseImportMng_OnSeaSparepart_View> WarehouseImportMng_OnSeaSparepart_View { get; set; }
        public virtual DbSet<WarehouseImportMng_WarehouseImportProductDetail_View> WarehouseImportMng_WarehouseImportProductDetail_View { get; set; }
        public virtual DbSet<WarehouseImportMng_WarehouseImportSparepartDetail_View> WarehouseImportMng_WarehouseImportSparepartDetail_View { get; set; }
        public virtual DbSet<WarehouseImportProductDetail> WarehouseImportProductDetail { get; set; }
        public virtual DbSet<WarehouseImportSparepartDetail> WarehouseImportSparepartDetail { get; set; }
        public virtual DbSet<WarehouseImportMng_WarehouseImportAreaDetail_View> WarehouseImportMng_WarehouseImportAreaDetail_View { get; set; }
        public virtual DbSet<WarehouseImportColliDetail> WarehouseImportColliDetail { get; set; }
        public virtual DbSet<WarehouseImportMng_WarehouseImportColliDetail_View> WarehouseImportMng_WarehouseImportColliDetail_View { get; set; }
        public virtual DbSet<WarehouseImportMng_ProductColli_View> WarehouseImportMng_ProductColli_View { get; set; }
        public virtual DbSet<WarehouseImportMng_Client_View> WarehouseImportMng_Client_View { get; set; }
        public virtual DbSet<WarehouseImportMng_LoadingPlan_View> WarehouseImportMng_LoadingPlan_View { get; set; }
        public virtual DbSet<WarehouseImportMng_Product_View> WarehouseImportMng_Product_View { get; set; }
        public virtual DbSet<WarehouseImportMng_SaleOrder_View> WarehouseImportMng_SaleOrder_View { get; set; }
        public virtual DbSet<WarehouseImportMng_ExportToWexData_View> WarehouseImportMng_ExportToWexData_View { get; set; }
    
        public virtual ObjectResult<WarehouseImportMng_OnSeaProduct_View> WarehouseImportMng_function_SearchOnSeaProduct(string searchQuery, string sortedBy, string sortedDirection)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_OnSeaProduct_View>("WarehouseImportMng_function_SearchOnSeaProduct", searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_OnSeaProduct_View> WarehouseImportMng_function_SearchOnSeaProduct(string searchQuery, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_OnSeaProduct_View>("WarehouseImportMng_function_SearchOnSeaProduct", mergeOption, searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_WarehouseImportSearchResult_View> WarehouseImportMng_function_SearchWarehouseImport(string receiptNo, string proformaInvoiceNo, string clientUD, string clientNM, string bLNo, string refNo, string containerNo, string articleCode, string description, Nullable<int> importTypeID, string sortedBy, string sortedDirection)
        {
            var receiptNoParameter = receiptNo != null ?
                new ObjectParameter("ReceiptNo", receiptNo) :
                new ObjectParameter("ReceiptNo", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var refNoParameter = refNo != null ?
                new ObjectParameter("RefNo", refNo) :
                new ObjectParameter("RefNo", typeof(string));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var importTypeIDParameter = importTypeID.HasValue ?
                new ObjectParameter("ImportTypeID", importTypeID) :
                new ObjectParameter("ImportTypeID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_WarehouseImportSearchResult_View>("WarehouseImportMng_function_SearchWarehouseImport", receiptNoParameter, proformaInvoiceNoParameter, clientUDParameter, clientNMParameter, bLNoParameter, refNoParameter, containerNoParameter, articleCodeParameter, descriptionParameter, importTypeIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_WarehouseImportSearchResult_View> WarehouseImportMng_function_SearchWarehouseImport(string receiptNo, string proformaInvoiceNo, string clientUD, string clientNM, string bLNo, string refNo, string containerNo, string articleCode, string description, Nullable<int> importTypeID, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var receiptNoParameter = receiptNo != null ?
                new ObjectParameter("ReceiptNo", receiptNo) :
                new ObjectParameter("ReceiptNo", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var refNoParameter = refNo != null ?
                new ObjectParameter("RefNo", refNo) :
                new ObjectParameter("RefNo", typeof(string));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var importTypeIDParameter = importTypeID.HasValue ?
                new ObjectParameter("ImportTypeID", importTypeID) :
                new ObjectParameter("ImportTypeID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_WarehouseImportSearchResult_View>("WarehouseImportMng_function_SearchWarehouseImport", mergeOption, receiptNoParameter, proformaInvoiceNoParameter, clientUDParameter, clientNMParameter, bLNoParameter, refNoParameter, containerNoParameter, articleCodeParameter, descriptionParameter, importTypeIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int WarehouseImportMng_function_CreateReservation(Nullable<int> wareHouseImportID, Nullable<int> updatedBy)
        {
            var wareHouseImportIDParameter = wareHouseImportID.HasValue ?
                new ObjectParameter("WareHouseImportID", wareHouseImportID) :
                new ObjectParameter("WareHouseImportID", typeof(int));
    
            var updatedByParameter = updatedBy.HasValue ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WarehouseImportMng_function_CreateReservation", wareHouseImportIDParameter, updatedByParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_OnSeaSparepart_View> WarehouseImportMng_function_SearchOnSeaSparepart(string searchQuery, string sortedBy, string sortedDirection)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_OnSeaSparepart_View>("WarehouseImportMng_function_SearchOnSeaSparepart", searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_OnSeaSparepart_View> WarehouseImportMng_function_SearchOnSeaSparepart(string searchQuery, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_OnSeaSparepart_View>("WarehouseImportMng_function_SearchOnSeaSparepart", mergeOption, searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int WarehouseImportMng_function_GenerateReceipNo(Nullable<int> warehouseImportID, string season)
        {
            var warehouseImportIDParameter = warehouseImportID.HasValue ?
                new ObjectParameter("WarehouseImportID", warehouseImportID) :
                new ObjectParameter("WarehouseImportID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WarehouseImportMng_function_GenerateReceipNo", warehouseImportIDParameter, seasonParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_CreditNoteProduct_View> WarehouseImportMng_function_SearchCreditNoteProduct(string searchQuery, string sortedBy, string sortedDirection)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_CreditNoteProduct_View>("WarehouseImportMng_function_SearchCreditNoteProduct", searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_CreditNoteProduct_View> WarehouseImportMng_function_SearchCreditNoteProduct(string searchQuery, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_CreditNoteProduct_View>("WarehouseImportMng_function_SearchCreditNoteProduct", mergeOption, searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_CreditNoteSparepart_View> WarehouseImportMng_function_SearchCreditNoteSparepart(string searchQuery, string sortedBy, string sortedDirection)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_CreditNoteSparepart_View>("WarehouseImportMng_function_SearchCreditNoteSparepart", searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_CreditNoteSparepart_View> WarehouseImportMng_function_SearchCreditNoteSparepart(string searchQuery, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var searchQueryParameter = searchQuery != null ?
                new ObjectParameter("SearchQuery", searchQuery) :
                new ObjectParameter("SearchQuery", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_CreditNoteSparepart_View>("WarehouseImportMng_function_SearchCreditNoteSparepart", mergeOption, searchQueryParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_ExportToWexData_View> WarehouseImportMng_function_CreateWexCSVFile(Nullable<int> warehouseImportID)
        {
            var warehouseImportIDParameter = warehouseImportID.HasValue ?
                new ObjectParameter("WarehouseImportID", warehouseImportID) :
                new ObjectParameter("WarehouseImportID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_ExportToWexData_View>("WarehouseImportMng_function_CreateWexCSVFile", warehouseImportIDParameter);
        }
    
        public virtual ObjectResult<WarehouseImportMng_ExportToWexData_View> WarehouseImportMng_function_CreateWexCSVFile(Nullable<int> warehouseImportID, MergeOption mergeOption)
        {
            var warehouseImportIDParameter = warehouseImportID.HasValue ?
                new ObjectParameter("WarehouseImportID", warehouseImportID) :
                new ObjectParameter("WarehouseImportID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarehouseImportMng_ExportToWexData_View>("WarehouseImportMng_function_CreateWexCSVFile", mergeOption, warehouseImportIDParameter);
        }
    }
}