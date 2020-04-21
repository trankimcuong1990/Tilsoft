using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private DraftCommercialInvoiceMngEntities CreateContext()
        {
            return new DraftCommercialInvoiceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.DraftCommercialInvoiceMngModel"));
        }

        public DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.DraftCommercialInvoiceSearchDTO>();
            data.Seasons = new List<DTO.Season>();
            //data.WorkCenters = new List<DTO.WorkCenterDTO>();
            totalRows = 0;

            string invoiceNo = null;
            string clientUD = null;
            string clientNM = null;
            string clientInvoiceNo = null;
            string season = null;
            if (filters.ContainsKey("invoiceNo") && !string.IsNullOrEmpty(filters["invoiceNo"].ToString()))
            {
                invoiceNo = filters["invoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
            {
                clientUD = filters["clientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("clientNM") && !string.IsNullOrEmpty(filters["clientNM"].ToString()))
            {
                clientNM = filters["clientNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("clientInvoiceNo") && !string.IsNullOrEmpty(filters["clientInvoiceNo"].ToString()))
            {
                clientInvoiceNo = filters["clientInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
            {
                season = filters["season"].ToString().Replace("'", "''");
            }


            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    totalRows = context.DraftCommercialInvoiceMng_function_SearchDraftCommercialInvoice(invoiceNo, clientUD, clientNM, clientInvoiceNo, season, orderBy, orderDirection).Count();
                    var result = context.DraftCommercialInvoiceMng_function_SearchDraftCommercialInvoice(invoiceNo, clientUD, clientNM, clientInvoiceNo, season, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SearchDraftCommercialInvoice(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                //data.WorkCenters = converter.DB2DTO_GetWorkCenter(context.OutsourcingCostMng_WorkCenter_View.ToList());
                data.Seasons = GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.DraftCommercialInvoiceDTO();
            data.Seasons = new List<DTO.Season>();
            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    if (id != 0)
                    {
                        data.Data = converter.DB2DTO_DraftCommercialInvoice(context.DraftCommercialInvoiceMng_DraftCommercialInvoice_View.FirstOrDefault(o => o.DraftCommercialInvoiceID == id));
                    }
                    data.Seasons = GetSeason().ToList();
                    data.DeliveryTerms = converter.DB2DTO_DeliveryTerm(context.DraftCommercialInvoiceMng_DeliveryTerm_View.ToList());
                    data.PaymentTerms = converter.DB2DTO_PaymentTerm(context.DraftCommercialInvoiceMng_PaymentTerm_View.ToList());
                    data.TurnOverLedgerAccounts = converter.DB2DTO_TurnOverLedgerAccount(context.DraftCommercialInvoiceMng_TurnOverLedgerAccount_View.ToList());
                }
   
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetOverView(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.DataOverView = new DTO.DraftCommercialInvoiceOverViewDTO();
            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    data.DataOverView = converter.DB2DTO_DraftCommercialInvoiceOverView(context.DraftCommercialInvoiceMng_DraftCommercialInvoice_OverView.FirstOrDefault(o => o.DraftCommercialInvoiceID == id));
                }

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DraftCommercialInvoiceDTO dtoDraftCommercialInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.DraftCommercialInvoiceDTO>();
            try
            {
                ////get companyID
                //Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                //int? companyID = fw_factory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    DraftCommercialInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new DraftCommercialInvoice();
                        context.DraftCommercialInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.DraftCommercialInvoice.Where(o => o.DraftCommercialInvoiceID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        if(id == 0)
                        {
                            string Season = dtoDraftCommercialInvoice.Season;
                            string Season_Year = Season.Substring(0, 4);
                            string invoiceNoTemplate = Season_Year.Substring(2, 2) + "42";
                            var invoices = context.DraftCommercialInvoice.Where(o => o.Season == Season && o.InvoiceNo.Substring(0, 4) == Season_Year).OrderByDescending(o => o.InvoiceNo);
                            if (invoices.ToList().Count() == 0)
                            {
                                dtoDraftCommercialInvoice.InvoiceNo = Season_Year + "0001";
                            }
                            else
                            {
                                var x = invoices.FirstOrDefault();
                                int iNo = 0;
                                iNo = Convert.ToInt32(x.InvoiceNo.Substring(4, 4)) + 1;
                                dtoDraftCommercialInvoice.InvoiceNo = Season_Year + iNo.ToString().PadLeft(4, '0');
                            }
                        }

                        //
                        //convert dto to db
                        converter.DTO2DB_DraftCommercialInvoice(dtoDraftCommercialInvoice, ref dbItem);
                        if(id == 0)
                        {                           
                            dbItem.CreatedBy = userId;
                            dbItem.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;
                        }

                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.DraftCommercialInvoiceID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        //public bool Delete(int id, out Notification notification)
        //{
        //    notification = new Notification { Type = NotificationType.Success };
        //    try
        //    {
        //        using (var context = CreateContext())
        //        {
        //            var dbItem = context.OutsourcingCost.Where(o => o.OutsourcingCostID == id).FirstOrDefault();
        //            context.OutsourcingCost.Remove(dbItem);
        //            context.SaveChanges();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //        notification.DetailMessage.Add(ex.Message);
        //        if (ex.GetBaseException() != null)
        //        {
        //            notification.DetailMessage.Add(ex.GetBaseException().Message);
        //        }
        //        return false;
        //    }
        //}
        public List<DTO.SupportClient> GetSupportClient(string client, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.SupportClient> data = new List<DTO.SupportClient>();
            data = new List<DTO.SupportClient>();
            try
            {
                using (DraftCommercialInvoiceMngEntities context = CreateContext())
                {
                    return data = converter.DB2DTO_SupportClient(context.DraftCommercialInvoiceMng_function_SearchClient(client).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return null;
            }           
        }

        private List<DTO.Season> GetSeason()
        {
            List<DTO.Season> data = new List<DTO.Season>();
            for (int i = 2005; i <= DateTime.Now.Year + 1; i++)
            {
                data.Add(new DTO.Season() { SeasonValue = i.ToString() + "/" + (i + 1).ToString(), SeasonText = i.ToString() + "/" + (i + 1).ToString() });
            }
            var result = data.OrderByDescending(f => f.SeasonValue).ToList();

            return result;
        }
        public object GetSaleOrder(int clientID, int checkSelect, string season, string proformaInvoiceNo, string articleCode,  out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.SaleOrderDetailDTO> saleOrderDetail = new List<DTO.SaleOrderDetailDTO>();
            List<DTO.SaleOrderDetailSparepartDTO> saleOrderDetailSparepart = new List<DTO.SaleOrderDetailSparepartDTO>();
            //try to get data
            try
            {
                using (DraftCommercialInvoiceMngEntities context = CreateContext())
                {
                    if(checkSelect == 0)
                    {
                        return converter.DB2DTO_SaleOrderDetail(context.DraftCommerialInvoiceMng_function_SaleOrderDetail(clientID, season, proformaInvoiceNo, articleCode).ToList());
                    }   
                    else
                    {
                        return converter.DB2DTO_SaleOrderDetailSparepart(context.DraftCommerialInvoiceMng_function_SaleOrderDetailSparepart(clientID, season, proformaInvoiceNo, articleCode).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                if (checkSelect == 0)
                {
                    return saleOrderDetail;
                }
                else { return saleOrderDetailSparepart; }
            }
        }
        public bool Confirm(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.DraftCommercialInvoice.FirstOrDefault(o => o.DraftCommercialInvoiceID == id);
                    if (dbItem == null)
                    {

                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedBy = userId;
                    dbItem.ConfirmedDate = DateTime.Now;

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.DraftCommercialInvoiceID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public string GetDraftInvoicePrintoutData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DraftInvoiceDataObject ds = new DraftInvoiceDataObject();

            //try to get data
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DraftCommercialInvoiceMng_function_GetDraftCommercialInvoice", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Id", id);
                adap.TableMappings.Add("Table", "DraftInvoice");
                adap.TableMappings.Add("Table1", "DraftInvoiceDetail");
                adap.Fill(ds);              

                return Library.Helper.CreateReceiptPrintout(ds.Tables["DraftInvoice"], ds.Tables["DraftInvoiceDetail"], "DraftInvoicePrintoutPDF.rdlc");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }
    }
}
