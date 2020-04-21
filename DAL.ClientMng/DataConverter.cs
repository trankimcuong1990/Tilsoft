using AutoMapper;
using DALBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DAL.ClientMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientMng_ClientSearchResult_View, DTO.ClientMng.ClientSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientContractDetail_View, DTO.ClientMng.ClientContractDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientContract_View, DTO.ClientMng.ClientContract>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientContractDetails, o => o.MapFrom(s => s.ClientMng_ClientContractDetail_View))
                    .ForMember(d => d.ContractFileURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContractFileURL) ? s.ContractFileURL : "")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientContact_View, DTO.ClientMng.ClientContact>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientContactHistory_View, DTO.ClientMng.ClientContactHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ContactDate, o => o.ResolveUsing(s => s.ContactDate.HasValue ? s.ContactDate.Value.ToString("dd/MM/yyyy HH:mm ") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ClientContactHistory_View, DTO.ClientMng.ClientContactHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ContactDate, o => o.ResolveUsing(s => s.ContactDate.HasValue ? s.ContactDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientSpecialPackagingMethod_View, DTO.ClientMng.ClientSpecialPackagingMethod>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_DDCDetail_View, DTO.ClientMng.DDCDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_SaleOrder_View, DTO.ClientMng.SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Appointment_View, DTO.ClientMng.AppointmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ClientDocumentType_View, DTO.ClientMng.ClientDocumentTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<ClientMng_Offer_OverView, DTO.ClientMng.Overview.Offer.OfferOverview>()
                //    .IgnoreAllNonExisting()
                //    .ForMember(d => d.OfferLines, o => o.MapFrom(s => s.ClientMng_OfferLine_OverView))
                //    .ForMember(d => d.OfferLineSpareparts, o => o.MapFrom(s => s.ClientMng_OfferLineSparepart_OverView))
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_OfferMargin_View, DTO.ClientMng.Overview.Offer.OfferMarginDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_OfferMarginDetail_View, DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ProductThumbnailLocation) ? s.ProductThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductFileLocation) ? s.ProductFileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<ClientMng_OfferLine_OverView, DTO.ClientMng.Overview.Offer.OfferlineOverview>()
                //    .IgnoreAllNonExisting()
                //    .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ProductThumbnailLocation) ? s.ProductThumbnailLocation : "no-image.jpg")))
                //    .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductFileLocation) ? s.ProductFileLocation : "no-image.jpg")))
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<ClientMng_OfferLineSparepart_OverView, DTO.ClientMng.Overview.Offer.OfferLineSparepartOverview>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Client_View, DTO.ClientMng.Client>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientContracts, o => o.MapFrom(s => s.ClientMng_ClientContract_View))
                    .ForMember(d => d.ClientContacts, o => o.MapFrom(s => s.ClientMng_ClientContact_View))
                    .ForMember(d => d.ClientContactHistories, o => o.MapFrom(s => s.ClientMng_ClientContactHistory_View))
                    .ForMember(d => d.ClientSpecialPackagingMethods, o => o.MapFrom(s => s.ClientMng_ClientSpecialPackagingMethod_View))
                    .ForMember(d => d.DDCDetails, o => o.MapFrom(s => s.ClientMng_DDCDetail_View.OrderByDescending(x => x.Season)))
                    .ForMember(d => d.SaleOrders, o => o.MapFrom(s => s.ClientMng_SaleOrder_View))
                    .ForMember(d => d.AppointmentDTOs, o => o.MapFrom(s => s.ClientMng_Appointment_View))
                    .ForMember(d => d.PenaltyTerms, o => o.MapFrom(s => s.ClientMng_PenaltyTerm_View))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ClientEstimatedAdditionalCost, o => o.MapFrom(s => s.ClientMng_ClientEstimatedAdditionalCost_View))
                    .ForMember(d => d.ClientAdditionalConditionDTO, o => o.MapFrom(s => s.ClientMng_ClientAdditionalCondition_View))
                    .ForMember(d => d.ClientBusinessCardDTO, o => o.MapFrom(s => s.ClientMng_ClientBusinessCard_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientEstimatedAdditionalCost_View, DTO.ClientMng.ClientEstimatedAdditionalCostDTO>()
                    .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientAdditionalCondition_View, DTO.ClientMng.ClientAdditionalConditionDTO>()
                    //.ForMember(d => d.UpdatedCheckDate, o => o.ResolveUsing(s => s.UpdatedCheckDate.HasValue ? s.UpdatedCheckDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    //.ForMember(d => d.UpdatedUnCheckDate, o => o.ResolveUsing(s => s.UpdatedUnCheckDate.HasValue ? s.UpdatedUnCheckDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.AttachFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.AttachFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.AttachFileURL)))
                    .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_AccountManager_View, DTO.ClientMng.AccountManagerDTO>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Client overview view
                AutoMapper.Mapper.CreateMap<ClientMng_Overview_Client_View, DTO.ClientMng.ClientOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ClientContacts, o => o.MapFrom(s => s.ClientMng_Overview_ClientContact_View))
                    .ForMember(d => d.ClientContracts, o => o.MapFrom(s => s.ClientMng_Overview_ClientContract_View))
                    .ForMember(d => d.Appointments, o => o.MapFrom(s => s.ClientMng_Overview_Appointment_View))
                    .ForMember(d => d.ClientDDCs, o => o.MapFrom(s => s.ClientMng_Overview_DDC_View))
                    .ForMember(d => d.PenaltyTerms, o => o.MapFrom(s => s.ClientMng_OverView_PenaltyTerm_View))
                    .ForMember(d => d.ClientContactHistories, o => o.MapFrom(s => s.ClientMng_Overview_ClientContactHistory_View))
                    .ForMember(d => d.ClientBusinessCardDTO, o => o.MapFrom(s => s.ClientMng_Overview_ClientBusinessCard_View))
                    .ForMember(d => d.ClientAdditionalConditionDTO, o => o.MapFrom(s => s.ClientMng_Overview_ClientAdditionalCondition_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Client PLC view
                AutoMapper.Mapper.CreateMap<ClientMng_Overview_PLC_View, DTO.ClientMng.ClientPLC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RatedDate, o => o.ResolveUsing(s => s.RatedDate.HasValue ? s.RatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    //.ForMember(d => d.PLCLoadingPLans, o => o.MapFrom(s => s.ClientMng_Overview_PLC_LoadingPlan_View))
                    //.ForMember(d => d.PLCSaleOrders, o => o.MapFrom(s => s.ClientMng_Overview_PLC_SaleOrder_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Mapping child list of Client Overview

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ClientAdditionalCondition_View, DTO.ClientMng.ClientAdditionalConditionDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.AttachFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.AttachFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.AttachFileURL)))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ClientBusinessCard_View, DTO.ClientMng.ClientBusinessCardDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ClientContact_View, DTO.ClientMng.ClientContact>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ClientContract_View, DTO.ClientMng.ClientContract>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ContractFileURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContractFile) ? s.ContractFile : "")))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_Appointment_View, DTO.ClientMng.AppointmentDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy") : ""))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ClientComplaint_View, DTO.ClientMng.ClientComplaint>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.DeliveryDateClient, o => o.ResolveUsing(s => s.DeliveryDateClient.HasValue ? s.DeliveryDateClient.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.ReceivedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_DDC_View, DTO.ClientMng.ClientOverviewDDC>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ECommercialInvoice_View, DTO.ClientMng.ClientECommercialInvoice>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.PrintedDate, o => o.ResolveUsing(s => s.PrintedDate.HasValue ? s.PrintedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.PrintedDate, o => o.ResolveUsing(s => s.PrintedDate.HasValue ? s.PrintedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_Offer_View, DTO.ClientMng.ClientOffer>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.OfferDate, o => o.ResolveUsing(s => s.OfferDate.HasValue ? s.OfferDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_SampleOrder_View, DTO.ClientMng.ClientSampleOrder>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.HardDeadline, o => o.ResolveUsing(s => s.HardDeadline.HasValue ? s.HardDeadline.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd-MMM-yyyy", new CultureInfo("en-US")) : ""))
                 .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_SaleOrder_View, DTO.ClientMng.ClientSaleOrder>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.PIReceivedDate, o => o.ResolveUsing(s => s.PIReceivedDate.HasValue ? s.PIReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.DeliveryDate, o => o.ResolveUsing(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.DeliveryDateInternal, o => o.ResolveUsing(s => s.DeliveryDateInternal.HasValue ? s.DeliveryDateInternal.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.SignedPIFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SignedPIFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.SignedPIFileURL)))
                 .ForMember(d => d.ClientPOFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ClientPOFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ClientPOFileURL)))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientContactHistory_View, DTO.ClientMng.ClientContactHistory>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.ContactDate, o => o.ResolveUsing(s => s.ContactDate.HasValue ? s.ContactDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ClientContactHistory_View, DTO.ClientMng.ClientContactHistory>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.ContactDate, o => o.ResolveUsing(s => s.ContactDate.HasValue ? s.ContactDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Mapping child list of Client PLC Overview
                AutoMapper.Mapper.CreateMap<ClientMng_Overview_PLC_LoadingPlan_View, DTO.ClientMng.PLCLoadingPLan>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_PLC_SaleOrder_View, DTO.ClientMng.PLCSaleOrder>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientMng.Client, Client>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientID, o => o.Ignore())
                    .ForMember(d => d.ClientContract, o => o.Ignore())
                    .ForMember(d => d.ClientContact, o => o.Ignore())
                    .ForMember(d => d.ClientContactHistory, o => o.Ignore())
                    .ForMember(d => d.ClientSpecialPackagingMethod, o => o.Ignore())
                    .ForMember(d => d.PenaltyTerm, o => o.Ignore())
                    .ForMember(d => d.DDCDetail, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.LogoImage, o => o.Ignore())
                    .ForMember(d => d.ClientEstimatedAdditionalCost, o => o.Ignore())
                    .ForMember(d => d.ClientAdditionalCondition, o => o.Ignore())
                    .ForMember(d => d.ClientBusinessCard, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientContract, ClientContract>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ClientContractID, o => o.Ignore())
                     .ForMember(d => d.ClientContractDetail, o => o.Ignore())
                     ;
                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientContact, ClientContact>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ClientContactID, o => o.Ignore())
                     ;
                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientContactHistory, ClientContactHistory>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ContactDate, o => o.Ignore())
                     .ForMember(d => d.ClientContactHistoryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientSpecialPackagingMethod, ClientSpecialPackagingMethod>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ClientSpecialPackagingMethodID, o => o.Ignore())
                     ;
                AutoMapper.Mapper.CreateMap<DTO.ClientMng.DDCDetail, DDCDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.DDCDetailID, o => o.Ignore())
                     ;
                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientContractDetail, ClientContractDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ClientContractDetailID, o => o.Ignore())
                     ;
                AutoMapper.Mapper.CreateMap<DTO.ClientMng.PenaltyTermDTO, PenaltyTerm>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.PenaltyTermID, o => o.Ignore())
                     .ForMember(d => d.ClientID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_SaleOrderDetail_View, DTO.ClientMng.SaleOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_OfferLine_View, DTO.ClientMng.OfferLine>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ECommercialInvoiceDetail_View, DTO.ClientMng.ECommercialInvoiceDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_WarehouseInvoiceProductDetail_View, DTO.ClientMng.WarehouseInvoiceProductDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ECommercialInvoiceExtDetail_View, DTO.ClientMng.ECommercialInvoiceExtDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ChartCommercialInvoice_View, DTO.ClientMng.ChartCommercialInvoice>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ShippingPerformance_View, DTO.ClientMng.Overview.CIS.ShippingPerformanceDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ShippingPerformanceChart_View, DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ShippingPerformanceChart2_View, DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO2>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ClientMng_Overview_Item_View, DTO.ClientMng.Overview.CIS.ItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_PriceCompare_View, DTO.ClientMng.Overview.CIS.PriceComparisonDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_LoadingPlan_View, DTO.ClientMng.Overview.LoadingPlanDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.CutOffDate, o => o.ResolveUsing(s => s.CutOffDate.HasValue ? s.CutOffDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_PISearchResult_View, DTO.ClientMng.Overview.PISearchResultDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_EurofarPriceOverview_View, DTO.ClientMng.Overview.Delta.EurofarPriceOverviewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_EurofarPriceOverviewGroupByItem_View, DTO.ClientMng.Overview.Delta2.EurofarPriceOverviewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                   .ForMember(d => d.CommissionAmount, o => o.ResolveUsing(s => s.CommissionAmount.HasValue ? s.CommissionAmount.Value : 0))
                   .ForMember(d => d.DamagesCost, o => o.ResolveUsing(s => s.DamagesCost.HasValue ? s.DamagesCost.Value : 0))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_EurofarPriceOverviewGroupByModel_View, DTO.ClientMng.Overview.Delta3.EurofarPriceOverviewDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                   .ForMember(d => d.CommissionAmount, o => o.ResolveUsing(s => s.CommissionAmount.HasValue ? s.CommissionAmount.Value : 0))
                   .ForMember(d => d.DamagesCost, o => o.ResolveUsing(s => s.DamagesCost.HasValue ? s.DamagesCost.Value : 0))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Option For Breakdown Legend :( 
                AutoMapper.Mapper.CreateMap<ClientMng_BreakdownCategoryOption_View, DTO.ClientMng.Overview.Breakdown.BreakdownCategoryOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Breakdown_FactoryQuotationSearchResult_View, DTO.ClientMng.Overview.Breakdown.FactoryQuotationSeachDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForMember(d => d.PriceUpdatedDate, o => o.ResolveUsing(s => s.PriceUpdatedDate.HasValue ? s.PriceUpdatedDate.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_BreakdownCategory_view, DTO.ClientMng.Overview.Breakdown.BreakdownCategoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_BreakdownManagementFee_View, DTO.ClientMng.Overview.Breakdown.BreakdownManagementFeeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_BreakdownSeasonSpecPercent_View, DTO.ClientMng.Overview.Breakdown.BreakdownSeasonSpecPercentDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_QuotationStatus_View, DTO.ClientMng.Overview.Breakdown.QuotationStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_OrderType_View, DTO.ClientMng.Overview.Breakdown.OrderTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_SubTotal_View, DTO.ClientMng.Overview.Breakdown.SubTotalDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_Model_View, DTO.ClientMng.Overview.CIS.ModelDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientEstimatedAdditionalCostDTO, ClientEstimatedAdditionalCost>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientEstimatedAdditionalCostID, o => o.Ignore())
                    .ForMember(d => d.ClientID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientAdditionalConditionDTO, ClientAdditionalCondition>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ClientAdditionalConditionID, o => o.Ignore())                   
                   .ForMember(d => d.ClientID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ComplaintStatus_View, DTO.ClientMng.ClientComplaintStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_Overview_ComplaintType_View, DTO.ClientMng.ClientComplaintTypeDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_PenaltyTerm_View, DTO.ClientMng.PenaltyTermDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PenaltyTermDate, o => o.ResolveUsing(s => s.PenaltyTermDate.HasValue ? s.PenaltyTermDate.Value.ToString("dd/MM/yyyy") : ""));

                AutoMapper.Mapper.CreateMap<ClientMng_OverView_PenaltyTerm_View, DTO.ClientMng.PenaltyTermDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.PenaltyTermDate, o => o.ResolveUsing(s => s.PenaltyTermDate.HasValue ? s.PenaltyTermDate.Value.ToString("dd/MM/yyyy") : ""));

                AutoMapper.Mapper.CreateMap<ClientMng_function_GetOfferLinePlaningPurchasingPrice_Result, DTO.ClientMng.PlaningPurchasingPriceDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_PlaningPurchasingPriceSource_View, DTO.ClientMng.PlaningPurchasingPriceSourceDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientAdditionalConditionGetdata_View, DTO.ClientMng.ClientGetDataAdditionalConditionDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_ClientBusinessCard_View, DTO.ClientMng.ClientBusinessCardDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FrontThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FrontThumbnailLocation) ? s.FrontThumbnailLocation : "no-image.jpg")))
                  .ForMember(d => d.BehindThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.BehindThumbnailLocation) ? s.BehindThumbnailLocation : "no-image.jpg")))
                  .ForMember(d => d.FrontFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FrontFileLocation) ? s.FrontFileLocation : "no-image.jpg")))
                  .ForMember(d => d.BehindFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BehindFileLocation) ? s.BehindFileLocation : "no-image.jpg")))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_User2_View, DTO.ClientMng.UsersDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientMng.ClientBusinessCardDTO, ClientBusinessCard>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.ClientBusinessCardID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<Support_ClientAdditionalCondition_View, DTO.ClientMng.ClientAdditionalConditionTypeDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientMng_GICShowContent_View, DTO.ClientMng.ClientGICComment>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ClientMng.ClientSearchResult> DB2DTO_ClientSearch(List<ClientMng_ClientSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_ClientSearchResult_View>, List<DTO.ClientMng.ClientSearchResult>>(dbItems);
        }

        public List<DTO.ClientMng.AccountManagerDTO> DB2DTO_AccountManagerDTO(List<SupportMng_AccountManager_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_AccountManager_View>, List<DTO.ClientMng.AccountManagerDTO>>(dbItems);
        }

        public DTO.ClientMng.ClientGetDataAdditionalConditionDTO DB2DTO_GetDataAdditionalCondition(ClientMng_ClientAdditionalConditionGetdata_View dbItems)
        {
            return AutoMapper.Mapper.Map<ClientMng_ClientAdditionalConditionGetdata_View, DTO.ClientMng.ClientGetDataAdditionalConditionDTO>(dbItems);
        }

        public List<DTO.ClientMng.ClientGICComment> DB2DTO_ClientGICComment(List<ClientMng_GICShowContent_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_GICShowContent_View>, List<DTO.ClientMng.ClientGICComment>>(dbItems);
        }

        public DTO.ClientMng.Client DB2DTO_Client(ClientMng_Client_View dbItem)
        {
            DTO.ClientMng.Client dtoItem = AutoMapper.Mapper.Map<ClientMng_Client_View, DTO.ClientMng.Client>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public DTO.ClientMng.ClientOverview DB2DTO_ClientOverview(ClientMng_Overview_Client_View dbItem)
        {
            DTO.ClientMng.ClientOverview dtoItem = AutoMapper.Mapper.Map<ClientMng_Overview_Client_View, DTO.ClientMng.ClientOverview>(dbItem);
            
            return dtoItem;
        }

        public void DTO2DB_Client(DTO.ClientMng.Client dtoItem, ref Client dbItem)
        {
            /*
             * MAP & CHECK ClientContract
             */
            List<ClientContract> contract_tobe_deleted = new List<ClientContract>();
            List<ClientContractDetail> contractDetail_tobe_deleted;
            if (dtoItem.ClientContracts != null)
            {
                //CHECK
                foreach (var dbContract in dbItem.ClientContract)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.ClientContracts.Select(s => s.ClientContractID).Contains(dbContract.ClientContractID))
                    {
                        contract_tobe_deleted.Add(dbContract);
                    }
                    else //DB IS EXIST IN DB
                    {
                        contractDetail_tobe_deleted = new List<ClientContractDetail>();
                        foreach (var dbContractDetail in dbContract.ClientContractDetail)
                        {
                            if (!dtoItem.ClientContracts.Where(o => o.ClientContractID == dbContract.ClientContractID).FirstOrDefault().ClientContractDetails.Select(x => x.ClientContractDetailID).Contains(dbContractDetail.ClientContractDetailID))
                            {
                                contractDetail_tobe_deleted.Add(dbContractDetail);
                            }
                        }
                        foreach (var dbDetailDescription in contractDetail_tobe_deleted)
                        {
                            dbContract.ClientContractDetail.Remove(dbDetailDescription);
                        }
                    }
                }

                foreach (var dbContract in contract_tobe_deleted)
                {
                    contractDetail_tobe_deleted = new List<ClientContractDetail>();
                    foreach (var dbContractDetail in dbContract.ClientContractDetail)
                    {
                        contractDetail_tobe_deleted.Add(dbContractDetail);
                    }
                    foreach (var item in contractDetail_tobe_deleted)
                    {
                        dbItem.ClientContract.Where(o => o.ClientContractID == dbContract.ClientContractID).FirstOrDefault().ClientContractDetail.Remove(item);
                    }
                    dbItem.ClientContract.Remove(dbContract);
                }

                //MAP
                ClientContract _dbContract;
                ClientContractDetail _dbContractDetail;
                foreach (var dtoContract in dtoItem.ClientContracts)
                {
                    if (dtoContract.ClientContractID < 0)
                    {
                        _dbContract = new ClientContract();
                        if (dtoContract.ClientContractDetails != null)
                        {
                            foreach (var dtoContractDetail in dtoContract.ClientContractDetails)
                            {
                                _dbContractDetail = new ClientContractDetail();
                                _dbContract.ClientContractDetail.Add(_dbContractDetail);
                                AutoMapper.Mapper.Map<DTO.ClientMng.ClientContractDetail, ClientContractDetail>(dtoContractDetail, _dbContractDetail);
                            }
                        }
                        dbItem.ClientContract.Add(_dbContract);
                    }
                    else
                    {
                        _dbContract = dbItem.ClientContract.FirstOrDefault(o => o.ClientContractID == dtoContract.ClientContractID);
                        if (_dbContract != null && dtoContract.ClientContractDetails != null)
                        {
                            foreach (var dtoContractDetail in dtoContract.ClientContractDetails)
                            {
                                if (dtoContractDetail.ClientContractDetailID < 0)
                                {
                                    _dbContractDetail = new ClientContractDetail();
                                    _dbContract.ClientContractDetail.Add(_dbContractDetail);
                                }
                                else
                                {
                                    _dbContractDetail = _dbContract.ClientContractDetail.FirstOrDefault(o => o.ClientContractDetailID == dtoContractDetail.ClientContractDetailID);
                                }
                                if (_dbContractDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ClientMng.ClientContractDetail, ClientContractDetail>(dtoContractDetail, _dbContractDetail);
                                }
                            }
                        }
                    }
                    if (_dbContract != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientMng.ClientContract, ClientContract>(dtoContract, _dbContract);
                    }
                }
            }

            /*
            * MAP & CHECK ClientContact
            */
            List<ClientContact> clientContact_need_delete = new List<ClientContact>();
            if (dtoItem.ClientContacts != null)
            {
                //CHECK
                foreach (var item in dbItem.ClientContact.Where(o => !dtoItem.ClientContacts.Select(s => s.ClientContactID).Contains(o.ClientContactID)))
                {
                    clientContact_need_delete.Add(item);
                }
                foreach (var item in clientContact_need_delete)
                {
                    dbItem.ClientContact.Remove(item);
                }
                //MAP
                foreach (DTO.ClientMng.ClientContact dtoDetail in dtoItem.ClientContacts)
                {
                    ClientContact dbDetail;
                    if (dtoDetail.ClientContactID < 0)
                    {
                        dbDetail = new ClientContact();
                        dbItem.ClientContact.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.ClientContact.FirstOrDefault(o => o.ClientContactID == dtoDetail.ClientContactID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientMng.ClientContact, ClientContact>(dtoDetail, dbDetail);
                    }
                }
            }

            /*
           * MAP & CHECK ClientSpecialPackagingMethod
           */
            List<ClientSpecialPackagingMethod> clientSpecialPackagingMethod_need_delete = new List<ClientSpecialPackagingMethod>();
            if (dtoItem.ClientContacts != null)
            {
                //CHECK
                foreach (var item in dbItem.ClientSpecialPackagingMethod.Where(o => !dtoItem.ClientSpecialPackagingMethods.Select(s => s.ClientSpecialPackagingMethodID).Contains(o.ClientSpecialPackagingMethodID)))
                {
                    clientSpecialPackagingMethod_need_delete.Add(item);
                }
                foreach (var item in clientSpecialPackagingMethod_need_delete)
                {
                    dbItem.ClientSpecialPackagingMethod.Remove(item);
                }
                if (dtoItem.ClientSpecialPackagingMethods != null)
                {
                    //MAP
                    foreach (DTO.ClientMng.ClientSpecialPackagingMethod dtoDetail in dtoItem.ClientSpecialPackagingMethods)
                    {
                        ClientSpecialPackagingMethod dbDetail;
                        if (dtoDetail.ClientSpecialPackagingMethodID < 0)
                        {
                            dbDetail = new ClientSpecialPackagingMethod();
                            dbItem.ClientSpecialPackagingMethod.Add(dbDetail);
                        }
                        else
                        {
                            dbDetail = dbItem.ClientSpecialPackagingMethod.FirstOrDefault(o => o.ClientSpecialPackagingMethodID == dtoDetail.ClientSpecialPackagingMethodID);
                        }

                        if (dbDetail != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ClientMng.ClientSpecialPackagingMethod, ClientSpecialPackagingMethod>(dtoDetail, dbDetail);
                        }
                    }
                }
            }

            /*
            * MAP & CHECK ClientContactHistory
            */
            //List<ClientContactHistory> ClientContactHistoryHistory_need_delete = new List<ClientContactHistory>();
            if (dtoItem.ClientContactHistories != null)
            {
                var dtoContactHistory = dtoItem.ClientContactHistories.FirstOrDefault(o => o.HasChanged);
                if (dtoContactHistory != null)
                {
                    dbItem.ClientContactHistory.Add(
                        new ClientContactHistory()
                        {
                            ClientID = dtoContactHistory.ClientID,
                            CommunicationContent = dtoContactHistory.CommunicationContent,
                            ContactDate = DateTime.Now
                        }
                    );
                }
                ////CHECK
                //foreach (var item in dbItem.ClientContactHistory.Where(o => !dtoItem.ClientContactHistories.Select(s => s.ClientContactHistoryID).Contains(o.ClientContactHistoryID)))
                //{
                //    ClientContactHistoryHistory_need_delete.Add(item);
                //}
                //foreach (var item in ClientContactHistoryHistory_need_delete)
                //{
                //    dbItem.ClientContactHistory.Remove(item);
                //}
                ////MAP
                //foreach (DTO.ClientMng.ClientContactHistory dtoDetail in dtoItem.ClientContactHistories)
                //{
                //    ClientContactHistory dbDetail;
                //    if (dtoDetail.ClientContactHistoryID < 0)
                //    {
                //        dbDetail = new ClientContactHistory();
                //        dbItem.ClientContactHistory.Add(dbDetail);                        
                //    }
                //    else
                //    {
                //        dbDetail = dbItem.ClientContactHistory.FirstOrDefault(o => o.ClientContactHistoryID == dtoDetail.ClientContactHistoryID);                        
                //    }

                //    if (dbDetail != null)
                //    {
                //        dbDetail.ContactDate = dtoDetail.ContactDate.ConvertStringToDateTime();
                //        AutoMapper.Mapper.Map<DTO.ClientMng.ClientContactHistory, ClientContactHistory>(dtoDetail, dbDetail);
                //    }
                //}
            }

            //MAP DDCDetail
            foreach (DTO.ClientMng.DDCDetail item in dtoItem.DDCDetails)
            {
                DDCDetail dbDDCDetail;
                dbDDCDetail = dbItem.DDCDetail.FirstOrDefault(o => o.DDCDetailID == item.DDCDetailID);

                if (dbDDCDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.ClientMng.DDCDetail, DDCDetail>(item, dbDDCDetail);
                }
            }

            // MAP PenaltyTerms
            if (dtoItem.PenaltyTerms != null)
            {
                foreach (var dbPenaltyTerms in dbItem.PenaltyTerm.ToList())
                {
                    if (!dtoItem.PenaltyTerms.Select(s => s.PenaltyTermID).Contains(dbPenaltyTerms.PenaltyTermID))
                    {
                        dbItem.PenaltyTerm.Remove(dbPenaltyTerms);
                    }
                }

                foreach (var dtoPenaltyTerms in dtoItem.PenaltyTerms.ToList())
                {
                    PenaltyTerm penaltyTerm;

                    if (dtoPenaltyTerms.PenaltyTermID < 0)
                    {
                        penaltyTerm = new PenaltyTerm();
                        dbItem.PenaltyTerm.Add(penaltyTerm);
                    }
                    else
                    {
                        penaltyTerm = dbItem.PenaltyTerm.FirstOrDefault(s => s.PenaltyTermID == dtoPenaltyTerms.PenaltyTermID);
                    }

                    if (penaltyTerm != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientMng.PenaltyTermDTO, PenaltyTerm>(dtoPenaltyTerms, penaltyTerm);
                    }
                }
            }


            AutoMapper.Mapper.Map<DTO.ClientMng.Client, Client>(dtoItem, dbItem);

            // MAP ClientEstimatedAdditionalCost
            if (dtoItem.ClientEstimatedAdditionalCost != null)
            {
                foreach (var dbClientEstimatedAdditionalCost in dbItem.ClientEstimatedAdditionalCost.ToList())
                {
                    if (!dtoItem.ClientEstimatedAdditionalCost.Select(s => s.ClientEstimatedAdditionalCostID).Contains(dbClientEstimatedAdditionalCost.ClientEstimatedAdditionalCostID))
                    {
                        dbItem.ClientEstimatedAdditionalCost.Remove(dbClientEstimatedAdditionalCost);
                    }
                }

                foreach (var dtoClientEstimatedAdditionalCost in dtoItem.ClientEstimatedAdditionalCost.ToList())
                {
                    ClientEstimatedAdditionalCost clientEstimatedAdditionalCost;
                    if (dtoClientEstimatedAdditionalCost.IsConfirmed == true && dtoClientEstimatedAdditionalCost.IsChange == true)
                    {
                        dtoClientEstimatedAdditionalCost.ConfirmedBy = dtoItem.UpdatedBy;
                    }
                    if (dtoClientEstimatedAdditionalCost.ClientEstimatedAdditionalCostID < 0)
                    {
                        clientEstimatedAdditionalCost = new ClientEstimatedAdditionalCost();
                        dbItem.ClientEstimatedAdditionalCost.Add(clientEstimatedAdditionalCost);
                    }
                    else
                    {
                        clientEstimatedAdditionalCost = dbItem.ClientEstimatedAdditionalCost.FirstOrDefault(s => s.ClientEstimatedAdditionalCostID == dtoClientEstimatedAdditionalCost.ClientEstimatedAdditionalCostID);
                    }

                    if (clientEstimatedAdditionalCost != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientMng.ClientEstimatedAdditionalCostDTO, ClientEstimatedAdditionalCost>(dtoClientEstimatedAdditionalCost, clientEstimatedAdditionalCost);
                    }
                }
            }
            // MAP ClientAdditionCondition
            if (dtoItem.ClientAdditionalConditionDTO != null)
            {
                foreach (var dbClientAdditionalCondition in dbItem.ClientAdditionalCondition.ToList())
                {
                    if (!dtoItem.ClientAdditionalConditionDTO.Select(s => s.ClientAdditionalConditionID).Contains(dbClientAdditionalCondition.ClientAdditionalConditionID))
                    {
                        dbItem.ClientAdditionalCondition.Remove(dbClientAdditionalCondition);
                    }
                }

                foreach (var dtoClientAdditionalConditionDTO in dtoItem.ClientAdditionalConditionDTO.ToList())
                {
                    ClientAdditionalCondition clientAdditionalCondition;
                    if (dtoClientAdditionalConditionDTO.ClientAdditionalConditionID < 0)
                    {
                        clientAdditionalCondition = new ClientAdditionalCondition();
                        dbItem.ClientAdditionalCondition.Add(clientAdditionalCondition);
                        if(dtoClientAdditionalConditionDTO.IsSelected == true)
                        {
                            dtoClientAdditionalConditionDTO.UpdatedCheckBy = dtoItem.UpdatedBy;
                            dtoClientAdditionalConditionDTO.UpdatedCheckDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        clientAdditionalCondition = dbItem.ClientAdditionalCondition.FirstOrDefault(s => s.ClientAdditionalConditionID == dtoClientAdditionalConditionDTO.ClientAdditionalConditionID);

                        if (clientAdditionalCondition.AdditionalConditionID == dtoClientAdditionalConditionDTO.AdditionalConditionID)
                        {
                            if (clientAdditionalCondition.IsSelected == dtoClientAdditionalConditionDTO.IsSelected && dtoClientAdditionalConditionDTO.IsSelected == true && clientAdditionalCondition.TypeWhoToPayID != dtoClientAdditionalConditionDTO.TypeWhoToPayID)
                            {
                                dtoClientAdditionalConditionDTO.UpdatedBy = dtoItem.UpdatedBy;
                                dtoClientAdditionalConditionDTO.UpdatedDate = DateTime.Now;
                            }
                            else if (clientAdditionalCondition.IsSelected != dtoClientAdditionalConditionDTO.IsSelected && dtoClientAdditionalConditionDTO.IsSelected == true)
                            {
                                dtoClientAdditionalConditionDTO.UpdatedCheckBy = dtoItem.UpdatedBy;
                                dtoClientAdditionalConditionDTO.UpdatedCheckDate = DateTime.Now;
                            }
                            else if (clientAdditionalCondition.IsSelected != dtoClientAdditionalConditionDTO.IsSelected && dtoClientAdditionalConditionDTO.IsSelected == false)
                            {
                                dtoClientAdditionalConditionDTO.UpdatedUnCheckBy = dtoItem.UpdatedBy;
                                dtoClientAdditionalConditionDTO.UpdatedUnCheckDate = DateTime.Now;
                            }
                            else if (dtoClientAdditionalConditionDTO.IsSelected == true && clientAdditionalCondition.TypeWhoToPayID != dtoClientAdditionalConditionDTO.TypeWhoToPayID)
                            {
                                dtoClientAdditionalConditionDTO.UpdatedBy = dtoItem.UpdatedBy;
                                dtoClientAdditionalConditionDTO.UpdatedDate = DateTime.Now;
                            }

                        }
                    }                                       
                    if (clientAdditionalCondition != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientMng.ClientAdditionalConditionDTO, ClientAdditionalCondition>(dtoClientAdditionalConditionDTO, clientAdditionalCondition);
                    }
                }
            }
            // Business Card
            if (dtoItem.ClientBusinessCardDTO != null)
            {
                foreach (var item in dbItem.ClientBusinessCard.ToArray())
                {
                    if (!dtoItem.ClientBusinessCardDTO.Select(o => o.ClientBusinessCardID).Contains(item.ClientBusinessCardID))
                    {
                        dbItem.ClientBusinessCard.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.ClientBusinessCardDTO)
                {
                    ClientBusinessCard dbFactoryBusinessCard;
                    if (item.ClientBusinessCardID <= 0)
                    {
                        dbFactoryBusinessCard = new ClientBusinessCard();
                        dbItem.ClientBusinessCard.Add(dbFactoryBusinessCard);
                    }
                    else
                    {
                        dbFactoryBusinessCard = dbItem.ClientBusinessCard.FirstOrDefault(o => o.ClientBusinessCardID == item.ClientBusinessCardID);
                    }
                    if (dbFactoryBusinessCard != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientMng.ClientBusinessCardDTO, ClientBusinessCard>(item, dbFactoryBusinessCard);
                    }
                }
            }
        }

        public void DTO2DB_ClientContactHistory(DTO.ClientMng.ClientContactHistory dtoItem, ref ClientContactHistory dbItem)
        {
            if (dtoItem.ClientContactHistoryID < 0)
            {
                dbItem = new ClientContactHistory();
            }
            dbItem.ContactDate = dtoItem.ContactDate.ConvertStringToDateTime();
            AutoMapper.Mapper.Map<DTO.ClientMng.ClientContactHistory, ClientContactHistory>(dtoItem, dbItem);
        }

        public List<DTO.ClientMng.ClientOffer> DB2DTO_ClientOffer(List<ClientMng_Overview_Offer_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_Offer_View>, List<DTO.ClientMng.ClientOffer>>(dbItem);
        }

        public List<DTO.ClientMng.ClientPLC> DB2DTO_ClientPLC(List<ClientMng_Overview_PLC_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_PLC_View>, List<DTO.ClientMng.ClientPLC>>(dbItem);
        }
        public List<DTO.ClientMng.PLCLoadingPLan> DB2DTO_PLCLoadingPLan(List<ClientMng_Overview_PLC_LoadingPlan_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_PLC_LoadingPlan_View>, List<DTO.ClientMng.PLCLoadingPLan>>(dbItem);
        }
        public List<DTO.ClientMng.PLCSaleOrder> DB2DTO_PLCSaleOrder(List<ClientMng_Overview_PLC_SaleOrder_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_PLC_SaleOrder_View>, List<DTO.ClientMng.PLCSaleOrder>>(dbItem);
        }

        public List<DTO.ClientMng.ClientECommercialInvoice> DB2DTO_CommercialInvoice(List<ClientMng_Overview_ECommercialInvoice_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_ECommercialInvoice_View>, List<DTO.ClientMng.ClientECommercialInvoice>>(dbItem);
        }

        public List<DTO.ClientMng.ClientSampleOrder> DB2DTO_SampleOrder(List<ClientMng_Overview_SampleOrder_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_SampleOrder_View>, List<DTO.ClientMng.ClientSampleOrder>>(dbItem);
        }

        public List<DTO.ClientMng.ClientComplaint> DB2DTO_ClientComplaint(List<ClientMng_Overview_ClientComplaint_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_ClientComplaint_View>, List<DTO.ClientMng.ClientComplaint>>(dbItem);
        }

        public List<DTO.ClientMng.ClientSaleOrder> DB2DTO_SaleOrder(List<ClientMng_Overview_SaleOrder_View> dbItem)
        {
            return Mapper.Map<List<ClientMng_Overview_SaleOrder_View>, List<DTO.ClientMng.ClientSaleOrder>>(dbItem);
        }

        public List<DTO.ClientMng.Overview.CIS.ShippingPerformanceDTO> DB2DTO_Overview_ShippingPerformaceDTO(List<ClientMng_Overview_ShippingPerformance_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_ShippingPerformance_View>, List<DTO.ClientMng.Overview.CIS.ShippingPerformanceDTO>>(dbItems);
        }
        public List<DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO> DB2DTO_Overview_ShippingPerformanceChartDTO(List<ClientMng_Overview_ShippingPerformanceChart_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_ShippingPerformanceChart_View>, List<DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO>>(dbItems);
        }
        public List<DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO2> DB2DTO_Overview_ShippingPerformanceChartDTO2(List<ClientMng_Overview_ShippingPerformanceChart2_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_ShippingPerformanceChart2_View>, List<DTO.ClientMng.Overview.CIS.ShippingPerformanceChartDTO2>>(dbItems);
        }
        public List<DTO.ClientMng.Overview.CIS.ItemDTO> DB2DTO_Overview_ItemDTO(List<ClientMng_Overview_Item_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_Item_View>, List<DTO.ClientMng.Overview.CIS.ItemDTO>>(dbItems);
        }
        public List<DTO.ClientMng.Overview.CIS.PriceComparisonDTO> DB2DTO_Overview_PriceComparisonDTO(List<ClientMng_Overview_PriceCompare_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_PriceCompare_View>, List<DTO.ClientMng.Overview.CIS.PriceComparisonDTO>>(dbItems);
        }
        public List<DTO.ClientMng.Overview.LoadingPlanDTO> DB2DTO_Overview_LoadingPlanDTO(List<ClientMng_Overview_LoadingPlan_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_LoadingPlan_View>, List<DTO.ClientMng.Overview.LoadingPlanDTO>>(dbItems);
        }
        public List<DTO.ClientMng.Overview.PISearchResultDTO> DB2DTO_Overview_PISearchResultDTO(List<ClientMng_Overview_PISearchResult_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_PISearchResult_View>, List<DTO.ClientMng.Overview.PISearchResultDTO>>(dbItems);
        }
        public List<DTO.ClientMng.Overview.Delta.EurofarPriceOverviewDTO> DB2DTO_Overview_Delta_EurofarPriceOverviewDTO(List<ClientMng_Overview_EurofarPriceOverview_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_EurofarPriceOverview_View>, List<DTO.ClientMng.Overview.Delta.EurofarPriceOverviewDTO>>(dbItems);
        }

        public List<DTO.ClientMng.Overview.Delta2.EurofarPriceOverviewDTO> DB2DTO_Overview_Delta2_EurofarPriceOverviewDTO(List<ClientMng_Overview_EurofarPriceOverviewGroupByItem_View> dbItems)
        {
            return Mapper.Map<List<ClientMng_Overview_EurofarPriceOverviewGroupByItem_View>, List<DTO.ClientMng.Overview.Delta2.EurofarPriceOverviewDTO>>(dbItems);
        }


        //For Breakdown Legend :(
        public List<DTO.ClientMng.Overview.Breakdown.FactoryQuotationSeachDTO> DB2DTO_SearchfactoryQuotation(List<ClientMng_Breakdown_FactoryQuotationSearchResult_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_Breakdown_FactoryQuotationSearchResult_View>, List<DTO.ClientMng.Overview.Breakdown.FactoryQuotationSeachDTO>>(dbitems);
        }

        public List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryOptionDTO> DB2DTO_BreakdownCategoryOption(List<ClientMng_BreakdownCategoryOption_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_BreakdownCategoryOption_View>, List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryOptionDTO>>(dbitems);
        }

        public List<DTO.ClientMng.Overview.Breakdown.QuotationStatusDTO> DB2DTO_QuotationSupportList(List<SupportMng_QuotationStatus_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_QuotationStatus_View>, List<DTO.ClientMng.Overview.Breakdown.QuotationStatusDTO>>(dbitems);
        }

        public List<DTO.ClientMng.Overview.Breakdown.OrderTypeDTO> DB2DTO_OrderTypeSupport(List<SupportMng_OrderType_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_OrderType_View>, List<DTO.ClientMng.Overview.Breakdown.OrderTypeDTO>>(dbitems);
        }

        public List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryDTO> DB2DTO_BreakdownCategory(List<ClientMng_BreakdownCategory_view> dbitems)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_BreakdownCategory_view>, List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryDTO>>(dbitems);
        }

        public DTO.ClientMng.Overview.Breakdown.SubTotalDTO DB2DTO_SubtotalDTO(ClientMng_SubTotal_View dbitem)
        {
            return AutoMapper.Mapper.Map<ClientMng_SubTotal_View, DTO.ClientMng.Overview.Breakdown.SubTotalDTO>(dbitem);
        }

        public List<DTO.ClientMng.Overview.CIS.ModelDTO> DB2DTO_Overview_ModelDTO(List<ClientMng_Overview_Model_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_Overview_Model_View>, List<DTO.ClientMng.Overview.CIS.ModelDTO>>(dbItem);
        }

        public List<DTO.ClientMng.ClientDocumentTypeDTO> DB2DTO_ClientDocumentTypeDTO(List<SupportMng_ClientDocumentType_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientDocumentType_View>, List<DTO.ClientMng.ClientDocumentTypeDTO>>(dbItem);
        }

        public List<DTO.ClientMng.ClientAdditionalConditionDTO> DB2DTO_ClientAdditionalCondition(List<ClientMng_ClientAdditionalCondition_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_ClientAdditionalCondition_View>, List<DTO.ClientMng.ClientAdditionalConditionDTO>>(dbItem);
        }

        public List<DTO.ClientMng.ClientGetDataAdditionalConditionDTO> DB2DTO_ClientGetDataAdditionalCondition(List<ClientMng_ClientAdditionalConditionGetdata_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_ClientAdditionalConditionGetdata_View>, List<DTO.ClientMng.ClientGetDataAdditionalConditionDTO>>(dbItem);
        }

        public List<DTO.ClientMng.ClientAdditionalConditionTypeDTO> DB2DTO_ClientGetDataType(List<Support_ClientAdditionalCondition_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<Support_ClientAdditionalCondition_View>, List<DTO.ClientMng.ClientAdditionalConditionTypeDTO>>(dbItem);
        }
        public List<DTO.ClientMng.ClientBusinessCardDTO> DB2DTO_ClientBusinessCard(List<ClientMng_ClientBusinessCard_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_ClientBusinessCard_View>, List<DTO.ClientMng.ClientBusinessCardDTO>>(dbItem);
        }
        //public List<DTO.ClientMng.Overview.Offer.OfferOverview> DB2DTO_OfferOverView(List<ClientMng_Offer_OverView> dbItem)
        //{
        //    return AutoMapper.Mapper.Map<List<ClientMng_Offer_OverView>, List<DTO.ClientMng.Overview.Offer.OfferOverview>>(dbItem);
        //}

        public List<DTO.ClientMng.Overview.Offer.OfferMarginDTO> DB2DTO_OfferMargin(List<ClientMng_OfferMargin_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_OfferMargin_View>, List<DTO.ClientMng.Overview.Offer.OfferMarginDTO>>(dbItem);
        }

        public List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO> DB2DTO_OfferMarginDetail(List<ClientMng_OfferMarginDetail_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_OfferMarginDetail_View>, List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO>>(dbItem);
        }

        public List<DTO.ClientMng.PlaningPurchasingPriceDTO> DB2DTO_PlaningPurchasingPriceDTO(List<ClientMng_function_GetOfferLinePlaningPurchasingPrice_Result> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientMng_function_GetOfferLinePlaningPurchasingPrice_Result>, List<DTO.ClientMng.PlaningPurchasingPriceDTO>>(dbItem);
        }

        public List<DTO.ClientMng.PlaningPurchasingPriceSourceDTO> DB2DTO_PlaningPurchasingPriceSourceDTO(List<SupportMng_PlaningPurchasingPriceSource_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PlaningPurchasingPriceSource_View>, List<DTO.ClientMng.PlaningPurchasingPriceSourceDTO>>(dbItem);
        }

        public List<DTO.ClientMng.UsersDTO> DB2DTO_UsersDTO(List<SupportMng_User2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_User2_View>, List<DTO.ClientMng.UsersDTO>>(dbItem);
        }
    }
}
