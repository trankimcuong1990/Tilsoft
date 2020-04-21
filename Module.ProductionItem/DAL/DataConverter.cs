using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.ProductionItem.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemSearchResultVirtual_View, DTO.ProductionItemSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItem_View, DTO.ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ProductionItemWarehouses, o => o.MapFrom(s => s.ProductionItemMng_ProductionItemWarehouse_View))
                    .ForMember(d => d.ProductionItemVenders, o => o.MapFrom(s => s.ProductionItemMng_ProductionItemVender_View))
                    .ForMember(d => d.productionItemSubUnitDTOs, o => o.MapFrom(s => s.ProductionItemMng_ProductionItemUnit_View))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemGroup_View, DTO.ProductionItemGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemType_View, DTO.ProductionItemType>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemSpec_View, DTO.ProductionItemSpec>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionItem, ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionItemWarehouse, o => o.Ignore())
                    .ForMember(d => d.StartDate, o => o.Ignore())
                    .ForMember(d => d.EndDate, o => o.Ignore())
                    .ForMember(d => d.ProductionItemID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemWarehouse_View, DTO.ProductionItemWarehouse>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemVender_View, DTO.ProductionItemVender>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemUnit_View, DTO.ProductionItemSubUnitDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.productionItemUnitHistorys, o => o.MapFrom(s => s.ProductionItemMng_ProductionItemUnitHistory_View))
                    .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy hh:mm tt") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_FactoryRawMaterialSupplier_List, DTO.FactoryRawMaterialSupplier>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Not using view: SupportMng_FactoryWarehouse_View.
                //AutoMapper.Mapper.CreateMap<SupportMng_FactoryWarehouse_View, DTO.FactoryWarehouse>()
                //   .IgnoreAllNonExisting()
                //   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemWarehouse, ProductionItemWarehouse>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ProductionItemWarehouseID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemVender, ProductionItemVender>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ProductionItemVenderID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemSubUnitDTO, ProductionItemUnit>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidFrom, o => o.Ignore())
                    .ForMember(d => d.ProductionItemUnitID, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_AssetClass_View, DTO.AssetClassDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ProductionItemMng_DepreciationType_View, DTO.DepreciationTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductionItemType_View, DTO.ProductionItemTypeSupport>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_BreakDownCategory_View, DTO.BreakDownCategoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Add new mapping: FactoryWarehouse, Branch.
                AutoMapper.Mapper.CreateMap<ProductionItemMng_FactoryWarehouse_View, DTO.FactoryWarehouse>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_Branch_View, DTO.BranchDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_FactoryWarehouseSearchResultVirtual_View, DTO.FactoryWarehouseSearchResultDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_OutSourcingCostType_View, DTO.OutSourcingCostTypeDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemUnitHistory_View, DTO.ProductionItemUnitHistoryDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ValidFromHistory, o => o.ResolveUsing(s => s.ValidFromHistory.HasValue ? s.ValidFromHistory.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.UpdateDate, o => o.ResolveUsing(s => s.UpdateDate.HasValue ? s.UpdateDate.Value.ToString("dd/MM/yyyy hh:mm tt") : ""))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ProductionItemSearchResult> DB2DTO_ProductionItemSearchResultList(List<ProductionItemMng_ProductionItemSearchResultVirtual_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemSearchResultVirtual_View>, List<DTO.ProductionItemSearchResult>>(dbItems);
        }

        public DTO.ProductionItem DB2DTO_ProductionItem(ProductionItemMng_ProductionItem_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionItemMng_ProductionItem_View, DTO.ProductionItem>(dbItem);
        }

        public List<DTO.ProductionItemGroup> DB2DTO_ProductionItemGroup(List<ProductionItemMng_ProductionItemGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemGroup_View>, List<DTO.ProductionItemGroup>>(dbItems);
        }

        public List<DTO.ProductionItemType> DB2DTO_ProductionItemType(List<ProductionItemMng_ProductionItemType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemType_View>, List<DTO.ProductionItemType>>(dbItems);
        }

        public List<DTO.ProductionItemSpec> DB2DTO_ProductionItemSpec(List<ProductionItemMng_ProductionItemSpec_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemSpec_View>, List<DTO.ProductionItemSpec>>(dbItems);
        }

        public List<DTO.AssetClassDTO> DB2DTO_AssetClassItem(List<ProductionItemMng_AssetClass_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_AssetClass_View>, List<DTO.AssetClassDTO>>(dbItems);
        }

        public List<DTO.DepreciationTypeDTO> DB2DTO_DepreciationTypeItem(List<ProductionItemMng_DepreciationType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_DepreciationType_View>, List<DTO.DepreciationTypeDTO>>(dbItems);
        }

        public List<DTO.ProductionItemTypeSupport> DB2DTO_ProductionItemType(List<SupportMng_ProductionItemType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductionItemType_View>, List<DTO.ProductionItemTypeSupport>>(dbItems);
        }

        public List<DTO.BreakDownCategoryDTO> DB2DTO_BreakDownCategory(List<ProductionItemMng_BreakDownCategory_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_BreakDownCategory_View>, List<DTO.BreakDownCategoryDTO>>(dbitems);
        }
        public void DTO2BD(DTO.ProductionItem dtoItem, ref ProductionItem dbItem, bool permissionApprove, int userId)
        {
            // ProductionItemWarehouse
            if (dtoItem.ProductionItemWarehouses != null)
            {
                foreach (var item in dbItem.ProductionItemWarehouse.ToArray())
                {
                    if (!dtoItem.ProductionItemWarehouses.Select(o => o.ProductionItemWarehouseID).Contains(item.ProductionItemWarehouseID))
                    {
                        dbItem.ProductionItemWarehouse.Remove(item);
                    }
                }

                //map child row
                foreach (var item in dtoItem.ProductionItemWarehouses)
                {
                    ProductionItemWarehouse dbProductionItemWarehouse;
                    if (item.ProductionItemWarehouseID <= 0)
                    {
                        dbProductionItemWarehouse = new ProductionItemWarehouse();
                        dbItem.ProductionItemWarehouse.Add(dbProductionItemWarehouse);
                    }
                    else
                    {
                        dbProductionItemWarehouse = dbItem.ProductionItemWarehouse.FirstOrDefault(o => o.ProductionItemWarehouseID == item.ProductionItemWarehouseID);
                    }
                    if (dbProductionItemWarehouse != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductionItemWarehouse, ProductionItemWarehouse>(item, dbProductionItemWarehouse);
                    }
                }
            }

            // ProductionItemVender
            if (dtoItem.ProductionItemVenders != null)
            {
                foreach (var item in dbItem.ProductionItemVender.ToArray())
                {
                    if (!dtoItem.ProductionItemVenders.Select(o => o.ProductionItemVenderID).Contains(item.ProductionItemVenderID))
                    {
                        dbItem.ProductionItemVender.Remove(item);
                    }
                }

                //map child row
                foreach (var item in dtoItem.ProductionItemVenders)
                {
                    ProductionItemVender dbProductionItemVender;
                    if (item.ProductionItemVenderID <= 0)
                    {
                        dbProductionItemVender = new ProductionItemVender();
                        dbItem.ProductionItemVender.Add(dbProductionItemVender);
                    }
                    else
                    {
                        dbProductionItemVender = dbItem.ProductionItemVender.FirstOrDefault(o => o.ProductionItemVenderID == item.ProductionItemVenderID);
                    }
                    if (dbProductionItemVender != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductionItemVender, ProductionItemVender>(item, dbProductionItemVender);
                    }
                }
            }

            //ProductionItemSubUnit
            if (dtoItem.productionItemSubUnitDTOs != null)
            {
                foreach (var item in dbItem.ProductionItemUnit.ToArray())
                {
                    if (!dtoItem.productionItemSubUnitDTOs.Select(o => o.ProductionItemUnitID).Contains(item.ProductionItemUnitID))
                    {
                        dbItem.ProductionItemUnit.Remove(item);
                    }
                }

                //map child row
                foreach (var item in dtoItem.productionItemSubUnitDTOs)
                {
                    ProductionItemUnit dbproductionItemUnit;
                    if (item.ProductionItemUnitID <= 0)
                    {
                        dbproductionItemUnit = new ProductionItemUnit();
                        dbItem.ProductionItemUnit.Add(dbproductionItemUnit);
                    }
                    else
                    {
                        dbproductionItemUnit = dbItem.ProductionItemUnit.FirstOrDefault(o => o.ProductionItemUnitID == item.ProductionItemUnitID);
                    }
                    if (dbproductionItemUnit != null)
                    {
                        if(item.ProductionItemUnitID < 0)
                        {
                            AutoMapper.Mapper.Map<DTO.ProductionItemSubUnitDTO, ProductionItemUnit>(item, dbproductionItemUnit);
                            dbproductionItemUnit.ValidFrom = item.ValidFrom.ConvertStringToDateTime();
                            dbproductionItemUnit.UpdatedBy = userId;
                            dbproductionItemUnit.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            if(permissionApprove)
                            {
                                var checkItem = dbItem.ProductionItemUnit.FirstOrDefault(o => o.ProductionItemUnitID == item.ProductionItemUnitID);
                                string ValidFromStr = checkItem.ValidFrom != null ? checkItem.ValidFrom.Value.ToString("dd/MM/yyyy") : "";
                                if (checkItem.ConversionFactor != item.ConversionFactor || ValidFromStr != item.ValidFrom) {
                                    AutoMapper.Mapper.Map<DTO.ProductionItemSubUnitDTO, ProductionItemUnit>(item, dbproductionItemUnit);
                                    dbproductionItemUnit.ValidFrom = item.ValidFrom.ConvertStringToDateTime();
                                    dbproductionItemUnit.UpdatedBy = userId;
                                    dbproductionItemUnit.UpdatedDate = DateTime.Now;
                                }
                            }
                            else
                            {
                                dbproductionItemUnit.Remark = item.Remark;
                            }
                        }
                    }
                }
            }
            //int? currentUnitID = dbItem.UnitID;
            AutoMapper.Mapper.Map<DTO.ProductionItem, ProductionItem>(dtoItem, dbItem);
            //if(dbItem.ProductionItemID > 0)
            //{
            //    dbItem.UnitID = currentUnitID;
            //}

            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
            dbItem.StartDate = dtoItem.StartDate.ConvertStringToDateTime();
            dbItem.EndDate = dtoItem.EndDate.ConvertStringToDateTime();
        }

        public List<DTO.FactoryRawMaterialSupplier> DB2DTO_FactoryRawMaterialSupplier(List<ProductionItemMng_FactoryRawMaterialSupplier_List> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_FactoryRawMaterialSupplier_List>, List<DTO.FactoryRawMaterialSupplier>>(dbItems);
        }

        public List<DTO.FactoryWarehouse> DB2DTO_FactoryWarehouse(List<SupportMng_FactoryWarehouse_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouse>>(dbItems);
        }
    }
}
