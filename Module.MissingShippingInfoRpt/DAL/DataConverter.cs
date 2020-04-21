using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;


namespace Module.MissingShippingInfoRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MissingShippingInfoRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MissingShippingInfoRpt_MissingShippingInfo_View, DTO.MissingShippingInfo>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PIReceivedDate, o => o.ResolveUsing(s => s.PIReceivedDate.HasValue ? s.PIReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MissingShippingInfo> DB2DTO_MissingShippingInfoList(List<MissingShippingInfoRpt_MissingShippingInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MissingShippingInfoRpt_MissingShippingInfo_View>, List<DTO.MissingShippingInfo>>(dbItems);
        }
    }
}
