using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class BookingMng : Lib.BLLBase2<DTO.BookingMng.SearchFormData, DTO.BookingMng.EditFormData, DTO.BookingMng.Booking>
    {
        private DAL.BookingMng.DataFactory factory;
        private Framework fwBLL = new Framework();
        public BookingMng() {
            factory = new DAL.BookingMng.DataFactory();
        }
        public BookingMng(string tempFolder)
        {
            factory = new DAL.BookingMng.DataFactory(tempFolder);
        }

        public override DTO.BookingMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of booking");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.BookingMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get booking: " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete booking: " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.BookingMng.Booking dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update booking: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.BookingMng.Booking dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve booking: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.Approve(id, ref dtoItem, out notification);
        }

        public override bool Reset(int id, ref DTO.BookingMng.Booking dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve booking: " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.Reset(id, ref dtoItem, out notification);
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.BookingMng.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public DTO.BookingMng.EditFormData GetData(int id, int clientID, int supplierID, string season, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get booking: " + id.ToString() + "; ClientID = " + clientID.ToString() + "; SupplierID = " + supplierID.ToString() + "; Season = " + season);

            // query data
            return factory.GetData(id, clientID, supplierID, season, out notification);
        }

        public DTO.BookingMng.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetInitData(out notification);
        }

        public string GetReportData(int bookingID, int iRequesterID, out Library.DTO.Notification notification)
        {
            DAL.BookingMng.ReportDataFactory print_factory = new DAL.BookingMng.ReportDataFactory();
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get booking report");

            // query data
            return print_factory.GetReportData(bookingID, out notification);
        }

        public bool ConfirmETA(int bookingID, int iRequesterID, string ETA, int ETAType, out string concurrencyFlag_String, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "confirm ETA for booking: " + bookingID.ToString());
            return factory.ConfirmETA(bookingID, iRequesterID, ETA, ETAType, out concurrencyFlag_String, out notification);
        }
    }
}
