using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.MIFactorySaleRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MIFactorySaleRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MIFactorySaleRpt_FactorySale_View, DTO.FactorySale>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactorySale> DB2DTO_FactorySale(List<MIFactorySaleRpt_FactorySale_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIFactorySaleRpt_FactorySale_View>, List<DTO.FactorySale>>(dbItems);
        }

        
    }
}
