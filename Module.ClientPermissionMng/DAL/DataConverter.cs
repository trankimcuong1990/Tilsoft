using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
namespace Module.ClientPermissionMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientPermissionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientPermissionMng_ClientPermission_View, DTO.ClientPermissionDTO>().IgnoreAllNonExisting()
                    .ForMember(d => d.ClientPermissionDetailDTOs, o => o.MapFrom(s => s.ClientPermissionMng_ClientPermissionDetail_View));
                AutoMapper.Mapper.CreateMap<DTO.ClientPermissionDTO, ClientPermission>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<ClientPermissionMng_ClientPermissionDetail_View, DTO.ClientPermissionDetailDTO>()
                    .IgnoreAllNonExisting();
                AutoMapper.Mapper.CreateMap<ClientPermissionMng_Employee_View, DTO.Employee>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ClientPermissionMng_ListModule_View, DTO.ModuleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.ClientPermissionDetailDTO, ClientPermissionDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            }
            FrameworkSetting.Setting.Maps.Add(mapName);

        }
        public List<DTO.ClientPermissionDTO> DB2DTO_ClientPermissionView(List<ClientPermissionMng_ClientPermission_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ClientPermissionMng_ClientPermission_View>, List<DTO.ClientPermissionDTO>>(dbItem);
        }
        public DTO.ClientPermissionDTO DB2DTO_OneClientPermissionView(ClientPermissionMng_ClientPermission_View dbItem)
        {
            return AutoMapper.Mapper.Map<ClientPermissionMng_ClientPermission_View, DTO.ClientPermissionDTO>(dbItem);
        }
        public void DTO2DB_ClientPermission(DTO.ClientPermissionDTO dto, ref ClientPermission db)
        {
             AutoMapper.Mapper.Map<DTO.ClientPermissionDTO, ClientPermission>(dto,db);
        }
        public List<DTO.Employee> DB2DTO_Employee(List<ClientPermissionMng_Employee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientPermissionMng_Employee_View>, List<DTO.Employee>>(dbItems);
        }
        public List<DTO.ModuleDTO> DB2DTO_Module(List<ClientPermissionMng_ListModule_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientPermissionMng_ListModule_View>, List<DTO.ModuleDTO>>(dbItems);
        }
        public ClientPermissionDetail DTO2DB_ClientPermissionDetail(DTO.ClientPermissionDetailDTO dtoItem)
        {
            return AutoMapper.Mapper.Map<DTO.ClientPermissionDetailDTO, ClientPermissionDetail>(dtoItem);
        }
    }
}
