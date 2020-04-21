using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientLPMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientOfferMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ClientLPMng_ClientLPMng_SearchView, DTO.SearchClientLPDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""));

                AutoMapper.Mapper.CreateMap<ClientLPMng_ClientLPMng_View, DTO.ClientLPDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ClientLPNotificationDTOs, o => o.MapFrom(s => s.ClientLPMng_ClientLPNotification_View));

                AutoMapper.Mapper.CreateMap<ClientLPMng_ClientLPNotification_View, DTO.ClientLPNotificationDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<SupportMng_LPManagingTeam_View, DTO.SupportLPManagingDTO>()
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<ClientLPMng_SupportEmployee_View, DTO.SupportEmployeeDTO>()
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.ClientLPNotificationDTO, ClientLPNotification>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.ClientLPDTO, ClientLP>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.ClientLPNotification, o => o.Ignore());
                   //.ForMember(d => d.ClientOfferID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.SearchClientLPDTO> DB2DTO_ClientLPSearch(List<ClientLPMng_ClientLPMng_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientLPMng_ClientLPMng_SearchView>, List<DTO.SearchClientLPDTO>>(dbItems);
        }

        public DTO.ClientLPDTO DB2DTO_ClientLP(ClientLPMng_ClientLPMng_View dbItem)
        {
            return AutoMapper.Mapper.Map<ClientLPMng_ClientLPMng_View, DTO.ClientLPDTO>(dbItem);
        }

        public List<DTO.SupportEmployeeDTO> DB2DTO_SupportEmployee(List<ClientLPMng_SupportEmployee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientLPMng_SupportEmployee_View>, List<DTO.SupportEmployeeDTO>>(dbItems);
        }

        public List<DTO.SupportLPManagingDTO> DB2DTO_SupportLPManaging(List<SupportMng_LPManagingTeam_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_LPManagingTeam_View>, List<DTO.SupportLPManagingDTO>>(dbItems);
        }

        public void DTO2DB_ClientLP(DTO.ClientLPDTO dtoItem, ref ClientLP dbItem)
        {           
            if (dtoItem.ClientLPNotificationDTOs != null)
            {
                foreach (ClientLPNotification item in dbItem.ClientLPNotification.ToList())
                {
                    if (!dtoItem.ClientLPNotificationDTOs.Select(s => s.ClientLPNotificationID).Contains(item.ClientLPNotificationID))
                        dbItem.ClientLPNotification.Remove(item);
                }

                foreach (DTO.ClientLPNotificationDTO dto in dtoItem.ClientLPNotificationDTOs)
                {
                    ClientLPNotification item;

                    if (dto.ClientLPNotificationID < 0)
                    {
                        item = new ClientLPNotification();

                        dbItem.ClientLPNotification.Add(item);
                    }
                    else
                    {
                        item = dbItem.ClientLPNotification.FirstOrDefault(s => s.ClientLPNotificationID == dto.ClientLPNotificationID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.ClientLPNotificationDTO, ClientLPNotification>(dto, item);
                }
            } 
            AutoMapper.Mapper.Map<DTO.ClientLPDTO, ClientLP>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }
    }
}
