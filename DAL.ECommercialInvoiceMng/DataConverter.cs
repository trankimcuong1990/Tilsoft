using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ECommercialInvoiceMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ECommercialInvoiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoicePurchasing_View, DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescription>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceDetail_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDetailDescriptions, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceSampleDetailDescriptions, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetailDescription>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceExtDetail_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceExtDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceDescription_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceDescription>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d=>d.eCommercialInvoiceSparepartDetailDescriptions, o=>o.MapFrom(s=>s.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetailDescription>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehouseInvoiceProductDetail_View, DTO.ECommercialInvoiceMng.WarehouseInvoiceProductDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_View, DTO.ECommercialInvoiceMng.WarehouseInvoiceSparepartDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_Booking_View, DTO.ECommercialInvoiceMng.Booking>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_CreditNote_View, DTO.ECommercialInvoiceMng.CreditNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehouseImport_View, DTO.ECommercialInvoiceMng.WarehouseImport>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_View, DTO.ECommercialInvoiceMng.ECommercialInvoiceContainerTransport>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ContainerTransport_View, DTO.ECommercialInvoiceMng.ContainerTransport>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_BillTransport_View, DTO.ECommercialInvoiceMng.BillTransport>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ContainerTransports, o => o.MapFrom(s => s.ECommercialInvoiceMng_ContainerTransport_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoice_View, DTO.ECommercialInvoiceMng.ECommercialInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.IsDonePaymentText, o => o.ResolveUsing(s => (s.IsDonePayment.HasValue && s.IsDonePayment == true ? Library.Helper.TEXT_INVOICE_DONE_PAYMENT : Library.Helper.TEXT_INVOICE_NOT_DONE_PAYMENT)))
                    .ForMember(d => d.IsDonePaymentLabel, o => o.ResolveUsing(s => (s.IsDonePayment.HasValue && s.IsDonePayment == true ? Library.Helper.LABEL_INVOICE_UNDO_DONE_PAYMENT : Library.Helper.LABEL_INVOICE_DONE_PAYMENT)))
                    .ForMember(d => d.ECommercialInvoiceDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceDetail_View))
                    .ForMember(d => d.ECommercialInvoiceExtDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceExtDetail_View))
                    .ForMember(d => d.ECommercialInvoiceDescriptions, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceDescription_View))
                    .ForMember(d => d.ECommercialInvoiceSparepartDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View))
                    .ForMember(d => d.ECommercialInvoiceContainerTransports, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_View))
                    .ForMember(d => d.WarehouseInvoiceProductDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_WarehouseInvoiceProductDetail_View))
                    .ForMember(d => d.WarehouseInvoiceSparepartDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_View))                    
                    .ForMember(d => d.ECommercialInvoiceSampleDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_View))                    
                    .ForMember(d => d.Bookings, o => o.MapFrom(s => s.ECommercialInvoiceMng_Booking_View))
                    .ForMember(d => d.CreditNotes, o => o.MapFrom(s => s.ECommercialInvoiceMng_CreditNote_View))
                    .ForMember(d => d.WarehouseImports, o => o.MapFrom(s => s.ECommercialInvoiceMng_WarehouseImport_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoice, ECommercialInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceID, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceExtDetail, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceDescription, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceContainerTransport, o => o.Ignore())
                    .ForMember(d => d.WarehouseInvoiceProductDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseInvoiceSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.PrintedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetail, ECommercialInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDetailID, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceDetailDescription, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceExtDetail, ECommercialInvoiceExtDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceExtDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceDescription, ECommercialInvoiceDescription>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDescriptionID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetail, ECommercialInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceSparepartDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceContainerTransport, ECommercialInvoiceContainerTransport>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ECommercialInvoiceContainerTransportID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescription, ECommercialInvoiceDetailDescription>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDetailDescriptionID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetailDescription, ECommercialInvoiceSparepartDetailDescription>()
                    .IgnoreAllNonExisting()
                    .ForSourceMember(d => d.ECommercialInvoiceSparepartDetailDescriptionID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.WarehouseInvoiceProductDetail, WarehouseInvoiceProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseInvoiceProductDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.WarehouseInvoiceSparepartDetail, WarehouseInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseInvoiceSparepartDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetail, ECommercialInvoiceSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceSampleDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetailDescription, ECommercialInvoiceSampleDetailDescription>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceSampleDetailDescriptionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_InitFobInvoice_View, DTO.ECommercialInvoiceMng.InitFobInvoice>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_DeliveryTerm, DTO.ECommercialInvoiceMng.DeliveryTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_PaymentTerm, DTO.ECommercialInvoiceMng.PaymentTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_TurnOverLedgerAccount_View, DTO.ECommercialInvoiceMng.TurnOverLedgerAccount>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoice_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_InitWarehouseInfo_View, DTO.ECommercialInvoiceMng.InitWarehouseInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehousePickingListDetail_View, DTO.ECommercialInvoiceMng.WarehousePickingListDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehousePickingList_View, DTO.ECommercialInvoiceMng.WarehousePickingList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehousePickingListDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_WarehousePickingListDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_PurchasingInvoiceDetail_View, DTO.ECommercialInvoiceMng.PurchasingInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_PurchasingInvoiceSparepartDetail_View, DTO.ECommercialInvoiceMng.PurchasingInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_PurchasingInvoiceSampleDetail_View, DTO.ECommercialInvoiceMng.PurchasingInvoiceSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_PurchasingInvoice_View, DTO.ECommercialInvoiceMng.PurchasingInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchasingInvoiceDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_PurchasingInvoiceDetail_View))
                    .ForMember(d => d.PurchasingInvoiceSparepartDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_PurchasingInvoiceSparepartDetail_View))
                    .ForMember(d => d.PurchasingInvoiceSampleDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_PurchasingInvoiceSampleDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoice_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_Client_View, DTO.ECommercialInvoiceMng.Client>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ReturnProduct_View, DTO.ECommercialInvoiceMng.ReturnProduct>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ReturnSparepart_View, DTO.ECommercialInvoiceMng.ReturnSparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ClientCostItem_View, DTO.ECommercialInvoiceMng.ClientCostItem>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ClientOfferCostItem_View, DTO.ECommercialInvoiceMng.ClientOfferCostItem>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //For Overview

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescriptionOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDetailDescriptions, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_OverView))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceExtDetailOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceDescriptionOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetailOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView, DTO.ECommercialInvoiceMng.WarehouseInvoiceProductDetailOverview>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView, DTO.ECommercialInvoiceMng.WarehouseInvoiceSparepartDetailOverview>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_Booking_OverView, DTO.ECommercialInvoiceMng.BookingOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_CreditNote_OverView, DTO.ECommercialInvoiceMng.CreditNoteOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehouseImport_OverView, DTO.ECommercialInvoiceMng.WarehouseImportOverview>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceContainerTransportOverview>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_ECommercialInvoice_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView))
                    .ForMember(d => d.ECommercialInvoiceExtDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView))
                    .ForMember(d => d.ECommercialInvoiceDescriptions, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView))
                    .ForMember(d => d.ECommercialInvoiceSparepartDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView))
                    .ForMember(d => d.ECommercialInvoiceContainerTransports, o => o.MapFrom(s => s.ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView))
                    .ForMember(d => d.WarehouseInvoiceProductDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView))
                    .ForMember(d => d.WarehouseInvoiceSparepartDetails, o => o.MapFrom(s => s.ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView))
                    .ForMember(d => d.Bookings, o => o.MapFrom(s => s.ECommercialInvoiceMng_Booking_OverView))
                    .ForMember(d => d.CreditNotes, o => o.MapFrom(s => s.ECommercialInvoiceMng_CreditNote_OverView))
                    .ForMember(d => d.WarehouseImports, o => o.MapFrom(s => s.ECommercialInvoiceMng_WarehouseImport_OverView))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceOverview, ECommercialInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceID, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceExtDetail, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceDescription, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceContainerTransport, o => o.Ignore())
                    .ForMember(d => d.WarehouseInvoiceProductDetail, o => o.Ignore())
                    .ForMember(d => d.WarehouseInvoiceSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.PrintedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailOverview, ECommercialInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDetailID, o => o.Ignore())
                    .ForMember(d => d.ECommercialInvoiceDetailDescription, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceExtDetailOverview, ECommercialInvoiceExtDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceExtDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceDescriptionOverview, ECommercialInvoiceDescription>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDescriptionID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetailOverview, ECommercialInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceSparepartDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceContainerTransportOverview, ECommercialInvoiceContainerTransport>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ECommercialInvoiceContainerTransportID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescriptionOverview, ECommercialInvoiceDetailDescription>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ECommercialInvoiceDetailDescriptionID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.WarehouseInvoiceProductDetailOverview, WarehouseInvoiceProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseInvoiceProductDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ECommercialInvoiceMng.WarehouseInvoiceSparepartDetailOverview, WarehouseInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseInvoiceSparepartDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<ECommercialInvoiceMng_WarehouseInvoice_View, DTO.ECommercialInvoiceMng.WarehouseInvoice>()
                    .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult> DB2DTO_ECommercialInvoiceSearch(List<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View>, List<DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult>>(dbItems);
        }

        public DTO.ECommercialInvoiceMng.ECommercialInvoice DB2DTO_ECommercialInvoice(ECommercialInvoiceMng_ECommercialInvoice_View dbItem)
        {
            DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem = AutoMapper.Mapper.Map<ECommercialInvoiceMng_ECommercialInvoice_View, DTO.ECommercialInvoiceMng.ECommercialInvoice>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.InvoiceDate.HasValue)
                dtoItem.InvoiceDateFormated = dbItem.InvoiceDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");
                        
            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PrintedDate.HasValue)
                dtoItem.PrintedDateFormated = dbItem.PrintedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.ConfirmedDate.HasValue)
                dtoItem.ConfirmedDateFormated = dbItem.ConfirmedDate.Value.ToString("dd/MM/yyyy");


            return dtoItem;
        }

        public void DTO2DB_ECommercialInvoice(DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem, ref ECommercialInvoice dbItem)
        {
            /*
             * MAP & CHECK ECommercialInvoiceDetailDescription DELETED
             */
            List<ECommercialInvoiceDetail> itemNeedDelete_Details = new List<ECommercialInvoiceDetail>();
            List<ECommercialInvoiceDetailDescription> itemNeedDelete_DetailDescriptions;
            if (dtoItem.ECommercialInvoiceDetails != null)
            {
                //CHECK
                foreach (ECommercialInvoiceDetail dbDetail in dbItem.ECommercialInvoiceDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.ECommercialInvoiceDetails.Select(s => s.ECommercialInvoiceDetailID).Contains(dbDetail.ECommercialInvoiceDetailID))
                    {
                        itemNeedDelete_Details.Add(dbDetail);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_DetailDescriptions = new List<ECommercialInvoiceDetailDescription>();
                        foreach (ECommercialInvoiceDetailDescription dbDetailDescription in dbDetail.ECommercialInvoiceDetailDescription)
                        {
                            if (!dtoItem.ECommercialInvoiceDetails.Where(o => o.ECommercialInvoiceDetailID == dbDetail.ECommercialInvoiceDetailID).FirstOrDefault().ECommercialInvoiceDetailDescriptions.Select(x => x.ECommercialInvoiceDetailDescriptionID).Contains(dbDetailDescription.ECommercialInvoiceDetailDescriptionID))
                            {
                                itemNeedDelete_DetailDescriptions.Add(dbDetailDescription);
                            }
                        }
                        foreach (ECommercialInvoiceDetailDescription dbDetailDescription in itemNeedDelete_DetailDescriptions)
                        {
                            dbDetail.ECommercialInvoiceDetailDescription.Remove(dbDetailDescription);
                        }
                    }
                }

                foreach (ECommercialInvoiceDetail dbDetail in itemNeedDelete_Details)
                {
                    List<ECommercialInvoiceDetailDescription> item_deleteds = new List<ECommercialInvoiceDetailDescription>();
                    foreach (ECommercialInvoiceDetailDescription dbDetailDescription in dbDetail.ECommercialInvoiceDetailDescription)
                    {
                        item_deleteds.Add(dbDetailDescription);
                    }
                    foreach (ECommercialInvoiceDetailDescription item in item_deleteds)
                    {
                        dbItem.ECommercialInvoiceDetail.Where(o => o.ECommercialInvoiceDetailID == dbDetail.ECommercialInvoiceDetailID).FirstOrDefault().ECommercialInvoiceDetailDescription.Remove(item);
                    }
                    dbItem.ECommercialInvoiceDetail.Remove(dbDetail);
                }

                //MAP
                ECommercialInvoiceDetail _dbDetail;
                ECommercialInvoiceDetailDescription _dbDetailDescription;
                foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceDetail dtoDetail in dtoItem.ECommercialInvoiceDetails)
                {
                    if (dtoDetail.ECommercialInvoiceDetailID < 0)
                    {
                        _dbDetail = new ECommercialInvoiceDetail();

                        if (dtoDetail.ECommercialInvoiceDetailDescriptions != null)
                        {
                            foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescription dtoDetailExtend in dtoDetail.ECommercialInvoiceDetailDescriptions)
                            {
                                _dbDetailDescription = new ECommercialInvoiceDetailDescription();
                                _dbDetail.ECommercialInvoiceDetailDescription.Add(_dbDetailDescription);
                                AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescription, ECommercialInvoiceDetailDescription>(dtoDetailExtend, _dbDetailDescription);
                            }
                        }

                        dbItem.ECommercialInvoiceDetail.Add(_dbDetail);
                    }
                    else
                    {
                        _dbDetail = dbItem.ECommercialInvoiceDetail.FirstOrDefault(o => o.ECommercialInvoiceDetailID == dtoDetail.ECommercialInvoiceDetailID);
                        if (_dbDetail != null && dtoDetail.ECommercialInvoiceDetailDescriptions != null)
                        {
                            foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescription dtoDetailExtend in dtoDetail.ECommercialInvoiceDetailDescriptions)
                            {
                                if (dtoDetailExtend.ECommercialInvoiceDetailDescriptionID < 0)
                                {
                                    _dbDetailDescription = new ECommercialInvoiceDetailDescription();
                                    _dbDetail.ECommercialInvoiceDetailDescription.Add(_dbDetailDescription);
                                }
                                else
                                {
                                    _dbDetailDescription = _dbDetail.ECommercialInvoiceDetailDescription.FirstOrDefault(o => o.ECommercialInvoiceDetailDescriptionID == dtoDetailExtend.ECommercialInvoiceDetailDescriptionID);
                                }
                                if (_dbDetailDescription != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetailDescription, ECommercialInvoiceDetailDescription>(dtoDetailExtend, _dbDetailDescription);
                                }
                            }
                        }
                    }

                    if (_dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetail, ECommercialInvoiceDetail>(dtoDetail, _dbDetail);
                    }
                }
            }



            /*
             * MAP & CHECK ECommercialInvoiceExtDetail DELETED
             */
            List<ECommercialInvoiceExtDetail> ItemNeedDelete_ExtDetails = new List<ECommercialInvoiceExtDetail>();
            if (dtoItem.ECommercialInvoiceExtDetails != null)
            {
                //CHECK
                foreach (ECommercialInvoiceExtDetail dbExtDetail in dbItem.ECommercialInvoiceExtDetail.Where(o => !dtoItem.ECommercialInvoiceExtDetails.Select(s => s.ECommercialInvoiceExtDetailID).Contains(o.ECommercialInvoiceExtDetailID)))
                {
                    ItemNeedDelete_ExtDetails.Add(dbExtDetail);
                }
                foreach (ECommercialInvoiceExtDetail dbExtDetail in ItemNeedDelete_ExtDetails)
                {
                    dbItem.ECommercialInvoiceExtDetail.Remove(dbExtDetail);
                }
                //MAP
                foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceExtDetail dtoExtDetail in dtoItem.ECommercialInvoiceExtDetails)
                {
                    ECommercialInvoiceExtDetail dbExtDetail;
                    if (dtoExtDetail.ECommercialInvoiceExtDetailID < 0)
                    {
                        dbExtDetail = new ECommercialInvoiceExtDetail();
                        dbItem.ECommercialInvoiceExtDetail.Add(dbExtDetail);
                    }
                    else
                    {
                        dbExtDetail = dbItem.ECommercialInvoiceExtDetail.FirstOrDefault(o => o.ECommercialInvoiceExtDetailID == dtoExtDetail.ECommercialInvoiceExtDetailID);
                    }

                    if (dbExtDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceExtDetail, ECommercialInvoiceExtDetail>(dtoExtDetail, dbExtDetail);
                    }
                }
            }

            /*
             * MAP & CHECK ECommercialInvoiceDescription DELETED
             */
            List<ECommercialInvoiceDescription> ItemNeedDelete_Descriptions = new List<ECommercialInvoiceDescription>();
            if (dtoItem.ECommercialInvoiceDescriptions != null)
            {
                //CHECK
                foreach (ECommercialInvoiceDescription dbDescription in dbItem.ECommercialInvoiceDescription.Where(o => !dtoItem.ECommercialInvoiceDescriptions.Select(s => s.ECommercialInvoiceDescriptionID).Contains(o.ECommercialInvoiceDescriptionID)))
                {
                    ItemNeedDelete_Descriptions.Add(dbDescription);
                }
                foreach (ECommercialInvoiceDescription dbDescription in ItemNeedDelete_Descriptions)
                {
                    dbItem.ECommercialInvoiceDescription.Remove(dbDescription);
                }
                //MAP
                foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceDescription dtoDescription in dtoItem.ECommercialInvoiceDescriptions)
                {
                    ECommercialInvoiceDescription dbDescription;
                    if (dtoDescription.ECommercialInvoiceDescriptionID < 0)
                    {
                        dbDescription = new ECommercialInvoiceDescription();
                        dbItem.ECommercialInvoiceDescription.Add(dbDescription);
                    }
                    else
                    {
                        dbDescription = dbItem.ECommercialInvoiceDescription.FirstOrDefault(o => o.ECommercialInvoiceDescriptionID == dtoDescription.ECommercialInvoiceDescriptionID);
                    }

                    if (dbDescription != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceDescription, ECommercialInvoiceDescription>(dtoDescription, dbDescription);
                    }
                }
            }


            /*
             * MAP & CHECK ECommercialInvoiceSparepartDetail DELETED
             */
            List<ECommercialInvoiceSparepartDetail> need_delete_spareparts = new List<ECommercialInvoiceSparepartDetail>();
            List<ECommercialInvoiceSparepartDetailDescription> itemNeedDelete_SparepartDetailDescriptions;
            if (dtoItem.ECommercialInvoiceSparepartDetails != null)
            {
                //CHECK
                foreach(var dbsDetail in dbItem.ECommercialInvoiceSparepartDetail)
                {
                    if (!dtoItem.ECommercialInvoiceSparepartDetails.Select(s => s.ECommercialInvoiceSparepartDetailID).Contains(dbsDetail.ECommercialInvoiceSparepartDetailID))
                    {
                        need_delete_spareparts.Add(dbsDetail);
                    }
                    else
                    {
                        itemNeedDelete_SparepartDetailDescriptions = new List<ECommercialInvoiceSparepartDetailDescription>();
                        foreach(var dbSparepartDetailDescription in dbsDetail.ECommercialInvoiceSparepartDetailDescription)
                        {
                            if(!dtoItem.ECommercialInvoiceSparepartDetails.Where(o=>o.ECommercialInvoiceSparepartDetailID == dbsDetail.ECommercialInvoiceSparepartDetailID).FirstOrDefault().eCommercialInvoiceSparepartDetailDescriptions.Select(x => x.ECommercialInvoiceSparepartDetailDescriptionID).Contains(dbSparepartDetailDescription.ECommercialInvoiceSparepartDetailDescriptionID))
                            {
                                itemNeedDelete_SparepartDetailDescriptions.Add(dbSparepartDetailDescription);
                            }
                        }
                        foreach(var dbSparepartDetailDescriptions in itemNeedDelete_SparepartDetailDescriptions)
                        {
                            dbsDetail.ECommercialInvoiceSparepartDetailDescription.Remove(dbSparepartDetailDescriptions);
                        }
                    }
                }

                foreach(var dbDeatil in need_delete_spareparts)
                {
                    List<ECommercialInvoiceSparepartDetailDescription> item_sdeleteds = new List<ECommercialInvoiceSparepartDetailDescription>();
                    foreach(var dbSparepartDetailDescription in dbDeatil.ECommercialInvoiceSparepartDetailDescription)
                    {
                        item_sdeleteds.Add(dbSparepartDetailDescription);
                    }
                    foreach(var itemk in item_sdeleteds)
                    {
                        dbItem.ECommercialInvoiceSparepartDetail.Where(o => o.ECommercialInvoiceSparepartDetailID == dbDeatil.ECommercialInvoiceSparepartDetailID).FirstOrDefault().ECommercialInvoiceSparepartDetailDescription.Remove(itemk);
                    }
                    dbItem.ECommercialInvoiceSparepartDetail.Remove(dbDeatil);
                }

                //MAP
                ECommercialInvoiceSparepartDetail _dbsDetail;
                ECommercialInvoiceSparepartDetailDescription _dbsDetailDescription;
                foreach (var item in dtoItem.ECommercialInvoiceSparepartDetails)
                {
                    if (item.ECommercialInvoiceSparepartDetailID < 0)
                    {
                        _dbsDetail = new ECommercialInvoiceSparepartDetail();
                        if(item.eCommercialInvoiceSparepartDetailDescriptions != null)
                        {
                            foreach(var dtoDetailDescription in item.eCommercialInvoiceSparepartDetailDescriptions)
                            {
                                _dbsDetailDescription = new ECommercialInvoiceSparepartDetailDescription();
                                _dbsDetail.ECommercialInvoiceSparepartDetailDescription.Add(_dbsDetailDescription);
                                AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetailDescription, ECommercialInvoiceSparepartDetailDescription>(dtoDetailDescription, _dbsDetailDescription);
                            }
                        }
                        dbItem.ECommercialInvoiceSparepartDetail.Add(_dbsDetail);
                    }
                    else
                    {
                        _dbsDetail = dbItem.ECommercialInvoiceSparepartDetail.FirstOrDefault(o => o.ECommercialInvoiceSparepartDetailID == item.ECommercialInvoiceSparepartDetailID);
                        if(_dbsDetail !=null && item.eCommercialInvoiceSparepartDetailDescriptions != null)
                        {
                            foreach(DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetailDescription dtoDetailDescription in item.eCommercialInvoiceSparepartDetailDescriptions)
                            {
                                if(dtoDetailDescription.ECommercialInvoiceSparepartDetailDescriptionID < 0)
                                {
                                    _dbsDetailDescription = new ECommercialInvoiceSparepartDetailDescription();
                                    _dbsDetail.ECommercialInvoiceSparepartDetailDescription.Add(_dbsDetailDescription);
                                }
                                else
                                {
                                    _dbsDetailDescription = _dbsDetail.ECommercialInvoiceSparepartDetailDescription.FirstOrDefault(o => o.ECommercialInvoiceSparepartDetailDescriptionID == dtoDetailDescription.ECommercialInvoiceSparepartDetailDescriptionID);

                                }
                                if(_dbsDetailDescription != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetailDescription, ECommercialInvoiceSparepartDetailDescription>(dtoDetailDescription, _dbsDetailDescription);
                                }
                            }
                        }
                    }

                    if (_dbsDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetail, ECommercialInvoiceSparepartDetail>(item, _dbsDetail);
                    }
                }
            }

            /*
             * MAP & CHECK WarehouseInvoiceProductDetail DELETED
             */
            List<WarehouseInvoiceProductDetail> need_delete_warehouse_product = new List<WarehouseInvoiceProductDetail>();
            if (dtoItem.WarehouseInvoiceProductDetails != null)
            {
                //CHECK
                foreach (var item in dbItem.WarehouseInvoiceProductDetail.Where(o => !dtoItem.WarehouseInvoiceProductDetails.Select(s => s.WarehouseInvoiceProductDetailID).Contains(o.WarehouseInvoiceProductDetailID)))
                {
                    need_delete_warehouse_product.Add(item);
                }
                foreach (var item in need_delete_warehouse_product)
                {
                    dbItem.WarehouseInvoiceProductDetail.Remove(item);
                }
                //MAP
                foreach (var item in dtoItem.WarehouseInvoiceProductDetails)
                {
                    WarehouseInvoiceProductDetail db_warehouse_product;
                    if (item.WarehouseInvoiceProductDetailID < 0)
                    {
                        db_warehouse_product = new WarehouseInvoiceProductDetail();
                        dbItem.WarehouseInvoiceProductDetail.Add(db_warehouse_product);
                    }
                    else
                    {
                        db_warehouse_product = dbItem.WarehouseInvoiceProductDetail.FirstOrDefault(o => o.WarehouseInvoiceProductDetailID == item.WarehouseInvoiceProductDetailID);
                    }

                    if (db_warehouse_product != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.WarehouseInvoiceProductDetail, WarehouseInvoiceProductDetail>(item, db_warehouse_product);
                    }
                }
            }

            /*
             * MAP & CHECK WarehouseInvoiceSpareparttDetail DELETED
             */
            List<WarehouseInvoiceSparepartDetail> need_delete_warehouse_sparepart = new List<WarehouseInvoiceSparepartDetail>();
            if (dtoItem.WarehouseInvoiceSparepartDetails != null)
            {
                //CHECK
                foreach (var item in dbItem.WarehouseInvoiceSparepartDetail.Where(o => !dtoItem.WarehouseInvoiceSparepartDetails.Select(s => s.WarehouseInvoiceSparepartDetailID).Contains(o.WarehouseInvoiceSparepartDetailID)))
                {
                    need_delete_warehouse_sparepart.Add(item);
                }
                foreach (var item in need_delete_warehouse_sparepart)
                {
                    dbItem.WarehouseInvoiceSparepartDetail.Remove(item);
                }
                //MAP
                foreach (var item in dtoItem.WarehouseInvoiceSparepartDetails)
                {
                    WarehouseInvoiceSparepartDetail db_warehouse_sparepart;
                    if (item.WarehouseInvoiceSparepartDetailID < 0)
                    {
                        db_warehouse_sparepart = new WarehouseInvoiceSparepartDetail();
                        dbItem.WarehouseInvoiceSparepartDetail.Add(db_warehouse_sparepart);
                    }
                    else
                    {
                        db_warehouse_sparepart = dbItem.WarehouseInvoiceSparepartDetail.FirstOrDefault(o => o.WarehouseInvoiceSparepartDetailID == item.WarehouseInvoiceSparepartDetailID);
                    }

                    if (db_warehouse_sparepart != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.WarehouseInvoiceSparepartDetail, WarehouseInvoiceSparepartDetail>(item, db_warehouse_sparepart);
                    }
                }
            }

            /*
             * MAP & CHECK ECommercialInvoiceContainerTransport
             */
            if (dtoItem.ECommercialInvoiceContainerTransports != null)
            {
                foreach (var item in dbItem.ECommercialInvoiceContainerTransport.ToArray())
                {
                    if (!dtoItem.ECommercialInvoiceContainerTransports.Select(s => s.ECommercialInvoiceContainerTransportID).Contains(item.ECommercialInvoiceContainerTransportID))
                    {
                        dbItem.ECommercialInvoiceContainerTransport.Remove(item);
                    }
                }
                foreach (var item in dtoItem.ECommercialInvoiceContainerTransports)
                {
                    ECommercialInvoiceContainerTransport dbDetail = new ECommercialInvoiceContainerTransport();
                    if (item.ECommercialInvoiceContainerTransportID < 0)
                    {
                        if (item.ClientCostItemID.HasValue)
                        {
                            dbItem.ECommercialInvoiceContainerTransport.Add(dbDetail);
                        }
                    }
                    else
                    {
                        dbDetail = dbItem.ECommercialInvoiceContainerTransport.Where(o => o.ECommercialInvoiceContainerTransportID == item.ECommercialInvoiceContainerTransportID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceContainerTransport, ECommercialInvoiceContainerTransport>(item, dbDetail);
                    }
                }
            }

            /*
             * MAP & CHECK ECommercialInvoiceSampleDetailDescription DELETED
             */
            List<ECommercialInvoiceSampleDetail> itemNeedDelete_SampleDetails = new List<ECommercialInvoiceSampleDetail>();
            List<ECommercialInvoiceSampleDetailDescription> itemNeedDelete_SampleDetailDescriptions;
            if (dtoItem.ECommercialInvoiceSampleDetails != null)
            {
                //CHECK
                foreach (ECommercialInvoiceSampleDetail dbDetail in dbItem.ECommercialInvoiceSampleDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.ECommercialInvoiceSampleDetails.Select(s => s.ECommercialInvoiceSampleDetailID).Contains(dbDetail.ECommercialInvoiceSampleDetailID))
                    {
                        itemNeedDelete_SampleDetails.Add(dbDetail);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_SampleDetailDescriptions = new List<ECommercialInvoiceSampleDetailDescription>();
                        foreach (ECommercialInvoiceSampleDetailDescription dbDetailSampleDescription in dbDetail.ECommercialInvoiceSampleDetailDescription)
                        {
                            if (!dtoItem.ECommercialInvoiceSampleDetails.Where(o => o.ECommercialInvoiceSampleDetailID == dbDetail.ECommercialInvoiceSampleDetailID).FirstOrDefault().ECommercialInvoiceSampleDetailDescriptions.Select(x => x.ECommercialInvoiceSampleDetailDescriptionID).Contains(dbDetailSampleDescription.ECommercialInvoiceSampleDetailDescriptionID))
                            {
                                itemNeedDelete_SampleDetailDescriptions.Add(dbDetailSampleDescription);
                            }
                        }
                        foreach (ECommercialInvoiceSampleDetailDescription dbDetailDescription in itemNeedDelete_SampleDetailDescriptions)
                        {
                            dbDetail.ECommercialInvoiceSampleDetailDescription.Remove(dbDetailDescription);
                        }
                    }
                }

                foreach (ECommercialInvoiceSampleDetail dbDetail in itemNeedDelete_SampleDetails)
                {
                    List<ECommercialInvoiceSampleDetailDescription> item_deleteds = new List<ECommercialInvoiceSampleDetailDescription>();
                    foreach (ECommercialInvoiceSampleDetailDescription dbDetailDescription in dbDetail.ECommercialInvoiceSampleDetailDescription)
                    {
                        item_deleteds.Add(dbDetailDescription);
                    }
                    foreach (ECommercialInvoiceSampleDetailDescription item in item_deleteds)
                    {
                        dbItem.ECommercialInvoiceSampleDetail.Where(o => o.ECommercialInvoiceSampleDetailID == dbDetail.ECommercialInvoiceSampleDetailID).FirstOrDefault().ECommercialInvoiceSampleDetailDescription.Remove(item);
                    }
                    dbItem.ECommercialInvoiceSampleDetail.Remove(dbDetail);
                }

                //MAP
                ECommercialInvoiceSampleDetail _dbDetail;
                ECommercialInvoiceSampleDetailDescription _dbDetailDescription;
                foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetail dtoDetail in dtoItem.ECommercialInvoiceSampleDetails)
                {
                    if (dtoDetail.ECommercialInvoiceSampleDetailID < 0)
                    {
                        _dbDetail = new ECommercialInvoiceSampleDetail();

                        if (dtoDetail.ECommercialInvoiceSampleDetailDescriptions != null)
                        {
                            foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetailDescription dtoDetailExtend in dtoDetail.ECommercialInvoiceSampleDetailDescriptions)
                            {
                                _dbDetailDescription = new ECommercialInvoiceSampleDetailDescription();
                                _dbDetail.ECommercialInvoiceSampleDetailDescription.Add(_dbDetailDescription);
                                AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetailDescription, ECommercialInvoiceSampleDetailDescription>(dtoDetailExtend, _dbDetailDescription);
                            }
                        }

                        dbItem.ECommercialInvoiceSampleDetail.Add(_dbDetail);
                    }
                    else
                    {
                        _dbDetail = dbItem.ECommercialInvoiceSampleDetail.FirstOrDefault(o => o.ECommercialInvoiceSampleDetailID == dtoDetail.ECommercialInvoiceSampleDetailID);
                        if (_dbDetail != null && dtoDetail.ECommercialInvoiceSampleDetailDescriptions != null)
                        {
                            foreach (DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetailDescription dtoDetailExtend in dtoDetail.ECommercialInvoiceSampleDetailDescriptions)
                            {
                                if (dtoDetailExtend.ECommercialInvoiceSampleDetailDescriptionID < 0)
                                {
                                    _dbDetailDescription = new ECommercialInvoiceSampleDetailDescription();
                                    _dbDetail.ECommercialInvoiceSampleDetailDescription.Add(_dbDetailDescription);
                                }
                                else
                                {
                                    _dbDetailDescription = _dbDetail.ECommercialInvoiceSampleDetailDescription.FirstOrDefault(o => o.ECommercialInvoiceSampleDetailDescriptionID == dtoDetailExtend.ECommercialInvoiceSampleDetailDescriptionID);
                                }
                                if (_dbDetailDescription != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetailDescription, ECommercialInvoiceSampleDetailDescription>(dtoDetailExtend, _dbDetailDescription);
                                }
                            }
                        }
                    }

                    if (_dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetail, ECommercialInvoiceSampleDetail>(dtoDetail, _dbDetail);
                    }
                }
            }
            //End here

            //mapping all data
            AutoMapper.Mapper.Map<DTO.ECommercialInvoiceMng.ECommercialInvoice, ECommercialInvoice>(dtoItem, dbItem);
            dbItem.NetAmount = dtoItem.NetAmount;
            dbItem.VATAmount = dtoItem.VATAmount;
            dbItem.TotalAmount = dtoItem.TotalAmount;

            if (dtoItem.ECommercialInvoiceID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
            dbItem.InvoiceDate = dtoItem.InvoiceDateFormated.ConvertStringToDateTime();
        }

        public List<DTO.ECommercialInvoiceMng.PurchasingInvoiceDetail> DB2DTO_PurchasingInvoiceDetails(List<ECommercialInvoiceMng_PurchasingInvoiceDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_PurchasingInvoiceDetail_View>, List<DTO.ECommercialInvoiceMng.PurchasingInvoiceDetail>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.InitFobInvoice> DB2DTO_InitFobInvoice(List<ECommercialInvoiceMng_InitFobInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_InitFobInvoice_View>, List<DTO.ECommercialInvoiceMng.InitFobInvoice>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.DeliveryTerm> DB2DTO_DeliveryTerm(List<ECommercialInvoiceMng_DeliveryTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_DeliveryTerm>, List<DTO.ECommercialInvoiceMng.DeliveryTerm>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.PaymentTerm> DB2DTO_PaymentTerm(List<ECommercialInvoiceMng_PaymentTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_PaymentTerm>, List<DTO.ECommercialInvoiceMng.PaymentTerm>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.TurnOverLedgerAccount> DB2DTO_TurnOverLedgerAccount(List<ECommercialInvoiceMng_TurnOverLedgerAccount_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_TurnOverLedgerAccount_View>, List<DTO.ECommercialInvoiceMng.TurnOverLedgerAccount>>(dbItems);
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout DB2DTO_Printout(ECommercialInvoice_ReportView dbItem)
        {
            DTO.ECommercialInvoiceMng.InvoiceContainerPrintout dtoItem = new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
            dtoItem.Invoices = new List<DTO.ECommercialInvoiceMng.InvoicePrintout>();
            dtoItem.InvoiceDetails = new List<DTO.ECommercialInvoiceMng.InvoiceDetailPrintout>();

            //COPY INVOICE DATA
            DTO.ECommercialInvoiceMng.InvoicePrintout dtoInvoice = AutoMapper.Mapper.Map<ECommercialInvoice_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>(dbItem);
            dtoItem.Invoices.Add(dtoInvoice);

            //COPY INVOICEDETAIL DATA
            DTO.ECommercialInvoiceMng.InvoiceDetailPrintout dtoInvoiceDetail;

            //CREATE TOP DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "TOP").OrderBy(o =>o.RowIndex))
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDescription.Description;
                dtoInvoiceDetail.TotalAmount = dbDescription.TotalAmount;

                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }

            //CREATE BL NO
            dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
            dtoInvoiceDetail.Description = "BILL OF LADING NO.: " + dtoItem.Invoices.FirstOrDefault().BLNo;
            dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

            //CREATE BLANK ROW
            dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
            dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

            //CREATE CONTAINER ROW AND ITEM
            foreach (Container_ReportView dbCont in dbItem.Container_ReportView)
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbCont.ContInfo;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

                //CREATE PRODUCT ITEM
                foreach (ECommercialInvoiceDetail_ReportView dbDetail in dbItem.ECommercialInvoiceDetail_ReportView.Where(o =>o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                {
                    //CREATE ITEM ROW
                    dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();

                    dtoInvoiceDetail.ArticleCode = dbDetail.ArticleCode;
                    dtoInvoiceDetail.Description = dbDetail.Description;
                    dtoInvoiceDetail.Quantity = dbDetail.Quantity;
                    dtoInvoiceDetail.UnitPrice = dbDetail.UnitPrice;
                    dtoInvoiceDetail.TotalAmount = dbDetail.TotalAmount;

                    dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

                    //CREATE ITEM DESCRIPTION ROW
                    foreach (ECommercialInvoiceDetailDescription_ReportView dbDetailDescription in dbDetail.ECommercialInvoiceDetailDescription_ReportView.OrderBy(o =>o.RowIndex))
                    {
                        dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                        dtoInvoiceDetail.Description = dbDetailDescription.Description;
                        dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
                    }
                }

                //CREATE SPAREPART ITEM
                foreach (ECommercialInvoiceSparepartDetail_ReportView dbDetail in dbItem.ECommercialInvoiceSparepartDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                {
                    //CREATE ITEM ROW
                    dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();

                    dtoInvoiceDetail.ArticleCode = dbDetail.ArticleCode;
                    dtoInvoiceDetail.Description = dbDetail.Description;
                    dtoInvoiceDetail.Quantity = dbDetail.Quantity;
                    dtoInvoiceDetail.UnitPrice = dbDetail.UnitPrice;
                    dtoInvoiceDetail.TotalAmount = dbDetail.TotalAmount;

                    dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

                    //CREATE ITEM DESCRIPTION ROW
                    foreach(ECommercialInvoiceSparepartDetailDescription_ReportView dbDetailDescription in dbDetail.ECommercialInvoiceSparepartDetailDescription_ReportView.OrderBy(o => o.RowIndex))
                    {
                        dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                        dtoInvoiceDetail.Description = dbDetailDescription.Description;
                        dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
                    }
                }

                //CREATE SAMPLE ITEM
                foreach (ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_ReportView dbDetail in dbItem.ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                {
                    //CREATE ITEM ROW
                    dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();

                    dtoInvoiceDetail.ArticleCode = dbDetail.ArticleCode;
                    dtoInvoiceDetail.Description = dbDetail.Description;
                    dtoInvoiceDetail.Quantity = dbDetail.Quantity;
                    dtoInvoiceDetail.UnitPrice = dbDetail.UnitPrice;
                    dtoInvoiceDetail.TotalAmount = dbDetail.TotalAmount;

                    dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

                    //CREATE ITEM DESCRIPTION ROW
                    foreach (ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_ReportView dbDetailDescription in dbDetail.ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_ReportView.OrderBy(o => o.RowIndex))
                    {
                        dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                        dtoInvoiceDetail.Description = dbDetailDescription.Description;
                        dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
                    }
                }

                ////CREATE ITEM EXTEND
                //foreach (ECommercialInvoiceExtDetail_ReportView dbExtDetail in dbItem.ECommercialInvoiceExtDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                //{
                //    //CREATE ITEM ROW
                //    dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();

                //    dtoInvoiceDetail.ArticleCode = dbExtDetail.ArticleCode;
                //    dtoInvoiceDetail.Description = dbExtDetail.Description;
                //    dtoInvoiceDetail.Quantity = dbExtDetail.Quantity;
                //    dtoInvoiceDetail.UnitPrice = dbExtDetail.UnitPrice;
                //    dtoInvoiceDetail.TotalAmount = dbExtDetail.TotalAmount;

                //    dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
                //}

                //CREATE CONTYPE
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbCont.ContTypeInfo;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }

            //CREATE BLANK ROW
            dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
            dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

            //CREATE BOTTOM DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o =>o.RowIndex))
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDescription.Description;
                dtoInvoiceDetail.TotalAmount = dbDescription.TotalAmount;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }
            dtoItem.ReportName = dbItem.ReportName;
            return dtoItem;
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout DB2DTO_Printout_WarehouseInvoice(ECommercialInvoice_ReportView dbItem)
        {
            DTO.ECommercialInvoiceMng.InvoiceContainerPrintout dtoItem = new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
            dtoItem.Invoices = new List<DTO.ECommercialInvoiceMng.InvoicePrintout>();
            dtoItem.InvoiceDetails = new List<DTO.ECommercialInvoiceMng.InvoiceDetailPrintout>();

            //COPY INVOICE DATA
            DTO.ECommercialInvoiceMng.InvoicePrintout dtoInvoice = AutoMapper.Mapper.Map<ECommercialInvoice_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>(dbItem);
            dtoItem.Invoices.Add(dtoInvoice);

            //COPY INVOICEDETAIL DATA
            DTO.ECommercialInvoiceMng.InvoiceDetailPrintout dtoInvoiceDetail;

            //CREATE TOP DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "TOP").OrderBy(o =>o.RowIndex))
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDescription.Description;
                dtoInvoiceDetail.TotalAmount = dbDescription.TotalAmount;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }

            //CREATE ITEM
            foreach (WarehouseInvoiceProductDetail_ReportView dbDetail in dbItem.WarehouseInvoiceProductDetail_ReportView)
            {
                //CREATE ITEM ROW
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.ArticleCode = dbDetail.ArticleCode;
                dtoInvoiceDetail.Description = dbDetail.Description;
                dtoInvoiceDetail.Quantity = dbDetail.Quantity;
                dtoInvoiceDetail.UnitPrice = dbDetail.UnitPrice;
                dtoInvoiceDetail.TotalAmount = dbDetail.TotalAmount;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }

            foreach (WarehouseInvoiceSparepartDetail_ReportView dbSparepart in dbItem.WarehouseInvoiceSparepartDetail_ReportView)
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.ArticleCode = dbSparepart.ArticleCode;
                dtoInvoiceDetail.Description = dbSparepart.Description;
                dtoInvoiceDetail.Quantity = dbSparepart.Quantity;
                dtoInvoiceDetail.UnitPrice = dbSparepart.UnitPrice;
                dtoInvoiceDetail.TotalAmount = dbSparepart.TotalAmount;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }

            //CREATE BOTTOM DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o =>o.RowIndex))
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDescription.Description;
                dtoInvoiceDetail.TotalAmount = dbDescription.TotalAmount;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }
            dtoItem.ReportName = dbItem.ReportName;
            return dtoItem;
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout DB2DTO_Printout_Other(ECommercialInvoice_ReportView dbItem)
        {
            DTO.ECommercialInvoiceMng.InvoiceContainerPrintout dtoItem = new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
            dtoItem.Invoices = new List<DTO.ECommercialInvoiceMng.InvoicePrintout>();
            dtoItem.InvoiceDetails = new List<DTO.ECommercialInvoiceMng.InvoiceDetailPrintout>();

            //COPY INVOICE DATA
            DTO.ECommercialInvoiceMng.InvoicePrintout dtoInvoice = AutoMapper.Mapper.Map<ECommercialInvoice_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>(dbItem);
            dtoItem.Invoices.Add(dtoInvoice);

            //COPY INVOICEDETAIL DATA
            DTO.ECommercialInvoiceMng.InvoiceDetailPrintout dtoInvoiceDetail;

            //CREATE TOP DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "TOP").OrderBy(o => o.RowIndex))
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDescription.Description;
                dtoInvoiceDetail.TotalAmount = dbDescription.TotalAmount;

                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }

            //CREATE ITEM EXTEND
            foreach (ECommercialInvoiceExtDetail_ReportView dbExtDetail in dbItem.ECommercialInvoiceExtDetail_ReportView)
            {
                //CREATE ITEM ROW
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();

                dtoInvoiceDetail.ArticleCode = dbExtDetail.ArticleCode;
                dtoInvoiceDetail.Description = dbExtDetail.Description;
                dtoInvoiceDetail.Quantity = dbExtDetail.Quantity;
                dtoInvoiceDetail.UnitPrice = dbExtDetail.UnitPrice;
                dtoInvoiceDetail.TotalAmount = dbExtDetail.TotalAmount;

                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }

            //CREATE BOTTOM DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o => o.RowIndex))
            {
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDescription.Description;
                dtoInvoiceDetail.TotalAmount = dbDescription.TotalAmount;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }
            dtoItem.ReportName = dbItem.ReportName;
            return dtoItem;
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout DB2DTO_Printout_TransportContainer(ECommercialInvoice_ReportView dbItem)
        {
            DTO.ECommercialInvoiceMng.InvoiceContainerPrintout dtoItem = new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
            dtoItem.Invoices = new List<DTO.ECommercialInvoiceMng.InvoicePrintout>();
            dtoItem.InvoiceDetails = new List<DTO.ECommercialInvoiceMng.InvoiceDetailPrintout>();

            //COPY INVOICE DATA
            DTO.ECommercialInvoiceMng.InvoicePrintout dtoInvoice = AutoMapper.Mapper.Map<ECommercialInvoice_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>(dbItem);
            dtoInvoice.Reference = dbItem.TransportBLNo;
            dtoItem.Invoices.Add(dtoInvoice);

            //COPY INVOICEDETAIL DATA
            DTO.ECommercialInvoiceMng.InvoiceDetailPrintout dtoInvoiceDetail;
            var dbTransportDetail = dbItem.ECommercialInvoiceContainerTransport_ReportView.ToList();
            var clientCostItem = from item in dbTransportDetail group item by new { item.ClientCostItemID, item.ClientCostItemNM, item.ClientCostTypeID ,item.ClientCostTypeNM} into g select new { g.Key.ClientCostItemID, g.Key.ClientCostItemNM, g.Key.ClientCostTypeID, g.Key.ClientCostTypeNM };

            foreach (var costItem in clientCostItem)
            {
                //make cost item row
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
                if (costItem.ClientCostTypeID == 1)
                {
                    dtoInvoiceDetail.Description = costItem.ClientCostTypeNM.ToUpper() + ":";
                }
                else {
                    dtoInvoiceDetail.Description = costItem.ClientCostItemNM.ToUpper() + ":";
                }

                //make container row
                var contsPerCostItem = dbTransportDetail.Where(o => o.ClientCostItemID == costItem.ClientCostItemID).ToList();
                var transportPrice = from item in contsPerCostItem group item by new { item.TotalAmount } into g select new { g.Key.TotalAmount, CountPriceItem = g.Count(), SumTotalAmount = g.Sum(o => o.TotalAmount) };
                foreach (var item in transportPrice)
                {
                    dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                    dtoInvoiceDetail.Quantity = item.CountPriceItem;
                    dtoInvoiceDetail.UnitPrice = item.TotalAmount;
                    dtoInvoiceDetail.TotalAmount = item.SumTotalAmount;

                    //get conts is same this price ( in this case price is same TotalAmount)
                    string sCont = "";
                    int cont20HC = 0;
                    int cont40DC = 0;
                    int cont40HC = 0;
                    string sContType = "";
                    foreach (var cItem in contsPerCostItem.Where(o => o.TotalAmount == item.TotalAmount))
                    {
                        sCont += cItem.ContainerNo + "+";
                        if (cItem.ContainerTypeID == 1)
                        {
                            cont20HC += 1;
                        }
                        else if (cItem.ContainerTypeID == 2)
                        {
                            cont40DC += 1;
                        }
                        else if (cItem.ContainerTypeID == 3)
                        {
                            cont40HC += 1;
                        }
                    }
                    if (sCont.Length > 1) sCont = sCont.Substring(0, sCont.Length - 1);
                    sContType += "(";
                    sContType += cont20HC > 0 ? cont20HC.ToString() + "x20'HC" : "";
                    sContType += cont40DC > 0 ? cont40DC.ToString() + "x40'DC" : "";
                    sContType += cont40HC > 0 ? cont40HC.ToString() + "x40'HC" : "";
                    sContType += ")";

                    dtoInvoiceDetail.Description = sCont + "\n" + sContType;
                    dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

                    //make pol row in case seafreight
                    if (costItem.ClientCostTypeID == 1)//sea freight
                    {
                        dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                        dtoInvoiceDetail.Description = "ORIGIN: " + dbItem.TransportPOLName;
                        dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
                    }

                }           
            }

            

            //set report name
            dtoItem.ReportName = dbItem.ReportName;

            //return data
            return dtoItem;
        }

        public DTO.ECommercialInvoiceMng.PackingListContainerPrintout DB2DTO_PackingListPrintout(ECommercialInvoice_ReportView dbItem)
        {
            DTO.ECommercialInvoiceMng.PackingListContainerPrintout dtoItem = new DTO.ECommercialInvoiceMng.PackingListContainerPrintout();
            dtoItem.PackingListPrintouts = new List<DTO.ECommercialInvoiceMng.PackingListPrintout>();
            dtoItem.PackingListDetailPrintouts = new List<DTO.ECommercialInvoiceMng.PackingListDetailPrintout>();

            DTO.ECommercialInvoiceMng.PackingListPrintout dtoPackingList = new DTO.ECommercialInvoiceMng.PackingListPrintout();
            dtoPackingList.ClientNM = dbItem.ClientNM;
            dtoPackingList.Address = dbItem.Address;
            dtoPackingList.InvoiceDate = dbItem.InvoiceDate;
            dtoPackingList.OrderNo = dbItem.OrderNo;
            dtoPackingList.BLNo = dbItem.BLNo;
            dtoPackingList.Reference = dbItem.Reference;
            dtoPackingList.DeliveryTermNM = dbItem.DeliveryTermNM;

            dtoPackingList.TotalCBMs = dbItem.PackingListDetail_ReportView.Sum(o => o.CBMs) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.CBMs) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.CBMs) + dbItem.ECommercialInvoiceMng_PackingListSampleDetail_ReportView.Sum(o=>o.CBMs);
            dtoPackingList.TotalKGs = dbItem.PackingListDetail_ReportView.Sum(o => o.KGs) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.KGs) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.KGs) + dbItem.ECommercialInvoiceMng_PackingListSampleDetail_ReportView.Sum(o=>o.KGs);
            dtoPackingList.TotalNettWeight = dbItem.PackingListDetail_ReportView.Sum(o => o.NettWeight) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.NettWeight) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.NettWeight) + dbItem.ECommercialInvoiceMng_PackingListSampleDetail_ReportView.Sum(o => o.NettWeight);
            dtoPackingList.TotalPKGs = dbItem.PackingListDetail_ReportView.Sum(o => o.PKGs) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.PKGs) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.PKGs) + dbItem.ECommercialInvoiceMng_PackingListSampleDetail_ReportView.Sum(o => o.PKGs);
            dtoPackingList.TotalQuantityShipped = dbItem.PackingListDetail_ReportView.Sum(o => o.QuantityShipped) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.QuantityShipped) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.QuantityShipped) + dbItem.ECommercialInvoiceMng_PackingListSampleDetail_ReportView.Sum(o => o.QuantityShipped);

            dtoItem.PackingListPrintouts.Add(dtoPackingList);

            //PACKING LIST DETAIL
            DTO.ECommercialInvoiceMng.PackingListDetailPrintout dtoPackingListDetail;

            //CREATE TOP DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "TOP").OrderBy(o => o.RowIndex))
            {
                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                dtoPackingListDetail.Description = dbDescription.Description;
                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
            }

            //CREATE BL ROW
            dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
            dtoPackingListDetail.Description = "BILL OF LADING NO.:" + dtoPackingList.BLNo;
            dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

            //CREATE BLANK ROW
            dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
            dtoPackingListDetail.Description = " ";
            dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

            foreach (Container_ReportView dbCont in dbItem.Container_ReportView)
            {
                //CREATE CONTAINER ROW
                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                dtoPackingListDetail.Description = dbCont.ContInfo;
                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

                foreach (PackingListDetail_ReportView dbPackingListDetail in dbItem.PackingListDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                {
                    //CREATE PRODUCT ROW
                    dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                    dtoPackingListDetail.ArticleCode = dbPackingListDetail.ArticleCode;
                    dtoPackingListDetail.Description = dbPackingListDetail.Description;
                    dtoPackingListDetail.Unit = dbPackingListDetail.Unit;
                    dtoPackingListDetail.QuantityShipped = dbPackingListDetail.QuantityShipped;
                    dtoPackingListDetail.PKGs = dbPackingListDetail.PKGs;
                    dtoPackingListDetail.NettWeight = dbPackingListDetail.NettWeight;
                    dtoPackingListDetail.KGs = dbPackingListDetail.KGs;
                    dtoPackingListDetail.CBMs = dbPackingListDetail.CBMs;
                    dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

                    //CREATE ITEM DESCRIPTION ROW
                    foreach (ECommercialInvoiceDetailDescription_ReportView dbDetailDescription in dbPackingListDetail.ECommercialInvoiceDetailDescription_ReportView.OrderBy(o =>o.RowIndex))
                    {
                        dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                        dtoPackingListDetail.Description = dbDetailDescription.Description;
                        dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                    }
                }

                foreach (PackingListSparepartDetail_ReportView dbSparepart in dbItem.PackingListSparepartDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                {
                    //CREATE SPAREPART ROW
                    dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                    dtoPackingListDetail.ArticleCode = dbSparepart.ArticleCode;
                    dtoPackingListDetail.Description = dbSparepart.Description;
                    dtoPackingListDetail.Unit = dbSparepart.Unit;
                    dtoPackingListDetail.QuantityShipped = dbSparepart.QuantityShipped;
                    dtoPackingListDetail.PKGs = dbSparepart.PKGs;
                    dtoPackingListDetail.NettWeight = dbSparepart.NettWeight;
                    dtoPackingListDetail.KGs = dbSparepart.KGs;
                    dtoPackingListDetail.CBMs = dbSparepart.CBMs;
                    dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                }

                foreach (ECommercialInvoiceMng_PackingListSampleDetail_ReportView dbSample in dbItem.ECommercialInvoiceMng_PackingListSampleDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                {
                    //CREATE SPAREPART ROW
                    dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                    dtoPackingListDetail.ArticleCode = dbSample.ArticleCode;
                    dtoPackingListDetail.Description = dbSample.Description;
                    dtoPackingListDetail.Unit = dbSample.Unit;
                    dtoPackingListDetail.QuantityShipped = dbSample.QuantityShipped;
                    dtoPackingListDetail.PKGs = dbSample.PKGs;
                    dtoPackingListDetail.NettWeight = dbSample.NettWeight;
                    dtoPackingListDetail.KGs = dbSample.KGs;
                    dtoPackingListDetail.CBMs = dbSample.CBMs;
                    dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                }

                foreach (PackingListDetailExtend_ReportView dbPackingListDetailExt in dbItem.PackingListDetailExtend_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                {
                    //CREATE ITEM ROW
                    dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                    dtoPackingListDetail.ArticleCode = dbPackingListDetailExt.ArticleCode;
                    dtoPackingListDetail.Description = dbPackingListDetailExt.Description;
                    dtoPackingListDetail.Unit = dbPackingListDetailExt.Unit;
                    dtoPackingListDetail.QuantityShipped = dbPackingListDetailExt.QuantityShipped;
                    dtoPackingListDetail.PKGs = dbPackingListDetailExt.PKGs;
                    dtoPackingListDetail.NettWeight = dbPackingListDetailExt.NettWeight;
                    dtoPackingListDetail.KGs = dbPackingListDetailExt.KGs;
                    dtoPackingListDetail.CBMs = dbPackingListDetailExt.CBMs;
                    dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                }

                //CREATE CONTYPE
                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                dtoPackingListDetail.Description = dbCont.ContTypeInfo;
                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
            }

            //CREATE BLANK ROW
            dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
            dtoPackingListDetail.Description = " ";
            dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

            //CREATE BOTTOM DESCRIPTION
            foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o => o.RowIndex))
            {
                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                dtoPackingListDetail.Description = dbDescription.Description;
                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
            }
            return dtoItem;
        }

        public List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout> DB2DTO_PackingListPrintout_PerCont(ECommercialInvoice_ReportView dbItem, List<string> ContNos, List<DTO.ECommercialInvoiceMng.OrderNoDTO> OrderNoDTOs)
        {
            List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout> ListDtoItem = new List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout>();
            foreach (string ContNo in ContNos){
                DTO.ECommercialInvoiceMng.PackingListContainerPrintout dtoItem = new DTO.ECommercialInvoiceMng.PackingListContainerPrintout();
                dtoItem.PackingListPrintouts = new List<DTO.ECommercialInvoiceMng.PackingListPrintout>();
                dtoItem.PackingListDetailPrintouts = new List<DTO.ECommercialInvoiceMng.PackingListDetailPrintout>();

                DTO.ECommercialInvoiceMng.PackingListPrintout dtoPackingList = new DTO.ECommercialInvoiceMng.PackingListPrintout();
                dtoPackingList.ClientNM = dbItem.ClientNM;
                dtoPackingList.Address = dbItem.Address;
                dtoPackingList.InvoiceDate = dbItem.InvoiceDate;

                foreach(var item in OrderNoDTOs) {
                    if (item.ContainerNo == ContNo)
                    {
                        if(dtoPackingList.OrderNo != null && dtoPackingList.OrderNo != "")
                        {
                            dtoPackingList.OrderNo = dtoPackingList.OrderNo + "," + item.ProformaInvoiceNo;
                        }
                        else
                        {
                            dtoPackingList.OrderNo = item.ProformaInvoiceNo;
                        }
                    }
                }


                dtoPackingList.BLNo = dbItem.BLNo;
                dtoPackingList.Reference = dbItem.Reference;
                dtoPackingList.DeliveryTermNM = dbItem.DeliveryTermNM;

                dtoPackingList.TotalCBMs = dbItem.PackingListDetail_ReportView.Sum(o => o.CBMs) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.CBMs) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.CBMs);
                dtoPackingList.TotalKGs = dbItem.PackingListDetail_ReportView.Sum(o => o.KGs) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.KGs) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.KGs);
                dtoPackingList.TotalNettWeight = dbItem.PackingListDetail_ReportView.Sum(o => o.NettWeight) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.NettWeight) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.NettWeight);
                dtoPackingList.TotalPKGs = dbItem.PackingListDetail_ReportView.Sum(o => o.PKGs) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.PKGs) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.PKGs);
                dtoPackingList.TotalQuantityShipped = dbItem.PackingListDetail_ReportView.Sum(o => o.QuantityShipped) + dbItem.PackingListDetailExtend_ReportView.Sum(o => o.QuantityShipped) + dbItem.PackingListSparepartDetail_ReportView.Sum(o => o.QuantityShipped);

                dtoItem.PackingListPrintouts.Add(dtoPackingList);
                
                //PACKING LIST DETAIL
                DTO.ECommercialInvoiceMng.PackingListDetailPrintout dtoPackingListDetail;

                //CREATE TOP DESCRIPTION
                foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "TOP").OrderBy(o => o.RowIndex))
                {
                    dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                    dtoPackingListDetail.Description = dbDescription.Description;
                    dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                }

                //CREATE BL ROW
                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                dtoPackingListDetail.Description = "BILL OF LADING NO.:" + dtoPackingList.BLNo;
                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

                //CREATE BLANK ROW
                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                dtoPackingListDetail.Description = " ";
                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                
                foreach (Container_ReportView dbCont in dbItem.Container_ReportView)
                {
                    if (ContNo == dbCont.ContainerNo)
                    {
                        //CREATE CONTAINER ROW
                        dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                        dtoPackingListDetail.Description = dbCont.ContInfo;
                        dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

                        foreach (PackingListDetail_ReportView dbPackingListDetail in dbItem.PackingListDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                        {
                            //CREATE PRODUCT ROW
                            dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                            dtoPackingListDetail.ArticleCode = dbPackingListDetail.ArticleCode;
                            dtoPackingListDetail.Description = dbPackingListDetail.Description;
                            dtoPackingListDetail.Unit = dbPackingListDetail.Unit;
                            dtoPackingListDetail.QuantityShipped = dbPackingListDetail.QuantityShipped;
                            dtoPackingListDetail.PKGs = dbPackingListDetail.PKGs;
                            dtoPackingListDetail.NettWeight = dbPackingListDetail.NettWeight;
                            dtoPackingListDetail.KGs = dbPackingListDetail.KGs;
                            dtoPackingListDetail.CBMs = dbPackingListDetail.CBMs;
                            dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

                            //CREATE ITEM DESCRIPTION ROW
                            foreach (ECommercialInvoiceDetailDescription_ReportView dbDetailDescription in dbPackingListDetail.ECommercialInvoiceDetailDescription_ReportView.OrderBy(o => o.RowIndex))
                            {
                                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                                dtoPackingListDetail.Description = dbDetailDescription.Description;
                                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                            }
                        }

                        foreach (PackingListSparepartDetail_ReportView dbSparepart in dbItem.PackingListSparepartDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                        {
                            //CREATE SPAREPART ROW
                            dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                            dtoPackingListDetail.ArticleCode = dbSparepart.ArticleCode;
                            dtoPackingListDetail.Description = dbSparepart.Description;
                            dtoPackingListDetail.Unit = dbSparepart.Unit;
                            dtoPackingListDetail.QuantityShipped = dbSparepart.QuantityShipped;
                            dtoPackingListDetail.PKGs = dbSparepart.PKGs;
                            dtoPackingListDetail.NettWeight = dbSparepart.NettWeight;
                            dtoPackingListDetail.KGs = dbSparepart.KGs;
                            dtoPackingListDetail.CBMs = dbSparepart.CBMs;
                            dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                        }

                        foreach (ECommercialInvoiceMng_PackingListSampleDetail_ReportView dbSample in dbItem.ECommercialInvoiceMng_PackingListSampleDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                        {
                            //CREATE SPAREPART ROW
                            dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                            dtoPackingListDetail.ArticleCode = dbSample.ArticleCode;
                            dtoPackingListDetail.Description = dbSample.Description;
                            dtoPackingListDetail.Unit = dbSample.Unit;
                            dtoPackingListDetail.QuantityShipped = dbSample.QuantityShipped;
                            dtoPackingListDetail.PKGs = dbSample.PKGs;
                            dtoPackingListDetail.NettWeight = dbSample.NettWeight;
                            dtoPackingListDetail.KGs = dbSample.KGs;
                            dtoPackingListDetail.CBMs = dbSample.CBMs;
                            dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                        }

                        foreach (PackingListDetailExtend_ReportView dbPackingListDetailExt in dbItem.PackingListDetailExtend_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                        {
                            //CREATE ITEM ROW
                            dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                            dtoPackingListDetail.ArticleCode = dbPackingListDetailExt.ArticleCode;
                            dtoPackingListDetail.Description = dbPackingListDetailExt.Description;
                            dtoPackingListDetail.Unit = dbPackingListDetailExt.Unit;
                            dtoPackingListDetail.QuantityShipped = dbPackingListDetailExt.QuantityShipped;
                            dtoPackingListDetail.PKGs = dbPackingListDetailExt.PKGs;
                            dtoPackingListDetail.NettWeight = dbPackingListDetailExt.NettWeight;
                            dtoPackingListDetail.KGs = dbPackingListDetailExt.KGs;
                            dtoPackingListDetail.CBMs = dbPackingListDetailExt.CBMs;
                            dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                        }

                        //CREATE CONTYPE
                        dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                        dtoPackingListDetail.Description = dbCont.ContTypeInfo;
                        dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                    }
                }
                
                //CREATE BLANK ROW
                dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                dtoPackingListDetail.Description = " ";
                dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);

                //CREATE BOTTOM DESCRIPTION
                foreach (ECommercialInvoiceDescription_ReportView dbDescription in dbItem.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o => o.RowIndex))
                {
                    dtoPackingListDetail = new DTO.ECommercialInvoiceMng.PackingListDetailPrintout();
                    dtoPackingListDetail.Description = dbDescription.Description;
                    dtoItem.PackingListDetailPrintouts.Add(dtoPackingListDetail);
                }

                ListDtoItem.Add(dtoItem);
            }
            return ListDtoItem;
        }

        public List<DTO.ECommercialInvoiceMng.InitWarehouseInfo> DB2DTO_InitWarehouseInfos(List<ECommercialInvoiceMng_InitWarehouseInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_InitWarehouseInfo_View>, List<DTO.ECommercialInvoiceMng.InitWarehouseInfo>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.WarehousePickingList> DB2DTO_WarehousePickingList(List<ECommercialInvoiceMng_WarehousePickingList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_WarehousePickingList_View>, List<DTO.ECommercialInvoiceMng.WarehousePickingList>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.PurchasingInvoice> DB2DTO_PurchasingInvoice(List<ECommercialInvoiceMng_PurchasingInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_PurchasingInvoice_View>, List<DTO.ECommercialInvoiceMng.PurchasingInvoice>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.Client> DB2DTO_Client(List<ECommercialInvoiceMng_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_Client_View>, List<DTO.ECommercialInvoiceMng.Client>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.ReturnProduct> DB2DTO_ReturnProduct(List<ECommercialInvoiceMng_ReturnProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ReturnProduct_View>, List<DTO.ECommercialInvoiceMng.ReturnProduct>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.ReturnSparepart> DB2DTO_ReturnSparepart(List<ECommercialInvoiceMng_ReturnSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ReturnSparepart_View>, List<DTO.ECommercialInvoiceMng.ReturnSparepart>>(dbItems);
        }

        public List<DTO.ECommercialInvoiceMng.ClientCostItem> DB2DTO_ClientCostItem(List<ECommercialInvoiceMng_ClientCostItem_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ClientCostItem_View>, List<DTO.ECommercialInvoiceMng.ClientCostItem>>(dbItems);
        }

        public DTO.ECommercialInvoiceMng.ECommercialInvoiceOverview DB2DTO_ECommercialInvoiceOverview(ECommercialInvoiceMng_ECommercialInvoice_OverView dbItem)
        {
            DTO.ECommercialInvoiceMng.ECommercialInvoiceOverview dtoItem = AutoMapper.Mapper.Map<ECommercialInvoiceMng_ECommercialInvoice_OverView, DTO.ECommercialInvoiceMng.ECommercialInvoiceOverview>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.InvoiceDate.HasValue)
                dtoItem.InvoiceDateFormated = dbItem.InvoiceDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PrintedDate.HasValue)
                dtoItem.PrintedDateFormated = dbItem.PrintedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.ConfirmedDate.HasValue)
                dtoItem.ConfirmedDateFormated = dbItem.ConfirmedDate.Value.ToString("dd/MM/yyyy");


            return dtoItem;
        }
        public List<DTO.ECommercialInvoiceMng.WarehouseInvoice> DB2DTO_WarehouseInvoice(List<ECommercialInvoiceMng_WarehouseInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_WarehouseInvoice_View>, List<DTO.ECommercialInvoiceMng.WarehouseInvoice>>(dbItems);
        }
    }
}
