using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryStepNorm.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryStepNormMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryStepNormMng_FactoryStepNorm_SearchView, DTO.FactoryStepNormSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryStepNormMng_FactoryStepNormDetail_View, DTO.FactoryStepNormDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryStepNormMng_FactoryStepNorm_View, DTO.FactoryStepNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryStepNormDetails, o => o.MapFrom(s => s.FactoryStepNormMng_FactoryStepNormDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryStepNormDetail, FactoryStepNormDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryStepNormDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryStepNorm, FactoryStepNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryStepNormID, o => o.Ignore())
                    .ForMember(d => d.FactoryStepNormDetail, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryStepNormSearch> DB2DTO_FactoryStepNormSearch(List<FactoryStepNormMng_FactoryStepNorm_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryStepNormMng_FactoryStepNorm_SearchView>, List<DTO.FactoryStepNormSearch>>(dbItems);
        }

        public DTO.FactoryStepNorm DB2DTO_FactoryStepNorm(FactoryStepNormMng_FactoryStepNorm_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryStepNormMng_FactoryStepNorm_View, DTO.FactoryStepNorm>(dbItem);
        }

        public void DTO2DB_FactoryStepNorm(DTO.FactoryStepNorm dtoItem, ref FactoryStepNorm dbItem)
        {
            if (dtoItem.FactoryStepNormDetails != null)
            {
                foreach (var item in dbItem.FactoryStepNormDetail.ToArray())
                {
                    if (!dtoItem.FactoryStepNormDetails.Select(s => s.FactoryStepNormDetailID).Contains(item.FactoryStepNormDetailID))
                    {
                        dbItem.FactoryStepNormDetail.Remove(item);
                    }
                }

                foreach (var item in dtoItem.FactoryStepNormDetails)
                {
                    //read db image field
                    FactoryStepNormDetail dbDetail;
                    if (item.FactoryStepNormDetailID < 0)
                    {
                        dbDetail = new FactoryStepNormDetail();
                        dbItem.FactoryStepNormDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryStepNormDetail.Where(o => o.FactoryStepNormDetailID == item.FactoryStepNormDetailID).FirstOrDefault();
                    }

                    //map from dto image field to db image field
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryStepNormDetail, FactoryStepNormDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryStepNorm, FactoryStepNorm>(dtoItem, dbItem);
        }

    }
}
