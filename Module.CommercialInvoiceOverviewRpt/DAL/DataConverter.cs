using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.CommercialInvoiceOverviewRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "CommercialInvoiceOverviewRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CommercialInvoiceOverviewRpt_CommercialInvoice_View, DTO.CommercialInvoice>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CommercialInvoice> DB2DTO_CommercialInvoiceList(List<CommercialInvoiceOverviewRpt_CommercialInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CommercialInvoiceOverviewRpt_CommercialInvoice_View>, List<DTO.CommercialInvoice>>(dbItems);
        }
    }
}
