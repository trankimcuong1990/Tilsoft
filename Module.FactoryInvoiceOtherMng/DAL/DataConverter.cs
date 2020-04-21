using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;


namespace Module.FactoryInvoiceOtherMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "FactoryInvoiceOtherMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryInvoiceOtherMng_FactoryInvoiceSearchResult_View, DTO.FactoryInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceOtherMng_FactoryInvoice_View, DTO.FactoryInvoice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                   .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                   .ForMember(d => d.FactoryInvoiceExtras, o => o.MapFrom(s => s.FactoryInvoiceOtherMng_FactoryInvoiceExtra_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryInvoiceOtherMng_FactoryInvoiceExtra_View, DTO.FactoryInvoiceExtra>()
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
                    .ForMember(d => d.FactoryInvoiceExtra, o => o.Ignore())
                    .ForMember(d => d.SubTotalAmount, o => o.Ignore())
                    .ForMember(d => d.TotalAmount, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryInvoiceExtra, FactoryInvoiceExtra>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SubTotal, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceExtraID, o => o.Ignore())
                    .ForMember(d => d.FactoryInvoiceID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryInvoiceSearchResult> DB2DTO_FactoryInvoiceSearchResultList(List<FactoryInvoiceOtherMng_FactoryInvoiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryInvoiceOtherMng_FactoryInvoiceSearchResult_View>, List<DTO.FactoryInvoiceSearchResult>>(dbItems);
        }

        public DTO.FactoryInvoice DB2DTO_FactoryInvoice(FactoryInvoiceOtherMng_FactoryInvoice_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryInvoiceOtherMng_FactoryInvoice_View, DTO.FactoryInvoice>(dbItem);
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
