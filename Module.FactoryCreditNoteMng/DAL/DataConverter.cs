using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FactoryCreditNoteMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "FactoryCreditNoteMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View, DTO.FactoryCreditNoteSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.IssuedDate, o => o.ResolveUsing(s => s.IssuedDate.HasValue ? s.IssuedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View, DTO.FactoryInvoiceSearchResult>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryCreditNoteMng_FactoryCreditNote_View, DTO.FactoryCreditNote>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.IssuedDate, o => o.ResolveUsing(s => s.IssuedDate.HasValue ? s.IssuedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                   .ForMember(d => d.FactoryCreditNoteDetails, o => o.MapFrom(s => s.FactoryCreditNoteMng_FactoryCreditNoteDetail_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryCreditNoteMng_FactoryCreditNoteDetail_View, DTO.FactoryCreditNoteDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryCreditNote, FactoryCreditNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryCreditNoteID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.FactoryCreditNoteDetail, o => o.Ignore())
                    .ForMember(d => d.TotalAmount, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryCreditNoteDetail, FactoryCreditNoteDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryCreditNoteDetailID, o => o.Ignore())
                    .ForMember(d => d.FactoryCreditNoteID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryCreditNoteSearchResult> DB2DTO_FactoryCreditNoteSearchResultList(List<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryCreditNoteMng_FactoryCreditNoteSearchResult_View>, List<DTO.FactoryCreditNoteSearchResult>>(dbItems);
        }

        public DTO.FactoryCreditNote DB2DTO_FactoryCreditNote(FactoryCreditNoteMng_FactoryCreditNote_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryCreditNoteMng_FactoryCreditNote_View, DTO.FactoryCreditNote>(dbItem);
        }

        public List<DTO.FactoryInvoiceSearchResult> DB2DTO_FactoryInvoiceSearchResultList(List<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryCreditNoteMng_FactoryInvoiceSearchResult_View>, List<DTO.FactoryInvoiceSearchResult>>(dbItems);
        }

        public void DTO2DB(DTO.FactoryCreditNote dtoItem, ref FactoryCreditNote dbItem)
        {
            decimal subtotal = 0;

            // map fields
            AutoMapper.Mapper.Map<DTO.FactoryCreditNote, FactoryCreditNote>(dtoItem, dbItem);

            // map detail
            if (dtoItem.FactoryCreditNoteDetails != null)
            {
                // check for child rows deleted
                foreach (FactoryCreditNoteDetail dbDetail in dbItem.FactoryCreditNoteDetail.ToArray())
                {
                    if (!dtoItem.FactoryCreditNoteDetails.Select(o => o.FactoryCreditNoteDetailID).Contains(dbDetail.FactoryCreditNoteDetailID))
                    {
                        dbItem.FactoryCreditNoteDetail.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.FactoryCreditNoteDetail dtoDetail in dtoItem.FactoryCreditNoteDetails)
                {
                    FactoryCreditNoteDetail dbDetail;
                    if (dtoDetail.FactoryCreditNoteDetailID <= 0)
                    {
                        dbDetail = new FactoryCreditNoteDetail();
                        dbItem.FactoryCreditNoteDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryCreditNoteDetail.FirstOrDefault(o => o.FactoryCreditNoteDetailID == dtoDetail.FactoryCreditNoteDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryCreditNoteDetail, FactoryCreditNoteDetail>(dtoDetail, dbDetail);

                        if (dtoDetail.CreditAmount.HasValue)
                        {
                            subtotal += Math.Round(dtoDetail.CreditAmount.Value);
                        }
                    }
                }
            }

            dbItem.TotalAmount = subtotal;
        }
    }
}
