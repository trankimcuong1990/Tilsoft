using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryNorm.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryNormMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryNormMng_FactoryNorm_SearchView, DTO.FactoryNormSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryNormMng_FactoryMaterialNorm_View, DTO.FactoryMaterialNorm>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.IsEditing, o => o.UseValue(false))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryNormMng_FactoryFinishedProductNorm_View, DTO.FactoryFinishedProductNorm>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.IsEditing, o => o.UseValue(false))
                   .ForMember(d => d.FactoryMaterialNorms, o => o.MapFrom(s => s.FactoryNormMng_FactoryMaterialNorm_View))
                   .ForMember(d => d.FactoryFinishedProductNormFactoryGoodsProcedures, o => o.MapFrom(s => s.FactoryNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryNormMng_FactoryNorm_View, DTO.FactoryNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryFinishedProductNorms, o => o.MapFrom(s => s.FactoryNormMng_FactoryFinishedProductNorm_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View, DTO.FactoryFinishedProductNormFactoryGoodsProcedure>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.FactoryMaterialNorm, FactoryMaterialNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialNormID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryFinishedProductNorm, FactoryFinishedProductNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryMaterialNorm, o => o.Ignore())
                    .ForMember(d => d.FactoryFinishedProductNormID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryNorm, FactoryNorm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryFinishedProductNorm, o => o.Ignore())
                .ForMember(d => d.FactoryNormID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryNormSearch> DB2DTO_FactoryNormSearch(List<FactoryNormMng_FactoryNorm_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryNormMng_FactoryNorm_SearchView>, List<DTO.FactoryNormSearch>>(dbItems);
        }

        public DTO.FactoryNorm DB2DTO_FactoryNorm(FactoryNormMng_FactoryNorm_View dbItem)
        {
            DTO.FactoryNorm factoryNorm = AutoMapper.Mapper.Map<FactoryNormMng_FactoryNorm_View, DTO.FactoryNorm>(dbItem);

            DTO.FactoryFinishedProductNorm parentNormItem;
            foreach (var item in factoryNorm.FactoryFinishedProductNorms.ToArray())
            {
                if (item.ParentNormID != null)
                {
                    //get parent norm item
                    parentNormItem = factoryNorm.FactoryFinishedProductNorms.Where(o => o.FactoryFinishedProductNormID == item.ParentNormID).FirstOrDefault();

                    //push sub component norm to parent norm item
                    if (parentNormItem.FactoryFinishedProductNorms == null) parentNormItem.FactoryFinishedProductNorms = new List<DTO.FactoryFinishedProductNorm>();
                    parentNormItem.FactoryFinishedProductNorms.Add(item);
                    
                    //remove item
                    factoryNorm.FactoryFinishedProductNorms.Remove(item);
                }
            }

            return factoryNorm;
        }

        public void DTO2DB_FactoryNorm(DTO.FactoryNorm dtoItem, ref FactoryNorm dbItem)
        {
            //if (dtoItem.FactoryFinishedProductNorms != null)
            //{
            //    //user remove factory finished product in dto => check to remove in db
            //    foreach (var item in dbItem.FactoryFinishedProductNorm.ToArray())
            //    {
            //        if (!dtoItem.FactoryFinishedProductNorms.Select(s => s.FactoryFinishedProductNormID).Contains(item.FactoryFinishedProductNormID))
            //        {
            //            foreach (var mItem in item.FactoryMaterialNorm.ToArray())
            //            {
            //                item.FactoryMaterialNorm.Remove(mItem);
            //            }
            //            dbItem.FactoryFinishedProductNorm.Remove(item);
            //        }
            //        else
            //        {
            //            //user remove factory material in dto => check to remove in db
            //            foreach (var mItem in item.FactoryMaterialNorm.ToArray())
            //            {
            //                var x = dtoItem.FactoryFinishedProductNorms.Where(o => o.FactoryFinishedProductNormID == item.FactoryFinishedProductNormID).FirstOrDefault().FactoryMaterialNorms;
            //                if (!x.Select(s => s.FactoryMaterialNormID).Contains(mItem.FactoryMaterialNormID))
            //                {
            //                    item.FactoryMaterialNorm.Remove(mItem);
            //                }
            //            }
            //        }
            //    }
            //    //read factory finished product
            //    FactoryFinishedProductNorm dbFinishedProductNorm;
            //    FactoryMaterialNorm dbMaterialNorm;
            //    foreach (var item in dtoItem.FactoryFinishedProductNorms)
            //    {
            //        if (item.FactoryFinishedProductNormID > 0)
            //        {
            //            dbFinishedProductNorm = dbItem.FactoryFinishedProductNorm.Where(o => o.FactoryFinishedProductNormID == item.FactoryFinishedProductNormID).FirstOrDefault();
            //            if (dbFinishedProductNorm != null && item.FactoryMaterialNorms != null)
            //            {
            //                foreach (var mItem in item.FactoryMaterialNorms)
            //                {
            //                    if (mItem.FactoryMaterialNormID > 0)
            //                    {
            //                        dbMaterialNorm = dbFinishedProductNorm.FactoryMaterialNorm.Where(o => o.FactoryMaterialNormID == mItem.FactoryMaterialNormID).FirstOrDefault();
            //                    }
            //                    else
            //                    {
            //                        dbMaterialNorm = new FactoryMaterialNorm();
            //                        dbFinishedProductNorm.FactoryMaterialNorm.Add(dbMaterialNorm);
            //                    }
            //                    if (dbMaterialNorm != null)
            //                    {
            //                        AutoMapper.Mapper.Map<DTO.FactoryMaterialNorm, FactoryMaterialNorm>(mItem, dbMaterialNorm);
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            dbFinishedProductNorm = new FactoryFinishedProductNorm();
            //            dbItem.FactoryFinishedProductNorm.Add(dbFinishedProductNorm);
            //            if (item.FactoryMaterialNorms != null)
            //            {
            //                foreach (var mItem in item.FactoryMaterialNorms)
            //                {
            //                    dbMaterialNorm = new FactoryMaterialNorm();
            //                    dbFinishedProductNorm.FactoryMaterialNorm.Add(dbMaterialNorm);
            //                    AutoMapper.Mapper.Map<DTO.FactoryMaterialNorm, FactoryMaterialNorm>(mItem, dbMaterialNorm);
            //                }
            //            }
            //        }
            //        //mapping
            //        if (dbFinishedProductNorm != null)
            //        {
            //            AutoMapper.Mapper.Map<DTO.FactoryFinishedProductNorm, FactoryFinishedProductNorm>(item, dbFinishedProductNorm);
            //        }
            //    }
            //}
            AutoMapper.Mapper.Map<DTO.FactoryNorm, FactoryNorm>(dtoItem, dbItem);
        }

    }
}
