using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.TransportOffer.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "TransportOfferMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<TransportOfferMng_TransportOffer_SearchView, DTO.TransportOfferSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportOfferMng_TransportOffer_View, DTO.TransportOfferDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.TransportOfferConditionDetailDTOs, o => o.MapFrom(s => s.TransportOfferMng_TransportOfferConditionDetail_View))
                    .ForMember(d => d.TransportOfferCostDetailDTOs, o => o.MapFrom(s => s.TransportOfferMng_TransportOfferCostDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportOfferMng_TransportOfferConditionDetail_View, DTO.TransportOfferConditionDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportOfferMng_TransportOfferCostDetail_View, DTO.TransportOfferCostDetailDTO>()
                   .IgnoreAllNonExisting()
                   //.ForMember(d => d.POLID, o => o.ResolveUsing(s => s.POLID.HasValue && s.POLID.Value == -1 ? null : s.POLID))
                   //.ForMember(d => d.PODID, o => o.ResolveUsing(s => s.PODID.HasValue && s.PODID.Value == -1 ? null : s.PODID))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.TransportOfferConditionDetailDTO, TransportOfferConditionDetail>()
                   .IgnoreAllNonExisting()
                   //.ForMember(d => d.POLID, o => o.ResolveUsing(s => s.POLID.HasValue && s.POLID.Value == -1 ? null : s.POLID))
                   //.ForMember(d => d.PODID, o => o.ResolveUsing(s => s.PODID.HasValue && s.PODID.Value == -1 ? null : s.PODID))
                   .ForMember(d => d.TransportOfferConditionDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TransportOfferCostDetailDTO, TransportOfferCostDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.TransportOfferCostDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TransportOfferDTO, TransportOffer>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.ValidFrom, o => o.Ignore())
                   .ForMember(d => d.ValidTo, o => o.Ignore())
                   .ForMember(d => d.TransportOfferConditionDetail, o => o.Ignore())
                   .ForMember(d => d.TransportOfferCostDetail, o => o.Ignore())
                   .ForMember(d => d.TransportOfferID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<TransportOfferMng_TransportCostItem_View, DTO.TransportCostItemDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportOfferMng_TransportConditionItem_View, DTO.TransportConditionItemDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.TransportCostItemDTO, TransportCostItem>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.TransportCostItemID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TransportConditionItemDTO, TransportConditionItem>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.TransportConditionItemID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.TransportOfferSearchDTO> DB2DTO_TransportOfferSearch(List<TransportOfferMng_TransportOffer_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TransportOfferMng_TransportOffer_SearchView>, List<DTO.TransportOfferSearchDTO>>(dbItems);
        }

        public DTO.TransportOfferDTO DB2DTO_TransportOffer(TransportOfferMng_TransportOffer_View dbItem)
        {
            return AutoMapper.Mapper.Map<TransportOfferMng_TransportOffer_View, DTO.TransportOfferDTO>(dbItem);
        }

        public void DTO2DB_TransportOffer(DTO.TransportOfferDTO dtoItem, ref TransportOffer dbItem)
        {
            if (dtoItem.TransportOfferConditionDetailDTOs != null)
            {
                foreach (var item in dbItem.TransportOfferConditionDetail.ToArray())
                {
                    if (!dtoItem.TransportOfferConditionDetailDTOs.Select(s => s.TransportOfferConditionDetailID).Contains(item.TransportOfferConditionDetailID))
                    {
                        dbItem.TransportOfferConditionDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.TransportOfferConditionDetailDTOs)
                {
                    if (item.POLID.HasValue && item.POLID.Value == -1)
                    {
                        item.POLID = null;
                    }
                    if (item.PODID.HasValue && item.PODID == -1)
                    {
                        item.PODID = null;
                    }
                    TransportOfferConditionDetail dbDetail = new TransportOfferConditionDetail();
                    if (item.TransportOfferConditionDetailID < 0)
                    {
                        if (item.TransportConditionItemID.HasValue)
                        {
                            dbItem.TransportOfferConditionDetail.Add(dbDetail);
                        }                        
                    }
                    else
                    {
                        dbDetail = dbItem.TransportOfferConditionDetail.Where(o => o.TransportOfferConditionDetailID == item.TransportOfferConditionDetailID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.TransportOfferConditionDetailDTO, TransportOfferConditionDetail>(item, dbDetail);
                    }
                }
            }
            if (dtoItem.TransportOfferCostDetailDTOs != null)
            {
                foreach (var item in dbItem.TransportOfferCostDetail.ToArray())
                {
                    if (!dtoItem.TransportOfferCostDetailDTOs.Select(s => s.TransportOfferCostDetailID).Contains(item.TransportOfferCostDetailID))
                    {
                        dbItem.TransportOfferCostDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.TransportOfferCostDetailDTOs)
                {
                    if (item.POLID.HasValue && item.POLID.Value == -1)
                    {
                        item.POLID = null;
                    }
                    if (item.PODID.HasValue && item.PODID == -1)
                    {
                        item.PODID = null;
                    }
                    TransportOfferCostDetail dbDetail = new TransportOfferCostDetail();
                    if (item.TransportOfferCostDetailID < 0)
                    {
                        if (item.TransportCostItemID.HasValue)
                        {
                            dbItem.TransportOfferCostDetail.Add(dbDetail);
                        }                        
                    }
                    else
                    {
                        dbDetail = dbItem.TransportOfferCostDetail.Where(o => o.TransportOfferCostDetailID == item.TransportOfferCostDetailID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.TransportOfferCostDetailDTO, TransportOfferCostDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.TransportOfferDTO, TransportOffer>(dtoItem, dbItem);
            dbItem.ValidFrom = dtoItem.ValidFrom.ConvertStringToDateTime();
            dbItem.ValidTo = dtoItem.ValidTo.ConvertStringToDateTime();
            dbItem.UpdatedDate = DateTime.Now;
        }

        
    }
}
