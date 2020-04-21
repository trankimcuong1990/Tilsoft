using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.AVFInwardInvoiceMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View, DTO.AVFInwardInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVFInwardInvoiceMng_AVFInwardInvoice_View, DTO.AVFInwardInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.AVFInwardInvoiceSearchResult> DB2DTO_AVFInwardInvoiceSearchResultList(List<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVFInwardInvoiceMng_AVFInwardInvoice_SearchResult_View>, List<DTO.AVFInwardInvoiceSearchResult>>(dbItems);
        }
        public DTO.AVFInwardInvoice DB2DTO_AVFInwardInvoice(AVFInwardInvoiceMng_AVFInwardInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<AVFInwardInvoiceMng_AVFInwardInvoice_View, DTO.AVFInwardInvoice>(dbItem);
        }
    }
}
