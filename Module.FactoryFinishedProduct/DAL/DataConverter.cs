using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryFinishedProduct.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryFinishedProductMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryFinishedProduct, DTO.FactoryFinishedProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d =>d.FactoryFinishedProductPriceDTOs, o => o.MapFrom(s =>s.FactoryFinishedProductPrice))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryFinishedProductDTO, FactoryFinishedProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryFinishedProductID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryFinishedProductPrice, DTO.FactoryFinishedProductPriceDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryFinishedProductDTO> DB2DTO_FactoryFinishedProductSearch(List<FactoryFinishedProduct> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryFinishedProduct>, List<DTO.FactoryFinishedProductDTO>>(dbItems);
        }

        public DTO.FactoryFinishedProductDTO DB2DTO_FactoryFinishedProduct(FactoryFinishedProduct dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryFinishedProduct, DTO.FactoryFinishedProductDTO>(dbItem);
        }

        public void DTO2DB_FactoryFinishedProduct(DTO.FactoryFinishedProductDTO dtoItem, ref FactoryFinishedProduct dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryFinishedProductDTO, FactoryFinishedProduct>(dtoItem, dbItem);
        }
    }
}
