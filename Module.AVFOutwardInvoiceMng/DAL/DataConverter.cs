using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.AVFOutwardInvoiceMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View, DTO.AVFOutwardInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVFOutwardInvoiceMng_AVFOutwardInvoice_View, DTO.AVFOutwardInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.AVFOutwardInvoiceDetails, o => o.MapFrom(s => s.AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AVFOutwardInvoice, AVFOutwardInvoice>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.AVFOutwardInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.AVFOutwardInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View, DTO.AVFOutwardInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AVFOutwardInvoiceDetail, AVFOutwardInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.AVFOutwardInvoiceDetailID, o => o.Ignore())
                    .ForMember(d => d.AVFOutwardInvoiceID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.AVFOutwardInvoiceSearchResult> DB2DTO_AVFOutwardInvoiceSearchResultList(List<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVFOutwardInvoiceMng_AVFOutwardInvoice_SearchResult_View>, List<DTO.AVFOutwardInvoiceSearchResult>>(dbItems);
        }
        public DTO.AVFOutwardInvoice DB2DTO_AVFOutwardInvoice(AVFOutwardInvoiceMng_AVFOutwardInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<AVFOutwardInvoiceMng_AVFOutwardInvoice_View, DTO.AVFOutwardInvoice>(dbItem);
        }
        public void DTO2BD(DTO.AVFOutwardInvoice dtoItem, ref AVFOutwardInvoice dbItem)
        {
            dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
            AutoMapper.Mapper.Map<DTO.AVFOutwardInvoice, AVFOutwardInvoice>(dtoItem, dbItem);
            if (dtoItem.AVFOutwardInvoiceDetails != null)
            {
                //check for child rows deleted
                foreach (AVFOutwardInvoiceDetail dbDetail in dbItem.AVFOutwardInvoiceDetail.ToArray())
                {
                    if (!dtoItem.AVFOutwardInvoiceDetails.Select(o => o.AVFOutwardInvoiceDetailID).Contains(dbDetail.AVFOutwardInvoiceDetailID))
                    {
                        dbItem.AVFOutwardInvoiceDetail.Remove(dbDetail);
                    }
                }

                //map child row
                foreach (DTO.AVFOutwardInvoiceDetail dtoDetail in dtoItem.AVFOutwardInvoiceDetails)
                {
                    AVFOutwardInvoiceDetail dbDetail;
                    if (dtoDetail.AVFOutwardInvoiceDetailID <= 0)
                    {
                        dbDetail = new AVFOutwardInvoiceDetail();
                        dbItem.AVFOutwardInvoiceDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.AVFOutwardInvoiceDetail.FirstOrDefault(o => o.AVFOutwardInvoiceDetailID == dtoDetail.AVFOutwardInvoiceDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.AVFOutwardInvoiceDetail, AVFOutwardInvoiceDetail>(dtoDetail, dbDetail);
                    }
                }
                AutoMapper.Mapper.Map<DTO.AVFOutwardInvoice, AVFOutwardInvoice>(dtoItem, dbItem);
            }
        }
    }
}
