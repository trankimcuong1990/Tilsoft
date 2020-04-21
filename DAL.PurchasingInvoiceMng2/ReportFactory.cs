using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL.PurchasingInvoiceMng2
{
    public class ReportFactory
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private PurchasingInvoiceMngEntities CreateContext()
        {
            return new PurchasingInvoiceMngEntities(DALBase.Helper.CreateEntityConnectionString("PurchasingInvoiceMngModel"));
        }
        public string GetReportData(int iRequesterID, int purchasingInvoiceID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                //check permission on invoice
                if (fwFactory.CheckPurchasingInvoicePermission(iRequesterID, purchasingInvoiceID) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }

                int userOfficeID = fwFactory.GetUserOffice(iRequesterID);
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchasingInvoiceMng_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PurchasingInvoiceID", purchasingInvoiceID);

                adap.TableMappings.Add("Table", "PurchasingInvoiceMng_PurchasingInvoice_ReportView");
                adap.TableMappings.Add("Table1", "PurchasingInvoiceMng_PurchasingInvoiceDetail_ReportView");
                adap.TableMappings.Add("Table2", "PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_ReportView");
                adap.TableMappings.Add("Table3", "PurchasingInvoiceMng_LoadingPlan_ReportView");
                adap.TableMappings.Add("Table4", "PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_ReportView");
                adap.Fill(ds);

                //parese invoice
                ReportDataObject.InvoiceRow drInvoice = ds.Invoice.NewInvoiceRow();
                ReportDataObject.PurchasingInvoiceMng_PurchasingInvoice_ReportViewRow drOriginInvoice = ds.PurchasingInvoiceMng_PurchasingInvoice_ReportView.FirstOrDefault();

                drInvoice.InvoiceNo = drOriginInvoice.IsInvoiceNoNull() ? "" : drOriginInvoice.InvoiceNo;
                drInvoice.BLNo = drOriginInvoice.IsBLNoNull() ? "" : drOriginInvoice.BLNo;
                drInvoice.POLName = drOriginInvoice.IsPOLNameNull() ? "" : drOriginInvoice.POLName;
                drInvoice.PODName = drOriginInvoice.IsPODNameNull() ? "" : drOriginInvoice.PODName;
                drInvoice.TotalQuantity = drOriginInvoice.IsTotalQuantityNull() ? 0 : drOriginInvoice.TotalQuantity;
                drInvoice.TotalAmount = drOriginInvoice.IsTotalQuantityNull() ? 0 : drOriginInvoice.TotalAmount;
                drInvoice.SupplierNM = drOriginInvoice.IsSupplierNMNull() ? "" : drOriginInvoice.SupplierNM;
                drInvoice.Address = drOriginInvoice.IsAddressNull() ? "" : drOriginInvoice.Address;
                drInvoice.Director = drOriginInvoice.IsDirectorNull() ? "" : drOriginInvoice.Director;
                drInvoice.BankName = drOriginInvoice.IsBankNameNull() ? "" : drOriginInvoice.BankName;
                drInvoice.BankAddress = drOriginInvoice.IsBankAddressNull() ? "" : drOriginInvoice.BankAddress;
                drInvoice.BankSwiftCode = drOriginInvoice.IsBankSwiftCodeNull() ? "" : drOriginInvoice.BankSwiftCode;
                drInvoice.BankAccountNo = drOriginInvoice.IsBankAccountNoNull() ? "" : drOriginInvoice.BankAccountNo;
                drInvoice.InvoiceDate = drOriginInvoice.IsInvoiceDateNull() ? "" : drOriginInvoice.InvoiceDate;
                drInvoice.IsBuyingOrangePie = drOriginInvoice.IsIsBuyingOrangePieNull() ? 0 : drOriginInvoice.IsBuyingOrangePie;
                drInvoice.ShippedDate = drOriginInvoice.IsShippedDateNull() ? "" : drOriginInvoice.ShippedDate;
                drInvoice.Remark = drOriginInvoice.IsRemarkNull() ? "" : drOriginInvoice.Remark;
                drInvoice.TotalDeposit = drOriginInvoice.IsTotalDepositNull() ? 0 : drOriginInvoice.TotalDeposit;
                drInvoice.FSCCode = drOriginInvoice.IsFSCCodeNull() ? "" : drOriginInvoice.FSCCode;
                ds.Invoice.AddInvoiceRow(drInvoice);
                
                //parse detail data
                ReportDataObject.InvoiceDetailRow drInvoiceDetail;
                foreach (var loadingplan in ds.PurchasingInvoiceMng_LoadingPlan_ReportView)
                {
                    drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                    drInvoiceDetail.Description = loadingplan.ContainerInfo;
                    ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);

                    foreach (var product in ds.PurchasingInvoiceMng_PurchasingInvoiceDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                        drInvoiceDetail.Order_Client = product.ProformaInvoiceNo + " / " + product.ClientUD;
                        drInvoiceDetail.ArticleCode = product.ArticleCode;
                        drInvoiceDetail.Description = product.Description;
                        drInvoiceDetail.Quantity = product.IsQuantityNull() ? 0 : product.Quantity;
                        drInvoiceDetail.HSCode = product.IsHSCodeNull() ? "" : product.HSCode;
                        if (userOfficeID == 4) //user in factory
                        {
                            product.UnitPrice = 0;
                            drInvoiceDetail.UnitPrice = product.IsFactoryPriceNull() ? 0 : product.FactoryPrice;
                        }
                        else // user in eurofar 
                        {
                            if (drOriginInvoice.IsIssuesByFactory)
                            {
                                drInvoiceDetail.UnitPrice = product.IsFactoryPriceNull() ? 0 : product.FactoryPrice;
                            }
                            else if (drOriginInvoice.IsIssuesByFurnindo)
                            {
                                drInvoiceDetail.UnitPrice = product.IsUnitPriceNull() ? 0 : product.UnitPrice;
                            }
                        }
                        drInvoiceDetail.Amount = drInvoiceDetail.UnitPrice * drInvoiceDetail.Quantity;
                        ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);

                        //ArtCode, ArtName...
                        if (!string.IsNullOrEmpty(product.ClientArticleCode)) {
                            drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                            drInvoiceDetail.HSCode = "CLIENT ART CODE: ";
                            drInvoiceDetail.Description = product.ClientArticleCode;
                            ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);
                        }
                        if (!string.IsNullOrEmpty(product.ClientArticleName))
                        {
                            drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                            drInvoiceDetail.HSCode = "CLIENT ART NAME: ";
                            drInvoiceDetail.Description = product.ClientArticleName;
                            ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);
                        }
                    }

                    foreach (var sparepart in ds.PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                        drInvoiceDetail.Order_Client = sparepart.ProformaInvoiceNo + " / " + sparepart.ClientUD;
                        drInvoiceDetail.ArticleCode = sparepart.ArticleCode;
                        drInvoiceDetail.Description = sparepart.Description;
                        drInvoiceDetail.Quantity = sparepart.IsQuantityNull() ? 0 : sparepart.Quantity;

                        if (userOfficeID == 4)
                        {
                            sparepart.UnitPrice = 0;
                            drInvoiceDetail.UnitPrice = sparepart.IsFactoryPriceNull() ? 0 : sparepart.FactoryPrice;
                        }
                        else
                        {
                            if (drOriginInvoice.IsIssuesByFactory)
                            {
                                drInvoiceDetail.UnitPrice = sparepart.IsFactoryPriceNull() ? 0 : sparepart.FactoryPrice;
                            }
                            else if (drOriginInvoice.IsIssuesByFurnindo)
                            {
                                drInvoiceDetail.UnitPrice = sparepart.IsUnitPriceNull() ? 0 : sparepart.UnitPrice;
                            }
                        }
                        drInvoiceDetail.Amount = drInvoiceDetail.UnitPrice * drInvoiceDetail.Quantity;
                        ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);
                    }

                    foreach (var sample in ds.PurchasingInvoiceMng_PurchasingInvoiceSampleDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drInvoiceDetail = ds.InvoiceDetail.NewInvoiceDetailRow();
                        drInvoiceDetail.Order_Client = sample.ProformaInvoiceNo + " / " + sample.ClientUD;
                        drInvoiceDetail.ArticleCode = sample.ArticleCode;
                        drInvoiceDetail.Description = sample.Description;
                        drInvoiceDetail.Quantity = sample.IsQuantityNull() ? 0 : sample.Quantity;
                        drInvoiceDetail.HSCode = sample.IsHSCodeNull() ? "" : sample.HSCode;
                        if (userOfficeID == 4) //user in factory
                        {
                            sample.UnitPrice = 0;
                            drInvoiceDetail.UnitPrice = sample.IsFactoryPriceNull() ? 0 : sample.FactoryPrice;
                        }
                        else // user in eurofar 
                        {
                            if (drOriginInvoice.IsIssuesByFactory)
                            {
                                drInvoiceDetail.UnitPrice = sample.IsFactoryPriceNull() ? 0 : sample.FactoryPrice;
                            }
                            else if (drOriginInvoice.IsIssuesByFurnindo)
                            {
                                drInvoiceDetail.UnitPrice = sample.IsUnitPriceNull() ? 0 : sample.UnitPrice;
                            }
                        }
                        drInvoiceDetail.Amount = drInvoiceDetail.UnitPrice * drInvoiceDetail.Quantity;
                        ds.InvoiceDetail.AddInvoiceDetailRow(drInvoiceDetail);
                    }
                }
                //generate schema
                //DALBase.Helper.DevCreateReportXMLSource(ds, "PurchasingInvoice");
                // generate xml file
                string reportName = string.Empty;
                if (drOriginInvoice.IsIssuesByFactory) 
                {
                    reportName = "PurchasingInvoiceFactory";
                }
                else if (drOriginInvoice.IsIssuesByFurnindo)
                {
                    reportName = "PurchasingInvoiceFurnindo";
                }
                return DALBase.Helper.CreateReportFiles(ds, reportName);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                return string.Empty;
            }
        }

        public string ExportExactOnlineSoftware(int iRequesterID, string purchasingInvoiceIds, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            var itemPurchasingInvoice = purchasingInvoiceIds.Split(',');

            try
            {
                if (string.IsNullOrEmpty(purchasingInvoiceIds))
                {
                    throw new Exception("You need select invoice before export");
                }

                using (var context = CreateContext())
                {
                    foreach( var itemID in itemPurchasingInvoice)
                    {
                        if (itemID != "")
                        {
                            var purchasingInvoiceID = Convert.ToInt32(itemID);

                            foreach (var item in context.PurchasingInvoiceMng_PurchasingInvoiceSearch_View.Where(o => o.PurchasingInvoiceID == purchasingInvoiceID))
                            {
                                if (purchasingInvoiceID == item.PurchasingInvoiceID && item.IsConfirmed == false)
                                {
                                    throw new Exception("You need Comfirm invoice before export");
                                }
                            }
                        }
                    }
                };

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchasingInvoiceMng_function_GetExportExactOnline", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PurchasingInvoiceIDs", purchasingInvoiceIds);

                adap.TableMappings.Add("Table", "PurchasingInvoiceMng_ExportExactOnline_View");
                //adap.TableMappings.Add("Table1", "ECommercialInvoiceMng_SubInfo_View");
                adap.Fill(ds);

                //foreach (var item in ds.ECommercialInvoiceMng_ExportExactOnline_View)
                //{
                //    item.BLNo = "";
                //    string ProformaInvoiceNo = "";
                //    foreach (var dbItem in ds.ECommercialInvoiceMng_SubInfo_View.Where(o => o.ECommercialInvoiceID == item.ECommercialInvoiceID))
                //    {
                //        if (!dbItem.IsBLNoNull() && !item.BLNo.Contains(dbItem.BLNo)) item.BLNo = item.BLNo + dbItem.BLNo + " / ";
                //        if (!dbItem.IsProformaInvoiceNoNull() && !ProformaInvoiceNo.Contains(dbItem.ProformaInvoiceNo)) ProformaInvoiceNo = ProformaInvoiceNo + dbItem.ProformaInvoiceNo + " / ";
                //    }
                //    if (item.BLNo.Length >= 2) item.BLNo = item.BLNo.Substring(0, item.BLNo.Length - 2);
                //    if (ProformaInvoiceNo.Length >= 2)
                //    {
                //        ProformaInvoiceNo = ProformaInvoiceNo.Substring(0, ProformaInvoiceNo.Length - 2);
                //        item.DefinitionHeader = item.DefinitionHeader + " - " + ProformaInvoiceNo;
                //        item.Definition = item.Definition + " - " + ProformaInvoiceNo;
                //    }
                //}

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ExportExactOnlinePurchasingInvoice");

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "ExportExactOnlinePurchasingInvoice");
                //return Library.Helper.CreateCOMReportFile(ds, "ExportExactOnlinePurchasingInvoice");
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

        public object GetPrintoutData(int userID, int purchasingInvoiceID, int optionID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                PurchasingInvoiceObject ds = new PurchasingInvoiceObject();

                // Checking permission
                if (fwFactory.CheckPurchasingInvoicePermission(userID, purchasingInvoiceID) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchasingInvoiceMng_function_GetDataPrintout", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PurchasingInvoiceID", purchasingInvoiceID);

                adap.TableMappings.Add("Table", "CommercialInvoice");
                adap.TableMappings.Add("Table1", "CommercialInvoiceDetail");
                adap.TableMappings.Add("Table2", "SaleOrderDetail");
                adap.TableMappings.Add("Table3", "PackingListDetail");
                adap.Fill(ds);

                foreach (var item in ds.CommercialInvoice)
                {
                    item.OptionID = optionID;
                }

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ShipmentPurchasing");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return null;
            }
        }
    }
}
