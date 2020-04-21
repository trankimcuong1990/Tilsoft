using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using AutoMapper;
using Library;

namespace Module.MRPRpt.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MRP_ProductionItemSerach_View, DTO.MRPSearchFormData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MRP_ProductionItem_View, DTO.PoductionItemMRP>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.purchaseOrderDetailMRPs, m => m.MapFrom(v => v.MRP_PurchaseOrderDetail_View))
                    .ForMember(d => d.purchaseRequestDetailMRPs, m => m.MapFrom(v => v.MRP_PurchaseRequestDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MRP_PurchaseOrderDetail_View, DTO.PurchaseOrderDetailMRP>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PlannedETA, o => o.ResolveUsing(s => s.PlannedETA.HasValue ? s.PlannedETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MRP_PurchaseRequestDetail_View, DTO.PurchaseRequestDetailMRP>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MRP_SupportProductionItemGroup_SearchView, DTO.SupportProductionItemGroupSearchView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MRPSearchFormData> DB2DTO_SearchData(List<MRP_ProductionItemSerach_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MRP_ProductionItemSerach_View>, List<DTO.MRPSearchFormData>>(dbItems);
        }

        public DTO.PoductionItemMRP DB2DTO_Getproduction (MRP_ProductionItem_View dbItem)
        {
            return AutoMapper.Mapper.Map<MRP_ProductionItem_View, DTO.PoductionItemMRP>(dbItem);
        }

        public List<DTO.PurchaseOrderDetailMRP> Db2DTO_PurchaseOrder(List<MRP_PurchaseOrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MRP_PurchaseOrderDetail_View>, List<DTO.PurchaseOrderDetailMRP>>(dbItems);
        }

        public List<DTO.PurchaseRequestDetailMRP> DB2DTO_PurchaseRequest(List<MRP_PurchaseRequestDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MRP_PurchaseRequestDetail_View>, List<DTO.PurchaseRequestDetailMRP>>(dbItems);
        }

        public List<DTO.SupportProductionItemGroupSearchView> DB2DTO_SpProductionItemGroup(List<MRP_SupportProductionItemGroup_SearchView> dbItem)
        {
            return AutoMapper.Mapper.Map<List<MRP_SupportProductionItemGroup_SearchView>, List<DTO.SupportProductionItemGroupSearchView>>(dbItem);
        }
    }
}
