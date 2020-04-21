using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.FactoryProformaInvoiceMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryProformaInvoiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryProformaInvoiceMng_FactoryProformaInvoiceSearchResult_View, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProformaInvoiceMng_FactoryProformaInvoice_View, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.FactoryProformaInvoiceMng_FactoryProformaInvoiceDetail_View))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProformaInvoiceMng_FactoryProformaInvoiceDetail_View, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProformaInvoiceMng_FactoryOrderItemSearchResult_View, DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProformaInvoiceMng_FactoryOrderSearchResult_View, DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice, FactoryProformaInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.IsConfirmed, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryProformaInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.FactoryProformaInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceDetail, FactoryProformaInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryProformaInvoiceID, o => o.Ignore())
                    .ForMember(d => d.FactoryProformaInvoiceDetailID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceSearchResult> DB2DTO_FactoryProformaInvoiceSearchResultList(List<FactoryProformaInvoiceMng_FactoryProformaInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProformaInvoiceMng_FactoryProformaInvoiceSearchResult_View>, List<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceSearchResult>>(dbItems);
        }

        public List<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult> DB2DTO_FactoryOrderItemSearchResultList(List<FactoryProformaInvoiceMng_FactoryOrderItemSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProformaInvoiceMng_FactoryOrderItemSearchResult_View>, List<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult>>(dbItems);
        }

        public List<DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult> DB2DTO_FactoryOrderSearchResultList(List<FactoryProformaInvoiceMng_FactoryOrderSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProformaInvoiceMng_FactoryOrderSearchResult_View>, List<DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult>>(dbItems);
        }

        public DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice DB2DTO_FactoryProformaInvoice(FactoryProformaInvoiceMng_FactoryProformaInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryProformaInvoiceMng_FactoryProformaInvoice_View, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>(dbItem);
        }

        public void DTO2BD(DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem, ref FactoryProformaInvoice dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice, FactoryProformaInvoice>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            if (!string.IsNullOrEmpty(dtoItem.InvoiceDate))
            {
                try
                {
                    dbItem.InvoiceDate = DateTime.ParseExact(dtoItem.InvoiceDate, "d", new System.Globalization.CultureInfo("vi-VN"));
                }
                catch
                {
                    dbItem.InvoiceDate = null;
                }
            }

            // map pieces
            if (dtoItem.Details != null)
            {
                // check for child rows deleted
                foreach (FactoryProformaInvoiceDetail dbDetail in dbItem.FactoryProformaInvoiceDetail.ToArray())
                {
                    if (!dtoItem.Details.Select(o => o.FactoryProformaInvoiceDetailID).Contains(dbDetail.FactoryProformaInvoiceDetailID))
                    {
                        dbItem.FactoryProformaInvoiceDetail.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceDetail dtoDetail in dtoItem.Details)
                {
                    FactoryProformaInvoiceDetail dbDetail;
                    if (dtoDetail.FactoryProformaInvoiceDetailID <= 0)
                    {
                        dbDetail = new FactoryProformaInvoiceDetail();
                        dbItem.FactoryProformaInvoiceDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryProformaInvoiceDetail.FirstOrDefault(o => o.FactoryProformaInvoiceDetailID == dtoDetail.FactoryProformaInvoiceDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceDetail, FactoryProformaInvoiceDetail>(dtoDetail, dbDetail);
                    }
                }
            }
        }
    }
}
