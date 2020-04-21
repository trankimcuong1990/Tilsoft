using Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DAL
{
    internal partial class DataFactory
    {
        public List<DTO.PaymentSupportSearchSupplier> QuyerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PaymentSupportSearchSupplier> data = new List<DTO.PaymentSupportSearchSupplier>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.PaymentNoteMng_SupportFactoryRawMaterialSearch_View.Where(o => o.FactoryRawMaterialUD.Contains(query) || o.FactoryRawMaterialShortNM.Contains(query)).ToList();
                    data = converter.DB2DTO_SupportSerachClient(result).ToList();
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public DTO.SearchPurchasingInvoice SearchPurchasingInvoice(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchPurchasingInvoice searchData = new DTO.SearchPurchasingInvoice();

            try
            {
                string purchaseInvoiceUD = null;
                string purchaseInvoiceDateString = null;
                int? factoryRawMaterialID = null;
                string invoiceNo = null;

                if (filters.ContainsKey("purchaseInvoiceUD") && !string.IsNullOrEmpty(filters["purchaseInvoiceUD"].ToString()))
                {
                    purchaseInvoiceUD = filters["purchaseInvoiceUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("invoiceNo") && !string.IsNullOrEmpty(filters["invoiceNo"].ToString()))
                {
                    invoiceNo = filters["invoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseInvoiceDate") && !string.IsNullOrEmpty(filters["purchaseInvoiceDate"].ToString()))
                {
                    purchaseInvoiceDateString = filters["purchaseInvoiceDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null)
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }

                //int pageSize = Convert.ToInt32(filters["pageSize"]);

                DateTime? purchaseInvoiceDate = purchaseInvoiceDateString.ConvertStringToDateTime();

                using (var context = CreateContext())
                {
                    searchData.totalRows = context.PaymentNoteMng_Function_SearchPurchaseInvoice(factoryRawMaterialID, purchaseInvoiceUD, invoiceNo, purchaseInvoiceDate, orderBy, orderDirection).Count();
                    var result = context.PaymentNoteMng_Function_SearchPurchaseInvoice(factoryRawMaterialID, purchaseInvoiceUD, invoiceNo, purchaseInvoiceDate, orderBy, orderDirection);
                    searchData.Data = converter.DB2DTO_SupportSerachPurchaseInvoice(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public int SetStatus(int userId, int paymentNoteID, int statusID, int paymentNoteTypeID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                Module.Framework.BLL fwBLL = new Framework.BLL();
                using (var context = CreateContext())
                {
                    int? dbstatusID = context.PaymentNote.Where(o => o.PaymentNoteID == paymentNoteID).Select(o => o.StatusID).FirstOrDefault();

                    if (fwBLL.CanPerformAction(userId, "PaymentNoteMng", Library.DTO.ModuleAction.CanReset))
                    {
                        if (statusID == 1 && dbstatusID == 2) //Confirm 2 Open
                        {
                            if (paymentNoteTypeID == 1)
                            {
                                ValidatePayment(paymentNoteID, 1);
                            }
                            PaymentNote dbPaymentNote = context.PaymentNote.Where(o => o.PaymentNoteID == paymentNoteID).FirstOrDefault();
                            dbPaymentNote.StatusID = 1;
                            context.SaveChanges();
                        }
                        else if (statusID <= dbstatusID)
                        {
                            string newStatusNM = null;
                            if (statusID == 1) { newStatusNM = "Open"; } else if (statusID == 2) { newStatusNM = "Confirm"; } else { newStatusNM = "Cancel"; };

                            string dbStatusNM = null;
                            if (dbstatusID == 1) { dbStatusNM = "Open"; } else if (dbstatusID == 2) { dbStatusNM = "Confirm"; } else { dbStatusNM = "Cancel"; };

                            throw new Exception("Can not set status from " + dbStatusNM + " to " + newStatusNM);
                        }
                    }
                    else
                    {
                        if (statusID <= dbstatusID)
                        {
                            string newStatusNM = null;
                            if (statusID == 1) { newStatusNM = "Open"; } else if (statusID == 2) { newStatusNM = "Confirm"; } else { newStatusNM = "Cancel"; };

                            string dbStatusNM = null;
                            if (dbstatusID == 1) { dbStatusNM = "Open"; } else if (dbstatusID == 2) { dbStatusNM = "Confirm"; } else { dbStatusNM = "Cancel"; };

                            throw new Exception("Can not set status from " + dbStatusNM + " to " + newStatusNM);
                        }
                    }


                    if (statusID == 2) //Confirm
                    {
                        if (paymentNoteTypeID == 1)
                        {
                            ValidatePayment(paymentNoteID, 2);
                        }
                        PaymentNote dbPaymentNote = context.PaymentNote.Where(o => o.PaymentNoteID == paymentNoteID).FirstOrDefault();
                        dbPaymentNote.StatusID = 2;
                        dbPaymentNote.UpdateBy = userId;
                        dbPaymentNote.UpdateDate = DateTime.Now;

                        context.SaveChanges();

                        var listPurchaseInvoice = context.PaymentNoteInvoice.Where(o=>o.PaymentNoteID == paymentNoteID).GroupBy(o => o.PurchaseInvoiceID).ToList();
                        
                        foreach (IGrouping<int?, PaymentNoteInvoice> PurchaseInvoice in listPurchaseInvoice)
                        {
                            foreach(var item in PurchaseInvoice)
                            {
                                var checkFinish = context.PaymentNoteMng_PurchaseInvoiceCheckRemain_View.Where(o => o.PurchaseInvoiceID == item.PurchaseInvoiceID).FirstOrDefault();
                                if (checkFinish.Remain <= 0)
                                {
                                    var PurchaseInvoiceData = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == item.PurchaseInvoiceID).FirstOrDefault();
                                    PurchaseInvoiceData.PurchaseInvoiceStatusID = 5;
                                    context.SaveChanges();
                                }
                            }
                        }
                        ////Update Supplier Deposit table
                        //if(dbPaymentNote.PaymentNoteSupplier.Count > 0)
                        //{
                        //    var year = dbPaymentNote.PostingDate.Value.Year.ToString();
                        //    foreach(var item in dbPaymentNote.PaymentNoteSupplier)
                        //    {
                        //        var deposit = context.SupplierDeposit.FirstOrDefault(o => o.FactoryRawMaterialID == item.FactoryRawMaterialID && o.Currency == dbPaymentNote.Currency && o.Year == year);
                        //        if(deposit != null)
                        //        {
                        //            deposit.Amount = deposit.Amount + item.Amount;
                        //        }
                        //        else
                        //        {
                        //            SupplierDeposit supplierDeposit = new SupplierDeposit();
                        //            supplierDeposit.Amount = item.Amount;
                        //            supplierDeposit.Currency = dbPaymentNote.Currency;
                        //            supplierDeposit.FactoryRawMaterialID = item.FactoryRawMaterialID;
                        //            supplierDeposit.Year = year;
                        //            context.SupplierDeposit.Add(supplierDeposit);
                        //        }
                        //    }
                        //    context.SaveChanges();
                        //}
                    }
                    if (statusID == 3) //Cancel
                    {
                        PaymentNote dbPaymentNote = context.PaymentNote.Where(o => o.PaymentNoteID == paymentNoteID).FirstOrDefault();

                        if (paymentNoteTypeID == 1)
                        {
                            var dbpaymentNoteInvoice = context.PaymentNoteInvoice.Where(o => o.PaymentNoteID == dbPaymentNote.PaymentNoteID).ToList();
                            foreach (var item in dbpaymentNoteInvoice.ToList())
                            {
                                PurchaseInvoice dbPurchaseInvoice = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == item.PurchaseInvoiceID).FirstOrDefault();
                                if (dbPurchaseInvoice != null && dbPurchaseInvoice.PurchaseInvoiceStatusID == 3)
                                {
                                    dbPurchaseInvoice.PurchaseInvoiceStatusID = 2;
                                    dbPurchaseInvoice.SetStatusDate = DateTime.Now;
                                    dbPurchaseInvoice.FinishDate = null;
                                }
                            }
                        }
                        dbPaymentNote.StatusID = 3;
                        dbPaymentNote.UpdateBy = userId;
                        dbPaymentNote.UpdateDate = DateTime.Now;

                        context.SaveChanges();
                    }
                }
                return paymentNoteID;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return paymentNoteID;
            }
        }
        public void ValidatePayment(int paymentNoteID, int status)
        {
            using (var context = CreateContext())
            {
                var result = context.PaymentNoteMng_PaymentNoteInvoiceEdit_View.Where(o => o.PaymentNoteID == paymentNoteID).ToList();

                if (status == 1)//Open
                {
                    foreach (var item in result.ToList())
                    {
                        PurchaseInvoice dbPurchaseInvoice = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == item.PurchaseInvoiceID).FirstOrDefault();
                        if (dbPurchaseInvoice != null && dbPurchaseInvoice.PurchaseInvoiceStatusID == 3)
                        {
                            dbPurchaseInvoice.PurchaseInvoiceStatusID = 2;
                            dbPurchaseInvoice.FinishDate = null;
                            context.SaveChanges();
                        }
                    }
                }
                if (status == 2)//Confirm
                {
                    foreach (var item in result.ToList())
                    {
                        decimal? stockMoney = 0;

                        stockMoney = item.Remain - item.Amount;
                        if (stockMoney < 0)
                        {
                            throw new Exception("Please check Payment for Invoice: " + item.PurchaseInvoiceUD);
                        }
                        //else
                        //{
                        //    foreach (var itemdps in item.PaymentNoteMng_PaymentNotePODeposit_View.ToList())
                        //    {
                        //        decimal? stockMoneydps = 0;
                        //        stockMoneydps = itemdps.RemainDepositAmount - itemdps.DepositAmount;

                        //        if (stockMoneydps < 0)
                        //        {
                        //            throw new Exception("Please check Payment for PO: " + itemdps.PurchaseOrderUD);
                        //        }
                        //    }
                        //}
                        if (stockMoney == 0)
                        {
                            PurchaseInvoice dbPurchaseInvoice = context.PurchaseInvoice.Where(o => o.PurchaseInvoiceID == item.PurchaseInvoiceID).FirstOrDefault();
                            dbPurchaseInvoice.PurchaseInvoiceStatusID = 3;
                            dbPurchaseInvoice.FinishDate = DateTime.Now;
                            //dbPurchaseInvoice.SetStatusBy = userId;
                            context.SaveChanges();
                        }

                        //var invoice = context.PaymentNoteMng_SupportPurchaseInvoiceSearch_View.Where(o => o.PurchaseInvoiceID == item.PurchaseInvoiceID).FirstOrDefault();
                        //if (invoice != null)
                        //{
                            
                        //}
                        //else
                        //{
                        //    throw new Exception("Can't find Invoice !!!");
                        //}
                    }
                }
            }
        }

        public string GetPaymentPrintout(int paymentNoteprintID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PaymentNoteMng_Function_Print", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PaymentNoteID", paymentNoteprintID);

                adap.TableMappings.Add("Table", "Receipt");
                adap.TableMappings.Add("Table1", "ReceiptDetail");
                adap.Fill(ds);

                ds.Tables["Receipt"].Rows[0]["AmountToText"] = ConvertNumber2String(Convert.ToDecimal(ds.Tables["Receipt"].Rows[0]["Amount"]));

                return Library.Helper.CreateReceiptPrintout(ds.Tables["Receipt"], ds.Tables["ReceiptDetail"], "PaymentNote.rdlc");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return string.Empty;
            }
        }

        public static String ConvertNumber2String(decimal aom)
        {
            try
            {
                string result = "";
                aom = Math.Round(aom, 0);
                string[] numberString = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                string[] subUnits = { "lẻ", "mốt", "", "", "", "năm" };
                string[] units = { "", "mươi", "trăm", "nghìn", "", "", "triệu", "", "", "tỷ", "", "", "nghìn", "", "", "triệu" };

                string aomStr = aom.ToString();
                int[] aomInt = new int[aomStr.Length];

                for (int i = 0; i < aomInt.Length; i++)
                {
                    aomInt[aomInt.Length - 1 - i] = Convert.ToInt32(aomStr.Substring(i, 1));
                }

                for (int i = aomInt.Length - 1; i >= 0; i--)
                {
                    if (i % 3 == 2) // number of Thousands
                    {
                        if (aomInt[i] == 0 && aomInt[i - 1] == 0 && aomInt[i - 2] == 0) continue; //ignore if three zero
                    }
                    else if (i % 3 == 1) // number of tens
                    {
                        if (aomInt[i] == 0)
                        {
                            if (aomInt[i - 1] == 0) { continue; } // ignore if tens and unit all zero.
                            else
                            {
                                result += " " + subUnits[aomInt[i]]; continue;// ignore tens is zero, read unit.
                            }
                        }
                        if (aomInt[i] == 1)//if tens then read mười
                        {
                            result += " mười"; continue;
                        }
                    }
                    else if (i != aomInt.Length - 1)// number of unit (not first)
                    {
                        if (aomInt[i] == 0)// number of unit is 0 then read unit
                        {
                            if (i + 2 <= aomInt.Length - 1 && aomInt[i + 2] == 0 && aomInt[i + 1] == 0) continue;
                            result += " " + (i % 3 == 0 ? units[i] : units[i % 3]);
                            continue;
                        }
                        if (aomInt[i] == 1)// how to read number 1 : 0,1: một / remaind : mốt
                        {
                            result += " " + ((aomInt[i + 1] == 1 || aomInt[i + 1] == 0) ? numberString[aomInt[i]] : subUnits[aomInt[i]]);
                            result += " " + (i % 3 == 0 ? units[i] : units[i % 3]);
                            continue;
                        }
                        if (aomInt[i] == 5) // how to read number 5
                        {
                            if (aomInt[i + 1] != 0) //if tens  !0 then read  5 is lăm
                            {
                                result += " " + subUnits[aomInt[i]];// read number 
                                result += " " + (i % 3 == 0 ? units[i] : units[i % 3]);// read unit
                                continue;
                            }
                        }
                    }

                    result += (result == "" ? " " : " ") + numberString[aomInt[i]];// read number
                    result += " " + (i % 3 == 0 ? units[i] : units[i % 3]);// read unit
                }
                if (result[result.Length - 1] != ' ')
                    result += " đồng chẵn";
                else
                    result += "đồng chẵn";

                if (result.Length > 2)
                {
                    string rs1 = result.Substring(0, 2);
                    rs1 = rs1.ToUpper();
                    result = result.Substring(2);
                    result = rs1 + result;
                }
                return result.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười");
            }
            catch
            {
                return "";
            }
        }

        public DTO.SearchPO SearchPO(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchPO searchData = new DTO.SearchPO();

            try
            {
                int? purchaseOrderStatusID = null;
                string purchaseOrderUD = null;
                string purchaseOrderDate = null;
                int? factoryRawMaterialID = null;

                if (filters.ContainsKey("purchaseOrderStatusID") && filters["purchaseOrderStatusID"] != null)
                {
                    purchaseOrderStatusID = Convert.ToInt32(filters["purchaseOrderStatusID"]);
                }
                if (filters.ContainsKey("purchaseOrderUD") && !string.IsNullOrEmpty(filters["purchaseOrderUD"].ToString()))
                {
                    purchaseOrderUD = filters["purchaseOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseOrderDate") && !string.IsNullOrEmpty(filters["purchaseOrderDate"].ToString()))
                {
                    purchaseOrderDate = filters["purchaseOrderDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null)
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }


                using (var context = CreateContext())
                {
                    searchData.totalRows = context.PaymentNoteMng_Function_SearchPO(purchaseOrderStatusID, purchaseOrderUD, purchaseOrderDate, factoryRawMaterialID, orderBy, orderDirection).Count();
                    var result = context.PaymentNoteMng_Function_SearchPO(purchaseOrderStatusID, purchaseOrderUD, purchaseOrderDate, factoryRawMaterialID, orderBy, orderDirection).ToList();
                    searchData.Data = converter.DB2DTO_SupportSearchPO(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public List<DTO.POFromInvoiceDTO> GetPOFromInvoice(int purchaseInvoiceID, out Library.DTO.Notification notification) {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.POFromInvoiceDTO> datas = new List<DTO.POFromInvoiceDTO>();
            try
            {
                using (var context = CreateContext()) {
                    var result = context.PaymentNoteMng_GetPOFromInvoice_View.Where(o => o.PurchaseInvoiceID == purchaseInvoiceID).ToList();
                    datas = converter.DB2DTO_POFromInvoice(result);
                    return datas;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return datas;
            }
        }
    }
}
