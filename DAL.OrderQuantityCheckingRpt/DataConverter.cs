using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.OrderQuantityCheckingRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "OrderQuantityCheckingRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                Mapper.CreateMap<OrderQuantityCheckingRpt_SaleOrderDetail_View, DTO.OrderQuantityCheckingRpt.SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d=>d.FactoryOrderDetails, o=>o.MapFrom(s=>s.OrderQuantityCheckingRpt_FactoryOrderDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<OrderQuantityCheckingRpt_SaleOrderDetailSparepart_View, DTO.OrderQuantityCheckingRpt.SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryOrderDetails, o => o.MapFrom(s => s.OrderQuantityCheckingRpt_FactoryOrderSparepartDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<OrderQuantityCheckingRpt_FactoryOrderDetail_View, DTO.OrderQuantityCheckingRpt.FactoryOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<OrderQuantityCheckingRpt_FactoryOrderSparepartDetail_View, DTO.OrderQuantityCheckingRpt.FactoryOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.OrderQuantityCheckingRpt.SaleOrder> DB2DTO_SaleOrderDetail2SaleOrder(List<OrderQuantityCheckingRpt_SaleOrderDetail_View> dbItems)
        {
            return Mapper.Map<List<OrderQuantityCheckingRpt_SaleOrderDetail_View>, List<DTO.OrderQuantityCheckingRpt.SaleOrder>>(dbItems);
        }

        public List<DTO.OrderQuantityCheckingRpt.SaleOrder> DB2DTO_SaleOrderSparepartDetail2SaleOrder(List<OrderQuantityCheckingRpt_SaleOrderDetailSparepart_View> dbItems)
        {
            return Mapper.Map<List<OrderQuantityCheckingRpt_SaleOrderDetailSparepart_View>, List<DTO.OrderQuantityCheckingRpt.SaleOrder>>(dbItems);
        }
    }
}
