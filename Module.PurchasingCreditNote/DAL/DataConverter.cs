using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
namespace Module.PurchasingCreditNote.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PurchasingCreditNote";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View, DTO.PurchasingCreditNoteDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View, DTO.PurchasingCreditNoteSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_View, DTO.PurchasingCreditNoteExtendDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingCreditNoteMng_PurchasingCreditNote_View, DTO.PurchasingCreditNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreditNoteDate, o => o.ResolveUsing(s => s.CreditNoteDate.HasValue ? s.CreditNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PurchasingCreditNoteDetails, o => o.MapFrom(s => s.PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForMember(d => d.PurchasingCreditNoteSparepartDetails, o => o.MapFrom(s => s.PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View))
                    .ForMember(d => d.PurchasingCreditNoteExtendDetails, o => o.MapFrom(s => s.PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceDetail_View, DTO.PurchasingInvoiceDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceSparepartDetail_View, DTO.PurchasingInvoiceSparepartDetail>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoice_View, DTO.PurchasingInvoice>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.PurchasingInvoiceDetails, o => o.MapFrom(s => s.PurchasingInvoiceDetail_View))
                  .ForMember(d => d.PurchasingInvoiceSparepartDetails, o => o.MapFrom(s => s.PurchasingInvoiceSparepartDetail_View))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //

                AutoMapper.Mapper.CreateMap<DTO.PurchasingCreditNote, PurchasingCreditNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreditNoteDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.PurchasingCreditNoteDetail, o => o.Ignore())
                    .ForMember(d => d.PurchasingCreditNoteSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.PurchasingCreditNoteExtendDetail, o => o.Ignore())
                    .ForMember(d => d.PurchasingCreditNoteID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchasingCreditNoteDetail, PurchasingCreditNoteDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchasingCreditNoteDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchasingCreditNoteSparepartDetail, PurchasingCreditNoteSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchasingCreditNoteSparepartDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchasingCreditNoteExtendDetail, PurchasingCreditNoteExtendDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchasingCreditNoteExtendDetailID, o => o.Ignore());

                //

                //AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceDetail, DTO.PurchasingCreditNoteDetail>()
                //   .IgnoreAllNonExisting()
                //   .ForMember(d => d.PurchasingCreditNoteDetailID, o => o.ResolveUsing(s => -1 * s.PurchasingInvoiceDetailID))
                //   .ForMember(d => d.Quantity, o => o.ResolveUsing(s => -1 * s.Quantity))
                //   .ForMember(d => d.Amount, o => o.ResolveUsing(s => -1 * s.Amount))
                //   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceSparepartDetail, DTO.PurchasingCreditNoteSparepartDetail>()
                //  .IgnoreAllNonExisting()
                //  .ForMember(d => d.PurchasingCreditNoteSparepartDetailID, o => o.ResolveUsing(s => -1 * s.PurchasingInvoiceSparepartDetailID))
                //   .ForMember(d => d.Quantity, o => o.ResolveUsing(s => -1 * s.Quantity))
                //   .ForMember(d => d.Amount, o => o.ResolveUsing(s => -1 * s.Amount))
                //  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoice, DTO.PurchasingCreditNote>()
                  .IgnoreAllNonExisting()
                  //.ForMember(d => d.PurchasingCreditNoteDetails, o => o.MapFrom(s => s.PurchasingInvoiceDetails))
                  //.ForMember(d => d.PurchasingCreditNoteSparepartDetails, o => o.MapFrom(s => s.PurchasingInvoiceSparepartDetails))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.PurchasingCreditNote DB2DTO_PurchasingCreditNote(PurchasingCreditNoteMng_PurchasingCreditNote_View dbItem) {
            return AutoMapper.Mapper.Map<PurchasingCreditNoteMng_PurchasingCreditNote_View, DTO.PurchasingCreditNote>(dbItem);
        }
        
        public DTO.PurchasingInvoice DB2DTO_PurchasingInvoice(PurchasingInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<PurchasingInvoice_View, DTO.PurchasingInvoice>(dbItem);
        }

        public void DTO2DB_PurchasingCreditNote(DTO.PurchasingCreditNote dtoItem, ref PurchasingCreditNote dbItem)
        {            
            if (dtoItem.PurchasingCreditNoteDetails != null)
            {
                List<PurchasingCreditNoteDetail> product_tobedeleted = new List<PurchasingCreditNoteDetail>();
                foreach (var item in dbItem.PurchasingCreditNoteDetail.Where(o => !dtoItem.PurchasingCreditNoteDetails.Select(s => s.PurchasingCreditNoteDetailID).Contains(o.PurchasingCreditNoteDetailID)))
                {
                    product_tobedeleted.Add(item);
                }
                foreach (var item in product_tobedeleted)
                {
                    dbItem.PurchasingCreditNoteDetail.Remove(item);
                }
                foreach (var item in dtoItem.PurchasingCreditNoteDetails)
                {
                    PurchasingCreditNoteDetail dbDetail;
                    if (item.PurchasingCreditNoteDetailID < 0)
                    {
                        dbDetail = new PurchasingCreditNoteDetail();
                        dbItem.PurchasingCreditNoteDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchasingCreditNoteDetail.FirstOrDefault(o => o.PurchasingCreditNoteDetailID == item.PurchasingCreditNoteDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchasingCreditNoteDetail, PurchasingCreditNoteDetail>(item, dbDetail);
                    }
                }
            }

            if (dtoItem.PurchasingCreditNoteSparepartDetails != null)
            {
                List<PurchasingCreditNoteSparepartDetail> product_tobedeleted = new List<PurchasingCreditNoteSparepartDetail>();
                foreach (var item in dbItem.PurchasingCreditNoteSparepartDetail.Where(o => !dtoItem.PurchasingCreditNoteSparepartDetails.Select(s => s.PurchasingCreditNoteSparepartDetailID).Contains(o.PurchasingCreditNoteSparepartDetailID)))
                {
                    product_tobedeleted.Add(item);
                }
                foreach (var item in product_tobedeleted)
                {
                    dbItem.PurchasingCreditNoteSparepartDetail.Remove(item);
                }
                foreach (var item in dtoItem.PurchasingCreditNoteSparepartDetails)
                {
                    PurchasingCreditNoteSparepartDetail dbDetail;
                    if (item.PurchasingCreditNoteSparepartDetailID < 0)
                    {
                        dbDetail = new PurchasingCreditNoteSparepartDetail();
                        dbItem.PurchasingCreditNoteSparepartDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchasingCreditNoteSparepartDetail.FirstOrDefault(o => o.PurchasingCreditNoteSparepartDetailID == item.PurchasingCreditNoteSparepartDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchasingCreditNoteSparepartDetail, PurchasingCreditNoteSparepartDetail>(item, dbDetail);
                    }
                }
            }

            if (dtoItem.PurchasingCreditNoteExtendDetails != null)
            {
                List<PurchasingCreditNoteExtendDetail> product_tobedeleted = new List<PurchasingCreditNoteExtendDetail>();
                foreach (var item in dbItem.PurchasingCreditNoteExtendDetail.Where(o => !dtoItem.PurchasingCreditNoteExtendDetails.Select(s => s.PurchasingCreditNoteExtendDetailID).Contains(o.PurchasingCreditNoteExtendDetailID)))
                {
                    product_tobedeleted.Add(item);
                }
                foreach (var item in product_tobedeleted)
                {
                    dbItem.PurchasingCreditNoteExtendDetail.Remove(item);
                }
                foreach (var item in dtoItem.PurchasingCreditNoteExtendDetails)
                {
                    PurchasingCreditNoteExtendDetail dbDetail;
                    if (item.PurchasingCreditNoteExtendDetailID < 0)
                    {
                        dbDetail = new PurchasingCreditNoteExtendDetail();
                        dbItem.PurchasingCreditNoteExtendDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchasingCreditNoteExtendDetail.FirstOrDefault(o => o.PurchasingCreditNoteExtendDetailID == item.PurchasingCreditNoteExtendDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchasingCreditNoteExtendDetail, PurchasingCreditNoteExtendDetail>(item, dbDetail);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.PurchasingCreditNote, PurchasingCreditNote>(dtoItem, dbItem);
            if (dtoItem.PurchasingCreditNoteID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
            dbItem.CreditNoteDate = dtoItem.CreditNoteDate.ConvertStringToDateTime();

        }

        public DTO.PurchasingCreditNote DTO2DTO_PurchasingCreditNote(DTO.PurchasingInvoice dtoInvoice)
        {
            return AutoMapper.Mapper.Map<DTO.PurchasingInvoice, DTO.PurchasingCreditNote>(dtoInvoice);
        }
    
        public List<DTO.PurchasingInvoice> DB2DTO_PurchasingInvoiceSearch(List<PurchasingInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoice_View>,List<DTO.PurchasingInvoice>>(dbItems);
        }
        public List<DTO.PurchasingCreditNote> DB2DTO_PurchasingCreditNoteSearch(List<PurchasingCreditNoteMng_PurchasingCreditNote_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingCreditNoteMng_PurchasingCreditNote_View>, List<DTO.PurchasingCreditNote>>(dbItems);
        }
    }
}
