using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.CashBookReceiptMng.DTO;
using Library;
using System.Data.SqlClient;

namespace Module.CashBookReceiptMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<SearchFormData, EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");

        public CashBookReceiptEntities CreateContext()
        {
            return new CashBookReceiptEntities(Library.Helper.CreateEntityConnectionString("DAL.CashBookReceiptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    CashBook dbItem = context.CashBook.FirstOrDefault(o => o.CashBookID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not found data!";

                        return false;
                    }

                    context.CashBook.Remove(dbItem);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            EditFormData data = new EditFormData()
            {
                Data = new CashBookData()
                {
                    CashBookDocuments = new List<CashBookDocumentData>()
                },

                CashBookTypes = new List<CashBookTypeData>(),
                CashBookSourceOfFlows = new List<CashBookSourceOfFlowData>(),
                CashBookLocations = new List<CashBookLocationData>(),
                CashBookPostCosts = new List<CashBookPostCostData>(),
                CashBookCostItems = new List<CashBookCostItemData>(),
                CashBookCostItemDetails = new List<CashBookCostItemDetailData>(),
                CashBookPaidBys = new List<CashBookPaidByData>()
            };

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_CashBook(context.CashBookMng_CashBook_View.FirstOrDefault(o => o.CashBookID == id));

                        if (data.Data == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";
                        }
                    }

                    data.CashBookTypes = converter.DB2DTO_CashBookType(context.CashBookMng_CashBookType_View.ToList());
                    data.CashBookSourceOfFlows = converter.DB2DTO_CashBookSourceOfFlow(context.CashBookMng_CashBookSourceOfFlow_View.ToList());
                    data.CashBookLocations = converter.DB2DTO_CashBookLocation(context.CashBookMng_CashBookLocation_View.ToList());
                    data.CashBookPostCosts = converter.DB2DTO_CashBookPostCost(context.CashBookMng_CashBookPostCost_View.ToList());
                    data.CashBookCostItems = converter.DB2DTO_CashBookCostItem(context.CashBookMng_CashBookCostItem_View.ToList());
                    data.CashBookCostItemDetails = converter.DB2DTO_CashBookCostItemDetail(context.CashBookMng_CashBookCostItemDetail_View.ToList());
                    data.CashBookPaidBys = converter.DB2DTO_CashBookPaidBy(context.CashBookMng_CashBookPaidBy2_View.ToList());

                    if (id == 0)
                    {
                        var dbItem = context.CashBook.OrderByDescending(o => o.UpdatedDate).FirstOrDefault();
                        if (dbItem != null)
                        {
                            data.Data.VNDUSDExRate = dbItem.VNDUSDExRate;
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return data;
            }
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            SearchFormData data = new SearchFormData()
            {
                Data = new List<CashBookData>()
            };

            try
            {
                string cashBookUD = filters.ContainsKey("Code") && filters["Code"] != null && !string.IsNullOrEmpty(filters["Code"].ToString().Replace("'", "''")) ? filters["Code"].ToString().Trim() : null;
                DateTime? fromDate = filters.ContainsKey("FromDate") && filters["FromDate"] != null && !string.IsNullOrEmpty(filters["FromDate"].ToString().Replace("'", "''")) ? filters["FromDate"].ToString().Trim().ConvertStringToDateTime() : null;
                DateTime? toDate = filters.ContainsKey("ToDate") && filters["ToDate"] != null && !string.IsNullOrEmpty(filters["ToDate"].ToString().Replace("'", "''")) ? filters["ToDate"].ToString().Trim().ConvertStringToDateTime() : null;
                int? cashBookTypeID = filters.ContainsKey("Type") && filters["Type"] != null && !string.IsNullOrEmpty(filters["Type"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["Type"].ToString().Trim()) : null;
                int? cashBookSourceOfFlowID = filters.ContainsKey("SourceOfFlow") && filters["SourceOfFlow"] != null && !string.IsNullOrEmpty(filters["SourceOfFlow"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["SourceOfFlow"].ToString().Trim()) : null;
                int? cashBookLocationID = filters.ContainsKey("Location") && filters["Location"] != null && !string.IsNullOrEmpty(filters["Location"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["Location"].ToString().Trim()) : null;
                int? cashBookPaidByID = filters.ContainsKey("PaidBy") && filters["PaidBy"] != null && !string.IsNullOrEmpty(filters["PaidBy"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["PaidBy"].ToString().Trim()) : null;
                int? cashBookPostCostID = filters.ContainsKey("PostCost") && filters["PostCost"] != null && !string.IsNullOrEmpty(filters["PostCost"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["PostCost"].ToString().Trim()) : null;
                int? cashBookCostItemID = filters.ContainsKey("CostItem") && filters["CostItem"] != null && !string.IsNullOrEmpty(filters["CostItem"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["CostItem"].ToString().Trim()) : null;
                int? cashBookCostItemDetailID = filters.ContainsKey("CostItemDetail") && filters["CostItemDetail"] != null && !string.IsNullOrEmpty(filters["CostItemDetail"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["CostItemDetail"].ToString().Trim()) : null;

                using (var context = CreateContext())
                {
                    totalRows = context.CashBookMng_function_CashBookSearchResult(cashBookUD, fromDate, toDate, cashBookTypeID, cashBookSourceOfFlowID, cashBookLocationID, cashBookPaidByID, cashBookPostCostID, cashBookCostItemID, cashBookCostItemDetailID, orderBy, orderDirection).Count();
                    data.Data = converter.DB2DTO_CashBookSearchResult(context.CashBookMng_function_CashBookSearchResult(cashBookUD, fromDate, toDate, cashBookTypeID, cashBookSourceOfFlowID, cashBookLocationID, cashBookPaidByID, cashBookPostCostID, cashBookCostItemID, cashBookCostItemDetailID, orderBy, orderDirection).ToList());
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return data;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            CashBookData dataItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<CashBookData>();

            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    CashBook dbItem;

                    if (id > 0)
                    {
                        dbItem = context.CashBook.FirstOrDefault(o => o.CashBookID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return false;
                        }
                    }
                    else
                    {
                        dbItem = context.CashBook.FirstOrDefault(o => o.CashBookUD == dataItem.CashBookUD);

                        if (dbItem != null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "CashBook receipt is exist in database!";

                            return false;
                        }

                        DateTime bookDate = new DateTime();

                        if (!string.IsNullOrEmpty(dataItem.BookDate))
                        {
                            if (DateTime.TryParse(dataItem.BookDate, nl, System.Globalization.DateTimeStyles.None, out DateTime tmpDate))
                            {
                                bookDate = tmpDate;
                            }
                        }

                        var cashBalance = context.CashBookBalanceMng_CashBookBalance_View.OrderByDescending(o => o.UpdatedDate).FirstOrDefault();

                        if (!IsValidCreateCashBook(bookDate, cashBalance, out string messageError, out NotificationType typeError))
                        {
                            notification.Type = typeError;
                            notification.Message = messageError;

                            return false;
                        }

                        dbItem = new CashBook();
                        context.CashBook.Add(dbItem);
                    }

                    foreach (CashBookDocumentData item in dataItem.CashBookDocuments)
                    {
                        Module.Framework.DAL.DataFactory factory = new Framework.DAL.DataFactory();

                        if (item.HasChange)
                        {
                            if (string.IsNullOrEmpty(item.NewFile))
                            {
                                factory.RemoveImageFile(item.FileUD);
                            }
                            else
                            {
                                item.FileUD = factory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", item.NewFile, item.FileUD);
                            }
                        }
                    }

                    converter.DTO2DB_CashBook(dataItem, ref dbItem);
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    context.CashBookDocument.Local.Where(o => o.CashBook == null).ToList().ForEach(o => context.CashBookDocument.Remove(o));

                    context.SaveChanges();

                    dtoItem = converter.DB2DTO_CashBook(context.CashBookMng_CashBook_View.FirstOrDefault(o => o.CashBookID == dbItem.CashBookID));
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public InitFormData GetInitData(out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            InitFormData data = new InitFormData()
            {
                CashBookTypes = new List<CashBookTypeData>(),
                CashBookSourceOfFlows = new List<CashBookSourceOfFlowData>(),
                CashBookLocations = new List<CashBookLocationData>(),
                CashBookPostCosts = new List<CashBookPostCostData>(),
                CashBookCostItems = new List<CashBookCostItemData>(),
                CashBookCostItemDetails = new List<CashBookCostItemDetailData>(),
                CashBookPaidBys = new List<CashBookPaidByData>()
            };

            try
            {
                using (var context = CreateContext())
                {
                    data.CashBookTypes = converter.DB2DTO_CashBookType(context.CashBookMng_CashBookType_View.ToList());
                    data.CashBookSourceOfFlows = converter.DB2DTO_CashBookSourceOfFlow(context.CashBookMng_CashBookSourceOfFlow_View.ToList());
                    data.CashBookLocations = converter.DB2DTO_CashBookLocation(context.CashBookMng_CashBookLocation_View.ToList());
                    data.CashBookPostCosts = converter.DB2DTO_CashBookPostCost(context.CashBookMng_CashBookPostCost_View.ToList());
                    data.CashBookCostItems = converter.DB2DTO_CashBookCostItem(context.CashBookMng_CashBookCostItem_View.ToList());
                    data.CashBookCostItemDetails = converter.DB2DTO_CashBookCostItemDetail(context.CashBookMng_CashBookCostItemDetail_View.ToList());
                    data.CashBookPaidBys = converter.DB2DTO_CashBookPaidBy(context.CashBookMng_CashBookPaidBy2_View.ToList());
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return data;
            }
        }

        public List<CashBookPostCostData> UpdatePostCostData(int userId, object dtoItem, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            CashBookPostCostData dataItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<CashBookPostCostData>();
            List<CashBookPostCostData> postCostData = new List<CashBookPostCostData>();

            try
            {
                using (var context = CreateContext())
                {
                    CashBookPostCost dbItem;

                    if (dataItem.CashBookPostCostID > 0)
                    {
                        dbItem = context.CashBookPostCost.FirstOrDefault(o => o.CashBookPostCostID == dataItem.CashBookPostCostID);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return postCostData;
                        }
                    }
                    else
                    {
                        dbItem = new CashBookPostCost();
                        context.CashBookPostCost.Add(dbItem);
                    }

                    converter.DTO2DB_CashBookPostCost(dataItem, ref dbItem);
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    context.SaveChanges();

                    postCostData = converter.DB2DTO_CashBookPostCost(context.CashBookMng_CashBookPostCost_View.ToList());
                }

                return postCostData;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return postCostData;
            }
        }

        public bool DeletePostCostData(int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    CashBookPostCost dbItem = context.CashBookPostCost.FirstOrDefault(o => o.CashBookPostCostID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not found data!";

                        return false;
                    }

                    // set null for all item using it.
                    var dbItem2 = context.CashBook.Where(o => o.CashBookPostCostID == dbItem.CashBookPostCostID);
                    foreach (var item in dbItem2)
                    {
                        item.CashBookPostCostID = null;
                        item.CashBookCostItemID = null;
                        item.CashBookCostItemDetailID = null;
                    }

                    context.CashBookPostCost.Remove(dbItem);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public List<CashBookCostItemData> UpdateCostItemData(int userId, object dtoItem, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            CashBookCostItemData dataItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<CashBookCostItemData>();
            List<CashBookCostItemData> costItemData = new List<CashBookCostItemData>();

            try
            {
                using (var context = CreateContext())
                {
                    CashBookCostItem dbItem;

                    if (dataItem.CashBookCostItemID > 0)
                    {
                        dbItem = context.CashBookCostItem.FirstOrDefault(o => o.CashBookCostItemID == dataItem.CashBookCostItemID);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return costItemData;
                        }
                    }
                    else
                    {
                        dbItem = new CashBookCostItem();
                        context.CashBookCostItem.Add(dbItem);
                    }

                    converter.DTO2DB_CashBookCostItem(dataItem, ref dbItem);
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    context.SaveChanges();

                    costItemData = converter.DB2DTO_CashBookCostItem(context.CashBookMng_CashBookCostItem_View.ToList());
                }

                return costItemData;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return costItemData;
            }
        }

        public bool DeleteCostItemData(int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    CashBookCostItem dbItem = context.CashBookCostItem.FirstOrDefault(o => o.CashBookCostItemID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not found data!";

                        return false;
                    }

                    // set null for all item using it.
                    var dbItem2 = context.CashBook.Where(o => o.CashBookCostItemID == dbItem.CashBookCostItemID);
                    foreach (var item in dbItem2)
                    {
                        item.CashBookCostItemID = null;
                        item.CashBookCostItemDetailID = null;
                    }

                    context.CashBookCostItem.Remove(dbItem);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public List<CashBookCostItemDetailData> UpdateCostItemDetailData(int userId, object dtoItem, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            CashBookCostItemDetailData dataItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<CashBookCostItemDetailData>();
            List<CashBookCostItemDetailData> costItemData = new List<CashBookCostItemDetailData>();

            try
            {
                using (var context = CreateContext())
                {
                    CashBookCostItemDetail dbItem;

                    if (dataItem.CashBookCostItemDetailID > 0)
                    {
                        dbItem = context.CashBookCostItemDetail.FirstOrDefault(o => o.CashBookCostItemDetailID == dataItem.CashBookCostItemDetailID);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return costItemData;
                        }
                    }
                    else
                    {
                        dbItem = new CashBookCostItemDetail();
                        context.CashBookCostItemDetail.Add(dbItem);
                    }

                    converter.DTO2DB_CashBookCostItemDetail(dataItem, ref dbItem);
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    context.SaveChanges();

                    costItemData = converter.DB2DTO_CashBookCostItemDetail(context.CashBookMng_CashBookCostItemDetail_View.ToList());
                }

                return costItemData;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return costItemData;
            }
        }

        public bool DeleteCostItemDetailData(int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    CashBookCostItemDetail dbItem = context.CashBookCostItemDetail.FirstOrDefault(o => o.CashBookCostItemDetailID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not found data!";

                        return false;
                    }

                    // set null for all item using it.
                    var dbItem2 = context.CashBook.Where(o => o.CashBookCostItemDetailID == dbItem.CashBookCostItemDetailID);
                    foreach (var item in dbItem2)
                    {
                        item.CashBookCostItemDetailID = null;
                    }

                    context.CashBookCostItemDetail.Remove(dbItem);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public string ExportCashBook(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            int? month = (filters.ContainsKey("Month") && filters["Month"] != null && !string.IsNullOrEmpty(filters["Month"].ToString().Trim().Replace("'", "''"))) ? (int?)Convert.ToInt32(filters["Month"].ToString().Trim()) : null;
            int? year = (filters.ContainsKey("Year") && filters["Year"] != null && !string.IsNullOrEmpty(filters["Year"].ToString().Trim().Replace("'", "''"))) ? (int?)Convert.ToInt32(filters["Year"].ToString().Trim()) : null;

            try
            {
                ReportDataObject rpt = new ReportDataObject();

                SqlDataAdapter sda = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand("Report_function_GetCashBook", new SqlConnection(Helper.GetSQLConnectionString()))
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    }
                };

                sda.SelectCommand.Parameters.AddWithValue("@Month", month);
                sda.SelectCommand.Parameters.AddWithValue("@Year", year);

                sda.TableMappings.Add("Table", "CashBookMng_ReportDetail_View");
                sda.TableMappings.Add("Table1", "CashBookMng_ReportTotal_View");

                sda.Fill(rpt);

                string pathFile = Helper.CreateReportFileWithEPPlus(rpt, "ReportCashBook");
                return pathFile;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return "";
            }
        }

        private bool IsValidCreateCashBook(DateTime date, CashBookBalanceMng_CashBookBalance_View balance, out string messageError, out NotificationType typeError)
        {
            messageError = "";
            typeError = NotificationType.Success;

            int monthBookDate = date.Month;
            int yearBookDate = date.Year;

            if (monthBookDate - balance.BalanceMonth.Value == 0)
            {
                typeError = NotificationType.Error;
                messageError = "You can create CashBook of past-time!";
                return false;
            }

            if (Math.Abs(monthBookDate - balance.BalanceMonth.Value) > 1)
            {
                typeError = NotificationType.Error;
                messageError = "You need balance CashBook of month before!";
                return false;
            }

            return true;
        }
    }
}
