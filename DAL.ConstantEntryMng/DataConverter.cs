using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ConstantEntryMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ConstantEntryMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ConstantEntryMng_ConstantEntry_SearchResultView, DTO.ConstantEntryMng.ConstantEntrySearchResults >()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ConstantEntryMng_ConstantEntry_View, DTO.ConstantEntryMng.ConstantEntry >()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ConstantEntryMng_ComboList, DTO.ConstantEntryMng.EntryGroupList>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ConstantEntryMng.ConstantEntry, ConstantEntry >()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConstantEntryID , o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ConstantEntryMng.ConstantEntrySearchResults> DB2DTO_ConstantEntrySearchResultList(List<ConstantEntryMng_ConstantEntry_SearchResultView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ConstantEntryMng_ConstantEntry_SearchResultView>, List<DTO.ConstantEntryMng.ConstantEntrySearchResults>>(dbItems);
        }

        public DTO.ConstantEntryMng.ConstantEntry DB2DTO_ConstantEntry(ConstantEntryMng_ConstantEntry_View dbItem)
        {
         
            return AutoMapper.Mapper.Map<ConstantEntryMng_ConstantEntry_View, DTO.ConstantEntryMng.ConstantEntry>(dbItem);
        }

        public List<DTO.ConstantEntryMng.EntryGroupList> DB2DTO_EntryGroups(List<ConstantEntryMng_ComboList> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ConstantEntryMng_ComboList>, List<DTO.ConstantEntryMng.EntryGroupList>>(dbItem);
        }


    
        public void DTO2BD_ConstantEntry(DTO.ConstantEntryMng.ConstantEntry dtoItem, ref ConstantEntry dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ConstantEntryMng.ConstantEntry, ConstantEntry>(dtoItem, dbItem);
        }
    }
}
