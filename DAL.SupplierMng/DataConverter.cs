using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.SupplierMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SupplierMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SupplierMng_SupplierSearchResult_View, DTO.SupplierMng.SupplierSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupplierMng_Factory_View, DTO.SupplierMng.Factory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupplierMng_Supplier_View, DTO.SupplierMng.Supplier>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Factories, o => o.MapFrom(s => s.SupplierMng_Factory_View))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SupplierMng.Supplier, Supplier>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SupplierID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SupplierMng.SupplierSearchResult> DB2DTO_SupplierSearchResultList(List<SupplierMng_SupplierSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupplierMng_SupplierSearchResult_View>, List<DTO.SupplierMng.SupplierSearchResult>>(dbItems);
        }

        public DTO.SupplierMng.Supplier DB2DTO_Supplier(SupplierMng_Supplier_View dbItem)
        {
            DTO.SupplierMng.Supplier dtoItem = AutoMapper.Mapper.Map<SupplierMng_Supplier_View, DTO.SupplierMng.Supplier>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);
            return dtoItem;
        }

        public void DTO2BD(DTO.SupplierMng.Supplier dtoItem, ref Supplier dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SupplierMng.Supplier, Supplier>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
