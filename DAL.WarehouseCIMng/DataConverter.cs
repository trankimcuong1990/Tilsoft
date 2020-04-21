using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.WarehouseCIMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WarehouseCIMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<WarehouseCIMng_WarehouseCI_SearchView, DTO.WarehouseCIMng.WarehouseCISearch>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseCIMng_WarehouseCIDetail_View, DTO.WarehouseCIMng.WarehouseCIDetail>()
                                .IgnoreAllNonExisting()
                                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseCIMng_WarehouseCIExtDetail_View, DTO.WarehouseCIMng.WarehouseCIExtDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WarehouseCIMng_WarehouseCI_View, DTO.WarehouseCIMng.WarehouseCI>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WarehouseCIDetails, o => o.MapFrom(s => s.WarehouseCIMng_WarehouseCIDetail_View))
                    .ForMember(d => d.WarehouseCIExtDetails, o => o.MapFrom(s => s.WarehouseCIMng_WarehouseCIExtDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WarehouseCIMng.WarehouseCI, WarehouseCI>()
                                .IgnoreAllNonExisting()
                                .ForMember(d => d.WarehouseCIID, o => o.Ignore())
                                .ForMember(d => d.WarehouseCIDetail, o => o.Ignore())
                                .ForMember(d => d.WarehouseCIExtDetail, o => o.Ignore())
                                ;

                AutoMapper.Mapper.CreateMap<DTO.WarehouseCIMng.WarehouseCIDetail, WarehouseCIDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.WarehouseCIDetailID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.WarehouseCIMng.WarehouseCIExtDetail, WarehouseCIExtDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.WarehouseCIExtDetailID, o => o.Ignore())
                   ;



                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WarehouseCIMng.WarehouseCISearch> DB2DTO_WarehouseCISearch(List<WarehouseCIMng_WarehouseCI_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseCIMng_WarehouseCI_SearchView>, List<DTO.WarehouseCIMng.WarehouseCISearch>>(dbItems);
        }

        public DTO.WarehouseCIMng.WarehouseCI DB2DTO_WarehouseCI(WarehouseCIMng_WarehouseCI_View dbItem)
        {
            DTO.WarehouseCIMng.WarehouseCI dtoItem = AutoMapper.Mapper.Map<WarehouseCIMng_WarehouseCI_View, DTO.WarehouseCIMng.WarehouseCI>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.IssuedDate.HasValue)
                dtoItem.IssuedDateFormated = dbItem.IssuedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2DB_WarehouseCI(DTO.WarehouseCIMng.WarehouseCI dtoItem, ref WarehouseCI dbItem)
        {
            /*
             * MAP & CHECK WarehouseCIDetail DELETED
             */
            List<WarehouseCIDetail> ItemNeedDelete_Detail = new List<WarehouseCIDetail>();
            if (dtoItem.WarehouseCIDetails != null)
            {
                //CHECK
                foreach (WarehouseCIDetail dbDetail in dbItem.WarehouseCIDetail.Where(o => !dtoItem.WarehouseCIDetails.Select(s => s.WarehouseCIDetailID).Contains(o.WarehouseCIDetailID)))
                {
                    ItemNeedDelete_Detail.Add(dbDetail);
                }
                foreach (WarehouseCIDetail dbDetail in ItemNeedDelete_Detail)
                {
                    dbItem.WarehouseCIDetail.Remove(dbDetail);
                }
                //MAP
                foreach (DTO.WarehouseCIMng.WarehouseCIDetail dtoDetail in dtoItem.WarehouseCIDetails)
                {
                    WarehouseCIDetail dbDetail;
                    if (dtoDetail.WarehouseCIDetailID < 0)
                    {
                        dbDetail = new WarehouseCIDetail();
                        dbItem.WarehouseCIDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.WarehouseCIDetail.FirstOrDefault(o => o.WarehouseCIDetailID == dtoDetail.WarehouseCIDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehouseCIMng.WarehouseCIDetail, WarehouseCIDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            /*
             * MAP & CHECK WarehouseCIExtDetail DELETED
             */
            List<WarehouseCIExtDetail> ItemNeedDelete_ExtDetail = new List<WarehouseCIExtDetail>();
            if (dtoItem.WarehouseCIExtDetails != null)
            {
                //CHECK
                foreach (WarehouseCIExtDetail dbDetail in dbItem.WarehouseCIExtDetail.Where(o => !dtoItem.WarehouseCIExtDetails.Select(s => s.WarehouseCIExtDetailID).Contains(o.WarehouseCIExtDetailID)))
                {
                    ItemNeedDelete_ExtDetail.Add(dbDetail);
                }
                foreach (WarehouseCIExtDetail dbDetail in ItemNeedDelete_ExtDetail)
                {
                    dbItem.WarehouseCIExtDetail.Remove(dbDetail);
                }
                //MAP
                foreach (DTO.WarehouseCIMng.WarehouseCIExtDetail dtoDetail in dtoItem.WarehouseCIExtDetails)
                {
                    WarehouseCIExtDetail dbDetail;
                    if (dtoDetail.WarehouseCIExtDetailID < 0)
                    {
                        dbDetail = new WarehouseCIExtDetail();
                        dbItem.WarehouseCIExtDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.WarehouseCIExtDetail.FirstOrDefault(o => o.WarehouseCIExtDetailID == dtoDetail.WarehouseCIExtDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WarehouseCIMng.WarehouseCIExtDetail, WarehouseCIExtDetail>(dtoDetail, dbDetail);
                    }
                }
            }


            if (!string.IsNullOrEmpty(dtoItem.IssuedDateFormated))
            {
                dtoItem.IssuedDate = DateTime.ParseExact(dtoItem.IssuedDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }

            AutoMapper.Mapper.Map<DTO.WarehouseCIMng.WarehouseCI, WarehouseCI>(dtoItem, dbItem);
        }

        //public List<DTO.WarehouseCIMng.InitInfo> DB2DTO_InitInfos(List<WarehouseCIMng_InitInfo_View> dbItems)
        //{
        //    AutoMapper.Mapper.Reset();

        //    AutoMapper.Mapper.CreateMap<WarehouseCIMng_InitInfo_View, DTO.WarehouseCIMng.InitInfo>()
        //        .IgnoreAllNonExisting()
        //        .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

        //    return AutoMapper.Mapper.Map<List<WarehouseCIMng_InitInfo_View>, List<DTO.WarehouseCIMng.InitInfo>>(dbItems);
        //}

      

       
    }
}
