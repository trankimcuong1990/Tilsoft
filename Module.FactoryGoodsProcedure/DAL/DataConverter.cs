using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FactoryGoodsProcedure.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryGoodsProcedure";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //
                // Mapping declaration.
                //
                AutoMapper.Mapper.CreateMap<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View, DTO.FactoryGoodsProcedureSearchResultDTO>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryGoodsProcedureMng_FactoryGoodsProcedure_View, DTO.FactoryGoodsProcedureDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryGoodsProcedureDetailDTOs, o => o.MapFrom(s => s.FactoryGoodsProcedureMng_FactoryGoodsProcedureDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryGoodsProcedureMng_FactoryGoodsProcedureDetail_View, DTO.FactoryGoodsProcedureDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryGoodsProcedureDTO, FactoryGoodsProcedure>()
                    .ForMember(d => d.FactoryGoodsProcedureID, o => o.Ignore())
                    .ForMember(d => d.FactoryGoodsProcedureDetail, o => o.Ignore())
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.FactoryGoodsProcedureDetailDTO, FactoryGoodsProcedureDetail>()
                    .ForMember(d => d.FactoryGoodsProcedureDetailID, o => o.Ignore())
                    .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryGoodsProcedureSearchResultDTO> DB2DTO_FactoryGoodsProcedureSearchResult(List<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryGoodsProcedureMng_FactoryGoodsProcedureSearchResult_View>, List<DTO.FactoryGoodsProcedureSearchResultDTO>>(dbItems);
        }

        public DTO.FactoryGoodsProcedureDTO DB2DTO_FactoryGoodsProcedure(FactoryGoodsProcedureMng_FactoryGoodsProcedure_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryGoodsProcedureMng_FactoryGoodsProcedure_View, DTO.FactoryGoodsProcedureDTO>(dbItem);
        }

        public void DTO2DB(DTO.FactoryGoodsProcedureDTO dtoItem, ref FactoryGoodsProcedure dbItem, string TmpFile, int userId)
        {
            AutoMapper.Mapper.Map<DTO.FactoryGoodsProcedureDTO, FactoryGoodsProcedure>(dtoItem, dbItem);
        }

        public void DTO2DB_FactoryGoodsProcedure(DTO.FactoryGoodsProcedureDTO dtoItem, ref FactoryGoodsProcedure dbItem)
        {
            if (dtoItem.FactoryGoodsProcedureDetailDTOs != null) // allow user delete item in case receipt is export
            {
                foreach (var item in dbItem.FactoryGoodsProcedureDetail.ToArray())
                {
                    if (!dtoItem.FactoryGoodsProcedureDetailDTOs.Select(s => s.FactoryGoodsProcedureDetailID).Contains(item.FactoryGoodsProcedureDetailID))
                        dbItem.FactoryGoodsProcedureDetail.Remove(item);
                }

                foreach (var item in dtoItem.FactoryGoodsProcedureDetailDTOs)
                {
                    if (item.FactoryStepID == null)
                        continue;

                    FactoryGoodsProcedureDetail dbDetail;

                    if (item.FactoryGoodsProcedureDetailID < 0)
                    {
                        dbDetail = new FactoryGoodsProcedureDetail();
                        dbItem.FactoryGoodsProcedureDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryGoodsProcedureDetail.Where(o => o.FactoryGoodsProcedureDetailID == item.FactoryGoodsProcedureDetailID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                        AutoMapper.Mapper.Map<DTO.FactoryGoodsProcedureDetailDTO, FactoryGoodsProcedureDetail>(item, dbDetail);
                }
            }

            AutoMapper.Mapper.Map<DTO.FactoryGoodsProcedureDTO, FactoryGoodsProcedure>(dtoItem, dbItem);
        }
    }
}
