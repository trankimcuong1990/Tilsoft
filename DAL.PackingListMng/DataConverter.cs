using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.PackingListMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PackingListMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PackingListMng_PackingList_SearchView, DTO.PackingListMng.PackingListSearchResult>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.PackingListSearchContainerNos, o => o.MapFrom(s => s.PackingListMng_PackingList_SearchContainerNoView))  //Container No
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_PackingListDetail_View, DTO.PackingListMng.PackingListDetail>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_PackingListSparepartDetail_View, DTO.PackingListMng.PackingListSparepartDetail>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_ECommercialInvoice_View, DTO.PackingListMng.ECommercialInvoice>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_PackingListDetailExtend_View, DTO.PackingListMng.PackingListDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_PackingList_View, DTO.PackingListMng.PackingList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PackingListDetails, o => o.MapFrom(s => s.PackingListMng_PackingListDetail_View))
                    .ForMember(d => d.PackingListSparepartDetails, o => o.MapFrom(s => s.PackingListMng_PackingListSparepartDetail_View))
                    .ForMember(d => d.PackingListDetailExtends, o => o.MapFrom(s => s.PackingListMng_PackingListDetailExtend_View))
                    .ForMember(d => d.ECommercialInvoices, o => o.MapFrom(s => s.PackingListMng_ECommercialInvoice_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PackingListMng.PackingList, PackingList>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.PackingListID, o => o.Ignore())
                                .ForMember(d => d.PackingListDetail, o => o.Ignore())
                                .ForMember(d => d.PackingListSparepartDetail, o => o.Ignore())
                                .ForMember(d => d.PackingListDetailExtend, o => o.Ignore())
                                ;

                AutoMapper.Mapper.CreateMap<DTO.PackingListMng.PackingListDetail, PackingListDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PackingListDetailID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.PackingListMng.PackingListSparepartDetail, PackingListSparepartDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PackingListSparepartDetailID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.PackingListMng.PackingListDetailExtend, PackingListDetailExtend>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PackingListDetailExtendID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<PackingListMng_InitInfo_View, DTO.PackingListMng.InitInfo>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_InitInfoDetail_View, DTO.PackingListMng.PackingListDetail>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_InitInfoSparepartDetail_View, DTO.PackingListMng.PackingListSparepartDetail>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PackingListMng_InitInfo_View, DTO.PackingListMng.PackingList>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.PackingListDetails, o => o.MapFrom(s => s.PackingListMng_InitInfoDetail_View))
                                .ForMember(d => d.PackingListSparepartDetails, o => o.MapFrom(s => s.PackingListMng_InitInfoSparepartDetail_View))
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                 //Container No
                AutoMapper.Mapper.CreateMap<PackingListMng_PackingList_SearchContainerNoView, DTO.PackingListMng.PackingListSearchContainerNo>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PackingListMng.PackingListSearchResult> DB2DTO_PackingListSearch(List<PackingListMng_PackingList_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PackingListMng_PackingList_SearchView>, List<DTO.PackingListMng.PackingListSearchResult>>(dbItems);
        }

        public DTO.PackingListMng.PackingList DB2DTO_PackingList(PackingListMng_PackingList_View dbItem)
        {
            DTO.PackingListMng.PackingList dtoItem = AutoMapper.Mapper.Map<PackingListMng_PackingList_View, DTO.PackingListMng.PackingList>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.PackingListDate.HasValue)
                dtoItem.PackingListDateFormated = dbItem.PackingListDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2DB_PackingList(DTO.PackingListMng.PackingList dtoItem, ref PackingList dbItem)
        {
            /*
             * MAP & CHECK PackingListDetail
             */
            List<PackingListDetail> ItemNeedDelete_Detail = new List<PackingListDetail>();
            if (dtoItem.PackingListDetails != null)
            {
                //CHECK
                foreach (PackingListDetail dbDetail in dbItem.PackingListDetail.Where(o => !dtoItem.PackingListDetails.Select(s => s.PackingListDetailID).Contains(o.PackingListDetailID)))
                {
                    ItemNeedDelete_Detail.Add(dbDetail);
                }
                foreach (PackingListDetail dbDetail in ItemNeedDelete_Detail)
                {
                    dbItem.PackingListDetail.Remove(dbDetail);
                }
                //MAP
                foreach (DTO.PackingListMng.PackingListDetail dtoDetail in dtoItem.PackingListDetails)
                {
                    PackingListDetail dbDetail;
                    if (dtoDetail.PackingListDetailID < 0)
                    {
                        dbDetail = new PackingListDetail();
                        dbItem.PackingListDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PackingListDetail.FirstOrDefault(o => o.PackingListDetailID == dtoDetail.PackingListDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PackingListMng.PackingListDetail, PackingListDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            /*
             * MAP & CHECK PackingListDetail
             */
            List<PackingListSparepartDetail> tobe_delete_sparepart = new List<PackingListSparepartDetail>();
            if (dtoItem.PackingListDetails != null)
            {
                //CHECK
                foreach (PackingListSparepartDetail dbDetail in dbItem.PackingListSparepartDetail.Where(o => !dtoItem.PackingListSparepartDetails.Select(s => s.PackingListSparepartDetailID).Contains(o.PackingListSparepartDetailID)))
                {
                    tobe_delete_sparepart.Add(dbDetail);
                }
                foreach (PackingListSparepartDetail dbDetail in tobe_delete_sparepart)
                {
                    dbItem.PackingListSparepartDetail.Remove(dbDetail);
                }
                //MAP
                foreach (DTO.PackingListMng.PackingListSparepartDetail dtoDetail in dtoItem.PackingListSparepartDetails)
                {
                    PackingListSparepartDetail dbDetail;
                    if (dtoDetail.PackingListSparepartDetailID < 0)
                    {
                        dbDetail = new PackingListSparepartDetail();
                        dbItem.PackingListSparepartDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PackingListSparepartDetail.FirstOrDefault(o => o.PackingListSparepartDetailID == dtoDetail.PackingListSparepartDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PackingListMng.PackingListSparepartDetail, PackingListSparepartDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            /*
             * MAP & CHECK PackingListDetailExtend
             */
            List<PackingListDetailExtend> ItemNeedDelete_DetailExt = new List<PackingListDetailExtend>();
            if (dtoItem.PackingListDetailExtends != null)
            {
                //CHECK
                foreach (PackingListDetailExtend dbDetailExt in dbItem.PackingListDetailExtend.Where(o => !dtoItem.PackingListDetailExtends.Select(s => s.PackingListDetailExtendID).Contains(o.PackingListDetailExtendID)))
                {
                    ItemNeedDelete_DetailExt.Add(dbDetailExt);
                }
                foreach (PackingListDetailExtend dbDetailExt in ItemNeedDelete_DetailExt)
                {
                    dbItem.PackingListDetailExtend.Remove(dbDetailExt);
                }
                //MAP
                foreach (DTO.PackingListMng.PackingListDetailExtend dtoDetailExt in dtoItem.PackingListDetailExtends)
                {
                    PackingListDetailExtend dbDetailExt;
                    if (dtoDetailExt.PackingListDetailExtendID < 0)
                    {
                        dbDetailExt = new PackingListDetailExtend();
                        dbItem.PackingListDetailExtend.Add(dbDetailExt);
                    }
                    else
                    {
                        dbDetailExt = dbItem.PackingListDetailExtend.FirstOrDefault(o => o.PackingListDetailExtendID == dtoDetailExt.PackingListDetailExtendID);
                    }

                    if (dbDetailExt != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PackingListMng.PackingListDetailExtend, PackingListDetailExtend>(dtoDetailExt, dbDetailExt);
                    }
                }
            }

            if (!string.IsNullOrEmpty(dtoItem.PackingListDateFormated))
            {
                dtoItem.PackingListDate = dtoItem.PackingListDateFormated.ConvertStringToDateTime();
            }

            AutoMapper.Mapper.Map<DTO.PackingListMng.PackingList, PackingList>(dtoItem, dbItem);
        }

        public List<DTO.PackingListMng.InitInfo> DB2DTO_InitInfos(List<PackingListMng_InitInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PackingListMng_InitInfo_View>, List<DTO.PackingListMng.InitInfo>>(dbItems);
        }

        public DTO.PackingListMng.PackingList DB2DTO_InitInfo(PackingListMng_InitInfo_View dbItem)
        {
            DTO.PackingListMng.PackingList dtoItem = AutoMapper.Mapper.Map<PackingListMng_InitInfo_View, DTO.PackingListMng.PackingList>(dbItem);
            return dtoItem;
        }


    }
}
