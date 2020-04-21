using Library;
using System.Collections.Generic;

namespace Module.ShowroomTransferMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName) == false)
            {
                AutoMapper.Mapper.CreateMap<ShowroomTransferMng_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomTransferMng_FactoryWarehousePallet_View, DTO.FactoryWarehousePalletDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomTransferMng_ShowroomTransferSearchResult_View, DTO.ShowroomTransferSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ShowroomTransferDate, o => o.ResolveUsing(s => s.ShowroomTransferDate.HasValue ? s.ShowroomTransferDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomTransferMng_ShowroomTransfer_View, DTO.ShowroomTransferDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ShowroomTransferDate, o => o.ResolveUsing(s => s.ShowroomTransferDate.HasValue ? s.ShowroomTransferDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.ShowroomTransferDetails, o => o.MapFrom(s => s.ShowroomTransferMng_ShowroomTransferDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomTransferMng_ShowroomTransferDetail_View, DTO.ShowroomTransferDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryWarehouseDTO> DB2DTO_SupportFactoryWarehouse(List<ShowroomTransferMng_FactoryWarehouse_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ShowroomTransferMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(dbItem);
        }

        public List<DTO.FactoryWarehousePalletDTO> DB2DTO_SupportFactoryWarehousePallet(List<ShowroomTransferMng_FactoryWarehousePallet_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ShowroomTransferMng_FactoryWarehousePallet_View>, List<DTO.FactoryWarehousePalletDTO>>(dbItem);
        }

        public List<DTO.ShowroomTransferSearchResultDTO> DB2DTO_ShowroomTransferSearchResult(List<ShowroomTransferMng_ShowroomTransferSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ShowroomTransferMng_ShowroomTransferSearchResult_View>, List<DTO.ShowroomTransferSearchResultDTO>>(dbItem);
        }

        public DTO.ShowroomTransferDTO DB2DTO_ShowroomTransfer(ShowroomTransferMng_ShowroomTransfer_View dbItem)
        {
            return AutoMapper.Mapper.Map<ShowroomTransferMng_ShowroomTransfer_View, DTO.ShowroomTransferDTO>(dbItem);
        }
    }
}
