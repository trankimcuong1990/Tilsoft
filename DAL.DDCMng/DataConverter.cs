using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.DDCMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DDCMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DDCMng_DDCSearchResult_View, DTO.DDCMng.DDCSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DDCMng_DDC_View, DTO.DDCMng.DDC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Details, o => o.MapFrom(s => s.DDCMng_DDCDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DDCMng_DDCDetail_View, DTO.DDCMng.DDCDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DDCMng.DDC, DDC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DDCID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DDCMng.DDCDetail, DDCDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DDCID, o => o.Ignore())
                    .ForMember(d => d.DDCDetailID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DDCMng.DDCSearchResult> DB2DTO_DDCSearchResultList(List<DDCMng_DDCSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DDCMng_DDCSearchResult_View>, List<DTO.DDCMng.DDCSearchResult>>(dbItems);
        }

        public DTO.DDCMng.DDC DB2DTO_DDC(DDCMng_DDC_View dbItem)
        {
            return AutoMapper.Mapper.Map<DDCMng_DDC_View, DTO.DDCMng.DDC>(dbItem);
        }

        public void DTO2BD(DTO.DDCMng.DDC dtoItem, ref DDC dbItem)
        {
            AutoMapper.Mapper.Map<DTO.DDCMng.DDC, DDC>(dtoItem, dbItem);

            // map load ddc detail
            if (dtoItem.Details != null)
            {
                // check for child rows deleted
                foreach (DDCDetail dbDetail in dbItem.DDCDetail.ToArray())
                {
                    if (!dtoItem.Details.Select(o => o.DDCDetailID).Contains(dbDetail.DDCDetailID))
                    {
                        dbItem.DDCDetail.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.DDCMng.DDCDetail dtoDetail in dtoItem.Details)
                {
                    DDCDetail dbDetail;
                    if (dtoDetail.DDCDetailID <= 0)
                    {
                        dbDetail = new DDCDetail();
                        dbItem.DDCDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.DDCDetail.FirstOrDefault(o => o.DDCDetailID == dtoDetail.DDCDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.DDCMng.DDCDetail, DDCDetail>(dtoDetail, dbDetail);
                    }
                }
            }
        }
    }
}
