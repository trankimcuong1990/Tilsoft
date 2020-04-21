using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.CashBookBalanceMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<CashBookBalanceMng_CashBookBalance_View, DTO.CashBookBalance>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookBalanceMng_CashBookBalanceDetail_View, DTO.CashBookBalanceDetail>()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CashBookBalance, CashBookBalance>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CashBookBalanceDetail, o => o.Ignore())
                    .ForMember(d => d.CashBookBalanceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.CashBookBalanceDetail, CashBookBalanceDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CashBookBalanceDetailID, o => o.Ignore())
                   .ForMember(d => d.CashBookBalanceID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CashBookBalance> DB2DTO_CashBookBalanceList(List<CashBookBalanceMng_CashBookBalance_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookBalanceMng_CashBookBalance_View>, List<DTO.CashBookBalance>>(dbItems);
        }

        public DTO.CashBookBalance DB2DTO_CashBookBalance(CashBookBalanceMng_CashBookBalance_View dbItem)
        {
            return AutoMapper.Mapper.Map<CashBookBalanceMng_CashBookBalance_View, DTO.CashBookBalance>(dbItem);
        }

        public List<DTO.CashBookBalanceDetail> DB2DTO_CashBookBalanceDetailList(List<CashBookBalanceMng_CashBookBalanceDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookBalanceMng_CashBookBalanceDetail_View>, List<DTO.CashBookBalanceDetail>>(dbItems);
        }
    }
}
