using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.AVFSupplierMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "AVFSupplierMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AVFSupplierMng_SupplierSearchResult_View, DTO.AVFSupplierSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVFSupplierMng_Supplier_View, DTO.AVFSupplier>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.AVFSupplierPurchasingInvoices, o => o.MapFrom(s => s.AVFSupplierMng_SupplierPurchasingInvoice_View))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AVFSupplier, AVFSupplier>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.AVFSupplierID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<AVFSupplierMng_SupplierPurchasingInvoice_View, DTO.AVFSupplierPurchasingInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.AVFSupplierPurchasingInvoiceDetails, o => o.MapFrom(s => s.AVFSupplierMng_SupplierPurchasingInvoiceDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVFSupplierMng_SupplierPurchasingInvoiceDetail_View, DTO.AVFSupplierPurchasingInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.AVFSupplierSearchResult> DB2DTO_AVFSupplierSearchResultList(List<AVFSupplierMng_SupplierSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVFSupplierMng_SupplierSearchResult_View>, List<DTO.AVFSupplierSearchResult>>(dbItems);
        }

        public DTO.AVFSupplier DB2DTO_AVFSupplier(AVFSupplierMng_Supplier_View dbItem)
        {
            DTO.AVFSupplier dtoItem = AutoMapper.Mapper.Map<AVFSupplierMng_Supplier_View, DTO.AVFSupplier>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);
            return dtoItem;
        }

        public void DTO2BD(DTO.AVFSupplier dtoItem, ref AVFSupplier dbItem)
        {
            AutoMapper.Mapper.Map<DTO.AVFSupplier, AVFSupplier>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }

        public DTO.AVFSupplierPurchasingInvoice DB2DTO_AVFSupplier(AVFSupplierMng_SupplierPurchasingInvoice_View dbItem)
        {
            DTO.AVFSupplierPurchasingInvoice dtoItem = AutoMapper.Mapper.Map<AVFSupplierMng_SupplierPurchasingInvoice_View, DTO.AVFSupplierPurchasingInvoice>(dbItem);
            return dtoItem;
        }

        public DTO.AVFSupplierPurchasingInvoiceDetail DB2DTO_AVFSupplier(AVFSupplierMng_SupplierPurchasingInvoiceDetail_View dbItem)
        {
            DTO.AVFSupplierPurchasingInvoiceDetail dtoItem = AutoMapper.Mapper.Map<AVFSupplierMng_SupplierPurchasingInvoiceDetail_View, DTO.AVFSupplierPurchasingInvoiceDetail>(dbItem);
            return dtoItem;
        }
    }
}
