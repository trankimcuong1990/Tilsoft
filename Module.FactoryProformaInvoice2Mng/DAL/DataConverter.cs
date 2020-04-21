using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.FactoryProformaInvoice2Mng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "FactoryProformaInvoice2Mng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryProformaInvoice2Mng_FactoryProformaInvoiceSearchResult_View, DTO.FactoryProformaInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FactoryConfirmedDate, o => o.ResolveUsing(s => s.FactoryConfirmedDate.HasValue ? s.FactoryConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FurnindoConfirmedDate, o => o.ResolveUsing(s => s.FurnindoConfirmedDate.HasValue ? s.FurnindoConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProformaInvoice2Mng_FactoryProformaInvoice_View, DTO.FactoryProformaInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FactoryConfirmedDate, o => o.ResolveUsing(s => s.FactoryConfirmedDate.HasValue ? s.FactoryConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FurnindoConfirmedDate, o => o.ResolveUsing(s => s.FurnindoConfirmedDate.HasValue ? s.FurnindoConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.FactoryProformaInvoiceDetails, o => o.MapFrom(s => s.FactoryProformaInvoice2Mng_FactoryProformaInvoiceDetail_View))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProformaInvoice2Mng_FactoryProformaInvoiceDetail_View, DTO.FactoryProformaInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProformaInvoice2Mng_FactoryOrderItemSearchResult_View, DTO.FactoryOrderItemSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryProformaInvoice, FactoryProformaInvoice2>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryProformaInvoiceID, o => o.Ignore())
                    .ForMember(d => d.ProformaInvoiceNo, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.IsFurnindoConfirmed, o => o.Ignore())
                    .ForMember(d => d.FurnindoConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.FurnindoConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.IsFactoryConfirmed, o => o.Ignore())
                    .ForMember(d => d.FactoryConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.FactoryConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryProformaInvoiceDetail2, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryProformaInvoiceDetail, FactoryProformaInvoiceDetail2>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryProformaInvoiceID, o => o.Ignore())
                    .ForMember(d => d.FactoryProformaInvoiceDetailID, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryProformaInvoiceSearchResult> DB2DTO_FactoryProformaInvoiceSearchResultList(List<FactoryProformaInvoice2Mng_FactoryProformaInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProformaInvoice2Mng_FactoryProformaInvoiceSearchResult_View>, List<DTO.FactoryProformaInvoiceSearchResult>>(dbItems);
        }

        public List<DTO.FactoryOrderItemSearchResult> DB2DTO_FactoryOrderItemSearchResultList(List<FactoryProformaInvoice2Mng_FactoryOrderItemSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProformaInvoice2Mng_FactoryOrderItemSearchResult_View>, List<DTO.FactoryOrderItemSearchResult>>(dbItems);
        }

        public DTO.FactoryProformaInvoice DB2DTO_FactoryProformaInvoice(FactoryProformaInvoice2Mng_FactoryProformaInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryProformaInvoice2Mng_FactoryProformaInvoice_View, DTO.FactoryProformaInvoice>(dbItem);
        }

        public void DTO2DB(DTO.FactoryProformaInvoice dtoItem, ref FactoryProformaInvoice2 dbItem, string _tempFolder, bool ignoreDetail)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.FactoryProformaInvoice, FactoryProformaInvoice2>(dtoItem, dbItem);
            if (dtoItem.AttachedFile_HasChanged)
            {
                dbItem.AttachedFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoItem.AttachedFile_NewFile, dtoItem.AttachedFile);
            }
            if (!string.IsNullOrEmpty(dtoItem.InvoiceDate))
            {
                if (DateTime.TryParse(dtoItem.InvoiceDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.InvoiceDate = tmpDate;
                }
            }

            // map detail
            if (dtoItem.FactoryProformaInvoiceDetails != null && !ignoreDetail)
            {
                // check for child rows deleted
                foreach (FactoryProformaInvoiceDetail2 dbDetail in dbItem.FactoryProformaInvoiceDetail2.ToArray())
                {
                    if (!dtoItem.FactoryProformaInvoiceDetails.Select(o => o.FactoryProformaInvoiceDetailID).Contains(dbDetail.FactoryProformaInvoiceDetailID))
                    {
                        dbItem.FactoryProformaInvoiceDetail2.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.FactoryProformaInvoiceDetail dtoDetail in dtoItem.FactoryProformaInvoiceDetails)
                {
                    FactoryProformaInvoiceDetail2 dbDetail;
                    if (dtoDetail.FactoryProformaInvoiceDetailID <= 0)
                    {
                        dbDetail = new FactoryProformaInvoiceDetail2();
                        dbItem.FactoryProformaInvoiceDetail2.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryProformaInvoiceDetail2.FirstOrDefault(o => o.FactoryProformaInvoiceDetailID == dtoDetail.FactoryProformaInvoiceDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryProformaInvoiceDetail, FactoryProformaInvoiceDetail2>(dtoDetail, dbDetail);
                        if (!string.IsNullOrEmpty(dtoDetail.UpdatedDate))
                        {
                            if (DateTime.TryParse(dtoDetail.UpdatedDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                            {
                                dbDetail.UpdatedDate = tmpDate;
                            }
                        }
                    }
                }
            }
        }
    }
}
