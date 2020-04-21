using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.CostInvoice2CreditorMng.DAL
{
   public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;


            AutoMapper.Mapper.CreateMap<CostInvoice2CreditorMng_CostInvoice2CreditorSearch_View, DTO.CostInvoice2CreditorMngSearchDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<CostInvoice2CreditorMng_CostInvoice2Creditor_View, DTO.CostInvoice2CreditorMngDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.CostInvoice2CreditorMngDto, CostInvoice2Creditor>()
                .ForMember(s => s.CostInvoice2CreditorID, o => o.Ignore())
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);

        }

        public List<DTO.CostInvoice2CreditorMngSearchDto> DB2DTO_CostInvoice2CreditorSearchMng(List<CostInvoice2CreditorMng_CostInvoice2CreditorSearch_View> items)
        {
            return AutoMapper.Mapper.Map<List<CostInvoice2CreditorMng_CostInvoice2CreditorSearch_View>, List<DTO.CostInvoice2CreditorMngSearchDto>>(items);
        }
        
        public DTO.CostInvoice2CreditorMngDto DB2DTO_CostInvoice2CreditorMng(CostInvoice2CreditorMng_CostInvoice2Creditor_View item)
        {
            return AutoMapper.Mapper.Map<CostInvoice2CreditorMng_CostInvoice2Creditor_View, DTO.CostInvoice2CreditorMngDto>(item);
        }
        
        public void BTO2DB_CostInvoice2CreditorMng(DTO.CostInvoice2CreditorMngDto dto, ref CostInvoice2Creditor db)
        {
            AutoMapper.Mapper.Map<DTO.CostInvoice2CreditorMngDto, CostInvoice2Creditor>(dto, db);
        }
    }
}
