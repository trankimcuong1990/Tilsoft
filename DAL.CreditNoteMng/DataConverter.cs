using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.CreditNoteMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "CreditNoteMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CreditNoteMng_CreditNoteDetail_View, DTO.CreditNoteMng.CreditNoteDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CreditNoteMng.CreditNoteMng_CreditNoteProductDetail_View, DTO.CreditNoteMng.CreditNoteProductDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CreditNoteMng_CreditNoteSparepartDetail_View, DTO.CreditNoteMng.CreditNoteSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CreditNoteMng_CreditNote_View, DTO.CreditNoteMng.CreditNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForMember(d => d.CreditNoteDetails, o => o.MapFrom(s => s.CreditNoteMng_CreditNoteDetail_View))
                    .ForMember(d => d.CreditNoteProductDetails, o => o.MapFrom(s => s.CreditNoteMng_CreditNoteProductDetail_View))
                    .ForMember(d => d.CreditNoteSparepartDetails, o => o.MapFrom(s => s.CreditNoteMng_CreditNoteSparepartDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.CreditNoteMng.CreditNote, CreditNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreditNoteID, o => o.Ignore())
                    .ForMember(d => d.CreditNoteDetail, o => o.Ignore())
                    .ForMember(d => d.CreditNoteProductDetail, o => o.Ignore())
                    .ForMember(d => d.CreditNoteSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.CreditNoteMng.CreditNoteDetail, CreditNoteDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.CreditNoteDetailID, o => o.Ignore())
                     ;
                AutoMapper.Mapper.CreateMap<DTO.CreditNoteMng.CreditNoteProductDetail, CreditNoteProductDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.CreditNoteProductDetailID, o => o.Ignore())
                     ;
                AutoMapper.Mapper.CreateMap<DTO.CreditNoteMng.CreditNoteSparepartDetail, CreditNoteSparepartDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.CreditNoteSparepartDetailID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<CreditNoteMng_CreditNote_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public DTO.CreditNoteMng.CreditNote DB2DTO_CreditNote(CreditNoteMng_CreditNote_View dbItem)
        {
            DTO.CreditNoteMng.CreditNote dtoItem = AutoMapper.Mapper.Map<CreditNoteMng_CreditNote_View, DTO.CreditNoteMng.CreditNote>(dbItem);
            return dtoItem;
        }

        public void DTO2DB_CreditNote(DTO.CreditNoteMng.CreditNote dtoItem, ref CreditNote dbItem)
        {
            //CreditNote Detail
            List<CreditNoteDetail> item_tobedeleted = new List<CreditNoteDetail>();
            if (dtoItem.CreditNoteDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.CreditNoteDetail.Where(o => !dtoItem.CreditNoteDetails.Select(s => s.CreditNoteDetailID).Contains(o.CreditNoteDetailID)))
                {
                    item_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in item_tobedeleted)
                {
                    dbItem.CreditNoteDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.CreditNoteDetails)
                {
                    CreditNoteDetail dbDetail;
                    if (dtoDetail.CreditNoteDetailID < 0)
                    {
                        dbDetail = new CreditNoteDetail();
                        dbItem.CreditNoteDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.CreditNoteDetail.FirstOrDefault(o => o.CreditNoteDetailID == dtoDetail.CreditNoteDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CreditNoteMng.CreditNoteDetail, CreditNoteDetail>(dtoDetail, dbDetail);
                    }
                }
            }
            //CreditNote Product Detail
            List<CreditNoteProductDetail> product_tobedeleted = new List<CreditNoteProductDetail>();
            if (dtoItem.CreditNoteProductDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.CreditNoteProductDetail.Where(o => !dtoItem.CreditNoteProductDetails.Select(s => s.CreditNoteProductDetailID).Contains(o.CreditNoteProductDetailID)))
                {
                    product_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in product_tobedeleted)
                {
                    dbItem.CreditNoteProductDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.CreditNoteProductDetails)
                {
                    CreditNoteProductDetail dbDetail;
                    if (dtoDetail.CreditNoteProductDetailID < 0)
                    {
                        dbDetail = new CreditNoteProductDetail();
                        dbItem.CreditNoteProductDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.CreditNoteProductDetail.FirstOrDefault(o => o.CreditNoteProductDetailID == dtoDetail.CreditNoteProductDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CreditNoteMng.CreditNoteProductDetail, CreditNoteProductDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            //CreditNote Sparepart Detail
            List<CreditNoteSparepartDetail> sparepart_tobedeleted = new List<CreditNoteSparepartDetail>();
            if (dtoItem.CreditNoteSparepartDetails != null)
            {
                //CHECK
                foreach (var dbDetail in dbItem.CreditNoteSparepartDetail.Where(o => !dtoItem.CreditNoteSparepartDetails.Select(s => s.CreditNoteSparepartDetailID).Contains(o.CreditNoteSparepartDetailID)))
                {
                    sparepart_tobedeleted.Add(dbDetail);
                }
                foreach (var dbDetail in sparepart_tobedeleted)
                {
                    dbItem.CreditNoteSparepartDetail.Remove(dbDetail);
                }
                //MAP
                foreach (var dtoDetail in dtoItem.CreditNoteSparepartDetails)
                {
                    CreditNoteSparepartDetail dbDetail;
                    if (dtoDetail.CreditNoteSparepartDetailID < 0)
                    {
                        dbDetail = new CreditNoteSparepartDetail();
                        dbItem.CreditNoteSparepartDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.CreditNoteSparepartDetail.FirstOrDefault(o => o.CreditNoteSparepartDetailID == dtoDetail.CreditNoteSparepartDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CreditNoteMng.CreditNoteSparepartDetail, CreditNoteSparepartDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            //CreditNote
            AutoMapper.Mapper.Map<DTO.CreditNoteMng.CreditNote, CreditNote>(dtoItem, dbItem);
            if (dtoItem.CreditNoteID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout DB2DTO_Printout(CreditNoteMng_CreditNote_ReportView dbItem)
        {
            DTO.ECommercialInvoiceMng.InvoiceContainerPrintout dtoItem = new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
            dtoItem.Invoices = new List<DTO.ECommercialInvoiceMng.InvoicePrintout>();
            dtoItem.InvoiceDetails = new List<DTO.ECommercialInvoiceMng.InvoiceDetailPrintout>();

            //COPY INVOICE DATA
            DTO.ECommercialInvoiceMng.InvoicePrintout dtoInvoice = AutoMapper.Mapper.Map<CreditNoteMng_CreditNote_ReportView, DTO.ECommercialInvoiceMng.InvoicePrintout>(dbItem);
            dtoItem.Invoices.Add(dtoInvoice);

            //COPY INVOICEDETAIL DATA
            DTO.ECommercialInvoiceMng.InvoiceDetailPrintout dtoInvoiceDetail;

            foreach (CreditNoteMng_CreditNoteDetail_ReportView dbDetail in dbItem.CreditNoteMng_CreditNoteDetail_ReportView)
            {
                //CREATE ITEM ROW
                dtoInvoiceDetail = new DTO.ECommercialInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDetail.Description;
                dtoInvoiceDetail.TotalAmount = dbDetail.Amount;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }
            return dtoItem;
        }
    }
}
