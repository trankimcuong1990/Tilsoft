using AutoMapper;
using Library;
using Module.AnnualLeaveCalendarMng.DTO;
using System.Collections.Generic;

namespace Module.AnnualLeaveCalendarMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //             
                Mapper.CreateMap<AnnualLeaveCalendarMng_SearchResult_View, AnnualLeaveCalendarSearchResultDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))                 
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<AnnualLeaveCalendarMng_Company_View, CompanyDTO>()
                  .IgnoreAllNonExisting()                 
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }      
        public List<AnnualLeaveCalendarSearchResultDTO> DB2DTO_SearchResultDTOs(List<AnnualLeaveCalendarMng_SearchResult_View> dbItems)
        {
            return Mapper.Map<List<AnnualLeaveCalendarMng_SearchResult_View>, List<AnnualLeaveCalendarSearchResultDTO>>(dbItems);
        }   
        public List<CompanyDTO> DB2DTO_CompanyDTOs(List<AnnualLeaveCalendarMng_Company_View> dbItems)
        {
            return Mapper.Map<List<AnnualLeaveCalendarMng_Company_View>, List<CompanyDTO>>(dbItems);
        }
    }
}
