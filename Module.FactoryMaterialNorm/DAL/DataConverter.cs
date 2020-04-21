using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryMaterialNorm.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryMaterialNormMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryMaterialNormMng_FactoryMaterialNorm_SearchView, DTO.FactoryMaterialNormSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialNormMng_FactoryMaterialNormDetail_View, DTO.FactoryMaterialNormDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialNormMng_FactoryMaterialNorm_View, DTO.FactoryMaterialNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialNormDetails, o => o.MapFrom(s => s.FactoryMaterialNormMng_FactoryMaterialNormDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialNormDetail, FactoryMaterialNormDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialNormDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialNorm, FactoryMaterialNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialNormID, o => o.Ignore())
                    .ForMember(d => d.FactoryMaterialNormDetail, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryMaterialNormSearch> DB2DTO_FactoryMaterialNormSearch(List<FactoryMaterialNormMng_FactoryMaterialNorm_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialNormMng_FactoryMaterialNorm_SearchView>, List<DTO.FactoryMaterialNormSearch>>(dbItems);
        }

        public DTO.FactoryMaterialNorm DB2DTO_FactoryMaterialNorm(FactoryMaterialNormMng_FactoryMaterialNorm_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryMaterialNormMng_FactoryMaterialNorm_View, DTO.FactoryMaterialNorm>(dbItem);
        }

        public void DTO2DB_FactoryMaterialNorm(DTO.FactoryMaterialNorm dtoItem, ref FactoryMaterialNorm dbItem)
        {
            if (dtoItem.FactoryMaterialNormDetails != null)
            {
                foreach (var item in dbItem.FactoryMaterialNormDetail.ToArray())
                {
                    if (!dtoItem.FactoryMaterialNormDetails.Select(s => s.FactoryMaterialNormDetailID).Contains(item.FactoryMaterialNormDetailID))
                    {
                        dbItem.FactoryMaterialNormDetail.Remove(item);
                    }
                }

                foreach (var item in dtoItem.FactoryMaterialNormDetails)
                {
                    //read db image field
                    FactoryMaterialNormDetail dbDetail;
                    if (item.FactoryMaterialNormDetailID < 0)
                    {
                        dbDetail = new FactoryMaterialNormDetail();
                        dbItem.FactoryMaterialNormDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryMaterialNormDetail.Where(o => o.FactoryMaterialNormDetailID == item.FactoryMaterialNormDetailID).FirstOrDefault();
                    }

                    //map from dto image field to db image field
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryMaterialNormDetail, FactoryMaterialNormDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryMaterialNorm, FactoryMaterialNorm>(dtoItem, dbItem);
        }

    }
}
