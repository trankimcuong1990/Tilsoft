using System;

namespace Module.TransportCostForwarder
{
    public class Executor : Library.Base.IExecutor
    {
        private readonly BLL _bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return _bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return _bll.GetData(userId, id, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return _bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return _bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string action, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            switch (action.ToLower())
            {
                case "getdata":
                    break;

                case "getsearchfilter":
                    break;

                case "quicksearchforwarder":
                    return _bll.QuickSearchForwarder(userId, filters, out notification);
                case "quicksearchbooking":
                    return _bll.QuickSearchBooking(userId, filters, out notification);

                case "getdropdowncontainer": // Get drop down list Container No
                    return _bll.GetBookingContainer(userId, filters, out notification);

                case "getpriceperunit": // Get price unit
                    return _bll.GetPriceUnit(userId, filters, out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public string identifier { get; set; }
    }
}
