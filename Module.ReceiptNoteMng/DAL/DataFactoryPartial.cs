using Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Module.ReceiptNoteMng.DAL
{
    internal partial class DataFactory
    {
        public DTO.SupplierDTO GetSupplier(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SupplierDTO data = new DTO.SupplierDTO();

            try
            {              
                using (var context = CreateContext())
                {
                    var result = context.ReceiptNoteMn_function_GetSupplier(userID).FirstOrDefault();
                    data = converter.DB2DTO_Supplier(result);
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

        public DTO.SearchPurchasing SearchPurchasingInvoice(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchPurchasing searchData = new DTO.SearchPurchasing();

            try
            {
                string invoiceNo = null;
                string invoiceDateString = null;
                int? clientID = null;

                if (filters.ContainsKey("invoiceNo") && !string.IsNullOrEmpty(filters["invoiceNo"].ToString()))
                {
                    invoiceNo = filters["invoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("invoiceDate") && !string.IsNullOrEmpty(filters["invoiceDate"].ToString()))
                {
                    invoiceDateString = filters["invoiceDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("clientID") && filters["clientID"] != null)
                {
                    clientID = Convert.ToInt32(filters["clientID"]);
                }

                //int pageSize = Convert.ToInt32(filters["pageSize"]);

                DateTime? invoiceDate = invoiceDateString.ConvertStringToDateTime();

                using (var context = CreateContext())
                {
                    searchData.totalRows = context.ReceiptNoteMng_Function_SearchPurchasingInvoice(clientID, invoiceNo, invoiceDate, orderBy, orderDirection).Count();
                    var result = context.ReceiptNoteMng_Function_SearchPurchasingInvoice(clientID, invoiceNo, invoiceDate, orderBy, orderDirection);
                    searchData.Data = converter.DB2DTO_SupportSerachPurchasingInvoice(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public DTO.SearchFactorySaleInvoice SearchFactorySaleInvoice(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFactorySaleInvoice searchData = new DTO.SearchFactorySaleInvoice();

            try
            {
                string invoiceNo = null;
                string docNo = null;
                string invoiceDateString = null;
                int? factoryRawMaterialID = null;

                if (filters.ContainsKey("invoiceNo") && !string.IsNullOrEmpty(filters["invoiceNo"].ToString()))
                {
                    invoiceNo = filters["invoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("docNo") && !string.IsNullOrEmpty(filters["docNo"].ToString()))
                {
                    docNo = filters["docNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("invoiceDate") && !string.IsNullOrEmpty(filters["invoiceDate"].ToString()))
                {
                    invoiceDateString = filters["invoiceDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null)
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }

                //int pageSize = Convert.ToInt32(filters["pageSize"]);

                DateTime? invoiceDate = invoiceDateString.ConvertStringToDateTime();

                using (var context = CreateContext())
                {
                    searchData.totalRows = context.ReceiptNoteMng_Function_SearchFactorySaleInvoice(factoryRawMaterialID, invoiceNo, invoiceDate, docNo, orderBy, orderDirection).Count();
                    var result = context.ReceiptNoteMng_Function_SearchFactorySaleInvoice(factoryRawMaterialID, invoiceNo, invoiceDate, docNo, orderBy, orderDirection);
                    searchData.Data = converter.DB2DTO_SupportSearchFactorySaleInvoice(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public int SetStatus(int userId, int receiptNoteID, int statusID, int receiptNoteTypeID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                Module.Framework.BLL fwBLL = new Framework.BLL();
                using (var context = CreateContext())
                {
                    int? dbstatusID = context.ReceiptNote.Where(o => o.ReceiptNoteID == receiptNoteID).Select(o => o.StatusID).FirstOrDefault();

                    if (fwBLL.CanPerformAction(userId, "ReceiptNoteMng", Library.DTO.ModuleAction.CanReset))
                    {
                        if (statusID == 1 && dbstatusID == 2) //Confirm 2 Open
                        {
                            if (receiptNoteTypeID == 1)
                            {
                                ValidatePayment(receiptNoteID, 1);
                            }
                            ReceiptNote dbReceiptNote = context.ReceiptNote.Where(o => o.ReceiptNoteID == receiptNoteID).FirstOrDefault();
                            dbReceiptNote.StatusID = 1;
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
                        if (receiptNoteTypeID == 1)
                        {
                            ValidatePayment(receiptNoteID, 2);
                        }
                        ReceiptNote dbReceiptNote = context.ReceiptNote.Where(o => o.ReceiptNoteID == receiptNoteID).FirstOrDefault();
                        dbReceiptNote.StatusID = 2;
                        dbReceiptNote.UpdateBy = userId;
                        dbReceiptNote.UpdateDate = DateTime.Now;

                        context.SaveChanges();
                    }
                    if (statusID == 3) //Cancel
                    {
                        ReceiptNote dbReceiptNote = context.ReceiptNote.Where(o => o.ReceiptNoteID == receiptNoteID).FirstOrDefault();

                        //if (receiptNoteTypeID == 1)
                        //{
                        //    var dbReceiptNotPurchasing = context.ReceiptNoteInvoice.Where(o => o.ReceiptNoteID == receiptNoteID).ToList();
                        //    foreach (var item in dbReceiptNotPurchasing.ToList())
                        //    {
                        //        PurchasingInvoice dbPurchasingInvoice = context.PurchasingInvoice.Where(o => o.PurchasingInvoiceID == item.PurchasingInvoiceID).FirstOrDefault();
                        //        dbPurchasingInvoice.StatusID = 2;
                        //        dbPurchasingInvoice.FinishDate = null;
                        //    }
                        //}
                        dbReceiptNote.StatusID = 3;
                        dbReceiptNote.UpdateBy = userId;
                        dbReceiptNote.UpdateDate = DateTime.Now;
                        context.SaveChanges();
                    }
                }
                return receiptNoteID;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return receiptNoteID;
            }
        }

        public List<DTO.ReceiptSupportSearchSupplier> QuyerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ReceiptSupportSearchSupplier> data = new List<DTO.ReceiptSupportSearchSupplier>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.ReceiptNoteMng_SupportFactoryRawMaterialSearch_View.Where(o => o.FactoryRawMaterialUD.Contains(query) || o.FactoryRawMaterialShortNM.Contains(query)).ToList();
                    data = converter.DB2DTO_SupportSearchSupplier(result).ToList();
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

        public List<DTO.EmployeeDTO> QuyeryEmployee(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.EmployeeDTO> data = new List<DTO.EmployeeDTO>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.ReceiptNoteMng_Function_SearchEmployee(query).ToList();
                    data = converter.DB2DTO_SupportSearchEmployee(result).ToList();
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

        public List<DTO.ProductionItemDTO> QueryProductionItem(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ProductionItemDTO> data = new List<DTO.ProductionItemDTO>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.ReceiptNoteMng_SupportProductionItemSearch_View.Where(o => o.ProductionItemUD.Contains(query) || o.ProductionItemNM.Contains(query)).ToList();
                    data = converter.DB2DTO_SupportSearchProductionItem(result).ToList();
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

        public void ValidatePayment(int receiptNoteID, int status)
        {
            using (var context = CreateContext())
            {
                var result = context.ReceiptNoteMng_ReceiptNoteInvoiceEdit_View.Where(o => o.ReceiptNoteID == receiptNoteID).ToList();

                if (status == 1)//Open
                {
                    //foreach (var item in result.ToArray())
                    //{
                    //    PurchasingInvoice dbPurchasingInvoice = context.PurchasingInvoice.Where(o => o.PurchasingInvoiceID == item.PurchasingInvoiceID).FirstOrDefault();
                    //    if (dbPurchasingInvoice != null && dbPurchasingInvoice.StatusID == 3)
                    //    {
                    //        dbPurchasingInvoice.StatusID = 2;
                    //        dbPurchasingInvoice.FinishDate = null;
                    //        context.SaveChanges();
                    //    }
                    //    if (dbPurchasingInvoice != null)
                    //    {                      
                    //        context.SaveChanges();
                    //    }
                    //}
                }
                if (status == 2)//Confirm
                {
                    foreach (var item in result.ToArray())
                    {
                        var invoice = context.ReceiptNoteMng_SupportPurchasingInvoiceSearch_View.Where(o => o.PurchasingInvoiceID == item.PurchasingInvoiceID).FirstOrDefault();
                        if (invoice != null)
                        {
                            var stockMoney = invoice.RemainQnt - item.Amount;
                            if (stockMoney < 0)
                            {
                                throw new Exception("Plase check Payment for Invoice: " + invoice.InvoiceNo);
                            }
                            if (stockMoney == 0)
                            {
                                PurchasingInvoice dbPurchasingInvoice = context.PurchasingInvoice.Where(o => o.PurchasingInvoiceID == invoice.PurchasingInvoiceID).FirstOrDefault();
                                //dbPurchasingInvoice.StatusID = 3;
                                //dbPurchasingInvoice.FinishDate = DateTime.Now;
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            throw new Exception("Can't find Invoice !!!");
                        }
                    }
                }
            }
        }

        //public string GetReceiptPrintout(int receiptNoteprintID, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    ReportDataObject ds = new ReportDataObject();
        //    try
        //    {
        //        SqlDataAdapter adap = new SqlDataAdapter();
        //        adap.SelectCommand = new SqlCommand("ReceiptNoteMng_Function_Print", new SqlConnection(Library.Helper.GetSQLConnectionString()));
        //        adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //        adap.SelectCommand.Parameters.AddWithValue("@ReceiptNoteID", receiptNoteprintID);

        //        adap.TableMappings.Add("Table", "Receipt");
        //        adap.TableMappings.Add("Table1", "ReceiptDetail");
        //        adap.Fill(ds);

        //        ds.Tables["Receipt"].Rows[0]["AmountToText"] = ConvertNumber2String(Convert.ToDecimal(ds.Tables["Receipt"].Rows[0]["Amount"]));

        //        return Library.Helper.CreateReceiptPrintout(ds.Tables["Receipt"], ds.Tables["ReceiptDetail"], "ReceiptNote.rdlc");
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = Library.Helper.GetInnerException(ex).Message;
        //        return string.Empty;
        //    }
        //}

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
    }
}
