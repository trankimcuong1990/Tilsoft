using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RAPVNRpt.DAL
{
    public class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataConverter()
        {
            string mapName = "RAPVNRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //Search View
                AutoMapper.Mapper.CreateMap<RAPVNRpt_Order_View, DTO.OrderData>()
                    .IgnoreAllNonExisting();
                //.ForMember(d => d.DeliveryDate, o => o.ResolveUsing(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.ToString("dd/MM/yyyy") : ""))
                //.ForMember(d => d.FactoryOrderDate, o => o.ResolveUsing(s => s.FactoryOrderDate.HasValue ? s.FactoryOrderDate.Value.ToString("dd/MM/yyyy") : ""))
                //.ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<RAPVNRpt_Loaded_View, DTO.LoadedData>()
                .IgnoreAllNonExisting();
                //.ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<RAPVNRpt_Shipped_View, DTO.ShippedData>()
                .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<RAPVNRpt_WorkOrder_View, DTO.WorkOrderData>()
                .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.OrderData> DTO2DB_SearchOrder(List<RAPVNRpt_Order_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<RAPVNRpt_Order_View>, List<DTO.OrderData>>(dbItem);
        }

        public List<DTO.LoadedData> DTO2DB_SearchLoaded(List<RAPVNRpt_Loaded_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<RAPVNRpt_Loaded_View>, List<DTO.LoadedData>>(dbItem);
        }

        public List<DTO.ShippedData> DTO2DB_SearchShipped(List<RAPVNRpt_Shipped_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<RAPVNRpt_Shipped_View>, List<DTO.ShippedData>>(dbItem);
        }

        public List<DTO.WorkOrderData> DTO2DB_SearchWorkOrder(List<RAPVNRpt_WorkOrder_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<RAPVNRpt_WorkOrder_View>, List<DTO.WorkOrderData>>(dbItem);
        }
    }
}
