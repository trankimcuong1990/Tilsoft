using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierPaymentTermMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SupplierPaymentTermMng";

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            //SEARCH VIEW
            AutoMapper.Mapper.CreateMap<SupplierPaymentTermMng_SupplierPaymentTerm_SearchView, DTO.SearchSupplierPaymentTermDto>()
                .IgnoreAllNonExisting();

            //DB2DTO
            AutoMapper.Mapper.CreateMap<DTO.SupplierPaymentTermDto, SupplierPaymentTerm>()
                .IgnoreAllNonExisting()
                .ForMember(s => s.SupplierPaymentTermID, o => o.Ignore());

            //GET VIEW
            AutoMapper.Mapper.CreateMap<SupplierPaymentTermMng_SupplierPaymentTerm_View, DTO.SupplierPaymentTermDto>()
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        //GET VIEW
        public DTO.SupplierPaymentTermDto DB2DTO_SupplierPaymentTerm(SupplierPaymentTermMng_SupplierPaymentTerm_View dbItem)
        {
            return AutoMapper.Mapper.Map<SupplierPaymentTermMng_SupplierPaymentTerm_View, DTO.SupplierPaymentTermDto>(dbItem);
        }

        public void DTO2DB_SupplierPaymentTerm(DTO.SupplierPaymentTermDto dtoItem, ref SupplierPaymentTerm dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SupplierPaymentTermDto, SupplierPaymentTerm>(dtoItem, dbItem);
        }

        //SEARCH VIEW
        public List<DTO.SearchSupplierPaymentTermDto> DB2DTO_SeachSupplierPaymentTerm(List<SupplierPaymentTermMng_SupplierPaymentTerm_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupplierPaymentTermMng_SupplierPaymentTerm_SearchView>, List<DTO.SearchSupplierPaymentTermDto>>(dbItems);
        }
    }
}
