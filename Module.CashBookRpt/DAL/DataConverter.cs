using Library;
using System.Collections.Generic;

namespace Module.CashBookRpt.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CashBookRpt_SearchResult_View, DTO.CashBookRpt_SearchResultDto>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.NoteDate, o => o.ResolveUsing(s => s.NoteDate.HasValue ? s.NoteDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookRpt_SupportPaymentType_View, DTO.CashBookRptPaymentTypeDto>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CashBookRpt_BankAccount_View, DTO.CashBookRptBankAccountDto>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.CashBookRptBankAccountDto> DB2DTO_BankAccount(List<CashBookRpt_BankAccount_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookRpt_BankAccount_View>, List<DTO.CashBookRptBankAccountDto>>(dbItems);
        }
        public List<DTO.CashBookRptPaymentTypeDto> DB2DTO_PaymentType(List<CashBookRpt_SupportPaymentType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookRpt_SupportPaymentType_View>, List<DTO.CashBookRptPaymentTypeDto>>(dbItems);
        }
        public List<DTO.CashBookRpt_SearchResultDto> DB2DTO_SearchResults(List<CashBookRpt_SearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CashBookRpt_SearchResult_View>, List<DTO.CashBookRpt_SearchResultDto>>(dbItems);
        }

    }
}
