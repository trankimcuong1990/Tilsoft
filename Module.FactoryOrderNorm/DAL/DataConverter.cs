using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryOrderNorm.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryOrderNormMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryOrderNormMng_FactoryOrderNorm_SearchView, DTO.FactoryOrderNormSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderNormMng_FactoryMaterialOrderNorm_View, DTO.FactoryMaterialOrderNorm>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.IsEditing, o => o.UseValue(false))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderNormMng_FactoryFinishedProductOrderNorm_View, DTO.FactoryFinishedProductOrderNorm>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.IsEditing, o => o.UseValue(false))
                   .ForMember(d => d.FactoryMaterialOrderNorms, o => o.MapFrom(s => s.FactoryOrderNormMng_FactoryMaterialOrderNorm_View))
                   .ForMember(d => d.FactoryFinishedProductOrderNormFactoryGoodsProcedures, o => o.MapFrom(s => s.FactoryOrderNormMng_FactoryFinishedProductOrderNormFactoryGoodsProcedure_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderNormMng_FactoryOrderNorm_View, DTO.FactoryOrderNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryFinishedProductOrderNorms, o => o.MapFrom(s => s.FactoryOrderNormMng_FactoryFinishedProductOrderNorm_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderNormMng_ClientProduct_View, DTO.ClientProduct>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderNormMng_Client_View, DTO.Client>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Season, o => o.UseValue("2016/2017"))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryOrderNormMng_FactoryFinishedProductOrderNormFactoryGoodsProcedure_View, DTO.FactoryFinishedProductOrderNormFactoryGoodsProcedure>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialOrderNorm, FactoryMaterialOrderNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialOrderNormID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryFinishedProductOrderNorm, FactoryFinishedProductOrderNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialOrderNorm, o => o.Ignore())
                    .ForMember(d => d.FactoryFinishedProductOrderNormID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryOrderNorm, FactoryOrderNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryFinishedProductOrderNorm, o => o.Ignore())
                .ForMember(d => d.FactoryOrderNormID, o => o.Ignore());

                //DTO 2 DTO
                AutoMapper.Mapper.CreateMap<DTO.ClientProduct, DTO.ClientProduct>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryOrderNormSearch> DB2DTO_FactoryOrderNormSearch(List<FactoryOrderNormMng_FactoryOrderNorm_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderNormMng_FactoryOrderNorm_SearchView>, List<DTO.FactoryOrderNormSearch>>(dbItems);
        }

        public DTO.FactoryOrderNorm DB2DTO_FactoryOrderNorm(FactoryOrderNormMng_FactoryOrderNorm_View dbItem)
        {
            DTO.FactoryOrderNorm orderNorm =  AutoMapper.Mapper.Map<FactoryOrderNormMng_FactoryOrderNorm_View, DTO.FactoryOrderNorm>(dbItem);
            DTO.FactoryFinishedProductOrderNorm parentNormItem;
            foreach (var item in orderNorm.FactoryFinishedProductOrderNorms.ToArray())
            {
                if (item.ParentNormID != null)
                {
                    //get parent norm item
                    parentNormItem = orderNorm.FactoryFinishedProductOrderNorms.Where(o => o.FactoryFinishedProductOrderNormID == item.ParentNormID).FirstOrDefault();

                    //push sub component norm to parent norm item
                    if (parentNormItem.FactoryFinishedProductOrderNorms == null) parentNormItem.FactoryFinishedProductOrderNorms = new List<DTO.FactoryFinishedProductOrderNorm>();
                    parentNormItem.FactoryFinishedProductOrderNorms.Add(item);

                    //remove item
                    orderNorm.FactoryFinishedProductOrderNorms.Remove(item);
                }
            }
            return orderNorm;
        }

        public void DTO2DB_FactoryOrderNorm(DTO.FactoryOrderNorm dtoItem, ref FactoryOrderNorm dbItem)
        {
            //if (dtoItem.FactoryFinishedProductOrderNorms != null)
            //{
            //    //user remove factory finished product in dto => check to remove in db
            //    foreach (var item in dbItem.FactoryFinishedProductOrderNorm.ToArray())
            //    {
            //        if (!dtoItem.FactoryFinishedProductOrderNorms.Select(s => s.FactoryFinishedProductOrderNormID).Contains(item.FactoryFinishedProductOrderNormID))
            //        {
            //            foreach (var mItem in item.FactoryMaterialOrderNorm.ToArray())
            //            {
            //                item.FactoryMaterialOrderNorm.Remove(mItem);
            //            }
            //            dbItem.FactoryFinishedProductOrderNorm.Remove(item);
            //        }
            //        else
            //        {
            //            //user remove factory material in dto => check to remove in db
            //            foreach (var mItem in item.FactoryMaterialOrderNorm.ToArray())
            //            {
            //                var x = dtoItem.FactoryFinishedProductOrderNorms.Where(o => o.FactoryFinishedProductOrderNormID == item.FactoryFinishedProductOrderNormID).FirstOrDefault().FactoryMaterialOrderNorms;
            //                if (!x.Select(s => s.FactoryMaterialOrderNormID).Contains(mItem.FactoryMaterialOrderNormID))
            //                {
            //                    item.FactoryMaterialOrderNorm.Remove(mItem);
            //                }
            //            }
            //        }
            //    }
            //    //read factory finished product
            //    FactoryFinishedProductOrderNorm dbFinishedProductNorm;
            //    FactoryMaterialOrderNorm dbMaterialNorm;
            //    foreach (var item in dtoItem.FactoryFinishedProductOrderNorms)
            //    {
            //        if (item.FactoryFinishedProductOrderNormID > 0)
            //        {
            //            dbFinishedProductNorm = dbItem.FactoryFinishedProductOrderNorm.Where(o => o.FactoryFinishedProductOrderNormID == item.FactoryFinishedProductOrderNormID).FirstOrDefault();
            //            if (dbFinishedProductNorm != null && item.FactoryMaterialOrderNorms != null)
            //            {
            //                foreach (var mItem in item.FactoryMaterialOrderNorms)
            //                {
            //                    if (mItem.FactoryMaterialOrderNormID > 0)
            //                    {
            //                        dbMaterialNorm = dbFinishedProductNorm.FactoryMaterialOrderNorm.Where(o => o.FactoryMaterialOrderNormID == mItem.FactoryMaterialOrderNormID).FirstOrDefault();
            //                    }
            //                    else
            //                    {
            //                        dbMaterialNorm = new FactoryMaterialOrderNorm();
            //                        dbFinishedProductNorm.FactoryMaterialOrderNorm.Add(dbMaterialNorm);
            //                    }
            //                    if (dbMaterialNorm != null)
            //                    {
            //                        AutoMapper.Mapper.Map<DTO.FactoryMaterialOrderNorm, FactoryMaterialOrderNorm>(mItem, dbMaterialNorm);
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            dbFinishedProductNorm = new FactoryFinishedProductOrderNorm();
            //            dbItem.FactoryFinishedProductOrderNorm.Add(dbFinishedProductNorm);
            //            if (item.FactoryMaterialOrderNorms != null)
            //            {
            //                foreach (var mItem in item.FactoryMaterialOrderNorms)
            //                {
            //                    dbMaterialNorm = new FactoryMaterialOrderNorm();
            //                    dbFinishedProductNorm.FactoryMaterialOrderNorm.Add(dbMaterialNorm);
            //                    AutoMapper.Mapper.Map<DTO.FactoryMaterialOrderNorm, FactoryMaterialOrderNorm>(mItem, dbMaterialNorm);
            //                }
            //            }
            //        }
            //        //mapping
            //        if (dbFinishedProductNorm != null)
            //        {
            //            AutoMapper.Mapper.Map<DTO.FactoryFinishedProductOrderNorm, FactoryFinishedProductOrderNorm>(item, dbFinishedProductNorm);
            //        }
            //    }
            //}
            AutoMapper.Mapper.Map<DTO.FactoryOrderNorm, FactoryOrderNorm>(dtoItem, dbItem);
        }

        public List<DTO.Client> DB2DTO_Client(List<FactoryOrderNormMng_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderNormMng_Client_View>, List<DTO.Client>>(dbItems);
        }

        public List<DTO.ClientProduct> DB2DTO_ClientProduct(List<FactoryOrderNormMng_ClientProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryOrderNormMng_ClientProduct_View>, List<DTO.ClientProduct>>(dbItems);
        }

        //DTO 2 DTO
        public List<DTO.ClientProduct> DTO2DTO_ClientProduct(List<DTO.ClientProduct> dtoItems)
        {
            return AutoMapper.Mapper.Map<List<DTO.ClientProduct>, List<DTO.ClientProduct>>(dtoItems);
        }
    }
}
