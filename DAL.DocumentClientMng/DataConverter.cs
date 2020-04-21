using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.DocumentClientMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DocumentClientMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DocumentClientMng_DocumentClientSearchResult_View, DTO.DocumentClientMng.DocumentClientSearchResult>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.ETA2, o => o.ResolveUsing(s => s.ETA2.HasValue ? s.ETA2.Value.ToString("dd/MM/yyyy") : ""))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DocumentClientMng_DocumentClient_View, DTO.DocumentClientMng.DocumentClient>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BLFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.BLFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.BLFileURL)))
                    .ForMember(d => d.ECommercialInvoices, o => o.MapFrom(s =>s.DocumentClientMng_ECommercialInvoice_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DocumentClientMng.DocumentClient, DocumentClient>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DateEmailToClient, o => o.Ignore())
                    .ForMember(d => d.DateSendToClient, o => o.Ignore())
                    .ForMember(d => d.DateContainerDelivery, o => o.Ignore())
                    .ForMember(d => d.TimeContainerDelivery, o => o.Ignore())
                    .ForMember(d => d.DocumentClientID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<TypeOfDelivery, DTO.DocumentClientMng.TypeOfDelivery>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlaceOfBarge, DTO.DocumentClientMng.PlaceOfBarge>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlaceOfDelivery, DTO.DocumentClientMng.PlaceOfDelivery>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentStatus, DTO.DocumentClientMng.PaymentStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryStatus, DTO.DocumentClientMng.DeliveryStatus>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DocumentClientMng_ECommercialInvoice_View, DTO.DocumentClientMng.ECommercialInvoice>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DocumentClientMng.DocumentClientSearchUpdate, DocumentClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DocumentClientMng.DocumentClientSearchResult> DB2DTO_DocumentClientSearch(List<DocumentClientMng_DocumentClientSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DocumentClientMng_DocumentClientSearchResult_View>, List<DTO.DocumentClientMng.DocumentClientSearchResult>>(dbItems);
        }

        public DTO.DocumentClientMng.DocumentClient DB2DTO_DocumentClient(DocumentClientMng_DocumentClient_View dbItem)
        {
            DTO.DocumentClientMng.DocumentClient dtoItem = AutoMapper.Mapper.Map<DocumentClientMng_DocumentClient_View, DTO.DocumentClientMng.DocumentClient>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.DateEmailToClient.HasValue)
                dtoItem.DateEmailToClientFormated = dbItem.DateEmailToClient.Value.ToString("dd/MM/yyyy");

            if (dbItem.DateSendToClient.HasValue)
                dtoItem.DateSendToClientFormated = dbItem.DateSendToClient.Value.ToString("dd/MM/yyyy");

            if (dbItem.DateContainerDelivery.HasValue)
                dtoItem.DateContainerDeliveryFormated = dbItem.DateContainerDelivery.Value.ToString("dd/MM/yyyy");

            if (dbItem.TimeContainerDelivery.HasValue)
                dtoItem.TimeContainerDeliveryFormated = dbItem.TimeContainerDelivery.Value.ToString("HH:mm");

            if (dbItem.ETA.HasValue)
                dtoItem.ETAFormated = dbItem.ETA.Value.ToString("dd/MM/yyyy");

            if (dbItem.ETA2.HasValue)
                dtoItem.ETA2Formated = dbItem.ETA2.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.ConfirmedDateContainerDeliveryDate.HasValue)
                dtoItem.ConfirmedDateContainerDeliveryDateFormated = dbItem.ConfirmedDateContainerDeliveryDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2DB_DocumentClient(DTO.DocumentClientMng.DocumentClient dtoItem, ref DocumentClient dbItem)
        {
            AutoMapper.Mapper.Map<DTO.DocumentClientMng.DocumentClient, DocumentClient>(dtoItem, dbItem);

            dbItem.DateEmailToClient = dtoItem.DateEmailToClientFormated.ConvertStringToDateTime();
            dbItem.DateSendToClient = dtoItem.DateSendToClientFormated.ConvertStringToDateTime();
            if (dbItem.IsConfirmedDateContainerDelivery == null || (bool)dbItem.IsConfirmedDateContainerDelivery == false) dbItem.DateContainerDelivery = dtoItem.DateContainerDeliveryFormated.ConvertStringToDateTime();
            dbItem.TimeContainerDelivery = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + dtoItem.TimeContainerDeliveryFormated);
        }

        public void DTO2DB_QuickDocumentClient(DTO.DocumentClientMng.DocumentClientSearchUpdate dtoItem, ref DocumentClient dbItem)
        {
            AutoMapper.Mapper.Map<DTO.DocumentClientMng.DocumentClientSearchUpdate, DocumentClient>(dtoItem, dbItem);

            dbItem.DateEmailToClient = dtoItem.DateEmailToClientFormated.ConvertStringToDateTime();
            dbItem.DateSendToClient = dtoItem.DateSendToClientFormated.ConvertStringToDateTime();
            if (dbItem.IsConfirmedDateContainerDelivery == null || (bool)dbItem.IsConfirmedDateContainerDelivery == false)
                dbItem.DateContainerDelivery = dtoItem.DateContainerDeliveryFormated.ConvertStringToDateTime();
        }
        
        public List<DTO.DocumentClientMng.TypeOfDelivery> DB2DTO_TypeOfDeliverys(List<TypeOfDelivery> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TypeOfDelivery>, List<DTO.DocumentClientMng.TypeOfDelivery>>(dbItems);
        }

        public List<DTO.DocumentClientMng.PlaceOfBarge> DB2DTO_PlaceOfBarges(List<PlaceOfBarge> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlaceOfBarge>, List<DTO.DocumentClientMng.PlaceOfBarge>>(dbItems);
        }

        public List<DTO.DocumentClientMng.PlaceOfDelivery> DB2DTO_PlaceOfDeliverys(List<PlaceOfDelivery> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlaceOfDelivery>, List<DTO.DocumentClientMng.PlaceOfDelivery>>(dbItems);
        }
        
        public List<DTO.DocumentClientMng.DeliveryStatus> DB2DTO_DeliveryStatuss(List<DeliveryStatus> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DeliveryStatus>, List<DTO.DocumentClientMng.DeliveryStatus>>(dbItems);
        }
        
        public List<DTO.DocumentClientMng.PaymentStatus> DB2DTO_PaymentStatuss(List<PaymentStatus> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentStatus>, List<DTO.DocumentClientMng.PaymentStatus>>(dbItems);
        }

        //public ReportDataObject DB2DS_DocumentClientReport(List<DocumentClientMng_DocumentClient_ReportView> dbItems)
        //{
        //    ReportDataObject dsReport = new ReportDataObject();
        //    AutoMapper.Mapper.Reset();
        //    AutoMapper.Mapper.CreateMap<DocumentClientMng_ECommercialInvoice_ReportView, ReportDataObject.DocumentClientMng_ECommercialInvoice_ReportViewRow>()
        //        .IgnoreAllNonExisting()
        //        .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

        //    ReportDataObject.DocumentClientMng_ECommercialInvoice_ReportViewRow drMainReport;

        //    foreach (var documentItem in dbItems)
        //    {
        //        int i = 1;
        //        foreach (var eInvoiceItem in documentItem.DocumentClientMng_ECommercialInvoice_ReportView)
        //        {
        //            drMainReport = dsReport.DocumentClientMng_ECommercialInvoice_ReportView.NewDocumentClientMng_ECommercialInvoice_ReportViewRow();

        //            if (i == 1)
        //            {
        //                eInvoiceItem.DC20 = documentItem.DC20;
        //                eInvoiceItem.DC40 = documentItem.DC40;
        //                eInvoiceItem.HC40 = documentItem.HC40;
        //                eInvoiceItem.CostOfDemurage1 = documentItem.CostOfDemurage1;
        //                eInvoiceItem.CostOfDemurage2 = documentItem.CostOfDemurage2;
        //            }

        //            AutoMapper.Mapper.Map<DocumentClientMng_ECommercialInvoice_ReportView, ReportDataObject.DocumentClientMng_ECommercialInvoice_ReportViewRow>(eInvoiceItem, drMainReport);
        //            dsReport.DocumentClientMng_ECommercialInvoice_ReportView.AddDocumentClientMng_ECommercialInvoice_ReportViewRow(drMainReport);
        //            i++;
        //        }
        //    }

        //    return dsReport;
        //}
    }
}
