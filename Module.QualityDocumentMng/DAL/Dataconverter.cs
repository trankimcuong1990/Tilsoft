using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Collections;

namespace Module.QualityDocumentMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<QualityDocumentMng_QualityDocument_SearchView, DTO.QualityDocumentSearchDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                .ForMember(d => d.IssuedDate, o => o.ResolveUsing(s => s.IssuedDate.HasValue ? s.IssuedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<QualityDocumentMng_QualityDocument_View, DTO.QualityDocumentDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.IssuedDate, o => o.ResolveUsing(s => s.IssuedDate.HasValue ? s.IssuedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.QualityDocumentDTO, QualityDocument>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QualityDocumentID, o => o.Ignore())
                .ForMember(d => d.IssuedDate, o => o.Ignore())
                .ForMember(d => d.AttachedFile, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore());

                

            AutoMapper.Mapper.CreateMap<SupportMng_QualityDocumentType_View, DTO.SupportQualityDocument>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.QualityDocumentSearchDTO> DB2DTO_SearchResult(List<QualityDocumentMng_QualityDocument_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QualityDocumentMng_QualityDocument_SearchView>, List<DTO.QualityDocumentSearchDTO>>(dbItems);
        }

        public DTO.QualityDocumentDTO DB2DTO_QualityDocument(QualityDocumentMng_QualityDocument_View dbItem)
        {
            return AutoMapper.Mapper.Map<QualityDocumentMng_QualityDocument_View, DTO.QualityDocumentDTO>(dbItem);
        }

        public void DTO2DB_QualityDocument(DTO.QualityDocumentDTO dtoItem, ref QualityDocument dbItem)
        {
            AutoMapper.Mapper.Map<DTO.QualityDocumentDTO, QualityDocument>(dtoItem, dbItem);
            dbItem.IssuedDate = dtoItem.IssuedDate.ConvertStringToDateTime();
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }

        public List<DTO.SupportQualityDocument> DB2DTO_GetQualityType(List<SupportMng_QualityDocumentType_View> spType)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_QualityDocumentType_View>, List<DTO.SupportQualityDocument>>(spType);
        }

    }
}
