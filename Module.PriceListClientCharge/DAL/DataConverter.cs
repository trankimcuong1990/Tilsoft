namespace Module.PriceListClientCharge.DAL
{
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class DataConverter
    {
        #region ** Variable & Constant **

        protected readonly string FormatDateTimeToString = "dd/MM/yyyy";

        #endregion

        #region ** Constructor **

        public DataConverter()
        {
            var mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            // From database to object
            AutoMapper.Mapper.CreateMap<PriceListClientCharge_PriceListClientChargeSearch_View, DTO.PriceListClientChargeSearchDto>()
                .ForMember(d => d.StartDate, o => o.ResolveUsing(s => (s.StartDate.HasValue) ? s.StartDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.EndDate, o => o.ResolveUsing(s => (s.EndDate.HasValue) ? s.EndDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => (s.CreatedDate.HasValue) ? s.CreatedDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => (s.UpdatedDate.HasValue) ? s.UpdatedDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PriceListClientCharge_PriceListClientCharge_View, DTO.PriceListClientChargeDto>()
                .ForMember(d => d.StartDate, o => o.ResolveUsing(s => (s.StartDate.HasValue) ? s.StartDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.EndDate, o => o.ResolveUsing(s => (s.EndDate.HasValue) ? s.EndDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => (s.CreatedDate.HasValue) ? s.CreatedDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => (s.UpdatedDate.HasValue) ? s.UpdatedDate.Value.ToString(FormatDateTimeToString) : string.Empty))
                .ForMember(d => d.PriceListClientChargeDetail, o => o.MapFrom(s => s.PriceListClientCharge_PriceListClientChargeDetail_View))
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PriceListClientCharge_PriceListClientChargeDetail_View, DTO.PriceListClientChargeDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            // From object to database
            AutoMapper.Mapper.CreateMap<DTO.PriceListClientChargeDto, PriceListClientCharge>()
                .ForMember(d => d.StartDate, o => o.Ignore())
                .ForMember(d => d.EndDate, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.PriceListClientChargeDetail, o => o.Ignore())
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<DTO.PriceListClientChargeDetailDto, PriceListClientChargeDetail>()
                .ForMember(d => d.PoLID, o => o.ResolveUsing(s => s.PoLID ?? 0))
                .ForMember(d => d.PoDID, o => o.ResolveUsing(s => s.PoDID ?? 0))
                .ForMember(d => d.PricePerUnit, o => o.ResolveUsing(s => s.PricePerUnit ?? 0))
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        #endregion

        #region ** Function & Method **

        public List<DTO.PriceListClientChargeSearchDto> DB2DTO_PriceListClientChargeSearch(List<PriceListClientCharge_PriceListClientChargeSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceListClientCharge_PriceListClientChargeSearch_View>, List<DTO.PriceListClientChargeSearchDto>>(dbItems);
        }

        public DTO.PriceListClientChargeDto DB2DTO_PriceListClientCharge(PriceListClientCharge_PriceListClientCharge_View dbItem)
        {
            return AutoMapper.Mapper.Map<PriceListClientCharge_PriceListClientCharge_View, DTO.PriceListClientChargeDto>(dbItem);
        }

        public void DTO2DB_PriceListClientCharge(DTO.PriceListClientChargeDto dtoItem, PriceListClientCharge dbItem)
        {
            if (dtoItem.PriceListClientChargeDetail != null)
            {
                foreach (var item in dbItem.PriceListClientChargeDetail.ToList())
                {
                    if (!dtoItem.PriceListClientChargeDetail.Select(s => s.PriceListClientChargeDetailID).Contains(item.PriceListClientChargeDetailID))
                        dbItem.PriceListClientChargeDetail.Remove(item);
                }

                foreach (var dto in dtoItem.PriceListClientChargeDetail.ToList())
                {
                    PriceListClientChargeDetail item = null;

                    if (dto.PriceListClientChargeDetailID < 0)
                    {
                        item = new PriceListClientChargeDetail();

                        // Add item in PriceListClientChargeDetail
                        dbItem.PriceListClientChargeDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.PriceListClientChargeDetail.FirstOrDefault(o => o.PriceListClientChargeDetailID == dto.PriceListClientChargeDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.PriceListClientChargeDetailDto, PriceListClientChargeDetail>(dto, item);
                }
            }

            // Convert string to date
            dbItem.StartDate = dtoItem.StartDate.ConvertStringToDateTime();
            dbItem.EndDate = dtoItem.EndDate.ConvertStringToDateTime();

            AutoMapper.Mapper.Map<DTO.PriceListClientChargeDto, PriceListClientCharge>(dtoItem, dbItem);
        }

        #endregion
    }
}
