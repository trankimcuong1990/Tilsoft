using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.CostInvoice2TypeMng.DAL
{
   public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<CostInvoice2TypeMng_CostInvoice2TypeSearch_View, DTO.CostInvoice2TypeSearchDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<CostInvoice2TypeMng_CostInvoice2Type_View, DTO.CostInvoice2TypeDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.CostInvoice2TypeDto, CostInvoice2Type>()
                .ForMember(d => d.CostInvoice2TypeID, o => o.Ignore())
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);

        }

        public List<DTO.CostInvoice2TypeSearchDto> DB2DTO_CostInvoice2TypeSearch(List<CostInvoice2TypeMng_CostInvoice2TypeSearch_View> items)
        {
            return AutoMapper.Mapper.Map<List<CostInvoice2TypeMng_CostInvoice2TypeSearch_View>, List<DTO.CostInvoice2TypeSearchDto>>(items);
        }

        public DTO.CostInvoice2TypeDto DB2DTO_ConstInvoice2Type(CostInvoice2TypeMng_CostInvoice2Type_View item)
        {
            return AutoMapper.Mapper.Map<CostInvoice2TypeMng_CostInvoice2Type_View, DTO.CostInvoice2TypeDto>(item);
        }

        public void DTO2DB_CostInvoice(DTO.CostInvoice2TypeDto dto, ref CostInvoice2Type db)
        {
            AutoMapper.Mapper.Map<DTO.CostInvoice2TypeDto, CostInvoice2Type>(dto, db);
        }
    }
}
