using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.ProductionStatisticsMng.DAL
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
                AutoMapper.Mapper.CreateMap<ProductionStatisticsMng_ProductionStatisticsSearch_View, DTO.ProductionStatisticsSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProducedDate, o => o.ResolveUsing(s => s.ProducedDate.HasValue ? s.ProducedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionStatisticsMng_ProductionStatisticsDetail_View, DTO.ProductionStatisticsDetailDTO>()
                    .IgnoreAllNonExisting()
                     .ForMember(d => d.StartingTime, o => o.ResolveUsing(s => s.StartingTime.HasValue ? s.StartingTime.Value.ToStringDateTime() : ""))
                    .ForMember(d => d.FinishingTime, o => o.ResolveUsing(s => s.FinishingTime.HasValue ? s.FinishingTime.Value.ToStringDateTime() : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionStatisticsMng_ProductionStatistics_View, DTO.ProductionStatisticsDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProducedDate, o => o.ResolveUsing(s => s.ProducedDate.HasValue ? s.ProducedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ProductionStatisticsDetailDTOs, o => o.MapFrom(s => s.ProductionStatisticsMng_ProductionStatisticsDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionStatisticsDetailDTO, ProductionStatisticsDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartingTime, o => o.Ignore())
                    .ForMember(d => d.FinishingTime, o => o.Ignore())
                    .ForMember(d => d.ProductionStatisticsDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductionStatisticsDTO, ProductionStatistics>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProducedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionStatisticsDetail, o => o.Ignore())
                    .ForMember(d => d.ProductionStatisticsID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductionStatisticsMng_PlanningProductionTeam_View, DTO.PlanningProductionTeamDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionStatisticsMng_PlanningProductionTeam_View, DTO.ProductionStatisticsDetailDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkCenter, DTO.WorkCenterDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ProductionStatisticsSearchDTO> DB2DTO_Search(List<ProductionStatisticsMng_ProductionStatisticsSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionStatisticsMng_ProductionStatisticsSearch_View>, List<DTO.ProductionStatisticsSearchDTO>>(dbItems);
        }

        public DTO.ProductionStatisticsDTO DB2DTO_ProductionStatistics(ProductionStatisticsMng_ProductionStatistics_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionStatisticsMng_ProductionStatistics_View, DTO.ProductionStatisticsDTO>(dbItem);
        }

        public void DTO2DB_ProductionStatistics(DTO.ProductionStatisticsDTO dtoItem, ref ProductionStatistics dbItem, int? userId, int? companyID)
        {
            if (dtoItem.ProductionStatisticsDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.ProductionStatisticsDetail.ToArray())
                {
                    if (!dtoItem.ProductionStatisticsDetailDTOs.Select(s => s.ProductionStatisticsDetailID).Contains(item.ProductionStatisticsDetailID))
                    {
                        dbItem.ProductionStatisticsDetail.Remove(item);
                    }
                }
                //read from dto to db
                ProductionStatisticsDetail dbDetail;
                foreach (var item in dtoItem.ProductionStatisticsDetailDTOs)
                {
                    if (item.ProductionStatisticsDetailID > 0)
                    {
                        dbDetail = dbItem.ProductionStatisticsDetail.Where(o => o.ProductionStatisticsDetailID == item.ProductionStatisticsDetailID).FirstOrDefault();
                    }
                    else
                    {
                        dbDetail = new ProductionStatisticsDetail();
                        dbItem.ProductionStatisticsDetail.Add(dbDetail);
                    }
                    //read purchase request detail dto to db
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductionStatisticsDetailDTO, ProductionStatisticsDetail>(item, dbDetail);
                        if (!string.IsNullOrEmpty(item.StartingTime)) dbDetail.StartingTime = item.StartingTime.ConvertStringToDateTime();
                        if (!string.IsNullOrEmpty(item.FinishingTime)) dbDetail.FinishingTime = item.FinishingTime.ConvertStringToDateTime();
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.ProductionStatisticsDTO, ProductionStatistics>(dtoItem, dbItem);
            dbItem.ProducedDate = dtoItem.ProducedDate.ConvertStringToDateTime();
            dbItem.CompanyID = companyID;
            dbItem.UpdatedBy = userId;
            dbItem.UpdatedDate = DateTime.Now;
            if (dtoItem.ProductionStatisticsID == 0)
            {
                dbItem.CreatedBy = userId;
                dbItem.CreatedDate = DateTime.Now;
            }
        }

        
    }
}
