using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummaryPurchasesRpt.DAL
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
            AutoMapper.Mapper.CreateMap <SummaryPurchasesRpt_SupportSubSupplier_View, DTO.SupportSubSupplierData> ()
                .IgnoreAllNonExisting()                
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SummaryPurchaseRpt_ReceivingNote_ReportDataView, DTO.ReceivingNoteReportData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SummaryPurchaseRpt_SupplierOfReceivingNote_View, DTO.SupplierOfReceivingData>()
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
        }
        public List<DTO.SupportSubSupplierData> DB2DTO_GetSubSupplier(List<SummaryPurchasesRpt_SupportSubSupplier_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummaryPurchasesRpt_SupportSubSupplier_View>, List<DTO.SupportSubSupplierData>>(dbItems);
        }

        public List<DTO.ReceivingNoteReportData> DB2DTO_GetReceivingNoteReportData(List<SummaryPurchaseRpt_ReceivingNote_ReportDataView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummaryPurchaseRpt_ReceivingNote_ReportDataView>, List<DTO.ReceivingNoteReportData>>(dbItems);
        }

        public List<DTO.SupplierOfReceivingData> DB2DTO_GetSupplierOfReceivingNote(List<SummaryPurchaseRpt_SupplierOfReceivingNote_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummaryPurchaseRpt_SupplierOfReceivingNote_View>, List<DTO.SupplierOfReceivingData>>(dbItems);
        }
    }
}
