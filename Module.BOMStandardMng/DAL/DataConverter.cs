using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.BOMStandardMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "BOMStandardMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<BOMStandardMng_BOMStandard_View, DTO.BOMStandardDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BOMStandardDTOs, o => o.MapFrom(s => s.BOMStandardMng_BOMStandard_View1))
                    .ForMember(d => d.ProductionItemUnitDTOs, o => o.MapFrom(s => s.BOMStandardMng_ProductionItemUnitInBOMStandard_View))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMStandardMng_BOMStandardView_View, DTO.BOMStandardViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BOMStandardViewDTOs, o => o.MapFrom(s => s.BOMStandardMng_BOMStandardView_View1))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.BOMStandardDTO, BOMStandard>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Quantity, o => o.ResolveUsing(s => s.QtyByUnit * s.ConversionFactor))
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.DeletedDate, o => o.Ignore())
                    .ForMember(d => d.BOMStandard1, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<BOMStandardMng_ProductionItem_View, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionItemUnitDTOs, o => o.MapFrom(s => s.BOMStandardMng_ProductionItemUnit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMStandardMng_CreateImportTemplate_BOMStandard_View, DTO.ImportTemplate.BOMStandardSourceDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ProductionItemUnitDTOs, o => o.MapFrom(s => s.BOMStandardMng_CreateImportTemplate_ProductionItemUnit_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMStandardMng_ProductionProcess_SearchView, DTO.ProductionProcessSearchDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMStandardMng_ProductionProcess_View, DTO.ProductionProcessDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionProcessDTO, ProductionProcess>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionProcessID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<BOMStandardMng_ProductionItemUnit_View, DTO.ProductionItemUnitDTO>()
                   .IgnoreAllNonExisting()                   
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMStandardMng_ProductionItemUnitInBOMStandard_View, DTO.ProductionItemUnitDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMStandardMng_CreateImportTemplate_ProductionItemUnit_View, DTO.ImportTemplate.ProductionItemUnitDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMStandardMng_DateOfPrice_View, DTO.DateProductionPrice>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<BOMStandardMng_ProductionPrice_View, DTO.PriceList>()
                   .IgnoreAllNonExisting();
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public DTO.BOMStandardDTO DB2DTO_BOMStandard(BOMStandardMng_BOMStandard_View dbItem)
        {
            return AutoMapper.Mapper.Map<BOMStandardMng_BOMStandard_View, DTO.BOMStandardDTO>(dbItem);
        }

        public DTO.BOMStandardViewDTO DB2DTO_BOMStandardView(BOMStandardMng_BOMStandardView_View dbItem)
        {
            return AutoMapper.Mapper.Map<BOMStandardMng_BOMStandardView_View, DTO.BOMStandardViewDTO>(dbItem);
        }

        public void DTO2DB_BOMStandard(DTO.BOMStandardDTO dtoItem, ref BOMStandard dbItem)
        {
            BOMStandard dbBOMStandard;
            BOMStandard dbParentBOMStandard;
            if (dtoItem.ParentBOMStandardID.HasValue)
            {
                if (dtoItem.BOMStandardID < 0)
                {
                    dbBOMStandard = new BOMStandard();
                    dbParentBOMStandard = new BOMStandard();
                    GetDbItemByBOMStandardID(dtoItem.ParentBOMStandardID, dbItem, ref dbParentBOMStandard);
                    dbParentBOMStandard.BOMStandard1.Add(dbBOMStandard);
                    AutoMapper.Mapper.Map<DTO.BOMStandardDTO, BOMStandard>(dtoItem, dbBOMStandard);
                }
                else
                {
                    dbBOMStandard = new BOMStandard();
                    GetDbItemByBOMStandardID(dtoItem.BOMStandardID, dbItem, ref dbBOMStandard);
                    AutoMapper.Mapper.Map<DTO.BOMStandardDTO, BOMStandard>(dtoItem, dbBOMStandard);
                }
            }
            else
            {
                AutoMapper.Mapper.Map<DTO.BOMStandardDTO, BOMStandard>(dtoItem, dbItem);
            }
            foreach (var item in dtoItem.BOMStandardDTOs)
            {
                DTO2DB_BOMStandard(item, ref dbItem);
            }
        }
        private void GetDbItemByBOMStandardID(int? id, BOMStandard dbBOMStandard, ref BOMStandard dbBOMStandardResult)
        {
            if (dbBOMStandard.BOMStandardID == id)
            {
                dbBOMStandardResult = dbBOMStandard;
            }
            else
            {
                foreach (var item in dbBOMStandard.BOMStandard1)
                {
                    GetDbItemByBOMStandardID(id, item, ref dbBOMStandardResult);
                }
            }
        }
        public List<DTO.ProductionProcessSearchDTO> DB2DTO_ProductionProcessSearch(List<BOMStandardMng_ProductionProcess_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BOMStandardMng_ProductionProcess_SearchView>, List<DTO.ProductionProcessSearchDTO>>(dbItems);
        }

        public List<DTO.DateProductionPrice> DB2DTO_Date(List<BOMStandardMng_DateOfPrice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BOMStandardMng_DateOfPrice_View>, List<DTO.DateProductionPrice>>(dbItems);
        }
        public List<DTO.PriceList> DB2DTO_PriceList(List<BOMStandardMng_ProductionPrice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BOMStandardMng_ProductionPrice_View>, List<DTO.PriceList>>(dbItems);
        }
    }
}
