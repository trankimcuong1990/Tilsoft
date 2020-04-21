using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoice_SearchView, DTO.DraftCommercialInvoiceSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoice_View, DTO.DraftCommercialInvoiceDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DraftCommercialInvoiceDetails, o => o.MapFrom(s => s.DraftCommercialInvoiceMng_DraftCommercialInvoiceDetail_View))
                    .ForMember(d => d.DraftCommercialInvoiceDescriptions, o => o.MapFrom(s => s.DraftCommercialInvoiceMng_DraftCommercialInvoiceDescription_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoiceDetail_View, DTO.DraftCommercialInvoiceDetailDTO>()
                    .ForMember(d => d.DraftCommercialInvoiceDetailDescriptions, o => o.MapFrom(s => s.DraftCommercialInvoiceMng_DraftCommercialInvoiceDetailDescription_View))
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoice_OverView, DTO.DraftCommercialInvoiceOverViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DraftCommercialInvoiceDetails, o => o.MapFrom(s => s.DraftCommercialInvoiceMng_DraftCommercialInvoiceDetail_OverView))
                    .ForMember(d => d.DraftCommercialInvoiceDescriptions, o => o.MapFrom(s => s.DraftCommercialInvoiceMng_DraftCommercialInvoiceDescription_OverView))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoiceDetail_OverView, DTO.DraftCommercialInvoiceDetailOverViewDTO>()
                    .ForMember(d => d.DraftCommercialInvoiceDetailDescriptions, o => o.MapFrom(s => s.DraftCommercialInvoiceMng_DraftCommercialInvoiceDetailDescription_OverView))
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoiceDescription_OverView, DTO.DraftCommercialInvoiceDescriptionOverViewDTO>()
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoiceDetailDescription_OverView, DTO.DraftCommercialInvoiceDetailDescriptionOverViewDTO>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_SupportClient_View, DTO.SupportClient>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DeliveryTerm_View, DTO.DeliveryTermDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_PaymentTerm_View, DTO.PaymentTermDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_TurnOverLedgerAccount_View, DTO.TurnOverLedgerAccountDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_SaleOrderDetail_View, DTO.SaleOrderDetailDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_SaleOrderDetailSparepart_View, DTO.SaleOrderDetailSparepartDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoiceDescription_View, DTO.DraftCommercialInvoiceDescriptionDTO>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftCommercialInvoiceMng_DraftCommercialInvoiceDetailDescription_View, DTO.DraftCommercialInvoiceDetailDescriptionDTO>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.DraftCommercialInvoiceDTO, DraftCommercialInvoice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.DraftCommercialInvoiceDetail, o => o.Ignore())
                   .ForMember(d => d.DraftCommercialInvoiceDescription, o => o.Ignore())
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.CreatedDate, o => o.Ignore())
                   .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                   .ForMember(d => d.InvoiceDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DraftCommercialInvoiceDetailDTO, DraftCommercialInvoiceDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.DraftCommercialInvoiceDetailDescription, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DraftCommercialInvoiceDetailDescriptionDTO, DraftCommercialInvoiceDetailDescription>()
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.DraftCommercialInvoiceDescriptionDTO, DraftCommercialInvoiceDescription>()
                  .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        //public List<DTO.OutsourcingCostTypeSearchDTO> DB2DTO_OutSourcingCostTypeSearchResultList(List<OutsourcingCostTypeMng_OutSourcingCostTypeMng_SearchView> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<OutsourcingCostTypeMng_OutSourcingCostTypeMng_SearchView>, List<DTO.OutsourcingCostTypeSearchDTO>>(dbItems);
        //}

        public DTO.DraftCommercialInvoiceDTO DB2DTO_DraftCommercialInvoice(DraftCommercialInvoiceMng_DraftCommercialInvoice_View dbItems)
        {
            return AutoMapper.Mapper.Map<DraftCommercialInvoiceMng_DraftCommercialInvoice_View, DTO.DraftCommercialInvoiceDTO>(dbItems);
        }

        public DTO.DraftCommercialInvoiceOverViewDTO DB2DTO_DraftCommercialInvoiceOverView(DraftCommercialInvoiceMng_DraftCommercialInvoice_OverView dbItems)
        {
            return AutoMapper.Mapper.Map<DraftCommercialInvoiceMng_DraftCommercialInvoice_OverView, DTO.DraftCommercialInvoiceOverViewDTO>(dbItems);
        }

        public void DTO2DB_DraftCommercialInvoice(DTO.DraftCommercialInvoiceDTO dtoItem, ref DraftCommercialInvoice dbItem)
        {
            if (dtoItem.DraftCommercialInvoiceDetails != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.DraftCommercialInvoiceDetail.ToArray())
                {
                    if (!dtoItem.DraftCommercialInvoiceDetails.Select(s => s.DraftCommercialInvoiceDetailID).Contains(item.DraftCommercialInvoiceDetailID))
                    {
                        //delete Detail Descripton
                        foreach (var wItem in item.DraftCommercialInvoiceDetailDescription.ToArray())
                        {
                            item.DraftCommercialInvoiceDetailDescription.Remove(wItem);
                        }                   
                        //delete detail
                        dbItem.DraftCommercialInvoiceDetail.Remove(item);
                    }
                    else
                    {
                        var x = dtoItem.DraftCommercialInvoiceDetails.Where(o => o.DraftCommercialInvoiceDetailID == item.DraftCommercialInvoiceDetailID).FirstOrDefault().DraftCommercialInvoiceDetailDescriptions;

                        //delete detail Descripton
                        foreach (var wItem in item.DraftCommercialInvoiceDetailDescription.ToArray())
                        {
                            if (!x.Select(s => s.DraftCommercialInvoiceDetailDescriptionID).Contains(wItem.DraftCommercialInvoiceDetailDescriptionID))
                            {
                                item.DraftCommercialInvoiceDetailDescription.Remove(wItem);
                            }
                        }
                        
                    }
                }
                //read from dto to db
                DraftCommercialInvoiceDetail dbDraftCommercialInvoiceDetail;
                DraftCommercialInvoiceDetailDescription dbDraftCommercialInvoiceDetailDescription;
                foreach (var item in dtoItem.DraftCommercialInvoiceDetails)
                {
                    if (item.DraftCommercialInvoiceDetailID > 0)
                    {
                        dbDraftCommercialInvoiceDetail = dbItem.DraftCommercialInvoiceDetail.Where(o => o.DraftCommercialInvoiceDetailID == item.DraftCommercialInvoiceDetailID).FirstOrDefault();

                        //read from purchase order workorder detail dto to db
                        if (dbDraftCommercialInvoiceDetail != null && item.DraftCommercialInvoiceDetailDescriptions != null)
                        {
                            foreach (var wItem in item.DraftCommercialInvoiceDetailDescriptions)
                            {
                                if (wItem.DraftCommercialInvoiceDetailDescriptionID > 0)
                                {
                                    dbDraftCommercialInvoiceDetailDescription = dbDraftCommercialInvoiceDetail.DraftCommercialInvoiceDetailDescription.Where(o => o.DraftCommercialInvoiceDetailDescriptionID == wItem.DraftCommercialInvoiceDetailDescriptionID).FirstOrDefault();
                                }
                                else
                                {
                                    dbDraftCommercialInvoiceDetailDescription = new DraftCommercialInvoiceDetailDescription();
                                    dbDraftCommercialInvoiceDetail.DraftCommercialInvoiceDetailDescription.Add(dbDraftCommercialInvoiceDetailDescription);
                                }
                                if (dbDraftCommercialInvoiceDetailDescription != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.DraftCommercialInvoiceDetailDescriptionDTO, DraftCommercialInvoiceDetailDescription>(wItem, dbDraftCommercialInvoiceDetailDescription);
                                }
                            }
                        }                     
                    }
                    else
                    {
                        dbDraftCommercialInvoiceDetail = new DraftCommercialInvoiceDetail();
                        dbItem.DraftCommercialInvoiceDetail.Add(dbDraftCommercialInvoiceDetail);

                        //read from purchase order workorder detail dto to db
                        if (item.DraftCommercialInvoiceDetailDescriptions != null)
                        {
                            foreach (var mItem in item.DraftCommercialInvoiceDetailDescriptions)
                            {
                                dbDraftCommercialInvoiceDetailDescription = new DraftCommercialInvoiceDetailDescription();
                                dbDraftCommercialInvoiceDetail.DraftCommercialInvoiceDetailDescription.Add(dbDraftCommercialInvoiceDetailDescription);
                                AutoMapper.Mapper.Map<DTO.DraftCommercialInvoiceDetailDescriptionDTO, DraftCommercialInvoiceDetailDescription>(mItem, dbDraftCommercialInvoiceDetailDescription);
                            }
                        }                     
                    }
                    //read purchase request detail dto to db
                    if (dbDraftCommercialInvoiceDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.DraftCommercialInvoiceDetailDTO, DraftCommercialInvoiceDetail>(item, dbDraftCommercialInvoiceDetail);
                    }
                }
            }


            //Description
            if (dtoItem.DraftCommercialInvoiceDescriptions != null)
            {
                foreach (DraftCommercialInvoiceDescription item in dbItem.DraftCommercialInvoiceDescription.ToList())
                {
                    if (!dtoItem.DraftCommercialInvoiceDescriptions.Select(s => s.DraftCommercialInvoiceDescriptionID).Contains(item.DraftCommercialInvoiceDescriptionID))
                        dbItem.DraftCommercialInvoiceDescription.Remove(item);
                }

                foreach (DTO.DraftCommercialInvoiceDescriptionDTO dto in dtoItem.DraftCommercialInvoiceDescriptions)
                {
                    DraftCommercialInvoiceDescription item;

                    if (dto.DraftCommercialInvoiceDescriptionID < 0)
                    {
                        item = new DraftCommercialInvoiceDescription();

                        dbItem.DraftCommercialInvoiceDescription.Add(item);
                    }
                    else
                    {
                        item = dbItem.DraftCommercialInvoiceDescription.FirstOrDefault(s => s.DraftCommercialInvoiceDescriptionID == dto.DraftCommercialInvoiceDescriptionID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.DraftCommercialInvoiceDescriptionDTO, DraftCommercialInvoiceDescription>(dto, item);
                }
            }

            AutoMapper.Mapper.Map<DTO.DraftCommercialInvoiceDTO, DraftCommercialInvoice>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
            dbItem.CreatedDate = dtoItem.CreatedDate.ConvertStringToDateTime();
            dbItem.ConfirmedDate = dtoItem.ConfirmedDate.ConvertStringToDateTime();
            dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
        }

        public List<DTO.DraftCommercialInvoiceSearchDTO> DB2DTO_SearchDraftCommercialInvoice(List<DraftCommercialInvoiceMng_DraftCommercialInvoice_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftCommercialInvoiceMng_DraftCommercialInvoice_SearchView>, List<DTO.DraftCommercialInvoiceSearchDTO>>(dbItems);
        }
        public List<DTO.SupportClient> DB2DTO_SupportClient(List<DraftCommercialInvoiceMng_SupportClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftCommercialInvoiceMng_SupportClient_View>, List<DTO.SupportClient>>(dbItems);
        }
        public List<DTO.DeliveryTermDTO> DB2DTO_DeliveryTerm(List<DraftCommercialInvoiceMng_DeliveryTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftCommercialInvoiceMng_DeliveryTerm_View>, List<DTO.DeliveryTermDTO>>(dbItems);
        }
        public List<DTO.PaymentTermDTO> DB2DTO_PaymentTerm(List<DraftCommercialInvoiceMng_PaymentTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftCommercialInvoiceMng_PaymentTerm_View>, List<DTO.PaymentTermDTO>>(dbItems);
        }
        public List<DTO.TurnOverLedgerAccountDTO> DB2DTO_TurnOverLedgerAccount(List<DraftCommercialInvoiceMng_TurnOverLedgerAccount_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftCommercialInvoiceMng_TurnOverLedgerAccount_View>, List<DTO.TurnOverLedgerAccountDTO>>(dbItems);
        }
        public List<DTO.SaleOrderDetailDTO> DB2DTO_SaleOrderDetail(List<DraftCommercialInvoiceMng_SaleOrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftCommercialInvoiceMng_SaleOrderDetail_View>, List<DTO.SaleOrderDetailDTO>>(dbItems);
        }
        public List<DTO.SaleOrderDetailSparepartDTO> DB2DTO_SaleOrderDetailSparepart(List<DraftCommercialInvoiceMng_SaleOrderDetailSparepart_View> dbItems)
        {       
            return AutoMapper.Mapper.Map<List<DraftCommercialInvoiceMng_SaleOrderDetailSparepart_View>, List<DTO.SaleOrderDetailSparepartDTO>>(dbItems);
        }    
    }
}
