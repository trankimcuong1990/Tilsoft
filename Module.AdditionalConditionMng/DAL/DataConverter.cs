using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.AdditionalConditionMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AdditionalConditionMng_AdditionalCondition_View, DTO.AdditionalConditionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AdditionalConditionDTO, AdditionalCondition>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Support_AdditionalCondition_View, DTO.AdditionalConditionTypeDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AdditionalConditionMng_AdditionalConditionSearch_View, DTO.AdditionalConditionSearch>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public DTO.AdditionalConditionDTO DB2DTO_AdditionalConditionDTO(AdditionalConditionMng_AdditionalCondition_View dbItem)
        {
            return AutoMapper.Mapper.Map<AdditionalConditionMng_AdditionalCondition_View, DTO.AdditionalConditionDTO>(dbItem);
        }
        public void DTO2DB_AdditionalCondition(DTO.AdditionalConditionDTO dto, ref AdditionalCondition db)
        {
            AutoMapper.Mapper.Map<DTO.AdditionalConditionDTO, AdditionalCondition>(dto, db);
        }
        public List<DTO.AdditionalConditionSearch> BD2DTO_AdditionalConditionSearchResult(List<AdditionalConditionMng_AdditionalConditionSearch_View> items)
        {
            return AutoMapper.Mapper.Map<List<AdditionalConditionMng_AdditionalConditionSearch_View>, List<DTO.AdditionalConditionSearch>>(items);
        }
        public List<DTO.AdditionalConditionTypeDTO> DB2DTO_Support_AdditionalConditionTypeDTO(List<Support_AdditionalCondition_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Support_AdditionalCondition_View>, List<DTO.AdditionalConditionTypeDTO>>(dbItems);
        }
    }
}
