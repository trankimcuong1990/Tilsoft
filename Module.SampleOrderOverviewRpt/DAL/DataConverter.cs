using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.SampleOrderOverviewRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<SampleOrderOverviewRpt_Factory_View, Support.DTO.Factory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SampleOrder_View, DTO.SampleOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<Support.DTO.Factory> DB2DTO_Factory(List<SampleOrderOverviewRpt_Factory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SampleOrderOverviewRpt_Factory_View>, List<Support.DTO.Factory>>(dbItems);
        }

        public List<DTO.SampleOrder> DB2DTO_SampleOrder(List<SupportMng_SampleOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleOrder_View>, List<DTO.SampleOrder>>(dbItems);
        }
    }
}
