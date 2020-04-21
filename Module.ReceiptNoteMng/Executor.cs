using Library.DTO;
using System;
using System.Collections;


namespace Module.ReceiptNoteMng
{
    public class Executor : Library.Base.IExecutor2
    {
        BLL bll = new BLL();
        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            
            switch (identifier.ToLower())
            {
                case "get-supplier":
                    return bll.GetSupplier(userId, out notification);
                case "search-purchasing-invoice":
                    int pageSize = Convert.ToInt32(input["pageSize"]);
                    int pageIndex = Convert.ToInt32(input["pageIndex"]);
                    string sortedBy = input["sortedBy"].ToString();
                    string sortedDirection = input["sortedDirection"].ToString();
                    return bll.SearchPurchasingInvoice(userId, input, pageSize, pageIndex, sortedBy, sortedDirection, out notification);

                case "search-factory-sale-invoice":
                    int pageSize1 = Convert.ToInt32(input["pageSize"]);
                    int pageIndex1 = Convert.ToInt32(input["pageIndex"]);
                    string sortedBy1 = input["sortedBy"].ToString();
                    string sortedDirection1 = input["sortedDirection"].ToString();
                    return bll.SearchFactorySaleInvoice(userId, input, pageSize1, pageIndex1, sortedBy1, sortedDirection1, out notification);

                case "set-status":
                    int receiptNoteID = Convert.ToInt32(input["receiptNoteID"]);
                    int statusID = Convert.ToInt32(input["statusID"]);
                    int receiptNoteTypeID = Convert.ToInt32(input["receiptNoteTypeID"]);
                    return bll.SetStatus(userId, receiptNoteID, statusID, receiptNoteTypeID, out notification);

                case "query-supplier":
                    return bll.QuyerySupplier(input, out notification);

                case "query-employee":
                    return bll.QuyeryEmployee(input, out notification);

                case "query-productionitem":
                    return bll.QueryProductionItem(input, out notification);

                //case "get-receiptnote-printout":
                //    int receiptNoteprintID = Convert.ToInt32(input["receiptNoteID"]);
                //    return bll.GetReceiptPrintout(userId, receiptNoteprintID, out notification);

                default:
                    break;
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public object GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            return bll.GetData(userId, id, param, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
