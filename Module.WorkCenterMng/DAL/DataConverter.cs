using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WorkCenterMng";

            AutoMapper.Mapper.CreateMap<WorkCenterMng_WorkCenter_View, DTO.WorkCenterDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.WorkCenterDetails, o => o.MapFrom(s => s.WorkCenterMng_WorkCenterDetail_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<WorkCenterMng_WorkCenterDetail_View, DTO.WorkCenterDetailDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.WorkCenterDto, DAL.WorkCenter>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.WorkCenterID, o => o.Ignore())
                .ForMember(d => d.WorkCenterDetail, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.WorkCenterDetailDTO, WorkCenterDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.WorkCenterDetailID, o => o.Ignore())
                .ForMember(d => d.WorkCenterID, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<WorkCenterMng_Branch_View, DTO.BranchDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<WorkCenterMng_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<WorkCenterMng_WorkCenterSearchResultVirtual_View, DTO.WorkCenterSearchResultDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<WorkCenterMng_WorkCenterDetailSearchResultVirtual_View, DTO.WorkCenterDetailSearchResultDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WorkCenterSearchResultDTO> DB2DTO_WorkCenterSearchResult(List<DAL.WorkCenterMng_WorkCenterSearchResultVirtual_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<DAL.WorkCenterMng_WorkCenterSearchResultVirtual_View>, List<DTO.WorkCenterSearchResultDTO>>(dbItem);
        }

        public DTO.WorkCenterDto DB2DTO_WorkCenter(DAL.WorkCenterMng_WorkCenter_View dbItem)
        {
            return AutoMapper.Mapper.Map<DAL.WorkCenterMng_WorkCenter_View, DTO.WorkCenterDto>(dbItem);
        }

        public void DTO2DB_WorkCenter(int userID, DTO.WorkCenterDto dtoItem, ref DAL.WorkCenter dbItem)
        {
            // Mapping workcenter.
            AutoMapper.Mapper.Map<DTO.WorkCenterDto, DAL.WorkCenter>(dtoItem, dbItem);

            // Mapping workcenter details.
            if (dtoItem.WorkCenterDetails != null)
            {
                foreach (WorkCenterDetail dbWorkCenterDetail in dbItem.WorkCenterDetail.ToList())
                {
                    if (!dtoItem.WorkCenterDetails.Select(s => s.WorkCenterDetailID).Contains(dbWorkCenterDetail.WorkCenterDetailID))
                    {
                        dbItem.WorkCenterDetail.Remove(dbWorkCenterDetail);
                    }
                }

                foreach (DTO.WorkCenterDetailDTO dtoWorkCenterDetail in dtoItem.WorkCenterDetails.ToList())
                {
                    WorkCenterDetail dbWorkCenterDetail;

                    if (dtoWorkCenterDetail.WorkCenterDetailID < 0)
                    {
                        dbWorkCenterDetail = new WorkCenterDetail();
                        dbItem.WorkCenterDetail.Add(dbWorkCenterDetail);
                    }
                    else
                    {
                        dbWorkCenterDetail = dbItem.WorkCenterDetail.FirstOrDefault(o => o.WorkCenterDetailID == dtoWorkCenterDetail.WorkCenterDetailID);
                    }

                    if (dbWorkCenterDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WorkCenterDetailDTO, WorkCenterDetail>(dtoWorkCenterDetail, dbWorkCenterDetail);

                        dbWorkCenterDetail.UpdatedBy = userID;
                        dbWorkCenterDetail.UpdatedDate = DateTime.Now;

                        if (dtoWorkCenterDetail.WorkCenterDetailID <= 0)
                        {
                            dbWorkCenterDetail.CreatedBy = userID;
                            dbWorkCenterDetail.CreatedDate = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}
