using Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.OfferMng
{
    public class FOBItemOnlyFactory
    {
        public string GetExcelFOBItemOnlyReportData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            FOBItemOnlyDataObject ds = new FOBItemOnlyDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferMng__function_FOBItemOnlyReportView", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", id);
                adap.TableMappings.Add("Table", "FOBItemOnly");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "Offer_FOBItemOnly");
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

        public string ExportExcelSearchOffer(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OfferReportDataObject ds = new OfferReportDataObject();

            string offerUD = null;
            string season = null;
            string clientUD = null;
            string clientNM = null;
            string paymentTermNM = null;
            string deliveryTermNM = null;
            string currency = null;
            string forwarderNM = null;
            string podNM = null;
            string articleCode = null;
            string description = null;
            int? v4id = null;
            int? offerStatus = null;
            bool? isPotential = null;
            int? saleID = null;


            if (filters.ContainsKey("OfferUD") && !string.IsNullOrEmpty(filters["OfferUD"].ToString()))
            {
                offerUD = filters["OfferUD"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                season = filters["Season"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                clientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
            {
                clientNM = filters["ClientNM"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("PaymentTermNM") && !string.IsNullOrEmpty(filters["PaymentTermNM"].ToString()))
            {
                paymentTermNM = filters["PaymentTermNM"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("DeliveryTermNM") && !string.IsNullOrEmpty(filters["DeliveryTermNM"].ToString()))
            {
                deliveryTermNM = filters["DeliveryTermNM"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("Currency") && !string.IsNullOrEmpty(filters["Currency"].ToString()))
            {
                currency = filters["Currency"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("ForwarderNM") && !string.IsNullOrEmpty(filters["ForwarderNM"].ToString()))
            {
                forwarderNM = filters["ForwarderNM"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("PODNM") && !string.IsNullOrEmpty(filters["PODNM"].ToString()))
            {
                podNM = filters["PODNM"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
            {
                articleCode = filters["ArticleCode"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                description = filters["Description"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("V4ID") && !string.IsNullOrEmpty(filters["V4ID"].ToString()))
            {
                v4id = Convert.ToInt32(filters["V4ID"].ToString().Replace("'", "''"));
            }

            if (filters.ContainsKey("OfferStatus") && filters["OfferStatus"] != null && !string.IsNullOrEmpty(filters["OfferStatus"].ToString()))
            {
                offerStatus = Convert.ToInt32(filters["OfferStatus"].ToString().Replace("'", "''"));
            }

            if (filters.ContainsKey("IsPotential") && filters["IsPotential"] != null && !string.IsNullOrEmpty(filters["IsPotential"].ToString()))
            {
                isPotential = filters["IsPotential"].ToString().ConvertStringToBool();
            }

            if (filters.ContainsKey("SaleID") && !string.IsNullOrEmpty(filters["SaleID"].ToString()))
            {
                saleID = Convert.ToInt32(filters["SaleID"].ToString().Replace("'", "''"));
            }

            try
            {

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("OfferMng_function_SearchOfferReport", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferUD", offerUD);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientUD", clientUD);
                adap.SelectCommand.Parameters.AddWithValue("@ClientNM", clientNM);
                adap.SelectCommand.Parameters.AddWithValue("@PaymentTermNM", paymentTermNM);
                adap.SelectCommand.Parameters.AddWithValue("@DeliveryTermNM", deliveryTermNM);
                adap.SelectCommand.Parameters.AddWithValue("@Currency", currency);
                adap.SelectCommand.Parameters.AddWithValue("@ForwarderNM", forwarderNM);
                adap.SelectCommand.Parameters.AddWithValue("@PODNM", podNM);
                adap.SelectCommand.Parameters.AddWithValue("@ArticleCode", articleCode);
                adap.SelectCommand.Parameters.AddWithValue("@Description", description);
                adap.SelectCommand.Parameters.AddWithValue("@V4ID", v4id);
                adap.SelectCommand.Parameters.AddWithValue("@OfferStatus", offerStatus);
                adap.SelectCommand.Parameters.AddWithValue("@IsPotential", isPotential);
                adap.SelectCommand.Parameters.AddWithValue("@SaleID", saleID);

                adap.TableMappings.Add("Table", "OfferSearchReport");
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "Offer_Search");
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
