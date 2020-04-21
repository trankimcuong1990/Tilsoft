using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.BookingMng
{
    public class ReportDataFactory
    {
        public string GetReportData(int bookingID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("BookingMng_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@BookingID", bookingID);

                adap.TableMappings.Add("Table", "BookingMng_Booking_ReportView");
                adap.Fill(ds);

                //generate schema
                //DALBase.Helper.DevCreateReportXMLSource(ds, "Booking");

                //generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "Booking");
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return string.Empty;
            }
        }
    }
}
