﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ECommercialInvoiceMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ECommercialInvoiceMngEntities : DbContext
    {
        public ECommercialInvoiceMngEntities()
            : base("name=ECommercialInvoiceMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ECommercialInvoiceDetailDescription> ECommercialInvoiceDetailDescription { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceExtDetail_View> ECommercialInvoiceMng_ECommercialInvoiceExtDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_View> ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_DeliveryTerm> ECommercialInvoiceMng_DeliveryTerm { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_PaymentTerm> ECommercialInvoiceMng_PaymentTerm { get; set; }
        public virtual DbSet<ECommercialInvoiceExtDetail> ECommercialInvoiceExtDetail { get; set; }
        public virtual DbSet<ECommercialInvoiceDescription_ReportView> ECommercialInvoiceMng_ECommercialInvoiceDescription_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceDetail_ReportView> ECommercialInvoiceMng_ECommercialInvoiceDetail_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceDetailDescription_ReportView> ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceExtDetail_ReportView> ECommercialInvoiceMng_ECommercialInvoiceExtDetail_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoice_ReportView> ECommercialInvoiceMng_ECommercialInvoice_ReportView { get; set; }
        public virtual DbSet<Container_ReportView> ECommercialInvoiceMng_Container_ReportView { get; set; }
        public virtual DbSet<PackingListDetailExtend_ReportView> ECommercialInvoiceMng_PackingListDetailExtend_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoice> ECommercialInvoice { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceDescription_View> ECommercialInvoiceMng_ECommercialInvoiceDescription_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_InitWarehouseInfo_View> ECommercialInvoiceMng_InitWarehouseInfo_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoice_View> ECommercialInvoiceMng_ECommercialInvoice_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_Client_View> ECommercialInvoiceMng_Client_View { get; set; }
        public virtual DbSet<ECommercialInvoiceDescription> ECommercialInvoiceDescription { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_Booking_View> ECommercialInvoiceMng_Booking_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_InitFobInvoice_View> ECommercialInvoiceMng_InitFobInvoice_View { get; set; }
        public virtual DbSet<ECommercialInvoiceDetail> ECommercialInvoiceDetail { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceDetail_View> ECommercialInvoiceMng_ECommercialInvoiceDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_PurchasingInvoice_View> ECommercialInvoiceMng_PurchasingInvoice_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_PurchasingInvoiceDetail_View> ECommercialInvoiceMng_PurchasingInvoiceDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_PurchasingInvoiceSparepartDetail_View> ECommercialInvoiceMng_PurchasingInvoiceSparepartDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View> ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceSparepartDetail> ECommercialInvoiceSparepartDetail { get; set; }
        public virtual DbSet<ECommercialInvoiceSparepartDetail_ReportView> ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_ReportView { get; set; }
        public virtual DbSet<PackingListSparepartDetail_ReportView> ECommercialInvoiceMng_PackingListSparepartDetail_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_CreditNote_View> ECommercialInvoiceMng_CreditNote_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_SubInfo_View> ECommercialInvoiceMng_SubInfo_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_TurnOverLedgerAccount_View> ECommercialInvoiceMng_TurnOverLedgerAccount_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehousePickingList_View> ECommercialInvoiceMng_WarehousePickingList_View { get; set; }
        public virtual DbSet<WarehouseInvoiceProductDetail> WarehouseInvoiceProductDetail { get; set; }
        public virtual DbSet<WarehouseInvoiceSparepartDetail> WarehouseInvoiceSparepartDetail { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehouseInvoiceProductDetail_View> ECommercialInvoiceMng_WarehouseInvoiceProductDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_View> ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_View { get; set; }
        public virtual DbSet<WarehouseInvoiceProductDetail_ReportView> ECommercialInvoiceMng_WarehouseInvoiceProductDetail_ReportView { get; set; }
        public virtual DbSet<WarehouseInvoiceSparepartDetail_ReportView> ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_SaleOrder_View> ECommercialInvoiceMng_SaleOrder_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehouseImport_View> ECommercialInvoiceMng_WarehouseImport_View { get; set; }
        public virtual DbSet<ReportTemplate> ReportTemplate { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_BillTransport_View> ECommercialInvoiceMng_BillTransport_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ContainerTransport_View> ECommercialInvoiceMng_ContainerTransport_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_View> ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_View { get; set; }
        public virtual DbSet<ECommercialInvoiceContainerTransport> ECommercialInvoiceContainerTransport { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ClientCostItem_View> ECommercialInvoiceMng_ClientCostItem_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ClientOfferCostItem_View> ECommercialInvoiceMng_ClientOfferCostItem_View { get; set; }
        public virtual DbSet<ECommercialInvoiceContainerTransport_ReportView> ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_Booking_OverView> ECommercialInvoiceMng_Booking_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_CreditNote_OverView> ECommercialInvoiceMng_CreditNote_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoice_OverView> ECommercialInvoiceMng_ECommercialInvoice_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView> ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView> ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView> ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_OverView> ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView> ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView> ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehouseImport_OverView> ECommercialInvoiceMng_WarehouseImport_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView> ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView> ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehousePickingListDetail_View> ECommercialInvoiceMng_WarehousePickingListDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_WarehouseInvoice_View> ECommercialInvoiceMng_WarehouseInvoice_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_View> ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_View { get; set; }
        public virtual DbSet<ECommercialInvoiceSparepartDetailDescription> ECommercialInvoiceSparepartDetailDescription { get; set; }
        public virtual DbSet<ECommercialInvoiceSparepartDetailDescription_ReportView> ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoicePurchasing_View> ECommercialInvoiceMng_ECommercialInvoicePurchasing_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View> ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View { get; set; }
        public virtual DbSet<ECommercialInvoiceSampleDetail> ECommercialInvoiceSampleDetail { get; set; }
        public virtual DbSet<ECommercialInvoiceSampleDetailDescription> ECommercialInvoiceSampleDetailDescription { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_View> ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_View> ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_PurchasingInvoiceSampleDetail_View> ECommercialInvoiceMng_PurchasingInvoiceSampleDetail_View { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_ReportView> ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_ReportView> ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_ReportView { get; set; }
        public virtual DbSet<ECommercialInvoiceMng_PackingListSampleDetail_ReportView> ECommercialInvoiceMng_PackingListSampleDetail_ReportView { get; set; }
        public virtual DbSet<NotificationMessage> NotificationMessage { get; set; }
        public virtual DbSet<PackingListDetail_ReportView> ECommercialInvoiceMng_PackingListDetail_ReportView { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<SaleOrder> SaleOrder { get; set; }
    
        public virtual ObjectResult<Nullable<bool>> ECommercialInvoiceMng_function_GenerateWarehouseInvoice(Nullable<int> warehousePickingPlanID)
        {
            var warehousePickingPlanIDParameter = warehousePickingPlanID.HasValue ?
                new ObjectParameter("WarehousePickingPlanID", warehousePickingPlanID) :
                new ObjectParameter("WarehousePickingPlanID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("ECommercialInvoiceMng_function_GenerateWarehouseInvoice", warehousePickingPlanIDParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_Client_View> ECommercialInvoiceMng_function_SearchClient(string sortedBy, string sortedDirection, string clientUD, string clientNM)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_Client_View>("ECommercialInvoiceMng_function_SearchClient", sortedByParameter, sortedDirectionParameter, clientUDParameter, clientNMParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_Client_View> ECommercialInvoiceMng_function_SearchClient(string sortedBy, string sortedDirection, string clientUD, string clientNM, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_Client_View>("ECommercialInvoiceMng_function_SearchClient", mergeOption, sortedByParameter, sortedDirectionParameter, clientUDParameter, clientNMParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_InitFobInvoice_View> ECommercialInvoiceMng_function_SearchInitFobInvoice(string sortedBy, string sortedDirection, string clientUD, string clientNM)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_InitFobInvoice_View>("ECommercialInvoiceMng_function_SearchInitFobInvoice", sortedByParameter, sortedDirectionParameter, clientUDParameter, clientNMParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_InitFobInvoice_View> ECommercialInvoiceMng_function_SearchInitFobInvoice(string sortedBy, string sortedDirection, string clientUD, string clientNM, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_InitFobInvoice_View>("ECommercialInvoiceMng_function_SearchInitFobInvoice", mergeOption, sortedByParameter, sortedDirectionParameter, clientUDParameter, clientNMParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_InitWarehouseInfo_View> ECommercialInvoiceMng_function_SearchInitWarehouseInvoice(string sortedBy, string sortedDirection, string clientUD, string clientNM)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_InitWarehouseInfo_View>("ECommercialInvoiceMng_function_SearchInitWarehouseInvoice", sortedByParameter, sortedDirectionParameter, clientUDParameter, clientNMParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_InitWarehouseInfo_View> ECommercialInvoiceMng_function_SearchInitWarehouseInvoice(string sortedBy, string sortedDirection, string clientUD, string clientNM, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_InitWarehouseInfo_View>("ECommercialInvoiceMng_function_SearchInitWarehouseInvoice", mergeOption, sortedByParameter, sortedDirectionParameter, clientUDParameter, clientNMParameter);
        }
    
        public virtual int FactoryOrderInfoCacheMng_function_BuildCacheForCommercialInvoice(Nullable<int> eCommercialInvoiceID)
        {
            var eCommercialInvoiceIDParameter = eCommercialInvoiceID.HasValue ?
                new ObjectParameter("ECommercialInvoiceID", eCommercialInvoiceID) :
                new ObjectParameter("ECommercialInvoiceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FactoryOrderInfoCacheMng_function_BuildCacheForCommercialInvoice", eCommercialInvoiceIDParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_ClientOfferCostItem_View> ECommercialInvoiceMng_function_GetClientOfferCostItem(Nullable<int> clientID, Nullable<int> bookingID)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var bookingIDParameter = bookingID.HasValue ?
                new ObjectParameter("BookingID", bookingID) :
                new ObjectParameter("BookingID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_ClientOfferCostItem_View>("ECommercialInvoiceMng_function_GetClientOfferCostItem", clientIDParameter, bookingIDParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_ClientOfferCostItem_View> ECommercialInvoiceMng_function_GetClientOfferCostItem(Nullable<int> clientID, Nullable<int> bookingID, MergeOption mergeOption)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var bookingIDParameter = bookingID.HasValue ?
                new ObjectParameter("BookingID", bookingID) :
                new ObjectParameter("BookingID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_ClientOfferCostItem_View>("ECommercialInvoiceMng_function_GetClientOfferCostItem", mergeOption, clientIDParameter, bookingIDParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View> ECommercialInvoiceMng_function_SearchECommercialInvoice(string sortedBy, string sortedDirection, string invoiceNo, string clientUD, string clientNM, string bLNo, string orderNo, string clientInvoiceNo, Nullable<int> typeOfInvoice, string containerNo, string cMRNo, string eTA, string eTA2, string eTD, string season, Nullable<int> internalCompanyID)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var orderNoParameter = orderNo != null ?
                new ObjectParameter("OrderNo", orderNo) :
                new ObjectParameter("OrderNo", typeof(string));
    
            var clientInvoiceNoParameter = clientInvoiceNo != null ?
                new ObjectParameter("ClientInvoiceNo", clientInvoiceNo) :
                new ObjectParameter("ClientInvoiceNo", typeof(string));
    
            var typeOfInvoiceParameter = typeOfInvoice.HasValue ?
                new ObjectParameter("TypeOfInvoice", typeOfInvoice) :
                new ObjectParameter("TypeOfInvoice", typeof(int));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var cMRNoParameter = cMRNo != null ?
                new ObjectParameter("CMRNo", cMRNo) :
                new ObjectParameter("CMRNo", typeof(string));
    
            var eTAParameter = eTA != null ?
                new ObjectParameter("ETA", eTA) :
                new ObjectParameter("ETA", typeof(string));
    
            var eTA2Parameter = eTA2 != null ?
                new ObjectParameter("ETA2", eTA2) :
                new ObjectParameter("ETA2", typeof(string));
    
            var eTDParameter = eTD != null ?
                new ObjectParameter("ETD", eTD) :
                new ObjectParameter("ETD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var internalCompanyIDParameter = internalCompanyID.HasValue ?
                new ObjectParameter("InternalCompanyID", internalCompanyID) :
                new ObjectParameter("InternalCompanyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View>("ECommercialInvoiceMng_function_SearchECommercialInvoice", sortedByParameter, sortedDirectionParameter, invoiceNoParameter, clientUDParameter, clientNMParameter, bLNoParameter, orderNoParameter, clientInvoiceNoParameter, typeOfInvoiceParameter, containerNoParameter, cMRNoParameter, eTAParameter, eTA2Parameter, eTDParameter, seasonParameter, internalCompanyIDParameter);
        }
    
        public virtual ObjectResult<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View> ECommercialInvoiceMng_function_SearchECommercialInvoice(string sortedBy, string sortedDirection, string invoiceNo, string clientUD, string clientNM, string bLNo, string orderNo, string clientInvoiceNo, Nullable<int> typeOfInvoice, string containerNo, string cMRNo, string eTA, string eTA2, string eTD, string season, Nullable<int> internalCompanyID, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var invoiceNoParameter = invoiceNo != null ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var orderNoParameter = orderNo != null ?
                new ObjectParameter("OrderNo", orderNo) :
                new ObjectParameter("OrderNo", typeof(string));
    
            var clientInvoiceNoParameter = clientInvoiceNo != null ?
                new ObjectParameter("ClientInvoiceNo", clientInvoiceNo) :
                new ObjectParameter("ClientInvoiceNo", typeof(string));
    
            var typeOfInvoiceParameter = typeOfInvoice.HasValue ?
                new ObjectParameter("TypeOfInvoice", typeOfInvoice) :
                new ObjectParameter("TypeOfInvoice", typeof(int));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var cMRNoParameter = cMRNo != null ?
                new ObjectParameter("CMRNo", cMRNo) :
                new ObjectParameter("CMRNo", typeof(string));
    
            var eTAParameter = eTA != null ?
                new ObjectParameter("ETA", eTA) :
                new ObjectParameter("ETA", typeof(string));
    
            var eTA2Parameter = eTA2 != null ?
                new ObjectParameter("ETA2", eTA2) :
                new ObjectParameter("ETA2", typeof(string));
    
            var eTDParameter = eTD != null ?
                new ObjectParameter("ETD", eTD) :
                new ObjectParameter("ETD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var internalCompanyIDParameter = internalCompanyID.HasValue ?
                new ObjectParameter("InternalCompanyID", internalCompanyID) :
                new ObjectParameter("InternalCompanyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View>("ECommercialInvoiceMng_function_SearchECommercialInvoice", mergeOption, sortedByParameter, sortedDirectionParameter, invoiceNoParameter, clientUDParameter, clientNMParameter, bLNoParameter, orderNoParameter, clientInvoiceNoParameter, typeOfInvoiceParameter, containerNoParameter, cMRNoParameter, eTAParameter, eTA2Parameter, eTDParameter, seasonParameter, internalCompanyIDParameter);
        }
    }
}
