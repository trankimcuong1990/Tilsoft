using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL.PackingListMng
{
    public class ReportFactory
    {
        public string GetReportData(int packingListID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PackingListMng_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PackingListID", packingListID);

                adap.TableMappings.Add("Table", "PackingListMng_PackingList_ReportView");
                adap.TableMappings.Add("Table1", "PackingListMng_PackingListDetail_ReportView");
                adap.TableMappings.Add("Table2", "PackingListMng_PackingListSparepartDetail_ReportView");
                adap.TableMappings.Add("Table3", "PackingListMng_LoadingPlan_ReportView");
                adap.TableMappings.Add("Table4", "PackingListMng_PackingListSampleDetail_ReportView");
                adap.Fill(ds);

                //parese invoice
                ReportDataObject.PackingListRow drPackingList = ds.PackingList.NewPackingListRow();
                ReportDataObject.PackingListMng_PackingList_ReportViewRow drOrigin = ds.PackingListMng_PackingList_ReportView.FirstOrDefault();

                drPackingList.InvoiceNo = drOrigin.InvoiceNo;
                drPackingList.BLNo = drOrigin.IsBLNoNull() ? "" : drOrigin.BLNo;
                drPackingList.POLName = drOrigin.IsPOLNameNull() ? "" : drOrigin.POLName;
                drPackingList.PODName = drOrigin.IsPODNameNull() ? "" : drOrigin.PODName;
                drPackingList.TotalQuantityShipped = drOrigin.IsTotalQuantityShippedNull() ? 0 : drOrigin.TotalQuantityShipped;
                drPackingList.TotalPKGs = drOrigin.IsTotalPKGsNull() ? 0 : drOrigin.TotalPKGs;
                drPackingList.TotalNettWeight = drOrigin.IsTotalNettWeightNull() ? 0 : drOrigin.TotalNettWeight;
                drPackingList.TotalKGs = drOrigin.IsTotalKGsNull() ? 0 : drOrigin.TotalKGs;
                drPackingList.TotalCBMs = drOrigin.IsTotalCBMsNull() ? 0 : drOrigin.TotalCBMs;
                drPackingList.ShippedDate = drOrigin.IsShipedDateNull() ? "" : drOrigin.ShipedDate.ToString("dd/MM/yyyy");
                drPackingList.SupplierNM = drOrigin.IsSupplierNMNull() ? "" : drOrigin.SupplierNM;
                drPackingList.Address = drOrigin.IsAddressNull() ? "" : drOrigin.Address;
                drPackingList.InvoiceDate = drOrigin.IsInvoiceDateNull() ? "" : drOrigin.InvoiceDate.ToString("dd/MM/yyyy");
                drPackingList.FSCCode = drOrigin.IsFSCCodeNull() ? "" : drOrigin.FSCCode;
                ds.PackingList.AddPackingListRow(drPackingList);

                //parse detail data
                ReportDataObject.PackingListDetailRow drPackingListDetail;
                foreach (var loadingplan in ds.PackingListMng_LoadingPlan_ReportView)
                {
                    drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                    drPackingListDetail.Description = loadingplan.ContainerInfo;
                    ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                    foreach (var product in ds.PackingListMng_PackingListDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.Order_Client = product.ProformaInvoiceNo + " / " + product.ClientUD;
                        drPackingListDetail.ArticleCode = product.ArticleCode;
                        drPackingListDetail.Description = product.Description;
                        drPackingListDetail.QuantityShipped = product.IsQuantityShippedNull() ? 0 : product.QuantityShipped;
                        drPackingListDetail.PKGs = product.IsPKGsNull() ? 0 : product.PKGs;
                        drPackingListDetail.NettWeight = product.IsNettWeightNull() ? 0 : product.NettWeight;
                        drPackingListDetail.KGs = product.IsKGsNull() ? 0 : product.KGs;
                        drPackingListDetail.CBMs = product.IsCBMsNull() ? 0 : product.CBMs;
                        drPackingListDetail.HSCode = product.IsHSCodeNull() ? "" : product.HSCode;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                        //ArtCode, ArtName...
                        if (!string.IsNullOrEmpty(product.ClientArticleCode))
                        {
                            drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                            drPackingListDetail.HSCode = "CLIENT ART CODE: ";
                            drPackingListDetail.Description = product.ClientArticleCode;
                            ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                        }
                        if (!string.IsNullOrEmpty(product.ClientArticleName))
                        {
                            drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                            drPackingListDetail.HSCode = "CLIENT ART NAME: ";
                            drPackingListDetail.Description = product.ClientArticleName;
                            ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                        }
                    }

                    foreach (var sparepart in ds.PackingListMng_PackingListSparepartDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.Order_Client = sparepart.ProformaInvoiceNo + " / " + sparepart.ClientUD;
                        drPackingListDetail.ArticleCode = sparepart.ArticleCode;
                        drPackingListDetail.Description = sparepart.Description;
                        drPackingListDetail.QuantityShipped = sparepart.IsQuantityShippedNull() ? 0 : sparepart.QuantityShipped;
                        drPackingListDetail.PKGs = sparepart.IsPKGsNull() ? 0 : sparepart.PKGs;
                        drPackingListDetail.NettWeight = sparepart.IsNettWeightNull() ? 0 : sparepart.NettWeight;
                        drPackingListDetail.KGs = sparepart.IsKGsNull() ? 0 : sparepart.KGs;
                        drPackingListDetail.CBMs = sparepart.IsCBMsNull() ? 0 : sparepart.CBMs;
                        drPackingListDetail.HSCode = sparepart.IsHSCodeNull() ? "" : sparepart.HSCode;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }

                    foreach (var sample in ds.PackingListMng_PackingListSampleDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.Order_Client = sample.ProformaInvoiceNo + " / " + sample.ClientUD;
                        drPackingListDetail.ArticleCode = sample.ArticleCode;
                        drPackingListDetail.Description = sample.Description;
                        drPackingListDetail.QuantityShipped = sample.IsQuantityShippedNull() ? 0 : sample.QuantityShipped;
                        drPackingListDetail.PKGs = sample.IsPKGsNull() ? 0 : sample.PKGs;
                        drPackingListDetail.NettWeight = sample.IsNettWeightNull() ? 0 : sample.NettWeight;
                        drPackingListDetail.KGs = sample.IsKGsNull() ? 0 : sample.KGs;
                        drPackingListDetail.CBMs = sample.IsCBMsNull() ? 0 : sample.CBMs;
                        drPackingListDetail.HSCode = sample.IsHSCodeNull() ? "" : sample.HSCode;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }
                }
                //generate schema
                //DALBase.Helper.DevCreateReportXMLSource(ds, "PackingList");
                // generate xml file
                string reportFile = DALBase.Helper.CreateReportFiles(ds, "PackingList");
                return reportFile = reportFile + ".xlsm";

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                return string.Empty;
            }
        }

        public string GetOrangePiePrintout(int packingListID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OrangePiePrintoutDataObject ds = new OrangePiePrintoutDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PackingListMng_function_GetOrangePiePrintout", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PackingListID", packingListID);

                adap.TableMappings.Add("Table", "OrangePie_PackingList");
                adap.TableMappings.Add("Table1", "OrangePie_Container");
                adap.TableMappings.Add("Table2", "OrangePie_Goods");
                adap.TableMappings.Add("Table3", "OrangePie_GoodsDescription");
                adap.Fill(ds);

                string ClientOrderNos = "";
                string CustomerOrderNos = "";
                foreach (var item in ds.OrangePie_Goods)
                {
                    if (!string.IsNullOrEmpty(item.ProformaInvoiceNo) && !ClientOrderNos.Contains(item.ProformaInvoiceNo))
                    {
                        ClientOrderNos += item.ProformaInvoiceNo + ", ";
                    }
                    if (!string.IsNullOrEmpty(item.ClientOrderNumber) && !CustomerOrderNos.Contains(item.ClientOrderNumber))
                    {
                        CustomerOrderNos += item.ClientOrderNumber + ", ";
                    }
                }
                ds.OrangePie_PackingList.FirstOrDefault().ClientOrderNos = ClientOrderNos;
                ds.OrangePie_PackingList.FirstOrDefault().CustomerOrderNos = CustomerOrderNos;

                OrangePiePrintoutDataObject.PackingListRow drPackingList = ds.PackingList.NewPackingListRow();
                OrangePiePrintoutDataObject.OrangePie_PackingListRow drOrigin = ds.OrangePie_PackingList.FirstOrDefault();
                drPackingList.SupplierNM = drOrigin.IsSupplierNMNull() ? "" : drOrigin.SupplierNM;
                drPackingList.Address = drOrigin.IsAddressNull() ? "" : drOrigin.Address;
                drPackingList.ConsigneeInfo = drOrigin.IsConsigneeInfoNull() ? "" : drOrigin.ConsigneeInfo;
                drPackingList.NotifyParty = drOrigin.IsNotifyPartyNull() ? "" : drOrigin.NotifyParty;
                drPackingList.PackingListUD = drOrigin.IsPackingListUDNull() ? "" : drOrigin.PackingListUD;
                drPackingList.PackingListDate = drOrigin.IsPackingListDateNull() ? "" : drOrigin.PackingListDate;
                drPackingList.ClientOrderNos = drOrigin.IsClientOrderNosNull() ? "" : drOrigin.ClientOrderNos;
                drPackingList.CustomerOrderNos = drOrigin.IsCustomerOrderNosNull() ? "" : drOrigin.CustomerOrderNos;
                drPackingList.ForwarderNM = drOrigin.IsForwarderNMNull() ? "" : drOrigin.ForwarderNM;
                drPackingList.ShipedDate = drOrigin.IsShipedDateNull() ? "" : drOrigin.ShipedDate;
                drPackingList.POLName = drOrigin.IsPOLNameNull() ? "" : drOrigin.POLName;
                drPackingList.PODName = drOrigin.IsPODNameNull() ? "" : drOrigin.PODName;
                drPackingList.Vessel = drOrigin.IsVesselNull() ? "" : drOrigin.Vessel;
                drPackingList.BLNo = drOrigin.IsBLNoNull() ? "" : drOrigin.BLNo;
                drPackingList.TotalQuantityShipped = drOrigin.IsTotalQuantityShippedNull() ? 0 : drOrigin.TotalQuantityShipped;
                drPackingList.TotalPKGs = drOrigin.IsTotalPKGsNull() ? 0 : drOrigin.TotalPKGs;
                drPackingList.TotalNettWeight = drOrigin.IsTotalNettWeightNull() ? 0 : drOrigin.TotalNettWeight;
                drPackingList.TotalKGs = drOrigin.IsTotalPKGsNull() ? 0 : drOrigin.TotalKGs;
                drPackingList.TotalCBMs = drOrigin.IsTotalCBMsNull() ? 0 : drOrigin.TotalCBMs;
                drPackingList.FSCCode = drOrigin.IsFSCCodeNull() ? "" : drOrigin.FSCCode;
                ds.PackingList.AddPackingListRow(drPackingList);

                //parse detail data
                OrangePiePrintoutDataObject.PackingListDetailRow drPackingListDetail;

                foreach (var container in ds.OrangePie_Container)
                {
                    drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                    drPackingListDetail.Description = "1x" + (container.IsContainerTypeNMNull() ? "" : container.ContainerTypeNM) + " CONTAINER / CONT.NO: " + (container.IsContainerNoNull() ? "" : container.ContainerNo) + " / SEAL NO: " + (container.IsSealNoNull() ? "" : container.SealNo);
                    ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    foreach (var product in ds.OrangePie_Goods.Where(o => o.ContainerNo == container.ContainerNo))
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.ArticleCode = product.IsArticleCodeNull() ? "" : product.ArticleCode;
                        drPackingListDetail.Description = product.IsDescriptionNull() ? "" : product.Description;
                        drPackingListDetail.QuantityShipped = product.IsQuantityShippedNull() ? 0 : product.QuantityShipped;
                        drPackingListDetail.PKGs = product.IsPKGsNull() ? 0 : product.PKGs;
                        drPackingListDetail.NettWeight = product.IsNettWeightNull() ? 0 : product.NettWeight;
                        drPackingListDetail.KGs = product.IsKGsNull() ? 0 : product.KGs;
                        drPackingListDetail.CBMs = product.IsCBMsNull() ? 0 : product.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }
                }

                //generate schema
                //DALBase.Helper.DevCreateReportXMLSource(ds, "PackingList");
                // generate xml file
                string reportFile = DALBase.Helper.CreateReportFiles(ds, "PackingList_OrangePine");
                return reportFile = reportFile + ".xlsm";
                //return Library.Helper.CreateReportFileWithEPPlus2(ds, "PackingList_OrangePine");
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

        // report By Container
        public string GetReportDataByContainer(int packingListID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PackingListMng_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PackingListID", packingListID);

                adap.TableMappings.Add("Table", "PackingListMng_PackingList_ReportView");
                adap.TableMappings.Add("Table1", "PackingListMng_PackingListDetail_ReportView");
                adap.TableMappings.Add("Table2", "PackingListMng_PackingListSparepartDetail_ReportView");
                adap.TableMappings.Add("Table3", "PackingListMng_LoadingPlan_ReportView");
                adap.Fill(ds);

                //parese invoice
                ReportDataObject.PackingListRow drPackingList = ds.PackingList.NewPackingListRow();
                ReportDataObject.PackingListMng_PackingList_ReportViewRow drOrigin = ds.PackingListMng_PackingList_ReportView.FirstOrDefault();

                drPackingList.InvoiceNo = drOrigin.InvoiceNo;
                drPackingList.BLNo = drOrigin.IsBLNoNull() ? "" : drOrigin.BLNo;
                drPackingList.POLName = drOrigin.IsPOLNameNull() ? "" : drOrigin.POLName;
                drPackingList.PODName = drOrigin.IsPODNameNull() ? "" : drOrigin.PODName;
                drPackingList.TotalQuantityShipped = drOrigin.IsTotalQuantityShippedNull() ? 0 : drOrigin.TotalQuantityShipped;
                drPackingList.TotalPKGs = drOrigin.IsTotalPKGsNull() ? 0 : drOrigin.TotalPKGs;
                drPackingList.TotalNettWeight = drOrigin.IsTotalNettWeightNull() ? 0 : drOrigin.TotalNettWeight;
                drPackingList.TotalKGs = drOrigin.IsTotalKGsNull() ? 0 : drOrigin.TotalKGs;
                drPackingList.TotalCBMs = drOrigin.IsTotalCBMsNull() ? 0 : drOrigin.TotalCBMs;
                ds.PackingList.AddPackingListRow(drPackingList);

                //parse detail data
                ReportDataObject.PackingListDetailRow drPackingListDetail;
                foreach (var loadingplan in ds.PackingListMng_LoadingPlan_ReportView)
                {
                    drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                    drPackingListDetail.Description = loadingplan.ContainerInfo;
                    ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                    foreach (var product in ds.PackingListMng_PackingListDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.Order_Client = product.ProformaInvoiceNo + " / " + product.ClientUD;
                        drPackingListDetail.ArticleCode = product.ArticleCode;
                        drPackingListDetail.Description = product.Description;
                        drPackingListDetail.QuantityShipped = product.IsQuantityShippedNull() ? 0 : product.QuantityShipped;
                        drPackingListDetail.PKGs = product.IsPKGsNull() ? 0 : product.PKGs;
                        drPackingListDetail.NettWeight = product.IsNettWeightNull() ? 0 : product.NettWeight;
                        drPackingListDetail.KGs = product.IsKGsNull() ? 0 : product.KGs;
                        drPackingListDetail.CBMs = product.IsCBMsNull() ? 0 : product.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }

                    foreach (var sparepart in ds.PackingListMng_PackingListSparepartDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.Order_Client = sparepart.ProformaInvoiceNo + " / " + sparepart.ClientUD;
                        drPackingListDetail.ArticleCode = sparepart.ArticleCode;
                        drPackingListDetail.Description = sparepart.Description;
                        drPackingListDetail.QuantityShipped = sparepart.IsQuantityShippedNull() ? 0 : sparepart.QuantityShipped;
                        drPackingListDetail.PKGs = sparepart.IsPKGsNull() ? 0 : sparepart.PKGs;
                        drPackingListDetail.NettWeight = sparepart.IsNettWeightNull() ? 0 : sparepart.NettWeight;
                        drPackingListDetail.KGs = sparepart.IsKGsNull() ? 0 : sparepart.KGs;
                        drPackingListDetail.CBMs = sparepart.IsCBMsNull() ? 0 : sparepart.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }
                }

                return Library.Helper.CreateReportFileWithEPPlus(ds, "PackingListByContainer");
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return string.Empty;
            }
        }

        public string PrintPackingListPDF(int packingListID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //OrangePiePrintoutDataObject ds = new OrangePiePrintoutDataObject();
            PackingListDataSet ds = new PackingListDataSet();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PackingListMng_function_GetOrangePiePrintout", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PackingListID", packingListID);

                adap.TableMappings.Add("Table", "OrangePie_PackingList");
                adap.TableMappings.Add("Table1", "OrangePie_Container");
                adap.TableMappings.Add("Table2", "OrangePie_Goods");
                adap.TableMappings.Add("Table3", "OrangePie_GoodsDescription");

                adap.Fill(ds);

                string ClientOrderNos = "";
                string CustomerOrderNos = "";
                for (int i = 0; i < ds.Tables["OrangePie_Goods"].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ds.Tables["OrangePie_Goods"].Rows[i]["ProformaInvoiceNo"].ToString()) && !ClientOrderNos.Contains(ds.Tables["OrangePie_Goods"].Rows[i]["ProformaInvoiceNo"].ToString()))
                    {
                        ClientOrderNos += ds.Tables["OrangePie_Goods"].Rows[i]["ProformaInvoiceNo"].ToString() + ", ";
                    }
                    if (!string.IsNullOrEmpty(ds.Tables["OrangePie_Goods"].Rows[i]["ClientOrderNumber"].ToString()) && !CustomerOrderNos.Contains(ds.Tables["OrangePie_Goods"].Rows[i]["ClientOrderNumber"].ToString()))
                    {
                        CustomerOrderNos += ds.Tables["OrangePie_Goods"].Rows[i]["ClientOrderNumber"].ToString() + ", ";
                    }
                }
                ds.OrangePie_PackingList.ClientOrderNosColumn.ReadOnly = false;
                ds.OrangePie_PackingList.CustomerOrderNosColumn.ReadOnly = false;
                ds.OrangePie_PackingList.FirstOrDefault().ClientOrderNos = ClientOrderNos;
                ds.OrangePie_PackingList.FirstOrDefault().CustomerOrderNos = CustomerOrderNos;

                DataTable Table4 = new DataTable("OrangePie_PackingListDetail");
                Table4.Columns.Add("ArticleCode", typeof(string));
                Table4.Columns.Add("Description", typeof(string));
                Table4.Columns.Add("TotalQty", typeof(string));
                Table4.Columns.Add("TotalCtns", typeof(string));
                Table4.Columns.Add("NetWeight", typeof(string));
                Table4.Columns.Add("GrossWeight", typeof(string));
                Table4.Columns.Add("Measurement", typeof(string));
                int totalQty = 0, totalCtns = 0;
                decimal totalNettWeight = 0, totalGrossWeight = 0, totalMeasurement = 0;
                foreach (DataRow contRow in ds.Tables["OrangePie_Container"].Rows)
                {
                    Table4.Rows.Add("", "1x" + contRow["ContainerTypeNM"] + " CONTAINER / CONT.NO: " + contRow["ContainerNo"] + " / SEAL NO: " + contRow["SealNo"], "", "", "", "", "");
                    foreach (DataRow goodRow in ds.Tables["OrangePie_Goods"].Rows)
                    {
                        if (goodRow["ContainerNo"].ToString() == contRow["ContainerNo"].ToString())
                        {
                            totalQty += Convert.ToInt32(goodRow["QuantityShipped"]);
                            totalCtns += Convert.ToInt32(goodRow["PKGs"]);
                            totalNettWeight += Convert.ToDecimal(goodRow["NettWeight"]);
                            totalGrossWeight += Convert.ToDecimal(goodRow["KGs"]);
                            totalMeasurement += Convert.ToDecimal(goodRow["CBMs"]);
                            Table4.Rows.Add(goodRow["ArticleCode"], goodRow["Description"], goodRow["QuantityShipped"], goodRow["PKGs"], goodRow["NettWeight"], goodRow["KGs"], goodRow["CBMs"]);
                            foreach (DataRow goodDesRow in ds.Tables["OrangePie_GoodsDescription"].Rows)
                            {
                                if (goodDesRow["GoodsID"].ToString() == goodRow["GoodsID"].ToString())
                                {
                                    Table4.Rows.Add("", goodDesRow["Description"], "", "", "", "", "");
                                }
                            }
                        }
                    }
                }
                //DataTable Table5 = new DataTable("OrangePie_PackingListTotal");
                //Table5.Columns.Add("TotalQuantity", typeof(string));
                //Table5.Columns.Add("TotalPKGs", typeof(string));
                //Table5.Columns.Add("TotalNetWeight", typeof(string));
                //Table5.Columns.Add("TotalGrossWeight", typeof(string));
                //Table5.Columns.Add("TotalMeasurement", typeof(string));

                //Table5.Rows.Add(totalQty, totalCtns, totalNettWeight, totalGrossWeight, totalMeasurement);

                ds.Tables.Add(Table4);
                //ds.Tables.Add(Table5);
                //return "";
                return Library.Helper.CreateReceiptPrintout(ds.Tables["OrangePie_PackingList"], ds.Tables["OrangePie_PackingListDetail"], "PackingListPrint_OrangePinev2.rdlc");
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

        public string GetOrangePineReportDataByContainer(int packingListID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OrangePiePrintoutDataObject ds = new OrangePiePrintoutDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PackingListMng_function_GetOrangePiePrintout", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PackingListID", packingListID);

                adap.TableMappings.Add("Table", "OrangePie_PackingList");
                adap.TableMappings.Add("Table1", "OrangePie_Container");
                adap.TableMappings.Add("Table2", "OrangePie_Goods");
                adap.TableMappings.Add("Table3", "OrangePie_GoodsDescription");
                adap.Fill(ds);

                string ClientOrderNos = "";
                string CustomerOrderNos = "";
                foreach (var item in ds.OrangePie_Goods)
                {
                    if (!string.IsNullOrEmpty(item.ProformaInvoiceNo) && !ClientOrderNos.Contains(item.ProformaInvoiceNo))
                    {
                        ClientOrderNos += item.ProformaInvoiceNo + ", ";
                    }
                    if (!string.IsNullOrEmpty(item.ClientOrderNumber) && !CustomerOrderNos.Contains(item.ClientOrderNumber))
                    {
                        CustomerOrderNos += item.ClientOrderNumber + ", ";
                    }
                }
                ds.OrangePie_PackingList.FirstOrDefault().ClientOrderNos = ClientOrderNos;
                ds.OrangePie_PackingList.FirstOrDefault().CustomerOrderNos = CustomerOrderNos;
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "PackingList_OrangePine_ByContainer");
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
