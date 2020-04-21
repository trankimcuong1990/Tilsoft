using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.AVFPurchasingInvoiceMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AVFPurchasingInvoiceMng_AVFPurchasingInvoiceSearchResult_View, DTO.AVFPurchasingInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVFPurchasingInvoiceMng_AVFPurchasingInvoice_View, DTO.AVFPurchasingInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.AVFPurchasingInvoiceDetails, o => o.MapFrom(s => s.AVFPurchasingInvoiceMng_AVFPurchasingInvoiceDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AVFPurchasingInvoice, AVFPurchasingInvoice>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.DocumentDate, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.AVFPurchasingInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.AVFPurchasingInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<AVFPurchasingInvoiceMng_AVFPurchasingInvoiceDetail_View, DTO.AVFPurchasingInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AVFPurchasingInvoiceDetail, AVFPurchasingInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.AVFPurchasingInvoiceDetailID, o => o.Ignore())
                    .ForMember(d => d.AVFPurchasingInvoiceID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.AVFPurchasingInvoiceSearchResult> DB2DTO_AVFPurchasingInvoiceSearchResultList(List<AVFPurchasingInvoiceMng_AVFPurchasingInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVFPurchasingInvoiceMng_AVFPurchasingInvoiceSearchResult_View>, List<DTO.AVFPurchasingInvoiceSearchResult>>(dbItems);
        }
        public DTO.AVFPurchasingInvoice DB2DTO_AVFPurchasingInvoice(AVFPurchasingInvoiceMng_AVFPurchasingInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<AVFPurchasingInvoiceMng_AVFPurchasingInvoice_View, DTO.AVFPurchasingInvoice>(dbItem);
        }
        public void DTO2BD(DTO.AVFPurchasingInvoice dtoItem, ref AVFPurchasingInvoice dbItem)
        {
            dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
            dbItem.DocumentDate = dtoItem.DocumentDate.ConvertStringToDateTime();

            AutoMapper.Mapper.Map<DTO.AVFPurchasingInvoice, AVFPurchasingInvoice>(dtoItem, dbItem);

            if(dtoItem.AVFPurchasingInvoiceDetails != null)
            {
                //check for child rows deleted
                foreach(AVFPurchasingInvoiceDetail dbDetail in dbItem.AVFPurchasingInvoiceDetail.ToArray())
                {
                    if(!dtoItem.AVFPurchasingInvoiceDetails.Select(o => o.AVFPurchasingInvoiceDetailID).Contains(dbDetail.AVFPurchasingInvoiceDetailID))
                    {
                        dbItem.AVFPurchasingInvoiceDetail.Remove(dbDetail);
                    }
                }

                //map child row
                foreach(DTO.AVFPurchasingInvoiceDetail dtoDetail in dtoItem.AVFPurchasingInvoiceDetails)
                {
                    AVFPurchasingInvoiceDetail dbDetail;
                    if (dtoDetail.AVFPurchasingInvoiceDetailID <= 0)
                    {
                        dbDetail = new AVFPurchasingInvoiceDetail();
                        dbItem.AVFPurchasingInvoiceDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.AVFPurchasingInvoiceDetail.FirstOrDefault(o => o.AVFPurchasingInvoiceDetailID == dtoDetail.AVFPurchasingInvoiceDetailID);
                    }

                    if(dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.AVFPurchasingInvoiceDetail, AVFPurchasingInvoiceDetail>(dtoDetail, dbDetail);
                    }
                }
                AutoMapper.Mapper.Map<DTO.AVFPurchasingInvoice, AVFPurchasingInvoice>(dtoItem, dbItem);
            }
        }
    }
}
