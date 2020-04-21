using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.DeliveryTermMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DeliveryTermMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DeliveryTermMng_DeliveryTermSearchResult_View, DTO.DeliveryTermMng.DeliveryTermSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryTermMng_DeliveryTerm_View, DTO.DeliveryTermMng.DeliveryTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DeliveryTermMng.DeliveryTerm, DeliveryTerm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DeliveryTermID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DeliveryTermMng.DeliveryTermSearchResult> DB2DTO_DeliveryTermSearchResultList(List<DeliveryTermMng_DeliveryTermSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DeliveryTermMng_DeliveryTermSearchResult_View>, List<DTO.DeliveryTermMng.DeliveryTermSearchResult>>(dbItems);
        }

        public DTO.DeliveryTermMng.DeliveryTerm DB2DTO_Material(DeliveryTermMng_DeliveryTerm_View dbItem)
        {
            return AutoMapper.Mapper.Map<DeliveryTermMng_DeliveryTerm_View, DTO.DeliveryTermMng.DeliveryTerm>(dbItem);
        }

        public void DTO2BD_Material(DTO.DeliveryTermMng.DeliveryTerm dtoItem, ref DeliveryTerm dbItem)
        {
            AutoMapper.Mapper.Map<DTO.DeliveryTermMng.DeliveryTerm, DeliveryTerm>(dtoItem, dbItem);
        }
    }
}
