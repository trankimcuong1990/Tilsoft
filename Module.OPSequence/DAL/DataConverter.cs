using Library;
using System.Collections.Generic;
using System.Linq;

namespace Module.OPSequence.DAL
{
    internal class DataConverter
    {
        protected readonly string FormatDateToString = "dd/MM/yyyy";

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<OPSequence_OPSequenceSearchResult_View, DTO.OPSequenceSearchResultDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(FormatDateToString) : string.Empty))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<OPSequence_OPSequence_View, DTO.OPSequenceDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(FormatDateToString) : string.Empty))
                .ForMember(d => d.OPSequenceDetails, o => o.MapFrom(s => s.OPSequence_OPSequenceDetail_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<OPSequence_OPSequenceDetail_View, DTO.OPSequenceDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.OPSequenceDto, OPSequence>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.OPSequenceID, o => o.Ignore())
                .ForMember(d => d.CompanyID, o => o.Ignore())
                .ForMember(d => d.OPSequenceDetail, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<DTO.OPSequenceDetailDto, OPSequenceDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.OPSequenceDetailID, o => o.Ignore());

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.OPSequenceSearchResultDto> DB2DTO_Search(List<OPSequence_OPSequenceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OPSequence_OPSequenceSearchResult_View>, List<DTO.OPSequenceSearchResultDto>>(dbItems);
        }

        public DTO.OPSequenceDto DB2DTO_Get(OPSequence_OPSequence_View dbItem)
        {
            return AutoMapper.Mapper.Map<OPSequence_OPSequence_View, DTO.OPSequenceDto>(dbItem);
        }

        public void DTO2DB_Update(DTO.OPSequenceDto dtoItem, ref OPSequence dbItem)
        {
            if (dtoItem.OPSequenceDetails != null)
            {
                foreach (OPSequenceDetail item in dbItem.OPSequenceDetail.ToArray())
                {
                    if (!dtoItem.OPSequenceDetails.Select(s => s.OPSequenceDetailID).Contains(item.OPSequenceDetailID))
                        dbItem.OPSequenceDetail.Remove(item);
                }

                foreach (DTO.OPSequenceDetailDto dto in dtoItem.OPSequenceDetails)
                {
                    OPSequenceDetail item;

                    if (dto.OPSequenceDetailID < 0)
                    {
                        item = new OPSequenceDetail();

                        dbItem.OPSequenceDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.OPSequenceDetail.FirstOrDefault(s => s.OPSequenceDetailID == dto.OPSequenceDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.OPSequenceDetailDto, OPSequenceDetail>(dto, item);
                }
            }

            AutoMapper.Mapper.Map<DTO.OPSequenceDto, OPSequence>(dtoItem, dbItem);
        }
    }
}
