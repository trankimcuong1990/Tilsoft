using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using System.Globalization;

namespace Module.OfferReportRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "OfferReportRpt";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
               
                AutoMapper.Mapper.CreateMap<OfferReportRpt_FOBItemOnly_ReportView, DTO.FOBItemOnlyDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
    }
}
