using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;


namespace Module.FactoryInvoiceMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "FactoryInvoiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_FactoryInvoiceSearchResult_View, DTO.FactoryInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_BookingSearchResult_View, DTO.BookingSearchResult>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CutOffDate, o => o.ResolveUsing(s => s.CutOffDate.HasValue ? s.CutOffDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ETD, o => o.ResolveUsing(s => s.ETD.HasValue ? s.ETD.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_FactoryInvoice_View, DTO.FactoryInvoice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                   .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                   .ForMember(d => d.FactoryInvoiceDetails, o => o.MapFrom(s => s.FactoryInvoiceMng_FactoryInvoiceDetail_View))
                   .ForMember(d => d.FactoryInvoiceSparepartDetails, o => o.MapFrom(s => s.FactoryInvoiceMng_FactoryInvoiceSparepartDetail_View))
                   .ForMember(d => d.FactoryInvoiceExtras, o => o.MapFrom(s => s.FactoryInvoiceMng_FactoryInvoiceExtra_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_FactoryInvoiceDetail_View, DTO.FactoryInvoiceDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_FactoryInvoiceSparepartDetail_View, DTO.FactoryInvoiceSparepartDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_FactoryInvoiceExtra_View, DTO.FactoryInvoiceExtra>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_FactoryOrderDetailSearchResult_View, DTO.FactoryOrderDetailSearchResult>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceMng_FactoryOrderSparepartDetailSearchResult_View, DTO.FactoryOrderSparepartDetailSearchResult>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));






                AutoMapper.Mapper.CreateMap<DTO.FactoryInvoice, FactoryInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryInvoiceID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ScanFile, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceExtra, o => o.Ignore())
                    .ForMember(d => d.SubTotalAmount, o => o.Ignore())
                    .ForMember(d => d.TotalAmount, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryInvoiceDetail, FactoryInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SubTotal, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceDetailID, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryInvoiceSparepartDetail, FactoryInvoiceSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SubTotal, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceSparepartDetailID, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryInvoiceExtra, FactoryInvoiceExtra>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SubTotal, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceExtraID, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryInvoiceSearchResult> DB2DTO_FactoryInvoiceSearchResultList(List<FactoryInvoiceMng_FactoryInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryInvoiceMng_FactoryInvoiceSearchResult_View>, List<DTO.FactoryInvoiceSearchResult>>(dbItems);
        }

        public List<DTO.BookingSearchResult> DB2DTO_BookingSearchResultList(List<FactoryInvoiceMng_BookingSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryInvoiceMng_BookingSearchResult_View>, List<DTO.BookingSearchResult>>(dbItems);
        }

        public DTO.BookingSearchResult DB2DTO_BookingSearchResult(FactoryInvoiceMng_BookingSearchResult_View dbItems)
        {
            return AutoMapper.Mapper.Map<FactoryInvoiceMng_BookingSearchResult_View, DTO.BookingSearchResult>(dbItems);
        }

        public DTO.FactoryInvoice DB2DTO_FactoryInvoice(FactoryInvoiceMng_FactoryInvoice_View dbItems)
        {
            return AutoMapper.Mapper.Map<FactoryInvoiceMng_FactoryInvoice_View, DTO.FactoryInvoice>(dbItems);
        }

        public List<DTO.FactoryOrderDetailSearchResult> DB2DTO_FactoryOrderDetailSearchResultList(List<FactoryInvoiceMng_FactoryOrderDetailSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryInvoiceMng_FactoryOrderDetailSearchResult_View>, List<DTO.FactoryOrderDetailSearchResult>>(dbItems);
        }

        public List<DTO.FactoryOrderSparepartDetailSearchResult> DB2DTO_FactoryOrderSparepartDetailSearchResultList(List<FactoryInvoiceMng_FactoryOrderSparepartDetailSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryInvoiceMng_FactoryOrderSparepartDetailSearchResult_View>, List<DTO.FactoryOrderSparepartDetailSearchResult>>(dbItems);
        }

        public void DTO2DB(DTO.FactoryInvoice dtoItem, ref FactoryInvoice dbItem, string _tempFolder)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.FactoryInvoice, FactoryInvoice>(dtoItem, dbItem);

            // insert file
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            if (dtoItem.ScanFile_HasChange)
            {
                dbItem.ScanFile = fwFactory.CreateNoneImageFilePointer(_tempFolder, dtoItem.ScanFile_NewFile, dtoItem.ScanFile);
            }
            if (!string.IsNullOrEmpty(dtoItem.InvoiceDate))
            {
                if (DateTime.TryParse(dtoItem.InvoiceDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.InvoiceDate = tmpDate;
                }
            }
            decimal subtotal = 0;

            // map detail
            if (dtoItem.FactoryInvoiceDetails != null)
            {
                // check for child rows deleted
                foreach (FactoryInvoiceDetail dbDetail in dbItem.FactoryInvoiceDetail.ToArray())
                {
                    if (!dtoItem.FactoryInvoiceDetails.Select(o => o.FactoryInvoiceDetailID).Contains(dbDetail.FactoryInvoiceDetailID))
                    {
                        dbItem.FactoryInvoiceDetail.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.FactoryInvoiceDetail dtoDetail in dtoItem.FactoryInvoiceDetails)
                {
                    FactoryInvoiceDetail dbDetail;
                    if (dtoDetail.FactoryInvoiceDetailID <= 0)
                    {
                        dbDetail = new FactoryInvoiceDetail();
                        dbItem.FactoryInvoiceDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.FactoryInvoiceDetail.FirstOrDefault(o => o.FactoryInvoiceDetailID == dtoDetail.FactoryInvoiceDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryInvoiceDetail, FactoryInvoiceDetail>(dtoDetail, dbDetail);

                        if (dtoDetail.UnitPrice.HasValue && dtoDetail.Quantity.HasValue)
                        {
                            dbDetail.SubTotal = Math.Round(dtoDetail.UnitPrice.Value * dtoDetail.Quantity.Value, 2, MidpointRounding.AwayFromZero);
                            subtotal += Math.Round(dtoDetail.UnitPrice.Value * dtoDetail.Quantity.Value, 2, MidpointRounding.AwayFromZero);
                        }                        
                    }
                }
            }

            // map sparepart detail
            if (dtoItem.FactoryInvoiceSparepartDetails != null)
            {
                // check for child rows deleted
                foreach (FactoryInvoiceSparepartDetail dbSparepartDetail in dbItem.FactoryInvoiceSparepartDetail.ToArray())
                {
                    if (!dtoItem.FactoryInvoiceSparepartDetails.Select(o => o.FactoryInvoiceSparepartDetailID).Contains(dbSparepartDetail.FactoryInvoiceSparepartDetailID))
                    {
                        dbItem.FactoryInvoiceSparepartDetail.Remove(dbSparepartDetail);
                    }
                }

                // map child rows
                foreach (DTO.FactoryInvoiceSparepartDetail dtoSparepartDetail in dtoItem.FactoryInvoiceSparepartDetails)
                {
                    FactoryInvoiceSparepartDetail dbSparepartDetail;
                    if (dtoSparepartDetail.FactoryInvoiceSparepartDetailID <= 0)
                    {
                        dbSparepartDetail = new FactoryInvoiceSparepartDetail();
                        dbItem.FactoryInvoiceSparepartDetail.Add(dbSparepartDetail);
                    }
                    else
                    {
                        dbSparepartDetail = dbItem.FactoryInvoiceSparepartDetail.FirstOrDefault(o => o.FactoryInvoiceSparepartDetailID == dtoSparepartDetail.FactoryInvoiceSparepartDetailID);
                    }

                    if (dbSparepartDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryInvoiceSparepartDetail, FactoryInvoiceSparepartDetail>(dtoSparepartDetail, dbSparepartDetail);

                        if (dtoSparepartDetail.UnitPrice.HasValue && dtoSparepartDetail.Quantity.HasValue)
                        {
                            dbSparepartDetail.SubTotal = Math.Round(dtoSparepartDetail.UnitPrice.Value * dtoSparepartDetail.Quantity.Value, 2, MidpointRounding.AwayFromZero);
                            subtotal += Math.Round(dtoSparepartDetail.UnitPrice.Value * dtoSparepartDetail.Quantity.Value, 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }
            }

            // map extra
            if (dtoItem.FactoryInvoiceExtras != null)
            {
                // check for child rows deleted
                foreach (FactoryInvoiceExtra dbExtra in dbItem.FactoryInvoiceExtra.ToArray())
                {
                    if (!dtoItem.FactoryInvoiceExtras.Select(o => o.FactoryInvoiceExtraID).Contains(dbExtra.FactoryInvoiceExtraID))
                    {
                        dbItem.FactoryInvoiceExtra.Remove(dbExtra);
                    }
                }

                // map child rows
                foreach (DTO.FactoryInvoiceExtra dtoExtra in dtoItem.FactoryInvoiceExtras)
                {
                    FactoryInvoiceExtra dbExtra;
                    if (dtoExtra.FactoryInvoiceExtraID <= 0)
                    {
                        dbExtra = new FactoryInvoiceExtra();
                        dbItem.FactoryInvoiceExtra.Add(dbExtra);
                    }
                    else
                    {
                        dbExtra = dbItem.FactoryInvoiceExtra.FirstOrDefault(o => o.FactoryInvoiceExtraID == dtoExtra.FactoryInvoiceExtraID);
                    }

                    if (dbExtra != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryInvoiceExtra, FactoryInvoiceExtra>(dtoExtra, dbExtra);

                        if (dtoExtra.UnitPrice.HasValue && dtoExtra.Quantity.HasValue)
                        {
                            dbExtra.SubTotal = Math.Round(dtoExtra.UnitPrice.Value * dtoExtra.Quantity.Value, 2, MidpointRounding.AwayFromZero);
                            subtotal += Math.Round(dtoExtra.UnitPrice.Value * dtoExtra.Quantity.Value, 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }
            }

            dbItem.SubTotalAmount = subtotal;
            if (dbItem.DeductedAmount.HasValue)
            {
                dbItem.TotalAmount = dbItem.SubTotalAmount.Value - dbItem.DeductedAmount.Value;
            }
            else
            {
                dbItem.TotalAmount = dbItem.SubTotalAmount;
            }
        }
    }
}
