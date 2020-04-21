using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.ReceiptNoteMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //Map DB 2 DTO
                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_ReceiptNoteSearch_View, DTO.ReceiptNoteSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ReceiptNoteDate, o => o.ResolveUsing(s => s.ReceiptNoteDate.HasValue ? s.ReceiptNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdateDate, o => o.ResolveUsing(s => s.UpdateDate.HasValue ? s.UpdateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreateDate, o => o.ResolveUsing(s => s.CreateDate.HasValue ? s.CreateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_ReceiptNoteEdit_View, DTO.ReceiptNoteEditResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ReceiptNoteDate, o => o.ResolveUsing(s => s.ReceiptNoteDate.HasValue ? s.ReceiptNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreateDate, o => o.ResolveUsing(s => s.CreateDate.HasValue ? s.CreateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdateDate, o => o.ResolveUsing(s => s.UpdateDate.HasValue ? s.UpdateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                    .ForMember(d => d.receiptNoteClientResults, o => o.MapFrom(s => s.ReceiptNoteMng_ReceiptNoteClientEdit_View))
                    .ForMember(d => d.receiptNoteInvoiceResults, o => o.MapFrom(s => s.ReceiptNoteMng_ReceiptNoteInvoiceEdit_View))
                    .ForMember(d => d.receiptNoteOtherResults, o => o.MapFrom(s => s.ReceiptNoteMng_ReceiptNoteOtherEdit_View))
                    .ForMember(d => d.receiptNoteSaleInvoiceResults, o => o.MapFrom(s => s.ReceiptNoteMng_ReceiptNoteSaleInvoiceEdit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_ReceiptNoteClientEdit_View, DTO.ReceiptNoteClientResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_ReceiptNoteInvoiceEdit_View, DTO.ReceiptNoteInvoiceResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_ReceiptNoteOtherEdit_View, DTO.ReceiptNoteOtherResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_ReceiptNoteSaleInvoiceEdit_View, DTO.ReceiptNoteSaleInvoiceResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportPurchasingInvoiceSearch_View, DTO.ReceiptNoteSupportSerachPurchasingInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportReceiptNoteItemType_View, DTO.ReceiptNoteSupportItemType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportReceiptNoteType_View, DTO.ReceiptNoteSupportType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportReceiveType_View, DTO.ReceiveSupportType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportClientSearch_View, DTO.ReceiptSupportSearchClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_MasterExchangeRate_View, DTO.MasterExchangeRateReceipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidDate, o => o.ResolveUsing(s => s.ValidDate.HasValue ? s.ValidDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportFactorySaleInvoiceSearch_View, DTO.ReceiptNoteSupportSearchFactorySaleInvoice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportFactoryRawMaterialSearch_View, DTO.ReceiptSupportSearchSupplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Map DTO 2 DB

                AutoMapper.Mapper.CreateMap<DTO.ReceiptNoteEditResult, ReceiptNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptNoteID, o => o.Ignore())
                    .ForMember(d => d.PostingDate, o => o.Ignore())
                    .ForMember(d => d.ReceiptNoteDate, o => o.Ignore())
                    .ForMember(d => d.UpdateDate, o => o.Ignore())
                    .ForMember(d => d.CreateDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ReceiptNoteInvoiceResult, ReceiptNoteInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptNoteInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ReceiptNoteClientResult, ReceiptNoteClient>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptNoteClientID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ReceiptNoteOtherResult, ReceiptNoteOther>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptNoteOtherID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ReceiptNoteSaleInvoiceResult, ReceiptNoteSaleInvoice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ReceiptNoteSaleInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_Support_SupplierBank_View, DTO.ReceiptNoteBankAccount>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMn_function_GetSupplier_Result, DTO.SupplierDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportEmployeeSearch_View, DTO.EmployeeDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceiptNoteMng_SupportProductionItemSearch_View, DTO.ProductionItemDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public DTO.SupplierDTO DB2DTO_Supplier(ReceiptNoteMn_function_GetSupplier_Result dbItems)
        {
            return AutoMapper.Mapper.Map<ReceiptNoteMn_function_GetSupplier_Result, DTO.SupplierDTO>(dbItems);
        }

        //DMZ Zone
        public List<DTO.ReceiptNoteSearchResult> DB2DTO_SearchResult(List<ReceiptNoteMng_ReceiptNoteSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_ReceiptNoteSearch_View>, List<DTO.ReceiptNoteSearchResult>>(dbItems);
        }

        public DTO.ReceiptNoteEditResult DB2DTO_EditData(ReceiptNoteMng_ReceiptNoteEdit_View dbItem)
        {
            return AutoMapper.Mapper.Map<ReceiptNoteMng_ReceiptNoteEdit_View, DTO.ReceiptNoteEditResult>(dbItem);
        }

        //Support Zone
        public List<DTO.ReceiptNoteBankAccount> DB2DTO_BankAccunt(List<ReceiptNoteMng_Support_SupplierBank_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_Support_SupplierBank_View>, List<DTO.ReceiptNoteBankAccount>>(dbItems);
        }
        public List<DTO.ReceiptNoteSupportItemType> DB2DTO_SupportItemType(List<ReceiptNoteMng_SupportReceiptNoteItemType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportReceiptNoteItemType_View>, List<DTO.ReceiptNoteSupportItemType>>(dbItems);
        }
        public List<DTO.ReceiptNoteSupportSerachPurchasingInvoice> DB2DTO_SupportSerachPurchasingInvoice(List<ReceiptNoteMng_SupportPurchasingInvoiceSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportPurchasingInvoiceSearch_View>, List<DTO.ReceiptNoteSupportSerachPurchasingInvoice>>(dbItems);
        }
        public List<DTO.ReceiptNoteSupportSearchFactorySaleInvoice> DB2DTO_SupportSearchFactorySaleInvoice(List<ReceiptNoteMng_SupportFactorySaleInvoiceSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportFactorySaleInvoiceSearch_View>, List<DTO.ReceiptNoteSupportSearchFactorySaleInvoice>>(dbItems);
        }
        public List<DTO.ReceiptNoteSupportType> DB2DTO_SupportType(List<ReceiptNoteMng_SupportReceiptNoteType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportReceiptNoteType_View>, List<DTO.ReceiptNoteSupportType>>(dbItems);
        }
        public List<DTO.ReceiveSupportType> DB2DTO_ReceiveSupportType(List<ReceiptNoteMng_SupportReceiveType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportReceiveType_View>, List<DTO.ReceiveSupportType>>(dbItems);
        }
        public List<DTO.ReceiptSupportSearchClient> DB2DTO_SupportSerachClient(List<ReceiptNoteMng_SupportClientSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportClientSearch_View>, List<DTO.ReceiptSupportSearchClient>>(dbItems);
        }
        public List<DTO.ReceiptSupportSearchSupplier> DB2DTO_SupportSearchSupplier(List<ReceiptNoteMng_SupportFactoryRawMaterialSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportFactoryRawMaterialSearch_View>, List<DTO.ReceiptSupportSearchSupplier>>(dbItems);
        }

        public List<DTO.ProductionItemDTO> DB2DTO_SupportSearchProductionItem(List<ReceiptNoteMng_SupportProductionItemSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportProductionItemSearch_View>, List<DTO.ProductionItemDTO>>(dbItems);
        }

        public List<DTO.EmployeeDTO> DB2DTO_SupportSearchEmployee(List<ReceiptNoteMng_SupportEmployeeSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceiptNoteMng_SupportEmployeeSearch_View>, List<DTO.EmployeeDTO>>(dbItems);
        }
        //Update Zone
        public void DTO2DB_Update(DTO.ReceiptNoteEditResult dtoItem, ref ReceiptNote dbItem, int userId)
        {
            decimal? totalByCurrency = 0;
            //From Invoce
            if (dtoItem.ReceiptNoteTypeID == 1)
            {
                if (dtoItem.receiptNoteInvoiceResults != null)
                {
                    //remove 
                    foreach (var item in dbItem.ReceiptNoteInvoice.ToArray())
                    {
                        if (!dtoItem.receiptNoteInvoiceResults.Select(o => o.ReceiptNoteInvoiceID).Contains(item.ReceiptNoteInvoiceID))
                        {
                            dbItem.ReceiptNoteInvoice.Remove(item);
                        }
                    }

                    //Checking And Map
                    foreach (var dtoItemInvoice in dtoItem.receiptNoteInvoiceResults)
                    {
                        ReceiptNoteInvoice dbInvoice = new ReceiptNoteInvoice();
                        if (dtoItemInvoice.ReceiptNoteInvoiceID < 0)
                        {
                            dbItem.ReceiptNoteInvoice.Add(dbInvoice);
                        }
                        else
                        {
                            dbInvoice = dbItem.ReceiptNoteInvoice.Where(o => o.ReceiptNoteInvoiceID == dtoItemInvoice.ReceiptNoteInvoiceID).FirstOrDefault();
                        }
                        if (dbInvoice != null)
                        {
                            if (dtoItem.Currency == "VND")
                            {
                                totalByCurrency += (dtoItemInvoice.AmountByCurrency == null ? 0 : dtoItemInvoice.AmountByCurrency);

                                dtoItemInvoice.Amount = (dtoItemInvoice.AmountByCurrency == null ? 0 : dtoItemInvoice.AmountByCurrency) / dtoItem.ExchangeRate;
                            }
                            else
                            {
                                dtoItemInvoice.Amount = dtoItemInvoice.AmountByCurrency;
                            }
                            AutoMapper.Mapper.Map<DTO.ReceiptNoteInvoiceResult, ReceiptNoteInvoice>(dtoItemInvoice, dbInvoice);
                        }
                    }

                }
            }

            //From Client
            if (dtoItem.ReceiptNoteTypeID == 2)
            {
                if (dtoItem.receiptNoteClientResults != null)
                {
                    //remove 
                    foreach (var item in dbItem.ReceiptNoteClient.ToArray())
                    {
                        if (!dtoItem.receiptNoteClientResults.Select(o => o.ReceiptNoteClientID).Contains(item.ReceiptNoteClientID))
                        {
                            dbItem.ReceiptNoteClient.Remove(item);
                        }
                    }

                    //Checking And Map
                    foreach (var dtoItemClient in dtoItem.receiptNoteClientResults)
                    {
                        ReceiptNoteClient dbClient = new ReceiptNoteClient();
                        if (dtoItemClient.ReceiptNoteClientID < 0)
                        {
                            dbItem.ReceiptNoteClient.Add(dbClient);
                        }
                        else
                        {
                            dbClient = dbItem.ReceiptNoteClient.Where(o => o.ReceiptNoteClientID == dtoItemClient.ReceiptNoteClientID).FirstOrDefault();
                        }
                        if (dbClient != null)
                        {
                            if (dtoItem.Currency == "VND")
                            {
                                totalByCurrency += (dtoItemClient.Amount == null ? 0 : dtoItemClient.Amount);
                            }
                            AutoMapper.Mapper.Map<DTO.ReceiptNoteClientResult, ReceiptNoteClient>(dtoItemClient, dbClient);
                        }
                    }
                }
            }

            //From other
            if (dtoItem.ReceiptNoteTypeID == 3)
            {
                if (dtoItem.receiptNoteOtherResults != null)
                {
                    //remove 
                    foreach (var item in dbItem.ReceiptNoteOther.ToArray())
                    {
                        if (!dtoItem.receiptNoteOtherResults.Select(o => o.ReceiptNoteOtherID).Contains(item.ReceiptNoteOtherID))
                        {
                            dbItem.ReceiptNoteOther.Remove(item);
                        }
                    }

                    //Checking And Map
                    foreach (var dtoItemOther in dtoItem.receiptNoteOtherResults)
                    {
                        ReceiptNoteOther dbOther = new ReceiptNoteOther();
                        if (dtoItemOther.ReceiptNoteOtherID < 0)
                        {
                            dbItem.ReceiptNoteOther.Add(dbOther);
                        }
                        else
                        {
                            dbOther = dbItem.ReceiptNoteOther.Where(o => o.ReceiptNoteOtherID == dtoItemOther.ReceiptNoteOtherID).FirstOrDefault();
                        }
                        if (dbOther != null)
                        {
                            if (dtoItem.Currency == "VND")
                            {
                                totalByCurrency += (dtoItemOther.Amount == null ? 0 : dtoItemOther.Amount);
                            }
                            AutoMapper.Mapper.Map<DTO.ReceiptNoteOtherResult, ReceiptNoteOther>(dtoItemOther, dbOther);
                        }
                    }
                }
            }

            //From sale invoice
            if (dtoItem.ReceiptNoteTypeID == 4)
            {
                if (dtoItem.receiptNoteSaleInvoiceResults != null)
                {
                    //remove 
                    foreach (var item in dbItem.ReceiptNoteSaleInvoice.ToArray())
                    {
                        if (!dtoItem.receiptNoteSaleInvoiceResults.Select(o => o.ReceiptNoteSaleInvoiceID).Contains(item.ReceiptNoteSaleInvoiceID))
                        {
                            dbItem.ReceiptNoteSaleInvoice.Remove(item);
                        }
                    }

                    //Checking And Map
                    foreach (var dtoItemSaleInvoice in dtoItem.receiptNoteSaleInvoiceResults)
                    {
                        ReceiptNoteSaleInvoice dbSaleInvoice = new ReceiptNoteSaleInvoice();
                        if (dtoItemSaleInvoice.ReceiptNoteSaleInvoiceID < 0)
                        {
                            dbItem.ReceiptNoteSaleInvoice.Add(dbSaleInvoice);
                        }
                        else
                        {
                            dbSaleInvoice = dbItem.ReceiptNoteSaleInvoice.Where(o => o.ReceiptNoteSaleInvoiceID == dtoItemSaleInvoice.ReceiptNoteSaleInvoiceID).FirstOrDefault();
                        }
                        if (dbSaleInvoice != null)
                        {
                            if (dtoItem.Currency == "VND")
                            {
                                totalByCurrency += (dtoItemSaleInvoice.AmountByCurrency == null ? 0 : dtoItemSaleInvoice.AmountByCurrency);

                                dtoItemSaleInvoice.Amount = (dtoItemSaleInvoice.AmountByCurrency == null ? 0 : dtoItemSaleInvoice.AmountByCurrency) / dtoItem.ExchangeRate;
                            }
                            else
                            {
                                dtoItemSaleInvoice.Amount = dtoItemSaleInvoice.AmountByCurrency;
                            }
                            AutoMapper.Mapper.Map<DTO.ReceiptNoteSaleInvoiceResult, ReceiptNoteSaleInvoice>(dtoItemSaleInvoice, dbSaleInvoice);
                        }
                    }

                }
            }


            dtoItem.TotalByCurrency = totalByCurrency;

            AutoMapper.Mapper.Map<DTO.ReceiptNoteEditResult, ReceiptNote>(dtoItem, dbItem);

            //Manual mapping
            dbItem.PostingDate = dtoItem.PostingDate.ConvertStringToDateTime();
            dbItem.ReceiptNoteDate = dtoItem.ReceiptNoteDate.ConvertStringToDateTime();

        }
    }
}
