namespace Module.TransportCICharge
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.DTO;

    public class Executor : Library.Base.IExecutor
    {
        #region ** Variables & Constants **

        private readonly BLL _bll = new BLL();

        public string identifier { get; set; }

        #endregion

        #region ** Functions & Methods **

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "quicksearchbooking":
                    return _bll.QuickSearchBooking(userId, input, out notification);

                case "getdropdowncontainer":
                    return _bll.GetBookingContainer(userId, input, out notification);

                case "getpriceperunit": // Get price unit
                    return _bll.GetPricePerUnit(userId, input, out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return _bll.DeleteData(userId, id, out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            return _bll.GetData(userId, id, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return _bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return _bll.UpdateData(userId, id, ref dtoItem, out notification);
        }
        
        #endregion
    }
}
