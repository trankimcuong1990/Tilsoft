using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryMaterialOrderNorm.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryMaterialOrderNormMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_SearchView, DTO.FactoryMaterialOrderNormSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialOrderNormMng_FactoryMaterialOrderNormDetail_View, DTO.FactoryMaterialOrderNormDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View, DTO.FactoryMaterialOrderNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialOrderNormDetails, o => o.MapFrom(s => s.FactoryMaterialOrderNormMng_FactoryMaterialOrderNormDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialOrderNormDetail, FactoryMaterialOrderNormDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialOrderNormDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialOrderNorm, FactoryMaterialOrderNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialOrderNormID, o => o.Ignore())
                    .ForMember(d => d.FactoryMaterialOrderNormDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryMaterialOrderNormMng_ClientProduct_View, DTO.ClientProduct>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialOrderNormMng_Client_View, DTO.Client>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialOrderNormMng_DefaultNorm_View, DTO.DefaultNorm>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //DTO 2 DTO
                AutoMapper.Mapper.CreateMap<DTO.ClientProduct, DTO.ClientProduct>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DefaultNorm, DTO.DefaultNorm>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryMaterialOrderNormSearch> DB2DTO_FactoryMaterialOrderNormSearch(List<FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_SearchView>, List<DTO.FactoryMaterialOrderNormSearch>>(dbItems);
        }

        public DTO.FactoryMaterialOrderNorm DB2DTO_FactoryMaterialOrderNorm(FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View, DTO.FactoryMaterialOrderNorm>(dbItem);
        }

        public List<DTO.Client> DB2DTO_Client(List<FactoryMaterialOrderNormMng_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialOrderNormMng_Client_View>, List<DTO.Client>>(dbItems);
        }

        public List<DTO.ClientProduct> DB2DTO_ClientProduct(List<FactoryMaterialOrderNormMng_ClientProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialOrderNormMng_ClientProduct_View>, List<DTO.ClientProduct>>(dbItems);
        }

        public void DTO2DB_FactoryMaterialOrderNorm(DTO.FactoryMaterialOrderNorm dtoItem, ref FactoryMaterialOrderNorm dbItem)
        {
            if (dtoItem.FactoryMaterialOrderNormDetails != null)
            {
                foreach (var item in dbItem.FactoryMaterialOrderNormDetail.ToArray())
                {
                    if (!dtoItem.FactoryMaterialOrderNormDetails.Select(s => s.FactoryMaterialOrderNormDetailID).Contains(item.FactoryMaterialOrderNormDetailID))
                    {
                        dbItem.FactoryMaterialOrderNormDetail.Remove(item);
                    }
                }

                foreach (var item in dtoItem.FactoryMaterialOrderNormDetails)
                {
                    FactoryMaterialOrderNormDetail dbDetail;
                    if (item.FactoryMaterialOrderNormDetailID < 0)
                    {
                        dbDetail = new FactoryMaterialOrderNormDetail();
                        dbItem.FactoryMaterialOrderNormDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryMaterialOrderNormDetail.Where(o => o.FactoryMaterialOrderNormDetailID == item.FactoryMaterialOrderNormDetailID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryMaterialOrderNormDetail, FactoryMaterialOrderNormDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.FactoryMaterialOrderNorm, FactoryMaterialOrderNorm>(dtoItem, dbItem);
        }

        public List<DTO.DefaultNorm> DB2DTO_DefaultNorm(List<FactoryMaterialOrderNormMng_DefaultNorm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMaterialOrderNormMng_DefaultNorm_View>, List<DTO.DefaultNorm>>(dbItems);
        }

        //DTO 2 DTO
        public List<DTO.ClientProduct> DTO2DTO_ClientProduct(List<DTO.ClientProduct> dtoItems)
        {
            return AutoMapper.Mapper.Map<List<DTO.ClientProduct>, List<DTO.ClientProduct>>(dtoItems);
        }

        public List<DTO.DefaultNorm> DTO2DTO_DefaultNorm(List<DTO.DefaultNorm> dtoItems)
        {
            return AutoMapper.Mapper.Map<List<DTO.DefaultNorm>, List<DTO.DefaultNorm>>(dtoItems);
        }

    }
}
