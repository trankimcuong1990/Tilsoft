using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ReportDDC
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DDCReport";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DDCRpt_ClientInfo_View, DTO.DDCReport.ClientInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DDCReport.ClientInfo> DB2DTO_ClientInfo(List<DDCRpt_ClientInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DDCRpt_ClientInfo_View>, List<DTO.DDCReport.ClientInfo>>(dbItems);
        }
        
    }
}
