using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL.FactoryPaymentMng
{
    public class DataFactoryPayment : DALBase.FactoryBase2<DTO.FactoryPaymentMng.SearchData, DTO.FactoryPaymentMng.EditData, DTO.FactoryPaymentMng.FactoryPayment>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory support_factory = new Support.DataFactory();

        private FactoryPaymentMngEntities CreateContext()
        {
            return new FactoryPaymentMngEntities(DALBase.Helper.CreateEntityConnectionString("FactoryPaymentMngModel"));
        }

        public override DTO.FactoryPaymentMng.SearchData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryPaymentMng.SearchData data = new DTO.FactoryPaymentMng.SearchData();
            data.FactoryPayments = new List<DTO.FactoryPaymentMng.FactoryPaymentSearch>();

            data.Seasons = support_factory.GetSeason().ToList();
            data.Factories = support_factory.GetFactory().ToList();
            data.Suppliers = support_factory.GetSupplier().ToList();

            string PaymentReceiptNo = string.Empty;
            string Season = string.Empty;              
            int FactoryID = 0;

            if (filters.ContainsKey("PaymentReceiptNo")) PaymentReceiptNo = filters["PaymentReceiptNo"].ToString();
            if (filters.ContainsKey("Season") && filters["Season"]!=null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("FactoryID") && filters["FactoryID"]!=null) FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());

            totalRows = 0;            
            try
            {
                using (FactoryPaymentMngEntities context = CreateContext())
                {
                    totalRows = context.FactoryPaymentMng_function_Search(orderBy, orderDirection, PaymentReceiptNo,Season,FactoryID).Count();
                    var result = context.FactoryPaymentMng_function_Search(orderBy, orderDirection, PaymentReceiptNo, Season, FactoryID);
                    data.FactoryPayments = converter.DB2DTO_FactoryPaymentSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        public override DTO.FactoryPaymentMng.EditData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryPaymentMng.EditData data = new DTO.FactoryPaymentMng.EditData();
            data.FactoryPayment = new DTO.FactoryPaymentMng.FactoryPayment();

            try
            {
                using (FactoryPaymentMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.FactoryPayment = converter.DB2DTO_FactoryPayment(context.FactoryPaymentMng_FactoryPayment_View.FirstOrDefault(o => o.FactoryPaymentID == id));
                    }
                    data.Seasons = support_factory.GetSeason().ToList();
                    data.Factories = support_factory.GetFactory().ToList();
                    data.Suppliers = support_factory.GetSupplier().ToList();
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
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Delete success" };
            try
            {
                using (FactoryPaymentMngEntities context = CreateContext())
                {
                    FactoryPayment dbItem = context.FactoryPayment.FirstOrDefault(o => o.FactoryPaymentID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "FactoryPayment not found!";
                        return false;
                    }
                    else
                    {
                        context.FactoryPayment.Remove(dbItem);
                        context.SaveChanges();

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

        public override bool UpdateData(int id, ref DTO.FactoryPaymentMng.FactoryPayment dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryPaymentMngEntities context = CreateContext())
                {
                    FactoryPayment dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryPayment();
                        context.FactoryPayment.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryPayment.FirstOrDefault(o => o.FactoryPaymentID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "FactoryPayment not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD_FactoryPayment(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.FactoryPaymentID, out notification).FactoryPayment;
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

        public override bool Approve(int id, ref DTO.FactoryPaymentMng.FactoryPayment dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.FactoryPaymentMng.FactoryPayment dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public string GetGetOutStandingBalance(string Season, int SupplierID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success , Message = "Get report data sucess"};
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryPayment_function_GetOutStandingBalance", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@SupplierID", SupplierID);

                adap.TableMappings.Add("Table", "FactoryPayment_FactoryInvoice_ReportView");
                adap.TableMappings.Add("Table1", "FactoryPayment_FactoryPayment_ReportView");
                adap.TableMappings.Add("Table2", "FactoryPayment_FactoryInvoiceDetail_ReportView");
                adap.Fill(ds);


                //create report data
                if (ds.FactoryPayment_FactoryInvoice_ReportView.Count() > ds.FactoryPayment_FactoryPayment_ReportView.Count())
                {
                    for (int i = 0; i < ds.FactoryPayment_FactoryInvoice_ReportView.Count; i++)
                    {
                        DataRow drNew = ds.Invoice.NewRow();
                        drNew["No"] = i + 1;

                        drNew["Season"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["Season"];
                        drNew["SupplierUD"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["SupplierUD"];

                        drNew["FactoryInvoiceNo"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["FactoryInvoiceNo"];
                        drNew["InvoiceDate"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["InvoiceDate"];
                        drNew["Company"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["Company"];
                        drNew["ClientUD"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["ClientUD"];
                        drNew["InvoiceAmount"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["InvoiceAmount"];
                        drNew["CumulationInvoiceAmount"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["CumulationInvoiceAmount"];
                        drNew["EuroFurnidoInvoiceAmount"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["EuroFurnidoInvoiceAmount"];

                        if (i < ds.FactoryPayment_FactoryPayment_ReportView.Count)
                        {
                            drNew["PaymentDate"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentDate"];
                            drNew["PaymentReceiptNo"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentReceiptNo"];
                            drNew["USDAmount"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["USDAmount"];
                        }

                        

                        ds.Invoice.Rows.Add(drNew);
                    }            
                }
                else
                {
                    for (int i = 0; i < ds.FactoryPayment_FactoryPayment_ReportView.Count; i++)
                    {
                        DataRow drNew = ds.Invoice.NewRow();
                        drNew["No"] = i + 1;

                        if (i < ds.FactoryPayment_FactoryInvoice_ReportView.Count)
                        {
                            drNew["Season"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["Season"];
                            drNew["SupplierUD"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["SupplierUD"];

                            drNew["FactoryInvoiceNo"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["FactoryInvoiceNo"];
                            drNew["InvoiceDate"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["InvoiceDate"];
                            drNew["Company"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["Company"];
                            drNew["ClientUD"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["ClientUD"];
                            drNew["InvoiceAmount"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["InvoiceAmount"];
                            drNew["CumulationInvoiceAmount"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["CumulationInvoiceAmount"];
                            drNew["EuroFurnidoInvoiceAmount"] = ds.FactoryPayment_FactoryInvoice_ReportView[i]["EuroFurnidoInvoiceAmount"];
                        }

                        drNew["PaymentDate"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentDate"];
                        drNew["PaymentReceiptNo"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentReceiptNo"];
                        drNew["USDAmount"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["USDAmount"];

                        ds.Invoice.Rows.Add(drNew);
                    }         
                }

                //MODIFY Client
                foreach (var dr1 in ds.Invoice.Where(o => !o.IsFactoryInvoiceNoNull()))
                {
                    dr1.ClientUD = "";
                    foreach (var dr2 in ds.FactoryPayment_FactoryInvoiceDetail_ReportView.Where(o => !o.IsFactoryInvoiceNoNull() && o.FactoryInvoiceNo == dr1.FactoryInvoiceNo))
                    {
                        if (!dr2.IsClientUDNull() && !dr1.ClientUD.Contains(dr2.ClientUD))
                        {
                            dr1.ClientUD += dr2.ClientUD + ",";
                        }
                    }
                    if (dr1.ClientUD.Length > 2 && dr1.ClientUD.Contains(",")) dr1.ClientUD = dr1.ClientUD.Substring(0, dr1.ClientUD.Length - 1);
                }

                //invoice detail
                if (ds.FactoryPayment_FactoryInvoiceDetail_ReportView.Count >= ds.FactoryPayment_FactoryPayment_ReportView.Count)
                {
                    for (int i = 0; i < ds.FactoryPayment_FactoryInvoiceDetail_ReportView.Count; i++)
                    {
                        DataRow drNew = ds.InvoiceDetail.NewRow();
                        drNew["No"] = i + 1;
                        drNew["ProformaInvoiceNo"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ProformaInvoiceNo"];
                        drNew["ClientUD"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ClientUD"];
                        drNew["ArticleCode"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ArticleCode"];
                        drNew["Description"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["Description"];
                        drNew["ContainerNo"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ContainerNo"];
                        drNew["Quantity"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["Quantity"];
                        drNew["PurchasingPrice"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["PurchasingPrice"];
                        drNew["Amount"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["Amount"];
                        drNew["TotalAmount"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["TotalAmount"];
                        drNew["FactoryInvoiceNo"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["FactoryInvoiceNo"];
                        drNew["OrderQnt"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["OrderQnt"];
                        drNew["CostPrice"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["CostPrice"];
                        drNew["CostAmount"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["CostAmount"];

                        if (i < ds.FactoryPayment_FactoryPayment_ReportView.Count)
                        {
                            drNew["PaymentReceiptNo"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentReceiptNo"];
                            drNew["PaymentDate"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentDate"];
                            drNew["USDAmount"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["USDAmount"];
                        }
                        ds.InvoiceDetail.Rows.Add(drNew);
                    }
                }
                else
                {
                    for (int i = 0; i < ds.FactoryPayment_FactoryPayment_ReportView.Count; i++)
                    {
                        DataRow drNew = ds.InvoiceDetail.NewRow();
                        drNew["No"] = i + 1;

                        drNew["FactoryInvoiceNo"] = "-";
                        drNew["ContainerNo"] = "-";

                        if (i < ds.FactoryPayment_FactoryInvoiceDetail_ReportView.Count)
                        {
                            drNew["ProformaInvoiceNo"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ProformaInvoiceNo"];
                            drNew["ClientUD"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ClientUD"];
                            drNew["ArticleCode"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ArticleCode"];
                            drNew["Description"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["Description"];
                            drNew["ContainerNo"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["ContainerNo"];
                            drNew["Quantity"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["Quantity"];
                            drNew["PurchasingPrice"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["PurchasingPrice"];
                            drNew["Amount"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["Amount"];
                            drNew["TotalAmount"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["TotalAmount"];
                            drNew["FactoryInvoiceNo"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["FactoryInvoiceNo"];
                            drNew["OrderQnt"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["OrderQnt"];
                            drNew["CostPrice"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["CostPrice"];
                            drNew["CostAmount"] = ds.FactoryPayment_FactoryInvoiceDetail_ReportView[i]["CostAmount"];
                        }

                        drNew["PaymentReceiptNo"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentReceiptNo"];
                        drNew["PaymentDate"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["PaymentDate"];
                        drNew["USDAmount"] = ds.FactoryPayment_FactoryPayment_ReportView[i]["USDAmount"];

                        ds.InvoiceDetail.Rows.Add(drNew);
                    }
                }

                //PREPARE No AND CALCAULATE SUM
                var items =
                            from o in ds.InvoiceDetail.ToList()
                            group o by new { o.FactoryInvoiceNo, o.ContainerNo } into g
                            select new { g.Key.FactoryInvoiceNo, g.Key.ContainerNo, TotalAmount = g.Sum(o => (o.IsAmountNull() ? 0 : o.Amount)) };

                int j = 1;
                int k = 1;
                foreach (var item in items)
                {
                    k = 1;
                    foreach (var dr in ds.InvoiceDetail
                        .Where(o => o.FactoryInvoiceNo == item.FactoryInvoiceNo && o.ContainerNo == item.ContainerNo).ToArray())
                    {
                        dr.No = j;

                        if (k == 1)
                            dr.TotalAmount = item.TotalAmount;
                        else
                            dr.TotalAmount = 0;

                        k += 1;
                    }
                    j += 1;
                }
                ds.AcceptChanges();


                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "RAPEU");

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "FactoryOutStandingBalance");
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
