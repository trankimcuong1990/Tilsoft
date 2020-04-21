using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.LedgerAccountMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<LedgerAccountMng_LedgerAccountSearchResult_View, DTO.LedgerAccountSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LedgerAccountMng_LedgerAccount_View, DTO.LedgerAccount>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.LedgerAccount, LedgerAccount>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<LedgerAccountMng_LedgerAccountDetailOverview_View, DTO.LedgerAccountDetailOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.LedgerAccountSearchResult> DB2DTO_LedgerAccountSearchResultList(List<LedgerAccountMng_LedgerAccountSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LedgerAccountMng_LedgerAccountSearchResult_View>, List<DTO.LedgerAccountSearchResult>>(dbItems);
        }
        public DTO.LedgerAccount DB2DTO_LedgerAccount(LedgerAccountMng_LedgerAccount_View dbItem)
        {
            return AutoMapper.Mapper.Map<LedgerAccountMng_LedgerAccount_View, DTO.LedgerAccount>(dbItem);
        }
        public List<DTO.LedgerAccountDetailOverview> DB2DTO_LedgerAccountDetailOverview(List<LedgerAccountMng_LedgerAccountDetailOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LedgerAccountMng_LedgerAccountDetailOverview_View>, List<DTO.LedgerAccountDetailOverview>>(dbItems);
        }

        public void DTO2BD(DTO.LedgerAccount dtoItem, ref LedgerAccount dbItem)
        {
            AutoMapper.Mapper.Map<DTO.LedgerAccount, LedgerAccount>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
