using System;
using System.Collections;
using Library.DTO;
using Module.TransportCostForwarder.DAL;
using Module.TransportCostForwarder.DTO;

namespace Module.TransportCostForwarder
{
    internal class BLL
    {
        private readonly DataFactory _factory = new DataFactory();
        private readonly Framework.BLL _fwBll = new Framework.BLL();

        public SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "get list of transport cost forwarder invoice");

            return _factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public EditFormData GetData(int iRequesterID, int id, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "get transport cost forwarder invoice " + id);

            return _factory.GetData(iRequesterID, id, out notification);
        }
        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "delete transport cost forwarder invoice " + id);

            return _factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {

            _fwBll.WriteLog(iRequesterID, 0, "update transport cost forwarder invoice " + id);

            return _factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public ForwarderList QuickSearchForwarder(int iRequesterID, Hashtable filters, out Notification notification)
        {
            // keep log entry
            _fwBll.WriteLog(iRequesterID, 0, "quick search forwarders by UD");
            return _factory.QuickSearchForwarder(filters, out notification);
        }

        public BookingList QuickSearchBooking(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "quick search booking by UD and ForwardID and UserID");
            return _factory.QuickSearchBooking(iRequesterID, filters, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<BookingContainer> GetBookingContainer(int iRequesterID, Hashtable filters, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "get booking container with BLNo");
            return _factory.GetBookingContainer(filters, out notification);
        }

        public decimal GetPriceUnit(int iRequesterID, Hashtable filters, out Notification notification)
        {
            return _factory.GetPricePerUnit(filters, out notification);
        }
    }
}
