using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;


namespace Module.ShowroomRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ShowroomRpt_ProductionItemWithDescription_View, DTO.ProductionItemWithDescription>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomRpt_Showroom2_View, DTO.ShowroomDataDto>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (string.IsNullOrEmpty(s.FileLocation) ? "no-image.jpg" : s.FileLocation)))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (string.IsNullOrEmpty(s.ThumbnailLocation) ? "no-image.jpg" : s.ThumbnailLocation)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomRpt_SupportFactoryWarehouseAVF_View, DTO.FactoryWarehouse>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomRpt_SupportFactoryWarehouseAVT_View, DTO.FactoryWarehouse>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomRpt_ReceivingNote_View, DTO.ReceivingNote>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ShowroomRpt_FactoryWarehousePallet_View, DTO.FactoryWarehousePallet>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ShowroomDataDto> DB2DTO_Showroom2Rpt(List<ShowroomRpt_Showroom2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ShowroomRpt_Showroom2_View>, List<DTO.ShowroomDataDto>>(dbItem);
        }

        public List<DTO.ProductionItemWithDescription> DB2DTO_ProductionItemWithDescription(List<ShowroomRpt_ProductionItemWithDescription_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ShowroomRpt_ProductionItemWithDescription_View>, List<DTO.ProductionItemWithDescription>>(dbItem);
        }

        public List<DTO.ReceivingNote> DB2DTO_ReceivingNote(List<ShowroomRpt_ReceivingNote_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ShowroomRpt_ReceivingNote_View>, List<DTO.ReceivingNote>>(dbItem);
        }

        public List<DTO.FactoryWarehousePallet> DB2DTO_FactoryWarehousePallet(List<ShowroomRpt_FactoryWarehousePallet_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ShowroomRpt_FactoryWarehousePallet_View>, List<DTO.FactoryWarehousePallet>>(dbItem);
        }
    }
}
