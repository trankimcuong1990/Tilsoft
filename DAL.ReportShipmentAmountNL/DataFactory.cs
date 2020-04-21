using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.ReportShipmentAmount
{
    public class DataFactory
    {
        public string GetShipmentAmountNL(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get report success" };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Report_function_GetShipmentAmountNL", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);

                adap.TableMappings.Add("Table", "Report_ShipmentAmountNL_View");
                adap.TableMappings.Add("Table1", "Report_ShipmentAmountNL_Detail_View");
                adap.TableMappings.Add("Table2", "Report_ShipmentAmountNL_Support_View");
                adap.TableMappings.Add("Table3", "Report_ShipmentAmountVN_Support_View");
                
                adap.Fill(ds);


                //modify some columns is number
                int i;
                var PurchasingInvoice = from p in ds.Report_ShipmentAmountNL_View.Where(o => !o.IsPurchasingInvoiceIDNull() && !o.IsIsPurchasingCreditNoteNull()) group p by new { p.PurchasingInvoiceID, p.IsPurchasingCreditNote } into g select new { g.Key.PurchasingInvoiceID, g.Key.IsPurchasingCreditNote};
                foreach (var pItem in PurchasingInvoice)
                {
                    i = 1;
                    foreach (var rNLItem in ds.Report_ShipmentAmountNL_View.Where(o => !o.IsPurchasingInvoiceIDNull() && o.PurchasingInvoiceID == pItem.PurchasingInvoiceID && !o.IsIsPurchasingCreditNoteNull() && o.IsPurchasingCreditNote == pItem.IsPurchasingCreditNote))
                    {
                        if (i > 1)
                        {
                            rNLItem.FactoryInvoiceAmount = 0;
                            rNLItem.PurchasingInvoiceAmount = 0;
                            rNLItem.DeltaAmount = 0;
                            rNLItem.DeltaAmountPercent = 0;
                            rNLItem.DeltaAmountOnBooking = 0;
                            rNLItem.DeltaAmountPercentOnBooking = 0;
                        }
                        if (rNLItem.IsPurchasingCreditNote)
                        {
                            rNLItem.ECommercialInvoiceAmountInUSDPerCont = 0;
                            rNLItem.ECommercialInvoiceAmountInEURPerCont = 0;
                            rNLItem.ECommercialInvoiceAmountEURPerCont = 0;
                        }
                        i++;
                    }
                }

                var ECommercialInvoice = from p in ds.Report_ShipmentAmountNL_View.Where(o => !o.IsECommercialInvoiceIDNull()) group p by new { p.ECommercialInvoiceID } into g select new { g.Key.ECommercialInvoiceID };
                foreach (var eItem in ECommercialInvoice)
                {
                    i = 1;
                    foreach (var rNLItem in ds.Report_ShipmentAmountNL_View.Where(o => !o.IsECommercialInvoiceIDNull() && o.ECommercialInvoiceID == eItem.ECommercialInvoiceID))
                    {
                        if (i > 1)
                        {
                            rNLItem.EVATAmountEUR = 0;
                            rNLItem.ECommercialInvoiceDiscountAmountEUR = 0;
                            rNLItem.ECommercialInvoiceSeaFreightAmountEUR = 0;
                            rNLItem.ECommercialInvoiceTruckingAmountEUR = 0;
                            rNLItem.ECommercialInvoiceOtherAmountEUR = 0;
                            rNLItem.ECommercialInvoiceNetAmountInUSD = 0;
                            rNLItem.ECommercialInvoiceNetAmountInEUR = 0;
                            rNLItem.ECommercialInvoiceNetAmountEUR = 0;
                            rNLItem.ECommercialInvoiceTotalAmountInUSD = 0;
                            rNLItem.ECommercialInvoiceTotalAmountInEUR = 0;
                            rNLItem.ECommercialInvoiceTotalAmountEUR = 0;
                        }
                        i++;
                    }
                }

                //modify colum PO, Client
                foreach (var rNLItem in ds.Report_ShipmentAmountNL_View.Where(o => !o.IsECommercialInvoiceIDNull() || !o.IsPurchasingInvoiceIDNull()))
                {
                    if (!rNLItem.IsECommercialInvoiceIDNull()) //take info from eurofar invoice
                    {
                        rNLItem.ProformaInvoiceNos = "";
                        foreach (var sNLItem in ds.Report_ShipmentAmountNL_Support_View.Where(o => o.ECommercialInvoiceID == rNLItem.ECommercialInvoiceID))
                        {
                            if (!rNLItem.ProformaInvoiceNos.Contains(sNLItem.ProformaInvoiceNo)) rNLItem.ProformaInvoiceNos += sNLItem.ProformaInvoiceNo + " / ";
                        }
                        if (rNLItem.ProformaInvoiceNos.Length > 2) rNLItem.ProformaInvoiceNos = rNLItem.ProformaInvoiceNos.Substring(0, rNLItem.ProformaInvoiceNos.Length - 2);
                    }
                    else if (!rNLItem.IsPurchasingInvoiceIDNull()) // take info from purchasing invoice
                    {
                        rNLItem.ProformaInvoiceNos = "";
                        rNLItem.ClientUDs = "";
                        rNLItem.ClientNMs = "";
                        foreach (var sVNItem in ds.Report_ShipmentAmountVN_Support_View.Where(o => o.PurchasingInvoiceID == rNLItem.PurchasingInvoiceID))
                        {
                            if (!rNLItem.ProformaInvoiceNos.Contains(sVNItem.ProformaInvoiceNo)) rNLItem.ProformaInvoiceNos += sVNItem.ProformaInvoiceNo + " / ";
                            if (!rNLItem.ClientUDs.Contains(sVNItem.ClientUD)) rNLItem.ClientUDs += sVNItem.ClientUD + " / ";
                            if (!rNLItem.ClientNMs.Contains(sVNItem.ClientNM)) rNLItem.ClientNMs += sVNItem.ClientNM + "\n";
                        }
                        if (rNLItem.ProformaInvoiceNos.Length > 2) rNLItem.ProformaInvoiceNos = rNLItem.ProformaInvoiceNos.Substring(0, rNLItem.ProformaInvoiceNos.Length - 2);
                        if (rNLItem.ClientUDs.Length > 2) rNLItem.ClientUDs = rNLItem.ClientUDs.Substring(0, rNLItem.ClientUDs.Length - 2);
                        if (rNLItem.ClientNMs.Length > 2) rNLItem.ClientNMs = rNLItem.ClientNMs.Substring(0, rNLItem.ClientNMs.Length - 2);
                    }
                }

                //modify some columns is number in sheet detail
                var VN_Detail = from p in ds.Report_ShipmentAmountNL_Detail_View.Where(o => !o.IsGoodsIDNull() && !o.IsGoodsTypeNull() && !o.IsIsPurchasingCreditNoteNull()) group p by new { p.GoodsID, p.GoodsType, p.IsPurchasingCreditNote} into g select new { g.Key.GoodsID, g.Key.GoodsType, g.Key.IsPurchasingCreditNote};
                foreach (var rVNDetailItem in VN_Detail)
                {
                    i = 1;
                    foreach (var rNLDetailItem in ds.Report_ShipmentAmountNL_Detail_View.Where(o => !o.IsGoodsIDNull() && !o.IsGoodsTypeNull () && !o.IsIsPurchasingCreditNoteNull() && o.GoodsID == rVNDetailItem.GoodsID && o.GoodsType == rVNDetailItem.GoodsType && o.IsPurchasingCreditNote == rVNDetailItem.IsPurchasingCreditNote))
                    {
                        if (i > 1)
                        {
                            rNLDetailItem.FactoryInvoiceAmount = 0;
                            rNLDetailItem.PurchasingInvoiceAmount = 0;
                            rNLDetailItem.DeltaAmount = 0;
                            rNLDetailItem.DeltaAmountPercent = 0;
                        }
                        if (rNLDetailItem.IsPurchasingCreditNote)
                        {
                            rNLDetailItem.ECommercialInvoiceAmountInUSD = 0;
                            rNLDetailItem.ECommercialInvoiceAmountInEUR = 0;
                            rNLDetailItem.ECommercialInvoiceAmountEUR = 0;
                        }
                        i++;
                    }
                }

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ShipmentAmount_NL");
                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "ShipmentAmount_NL");
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

        public string GetShipmentAmountVN(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get report success" };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Report_function_GetShipmentAmountVN", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);

                adap.TableMappings.Add("Table", "Report_ShipmentAmountVN_View");
                adap.TableMappings.Add("Table1", "Report_ShipmentAmountVN_Detail_View");
                adap.TableMappings.Add("Table2", "Report_ShipmentAmountVN_Support_View");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ShipmentAmount_VN");
                foreach (var rNLItem in ds.Report_ShipmentAmountVN_View)
                {
                    rNLItem.ProformaInvoiceNos = "";
                    rNLItem.ClientUDs = "";
                    foreach (var sVNItem in ds.Report_ShipmentAmountVN_Support_View.Where(o => o.PurchasingInvoiceID == rNLItem.PurchasingInvoiceID))
                    {
                        if (!rNLItem.ProformaInvoiceNos.Contains(sVNItem.ProformaInvoiceNo)) rNLItem.ProformaInvoiceNos += sVNItem.ProformaInvoiceNo + " / ";
                        if (!rNLItem.ClientUDs.Contains(sVNItem.ClientUD)) rNLItem.ClientUDs += sVNItem.ClientUD + " / ";
                    }
                    if (rNLItem.ProformaInvoiceNos.Length > 2) rNLItem.ProformaInvoiceNos = rNLItem.ProformaInvoiceNos.Substring(0, rNLItem.ProformaInvoiceNos.Length - 2);
                    if (rNLItem.ClientUDs.Length > 2) rNLItem.ClientUDs = rNLItem.ClientUDs.Substring(0, rNLItem.ClientUDs.Length - 2);
                }

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "ShipmentAmount_VN");
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

        public IEnumerable<DTO.Support.Season> GetSeasons(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory support_factory = new Support.DataFactory();
            try
            {
                return support_factory.GetSeason().ToList(); ;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.Support.Season>();
            }
        }

    }
}
