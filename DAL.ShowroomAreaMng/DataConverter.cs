using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ShowroomAreaMng
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = "ShowroomAreaMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ShowroomAreaMng_ShowroomArea_SearchView, DTO.ShowroomAreaMng.ShowroomAreaSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomAreaMng_ShowroomArea_View, DTO.ShowroomAreaMng.ShowroomArea>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ShowroomAreaImages, o => o.MapFrom(s => s.ShowroomAreaMng_ShowroomAreaImage_View))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomAreaMng_ShowroomAreaImage_View, DTO.ShowroomAreaMng.ShowroomAreaImage>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ShowroomAreaThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomAreaThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ShowroomAreaThumbnailImage)))
                  .ForMember(d => d.ShowroomAreaFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomAreaFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ShowroomAreaFullImage)))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ShowroomAreaMng.ShowroomArea, ShowroomArea>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ShowroomAreaImage, o => o.Ignore())
                   .ForMember(d => d.ShowroomAreaID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.ShowroomAreaMng.ShowroomAreaImage,ShowroomAreaImage>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ShowroomAreaImageID, o => o.Ignore())
                  ;

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ShowroomAreaMng.ShowroomAreaSearchResult> DB2DTO_ShowroomAreaSearch(List<ShowroomAreaMng_ShowroomArea_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ShowroomAreaMng_ShowroomArea_SearchView>, List<DTO.ShowroomAreaMng.ShowroomAreaSearchResult>>(dbItems);
        }

        public DTO.ShowroomAreaMng.ShowroomArea DB2DTO_ShowroomArea(ShowroomAreaMng_ShowroomArea_View dbItem)
        {
            return AutoMapper.Mapper.Map<ShowroomAreaMng_ShowroomArea_View, DTO.ShowroomAreaMng.ShowroomArea>(dbItem);
        }

        public void DTO2DB_ShowroomArea(DTO.ShowroomAreaMng.ShowroomArea dtoItem, ref ShowroomArea dbItem, string tempFolder)
        {
            List<ShowroomAreaImage> image_tobedeleted = new List<ShowroomAreaImage>();

            if (dtoItem.ShowroomAreaImages != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.ShowroomAreaImage.Where(o => !dtoItem.ShowroomAreaImages.Select(s => s.ShowroomAreaImageID).Contains(o.ShowroomAreaImageID)))
                {
                    image_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in image_tobedeleted)
                {
                    dbItem.ShowroomAreaImage.Remove(dbDetail);
                }
                //MAP
                foreach (var item in dtoItem.ShowroomAreaImages)
                {
                    if (item.ImageFile_HasChange.HasValue && item.ImageFile_HasChange.Value)
                    {
                        item.ImageFile = fwFactory.CreateFilePointer(tempFolder, item.ImageFile_NewFile, item.ImageFile);
                    }
                    
                    ShowroomAreaImage dbAreaImage;
                    if (item.ShowroomAreaImageID < 0)
                    {
                        dbAreaImage = new ShowroomAreaImage();
                        dbItem.ShowroomAreaImage.Add(dbAreaImage);
                    }
                    else
                    {
                        dbAreaImage = dbItem.ShowroomAreaImage.FirstOrDefault(o => o.ShowroomAreaImageID == item.ShowroomAreaImageID);
                    }

                    if (dbAreaImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ShowroomAreaMng.ShowroomAreaImage, ShowroomAreaImage>(item, dbAreaImage);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.ShowroomAreaMng.ShowroomArea, ShowroomArea>(dtoItem, dbItem);
        }
    }
}
