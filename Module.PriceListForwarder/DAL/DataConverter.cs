namespace Module.PriceListForwarder.DAL
{
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class DataConverter
    {
        #region ** Variables & Constants **

        protected static readonly string DateTimeFormatString = "dd/MM/yyyy";

        #endregion

        #region ** Constructors **

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // Create map DB to DTO
                // 1. PriceListForwarder_PriceListForwarderSearchResult_View to PriceListForwarderSearchResult
                AutoMapper.Mapper.CreateMap<PriceListForwarder_PriceListForwarderSearchResult_View, DTO.PriceListForwarderSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString(DateTimeFormatString) : string.Empty))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString(DateTimeFormatString) : string.Empty))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(DateTimeFormatString) : string.Empty))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(DateTimeFormatString) : string.Empty));

                AutoMapper.Mapper.CreateMap<PriceListForwarder_PriceListForwarder_View, DTO.PriceListForwarder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString(DateTimeFormatString) : string.Empty))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString(DateTimeFormatString) : string.Empty))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(DateTimeFormatString) : string.Empty))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(DateTimeFormatString) : string.Empty))
                    .ForMember(d => d.PriceListForwarderDetails, o => o.MapFrom(s => s.PriceListForwarder_PriceListForwarderDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PriceListForwarder_PriceListForwarderDetail_View, DTO.PriceListForwarderDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PriceListForwarder, PriceListForwarder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.Ignore())
                    .ForMember(d => d.EndDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.PriceListForwarderDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PriceListForwarderDetail, PriceListForwarderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PoLID, o => o.ResolveUsing(s => s.PoLID ?? 0))
                    .ForMember(d => d.PoDID, o => o.ResolveUsing(s => s.PoDID ?? 0))
                    .ForMember(d => d.PricePerUnit, o => o.ResolveUsing(s => s.PricePerUnit ?? 0));
            }

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        #endregion

        #region ** Methods & Functions **

        public List<DTO.PriceListForwarderSearchResult> DB2DTOSearchResult(List<PriceListForwarder_PriceListForwarderSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceListForwarder_PriceListForwarderSearchResult_View>, List<DTO.PriceListForwarderSearchResult>>(dbItems);
        }

        public DTO.PriceListForwarder DB2DTOGetData(PriceListForwarder_PriceListForwarder_View dbItem)
        {
            return AutoMapper.Mapper.Map<PriceListForwarder_PriceListForwarder_View, DTO.PriceListForwarder>(dbItem);
        }

        public void DTO2DB(DTO.PriceListForwarder dtoConverter, PriceListForwarder dbItem)
        {
            if (dtoConverter.PriceListForwarderDetails != null)
            {
                foreach(var item in dbItem.PriceListForwarderDetail.ToArray())
                {
                    if (!dtoConverter.PriceListForwarderDetails.Select(s => s.PriceListForwarderDetailID).Contains(item.PriceListForwarderDetailID))
                    {
                        dbItem.PriceListForwarderDetail.Remove(item);
                    }
                }

                foreach(var dto in dtoConverter.PriceListForwarderDetails)
                {
                    PriceListForwarderDetail item;

                    if (dto.PriceListForwarderDetailID < 0)
                    {
                        item = new PriceListForwarderDetail();
                        dbItem.PriceListForwarderDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.PriceListForwarderDetail.FirstOrDefault(o => o.PriceListForwarderDetailID == dto.PriceListForwarderDetailID);
                    }

                    if (item != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PriceListForwarderDetail, PriceListForwarderDetail>(dto, item);
                    }
                }
            }

            // converter start date and end date
            dbItem.StartDate = dtoConverter.StartDate.ConvertStringToDateTime().Value;
            dbItem.EndDate = dtoConverter.EndDate.ConvertStringToDateTime().Value;

            AutoMapper.Mapper.Map<DTO.PriceListForwarder, PriceListForwarder>(dtoConverter, dbItem);
        }

        #endregion
    }
}
