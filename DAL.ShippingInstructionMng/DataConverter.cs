using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ShippingInstructionMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ShippingInstructionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ShippingInstructionMng_ShippingInstructionSearchResult_View, DTO.ShippingInstructionMng.ShippingInstructionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShippingInstructionMng_ShippingInstruction_View, DTO.ShippingInstructionMng.ShippingInstruction>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ShippingInstructionMng.ShippingInstruction, ShippingInstruction>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.IsConfirmed, o => o.Ignore())
                    .ForMember(d => d.ClientPONo, o => o.Ignore())
                    .ForMember(d => d.ShippingInstructionID, o => o.Ignore());
                
                AutoMapper.Mapper.CreateMap<ShippingInstructionMng_PODCountry_View, DTO.ShippingInstructionMng.PODCountry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ShippingInstructionMng.ShippingInstructionSearchResult> DB2DTO_ShippingInstructionSearchResultList(List<ShippingInstructionMng_ShippingInstructionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ShippingInstructionMng_ShippingInstructionSearchResult_View>, List<DTO.ShippingInstructionMng.ShippingInstructionSearchResult>>(dbItems);
        }

        public DTO.ShippingInstructionMng.ShippingInstruction DB2DTO_ShippingInstruction(ShippingInstructionMng_ShippingInstruction_View dbItem)
        {
            return AutoMapper.Mapper.Map<ShippingInstructionMng_ShippingInstruction_View, DTO.ShippingInstructionMng.ShippingInstruction>(dbItem);
        }

        public DTO.ShippingInstructionMng.PODCountry DB2DTO_PODCountry(ShippingInstructionMng_PODCountry_View dbItem)
        {
            return AutoMapper.Mapper.Map<ShippingInstructionMng_PODCountry_View, DTO.ShippingInstructionMng.PODCountry>(dbItem);
        }

        public void DTO2DB(DTO.ShippingInstructionMng.ShippingInstruction dtoItem, ref ShippingInstruction dbItem)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.ShippingInstructionMng.ShippingInstruction, ShippingInstruction>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
