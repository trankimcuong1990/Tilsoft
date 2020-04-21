using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.ClientOfferMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientOfferMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientOffer_SearchView, DTO.ClientOfferSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientOffer_View, DTO.ClientOffer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ClientOfferConditionDetailDTOs, o => o.MapFrom(s => s.ClientOfferMng_ClientOfferConditionDetail_View))
                    .ForMember(d => d.ClientOfferCostDetailDTOs, o => o.MapFrom(s => s.ClientOfferMng_ClientOfferCostDetail_View))
                    .ForMember(d => d.ClientOfferAdditionalDetailDTOs, o => o.MapFrom(s => s.ClientOfferMng_ClientOfferAdditionalDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientOfferConditionDetail_View, DTO.ClientOfferConditionDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientOfferCostDetail_View, DTO.ClientOfferCostDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientOfferAdditionalDetail_View, DTO.ClientOfferAdditionalDetailDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientOfferConditionDetailDTO, ClientOfferConditionDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ClientOfferConditionDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ClientOfferCostDetailDTO, ClientOfferCostDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ClientOfferCostDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ClientOfferAdditionalDetailDTO, ClientOfferAdditionalDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ClientOfferAdditionalDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ClientOffer, ClientOffer>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.ValidFrom, o => o.Ignore())
                   .ForMember(d => d.ValidTo, o => o.Ignore())
                   .ForMember(d => d.ClientOfferConditionDetail, o => o.Ignore())
                   .ForMember(d => d.ClientOfferCostDetail, o => o.Ignore())
                   .ForMember(d => d.ClientOfferAdditionalDetail, o => o.Ignore())
                   .ForMember(d => d.ClientOfferID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientCostItem_View, DTO.ClientCostItemDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientConditionItem_View, DTO.ClientConditionItemDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientOfferMng_ClientAdditionalItem_View, DTO.ClientAdditionalItemDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientCostItemDTO, ClientCostItem>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ClientCostItemID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ClientConditionItemDTO, ClientConditionItem>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ClientConditionItemID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ClientAdditionalItemDTO, ClientAdditionalItem>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.ClientAdditionalItemID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ClientOfferSearch> DB2DTO_ClientOfferSearch(List<ClientOfferMng_ClientOffer_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientOfferMng_ClientOffer_SearchView>, List<DTO.ClientOfferSearch>>(dbItems);
        }

        public DTO.ClientOffer DB2DTO_ClientOffer(ClientOfferMng_ClientOffer_View dbItem)
        {
            return AutoMapper.Mapper.Map<ClientOfferMng_ClientOffer_View, DTO.ClientOffer>(dbItem);
        }

        public void DTO2DB_ClientOffer(DTO.ClientOffer dtoItem, ref ClientOffer dbItem)
        {
            if (dtoItem.ClientOfferCostDetailDTOs != null)
            {
                foreach (var item in dbItem.ClientOfferCostDetail.ToArray())
                {
                    if (!dtoItem.ClientOfferCostDetailDTOs.Select(s => s.ClientOfferCostDetailID).Contains(item.ClientOfferCostDetailID))
                    {
                        dbItem.ClientOfferCostDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.ClientOfferCostDetailDTOs)
                {
                    ClientOfferCostDetail dbDetail = new ClientOfferCostDetail();
                    if (item.ClientOfferCostDetailID < 0)
                    {
                        if (item.ClientCostItemID.HasValue)
                        {
                            dbItem.ClientOfferCostDetail.Add(dbDetail);
                        }
                    }
                    else
                    {
                        dbDetail = dbItem.ClientOfferCostDetail.Where(o => o.ClientOfferCostDetailID == item.ClientOfferCostDetailID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientOfferCostDetailDTO, ClientOfferCostDetail>(item, dbDetail);
                    }
                }
            }

            if (dtoItem.ClientOfferConditionDetailDTOs != null)
            {
                foreach (var item in dbItem.ClientOfferConditionDetail.ToArray())
                {
                    if (!dtoItem.ClientOfferConditionDetailDTOs.Select(s => s.ClientOfferConditionDetailID).Contains(item.ClientOfferConditionDetailID))
                    {
                        dbItem.ClientOfferConditionDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.ClientOfferConditionDetailDTOs)
                {
                    ClientOfferConditionDetail dbDetail = new ClientOfferConditionDetail();
                    if (item.ClientOfferConditionDetailID < 0)
                    {
                        if (item.ClientConditionItemID.HasValue)
                        {
                            dbItem.ClientOfferConditionDetail.Add(dbDetail);
                        }                     
                    }
                    else
                    {
                        dbDetail = dbItem.ClientOfferConditionDetail.Where(o => o.ClientOfferConditionDetailID == item.ClientOfferConditionDetailID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientOfferConditionDetailDTO, ClientOfferConditionDetail>(item, dbDetail);
                    }
                }
            }

            if (dtoItem.ClientOfferAdditionalDetailDTOs != null)
            {
                foreach (var item in dbItem.ClientOfferAdditionalDetail.ToArray())
                {
                    if (!dtoItem.ClientOfferAdditionalDetailDTOs.Select(s => s.ClientOfferAdditionalDetailID).Contains(item.ClientOfferAdditionalDetailID))
                    {
                        dbItem.ClientOfferAdditionalDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.ClientOfferAdditionalDetailDTOs)
                {
                    ClientOfferAdditionalDetail dbDetail = new ClientOfferAdditionalDetail();
                    if (item.ClientOfferAdditionalDetailID < 0)
                    {
                        dbItem.ClientOfferAdditionalDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.ClientOfferAdditionalDetail.Where(o => o.ClientOfferAdditionalDetailID == item.ClientOfferAdditionalDetailID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientOfferAdditionalDetailDTO, ClientOfferAdditionalDetail>(item, dbDetail);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.ClientOffer, ClientOffer>(dtoItem, dbItem);
            dbItem.ValidFrom = dtoItem.ValidFrom.ConvertStringToDateTime();
            dbItem.ValidTo = dtoItem.ValidTo.ConvertStringToDateTime();
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
