using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FrameMaterialProfileMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View, DTO.FrameMaterialProfileSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ImageFile_FullSizeUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ImageFile_FullSizeUrl) ? s.ImageFile_FullSizeUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialProfileMng_FrameMaterialProfile_View, DTO.FrameMaterialProfile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.FrameMaterialProfileFactories, o => o.MapFrom(s => s.FrameMaterialProfileMng_FrameMaterialProfileFactory_View))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialProfile, FrameMaterialProfile>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageFile, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialProfileFactory, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialProfileID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FrameMaterialProfileMng_FrameMaterialProfileFactory_View, DTO.FrameMaterialProfileFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialProfileFactory, FrameMaterialProfileFactory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FrameMaterialProfileFactoryID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialProfileID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FrameMaterialProfileSearchResult> DB2DTO_FrameMaterialProfileSearchResultList(List<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FrameMaterialProfileMng_FrameMaterialProfileSearchResult_View>, List<DTO.FrameMaterialProfileSearchResult>>(dbItems);
        }

        public DTO.FrameMaterialProfile DB2DTO_FrameMaterialProfile(FrameMaterialProfileMng_FrameMaterialProfile_View dbItem)
        {
            return AutoMapper.Mapper.Map<FrameMaterialProfileMng_FrameMaterialProfile_View, DTO.FrameMaterialProfile>(dbItem);
        }

        public void DTO2BD(DTO.FrameMaterialProfile dtoItem, ref FrameMaterialProfile dbItem)
        {
            // map factory
            if (dtoItem.FrameMaterialProfileFactories != null)
            {
                // check for child rows deleted
                foreach (FrameMaterialProfileFactory dbFactory in dbItem.FrameMaterialProfileFactory.ToArray())
                {
                    if (!dtoItem.FrameMaterialProfileFactories.Select(o => o.FrameMaterialProfileFactoryID).Contains(dbFactory.FrameMaterialProfileFactoryID))
                    {
                        dbItem.FrameMaterialProfileFactory.Remove(dbFactory);
                    }
                }

                // map child rows
                foreach (DTO.FrameMaterialProfileFactory dtoFactory in dtoItem.FrameMaterialProfileFactories)
                {
                    FrameMaterialProfileFactory dbFactory;
                    if (dtoFactory.FrameMaterialProfileFactoryID <= 0)
                    {
                        dbFactory = new FrameMaterialProfileFactory();
                        dbItem.FrameMaterialProfileFactory.Add(dbFactory);
                    }
                    else
                    {
                        dbFactory = dbItem.FrameMaterialProfileFactory.FirstOrDefault(o => o.FrameMaterialProfileFactoryID == dtoFactory.FrameMaterialProfileFactoryID);
                    }

                    if (dbFactory != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FrameMaterialProfileFactory, FrameMaterialProfileFactory>(dtoFactory, dbFactory);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FrameMaterialProfile, FrameMaterialProfile>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
