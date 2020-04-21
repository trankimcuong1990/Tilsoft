using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DraftPackingListMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DraftPackingList_DraftPackingList_SearchView, DTO.DraftPackingListSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PackingListDate, o => o.ResolveUsing(s => s.PackingListDate.HasValue ? s.PackingListDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DraftPackingList_InitInfo_View, DTO.InitInfo>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DraftPackingList_DraftPackingList_View, DTO.DraftPackingListDTO>()
                    .ForMember(d => d.DraftPackingListDetail, o => o.MapFrom(s => s.DraftPackingList_DraftPackingListDetail_View))
                    .ForMember(d => d.DraftPackingListSparepartDetail, o => o.MapFrom(s => s.DraftPackingList_DraftPackingListSparepartDetail_View))
                    .ForMember(d => d.PackingListDate, o => o.ResolveUsing(s => s.PackingListDate.HasValue ? s.PackingListDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftPackingList_DraftPackingListDetail_View, DTO.DraftPackingListDetail>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftPackingList_DraftPackingListSparepartDetail_View, DTO.DraftPackingListSparepartDetail>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DraftPackingList_NewInfoDetail_View, DTO.DraftPackingListDetail>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DraftPackingList_NewInfoSparepartDetail_View, DTO.DraftPackingListSparepartDetail>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DraftPackingList_NewInfo_View, DTO.DraftPackingListDTO>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.DraftPackingListDetail, o => o.MapFrom(s => s.DraftPackingList_NewInfoDetail_View))
                                .ForMember(d => d.DraftPackingListSparepartDetail, o => o.MapFrom(s => s.DraftPackingList_NewInfoSparepartDetail_View))
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DraftPackingListDTO, DraftPackingList>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.DraftPackingListID, o => o.Ignore())
                                .ForMember(d => d.DraftPackingListDetail, o => o.Ignore())
                                ;
                AutoMapper.Mapper.CreateMap<DTO.DraftPackingListDetail, DraftPackingListDetail>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.DraftPackingListID, o => o.Ignore())
                                ;
                AutoMapper.Mapper.CreateMap<DTO.DraftPackingListSparepartDetail, DraftPackingListDetail>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.DraftPackingListID, o => o.Ignore())
                                ;
            }
        }

        public List<DTO.DraftPackingListSearchResult> DB2DTO_PackingListSearch(List<DraftPackingList_DraftPackingList_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftPackingList_DraftPackingList_SearchView>, List<DTO.DraftPackingListSearchResult>>(dbItems);
        }

        public List<DTO.InitInfo> DB2DTO_InitInfos(List<DraftPackingList_InitInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftPackingList_InitInfo_View>, List<DTO.InitInfo>>(dbItems);
        }

        public DTO.DraftPackingListDTO DB2DTO_DraftPackingList(DraftPackingList_DraftPackingList_View dbItems)
        {
            return AutoMapper.Mapper.Map<DraftPackingList_DraftPackingList_View, DTO.DraftPackingListDTO>(dbItems);
        }

        public DTO.DraftPackingListDTO DB2DTO_NewInfo(DraftPackingList_NewInfo_View dbItems)
        {
            return AutoMapper.Mapper.Map<DraftPackingList_NewInfo_View, DTO.DraftPackingListDTO>(dbItems);
        }

        public List<DTO.DraftPackingListDetail> DB2DTO_DraftPackingListDetail(List<DraftPackingList_NewInfoDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftPackingList_NewInfoDetail_View>, List<DTO.DraftPackingListDetail>>(dbItems);
        }

        public List<DTO.DraftPackingListSparepartDetail> DB2DTO_DraftPackingListSparepartDetail(List<DraftPackingList_NewInfoSparepartDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DraftPackingList_NewInfoSparepartDetail_View>, List<DTO.DraftPackingListSparepartDetail>>(dbItems);
        }

        public void DTO2DB_DraftPackingList(DTO.DraftPackingListDTO dtoItem, ref DraftPackingList dbItem)
        {
            /*
             * MAP & CHECK PackingListDetail
             */
            List<DraftPackingListDetail> ItemNeedDelete_Detail = new List<DraftPackingListDetail>();
            if (dtoItem.DraftPackingListDetail != null)
            {
                //CHECK
                //foreach (DraftPackingListDetail dbDetail in dbItem.DraftPackingListDetail.Where(o => !dtoItem.DraftPackingListDetail.Select(s => s.DraftPackingListDetailID).Contains(o.DraftPackingListDetailID)))
                //{
                //    ItemNeedDelete_Detail.Add(dbDetail);
                //}
                //foreach (DraftPackingListDetail dbDetail in ItemNeedDelete_Detail)
                //{
                //    dbItem.DraftPackingListDetail.Remove(dbDetail);
                //}
                //MAP
                foreach (DTO.DraftPackingListDetail dtoDetail in dtoItem.DraftPackingListDetail)
                {
                    DraftPackingListDetail dbDetail;
                    if (dtoDetail.DraftPackingListDetailID < 0)
                    {
                        dbDetail = new DraftPackingListDetail();
                        dbItem.DraftPackingListDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.DraftPackingListDetail.FirstOrDefault(o => o.DraftPackingListDetailID == dtoDetail.DraftPackingListDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.DraftPackingListDetail, DraftPackingListDetail>(dtoDetail, dbDetail);
                    }
                }

                foreach (DTO.DraftPackingListSparepartDetail dtoSparePartDetail in dtoItem.DraftPackingListSparepartDetail)
                {
                    DraftPackingListDetail dbDetail;
                    if (dtoSparePartDetail.DraftPackingListDetailID < 0)
                    {
                        dbDetail = new DraftPackingListDetail();
                        dbItem.DraftPackingListDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.DraftPackingListDetail.FirstOrDefault(o => o.DraftPackingListDetailID == dtoSparePartDetail.DraftPackingListDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.DraftPackingListSparepartDetail, DraftPackingListDetail>(dtoSparePartDetail, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.DraftPackingListDTO, DraftPackingList>(dtoItem, dbItem);
        }
    }
}
