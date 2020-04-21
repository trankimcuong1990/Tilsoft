using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.PODMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PODMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PODMng_PODSearchResult_View, DTO.PODMng.PODSearchResult>()
                        .IgnoreAllNonExisting()
                        .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PODMng_POD_View, DTO.PODMng.POD>()
                        .IgnoreAllNonExisting()
                        .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PODMng.POD, POD>()
                        .IgnoreAllNonExisting()
                        .ForMember(d => d.PoDID, o => o.Ignore());           

                FrameworkSetting.Setting.Maps.Add(mapName);
                    
            }
        }

        public List<DTO.PODMng.PODSearchResult> DB2DTO_PODSearchResultList(List<PODMng_PODSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PODMng_PODSearchResult_View>, List<DTO.PODMng.PODSearchResult>>(dbItems);
        }

        public DTO.PODMng.POD DB2DTO_POD(PODMng_POD_View dbItem)
        {
            return AutoMapper.Mapper.Map<PODMng_POD_View, DTO.PODMng.POD>(dbItem);
        }

        public void DB2DTO_POD(DTO.PODMng.POD dtoItem, ref POD dbItem)
        {
            AutoMapper.Mapper.Map<DTO.PODMng.POD, POD>(dtoItem, dbItem);
        }
    }
}
