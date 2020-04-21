using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.TransportInvoice.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "TransportInvoiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_TransportInvoice_SearchView, DTO.TransportInvoiceSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_TransportInvoice_View, DTO.TransportInvoiceDTO>()
                    .IgnoreAllNonExisting()
                     .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.TransportInvoiceDetailDTOs, o => o.MapFrom(s => s.TransportInvoiceMng_TransportInvoiceDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_TransportInvoiceDetail_View, DTO.TransportInvoiceDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.TransportInvoiceContainerDetailDTOs, o => o.MapFrom(s => s.TransportInvoiceMng_TransportInvoiceContainerDetail_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_TransportInvoiceContainerDetail_View, DTO.TransportInvoiceContainerDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.TransportInvoiceContainerDetailDTO, TransportInvoiceContainerDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.TransportInvoiceContainerDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TransportInvoiceDetailDTO, TransportInvoiceDetail>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.TransportInvoiceContainerDetail, o => o.Ignore())
                   .ForMember(d => d.TransportInvoiceDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TransportInvoiceDTO, TransportInvoice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.Ignore())
                   .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                   .ForMember(d => d.CreatedDate, o => o.Ignore())
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.CreatedBy, o => o.Ignore())
                   .ForMember(d => d.UpdatedBy, o => o.Ignore())
                   .ForMember(d => d.TransportInvoiceDetail, o => o.Ignore())
                   .ForMember(d => d.TransportInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_LoadingPlan_View, DTO.LoadingPlanDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_Booking_View, DTO.BookingDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_TransportCostItem_View, DTO.TransportCostItemDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportInvoiceMng_InitCostItem_View, DTO.InitCostItemDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.TransportInvoiceSearchDTO> DB2DTO_TransportInvoiceSearch(List<TransportInvoiceMng_TransportInvoice_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TransportInvoiceMng_TransportInvoice_SearchView>, List<DTO.TransportInvoiceSearchDTO>>(dbItems);
        }

        public DTO.TransportInvoiceDTO DB2DTO_TransportInvoice(TransportInvoiceMng_TransportInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<TransportInvoiceMng_TransportInvoice_View, DTO.TransportInvoiceDTO>(dbItem);
        }

        public void DTO2DB_TransportInvoice(DTO.TransportInvoiceDTO dtoItem, ref TransportInvoice dbItem)
        {
            if (dtoItem.TransportInvoiceDetailDTOs != null)
            {
                //delete
                foreach (var item in dbItem.TransportInvoiceDetail.ToArray())
                {
                    if (!dtoItem.TransportInvoiceDetailDTOs.Select(s => s.TransportInvoiceDetailID).Contains(item.TransportInvoiceDetailID))
                    {
                        foreach (var sItem in item.TransportInvoiceContainerDetail.ToArray())
                        {
                            item.TransportInvoiceContainerDetail.Remove(sItem);
                        }
                        dbItem.TransportInvoiceDetail.Remove(item);
                    }
                    else
                    {
                        foreach (var sItem in item.TransportInvoiceContainerDetail.ToArray())
                        {
                            var x = dtoItem.TransportInvoiceDetailDTOs.Where(o => o.TransportInvoiceDetailID == item.TransportInvoiceDetailID).FirstOrDefault().TransportInvoiceContainerDetailDTOs;
                            if (!x.Select(s => s.TransportInvoiceContainerDetailID).Contains(sItem.TransportInvoiceContainerDetailID))
                            {
                                item.TransportInvoiceContainerDetail.Remove(sItem);
                            }
                        }
                    }
                }
                //update
                TransportInvoiceDetail dbDetail;
                TransportInvoiceContainerDetail dbOrderDetail;
                foreach (var item in dtoItem.TransportInvoiceDetailDTOs)
                {
                    if (item.TransportInvoiceDetailID < 0)
                    {
                        dbDetail = new TransportInvoiceDetail();
                        dbItem.TransportInvoiceDetail.Add(dbDetail);
                        if (item.TransportInvoiceContainerDetailDTOs != null)
                        {
                            foreach (var sItem in item.TransportInvoiceContainerDetailDTOs)
                            {
                                dbOrderDetail = new TransportInvoiceContainerDetail();
                                dbDetail.TransportInvoiceContainerDetail.Add(dbOrderDetail);
                                AutoMapper.Mapper.Map<DTO.TransportInvoiceContainerDetailDTO, TransportInvoiceContainerDetail>(sItem, dbOrderDetail);
                            }
                        }
                    }
                    else
                    {
                        dbDetail = dbItem.TransportInvoiceDetail.Where(o => o.TransportInvoiceDetailID == item.TransportInvoiceDetailID).FirstOrDefault();
                        if (dbDetail != null && item.TransportInvoiceContainerDetailDTOs != null)
                        {
                            foreach (var sItem in item.TransportInvoiceContainerDetailDTOs)
                            {
                                if (sItem.TransportInvoiceContainerDetailID > 0)
                                {
                                    dbOrderDetail = dbDetail.TransportInvoiceContainerDetail.Where(o => o.TransportInvoiceContainerDetailID == sItem.TransportInvoiceContainerDetailID).FirstOrDefault();
                                }
                                else
                                {
                                    dbOrderDetail = new TransportInvoiceContainerDetail();
                                    dbDetail.TransportInvoiceContainerDetail.Add(dbOrderDetail);
                                }
                                if (dbOrderDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.TransportInvoiceContainerDetailDTO, TransportInvoiceContainerDetail>(sItem, dbOrderDetail);
                                }
                            }
                        }
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.TransportInvoiceDetailDTO, TransportInvoiceDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.TransportInvoiceDTO, TransportInvoice>(dtoItem, dbItem);
            dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
        }
    }
}
