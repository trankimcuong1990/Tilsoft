using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummaryOutWardRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }        

            AutoMapper.Mapper.CreateMap<SummaryOutWardRpt_SummaryOutWard_View, DTO.SummaryOutWardReportData>()
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
        }      

        public List<DTO.SummaryOutWardReportData> DB2DTO_GetSummaryOutWardReportData(List<SummaryOutWardRpt_SummaryOutWard_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummaryOutWardRpt_SummaryOutWard_View>, List<DTO.SummaryOutWardReportData>>(dbItems);
        }
    }
}
