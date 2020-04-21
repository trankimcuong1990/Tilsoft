using System.Collections.Generic;
using AutoMapper;
using DALBase;
using DTO.RAP;
namespace DAL.RAP
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "RAP";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                Mapper.CreateMap<RAP_View, RapDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<RapDto> DB2DTO_RAP(List<RAP_View> dbItems)
        {
            return Mapper.Map<List<RAP_View>, List<RapDto>>(dbItems);
        }

    }
}
