using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ForwarderInvoiceMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ForwarderInvoiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ForwarderInvoiceMng_ForwarderInvoiceSearchResult_View, DTO.ForwarderInvoiceMng.ForwarderInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DueDate, o => o.ResolveUsing(s => s.DueDate.HasValue ? s.DueDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ForwarderInvoiceMng_ForwarderInvoice_View, DTO.ForwarderInvoiceMng.ForwarderInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DueDate, o => o.ResolveUsing(s => s.DueDate.HasValue ? s.DueDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ForwarderInvoiceDetails, o => o.MapFrom(s => s.ForwarderInvoiceMng_ForwarderInvoiceDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ForwarderInvoiceMng_ForwarderInvoiceDetail_View, DTO.ForwarderInvoiceMng.ForwarderInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ForwarderInvoiceMng.ForwarderInvoice, ForwarderInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.DueDate, o => o.Ignore())
                    .ForMember(d => d.ForwarderInvoiceDetails, o => o.Ignore())
                    .ForMember(d => d.ForwarderInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ForwarderInvoiceMng.ForwarderInvoiceDetail, ForwarderInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ForwarderInvoiceID, o => o.Ignore())
                    .ForMember(d => d.ForwarderInvoiceDetailID, o => o.Ignore());
                
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ForwarderInvoiceMng.ForwarderInvoiceSearchResult> DB2DTO_ForwarderInvoiceSearchResultList(List<ForwarderInvoiceMng_ForwarderInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ForwarderInvoiceMng_ForwarderInvoiceSearchResult_View>, List<DTO.ForwarderInvoiceMng.ForwarderInvoiceSearchResult>>(dbItems);
        }

        public DTO.ForwarderInvoiceMng.ForwarderInvoice DB2DTO_ForwarderInvoice(ForwarderInvoiceMng_ForwarderInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<ForwarderInvoiceMng_ForwarderInvoice_View, DTO.ForwarderInvoiceMng.ForwarderInvoice>(dbItem);
        }

        public void DTO2BD(DTO.ForwarderInvoiceMng.ForwarderInvoice dtoItem, ref ForwarderInvoice dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ForwarderInvoiceMng.ForwarderInvoice, ForwarderInvoice>(dtoItem, dbItem);
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

            if (!string.IsNullOrEmpty(dtoItem.DueDate))
            {
                try
                {
                    dbItem.DueDate = DateTime.ParseExact(dtoItem.DueDate, "d", new System.Globalization.CultureInfo("vi-VN"));
                }
                catch
                {
                    dbItem.DueDate = null;
                }
            }

             //map pieces
            if (dtoItem.ForwarderInvoiceDetails != null)
            {
                // check for child rows deleted
                foreach (ForwarderInvoiceDetail dbDetail in dbItem.ForwarderInvoiceDetails.ToArray())
                {
                    if (!dtoItem.ForwarderInvoiceDetails.Select(o => o.ForwarderInvoiceDetailID).Contains(dbDetail.ForwarderInvoiceDetailID))
                    {
                        dbItem.ForwarderInvoiceDetails.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.ForwarderInvoiceMng.ForwarderInvoiceDetail dtoDetail in dtoItem.ForwarderInvoiceDetails)
                {
                    ForwarderInvoiceDetail dbDetail;
                    if (dtoDetail.ForwarderInvoiceDetailID <= 0)
                    {
                        dbDetail = new ForwarderInvoiceDetail();
                        dbItem.ForwarderInvoiceDetails.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.ForwarderInvoiceDetails.FirstOrDefault(o => o.ForwarderInvoiceDetailID == dtoDetail.ForwarderInvoiceDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ForwarderInvoiceMng.ForwarderInvoiceDetail, ForwarderInvoiceDetail>(dtoDetail, dbDetail);
                    }
                }
            }
        }
    }
}
