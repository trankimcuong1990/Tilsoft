using Library;
using System.Collections.Generic;

namespace Module.AccountReceivableRpt.DAL
{
    public class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_TotalLiabilitiesHTML_View, DTO.TotalLiabilities>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_FactoryRawMaterial_View, DTO.SupplierDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_function_DetailOfLiabilities_Result, DTO.DetailOfLiabilitiesDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_SumDetailOfLiabilities_View, DTO.SumDetailOfLiabilitiesDto>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_function_DetailOfLiabilitiesByInvoiceNo_Result, DTO.DetailOfLiabilitiesByInvoiceNoDto>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_KeyRawMaterial_View, DTO.KeyRawMaterial>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_DueColor_View, DTO.DueColorDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountReceivableRpt_SupportFactoryRawMaterialSearch_View, DTO.SupportFactoryRawMaterialDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.TotalLiabilities> DB2DTO_TotalLiabilities(List<AccountReceivableRpt_TotalLiabilitiesHTML_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountReceivableRpt_TotalLiabilitiesHTML_View>, List<DTO.TotalLiabilities>>(dbItems);
        }
        public List<DTO.SupplierDTO> DB2DTO_Supplier(List<AccountReceivableRpt_FactoryRawMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountReceivableRpt_FactoryRawMaterial_View>, List<DTO.SupplierDTO>>(dbItems);
        }
        public List<DTO.SumDetailOfLiabilitiesDto> DB2DTO_SumLiabilities(List<AccountReceivableRpt_SumDetailOfLiabilities_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountReceivableRpt_SumDetailOfLiabilities_View>, List<DTO.SumDetailOfLiabilitiesDto>>(dbItems);
        }
        public List<DTO.DetailOfLiabilitiesDto> DB2DTO_DetailOfLiabilities(List<AccountReceivableRpt_function_DetailOfLiabilities_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountReceivableRpt_function_DetailOfLiabilities_Result>, List<DTO.DetailOfLiabilitiesDto>>(dbItems);
        }
        public List<DTO.DetailOfLiabilitiesByInvoiceNoDto> DB2DTO_DetailOfLiabilitiesByInvoice(List<AccountReceivableRpt_function_DetailOfLiabilitiesByInvoiceNo_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountReceivableRpt_function_DetailOfLiabilitiesByInvoiceNo_Result>, List<DTO.DetailOfLiabilitiesByInvoiceNoDto>>(dbItems);
        }

        public List<DTO.SupportFactoryRawMaterialDTO> DB2DTO_SearchSupplier(List<AccountReceivableRpt_SupportFactoryRawMaterialSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountReceivableRpt_SupportFactoryRawMaterialSearch_View>, List<DTO.SupportFactoryRawMaterialDTO>>(dbItems);
        }
    }
}
