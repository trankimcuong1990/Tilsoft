using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Module.PurchasingCreditNote.DAL
{
    public class ReportFactory
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public string GetReportData(int userId, int purchasingCreditNoteID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                //check permission on booking
                if (fwFactory.CheckPurchasingCreditNotePermission(userId, purchasingCreditNoteID) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }
                
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchasingCreditNoteMng_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PurchasingCreditNoteID", purchasingCreditNoteID);

                adap.TableMappings.Add("Table", "PurchasingCreditNoteMng_PurchasingCreditNote_ReportView");
                adap.TableMappings.Add("Table1", "PurchasingCreditNoteMng_PurchasingCreditNoteDetail_ReportView");
                adap.TableMappings.Add("Table2", "PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_ReportView");
                adap.TableMappings.Add("Table3", "PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_ReportView");
                adap.TableMappings.Add("Table4", "PurchasingCreditNoteMng_LoadingPlan_ReportView");
                adap.Fill(ds);

                //parese invoice
                ReportDataObject.InvoiceRow drInvoice = ds.Invoice.NewInvoiceRow();
                ReportDataObject.PurchasingCreditNoteMng_PurchasingCreditNote_ReportViewRow drOriginInvoice = ds.PurchasingCreditNoteMng_PurchasingCreditNote_ReportView.FirstOrDefault();

                drInvoice.InvoiceNo = drOriginInvoice.CreditNoteNo;
                drInvoice.BLNo = drOriginInvoice.IsBLNoNull() ? "" : drOriginInvoice.BLNo;
                drInvoice.POLName = drOriginInvoice.IsPOLNameNull() ? "" : drOriginInvoice.POLName;
                drInvoice.PODName = drOriginInvoice.IsPODNameNull() ? "" : drOriginInvoice.PODName;
                drInvoice.TotalQuantity = drOriginInvoice.IsTotalQuantityNull() ? 0 : drOriginInvoice.TotalQuantity;
                drInvoice.TotalAmount = drOriginInvoice.IsTotalQuantityNull() ? 0 : drOriginInvoice.TotalAmount;
                drInvoice.SupplierNM = drOriginInvoice.IsSupplierNMNull() ? "" : drOriginInvoice.SupplierNM;
                drInvoice.Address = drOriginInvoice.IsAddressNull() ? "" : drOriginInvoice.Address;
                drInvoice.Director = drOriginInvoice.IsDirectorNull() ? "" : drOriginInvoice.Director;

                ds.Invoice.AddInvoiceRow(drInvoice);
                
                //parse detail data
                ReportDataObject.InvoiceDetailRow drInvoiceDetail;
                foreach (var loadingplan in ds.PurchasingCreditNoteMng_LoadingPlan_ReportView)
                {
                    drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                    drInvoiceDetail.Description = loadingplan.ContainerInfo;
                    ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);

                    foreach (var product in ds.PurchasingCreditNoteMng_PurchasingCreditNoteDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                        drInvoiceDetail.Order_Client = product.ProformaInvoiceNo + " / " + product.ClientUD;
                        drInvoiceDetail.ArticleCode = product.ArticleCode;
                        drInvoiceDetail.Description = product.Description;
                        drInvoiceDetail.Quantity = product.IsQuantityNull() ? 0 : product.Quantity;
                        drInvoiceDetail.UnitPrice = product.IsUnitPriceNull() ? 0 : product.UnitPrice;
                        drInvoiceDetail.Amount = product.IsAmountNull() ? 0 : product.Amount;

                        ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);
                    }

                    foreach (var sparepart in ds.PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                        drInvoiceDetail.Order_Client = sparepart.ProformaInvoiceNo + " / " + sparepart.ClientUD;
                        drInvoiceDetail.ArticleCode = sparepart.ArticleCode;
                        drInvoiceDetail.Description = sparepart.Description;
                        drInvoiceDetail.Quantity = sparepart.IsQuantityNull() ? 0 : sparepart.Quantity;
                        drInvoiceDetail.UnitPrice = sparepart.IsUnitPriceNull() ? 0 : sparepart.UnitPrice;
                        drInvoiceDetail.Amount = sparepart.IsAmountNull() ? 0 : sparepart.Amount;

                        ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);
                    }
                }

                foreach (var item in ds.PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_ReportView)
                {
                    drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                    drInvoiceDetail.Order_Client = "";
                    drInvoiceDetail.ArticleCode = "";
                    drInvoiceDetail.Description = item.Description;
                    drInvoiceDetail.Quantity = 0;
                    drInvoiceDetail.UnitPrice = 0;
                    drInvoiceDetail.Amount = item.IsAmountNull() ? 0 : item.Amount;

                    ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);
                }
                //generate schema
                //Library.Helper.DevCreateReportXMLSource(ds, "PurchasingCreditNote");
                // generate xml file
                string reportName = string.Empty;
                if (drOriginInvoice.IsIssuesByFactory) reportName = "PurchasingCreditNoteFactory";
                else if (drOriginInvoice.IsIssuesByFurnindo)
                {
                    reportName = "PurchasingCreditNoteFurnindo";
                }

                return Library.Helper.CreateReportFiles(ds, reportName);
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
