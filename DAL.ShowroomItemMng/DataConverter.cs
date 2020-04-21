using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ShowroomItemMng
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = "ShowroomItemMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ShowroomItemMng_ShowroomItem_SearchView, DTO.ShowroomItemMng.ShowroomItemSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomItemMng_ShowroomItem_View, DTO.ShowroomItemMng.ShowroomItem>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.ShowroomItemThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomItemThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ShowroomItemThumbnailImage)))
                  .ForMember(d => d.ShowroomItemFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomItemFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ShowroomItemFullImage)))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ShowroomItemMng.ShowroomItem, ShowroomItem>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ShowroomItemID, o => o.Ignore())
                   .ForMember(d => d.CreatedDate, o => o.Ignore())
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<ShowroomItem_SampleProduct_View, DTO.ShowroomItemMng.SampleProduct>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.ThumbnailRefImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailRefImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ThumbnailRefImage)))
                 .ForMember(d => d.FileRefImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileRefImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileRefImage)))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.ShowroomItemMng.ShowroomItemSearch> DB2DTO_ShowroomItemSearch(List<ShowroomItemMng_ShowroomItem_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ShowroomItemMng_ShowroomItem_SearchView>, List<DTO.ShowroomItemMng.ShowroomItemSearch>>(dbItems);
        }

        public DTO.ShowroomItemMng.ShowroomItem DB2DTO_ShowroomItem(ShowroomItemMng_ShowroomItem_View dbItem)
        {
            return AutoMapper.Mapper.Map<ShowroomItemMng_ShowroomItem_View, DTO.ShowroomItemMng.ShowroomItem>(dbItem);
        }

        public void DTO2DB_ShowroomItem(DTO.ShowroomItemMng.ShowroomItem dtoItem, ref ShowroomItem dbItem, string tempFolder)
        {
            if (dtoItem.ImageFile_HasChange.HasValue && dtoItem.ImageFile_HasChange.Value)
            {
                dtoItem.ImageFile = fwFactory.CreateFilePointer(tempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
            }
            AutoMapper.Mapper.Map<DTO.ShowroomItemMng.ShowroomItem, ShowroomItem>(dtoItem, dbItem);
            if (dtoItem.ShowroomItemID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
        }

        public List<DTO.ShowroomItemMng.SampleProduct> DB2DTO_SampleProduct(List<ShowroomItem_SampleProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ShowroomItem_SampleProduct_View>, List<DTO.ShowroomItemMng.SampleProduct>>(dbItems);
        }
    }
}
