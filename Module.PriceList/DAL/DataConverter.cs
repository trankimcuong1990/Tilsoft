using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.PriceList.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PriceListMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<PriceListMng_PriceList_View, DTO.PriceListDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DateValid, o => o.ResolveUsing(s => s.DateValid.HasValue ? s.DateValid.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PriceListDTO, PriceList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DateValid, o => o.Ignore())
                    .ForMember(d => d.PriceListID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PriceListDTO> DB2DTO_PriceListSearch(List<PriceListMng_PriceList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceListMng_PriceList_View>, List<DTO.PriceListDTO>>(dbItems);
        }

        public DTO.PriceListDTO DB2DTO_PriceList(PriceListMng_PriceList_View dbItem)
        {
            return AutoMapper.Mapper.Map<PriceListMng_PriceList_View, DTO.PriceListDTO>(dbItem);
        }

        public void DTO2DB_PriceList(DTO.PriceListDTO dtoItem, ref PriceList dbItem)
        {
            AutoMapper.Mapper.Map<DTO.PriceListDTO, PriceList>(dtoItem, dbItem);
            if (!(dtoItem.PriceListID > 0))
            {
                dbItem.DateValid = DateTime.Now;
            }
        }

    }
}
