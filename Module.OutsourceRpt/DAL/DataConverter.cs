namespace Module.OutsourceRpt.DAL
{
    using Library;

    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "OutsourceRpt";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<OutsourceRpt_WorkOrder_View, DTO.OutsourceWorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourceRpt_ProductionItem_View, DTO.OutsourceProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourceRpt_DocumentNote_View, DTO.OutsourceDocumentNoteDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DocumentNoteDate, o => o.ResolveUsing(s => s.DocumentNoteDate.HasValue ? s.DocumentNoteDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourceRpt_ProductionTeam_View, DTO.OutsourceProductionTeamDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
    }
}
