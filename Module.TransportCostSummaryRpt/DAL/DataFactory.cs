using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Module.TransportCostSummaryRpt.DAL
{
    public class DataFactory
    {
        public string GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get transport cost report success !!!" };
            TransportCostSummaryRptObject ds = new TransportCostSummaryRptObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("TransportCostSummaryRpt_function_GetTransportCostSummary", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);

                adap.TableMappings.Add("Table", "TransportInvoice");
                adap.TableMappings.Add("Table1", "TransportInvoiceDetail");
                adap.TableMappings.Add("Table2", "ChargeClientInvoice");
                adap.TableMappings.Add("Table3", "ChargeClientInvoiceDetail");
                adap.TableMappings.Add("Table4", "NonFobDelivery");
                adap.TableMappings.Add("Table5", "FobDelivery");
                adap.TableMappings.Add("Table6", "ExchangeRate");

                adap.Fill(ds);

                //
                int i = 1;
                var bookingIDs = ds.NonFobDelivery.Where(o => !o.IsBookingIDNull()).Select(s => s.BookingID).ToList();
                foreach (var bookingID in bookingIDs)
                {
                    //set value = 0 if booking is double or trible for non fob delivery
                    i = 1;
                    int countClient = 0;
                    foreach (var nonFobItem in ds.NonFobDelivery.Where(o => !o.IsBookingIDNull() && o.BookingID == bookingID))
                    {
                        if (nonFobItem.IsDefaultClient)
                        {
                            countClient = countClient + 1;
                        }

                        if (!nonFobItem.IsDefaultClient || (countClient > 1))
                        {
                            nonFobItem.TotalSeaFreightChargeForwarder = 0;
                            nonFobItem.TotalHandlingChargeForwarder = 0;
                            nonFobItem.TotalTruckingChargeForwarder = 0;
                            nonFobItem.TotalOtherChargeForwarder = 0;
                            nonFobItem.TotalCostChargeForwarder = 0;
                            nonFobItem.TotalSeaFreightOfferForwarder = 0;
                            nonFobItem.TotalHandlingOfferForwarder = 0;
                            nonFobItem.TotalTruckingOfferForwarder = 0;
                            nonFobItem.DeltaSeaFreightForwarder = 0;
                            nonFobItem.DeltaHandlingForwarder = 0;
                            nonFobItem.DeltaTruckingForwarder = 0;
                        }
                    }

                    //set value = 0 if booking is double or trible for fob delivery
                    i = 1;
                    foreach (var fobItem in ds.FobDelivery.Where(o => !o.IsBookingIDNull() && o.BookingID == bookingID))
                    {
                        if (i > 1)
                        {
                            fobItem.TotalSeaFreightChargeForwarder = 0;
                            fobItem.TotalHandlingChargeForwarder = 0;
                            fobItem.TotalTruckingChargeForwarder = 0;
                            fobItem.TotalOtherChargeForwarder = 0;
                            fobItem.TotalCostChargeForwarder = 0;
                            fobItem.TotalSeaFreightOfferForwarder = 0;
                            fobItem.TotalHandlingOfferForwarder = 0;
                            fobItem.TotalTruckingOfferForwarder = 0;
                            fobItem.DeltaSeaFreightForwarder = 0;
                            fobItem.DeltaHandlingForwarder = 0;
                            fobItem.DeltaTruckingForwarder = 0;

                            fobItem.TotalSeaFreightChargeClient = 0;
                            fobItem.TotalHandlingChargeClient = 0;
                            fobItem.TotalTruckingChargeClient = 0;
                            fobItem.TotalOtherChargeClient = 0;
                            fobItem.TotalSeaFreightOfferClient = 0;
                            fobItem.TotalHandlingOfferClient = 0;
                            fobItem.TotalTruckingOfferClient = 0;
                            fobItem.DeltaSeaFreightClient = 0;
                            fobItem.DeltaHandlingClient = 0;
                            fobItem.DeltaTruckingClient = 0;
                        }
                        i++;
                    }
                }

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "TransportCostSummary");
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = iEx.Message;
                return string.Empty;
            }
        }

        public List<Module.Support.DTO.Season> GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            try
            {
                return new Module.Support.DAL.DataFactory().GetSeason();
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = iEx.Message;
                return null;
            }
        }
    }
}
