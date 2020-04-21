using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkSetting;
using AutoMapper;
using Module.ClientSpecificationMng.DTO;
using Library;

namespace Module.ClientSpecificationMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (Setting.Maps.Contains(mapName))
            {
                return;
            }

            Mapper.CreateMap<ClientSpecificationMng_SaleSearchResult_View, ClientSpecificationSearchResultDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForMember(d => d.ClientSpecificationUpdatedDate, o => o.ResolveUsing(s => s.ClientSpecificationUpdatedDate.HasValue ? s.ClientSpecificationUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ClientSpecificationPIs, o => o.MapFrom(s => s.ClientSpecificationMng_OfferLinePI_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<ClientSpecificationMng_Sale_View, ClientSpecificationDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForMember(d => d.ClientSpecificationUpdatedDate, o => o.ResolveUsing(s => s.ClientSpecificationUpdatedDate.HasValue ? s.ClientSpecificationUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.FactorySpecifications, o => o.MapFrom(s => s.ClientSpecificationMng_FactoryMng_View));

            Mapper.CreateMap<ClientSpecificationMng_FactoryMng_View, FactorySpecificationDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForMember(d => d.FactorySpecificationUpdatedDate, o => o.ResolveUsing(s => s.FactorySpecificationUpdatedDate.HasValue ? s.FactorySpecificationUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.FactorySpecificationComments, o => o.MapFrom(s => s.ClientSpecificationMng_FactoryMngFactoryComment_View));

            Mapper.CreateMap<ClientSpecificationMng_FactoryMngFactoryComment_View, FactorySpecificationCommentDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")));

            Mapper.CreateMap<ClientSpecificationMng_Type_View, ClientSpecificationTypeDTO>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<ClientSpecificationMng_OfferLinePI_View, ClientSpecificationOfferPIDTO>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<ClientSpecificationMng_StandardFile_View, ClientSpecificationStandardFileDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

            Mapper.CreateMap<ClientSpecificationDTO, ClientSpecification>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ClientSpecificationUpdatedBy, o => o.Ignore())
                .ForMember(d => d.ClientSpecificationUpdatedDate, o => o.Ignore())
                .ForMember(d => d.ClientSpecificationID, o => o.Ignore());

            Mapper.CreateMap<FactorySpecificationCommentDTO, FactorySpecificationComment>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FactorySpecificationCommentID, o => o.Ignore());

            Setting.Maps.Add(mapName);
        }

        public List<ClientSpecificationSearchResultDTO> DB2DTO_ClientSpecificationSearchResult(List<ClientSpecificationMng_SaleSearchResult_View> dbItem)
        {
            return Mapper.Map<List<ClientSpecificationMng_SaleSearchResult_View>, List<ClientSpecificationSearchResultDTO>>(dbItem);
        }

        public ClientSpecificationDTO DB2DTO_ClientSpecification(ClientSpecificationMng_Sale_View dbItem)
        {
            return Mapper.Map<ClientSpecificationMng_Sale_View, ClientSpecificationDTO>(dbItem);
        }

        public FactorySpecificationCommentDTO DB2DTO_FactorySpecificationComment(ClientSpecificationMng_FactoryMngFactoryComment_View dbItem)
        {
            return Mapper.Map<ClientSpecificationMng_FactoryMngFactoryComment_View, FactorySpecificationCommentDTO>(dbItem);
        }

        public List<FactorySpecificationCommentDTO> DB2DTO_FactorySpecificationComment(List<ClientSpecificationMng_FactoryMngFactoryComment_View> dbItem)
        {
            return Mapper.Map<List<ClientSpecificationMng_FactoryMngFactoryComment_View>, List<FactorySpecificationCommentDTO>>(dbItem);
        }

        public List<ClientSpecificationTypeDTO> DB2DTO_ClientSpecificationType(List<ClientSpecificationMng_Type_View> dbItem)
        {
            return Mapper.Map<List<ClientSpecificationMng_Type_View>, List<ClientSpecificationTypeDTO>>(dbItem);
        }

        public ClientSpecificationStandardFileDTO DB2DTO_ClientSpecificationStandardFile(ClientSpecificationMng_StandardFile_View dbItem)
        {
            return Mapper.Map<ClientSpecificationMng_StandardFile_View, ClientSpecificationStandardFileDTO>(dbItem);
        }

        public void DTO2DB_ClientSpecification(ClientSpecificationDTO dtoItem, ref ClientSpecification dbItem)
        {
            Mapper.Map<ClientSpecificationDTO, ClientSpecification>(dtoItem, dbItem);
        }

        public void DTO2DB_FactorySpecificationComment(FactorySpecificationCommentDTO dtoItem, ref FactorySpecificationComment dbItem)
        {
            Mapper.Map<FactorySpecificationCommentDTO, FactorySpecificationComment>(dtoItem, dbItem);
        }
    }
}
