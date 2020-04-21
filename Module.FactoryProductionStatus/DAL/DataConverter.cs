using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryProductionStatus.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryProductionStatusMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_FactoryProductionStatus_SearchView, DTO.FactoryProductionStatusSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StatusDate, o => o.ResolveUsing(s => s.StatusDate.HasValue ? s.StatusDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_FactoryProductionStatusOrderDetail_View, DTO.FactoryProductionStatusOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_FactoryProductionStatusDetail_View, DTO.FactoryProductionStatusDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryProductionStatusOrderDetailDTOs, o => o.MapFrom(s => s.FactoryProductionStatusMng_FactoryProductionStatusOrderDetail_View.OrderBy(x => x.Description)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_FactoryProductionStatus_View, DTO.FactoryProductionStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StatusDate, o => o.ResolveUsing(s => s.StatusDate.HasValue ? s.StatusDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryProductionStatusDetailDTOs, o => o.MapFrom(s => s.FactoryProductionStatusMng_FactoryProductionStatusDetail_View.OrderBy(x =>x.LDS)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryProductionStatusOrderDetailDTO, FactoryProductionStatusOrderDetail>()
                   .IgnoreAllNonExisting()                   
                   .ForMember(d => d.FactoryProductionStatusOrderDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryProductionStatusDetailDTO, FactoryProductionStatusDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryProductionStatusOrderDetail, o => o.Ignore())
                   .ForMember(d => d.FactoryProductionStatusDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryProductionStatusDTO, FactoryProductionStatus>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.FactoryProductionStatusDetail, o => o.Ignore())
                   .ForMember(d => d.FactoryProductionStatusID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_Order_View, DTO.Order>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_OrderDetail_View, DTO.OrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_ProductionByWeek_View, DTO.ProductionByWeek>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryProductionStatusMng_FactoryCapacity_View, DTO.FactoryCapacity>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryProductionStatusSearchDTO> DB2DTO_FactoryProductionStatusSearch(List<FactoryProductionStatusMng_FactoryProductionStatus_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProductionStatusMng_FactoryProductionStatus_SearchView>, List<DTO.FactoryProductionStatusSearchDTO>>(dbItems);
        }

        public DTO.FactoryProductionStatusDTO DB2DTO_FactoryProductionStatus(FactoryProductionStatusMng_FactoryProductionStatus_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryProductionStatusMng_FactoryProductionStatus_View, DTO.FactoryProductionStatusDTO>(dbItem);
        }

        public void DTO2DB_FactoryProductionStatus(DTO.FactoryProductionStatusDTO dtoItem, ref FactoryProductionStatus dbItem)
        {
            if (dtoItem.FactoryProductionStatusDetailDTOs != null)
            {
                //delete
                foreach (var item in dbItem.FactoryProductionStatusDetail.ToArray())
                {
                    if (!dtoItem.FactoryProductionStatusDetailDTOs.Select(s => s.FactoryProductionStatusDetailID).Contains(item.FactoryProductionStatusDetailID))
                    {
                        foreach (var sItem in item.FactoryProductionStatusOrderDetail.ToArray())
                        {
                            item.FactoryProductionStatusOrderDetail.Remove(sItem);
                        }
                        dbItem.FactoryProductionStatusDetail.Remove(item);
                    }
                    else
                    {
                        foreach (var sItem in item.FactoryProductionStatusOrderDetail.ToArray())
                        {
                            var x = dtoItem.FactoryProductionStatusDetailDTOs.Where(o => o.FactoryProductionStatusDetailID == item.FactoryProductionStatusDetailID).FirstOrDefault().FactoryProductionStatusOrderDetailDTOs;
                            if (!x.Select(s => s.FactoryProductionStatusOrderDetailID).Contains(sItem.FactoryProductionStatusOrderDetailID))
                            {
                                item.FactoryProductionStatusOrderDetail.Remove(sItem);
                            }
                        }
                    }
                }

                //update
                FactoryProductionStatusDetail dbDetail;
                FactoryProductionStatusOrderDetail dbOrderDetail;
                foreach (var item in dtoItem.FactoryProductionStatusDetailDTOs)
                {
                    if (item.FactoryProductionStatusDetailID < 0)
                    {
                        dbDetail = new FactoryProductionStatusDetail();
                        dbItem.FactoryProductionStatusDetail.Add(dbDetail);
                        if (item.FactoryProductionStatusOrderDetailDTOs != null)
                        {
                            foreach (var sItem in item.FactoryProductionStatusOrderDetailDTOs)
                            {
                                dbOrderDetail = new FactoryProductionStatusOrderDetail();
                                dbDetail.FactoryProductionStatusOrderDetail.Add(dbOrderDetail);
                                AutoMapper.Mapper.Map<DTO.FactoryProductionStatusOrderDetailDTO, FactoryProductionStatusOrderDetail>(sItem, dbOrderDetail);
                            }
                        }
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryProductionStatusDetail.Where(o => o.FactoryProductionStatusDetailID == item.FactoryProductionStatusDetailID).FirstOrDefault();
                        if (dbDetail != null && item.FactoryProductionStatusOrderDetailDTOs != null)
                        {
                            foreach (var sItem in item.FactoryProductionStatusOrderDetailDTOs)
                            {
                                if (sItem.FactoryProductionStatusOrderDetailID > 0)
                                {
                                    dbOrderDetail = dbDetail.FactoryProductionStatusOrderDetail.Where(o => o.FactoryProductionStatusOrderDetailID == sItem.FactoryProductionStatusOrderDetailID).FirstOrDefault();
                                }
                                else
                                {
                                    dbOrderDetail = new FactoryProductionStatusOrderDetail();
                                    dbDetail.FactoryProductionStatusOrderDetail.Add(dbOrderDetail);
                                }
                                if (dbOrderDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.FactoryProductionStatusOrderDetailDTO, FactoryProductionStatusOrderDetail>(sItem, dbOrderDetail);
                                }
                            }
                        }
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryProductionStatusDetailDTO, FactoryProductionStatusDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryProductionStatusDTO, FactoryProductionStatus>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }

        public List<DTO.Order> DB2DTO_Order(List<FactoryProductionStatusMng_Order_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProductionStatusMng_Order_View>, List<DTO.Order>>(dbItems);
        }

        public List<DTO.OrderDetail> DB2DTO_OrderDetail(List<FactoryProductionStatusMng_OrderDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProductionStatusMng_OrderDetail_View>, List<DTO.OrderDetail>>(dbItems);
        }

        public List<DTO.ProductionByWeek> DB2DTO_ProductionByWeek(List<FactoryProductionStatusMng_ProductionByWeek_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProductionStatusMng_ProductionByWeek_View>, List<DTO.ProductionByWeek>>(dbItems);
        }

        public List<DTO.FactoryCapacity> DB2DTO_FactoryCapacity(List<FactoryProductionStatusMng_FactoryCapacity_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryProductionStatusMng_FactoryCapacity_View>, List<DTO.FactoryCapacity>>(dbItems);
        }


    }
}
