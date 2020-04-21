using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.WAYNMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<WAYNMng_WAYN_View, DTO.WAYN>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WorkingDate, o => o.ResolveUsing(s => s.WorkingDate.HasValue ? s.WorkingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.start_string, o => o.ResolveUsing(s => s.WorkingDate.HasValue ? s.WorkingDate.Value.ToString("dd-MM-yyyy HH:mm") : ""))
                    .ForMember(d => d.end_string, o => o.ResolveUsing(s => s.WorkingDate.HasValue ? s.WorkingDate.Value.ToString("dd-MM-yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WAYNMng_EmployeeList_View, DTO.EmployeeList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WorkingDate, o => o.ResolveUsing(s => s.WorkingDate.HasValue ? s.WorkingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.EmployeeList, WAYN>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WorkingDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.WAYN> DB2DTO_WAYNList(List<WAYNMng_WAYN_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WAYNMng_WAYN_View>, List<DTO.WAYN>>(dbItems);
        }
        //public DTO.WAYN DB2DTO_WAYN(WAYNMng_WAYN_View dbItem)
        //{
        //    return AutoMapper.Mapper.Map<WAYNMng_WAYN_View, DTO.WAYN>(dbItem);
        //}
        //public DTO.EmployeeList DB2DTO_Employee(WAYNMng_EmployeeList_View dbItem)
        //{
        //    return AutoMapper.Mapper.Map<WAYNMng_EmployeeList_View, DTO.EmployeeList>(dbItem);
        //}
        public List<DTO.EmployeeList> DB2DTO_EmployeeList(List<WAYNMng_EmployeeList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WAYNMng_EmployeeList_View>, List<DTO.EmployeeList>>(dbItems);
        }
        private WAYNMngEntities CreateContext()
        {
            return new WAYNMngEntities(Library.Helper.CreateEntityConnectionString("DAL.WAYNMngModel"));
        }
        public void DTO2DB(DTO.EmployeeList dtoItem, ref WAYN dbItem)
        {
            AutoMapper.Mapper.Map<DTO.EmployeeList, WAYN>(dtoItem, dbItem);
            dbItem.WorkingDate = dtoItem.WorkingDate.ConvertStringToDateTime();
        }
    }
}
