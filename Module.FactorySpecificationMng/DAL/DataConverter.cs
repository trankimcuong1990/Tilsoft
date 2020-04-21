using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using System.Globalization;

namespace Module.FactorySpecificationMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactorySpecificationMng_FactorySpecification_Search, DTO.FactorySpecificationSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ClientFileLocation) ? s.ClientFileLocation : "no-image.jpg")))
                    .ForMember(d => d.FactoryFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FactoryFileLocation) ? s.FactoryFileLocation : "no-image.jpg")))
                    .ForMember(d => d.FactorySpecificationUpdatedDate, o => o.ResolveUsing(s => s.FactorySpecificationUpdatedDate.HasValue ? s.FactorySpecificationUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactorySpecificationPIs, o => o.MapFrom(s => s.FactorySpecificationMng_OfferLinePI_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySpecificationMng_FactorySpecification_View, DTO.FactorySpecification>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ClientFileLocation) ? s.ClientFileLocation : "no-image.jpg")))
                    .ForMember(d => d.FactoryFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FactoryFileLocation) ? s.FactoryFileLocation : "no-image.jpg")))
                    .ForMember(d => d.FactorySpecificationUpdatedDate, o => o.ResolveUsing(s => s.FactorySpecificationUpdatedDate.HasValue ? s.FactorySpecificationUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ClientSpecificationUpdatedDate, o => o.ResolveUsing(s => s.ClientSpecificationUpdatedDate.HasValue ? s.ClientSpecificationUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactorySpecificationComments, o => o.MapFrom(s => s.FactorySpecificationMng_FactorySpecificationComment_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactorySpecification, FactorySpecification>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactorySpecificationUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactorySpecificationComment, o => o.Ignore())
                    .ForMember(d => d.FactorySpecificationID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactorySpecificationMng_FactorySpecificationComment_View, DTO.FactorySpecificationComment>()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactorySpecificationMng_FactoryOrderDetail_View, DTO.FactoryOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))   
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactorySpecificationComment, FactorySpecificationComment>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.FactorySpecificationCommentID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactorySpecificationMng_OfferLinePI_View, DTO.FactorySpecificationOfferLinePI>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.FactorySpecificationSearch> DB2DTO_FactorySpecificationSearch(List<FactorySpecificationMng_FactorySpecification_Search> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactorySpecificationMng_FactorySpecification_Search>, List<DTO.FactorySpecificationSearch>>(dbItems);
        }

        public DTO.FactorySpecification DB2DTO_FactorySpecification(FactorySpecificationMng_FactorySpecification_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactorySpecificationMng_FactorySpecification_View, DTO.FactorySpecification>(dbItem);
        }

        public List<DTO.FactorySpecificationComment> DB2DTO_FactorySpecificationComment(List<FactorySpecificationMng_FactorySpecificationComment_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactorySpecificationMng_FactorySpecificationComment_View>, List<DTO.FactorySpecificationComment>>(dbItems);
        }

        public List<DTO.FactoryOrderDetail> DB2DTO_FactoryOrderDetail(List<FactorySpecificationMng_FactoryOrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactorySpecificationMng_FactoryOrderDetail_View>, List<DTO.FactoryOrderDetail>>(dbItems);
        }

        public void DTO2BD(DTO.FactorySpecification dtoItem, ref FactorySpecification dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactorySpecification, FactorySpecification>(dtoItem, dbItem);
            dbItem.FactorySpecificationUpdatedDate = dtoItem.FactorySpecificationUpdatedDate.ConvertStringToDateTime();
        }
    }
}
