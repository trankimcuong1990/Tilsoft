using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.ClientPaymentMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ClientPayment_View, DTO.ClientPayment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.BallanceDate, o => o.ResolveUsing(s => s.BallanceDate.HasValue ? s.BallanceDate.Value.ToString("dd/MM/yyyy") : ""))                    
                    .ForMember(d => d.ClientPaymentDetails, o => o.MapFrom(s => s.ClientPaymentMng_ClientPaymentDetail_View))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ClientPaymentDeduction_View, DTO.ClientPaymentDeduction>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ClientPaymentDetail_View, DTO.ClientPaymentDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaidDate, o => o.ResolveUsing(s => s.PaidDate.HasValue ? s.PaidDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SaleOrderDate, o => o.ResolveUsing(s => s.SaleOrderDate.HasValue ? s.SaleOrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ClientPaymentDeductions, o => o.MapFrom(s => s.ClientPaymentMng_ClientPaymentDeduction_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ClientPaymentSearchResult_View, DTO.ClientPaymentSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentDate, o => o.ResolveUsing(s => s.PaymentDate.HasValue ? s.PaymentDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.BallanceDate, o => o.ResolveUsing(s => s.BallanceDate.HasValue ? s.BallanceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_ECommercialInvoiceSearchResult_View, DTO.ECommercialInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_SaleOrderForDeductionSearchResult_View, DTO.SaleOrderForDeductionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientPaymentMng_SaleOrderSearchResult_View, DTO.SaleOrderSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientPayment, ClientPayment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientPaymentID, o => o.Ignore())
                    .ForMember(d => d.PaymentDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ClientPaymentDetail, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientPaymentDetail, ClientPaymentDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientPaymentDetailID, o => o.Ignore())
                    .ForMember(d => d.ClientPaymentID, o => o.Ignore())
                    .ForMember(d => d.PaidDate, o => o.Ignore())
                    .ForMember(d => d.ClientPaymentDeduction, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientPaymentDeduction, ClientPaymentDeduction>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientPaymentDeductionID, o => o.Ignore())
                    .ForMember(d => d.ClientPaymentDetailID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ClientPaymentSearchResult> DB2DTO_ClientPaymentSearchResultList(List<ClientPaymentMng_ClientPaymentSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientPaymentMng_ClientPaymentSearchResult_View>, List<DTO.ClientPaymentSearchResult>>(dbItems);
        }

        public DTO.ClientPayment DB2DTO_ClientPayment(ClientPaymentMng_ClientPayment_View dbItem)
        {
            return AutoMapper.Mapper.Map<ClientPaymentMng_ClientPayment_View, DTO.ClientPayment>(dbItem);
        }

        public List<DTO.ECommercialInvoiceSearchResult> DB2DTO_ECommercialInvoiceSearchResultList(List<ClientPaymentMng_ECommercialInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientPaymentMng_ECommercialInvoiceSearchResult_View>, List<DTO.ECommercialInvoiceSearchResult>>(dbItems);
        }

        public List<DTO.SaleOrderSearchResult> DB2DTO_SaleOrderSearchResultList(List<ClientPaymentMng_SaleOrderSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientPaymentMng_SaleOrderSearchResult_View>, List<DTO.SaleOrderSearchResult>>(dbItems);
        }

        public List<DTO.SaleOrderForDeductionSearchResult> DB2DTO_SaleOrderForDeductionSearchResultList(List<ClientPaymentMng_SaleOrderForDeductionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientPaymentMng_SaleOrderForDeductionSearchResult_View>, List<DTO.SaleOrderForDeductionSearchResult>>(dbItems);
        }

        public void DTO2DB(DTO.ClientPayment dtoItem, ref ClientPayment dbItem)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.ClientPayment, ClientPayment>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            if (DateTime.TryParse(dtoItem.PaymentDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
            {
                dbItem.PaymentDate = tmpDate;
            }

            // map payment detail
            if (dtoItem.ClientPaymentDetails != null)
            {
                // check for child rows deleted
                foreach (ClientPaymentDetail dbDetail in dbItem.ClientPaymentDetail.ToArray())
                {
                    if (!dtoItem.ClientPaymentDetails.Select(o => o.ClientPaymentDetailID).Contains(dbDetail.ClientPaymentDetailID))
                    {
                        dbItem.ClientPaymentDetail.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.ClientPaymentDetail dtoDetail in dtoItem.ClientPaymentDetails)
                {
                    ClientPaymentDetail dbDetail;
                    if (dtoDetail.ClientPaymentDetailID <= 0)
                    {
                        dbDetail = new ClientPaymentDetail();
                        dbItem.ClientPaymentDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.ClientPaymentDetail.FirstOrDefault(o => o.ClientPaymentDetailID == dtoDetail.ClientPaymentDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientPaymentDetail, ClientPaymentDetail>(dtoDetail, dbDetail);
                        if (DateTime.TryParse(dtoDetail.PaidDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbDetail.PaidDate = tmpDate;
                        }

                        // map deduction
                        if (dtoDetail.ClientPaymentDeductions != null)
                        {
                            // check for child rows deleted
                            foreach (ClientPaymentDeduction dbDeduction in dbDetail.ClientPaymentDeduction.ToArray())
                            {
                                if (!dtoDetail.ClientPaymentDeductions.Select(o => o.ClientPaymentDeductionID).Contains(dbDeduction.ClientPaymentDeductionID))
                                {
                                    dbDetail.ClientPaymentDeduction.Remove(dbDeduction);
                                }
                            }

                            // map child row
                            foreach (DTO.ClientPaymentDeduction dtoDeduction in dtoDetail.ClientPaymentDeductions)
                            {
                                ClientPaymentDeduction dbDeduction;
                                if (dtoDeduction.ClientPaymentDeductionID <= 0)
                                {
                                    dbDeduction = new ClientPaymentDeduction();
                                    dbDetail.ClientPaymentDeduction.Add(dbDeduction);
                                }
                                else
                                {
                                    dbDeduction = dbDetail.ClientPaymentDeduction.FirstOrDefault(o => o.ClientPaymentDeductionID == dtoDeduction.ClientPaymentDeductionID);
                                }

                                if (dbDeduction != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ClientPaymentDeduction, ClientPaymentDeduction>(dtoDeduction, dbDeduction);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
