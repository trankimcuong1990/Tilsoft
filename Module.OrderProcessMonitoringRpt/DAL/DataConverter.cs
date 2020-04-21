using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.OrderProcessMonitoringRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<OrderProcessMonitoringRpt_Offer_View, DTO.OfferDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferDate, o => o.ResolveUsing(s => s.OfferDate.HasValue ? s.OfferDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SaleOrderDTOs, o => o.MapFrom(s => s.OrderProcessMonitoringRpt_SaleOrder_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OrderProcessMonitoringRpt_SaleOrder_View, DTO.SaleOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : string.Empty)))
                    .ForMember(d => d.FactoryOrderDTOs, o => o.MapFrom(s => s.OrderProcessMonitoringRpt_FactoryOrder_View))
                    .ForMember(d => d.LoadingPlanDTOs, o => o.MapFrom(s => s.OrderProcessMonitoringRpt_LoadingPlan_View))
                    .ForMember(d => d.ECommercialInvoiceDTOs, o => o.MapFrom(s => s.OrderProcessMonitoringRpt_ECommercialInvoice_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OrderProcessMonitoringRpt_FactoryOrder_View, DTO.FactoryOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OrderDate, o => o.ResolveUsing(s => s.OrderDate.HasValue ? s.OrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OrderProcessMonitoringRpt_LoadingPlan_View, DTO.LoadingPlanDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))                    
                    .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OrderProcessMonitoringRpt_ECommercialInvoice_View, DTO.ECommercialInvoiceDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.OfferDTO DB2DTO_Offer(OrderProcessMonitoringRpt_Offer_View dbItem)
        {
            return AutoMapper.Mapper.Map<OrderProcessMonitoringRpt_Offer_View, DTO.OfferDTO>(dbItem);
        }
    }
}
