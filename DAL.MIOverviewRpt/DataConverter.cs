using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MIOverviewRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MIOverviewRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MIOverviewRpt_DDC_View, DTO.MIOverviewRpt.DDC>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MIOverviewRpt_Profoma_View, DTO.MIOverviewRpt.Proforma>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MIOverviewRpt_Production_View, DTO.MIOverviewRpt.Production>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MIOverviewRpt.DDC> DB2DTO_DDCList(List<MIOverviewRpt_DDC_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIOverviewRpt_DDC_View>, List<DTO.MIOverviewRpt.DDC>>(dbItems);
        }

        public List<DTO.MIOverviewRpt.Proforma> DB2DTO_ProformaList(List<MIOverviewRpt_Profoma_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIOverviewRpt_Profoma_View>, List<DTO.MIOverviewRpt.Proforma>>(dbItems);
        }

        public List<DTO.MIOverviewRpt.Production> DB2DTO_ProductionList(List<MIOverviewRpt_Production_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIOverviewRpt_Production_View>, List<DTO.MIOverviewRpt.Production>>(dbItems);
        }
    }
}
