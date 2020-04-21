using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.WareHouseMng
{
    internal class DataConverter
    {

        public DataConverter()
        {
            string mapName = "WarehouseMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<WareHouseMng_WareHouseSearchResult_View, DTO.WareHouseMng.WareHouseSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WareHouseMng_WareHouseArea_View, DTO.WareHouseMng.WareHouseArea>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WareHouseMng_WareHouse_View, DTO.WareHouseMng.WareHouse>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing<DALBase.Resolver.DateTime2JsonResolver>().FromMember(s => s.UpdatedDate))
                    .ForMember(d => d.ConcurrencyFlag, o => o.ResolveUsing<DALBase.Resolver.ByteArray2StringResolver>().FromMember(s => s.ConcurrencyFlag))
                    .ForMember(d => d.Areas, o => o.MapFrom(s => s.WareHouseMng_WareHouseArea_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WareHouseMng.WareHouseArea, WareHouseArea>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WareHouseID, o => o.Ignore())
                    .ForMember(d => d.WareHouseAreaID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WareHouseMng.WareHouse, WareHouse>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.WareHouseID, o => o.Ignore())
                    .ForMember(d => d.WareHouseArea, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());
                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }
        
        public List<DTO.WareHouseMng.WareHouseSearchResult> DB2DTO_WareHouseSearchResultList(List<WareHouseMng_WareHouseSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WareHouseMng_WareHouseSearchResult_View>, List<DTO.WareHouseMng.WareHouseSearchResult>>(dbItems);
        }

        public DTO.WareHouseMng.WareHouse DB2DTO_WareHouse(WareHouseMng_WareHouse_View dbItem)
        {
            

            return AutoMapper.Mapper.Map<WareHouseMng_WareHouse_View, DTO.WareHouseMng.WareHouse>(dbItem);
        }

        public void DTO2DB(DTO.WareHouseMng.WareHouse dtoItem, ref WareHouse dbItem)
        {
            // map child collection
            // delete case
            List<int> toBeDeletedAreaIDs = new List<int>();
            foreach (WareHouseArea toBeDeletedArea in dbItem.WareHouseArea)
            {
                if (!dtoItem.Areas.Select(o => o.WareHouseAreaID).Contains(toBeDeletedArea.WareHouseAreaID))
                {
                    toBeDeletedAreaIDs.Add(toBeDeletedArea.WareHouseAreaID);
                }
            }
            foreach (int toBeDeletedAreaID in toBeDeletedAreaIDs)
            {
                dbItem.WareHouseArea.Remove(dbItem.WareHouseArea.FirstOrDefault(o => o.WareHouseAreaID == toBeDeletedAreaID));
            }

            foreach (DTO.WareHouseMng.WareHouseArea dtoArea in dtoItem.Areas)
            {
                // add new case
                WareHouseArea dbArea = null;
                if (dtoArea.WareHouseAreaID <= 0)
                {
                    dbArea = new WareHouseArea();
                    dbArea.WareHouse = dbItem;
                    dbItem.WareHouseArea.Add(dbArea);
                }
                else
                {
                    dbArea = dbItem.WareHouseArea.FirstOrDefault(o => o.WareHouseAreaID == dtoArea.WareHouseAreaID);
                }

                if (dbArea != null)
                {
                    AutoMapper.Mapper.Map<DTO.WareHouseMng.WareHouseArea, WareHouseArea>(dtoArea, dbArea);
                }
            }

            // map fields
            AutoMapper.Mapper.Map<DTO.WareHouseMng.WareHouse, WareHouse>(dtoItem, dbItem);
        }
    }
}
