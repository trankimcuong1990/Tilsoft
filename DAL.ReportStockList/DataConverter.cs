using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ReportStockList
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ReportStockListMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ReportMng_StockList_View, DTO.ReportStockList.StockList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SelectedThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SelectedThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.SelectedThumbnailImage)))
                    .ForMember(d => d.SelectedFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SelectedFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.SelectedFullImage)))
                    .ForMember(d => d.CushionImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.CushionImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.CushionImage)))
                    .ForMember(d => d.MaterialImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.MaterialImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.MaterialImage)))

                    .ForMember(d => d.ProductThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ProductThumbnailImage)))
                    .ForMember(d => d.ProductGardenThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductGardenThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ProductGardenThumbnailImage)))
                    .ForMember(d => d.ModelThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ModelThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ModelThumbnailImage)))
                    .ForMember(d => d.ProductFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ProductFullImage)))
                    .ForMember(d => d.ProductGardenFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductGardenFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ProductGardenFullImage)))
                    .ForMember(d => d.ModelFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ModelFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ModelFullImage)))

                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockList_Reservation_View, DTO.ReportStockList.StockReservation>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockList_SaleOrderDetail_View, DTO.ReportStockList.SaleOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockList_WarehouseImportDetail_View, DTO.ReportStockList.WarehouseImportDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockList_WarehousePickingListDetail_View, DTO.ReportStockList.WarehousePickingListDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockList_LoadingPlanDetail_View, DTO.ReportStockList.LoadingPlanDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockList_ProductSetEANCode_View, DTO.ReportStockList.ProductSetEANCode>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.ReportStockList.StockList> DB2DTO_StockListSearch(List<ReportMng_StockList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockList_View>, List<DTO.ReportStockList.StockList>>(dbItems);
        }

        public List<DTO.ReportStockList.StockReservation> DB2DTO_StockReservation(List<ReportMng_StockList_Reservation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockList_Reservation_View>, List<DTO.ReportStockList.StockReservation>>(dbItems);
        }

        public List<DTO.ReportStockList.SaleOrderDetail> DB2DTO_SaleOrderDetail(List<ReportMng_StockList_SaleOrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockList_SaleOrderDetail_View>, List<DTO.ReportStockList.SaleOrderDetail>>(dbItems);
        }

        public List<DTO.ReportStockList.WarehouseImportDetail> DB2DTO_WarehouseImportDetail(List<ReportMng_StockList_WarehouseImportDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockList_WarehouseImportDetail_View>, List<DTO.ReportStockList.WarehouseImportDetail>>(dbItems);
        }

        public List<DTO.ReportStockList.WarehousePickingListDetail> DB2DTO_WarehousePickingListDetail(List<ReportMng_StockList_WarehousePickingListDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockList_WarehousePickingListDetail_View>, List<DTO.ReportStockList.WarehousePickingListDetail>>(dbItems);
        }

        public List<DTO.ReportStockList.LoadingPlanDetail> DB2DTO_LoadingPlanDetail(List<ReportMng_StockList_LoadingPlanDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockList_LoadingPlanDetail_View>, List<DTO.ReportStockList.LoadingPlanDetail>>(dbItems);
        }

        public List<DTO.ReportStockList.ProductSetEANCode> DB2DTO_ProductSetEANCode(List<ReportMng_StockList_ProductSetEANCode_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockList_ProductSetEANCode_View>, List<DTO.ReportStockList.ProductSetEANCode>>(dbItems);
        }

       

    }
}
