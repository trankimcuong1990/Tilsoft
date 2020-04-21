using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Module.DefectsMng.DTO;

namespace Module.DefectsMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DefectsGroupMng_DefectsGroup_View, DTO.DefectsGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Support_TypeOfDefects_View, DTO.TypeOfDefectDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));    
                
                AutoMapper.Mapper.CreateMap<DefectsMng_Defects_View, DTO.DefectsDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.defectsImageDTOs, o => o.MapFrom(s => s.DefectsMng_DefectsImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DefectsMng_Defects_SearchView, DTO.DefectsDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DefectsMng_DefectsImage_View, DTO.DefectsImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DefectsDTO, Defects>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DefectsImageDTO, DefectsImage>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
              
            }
        }
        public List<DTO.DefectsDTO> BD2DTO_DefectsMngSearchResult(List<DefectsMng_Defects_SearchView> items)
        {
            return AutoMapper.Mapper.Map<List<DefectsMng_Defects_SearchView>, List<DTO.DefectsDTO>>(items);
        }
        public DTO.DefectsDTO DB2DTO_DefectsDTO(DefectsMng_Defects_View dbItem)
        {
            return AutoMapper.Mapper.Map<DefectsMng_Defects_View, DTO.DefectsDTO>(dbItem);
        }
        public void DTO2DB_Defects(DTO.DefectsDTO dto, ref Defects db)
        {
            if (dto.defectsImageDTOs != null)
            {
                foreach (var item in db.DefectsImage.ToArray())
                {
                    if (!dto.defectsImageDTOs.Select(s => s.DefectImageID).Contains(item.DefectImageID))
                    {
                        db.DefectsImage.Remove(item);
                    }
                }
                foreach (var item in dto.defectsImageDTOs)
                {
                    DefectsImage dbDetail = new DefectsImage();
                    if (item.DefectImageID < 0)
                    {
                        db.DefectsImage.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = db.DefectsImage.Where(o => o.DefectImageID == item.DefectImageID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.DefectsImageDTO, DefectsImage>(item, dbDetail);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.DefectsDTO, Defects>(dto, db);
            db.DefectA = dto.DefectA;
            db.DefectB = dto.DefectB;
            db.DefectC = dto.DefectC;
        }
        public List<DTO.DefectsGroup> DB2DTO_Defects(List<DefectsGroupMng_DefectsGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DefectsGroupMng_DefectsGroup_View>, List<DTO.DefectsGroup>>(dbItems);
        }
        public List<DTO.TypeOfDefectDTO> DB2DTO_TypeOfDefects(List<Support_TypeOfDefects_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Support_TypeOfDefects_View>, List<DTO.TypeOfDefectDTO>>(dbItems);
        }
       
    }
}