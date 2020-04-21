using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwfactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //Map DB 2 DTO
                AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNoteSearch_View, DTO.PaymentNoteSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PaymentNoteDate, o => o.ResolveUsing(s => s.PaymentNoteDate.HasValue ? s.PaymentNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdateDate, o => o.ResolveUsing(s => s.UpdateDate.HasValue ? s.UpdateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreateDate, o => o.ResolveUsing(s => s.CreateDate.HasValue ? s.CreateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNoteEdit_View, DTO.PaymentNoteEditResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PostingDate, o => o.ResolveUsing(s => s.PostingDate.HasValue ? s.PostingDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PaymentNoteDate, o => o.ResolveUsing(s => s.PaymentNoteDate.HasValue ? s.PaymentNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreateDate, o => o.ResolveUsing(s => s.CreateDate.HasValue ? s.CreateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdateDate, o => o.ResolveUsing(s => s.UpdateDate.HasValue ? s.UpdateDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                    .ForMember(d => d.paymentNoteInvoiceResults, o => o.MapFrom(s => s.PaymentNoteMng_PaymentNoteInvoiceEdit_View))
                    .ForMember(d => d.paymentNoteOtherResults, o => o.MapFrom(s => s.PaymentNoteMng_PaymentNoteOtherEdit_View))
                    .ForMember(d => d.paymentNoteSupplierResults, o => o.MapFrom(s => s.PaymentNoteMng_PaymentNoteSupplierEdit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNoteSupplierEdit_View, DTO.PaymentNoteSupplierResult>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.supplierDepositResults, o => o.MapFrom(s => s.PaymentNoteMng_PaymentNoteSupplierDepositeEdit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNoteInvoiceEdit_View, DTO.PaymentNoteInvoiceResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.paymentNotePODepositResults, o => o.MapFrom(s => s.PaymentNoteMng_PaymentNotePODeposit_View))
                    .ForMember(d => d.PurchaseInvoiceDate, o => o.ResolveUsing(s => s.PurchaseInvoiceDate.HasValue ? s.PurchaseInvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNoteOtherEdit_View, DTO.PaymentNoteOtherResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNotePODeposit_View, DTO.PaymentNotePODepositResult>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_SupportPurchaseInvoiceSearch_View, DTO.PaymentNoteSupportSerachPurchaseInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseInvoiceDate, o => o.ResolveUsing(s => s.PurchaseInvoiceDate.HasValue ? s.PurchaseInvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_SupportPaymentNoteItemType_View, DTO.PaymentNoteSupportItemType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_SupportPaymentType_View, DTO.PaymentNoteSupportType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_SupportPaymentNoteType_View, DTO.PaymentSupportType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_SupportFactoryRawMaterialSearch_View, DTO.PaymentSupportSearchSupplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_Support_BankAccount_View, DTO.PaymentNoteBankAccount>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNoteSupplierDepositeEdit_View, DTO.SupplierDepositResult>()
                //   .IgnoreAllNonExisting()
                //   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<PaymentNoteMng_PaymentNoteSupplierDeposit_View, DTO.SupplierDepositResult>()
                //   .IgnoreAllNonExisting()
                //   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Map DTO 2 DB

                AutoMapper.Mapper.CreateMap<DTO.PaymentNoteEditResult, PaymentNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentNoteID, o => o.Ignore())
                    .ForMember(d => d.PostingDate, o => o.Ignore())
                    .ForMember(d => d.PaymentNoteDate, o => o.Ignore())
                    .ForMember(d => d.UpdateDate, o => o.Ignore())
                    .ForMember(d => d.CreateDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PaymentNoteInvoiceResult, PaymentNoteInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentNoteInvoiceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PaymentNoteSupplierResult, PaymentNoteSupplier>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.SupplierDeposit, o => o.Ignore())
                    //.ForMember(d => d.SupplierDeposit, o => o.MapFrom(s => s.supplierDepositResults))
                    .ForMember(d => d.PaymentNoteSupplierID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PaymentNoteOtherResult, PaymentNoteOther>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentNoteOtherID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PaymentNotePODepositResult, PaymentNotePODeposit>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentNotePODepositID, o => o.Ignore());

                //AutoMapper.Mapper.CreateMap<DTO.SupplierDepositResult, SupplierDeposit>()
                //    .IgnoreAllNonExisting()
                //    //.ForMember(d => d.PaymentNoteClientID, o => o.Ignore())
                //    .ForMember(d => d.SupplierDepositID, o => o.Ignore())
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_SupportPurchaseInvoiceSearch_View, DTO.PaymentNoteSupportSearchPO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_GetPOFromInvoice_View, DTO.POFromInvoiceDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentNoteMng_PurchaseOrderAmount_SearchView, DTO.PaymentNoteSupportSearchPO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        //DMZ Zone
        public List<DTO.PaymentNoteSearchResult> DB2DTO_SearchResult(List<PaymentNoteMng_PaymentNoteSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_PaymentNoteSearch_View>, List<DTO.PaymentNoteSearchResult>>(dbItems);
        }

        public DTO.PaymentNoteEditResult DB2DTO_EditData(PaymentNoteMng_PaymentNoteEdit_View dbItem)
        {
            return AutoMapper.Mapper.Map<PaymentNoteMng_PaymentNoteEdit_View, DTO.PaymentNoteEditResult>(dbItem);
        }

        //public List<DTO.SupplierDepositResult> DB2DTO_SupplierDeposit(List<PaymentNoteMng_PaymentNoteSupplierDeposit_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<PaymentNoteMng_PaymentNoteSupplierDeposit_View>, List<DTO.SupplierDepositResult>>(dbItems);
        //}

        //Support Zone
        public List<DTO.PaymentNoteSupportItemType> DB2DTO_SupportItemType(List<PaymentNoteMng_SupportPaymentNoteItemType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_SupportPaymentNoteItemType_View>, List<DTO.PaymentNoteSupportItemType>>(dbItems);
        }
        public List<DTO.PaymentNoteSupportSerachPurchaseInvoice> DB2DTO_SupportSerachPurchaseInvoice(List<PaymentNoteMng_SupportPurchaseInvoiceSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_SupportPurchaseInvoiceSearch_View>, List<DTO.PaymentNoteSupportSerachPurchaseInvoice>>(dbItems);
        }
        public List<DTO.PaymentSupportType> DB2DTO_SupportType(List<PaymentNoteMng_SupportPaymentNoteType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_SupportPaymentNoteType_View>, List<DTO.PaymentSupportType>>(dbItems);
        }
        public List<DTO.PaymentNoteSupportType> DB2DTO_ReceiveSupportType(List<PaymentNoteMng_SupportPaymentType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_SupportPaymentType_View>, List<DTO.PaymentNoteSupportType>>(dbItems);
        }
        public List<DTO.PaymentNoteBankAccount> DB2DTO_BankAccunt(List<PaymentNoteMng_Support_BankAccount_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_Support_BankAccount_View>, List<DTO.PaymentNoteBankAccount>>(dbItems);
        }
        public List<DTO.PaymentSupportSearchSupplier> DB2DTO_SupportSerachClient(List<PaymentNoteMng_SupportFactoryRawMaterialSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_SupportFactoryRawMaterialSearch_View>, List<DTO.PaymentSupportSearchSupplier>>(dbItems);
        }

        public List<DTO.PaymentNoteSupportSearchPO> DB2DTO_SupportSearchPO(List<PaymentNoteMng_PurchaseOrderAmount_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_PurchaseOrderAmount_SearchView>, List<DTO.PaymentNoteSupportSearchPO>>(dbItems);
        }

        public List<DTO.POFromInvoiceDTO> DB2DTO_POFromInvoice(List<PaymentNoteMng_GetPOFromInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentNoteMng_GetPOFromInvoice_View>, List<DTO.POFromInvoiceDTO>>(dbItems);
        }
        //Update Zone
        public void DTO2DB_Update(DTO.PaymentNoteEditResult dtoItem, ref PaymentNote dbItem, int userId)
        {
            //From Invoce
            if (dtoItem.PaymentNoteTypeID == 1)
            {
                if (dtoItem.paymentNoteInvoiceResults != null)
                {
                    //remove 
                    foreach (var item in dbItem.PaymentNoteInvoice.ToArray())
                    {
                        if (!dtoItem.paymentNoteInvoiceResults.Select(o => o.PaymentNoteInvoiceID).Contains(item.PaymentNoteInvoiceID))
                        {
                            dbItem.PaymentNoteInvoice.Remove(item);
                        }
                    }

                    //Checking And Map
                    foreach (var dtoItemInvoice in dtoItem.paymentNoteInvoiceResults)
                    {
                        PaymentNoteInvoice dbInvoice = new PaymentNoteInvoice();
                        if (dtoItemInvoice.PaymentNoteInvoiceID < 0)
                        {
                            dbItem.PaymentNoteInvoice.Add(dbInvoice);
                        }
                        else
                        {
                            dbInvoice = dbItem.PaymentNoteInvoice.Where(o => o.PaymentNoteInvoiceID == dtoItemInvoice.PaymentNoteInvoiceID).FirstOrDefault();
                        }
                        if (dbInvoice != null)
                        {
                            foreach (var item in dtoItemInvoice.paymentNotePODepositResults)
                            {
                                PaymentNotePODeposit notePODeposit = new PaymentNotePODeposit();
                                if (item.PaymentNotePODepositID < 0)
                                {
                                    dbInvoice.PaymentNotePODeposit.Add(notePODeposit);
                                }
                                else
                                {
                                    PaymentNoteInvoice itemData = dbItem.PaymentNoteInvoice.FirstOrDefault(o => o.PaymentNoteInvoiceID == dtoItemInvoice.PaymentNoteInvoiceID);
                                    if (itemData != null) {
                                        notePODeposit = itemData.PaymentNotePODeposit.Where(o => o.PaymentNotePODepositID == item.PaymentNotePODepositID).FirstOrDefault();
                                    }
                                }
                                if (notePODeposit != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.PaymentNotePODepositResult, PaymentNotePODeposit>(item, notePODeposit);
                                }
                            }

                            AutoMapper.Mapper.Map<DTO.PaymentNoteInvoiceResult, PaymentNoteInvoice>(dtoItemInvoice, dbInvoice);
                        }
                    }

                }
            }

            //From Supplier
            if (dtoItem.PaymentNoteTypeID == 2)
            {
                if (dtoItem.paymentNoteSupplierResults != null)
                {
                    //remove 
                    foreach (var item in dbItem.PaymentNoteSupplier.ToArray())
                    {
                        if (!dtoItem.paymentNoteSupplierResults.Select(o => o.PaymentNoteSupplierID).Contains(item.PaymentNoteSupplierID))
                        {
                            dbItem.PaymentNoteSupplier.Remove(item);
                        }
                    }

                    //Checking And Map
                    foreach (var dtoItemSupplier in dtoItem.paymentNoteSupplierResults)
                    {
                        PaymentNoteSupplier dbClient = new PaymentNoteSupplier();
                        if (dtoItemSupplier.PaymentNoteSupplierID < 0)
                        {
                            dbItem.PaymentNoteSupplier.Add(dbClient);
                        }
                        else
                        {
                            dbClient = dbItem.PaymentNoteSupplier.Where(o => o.PaymentNoteSupplierID == dtoItemSupplier.PaymentNoteSupplierID).FirstOrDefault();
                        }
                        if (dbClient != null)
                        {
                            AutoMapper.Mapper.Map<DTO.PaymentNoteSupplierResult, PaymentNoteSupplier>(dtoItemSupplier, dbClient);
                        }
                    }
                }
            }

            //From other
            if (dtoItem.PaymentNoteTypeID == 3)
            {
                if (dtoItem.paymentNoteOtherResults != null)
                {
                    //remove 
                    foreach (var item in dbItem.PaymentNoteOther.ToArray())
                    {
                        if (!dtoItem.paymentNoteOtherResults.Select(o => o.PaymentNoteOtherID).Contains(item.PaymentNoteOtherID))
                        {
                            dbItem.PaymentNoteOther.Remove(item);
                        }
                    }

                    //Checking And Map
                    foreach (var dtoItemOther in dtoItem.paymentNoteOtherResults)
                    {
                        PaymentNoteOther dbOther = new PaymentNoteOther();
                        if (dtoItemOther.PaymentNoteOtherID < 0)
                        {
                            dbItem.PaymentNoteOther.Add(dbOther);
                        }
                        else
                        {
                            dbOther = dbItem.PaymentNoteOther.Where(o => o.PaymentNoteOtherID == dtoItemOther.PaymentNoteOtherID).FirstOrDefault();
                        }
                        if (dbOther != null)
                        {
                            AutoMapper.Mapper.Map<DTO.PaymentNoteOtherResult, PaymentNoteOther>(dtoItemOther, dbOther);
                        }
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.PaymentNoteEditResult, PaymentNote>(dtoItem, dbItem);

            //Manual mapping
            dbItem.PostingDate = dtoItem.PostingDate.ConvertStringToDateTime();
            dbItem.PaymentNoteDate = dtoItem.PaymentNoteDate.ConvertStringToDateTime();

        }
    }
}
