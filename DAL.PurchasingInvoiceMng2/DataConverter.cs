using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.PurchasingInvoiceMng2
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PurchasingInvoiceMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                
                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PurchasingInvoiceSearch_View, DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PurchasingInvoiceContainers, o => o.MapFrom(s => s.PurchasingInvoiceMng_ContainerNo_View)) // Get Purchasing Invoice Container.
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PurchasingInvoice_View, DTO.PurchasingInvoiceMng.PurchasingInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedPriceDate, o => o.ResolveUsing(s => s.ConfirmedPriceDate.HasValue ? s.ConfirmedPriceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.ResolveUsing(s => s.ConcurrencyFlag != null ? Convert.ToBase64String(s.ConcurrencyFlag) : ""))
                    .ForMember(d => d.PurchasingInvoiceDetails, o => o.MapFrom(s => s.PurchasingInvoiceMng_PurchasingInvoiceDetail_View))
                    .ForMember(d => d.PurchasingInvoiceSparepartDetails, o => o.MapFrom(s => s.PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View))
                    .ForMember(d => d.PurchasingInvoiceExtraDetails, o => o.MapFrom(s => s.PurchasingInvoiceMng_PurchasingInvoiceExtraDetail_View))
                    .ForMember(d => d.PurchasingInvoiceSampleDetails, o => o.MapFrom(s => s.PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_View))
                    .ForMember(d => d.PurchasingCreditNotes, o => o.MapFrom(s => s.PurchasingInvoiceMng_PurchasingCreditNote_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PurchasingInvoiceDetail_View, DTO.PurchasingInvoiceMng.PurchasingInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View, DTO.PurchasingInvoiceMng.PurchasingInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PurchasingInvoiceExtraDetail_View, DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_View, DTO.PurchasingInvoiceMng.PurchasingInvoiceSampleDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceMng.PurchasingInvoice, PurchasingInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchasingInvoiceID, o => o.Ignore())
                    .ForMember(d => d.PurchasingInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.PurchasingInvoiceExtraDetail, o => o.Ignore())
                    .ForMember(d => d.PurchasingInvoiceSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedPriceDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceMng.PurchasingInvoiceDetail, PurchasingInvoiceDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.PurchasingInvoiceDetailID, o => o.Ignore())
                     .ForMember(d => d.UnitPrice, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceMng.PurchasingInvoiceSparepartDetail, PurchasingInvoiceSparepartDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.PurchasingInvoiceSparepartDetailID, o => o.Ignore())
                     .ForMember(d => d.UnitPrice, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail, PurchasingInvoiceExtraDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.PurchasingInvoiceExtraDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceMng.PurchasingInvoiceSampleDetail, PurchasingInvoiceSampleDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.PurchasingInvoiceSampleDetailID, o => o.Ignore());

                //INIT
                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_Booking_View, DTO.PurchasingInvoiceMng.Booking>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ShipedDate, o => o.ResolveUsing(s => s.ShipedDate.HasValue ? s.ShipedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_LoadingPlanDetail_View, DTO.PurchasingInvoiceMng.LoadingPlanDetail>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_LoadingPlanSparepartDetail_View, DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PriceDifferenceInvoiceSetting_View, DTO.PurchasingInvoiceMng.PriceDifferenceInvoiceSetting>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Credit Note
                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_PurchasingCreditNote_View, DTO.PurchasingInvoiceMng.PurchasingCreditNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreditNoteDate, o => o.ResolveUsing(s => s.CreditNoteDate.HasValue ? s.CreditNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PurchasingInvoiceMng.PurchasingCreditNote, DTO.PurchasingInvoiceMng.PurchasingCreditNote>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Mapping Purchasing Invoice Container.
                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_ContainerNo_View, DTO.PurchasingInvoiceMng.PurchasingInvoiceContainer>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingInvoiceMng_Company_View, DTO.PurchasingInvoiceMng.CompanyDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> DB2DTO_PurchasingInvoiceSearch(List<PurchasingInvoiceMng_PurchasingInvoiceSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoiceMng_PurchasingInvoiceSearch_View>, List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch>>(dbItems);
        }
        public DTO.PurchasingInvoiceMng.PurchasingInvoice DB2DTO_PurchasingInvoice(PurchasingInvoiceMng_PurchasingInvoice_View dbItem)
        {
            DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem = AutoMapper.Mapper.Map<PurchasingInvoiceMng_PurchasingInvoice_View, DTO.PurchasingInvoiceMng.PurchasingInvoice>(dbItem);
            return dtoItem;
        }
        public void DTO2DB_PurchasingInvoice(int iRequesterID, PurchasingInvoiceMng_Booking_View dbBooking, DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem, ref PurchasingInvoice dbItem)
        {
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            //int userOfficeID = fwFactory.GetUserOffice(iRequesterID);
            int[] internalCompanyID = new int[] { 1, 2, 3 };
            int userOfficeID = 1;
            if (!internalCompanyID.ToList().Contains(fwFactory.GetCompanyID(iRequesterID).Value)) // 1, 2, 3 = AVF, AVS, AVT
            {
                userOfficeID = 4; // fake the office id equal to 4, backward compatible
            }
            //Purchasing Invoice Detail
            if (dtoItem.PurchasingInvoiceDetails != null)
            {
                //CHECK
                foreach (var item in dbItem.PurchasingInvoiceDetail.ToArray())
                {
                    if (!dtoItem.PurchasingInvoiceDetails.Select(s => s.PurchasingInvoiceDetailID).Contains(item.PurchasingInvoiceDetailID))
                    {
                        dbItem.PurchasingInvoiceDetail.Remove(item);
                    }
                }
                //MAP
                foreach (var dtoDetail in dtoItem.PurchasingInvoiceDetails)
                {
                    PurchasingInvoiceDetail dbDetail;
                    if (dtoDetail.PurchasingInvoiceDetailID < 0)
                    {
                        dbDetail = new PurchasingInvoiceDetail();
                        dbDetail.UnitPrice = dbBooking.PurchasingInvoiceMng_LoadingPlanDetail_View.Where(o => o.LoadingPlanDetailID == dtoDetail.LoadingPlanDetailID).FirstOrDefault().UnitPrice;
                        dbItem.PurchasingInvoiceDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchasingInvoiceDetail.FirstOrDefault(o => o.PurchasingInvoiceDetailID == dtoDetail.PurchasingInvoiceDetailID);
                        if (userOfficeID != 4)
                        {
                            dbDetail.UnitPrice = dtoDetail.UnitPrice;
                        }
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchasingInvoiceMng.PurchasingInvoiceDetail, PurchasingInvoiceDetail>(dtoDetail, dbDetail);
                    }
                }
            }
            //Purchasing Invoice Sparepart Detail
            if (dtoItem.PurchasingInvoiceSparepartDetails != null)
            {
                //CHECK
                foreach (var item in dbItem.PurchasingInvoiceSparepartDetail.ToArray())
                {
                    if (!dtoItem.PurchasingInvoiceSparepartDetails.Select(s => s.PurchasingInvoiceSparepartDetailID).Contains(item.PurchasingInvoiceSparepartDetailID))
                    {
                        dbItem.PurchasingInvoiceSparepartDetail.Remove(item);
                    }
                }
                //MAP
                foreach (var dtoDetail in dtoItem.PurchasingInvoiceSparepartDetails)
                {
                    PurchasingInvoiceSparepartDetail dbDetail;
                    if (dtoDetail.PurchasingInvoiceSparepartDetailID < 0)
                    {
                        dbDetail = new PurchasingInvoiceSparepartDetail();
                        dbDetail.UnitPrice = dbBooking.PurchasingInvoiceMng_LoadingPlanSparepartDetail_View.Where(o => o.LoadingPlanSparepartDetailID == dtoDetail.LoadingPlanSparepartDetailID).FirstOrDefault().UnitPrice;
                        dbItem.PurchasingInvoiceSparepartDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchasingInvoiceSparepartDetail.FirstOrDefault(o => o.PurchasingInvoiceSparepartDetailID == dtoDetail.PurchasingInvoiceSparepartDetailID);
                        if (userOfficeID != 4)
                        {
                            dbDetail.UnitPrice = dtoDetail.UnitPrice;
                        }
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchasingInvoiceMng.PurchasingInvoiceSparepartDetail, PurchasingInvoiceSparepartDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            //Purchasing Invoice Sparepart Detail
            if (dtoItem.PurchasingInvoiceExtraDetails != null)
            {
                //CHECK
                foreach (var item in dbItem.PurchasingInvoiceExtraDetail.ToArray())
                {
                    if (!dtoItem.PurchasingInvoiceExtraDetails.Select(s => s.PurchasingInvoiceExtraDetailID).Contains(item.PurchasingInvoiceExtraDetailID))
                    {
                        dbItem.PurchasingInvoiceExtraDetail.Remove(item);
                    }
                }
                //MAP
                foreach (var dtoDetail in dtoItem.PurchasingInvoiceExtraDetails)
                {
                    PurchasingInvoiceExtraDetail dbDetail;
                    if (dtoDetail.PurchasingInvoiceExtraDetailID < 0)
                    {
                        dbDetail = new PurchasingInvoiceExtraDetail();
                        dbItem.PurchasingInvoiceExtraDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchasingInvoiceExtraDetail.FirstOrDefault(o => o.PurchasingInvoiceExtraDetailID == dtoDetail.PurchasingInvoiceExtraDetailID);
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchasingInvoiceMng.PurchasingInvoiceExtraDetail, PurchasingInvoiceExtraDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            //Purchasing Invoice Sample Detail
            if (dtoItem.PurchasingInvoiceSampleDetails != null)
            {
                //CHECK
                foreach (var item in dbItem.PurchasingInvoiceSampleDetail.ToArray())
                {
                    if (!dtoItem.PurchasingInvoiceSampleDetails.Select(s => s.PurchasingInvoiceSampleDetailID).Contains(item.PurchasingInvoiceSampleDetailID))
                    {
                        dbItem.PurchasingInvoiceSampleDetail.Remove(item);
                    }
                }
                //MAP
                foreach (var dtoDetail in dtoItem.PurchasingInvoiceSampleDetails)
                {
                    PurchasingInvoiceSampleDetail dbDetail;
                    if (dtoDetail.PurchasingInvoiceSampleDetailID < 0)
                    {
                        dbDetail = new PurchasingInvoiceSampleDetail();
                        dbDetail.UnitPrice = dbBooking.PurchasingInvoiceMng_LoadingPlanSampleDetail_View.Where(o => o.LoadingPlanSampleDetailID == dtoDetail.LoadingPlanSampleDetailID).FirstOrDefault().UnitPrice;
                        dbItem.PurchasingInvoiceSampleDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.PurchasingInvoiceSampleDetail.FirstOrDefault(o => o.PurchasingInvoiceSampleDetailID == dtoDetail.PurchasingInvoiceSampleDetailID);
                        if (userOfficeID != 4)
                        {
                            dbDetail.UnitPrice = dtoDetail.UnitPrice;
                        }
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchasingInvoiceMng.PurchasingInvoiceSampleDetail, PurchasingInvoiceSampleDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            //Purchasing Invoice
            AutoMapper.Mapper.Map<DTO.PurchasingInvoiceMng.PurchasingInvoice, PurchasingInvoice>(dtoItem, dbItem);
            if (dtoItem.PurchasingInvoiceID > 0)
            {
                dbItem.UpdatedDate = DateTime.Now;
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
            }
            else
            {
                dbItem.CreatedDate = DateTime.Now;
                dbItem.CreatedBy = dtoItem.UpdatedBy;
            }
            dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
            if(dbItem.SupplierID == 336037)
            {
                dbItem.FactoryRawMaterialID = 250;
            }
        }
        public List<DTO.PurchasingInvoiceMng.Booking> DB2DTO_Booking(List<PurchasingInvoiceMng_Booking_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoiceMng_Booking_View>, List<DTO.PurchasingInvoiceMng.Booking>>(dbItems);
        }
        public List<DTO.PurchasingInvoiceMng.LoadingPlanDetail> DB2DTO_LoadingPlanDetail(List<PurchasingInvoiceMng_LoadingPlanDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoiceMng_LoadingPlanDetail_View>, List<DTO.PurchasingInvoiceMng.LoadingPlanDetail>>(dbItems);
        }
        public List<DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail> DB2DTO_LoadingPlanSparepartDetail(List<PurchasingInvoiceMng_LoadingPlanSparepartDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoiceMng_LoadingPlanSparepartDetail_View>, List<DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail>>(dbItems);
        }
        public List<DTO.PurchasingInvoiceMng.PriceDifferenceInvoiceSetting> DB2DTO_PriceDifferenceInvoiceSetting(List<PurchasingInvoiceMng_PriceDifferenceInvoiceSetting_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoiceMng_PriceDifferenceInvoiceSetting_View>, List<DTO.PurchasingInvoiceMng.PriceDifferenceInvoiceSetting>>(dbItems);
        }
        public List<DTO.PurchasingInvoiceMng.PurchasingCreditNote> DB2DTO_PurchasingCreditNote(List<PurchasingInvoiceMng_PurchasingCreditNote_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoiceMng_PurchasingCreditNote_View>, List<DTO.PurchasingInvoiceMng.PurchasingCreditNote>>(dbItems);
        }
        public DTO.PurchasingInvoiceMng.PurchasingCreditNote DTO2DTO_PurchasingCreditNote(DTO.PurchasingInvoiceMng.PurchasingCreditNote dtoItem)
        {
            return AutoMapper.Mapper.Map<DTO.PurchasingInvoiceMng.PurchasingCreditNote, DTO.PurchasingInvoiceMng.PurchasingCreditNote>(dtoItem);
        }
        public List<DTO.PurchasingInvoiceMng.CompanyDTO> DB2DTO_Company(List<PurchasingInvoiceMng_Company_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingInvoiceMng_Company_View>, List<DTO.PurchasingInvoiceMng.CompanyDTO>>(dbItems);
        }
    }
}
