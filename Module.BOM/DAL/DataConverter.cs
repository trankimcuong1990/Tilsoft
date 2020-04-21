using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.BOM.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "BOMMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<BOMMng_BOM_View, DTO.BOMDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BOMDTOs, o => o.MapFrom(s => s.BOMMng_BOM_View1))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ProductThumbnailImageUrl, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductThumbnailImageUrl) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ProductThumbnailImageUrl)))
                    .ForMember(d => d.ProductFullSizeImageUrl, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductFullSizeImageUrl) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ProductFullSizeImageUrl)))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ProductionItemUnitDTOs, o => o.MapFrom(s => s.BOMMng_ProductionItemUnitInBOM_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.BOMDTO, BOM>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Quantity, o => o.ResolveUsing(s => s.QtyByUnit * s.ConversionFactor))
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.DeletedDate, o => o.Ignore())
                    .ForMember(d => d.BOM1, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<BOMMng_ProductionItem_View, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailLocation) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ThumbnailLocation)))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileLocation)))
                    .ForMember(d => d.ProductionItemUnitDTOs, o => o.MapFrom(s => s.BOMMng_ProductionItemUnit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemDTO, ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.UseValue(DateTime.Now))
                    .ForMember(d => d.ProductionItemID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<BOMStandard, BOM>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.IsActive, o => o.UseValue(true))
                    .ForMember(d => d.RevisionNumber, o => o.UseValue(1))
                    .ForMember(d => d.BOM1, o => o.MapFrom(s => s.BOMStandard1))
                    .ForMember(d => d.BOMID, o => o.ResolveUsing(s => -1 * s.BOMStandardID))
                    .ForMember(d => d.ParentBOMID, o => o.ResolveUsing(s => -1 * s.ParentBOMStandardID))
                    ;

                AutoMapper.Mapper.CreateMap<BOM, BOMStandard>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BOMStandard1, o => o.MapFrom(s => s.BOM1))
                    .ForMember(d => d.BOMStandardID, o => o.ResolveUsing(s => -1 * s.BOMID))
                     .ForMember(d => d.ParentBOMStandardID, o => o.ResolveUsing(s => -1 * s.ParentBOMID))
                    ;

                AutoMapper.Mapper.CreateMap<BOM, BOM>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.IsActive, o => o.UseValue(true))
                    .ForMember(d => d.RevisionNumber, o => o.ResolveUsing(s => s.RevisionNumber.HasValue ? s.RevisionNumber.Value + 1 : 0))
                    .ForMember(d => d.BOM1, o => o.MapFrom(s => s.BOM1))
                    .ForMember(d => d.BOMID, o => o.ResolveUsing(s => -1 * s.BOMID))
                    .ForMember(d => d.ParentBOMID, o => o.ResolveUsing(s => -1 * s.ParentBOMID))
                    ;

                AutoMapper.Mapper.CreateMap<BOMMng_ProductionProcess_View, DTO.WorkOrderAndProductionProcessForm.ProductionProcessDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMMng_CreateImportTemplate_BOMStandard_View, DTO.ImportTemplate.BOMStandardSourceDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ProductionItemUnitDTOs, o => o.MapFrom(s => s.BOMMng_ProductionItemUnitInBOMStandard_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMMng_ProductionItemUnit_View, DTO.ProductionItemUnitDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMMng_ProductionItemUnitInBOM_View, DTO.ProductionItemUnitDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMMng_ProductionItemUnitInBOMStandard_View, DTO.ImportTemplate.ProductionItemUnitDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BOMMng_DateOfPrice_View, DTO.DatePrices>()
                 .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<BOMMng_ProductionPrice_View, DTO.PriceList>()
                 .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<BOMMng_WorkOrderOPSequence_View, DTO.WorkOrderOPSequenceDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<BOMMng_SupportWorkOrderOPSequence_View, DTO.SupportWorkOrderOPSequenceDTO>()
                    .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public DTO.BOMDTO DB2DTO_BOM(BOMMng_BOM_View dbItem)
        {
            return AutoMapper.Mapper.Map<BOMMng_BOM_View, DTO.BOMDTO>(dbItem);
        }

        public void DTO2DB_BOM(DTO.BOMDTO dtoItem, ref BOM dbItem, int? workOrderStatusID, List<BOMMng_BOMHasArising_View> bomHasArising)
        {
            BOM dbBOM;
            BOM dbParentBOM;
            if (dtoItem.ParentBOMID.HasValue)
            {
                if (dtoItem.BOMID < 0)
                {
                    dbBOM = new BOM();
                    dbParentBOM = new BOM();
                    GetDbItemByBOMID(dtoItem.ParentBOMID, dbItem, ref dbParentBOM);
                    dbParentBOM.BOM1.Add(dbBOM);
                    AutoMapper.Mapper.Map<DTO.BOMDTO, BOM>(dtoItem, dbBOM);
                }
                else
                {
                    dbBOM = new BOM();
                    GetDbItemByBOMID(dtoItem.BOMID, dbItem, ref dbBOM);

                    bool hasArising = bomHasArising.Where(o => o.BOMID == dbBOM.BOMID).FirstOrDefault() != null;
                    
                    //only allow edit BOM incase workOrder did not confirmed or confirm frame
                    if (!workOrderStatusID.HasValue 
                        || workOrderStatusID.Value == 1 
                        || (workOrderStatusID == 5 && !hasArising)
                        || (workOrderStatusID == 2 && !hasArising)
                        ) //1: Open, 2:Confirm, 3: Finish, 4: Cancel, 5: Confirm Frame
                    {
                        AutoMapper.Mapper.Map<DTO.BOMDTO, BOM>(dtoItem, dbBOM);
                    }

                    //at this time allow edit quantity of BOM, when new season come in, we will lock this feature
                    dbBOM.QtyByUnit = dtoItem.QtyByUnit;
                    dbBOM.Quantity = dtoItem.QtyByUnit * dtoItem.ConversionFactor;
                    dbBOM.WastagePercent = dtoItem.WastagePercent;
                    dbBOM.Description = dtoItem.Description;
                    dbBOM.UnitID = dtoItem.UnitID;
                }
            }
            else
            {
                AutoMapper.Mapper.Map<DTO.BOMDTO, BOM>(dtoItem, dbItem);
            }
            foreach (var item in dtoItem.BOMDTOs)
            {
                DTO2DB_BOM(item, ref dbItem, workOrderStatusID, bomHasArising);
            }
        }

        private void GetDbItemByBOMID(int? id, BOM dbBOM, ref BOM dbBOMResult)
        {
            if (dbBOM.BOMID == id)
            {
                dbBOMResult = dbBOM;
            }
            else
            {
                foreach (var item in dbBOM.BOM1)
                {
                    GetDbItemByBOMID(id, item, ref dbBOMResult);
                }
            }
        }

        public DTO.ProductionItemDTO DB2DTO_ProductionItem(BOMMng_ProductionItem_View dbItem)
        {
            return AutoMapper.Mapper.Map<BOMMng_ProductionItem_View, DTO.ProductionItemDTO>(dbItem);
        }

        public void DTO2DB_ProductionItem(DTO.ProductionItemDTO dtoItem, ref ProductionItem dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductionItemDTO, ProductionItem>(dtoItem, dbItem);
        }

        //public void DB2DB_BOM(BOMStandard dbBOMStandard, ref BOM dbItem)
        //{
        //    BOM dbBOM = new BOM();
        //    BOM dbParentBOM = new BOM();

        //    getDbItemByBOMID(dbBOMStandard.BOMStandardID, dbItem, ref dbParentBOM);
        //    dbParentBOM.BOM1.Add(dbBOM);
        //    AutoMapper.Mapper.Map<BOMStandard, BOM>(dbBOMStandard, dbBOM);
        //    foreach (var item in dbBOMStandard.BOMStandard1)
        //    {
        //        DB2DB_BOM(item, ref dbItem);
        //    }
        //}

        public void DB2DB_BOM(BOMStandard dbBOMStandard, ref BOM dbBOM)
        {
            AutoMapper.Mapper.Map<BOMStandard, BOM>(dbBOMStandard, dbBOM);
        }

        public void DB2DB_BOMStandard(BOM dbBOM, ref BOMStandard dbBOMStandard)
        {
            AutoMapper.Mapper.Map<BOM, BOMStandard>(dbBOM, dbBOMStandard);
        }

        public void DB2DB_NewVersionForBOM(BOM dbOriginBOM, ref BOM dbBOM)
        {
            AutoMapper.Mapper.Map<BOM, BOM>(dbOriginBOM, dbBOM);
        }
        public List<DTO.DatePrices> DB2DTO_Date(List<BOMMng_DateOfPrice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BOMMng_DateOfPrice_View>, List<DTO.DatePrices>>(dbItems);
        }
        public List<DTO.PriceList> DB2DTO_PriceList(List<BOMMng_ProductionPrice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BOMMng_ProductionPrice_View>, List<DTO.PriceList>>(dbItems);
        }
    }
}
