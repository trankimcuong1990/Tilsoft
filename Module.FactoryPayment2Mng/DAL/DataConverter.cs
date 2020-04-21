using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FactoryPayment2Mng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "FactoryPayment2Mng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryPayment2Mng_FactoryPayment2SearchResult_View, DTO.FactoryPaymentSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPayment2Mng_PurchasingInvoiceSearchResult_View, DTO.PurchasingInvoiceSearchResult>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPayment2Mng_FactoryPayment2_View, DTO.FactoryPayment>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                   .ForMember(d => d.FactoryPaymentDetails, o => o.MapFrom(s => s.FactoryPayment2Mng_FactoryPayment2Detail_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPayment2Mng_FactoryPayment2Detail_View, DTO.FactoryPaymentDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryPayment, FactoryPayment2>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryPayment2ID, o => o.Ignore())
                    .ForMember(d => d.PaymentDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.IsConfirmed, o => o.Ignore())
                    .ForMember(d => d.ConfirmedAmount, o => o.Ignore())
                    .ForMember(d => d.ConfirmedRemark, o => o.Ignore())
                    .ForMember(d => d.FactoryPayment2Detail, o => o.Ignore())
                    .ForMember(d => d.TotalAmount, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryPaymentDetail, FactoryPayment2Detail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryPayment2DetailID, o => o.Ignore())
                    .ForMember(d => d.FactoryPayment2ID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryPaymentSearchResult> DB2DTO_FactoryPaymentSearchResultList(List<FactoryPayment2Mng_FactoryPayment2SearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPayment2Mng_FactoryPayment2SearchResult_View>, List<DTO.FactoryPaymentSearchResult>>(dbItems);
        }

        public DTO.FactoryPayment DB2DTO_FactoryPayment(FactoryPayment2Mng_FactoryPayment2_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryPayment2Mng_FactoryPayment2_View, DTO.FactoryPayment>(dbItem);
        }

        public List<DTO.PurchasingInvoiceSearchResult> DB2DTO_PurchasingInvoiceSearchResultList(List<FactoryPayment2Mng_PurchasingInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPayment2Mng_PurchasingInvoiceSearchResult_View>, List<DTO.PurchasingInvoiceSearchResult>>(dbItems);
        }

        public void DTO2DB(DTO.FactoryPayment dtoItem, ref FactoryPayment2 dbItem)
        {
            decimal subtotal = 0;

            // map fields
            AutoMapper.Mapper.Map<DTO.FactoryPayment, FactoryPayment2>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.PaymentDate))
            {
                if (DateTime.TryParse(dtoItem.PaymentDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.PaymentDate = tmpDate;
                }
            }

            // map detail
            if (dtoItem.FactoryPaymentDetails != null)
            {
                // check for child rows deleted
                foreach (FactoryPayment2Detail dbDetail in dbItem.FactoryPayment2Detail.ToArray())
                {
                    if (!dtoItem.FactoryPaymentDetails.Select(o => o.FactoryPayment2DetailID).Contains(dbDetail.FactoryPayment2DetailID))
                    {
                        dbItem.FactoryPayment2Detail.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.FactoryPaymentDetail dtoDetail in dtoItem.FactoryPaymentDetails)
                {
                    FactoryPayment2Detail dbDetail;
                    if (dtoDetail.FactoryPayment2DetailID <= 0)
                    {
                        dbDetail = new FactoryPayment2Detail();
                        dbItem.FactoryPayment2Detail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryPayment2Detail.FirstOrDefault(o => o.FactoryPayment2DetailID == dtoDetail.FactoryPayment2DetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryPaymentDetail, FactoryPayment2Detail>(dtoDetail, dbDetail);

                        if (dtoDetail.PaidAmount.HasValue)
                        {
                            subtotal += Math.Round(dtoDetail.PaidAmount.Value, 2, MidpointRounding.AwayFromZero);
                        }
                        else 
                        {
                            dbDetail.PaidAmount = 0;
                        }

                        if (dtoDetail.PaidAmount.HasValue && dtoDetail.DPDeductedAmount.HasValue && dtoDetail.TotalAmount.HasValue && (dtoDetail.PaidAmount.Value + dtoDetail.DPDeductedAmount.Value) > dtoDetail.TotalAmount.Value)
                        {
                            throw new Exception("DP amount + paid amount (" + (dtoDetail.PaidAmount.Value + dtoDetail.DPDeductedAmount.Value).ToString() + ") for invoice " + dtoDetail.InvoiceNo + " larger than to be paid amount (" + dtoDetail.TotalAmount.Value.ToString() + ")");
                        }
                    }
                }
            }

            if (dbItem.DPAmount.HasValue)
            {
                dbItem.TotalAmount = subtotal + Math.Round(dbItem.DPAmount.Value, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                dbItem.TotalAmount = subtotal;
                dbItem.DPAmount = 0;
            }
        }
    }
}
