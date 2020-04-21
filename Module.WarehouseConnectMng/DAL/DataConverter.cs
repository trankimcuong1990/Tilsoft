using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.WarehouseConnectMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<WarehouseConnectMng_Product_View, DTO.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseConnectMng_Gallery_View, DTO.ProductMediaDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.ProductDTO DB2DTO_Product(WarehouseConnectMng_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<WarehouseConnectMng_Product_View, DTO.ProductDTO>(dbItem);
        }

        public List<DTO.ProductDTO> DB2DTO_ProductList(List<WarehouseConnectMng_Product_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WarehouseConnectMng_Product_View>, List<DTO.ProductDTO>>(dbItem);
        }

        public DTO.ProductMediaDTO DB2DTO_Media(WarehouseConnectMng_Gallery_View dbItem)
        {
            return AutoMapper.Mapper.Map<WarehouseConnectMng_Gallery_View, DTO.ProductMediaDTO>(dbItem);
        }
    }
}
