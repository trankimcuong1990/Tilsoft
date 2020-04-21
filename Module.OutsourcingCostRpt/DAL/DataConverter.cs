using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.OutsourcingCostRpt.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwfactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<OutsourcingCostRpt_Function_GetReport_Result, DTO.ReportDataDTO>()
                    .IgnoreAllNonExisting()                    
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingCostRpt_Function_GetReportDetail_Result, DTO.ReportDataDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductionTeam_View, Support.DTO.ProductionTeam>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        //---------//
        public List<DTO.ReportDataDTO> DB2DTO_ReportView (List<OutsourcingCostRpt_Function_GetReport_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingCostRpt_Function_GetReport_Result>, List<DTO.ReportDataDTO>>(dbItems);
        }

    }
}
