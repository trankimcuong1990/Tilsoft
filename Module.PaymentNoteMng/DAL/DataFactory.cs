using Library;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchForm, DTO.EditForm>
    {
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private PaymentNoteMngEntities CreateContext()
        {
            return new PaymentNoteMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PaymentNoteMngModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var result = false;
            try
            {
                using (PaymentNoteMngEntities context = CreateContext())
                {
                    var deleteItem = context.PaymentNote.Where(o => o.PaymentNoteID == id).FirstOrDefault();
                    if (deleteItem == null)
                    {
                        throw new Exception("Cound not found item");
                    }
                    context.PaymentNote.Remove(deleteItem);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return result;
        }

        public override DTO.EditForm GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            DTO.EditForm editFormData = new DTO.EditForm();
            editFormData.Data = new DTO.PaymentNoteEditResult();
            editFormData.supportData = new DTO.PaymentNoteSupport();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.PaymentNoteMng_PaymentNoteEdit_View.FirstOrDefault(o => o.PaymentNoteID == id);
                        editFormData.Data = converter.DB2DTO_EditData(dbItem);
                    }
                    else
                    {
                        editFormData.Data.PostingDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.StatusID = 1;
                        editFormData.Data.PaymentNoteTypeID = 1;
                        //Exchange Rate
                        editFormData.Data.ExchangeRate = context.PaymentNoteMng_MasterExchangeRate_View.Where(o => o.Currency == "USD").Select(s => s.ExchangeRate).FirstOrDefault();
                    }

                    //Support Data
                    editFormData.supportData.paymentNoteSupportItemTypes = converter.DB2DTO_SupportItemType(context.PaymentNoteMng_SupportPaymentNoteItemType_View.ToList());
                    editFormData.supportData.paymentSupportTypes = converter.DB2DTO_SupportType(context.PaymentNoteMng_SupportPaymentNoteType_View.ToList());
                    editFormData.supportData.paymentNoteSupportTypes = converter.DB2DTO_ReceiveSupportType(context.PaymentNoteMng_SupportPaymentType_View.ToList());
                    editFormData.supportData.paymentNoteBankAccounts = converter.DB2DTO_BankAccunt(context.PaymentNoteMng_Support_BankAccount_View.ToList());

                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return editFormData;
            }
        }

        public override DTO.SearchForm GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.SearchForm searchData = new DTO.SearchForm();
            totalRows = 0;

            try
            {
                string paymentNoteNo = null;
                string paymentNoteDateString = null;
                string postingDateString = null;
                string factoryRawMaterialShortNM = null;
                string receiverName = null;
                int? statusID = null;
                int? paymentNoteTypeID = null;

                if (filters.ContainsKey("paymentNoteNo") && !string.IsNullOrEmpty(filters["paymentNoteNo"].ToString()))
                {
                    paymentNoteNo = filters["paymentNoteNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("paymentNoteDate") && !string.IsNullOrEmpty(filters["paymentNoteDate"].ToString()))
                {
                    paymentNoteDateString = filters["paymentNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("postingDate") && !string.IsNullOrEmpty(filters["postingDate"].ToString()))
                {
                    postingDateString = filters["postingDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialShortNM") && !string.IsNullOrEmpty(filters["factoryRawMaterialShortNM"].ToString()))
                {
                    factoryRawMaterialShortNM = filters["factoryRawMaterialShortNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receiverName") && !string.IsNullOrEmpty(filters["receiverName"].ToString()))
                {
                    receiverName = filters["receiverName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("statusID") && filters["statusID"] != null)
                {
                    statusID = Convert.ToInt32(filters["statusID"]);
                }
                if (filters.ContainsKey("paymentNoteTypeID") && filters["paymentNoteTypeID"] != null)
                {
                    paymentNoteTypeID = Convert.ToInt32(filters["paymentNoteTypeID"]);
                }

                DateTime? paymentNoteDate = paymentNoteDateString.ConvertStringToDateTime();
                DateTime? postingDate = postingDateString.ConvertStringToDateTime();

                //Add Support Status
                searchData.paymentSupportStatuses.Add(new DTO.PaymentSupportStatus { StatusID = 1, StatusNM = "Open" });
                searchData.paymentSupportStatuses.Add(new DTO.PaymentSupportStatus { StatusID = 2, StatusNM = "Confirm" });
                searchData.paymentSupportStatuses.Add(new DTO.PaymentSupportStatus { StatusID = 3, StatusNM = "Canceled" });

                using (var context = CreateContext())
                {
                    totalRows = context.PaymentNoteMng_Function_SearchResult(paymentNoteNo, paymentNoteDate, postingDate, factoryRawMaterialShortNM, receiverName, statusID, paymentNoteTypeID, orderBy, orderDirection).Count();
                    var result = context.PaymentNoteMng_Function_SearchResult(paymentNoteNo, paymentNoteDate, postingDate, factoryRawMaterialShortNM, receiverName, statusID, paymentNoteTypeID, orderBy, orderDirection);
                    searchData.Data = converter.DB2DTO_SearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    searchData.paymentSupportTypes = converter.DB2DTO_SupportType(context.PaymentNoteMng_SupportPaymentNoteType_View.ToList());
                }
                return searchData;
            }
            catch (Exception ex)
            {

                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return searchData;
            }
        }

        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PaymentNoteEditResult dtoPayment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PaymentNoteEditResult>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    if (dtoPayment.PaymentNoteTypeID == 3)
                    {
                        foreach (var item in dtoPayment.paymentNoteOtherResults)
                        {
                            if (item.FactoryRawMaterialID == null)
                            {
                                throw new Exception("missing Suppiler !!!");
                            }
                        }
                    }
                    if (dtoPayment.PaymentNoteTypeID == 1) {
                        foreach (var item in dtoPayment.paymentNoteInvoiceResults)
                        {
                            decimal? totalDpsAmount = 0;  

                            if (dtoPayment.Currency == "USD" && item.Currency == "VND") {
                                totalDpsAmount = (item.Amount == null ? 0 : item.Amount * dtoPayment.ExchangeRate);
                            }
                            else if (dtoPayment.Currency == "VND" && item.Currency == "USD")
                            {
                                totalDpsAmount = (item.Amount == null ? 0 : item.Amount / dtoPayment.ExchangeRate);
                            }
                            else
                            {
                                totalDpsAmount = (item.Amount == null ? 0 : item.Amount);
                            }
                            if (item.Remain < totalDpsAmount)
                            {
                                throw new Exception("Amount more than over remain " + item.PurchaseInvoiceUD);
                            }
                        }
                    }
                    ///
                    if (dtoPayment.PaymentNoteTypeID == 2) {
                        foreach (var item in dtoPayment.paymentNoteSupplierResults)
                        {
                            var itemx = context.PaymentNoteMng_CheckingCurrencySupplier_View.FirstOrDefault(o=>o.PurchaseOrderID == item.PurchaseOrderID);
                            if (itemx != null && itemx.Currency != dtoPayment.Currency)
                            {
                                throw new Exception("There are differences between the two currencies " + item.PurchaseOrderUD);
                            }
                        }
                    }

                    #region Convert
                    //if (dtoPayment.paymentNoteInvoiceResults != null && dtoPayment.PaymentNoteTypeID == 1)
                    //{
                    //    decimal? totalByCurrency = 0;
                    //    foreach (var dtoItemInvoice in dtoPayment.paymentNoteInvoiceResults)
                    //    {
                    //        string currency = context.PaymentNoteMng_PurchaseInvoiceCurrency_View.Where(o => o.PurchaseInvoiceID == dtoItemInvoice.PurchaseInvoiceID).Select(s => s.Currency).FirstOrDefault();
                    //        //If Purchase Invoice null Currency Throw 
                    //        if (string.IsNullOrEmpty(currency))
                    //        {
                    //            string purchaseUD = context.PaymentNoteMng_PurchaseInvoiceCurrency_View.Where(o => o.PurchaseInvoiceID == dtoItemInvoice.PurchaseInvoiceID).Select(s => s.PurchaseInvoiceUD).FirstOrDefault();
                    //            throw new Exception("Missing Currency for Invoice " + purchaseUD);
                    //        }
                    //        //Convert USD => VND or
                    //        if (dtoPayment.Currency == "VND")
                    //        {
                    //            if (currency == "VND")
                    //            {
                    //                dtoItemInvoice.Amount = dtoItemInvoice.AmountByCurrency + dtoItemInvoice.Deposit;
                    //            }
                    //            else
                    //            {
                    //                dtoItemInvoice.Amount = ((dtoItemInvoice.AmountByCurrency == null ? 0 : dtoItemInvoice.AmountByCurrency) + (dtoItemInvoice.Deposit == null ? 0 : dtoItemInvoice.Deposit)) / dtoPayment.ExchangeRate;
                    //            }
                    //            totalByCurrency += (dtoItemInvoice.Deposit == null ? 0 : dtoItemInvoice.Deposit);
                    //        }
                    //        else if (dtoPayment.Currency == "USD")
                    //        {
                    //            if (currency == "VND")
                    //            {
                    //                dtoItemInvoice.Amount = (dtoItemInvoice.AmountByCurrency == null ? 0 : dtoItemInvoice.AmountByCurrency) * dtoPayment.ExchangeRate;
                    //            }
                    //            else
                    //            {
                    //                dtoItemInvoice.Amount = dtoItemInvoice.AmountByCurrency + dtoItemInvoice.Deposit;
                    //            }
                    //            totalByCurrency += ((dtoItemInvoice.Deposit == null ? 0 : dtoItemInvoice.Deposit) + (dtoItemInvoice.Deposit == null ? 0 : dtoItemInvoice.Deposit));
                    //        }
                    //    }
                    //    dtoPayment.TotalByCurrency = totalByCurrency;
                    //}
                    #endregion

                    PaymentNote dbItem = null;

                    if (id == 0)
                    {
                        if (dtoPayment.StatusID == 2 || dtoPayment.StatusID == 3)
                        {
                            throw new Exception("Set Status open to Save !!!");
                        }

                        dbItem = new PaymentNote();
                        context.PaymentNote.Add(dbItem);

                        //Automatically generate code
                        if (dtoPayment.PaymentTypeID == 1)// Payment type Cash
                        {
                            int year = DateTime.Now.Year;
                            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
                            string receipt_pattern = "PC" + "_" + year.ToString().Substring(2) + month;
                            var db_receiptNo = context.PaymentNote.Where(o => o.PaymentNoteNo.Substring(0, 7) == receipt_pattern).OrderByDescending(o => o.PaymentNoteNo);
                            if (db_receiptNo.ToList().Count() == 0)
                            {
                                dtoPayment.PaymentNoteNo = receipt_pattern + "_" + "001";
                            }
                            else
                            {
                                var select_receipt = db_receiptNo.FirstOrDefault();
                                int iNo = Convert.ToInt32(select_receipt.PaymentNoteNo.Substring(8, 3)) + 1;
                                dtoPayment.PaymentNoteNo = select_receipt.PaymentNoteNo.Substring(0, 7) + "_" + iNo.ToString().PadLeft(3, '0');
                            }
                        }
                        else if (dtoPayment.PaymentTypeID == 2)// Payment type Bank
                        {
                            int year = DateTime.Now.Year;
                            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
                            string receipt_pattern = "BN" + "_" + year.ToString().Substring(2) + month;
                            var db_receiptNo = context.PaymentNote.Where(o => o.PaymentNoteNo.Substring(0, 7) == receipt_pattern).OrderByDescending(o => o.PaymentNoteNo);
                            if (db_receiptNo.ToList().Count() == 0)
                            {
                                dtoPayment.PaymentNoteNo = receipt_pattern + "_" + "001";
                            }
                            else
                            {
                                var select_receipt = db_receiptNo.FirstOrDefault();
                                int iNo = Convert.ToInt32(select_receipt.PaymentNoteNo.Substring(8, 3)) + 1;
                                dtoPayment.PaymentNoteNo = select_receipt.PaymentNoteNo.Substring(0, 7) + "_" + iNo.ToString().PadLeft(3, '0');
                            }
                        }
                    }
                    else
                    {
                        dbItem = context.PaymentNote.Where(o => o.PaymentNoteID == id).FirstOrDefault();

                        if (dbItem.PaymentNoteTypeID != dtoPayment.PaymentNoteTypeID)
                        {
                            throw new Exception("Can't change Payment Note type !!!");
                        }
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //check  status
                        /*
                         *  WorkOrderStatusID :
                         *      1 : Open
                         *      2 : Confimred                   
                         */

                        if (dbItem.StatusID == 2 || dbItem.StatusID == 3)
                        {
                            throw new Exception("Can't Update because Payment Comfirmed or Cancel !!!");
                        }
                        if (dtoPayment.StatusID == 2 && dtoPayment.PaymentNoteTypeID == 1)
                        {
                            ValidatePayment(dbItem.PaymentNoteID, 2);
                        }

                        //convert dto to db
                        converter.DTO2DB_Update(dtoPayment, ref dbItem, userId);

                        //upload file
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoPayment.File_HasChange.HasValue && dtoPayment.File_HasChange.Value)
                        {
                            dbItem.AttachedFile = fwFactory.CreateFilePointer(tempFolder, dtoPayment.File_NewFile, dtoPayment.AttachedFile, dtoPayment.FriendlyName);
                        }

                        if (id == 0)
                        {
                            dbItem.CreateDate = DateTime.Now;
                            dbItem.CreateBy = userId;
                        }
                        else
                        {
                            dbItem.UpdateDate = DateTime.Now;
                            dbItem.UpdateBy = userId;
                        }

                        //remove orphan
                        context.PaymentNoteSupplier.Local.Where(o => o.PaymentNote == null).ToList().ForEach(o => context.PaymentNoteSupplier.Remove(o));
                        context.PaymentNoteInvoice.Local.Where(o => o.PaymentNote == null).ToList().ForEach(o => context.PaymentNoteInvoice.Remove(o));
                        context.PaymentNoteOther.Local.Where(o => o.PaymentNote == null).ToList().ForEach(o => context.PaymentNoteOther.Remove(o));
                        context.PaymentNotePODeposit.Local.Where(o => o.PaymentNoteInvoice == null).ToList().ForEach(o => context.PaymentNotePODeposit.Remove(o));
                        
                        //save data
                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.PaymentNoteID, null, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public decimal GetPaymentByDeposit(int supplierID, string year, string currency, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            using (var context = CreateContext())
            {
                var PaymentByDeposit = context.PaymentNoteMng_Function_GetPaymentByDeposit(supplierID, year, currency).Select(o => o.GetValueOrDefault(0)).ToList();
                if (PaymentByDeposit.Count() > 0)
                    return PaymentByDeposit[0];
                else
                    return 0;
            }
        }

        public decimal GetTotalDeposit(int supplierID, string year, string currency, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            decimal DepositLastYearValue = 0;
            decimal DepositThisYearValue = 0;
            decimal SupplierDeposits = 0;

            using (var context = CreateContext())
            {
                var lastYear = (int.Parse(year) - 1).ToString();
                var DepositLastYear = context.PaymentNoteMng_Function_GetTotalDeposit(supplierID, lastYear, currency).Select(o=>o.GetValueOrDefault(0)).ToList();
                var DepositThisYear = context.PaymentNoteMng_Function_GetDepositForSupplier(supplierID, year, currency).Select(o => o.GetValueOrDefault(0)).ToList();

                if(DepositLastYear.Count() > 0)
                {
                    DepositLastYearValue = DepositLastYear[0];
                }
                if (DepositThisYear.Count() > 0)
                {
                    DepositThisYearValue = DepositThisYear[0];
                }
                SupplierDeposits = DepositLastYearValue + DepositThisYearValue;
            }
            return SupplierDeposits;
        }
    }
}
