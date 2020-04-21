using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.StandardCostPriceOverviewRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            return;


            AutoMapper.Mapper.CreateMap<StandardCostPriceOverviewRpt_ProductSearchResult_View, DTO.ProductSearchResultDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<StandardCostPriceOverviewRpt_Detail_View, DTO.StandardCostPriceDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.ProductItem, Product>()
                .IgnoreAllNonExisting()
                .ForMember(o => o.ProductID, n => n.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<Product, DTO.ProductItem>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.ProductPrice, Product>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ProductID, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<Product, DTO.ProductPrice>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));


            FrameworkSetting.Setting.Maps.Add(mapName);

        }


        public List<DTO.ProductSearchResultDto> BD2DTO_StandardCostPriceOverviewRptSearch(List<StandardCostPriceOverviewRpt_ProductSearchResult_View> items)
        {
            return AutoMapper.Mapper.Map<List<StandardCostPriceOverviewRpt_ProductSearchResult_View>, List<DTO.ProductSearchResultDto>>(items);
        }

        public List<DTO.StandardCostPriceDetail> DB2DTO_StandardCostPrice(List<StandardCostPriceOverviewRpt_Detail_View> items)
        {
            return AutoMapper.Mapper.Map<List<StandardCostPriceOverviewRpt_Detail_View>, List<DTO.StandardCostPriceDetail>>(items);
        }

        public DTO.ProductItem DB2DTO_Product(Product item)
        {
            return AutoMapper.Mapper.Map<Product, DTO.ProductItem>(item);
        }

        public void DTO2DB_Product(DTO.ProductPrice dtoItem, ref Product dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductPrice, Product>(dtoItem, dbItem);
            dbItem.CostPrice = dtoItem.CostPrice;
        }
    }
}
