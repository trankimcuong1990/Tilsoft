using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ReportStockOverview
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ReportStockOverviewMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ReportMng_StockOverview_View, DTO.ReportStockOverview.StockOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ImageFile) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ImageFile)))
                    .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductFileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ProductFileLocation)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockOverview_PhysicalStock_View, DTO.ReportStockOverview.PhysicalStockByArea>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_StockOverview_ProductImportRemark_View, DTO.ReportStockOverview.ProductImportRemark>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ReportStockOverview.StockOverview> DB2DTO_StockOverview(List<ReportMng_StockOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockOverview_View>, List<DTO.ReportStockOverview.StockOverview>>(dbItems);
        }
        public List<DTO.ReportStockOverview.PhysicalStockByArea> DB2DTO_PhysicalStockByArea(List<ReportMng_StockOverview_PhysicalStock_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockOverview_PhysicalStock_View>, List<DTO.ReportStockOverview.PhysicalStockByArea>>(dbItems);
        }
        public List<DTO.ReportStockOverview.ProductImportRemark> DB2DTO_ImportRemark(List<ReportMng_StockOverview_ProductImportRemark_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_StockOverview_ProductImportRemark_View>, List<DTO.ReportStockOverview.ProductImportRemark>>(dbItems);
        }
    }
}
