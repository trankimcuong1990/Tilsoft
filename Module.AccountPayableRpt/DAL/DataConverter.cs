using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt.DAL
{
    public class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AccountPayableRpt_TotalLiabilitiesHTML_View, DTO.TotalLiabilities>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableRpt_SupportFactoryRawMaterialSearch_View, DTO.SupplierSupport>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<LiabilitiesRpt_TongHopCongNoPhaiThuHTML_View, DTO.TongHopCongNoPhaiThu>()
                //   .IgnoreAllNonExisting()
                //   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableRpt_Client_View, DTO.ClientSupport>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<LiabilitiesRpt_ChiTietCongNoPhaiThu_View, DTO.ChiTietPhaiThuDto>()
                //  .IgnoreAllNonExisting()
                //  .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                //  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<LiabilitiesRpt_SumChiTietCongNoPhaiThu_View, DTO.SumChiTietPhaiThuDto>()
                //  .IgnoreAllNonExisting()
                //  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<LiabilitiesRpt_ChiTietPhaiThuByInvoiceNo_View, DTO.ChiTietPhaiThuByInvoiceNoDto>()
                //  .IgnoreAllNonExisting()
                //  .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                //  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableRpt_DetailOfLiabilities_View, DTO.DetailOfLiabilitiesDto>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.LoadingDate, o => o.ResolveUsing(s => s.LoadingDate.HasValue ? s.LoadingDate.Value.ToString("dd/MM/yyyy") : ""))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableRpt_SumDetailOfLiabilities_View, DTO.SumDetailOfLiabilitiesDto>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableRpt_function_DetailOfLiabilitiesByInvoiceNo_Result, DTO.DetailOfLiabilitiesByInvoiceNoDto>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableRpt_KeyRawMaterial_View, DTO.KeyRawMaterial>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableRpt_DueColor_View, DTO.DueColorDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableMng_PurchaseInvoice_View, DTO.PurchaseInvoiceDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountPayableMng_FactoryRawMaterial_View, DTO.SupplierDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.TotalLiabilities> DB2DTO_TotalLiabilities(List<AccountPayableRpt_TotalLiabilitiesHTML_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountPayableRpt_TotalLiabilitiesHTML_View>, List<DTO.TotalLiabilities>>(dbItems);
        }
        public List<DTO.SupplierSupport> DB2DTO_Supplier(List<AccountPayableRpt_SupportFactoryRawMaterialSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountPayableRpt_SupportFactoryRawMaterialSearch_View>, List<DTO.SupplierSupport>>(dbItems);
        }
        //public List<DTO.TongHopCongNoPhaiThu> DB2DTO_TongHopCongNoPhaiThu(List<LiabilitiesRpt_TongHopCongNoPhaiThuHTML_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<LiabilitiesRpt_TongHopCongNoPhaiThuHTML_View>, List<DTO.TongHopCongNoPhaiThu>>(dbItems);
        //}
        public List<DTO.ClientSupport> DB2DTO_Client(List<AccountPayableRpt_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountPayableRpt_Client_View>, List<DTO.ClientSupport>>(dbItems);
        }

        public List<DTO.PurchaseInvoiceDTO> DB2DTO_PurchaseInvoice(List<AccountPayableMng_PurchaseInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountPayableMng_PurchaseInvoice_View>, List<DTO.PurchaseInvoiceDTO>>(dbItems);
        }
        //public List<DTO.SumChiTietPhaiThuDto> DB2DTO_SumCongNoPhaiThu(List<LiabilitiesRpt_SumChiTietCongNoPhaiThu_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<LiabilitiesRpt_SumChiTietCongNoPhaiThu_View>, List<DTO.SumChiTietPhaiThuDto>>(dbItems);
        //}
        //public List<DTO.ChiTietPhaiThuDto> DB2DTO_ChiTietCongNoPhaiThu(List<LiabilitiesRpt_ChiTietCongNoPhaiThu_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<LiabilitiesRpt_ChiTietCongNoPhaiThu_View>, List<DTO.ChiTietPhaiThuDto>>(dbItems);
        //}
        //public List<DTO.ChiTietPhaiThuByInvoiceNoDto> DB2DTO_ChiTietCongNoPhaiThuByInvoice(List<LiabilitiesRpt_ChiTietPhaiThuByInvoiceNo_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<LiabilitiesRpt_ChiTietPhaiThuByInvoiceNo_View>, List<DTO.ChiTietPhaiThuByInvoiceNoDto>>(dbItems);
        //}
        public List<DTO.SumDetailOfLiabilitiesDto> DB2DTO_SumLiabilities(List<AccountPayableRpt_SumDetailOfLiabilities_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountPayableRpt_SumDetailOfLiabilities_View>, List<DTO.SumDetailOfLiabilitiesDto>>(dbItems);
        }
        public List<DTO.DetailOfLiabilitiesDto> DB2DTO_DetailOfLiabilities(List<AccountPayableRpt_DetailOfLiabilities_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountPayableRpt_DetailOfLiabilities_View>, List<DTO.DetailOfLiabilitiesDto>>(dbItems);
        }
        public List<DTO.DetailOfLiabilitiesByInvoiceNoDto> DB2DTO_DetailOfLiabilitiesByInvoice(List<AccountPayableRpt_function_DetailOfLiabilitiesByInvoiceNo_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountPayableRpt_function_DetailOfLiabilitiesByInvoiceNo_Result>, List<DTO.DetailOfLiabilitiesByInvoiceNoDto>>(dbItems);
        }
    }
}
