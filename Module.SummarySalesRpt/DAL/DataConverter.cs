using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummarySalesRpt.DAL
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
            AutoMapper.Mapper.CreateMap<SummarySalesRpt_SupportCustomer_view, DTO.SupportCustomerData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SummarySalesRpt_CustomerReport_View, DTO.CustomerData>()
                .ForMember(d => d.DocumentDate, o => o.ResolveUsing(s => s.DocumentDate.HasValue ? s.DocumentDate.Value.ToString("dd/MM/yyyy") : ""))
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SummarySalesRpt_ProductionItemReport_View, DTO.ProductionItemData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SummarySalesRpt_DeliveryNote_View, DTO.DeliveryNote>()
            .IgnoreAllNonExisting()
            .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.SupportCustomerData> DB2DTO_GetSubSupplier(List<SummarySalesRpt_SupportCustomer_view> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummarySalesRpt_SupportCustomer_view>, List<DTO.SupportCustomerData>>(dbItems);
        }

        public List<DTO.CustomerData> DB2DTO_GetCustomerDatas(List<SummarySalesRpt_CustomerReport_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummarySalesRpt_CustomerReport_View>, List<DTO.CustomerData>>(dbItems);
        }

        public List<DTO.ProductionItemData> DB2DTO_GetProductionItemDatas(List<SummarySalesRpt_ProductionItemReport_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummarySalesRpt_ProductionItemReport_View>, List<DTO.ProductionItemData>>(dbItems);
        }
        public List<DTO.DeliveryNote> DB2DTO_GetDeliveryNotes(List<SummarySalesRpt_DeliveryNote_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SummarySalesRpt_DeliveryNote_View>, List<DTO.DeliveryNote>>(dbItems);
        }
    }
}
