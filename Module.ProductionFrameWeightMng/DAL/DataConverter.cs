using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.ProductionFrameWeightMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductionFrameWeightMng_ProductionFrameWeight_View, DTO.ProductionFrameWeightData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.ProductionFrameWeightHistory, o => o.MapFrom(s => s.ProductionFrameWeightMng_ProductionFrameWeightHistory_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionFrameWeightMng_ProductionFrameWeightHistory_View, DTO.ProductionFrameWeightHistoryData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionFrameWeightData, ProductionFrameWeight>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionFrameWeightID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionFrameWeightHistory, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductionFrameWeightHistoryData, ProductionFrameWeightHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionFrameWeightHistoryID, o => o.Ignore())
                    .ForMember(d => d.ProductionFrameWeightID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductionFrameWeightMng_ProductionFrameWeightSearchResult_View, DTO.ProductionFrameWeightSearchResultData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionFrameWeightClient, o => o.MapFrom(s => s.ProductionFrameWeightMng_Client_View))
                    .ForMember(d => d.ProductionFrameWeightSaleOrder, o => o.MapFrom(s => s.ProductionFrameWeightMng_SaleOrder_View))
                    .ForMember(d => d.ProductionFrameWeightWorkOrder, o => o.MapFrom(s => s.ProductionFrameWeightMng_WorkOrder_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionFrameWeightMng_Client_View, DTO.ClientData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionFrameWeightMng_SaleOrder_View, DTO.SaleOrderData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionFrameWeightMng_WorkOrder_View, DTO.WorkOrderData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.ProductionFrameWeightData DB2DTO_ProductionFrameWeight(ProductionFrameWeightMng_ProductionFrameWeight_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionFrameWeightMng_ProductionFrameWeight_View, DTO.ProductionFrameWeightData>(dbItem);
        }

        public void DTO2DB_ProductionFrameWeight(int userID, DTO.ProductionFrameWeightData dtoItem, ref ProductionFrameWeight dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductionFrameWeightData, ProductionFrameWeight>(dtoItem, dbItem);

            dbItem.UpdatedBy = userID;
            dbItem.UpdatedDate = DateTime.Now;

            if (dtoItem.ProductionFrameWeightHistory != null)
            {
                foreach (var dtoItemDetail in dbItem.ProductionFrameWeightHistory.ToList())
                {
                    if (!dtoItem.ProductionFrameWeightHistory.Select(s => s.ProductionFrameWeightHistoryID).Contains(dtoItemDetail.ProductionFrameWeightHistoryID))
                    {
                        dbItem.ProductionFrameWeightHistory.Remove(dtoItemDetail);
                    }
                }

                foreach (var dtoItemDetail in dtoItem.ProductionFrameWeightHistory.ToList())
                {
                    ProductionFrameWeightHistory dbItemDetail;

                    if (dtoItemDetail.ProductionFrameWeightHistoryID <= 0)
                    {
                        dbItemDetail = new ProductionFrameWeightHistory();
                        dbItem.ProductionFrameWeightHistory.Add(dbItemDetail);

                        if (dbItemDetail != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ProductionFrameWeightHistoryData, ProductionFrameWeightHistory>(dtoItemDetail, dbItemDetail);
                            dbItemDetail.UpdatedBy = userID;
                            dbItemDetail.UpdatedDate = DateTime.Now;
                            dbItemDetail.Remark = dtoItem.Remark;
                        }
                    }
                    else
                    {
                        dbItemDetail = dbItem.ProductionFrameWeightHistory.FirstOrDefault(o => o.ProductionFrameWeightHistoryID == dtoItemDetail.ProductionFrameWeightHistoryID);
                    }
                }
            }
        }

        public List<DTO.ProductionFrameWeightSearchResultData> DB2DTO_ProductionFrameWeightSearchResult(List<ProductionFrameWeightMng_ProductionFrameWeightSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductionFrameWeightMng_ProductionFrameWeightSearchResult_View>, List<DTO.ProductionFrameWeightSearchResultData>>(dbItem);
        }
    }
}
