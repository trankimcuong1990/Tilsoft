namespace Module.TransportCICharge
{
    using Library.DTO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class BLL
    {
        #region ** Variable & Constant **

        private readonly DAL.DataFactory _factory = new DAL.DataFactory();
        private readonly Framework.BLL _fwBll = new Framework.BLL();

        #endregion

        #region ** Functions & Methods **

        public DTO.SearchData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "get list of transport cost forwarder invoice");
            return _factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditData GetData(int iRequesterID, int id, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "get transport cost forwarder invoice " + id);
            return _factory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "update transport cost forwarder invoice " + id);
            return _factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "delete transport cost forwarder invoice " + id);
            return _factory.DeleteData(id, out notification);
        }

        public DTO.ClientBookingList QuickSearchBooking(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "quick search booking by UD and ClientID");
            return _factory.QuickSearchBooking(iRequesterID, filters, out notification);
        }

        public List<DTO.ContainerNr> GetBookingContainer(int iRequesterID, Hashtable filters, out Notification notification)
        {
            _fwBll.WriteLog(iRequesterID, 0, "get booking container with BLNo");
            return _factory.GetBookingContainer(filters, out notification);
        }

        public DTO.PriceListCICharge GetPricePerUnit(int iRequesterID, Hashtable filters, out Notification notification)
        {
            return _factory.GetPricePerUnit(filters, out notification);
        }

        #endregion
    }
}
