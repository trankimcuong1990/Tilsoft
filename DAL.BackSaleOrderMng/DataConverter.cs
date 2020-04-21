using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.BackSaleOrderMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "BackSaleOrderMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<BackSaleOrderMng.BackSaleOrderMng_SaleOrderSearch_View, DTO.BackSaleOrderMng.SaleOrderSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PIReceivedDate, o => o.ResolveUsing(s => s.PIReceivedDate.HasValue ? s.PIReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DeliveryDate, o => o.ResolveUsing(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DeliveryDateInternal, o => o.ResolveUsing(s => s.DeliveryDateInternal.HasValue ? s.DeliveryDateInternal.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackSaleOrderMng.BackSaleOrderMng_GoodsRemaining_View, DTO.BackSaleOrderMng.GoodsRemaining>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackSaleOrderMng.BackSaleOrderMng_BackOrderDetail_View, DTO.BackSaleOrderMng.BackOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackSaleOrderMng_BackOrder_View, DTO.BackSaleOrderMng.BackOrder>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.BackOrderDetails, o => o.MapFrom(s => s.BackSaleOrderMng_BackOrderDetail_View))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.BackSaleOrderMng.SaleOrderSearch> DB2DTO_SaleOrderSearch(List<BackSaleOrderMng_SaleOrderSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BackSaleOrderMng_SaleOrderSearch_View>, List<DTO.BackSaleOrderMng.SaleOrderSearch>>(dbItems);
        }
        public List<DTO.BackSaleOrderMng.GoodsRemaining> DB2DTO_GoodsRemaining(List<BackSaleOrderMng_GoodsRemaining_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BackSaleOrderMng_GoodsRemaining_View>, List<DTO.BackSaleOrderMng.GoodsRemaining>>(dbItems);
        }
        public DTO.BackSaleOrderMng.BackOrder DB2DTO_BackOrder(BackSaleOrderMng_BackOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<BackSaleOrderMng_BackOrder_View, DTO.BackSaleOrderMng.BackOrder>(dbItem);
        }
        
    }
}
