using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseInvoiceGrossMarginRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WarehouseInvoiceGrossMarginRpt";

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<WarehouseInvoiceGrossMarginRpt_GrossMargin_View, DTO.GrossMarginData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : null));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.GrossMarginData> DB2DTO_GrossMarginDataSearchResult(List<WarehouseInvoiceGrossMarginRpt_GrossMargin_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WarehouseInvoiceGrossMarginRpt_GrossMargin_View>, List<DTO.GrossMarginData>>(dbItem);
        }
    }
}
