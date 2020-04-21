using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.FactoryPayment2BalanceMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "FactoryPayment2BalanceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View, DTO.FactoryPayment2BalanceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPayment2BalanceMng_Detail_View, DTO.Detail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.IssuedDate, o => o.ResolveUsing(s => s.IssuedDate.HasValue ? s.IssuedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryPayment2BalanceSearchResult> DB2DTO_FactoryPayment2BalanceSearchResultList(List<FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View>, List<DTO.FactoryPayment2BalanceSearchResult>>(dbItems);
        }

        public DTO.FactoryPayment2BalanceSearchResult DB2DTO_FactoryPayment2BalanceSearchResult(FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryPayment2BalanceMng_FactoryPayment2BalanceSearchResult_View, DTO.FactoryPayment2BalanceSearchResult>(dbItem);
        }

        public List<DTO.Detail> DB2DTO_DetailList(List<FactoryPayment2BalanceMng_Detail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPayment2BalanceMng_Detail_View>, List<DTO.Detail>>(dbItems);
        }
    }
}
