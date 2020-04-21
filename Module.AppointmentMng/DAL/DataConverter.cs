using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.AppointmentMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "AppointmentMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AppointmentMng_Appointment_View, DTO.AppointmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.StartTime_Date, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StartTime_Time, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.EndTime_Date, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndTime_Time, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.RemindTime_Date, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RemindTime_Time, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.start_string, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd-MM-yyyy HH:mm") : ""))
                    .ForMember(d => d.end_string, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd-MM-yyyy HH:mm") : ""))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AppointmentMng_ClientSearchResult_View, DTO.ClientSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AppointmentDTO, Appointment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.AppointmentID, o => o.Ignore())
                    .ForMember(d => d.AppointmentUD, o => o.Ignore())
                    .ForMember(d => d.UserID, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.AppointmentDTO> DB2DTO_AppointmentList(List<AppointmentMng_Appointment_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AppointmentMng_Appointment_View>, List<DTO.AppointmentDTO>>(dbItems);
        }

        public DTO.AppointmentDTO DB2DTO_Appointment(AppointmentMng_Appointment_View dbItem)
        {
            return AutoMapper.Mapper.Map<AppointmentMng_Appointment_View, DTO.AppointmentDTO>(dbItem);
        }

        public List<DTO.ClientSearchResult> DB2DTO_ClientSearchResultList(List<AppointmentMng_ClientSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AppointmentMng_ClientSearchResult_View>, List<DTO.ClientSearchResult>>(dbItems);
        }

        public void DTO2DB(DTO.AppointmentDTO dtoItem, ref Appointment dbItem)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.AppointmentDTO, Appointment>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.StartTime_Date) && !string.IsNullOrEmpty(dtoItem.StartTime_Time))
            {
                if (DateTime.TryParse(dtoItem.StartTime_Date + " " + dtoItem.StartTime_Time, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.StartTime = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.EndTime_Date) && !string.IsNullOrEmpty(dtoItem.EndTime_Time))
            {
                if (DateTime.TryParse(dtoItem.EndTime_Date + " " + dtoItem.EndTime_Time, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.EndTime = tmpDate;
                }
            }
            if(!string.IsNullOrEmpty(dtoItem.RemindTime_Date) && !string.IsNullOrEmpty(dtoItem.RemindTime_Time))
            {
                if (DateTime.TryParse(dtoItem.RemindTime_Date + " " + dtoItem.RemindTime_Time, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.RemindTime = tmpDate;
                }
            }
        }
    }
}
