using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryMaterial.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryMaterialMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryMaterialMng_FactoryMaterial_SearchView, DTO.FactoryMaterialSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialMng_FactoryMaterial_View, DTO.FactoryMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d =>d.FactoryMaterialImages,o =>o.MapFrom(s =>s.FactoryMaterialMng_FactoryMaterialImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialMng_FactoryMaterialImage_View, DTO.FactoryMaterialImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailLocation) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ThumbnailLocation)))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileLocation)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialImage, FactoryMaterialImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialImageID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterial, FactoryMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialID, o => o.Ignore())
                    .ForMember(d => d.FactoryMaterialImage, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryMaterialSearch> DB2DTO_FactoryMaterialSearch(List<FactoryMaterialMng_FactoryMaterial_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialMng_FactoryMaterial_SearchView>, List<DTO.FactoryMaterialSearch>>(dbItems);
        }

        public DTO.FactoryMaterial DB2DTO_FactoryMaterial(FactoryMaterialMng_FactoryMaterial_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryMaterialMng_FactoryMaterial_View, DTO.FactoryMaterial>(dbItem);
        }

        public void DTO2DB_FactoryMaterial(DTO.FactoryMaterial dtoItem, ref FactoryMaterial dbItem, string tempFolder)
        {
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            if (dtoItem.FactoryMaterialImages != null)
            {                               
                foreach (var item in dbItem.FactoryMaterialImage.ToArray())
                {
                    if (!dtoItem.FactoryMaterialImages.Select(s => s.FactoryMaterialImageID).Contains(item.FactoryMaterialImageID))
                    {
                        dbItem.FactoryMaterialImage.Remove(item);
                    }
                }
                
                foreach (var item in dtoItem.FactoryMaterialImages)
                {
                    //modify dto image field
                    if (item.ImageFile_HasChange.HasValue && item.ImageFile_HasChange.Value)
                    {
                        item.ImageFile = fwFactory.CreateFilePointer(tempFolder, item.ImageFile_NewFile, item.ImageFile);
                    } 
                    
                    //read db image field
                    FactoryMaterialImage dbDetail;
                    if (item.FactoryMaterialImageID < 0)
                    {
                        dbDetail = new FactoryMaterialImage();
                        dbItem.FactoryMaterialImage.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryMaterialImage.Where(o => o.FactoryMaterialImageID == item.FactoryMaterialImageID).FirstOrDefault();                        
                    }

                    //map from dto image field to db image field
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryMaterialImage, FactoryMaterialImage>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryMaterial, FactoryMaterial>(dtoItem, dbItem);
        }

        public List<DTO.FactoryMaterialImage> DB2DTO_FactoryMaterialImage(List<FactoryMaterialMng_FactoryMaterialImage_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialMng_FactoryMaterialImage_View>, List<DTO.FactoryMaterialImage>>(dbItems);
        }
    }
}
