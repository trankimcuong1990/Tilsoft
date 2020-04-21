using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.FactoryMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryMng_FactorySearchResult_View, DTO.FactoryMng.FactorySearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng_Factory_View, DTO.FactoryMng.Factory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryMng.Factory, Factory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryMng.FactorySearchResult> DB2DTO_FactorySearchResultList(List<FactoryMng_FactorySearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng_FactorySearchResult_View>, List<DTO.FactoryMng.FactorySearchResult>>(dbItems);
        }

        public DTO.FactoryMng.Factory DB2DTO_Material(FactoryMng_Factory_View dbItem)
        {
            DTO.FactoryMng.Factory dtoItem =  AutoMapper.Mapper.Map<FactoryMng_Factory_View, DTO.FactoryMng.Factory>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);

            return dtoItem;
        }

        public void DTO2BD_Material(DTO.FactoryMng.Factory dtoItem, ref Factory dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryMng.Factory, Factory>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
