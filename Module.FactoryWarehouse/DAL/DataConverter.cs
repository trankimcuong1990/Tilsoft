using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FactoryWarehouse.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryWarehouse_FactoryWarehouseSearch_View, DTO.FactoryWarehouseSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryWarehouse_FactoryWarehouse_View, DTO.FactoryWarehouse>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.FactoryWarehousePallets, o => o.MapFrom(s => s.FactoryWarehouse_FactoryWarehousePallet_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryWarehouse, FactoryWarehouse>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.FactoryWarehouseID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryWarehouse_FactoryWarehousePallet_View, DTO.FactoryWarehousePallet>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryWarehousePallet, FactoryWarehousePallet>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryWarehousePalletID, o => o.Ignore())
                    .ForMember(d => d.FactoryWarehouseID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryWarehouse_CapacityOverview_View, DTO.CapacityOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                //
                // author: themy
                // description: add stock overview
                //
                //AutoMapper.Mapper.CreateMap<FactoryWarehouse_StockOverView_View, DTO.StockOverview>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<FactoryWarehouse_StockOverviewDetail_View, DTO.StockOverviewDetail>()
                //    .IgnoreAllNonExisting()
                //    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryWarehouse_PalletOverview_View, DTO.PalletOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                //
                //
                //

                // Mapping Branch
                Mapper.CreateMap<FactoryWarehouseMng_Branch_View, DTO.BranchDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryWarehouseSearch> DB2DTO_FactoryWarehouseSearchList(List<FactoryWarehouse_FactoryWarehouseSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryWarehouse_FactoryWarehouseSearch_View>, List<DTO.FactoryWarehouseSearch>>(dbItems);
        }

        public DTO.FactoryWarehouse DB2DTO_FactoryWarehouse(FactoryWarehouse_FactoryWarehouse_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryWarehouse_FactoryWarehouse_View, DTO.FactoryWarehouse>(dbItem);
        }

        public List<DTO.FactoryWarehouse> DB2DTO_WarehouseList(List<FactoryWarehouse_FactoryWarehouse_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryWarehouse_FactoryWarehouse_View>, List<DTO.FactoryWarehouse>>(dbItems);
        }

        public void DTO2DB(DTO.FactoryWarehouse dtoItem, ref FactoryWarehouse dbItem)
        {
            if (dtoItem.FactoryWarehousePallets != null)
            {
                //check for child rows deleted
                foreach (FactoryWarehousePallet dbPallet in dbItem.FactoryWarehousePallet.ToArray())
                {
                    if (!dtoItem.FactoryWarehousePallets.Select(o => o.FactoryWarehousePalletID).Contains(dbPallet.FactoryWarehousePalletID))
                    {
                        dbItem.FactoryWarehousePallet.Remove(dbPallet);
                    }
                }

                //map child row
                foreach (DTO.FactoryWarehousePallet dtoPallet in dtoItem.FactoryWarehousePallets)
                {
                    FactoryWarehousePallet dbPallet;
                    if (dtoPallet.FactoryWarehousePalletID <= 0)
                    {
                        dbPallet = new FactoryWarehousePallet();
                        dbItem.FactoryWarehousePallet.Add(dbPallet);
                    }
                    else
                    {
                        dbPallet = dbItem.FactoryWarehousePallet.FirstOrDefault(o => o.FactoryWarehousePalletID == dtoPallet.FactoryWarehousePalletID);
                    }

                    if (dbPallet != null)
                    {
                        dtoPallet.CompanyID = dtoItem.CompanyID;
                        AutoMapper.Mapper.Map<DTO.FactoryWarehousePallet, FactoryWarehousePallet>(dtoPallet, dbPallet);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryWarehouse, FactoryWarehouse>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }

        
        public List<DTO.CapacityOverview> DB2DTO_CapacityOverview(List<FactoryWarehouse_CapacityOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryWarehouse_CapacityOverview_View>, List<DTO.CapacityOverview>>(dbItems);
        }
        //
        // author: themy
        // description: add stock overview
        //
        //public List<DTO.StockOverview> DB2DTO_StockOverview(List<FactoryWarehouse_StockOverView_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<FactoryWarehouse_StockOverView_View>, List<DTO.StockOverview>>(dbItems);
        //}

        //public List<DTO.StockOverviewDetail> DB2DTO_StockOverviewDetail(List<FactoryWarehouse_StockOverviewDetail_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<FactoryWarehouse_StockOverviewDetail_View>, List<DTO.StockOverviewDetail>>(dbItems);
        //}

        public List<DTO.PalletOverview> DB2DTO_PalletOverview(List<FactoryWarehouse_PalletOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryWarehouse_PalletOverview_View>, List<DTO.PalletOverview>>(dbItems);
        }
        //
        // 
        //
    }
}
