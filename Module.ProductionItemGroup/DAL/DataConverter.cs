using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;


namespace Module.ProductionItemGroup.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemGroup_Search, DTO.ProductionItemGroupSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemGroup_View, DTO.ProductionItemGroupDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ListProductionItemGroupNotification, o => o.MapFrom(s => s.ProductionItemGroupNotification_View))
                    .ForMember(d => d.ListProductionItemGroupStockNotification, o => o.MapFrom(s => s.ProductionItemGroupStockNotification_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemGroupDTO, ProductionItemGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionItemGroupID, o => o.Ignore())
                    .ForMember(d => d.ProductionItemGroupNotification, o => o.Ignore())
                    .ForMember(d => d.ProductionItemGroupStockNotification, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemGroupNotificationDTO, ProductionItemGroupNotification>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionItemGroupNotificationID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductionItemGroupNotification_View, DTO.ProductionItemGroupNotificationDTO>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemGroupStockNotificationDTO, ProductionItemGroupStockNotification>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionItemGroupStockNotificationID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductionItemGroupStockNotification_View, DTO.ProductionItemGroupStockNotificationDTO>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<ProductionItemGroupMng_User2_View, DTO.UserOnProductionItemGroup>()
                    .IgnoreAllNonExisting();


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ProductionItemGroupSearchDTO> DB2DTO_ProductionItemGroupSearchList(List<ProductionItemMng_ProductionItemGroup_Search> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemGroup_Search>, List<DTO.ProductionItemGroupSearchDTO>>(dbItems);
        }

        public DTO.ProductionItemGroupDTO DB2DTO_ProductionItemGroup(ProductionItemMng_ProductionItemGroup_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionItemMng_ProductionItemGroup_View, DTO.ProductionItemGroupDTO>(dbItem);
        }
        public List<DTO.UserOnProductionItemGroup> DB2DTO_UserOnProductionItemGroup(List<ProductionItemGroupMng_User2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemGroupMng_User2_View>, List<DTO.UserOnProductionItemGroup>>(dbItem);
        }

        //public void DTO2BD(DTO.ProductionItemGroupDTO dtoItem, ref ProductionItemGroup dbItem)
        //{
        //    AutoMapper.Mapper.Map<DTO.ProductionItemGroupDTO, ProductionItemGroup>(dtoItem, dbItem);
        //    dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        //}

        public void DTO2BD(DTO.ProductionItemGroupDTO dtoItem, ref ProductionItemGroup dbItem)
        {
            if (dtoItem.ListProductionItemGroupNotification != null)
            {
                foreach (ProductionItemGroupNotification item in dbItem.ProductionItemGroupNotification.ToArray())
                {
                    if (!dtoItem.ListProductionItemGroupNotification.Select(x => x.ProductionItemGroupNotificationID).Contains(item.ProductionItemGroupNotificationID))
                        dbItem.ProductionItemGroupNotification.Remove(item);
                }
                foreach (DTO.ProductionItemGroupNotificationDTO dto in dtoItem.ListProductionItemGroupNotification)
                {
                    ProductionItemGroupNotification item;
                    if (dto.ProductionItemGroupNotificationID <= 0)
                    {
                        item = new ProductionItemGroupNotification();
                        dbItem.ProductionItemGroupNotification.Add(item);
                    }
                    else
                    {
                        item = dbItem.ProductionItemGroupNotification.FirstOrDefault(s => s.ProductionItemGroupNotificationID == dto.ProductionItemGroupNotificationID);
                    }
                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.ProductionItemGroupNotificationDTO, ProductionItemGroupNotification>(dto, item);
                }

                foreach (ProductionItemGroupStockNotification item in dbItem.ProductionItemGroupStockNotification.ToArray())
                {
                    if (!dtoItem.ListProductionItemGroupStockNotification.Select(x => x.ProductionItemGroupStockNotificationID).Contains(item.ProductionItemGroupStockNotificationID))
                        dbItem.ProductionItemGroupStockNotification.Remove(item);
                }
                foreach (DTO.ProductionItemGroupStockNotificationDTO dto in dtoItem.ListProductionItemGroupStockNotification)
                {
                    ProductionItemGroupStockNotification item;
                    if (dto.ProductionItemGroupStockNotificationID <= 0)
                    {
                        item = new ProductionItemGroupStockNotification();
                        dbItem.ProductionItemGroupStockNotification.Add(item);
                    }
                    else
                    {
                        item = dbItem.ProductionItemGroupStockNotification.FirstOrDefault(s => s.ProductionItemGroupStockNotificationID == dto.ProductionItemGroupStockNotificationID);
                    }
                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.ProductionItemGroupStockNotificationDTO, ProductionItemGroupStockNotification>(dto, item);
                }
            }
            AutoMapper.Mapper.Map<DTO.ProductionItemGroupDTO, ProductionItemGroup>(dtoItem, dbItem);
            //AutoMapper.Mapper.Map<DTO.ProductionItemGroupDTO, ProductionItemGroup>(dtoItem, dbItem);
            //dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }


    }
}
