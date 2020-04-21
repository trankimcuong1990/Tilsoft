namespace Module.ProductionTeam.DAL
{
    using Library;
    using System.Collections.Generic;

    public class DataConverter
    {
        string FormatDateToString = "dd/MM/yyyy";

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<ProductionTeam_ProductionTeamSearch_View, DTO.ProductionTeamSearchDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<ProductionTeam_ProductionTeam_View, DTO.ProductionTeamDto>()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(this.FormatDateToString) : string.Empty))
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.ProductionTeamDto, ProductionTeam>()
                .ForMember(d => d.ProductionTeamID, o => o.Ignore())
                .ForMember(d => d.CompanyID, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.ProductionTeamSearchDto> DB2DTO_ProductionTeamSearchResult(List<ProductionTeam_ProductionTeamSearch_View> items)
        {
            return AutoMapper.Mapper.Map<List<ProductionTeam_ProductionTeamSearch_View>, List<DTO.ProductionTeamSearchDto>>(items);
        }

        public DTO.ProductionTeamDto DB2DTO_ProductionTeam(ProductionTeam_ProductionTeam_View item)
        {
            return AutoMapper.Mapper.Map<ProductionTeam_ProductionTeam_View, DTO.ProductionTeamDto>(item);
        }

        public void DTO2DB_ProductionTeam(DTO.ProductionTeamDto dto, ref ProductionTeam db)
        {
            AutoMapper.Mapper.Map<DTO.ProductionTeamDto, ProductionTeam>(dto, db);
        }
    }
}
