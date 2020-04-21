using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.CPLoadingPlan.DAL
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
                AutoMapper.Mapper.CreateMap<CP_LoadingPlanMng_LoadingPlanSearchResult_View, DTO.LoadingPlanSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShippedDate, o => o.ResolveUsing(s => s.ShippedDate.HasValue ? s.ShippedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipmentDate, o => o.ResolveUsing(s => s.ShipmentDate.HasValue ? s.ShipmentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CP_LoadingPlanMng_function_GetOrderInfo_Result, DTO.OrderInfoDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ContainerType_View, DTO.ContainerTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.LoadingPlanSearchResultDTO> DB2DTO(List<CP_LoadingPlanMng_LoadingPlanSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CP_LoadingPlanMng_LoadingPlanSearchResult_View>, List<DTO.LoadingPlanSearchResultDTO>>(dbItems);
        }

        public List<DTO.OrderInfoDTO> DB2DTO(List<CP_LoadingPlanMng_function_GetOrderInfo_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CP_LoadingPlanMng_function_GetOrderInfo_Result>, List<DTO.OrderInfoDTO>>(dbItems);
        }

        public List<DTO.ContainerTypeDTO> DB2DTO(List<SupportMng_ContainerType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ContainerType_View>, List<DTO.ContainerTypeDTO>>(dbItems);
        }
    }
}
