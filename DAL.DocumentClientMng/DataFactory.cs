using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using DALBase;
namespace DAL.DocumentClientMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.DocumentClientMng.DocumentClientSearchResult, DTO.DocumentClientMng.DocumentClient>
    {
        private DataConverter converter = new DataConverter();
        private DocumentClientMngEntities CreateContext()
        {
            return new DocumentClientMngEntities(DALBase.Helper.CreateEntityConnectionString("DocumentClientMngModel"));
        }

        public override IEnumerable<DTO.DocumentClientMng.DocumentClientSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ContainerNo = string.Empty;
            string BLNo = string.Empty;
            int DeliveryStatusID = 0;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            string ProformaInvoiceNo = string.Empty;
            string InvoiceNo = string.Empty;
            string Season = string.Empty;
            string ETA = string.Empty;
            string ETA2 = string.Empty;

            if (filters.ContainsKey("ContainerNo")) ContainerNo = filters["ContainerNo"].ToString();
            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
            if (filters.ContainsKey("DeliveryStatusID") && filters["DeliveryStatusID"] != null) DeliveryStatusID = Convert.ToInt32(filters["DeliveryStatusID"]);
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("ETA") && filters["ETA"] != null) ETA = filters["ETA"].ToString();
            if (filters.ContainsKey("ETA2") && filters["ETA2"] != null) ETA2 = filters["ETA2"].ToString();

            //try to get data
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    totalRows = context.DocumentClientMng_function_SearchDocumentClient(orderBy, orderDirection, ContainerNo, BLNo, DeliveryStatusID, ClientUD, ClientNM, ProformaInvoiceNo, InvoiceNo, Season, ETA, ETA2).Count();

                    var result = context.DocumentClientMng_function_SearchDocumentClient(orderBy, orderDirection, ContainerNo, BLNo, DeliveryStatusID, ClientUD, ClientNM, ProformaInvoiceNo, InvoiceNo, Season, ETA, ETA2);
                    List<DTO.DocumentClientMng.DocumentClientSearchResult> dtoSearch = new List<DTO.DocumentClientMng.DocumentClientSearchResult>();
                    dtoSearch = converter.DB2DTO_DocumentClientSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    var IDs = dtoSearch.Select(o => o.DocumentClientID).ToList();
                    var orderInfos = context.DocumentClientMng_OrderInfo_View.Where(o => o.DocumentClientID.HasValue && IDs.Contains(o.DocumentClientID.Value)).ToList();

                    foreach (var dtoItem in dtoSearch)
                    {
                        //foreach (var dbOrder in orderInfos.Where(o => o.DocumentClientID == dtoItem.DocumentClientID))
                        //{
                        //    if (!dtoItem.ClientUD.Contains(dbOrder.ClientUD)) dtoItem.ClientUD += dbOrder.ClientUD + "/ ";
                        //    if (!dtoItem.ClientNM.Contains(dbOrder.ClientNM)) dtoItem.ClientNM += dbOrder.ClientNM + "/ ";
                        //    if (!dtoItem.ProformaInvoiceNo.Contains(dbOrder.ProformaInvoiceNo)) dtoItem.ProformaInvoiceNo += dbOrder.ProformaInvoiceNo + "/ ";
                        //}
                        //if (dtoItem.ClientUD.Length > 2) dtoItem.ClientUD = dtoItem.ClientUD.Substring(0, dtoItem.ClientUD.Length - 2);
                        //if (dtoItem.ClientNM.Length > 2) dtoItem.ClientNM = dtoItem.ClientNM.Substring(0, dtoItem.ClientNM.Length - 2);
                        //if (dtoItem.ProformaInvoiceNo.Length > 2) dtoItem.ProformaInvoiceNo = dtoItem.ProformaInvoiceNo.Substring(0, dtoItem.ProformaInvoiceNo.Length - 2);

                        if (orderInfos.Count(o => o.DocumentClientID == dtoItem.DocumentClientID) > 0)
                        {
                            dtoItem.ClientUD = orderInfos.Where(o => o.DocumentClientID == dtoItem.DocumentClientID).Select(o => o.ClientUD).Distinct().Aggregate((i, j) => i + " / " + j);
                            dtoItem.ClientNM = orderInfos.Where(o => o.DocumentClientID == dtoItem.DocumentClientID).Select(o => o.ClientNM).Distinct().Aggregate((i, j) => i + " / " + j);
                            dtoItem.ProformaInvoiceNo = orderInfos.Where(o => o.DocumentClientID == dtoItem.DocumentClientID).Select(o => o.ProformaInvoiceNo).Distinct().Aggregate((i, j) => i + " / " + j);
                        }

                        foreach (var dbEurofarInvoice in context.DocumentClientMng_ECommercialInvoice_View.Where(o => o.DocumentClientID != null && o.DocumentClientID == dtoItem.DocumentClientID))
                        {
                            if (!dtoItem.InvoiceNo.Contains(dbEurofarInvoice.EurofarInvoiceNo)) dtoItem.InvoiceNo += dbEurofarInvoice.EurofarInvoiceNo + "/ ";
                        }
                        if (dtoItem.InvoiceNo.Length > 2) dtoItem.InvoiceNo = dtoItem.InvoiceNo.Substring(0, dtoItem.InvoiceNo.Length - 2);
                    }

                    return dtoSearch;
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
                return new List<DTO.DocumentClientMng.DocumentClientSearchResult>();
            }
        }

        public override DTO.DocumentClientMng.DocumentClient GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_DocumentClient(context.DocumentClientMng_DocumentClient_View.FirstOrDefault(o => o.DocumentClientID == id));
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
                return new DTO.DocumentClientMng.DocumentClient();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    DocumentClient dbItem = context.DocumentClient.FirstOrDefault(o => o.DocumentClientID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Material not found!";
                        return false;
                    }
                    else
                    {
                        context.DocumentClient.Remove(dbItem);
                        context.SaveChanges();

                        return true;
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
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.DocumentClientMng.DocumentClient dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    DocumentClient dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new DocumentClient();
                        context.DocumentClient.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.DocumentClient.FirstOrDefault(o => o.DocumentClientID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Data not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_DocumentClient(dtoItem, ref dbItem);
                        if (id == 0)
                        {
                            dbItem.CreatedBy = dtoItem.UpdatedBy;
                            dbItem.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            dbItem.UpdatedBy = dtoItem.UpdatedBy;
                            dbItem.UpdatedDate = DateTime.Now;
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.DocumentClientID, out notification);

                        return true;
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
                return false;
            }
        }

        public bool QuickUpdateData(int UserID, ref List<DTO.DocumentClientMng.DocumentClientSearchUpdate> dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    foreach (DTO.DocumentClientMng.DocumentClientSearchUpdate item in dtoItem)
                    {
                        DocumentClient dbItem = null;
                        dbItem = context.DocumentClient.FirstOrDefault(o => o.DocumentClientID == item.DocumentClientID);


                        if (dbItem == null)
                        {
                            notification.Message = "Data not found!";
                            return false;
                        }
                        else
                        {
                            if (item.IsEdit == 1)
                            {
                                converter.DTO2DB_QuickDocumentClient(item, ref dbItem);
                                dbItem.UpdatedBy = UserID;
                                dbItem.UpdatedDate = DateTime.Now;
                            }
                            context.SaveChanges();
                        }
                    }
                    return true;
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
                return false;
            }
        }

        public DTO.DocumentClientMng.DataContainer GetDataContainer(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    DTO.DocumentClientMng.DataContainer dtoItem = new DTO.DocumentClientMng.DataContainer();

                    if (id > 0)
                    {
                        DocumentClientMng_DocumentClient_View dbItem = context.DocumentClientMng_DocumentClient_View
                            .Include("DocumentClientMng_ECommercialInvoice_View")
                            .FirstOrDefault(o => o.DocumentClientID == id);
                        DTO.DocumentClientMng.DocumentClient documentClientDTOItem = converter.DB2DTO_DocumentClient(dbItem);

                        //if (documentClientDTOItem.ConcurrencyFlag != null)
                        //{
                        //    documentClientDTOItem.ConcurrencyFlag_String = Convert.ToBase64String(documentClientDTOItem.ConcurrencyFlag);
                        //}

                        var dbOrder = context.DocumentClientMng_OrderInfo_View.Where(o => o.DocumentClientID != null && o.DocumentClientID == id).FirstOrDefault();
                        if (dbOrder != null)
                        {
                            documentClientDTOItem.ClientUD = dbOrder.ClientUD;
                            documentClientDTOItem.ClientNM = dbOrder.ClientNM;
                            documentClientDTOItem.ProformaInvoiceNo = dbOrder.ProformaInvoiceNo;
                        }
                        dtoItem.DocumentClientData = documentClientDTOItem;
                    }
                    else
                    {
                        dtoItem.DocumentClientData = new DTO.DocumentClientMng.DocumentClient();
                    }

                    // get support data
                    dtoItem.TypeOfDeliverys = converter.DB2DTO_TypeOfDeliverys(context.TypeOfDelivery.ToList());
                    dtoItem.PlaceOfBarges = converter.DB2DTO_PlaceOfBarges(context.PlaceOfBarge.ToList());
                    dtoItem.PlaceOfDeliverys = converter.DB2DTO_PlaceOfDeliverys(context.PlaceOfDelivery.ToList());
                    dtoItem.DeliveryStatuss = converter.DB2DTO_DeliveryStatuss(context.DeliveryStatus.ToList());
                    dtoItem.PaymentStatuss = converter.DB2DTO_PaymentStatuss(context.PaymentStatus.ToList());
                    dtoItem.Seasons = new DAL.Support.DataFactory().GetSeason().ToList();
                    dtoItem.ReportTemplates = new DAL.Support.DataFactory().GetReportTemplate().ToList();

                    return dtoItem;
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
                return new DTO.DocumentClientMng.DataContainer();
            }
        }

        public bool Add_TypeOfDelivery(string TypeOfDeliveryNM, out int TypeOfDeliveryID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            TypeOfDeliveryID = -1;
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    TypeOfDelivery dbItem = null;
                    if (!string.IsNullOrEmpty(TypeOfDeliveryNM))
                    {
                        dbItem = new TypeOfDelivery();
                        dbItem.TypeOfDeliveryNM = TypeOfDeliveryNM;
                        context.TypeOfDelivery.Add(dbItem);
                        context.SaveChanges();
                        TypeOfDeliveryID = dbItem.TypeOfDeliveryID;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public bool Add_PlaceOfBarge(string PlaceOfBargeNM, out int PlaceOfBargeID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PlaceOfBargeID = -1;
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    PlaceOfBarge dbItem = null;
                    if (!string.IsNullOrEmpty(PlaceOfBargeNM))
                    {
                        dbItem = new PlaceOfBarge();
                        dbItem.PlaceOfBargeNM = PlaceOfBargeNM;
                        context.PlaceOfBarge.Add(dbItem);
                        context.SaveChanges();
                        PlaceOfBargeID = dbItem.PlaceOfBargeID;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public bool Add_PlaceOfDelivery(string PlaceOfDeliveryNM, out int PlaceOfDeliveryID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PlaceOfDeliveryID = -1;
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    PlaceOfDelivery dbItem = null;
                    if (!string.IsNullOrEmpty(PlaceOfDeliveryNM))
                    {
                        dbItem = new PlaceOfDelivery();
                        dbItem.PlaceOfDeliveryNM = PlaceOfDeliveryNM;
                        context.PlaceOfDelivery.Add(dbItem);
                        context.SaveChanges();
                        PlaceOfDeliveryID = dbItem.PlaceOfDeliveryID;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public bool Add_DeliveryStatus(string DeliveryStatusNM, out int DeliveryStatusID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DeliveryStatusID = -1;
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    DeliveryStatus dbItem = null;
                    if (!string.IsNullOrEmpty(DeliveryStatusNM))
                    {
                        dbItem = new DeliveryStatus();
                        dbItem.DeliveryStatusNM = DeliveryStatusNM;
                        context.DeliveryStatus.Add(dbItem);
                        context.SaveChanges();
                        DeliveryStatusID = dbItem.DeliveryStatusID;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public bool Add_PaymentStatus(string PaymentStatusNM, out int PaymentStatusID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PaymentStatusID = -1;
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    PaymentStatus dbItem = null;
                    if (!string.IsNullOrEmpty(PaymentStatusNM))
                    {
                        dbItem = new PaymentStatus();
                        dbItem.PaymentStatusNM = PaymentStatusNM;
                        context.PaymentStatus.Add(dbItem);
                        context.SaveChanges();
                        PaymentStatusID = dbItem.PaymentStatusID;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public DTO.DocumentClientMng.DataSearchContainer SearchDataContainer(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.DocumentClientMng.DataSearchContainer dtoSearch = new DTO.DocumentClientMng.DataSearchContainer();

            dtoSearch.DocumentClientSearchResults = GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification).ToList();
            using (DocumentClientMngEntities context = CreateContext())
            {
                dtoSearch.DeliveryStatuss = converter.DB2DTO_DeliveryStatuss(context.DeliveryStatus.ToList());
            }
            dtoSearch.Seasons = new DAL.Support.DataFactory().GetSeason().ToList();
            return dtoSearch;
        }

        public string GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Print Success" };
            ReportDataObject dsReport = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DocumentClientMng_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandTimeout = 600;
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.TableMappings.Add("Table", "DocumentClientMng_DocumentClient_ReportView");
                adap.TableMappings.Add("Table1", "DocumentClientMng_OrderInfo_View");
                adap.Fill(dsReport);

                ////get list documentclientID
                //var documentClientIDs = from p in dsReport.DocumentClientMng_DocumentClient_ReportView.Where(o => !o.IsDocumentClientIDNull()) group p by new { p.DocumentClientID } into g select new { g.Key.DocumentClientID };

                ////compite data report
                //foreach (var idItem in documentClientIDs)
                //{
                //    int i = 1;
                //    foreach (ReportDataObject.DocumentClientMng_DocumentClient_ReportViewRow documentItem in dsReport.DocumentClientMng_DocumentClient_ReportView.Where(o => !o.IsDocumentClientIDNull() && o.DocumentClientID == idItem.DocumentClientID))
                //    {
                //        if (i > 1)
                //        {
                //            documentItem["DC20"] = DBNull.Value;
                //            documentItem["DC40"] = DBNull.Value;
                //            documentItem["HC40"] = DBNull.Value;
                //            documentItem["CostOfDemurage1"] = DBNull.Value;
                //            documentItem["CostOfDemurage2"] = DBNull.Value;
                //        }
                //        i++;
                //    } 
                //}

                //SET Order Info for containers that were not put in ecommercialinvoice
                foreach (var item in dsReport.DocumentClientMng_DocumentClient_ReportView.Where(o => !o.IsDocumentClientIDNull() && (o.IsClientUDNull() || o.IsClientNMNull() || o.IsSaleNMNull())))
                {
                    // cuong.tran - 07-Jan-2018 - start
                    //item.ClientUD = "";
                    //item.ClientNM = "";
                    // cuong.tran - 07-Jan-2018 - e n d
                    item.SaleNM = "";
                    //foreach (var dbOrder in dsReport.DocumentClientMng_OrderInfo_View.Where(o => !o.IsDocumentClientIDNull() && o.DocumentClientID == item.DocumentClientID))
                    //{
                    //    if (!dbOrder.IsClientUDNull() && !item.ClientUD.Contains(dbOrder.ClientUD)) item.ClientUD += dbOrder.ClientUD + "/ ";
                    //    if (!dbOrder.IsClientNMNull() && !item.ClientNM.Contains(dbOrder.ClientNM)) item.ClientNM += dbOrder.ClientNM + "/ ";
                    //    if (!dbOrder.IsSaleNMNull() && !item.SaleNM.Contains(dbOrder.SaleNM)) item.SaleNM += dbOrder.SaleNM + "/ ";
                    //}
                    //if (item.ClientUD.Length > 2) item.ClientUD = item.ClientUD.Substring(0, item.ClientUD.Length - 2);
                    //if (item.ClientNM.Length > 2) item.ClientNM = item.ClientNM.Substring(0, item.ClientNM.Length - 2);
                    //if (item.SaleNM.Length > 2) item.SaleNM = item.SaleNM.Substring(0, item.SaleNM.Length - 2);

                    if (dsReport.DocumentClientMng_OrderInfo_View.Count(o => !o.IsDocumentClientIDNull() && o.DocumentClientID == item.DocumentClientID) > 0)
                    {
                        // cuong.tran - 07-Jan-2018 - start
                        //item.ClientUD = dsReport.DocumentClientMng_OrderInfo_View.Where(o => !o.IsDocumentClientIDNull() && o.DocumentClientID == item.DocumentClientID).Select(o => o.ClientUD).Distinct().Aggregate((i, j) => i + " / " + j);
                        //item.ClientNM = dsReport.DocumentClientMng_OrderInfo_View.Where(o => !o.IsDocumentClientIDNull() && o.DocumentClientID == item.DocumentClientID).Select(o => o.ClientNM).Distinct().Aggregate((i, j) => i + " / " + j);
                        // cuong.tran - 07-Jan-2018 - e n d
                        item.SaleNM = dsReport.DocumentClientMng_OrderInfo_View.Where(o => !o.IsDocumentClientIDNull() && o.DocumentClientID == item.DocumentClientID).Select(o => o.SaleNM).Distinct().Aggregate((i, j) => i + " / " + j);
                    }
                }

                dsReport.AcceptChanges();

                // DALBase.Helper.DevCreateReportXMLSource(dsReport, "DocumentClient");
                return DALBase.Helper.CreateReportFiles(dsReport, "DocumentClient");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public bool ConfirmDateContainerDelivery(int id, string dateContainerDelivery, int confirmedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Date container delivery have been confirmed success" };
            try
            {
                using (DocumentClientMngEntities context = CreateContext())
                {
                    DocumentClient dbItem = context.DocumentClient.Where(o => o.DocumentClientID == id).FirstOrDefault();
                    dbItem.DateContainerDelivery = dateContainerDelivery.ConvertStringToDateTime();
                    dbItem.IsConfirmedDateContainerDelivery = true;
                    dbItem.ConfirmedDateContainerDeliveryBy = confirmedBy;
                    dbItem.ConfirmedDateContainerDeliveryDate = DateTime.Now;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }
    }
}
