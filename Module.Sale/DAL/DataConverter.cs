using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.Sale.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SaleMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<Sale, DTO.SaleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SaleDTO, Sale>()
                    .IgnoreAllNonExisting()
                .ForMember(d => d.SaleID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SaleDTO> DB2DTO_SaleSearch(List<Sale> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Sale>, List<DTO.SaleDTO>>(dbItems);
        }

        public DTO.SaleDTO DB2DTO_Sale(Sale dbItem)
        {
            DTO.SaleDTO Sale = AutoMapper.Mapper.Map<Sale, DTO.SaleDTO>(dbItem);
            return Sale;
        }

        public void DTO2DB_Sale(DTO.SaleDTO dtoItem, ref Sale dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SaleDTO, Sale>(dtoItem, dbItem);
        }

    }
}
