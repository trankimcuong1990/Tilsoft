using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.ProductBreakDownPALMng
{
    public class Executor : Library.Base.IExecutor
    {
        private readonly BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userID, string identifier, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            switch (identifier.ToLower().Trim())
            {
                case "getdata":
                    return bll.GetData(userID, filters, out notification);

                case "getinitdata":
                    return bll.GetInitData(userID, filters, out notification);

                case "updatedata":
                    return bll.UpdateData(userID, filters, out notification);

                case "deletedata":
                    return bll.DeleteData(userID, filters, out notification);

                case "getmodel":
                    return bll.GetModelWithFilters(userID, filters, out notification);

                case "getsample":
                    return bll.GetSampleWithFilters(userID, filters, out notification);

                case "getclient":
                    return bll.GetClientWithFilters(userID, filters, out notification);

                case "getproduction":
                    return bll.GetProductionItemWithFilters(userID, filters, out notification);

                case "getcategory":
                    return bll.GetProductBreakDownCategoryPAL(userID, filters, out notification);

                case "getcategory2":
                    return bll.GetCategoryPALWithFilters(userID, filters, out notification);

                case "approvedata":
                    return bll.ApproveData(userID, filters, out notification);

                case "getproduct":
                    return bll.GetProductWithFilters(userID, filters, out notification);

                case "get-model-default-option":
                    int modelID = Convert.ToInt32(filters["modelID"]);
                    return bll.GetModelDefaultOption(modelID, out notification);

                case "export-excel":
                    return bll.ExportExcel(userID, filters, out notification);

                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Unknown matched!";
                    break;
            }

            return null;
        }

        public bool DeleteData(int userID, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userID, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilters(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userID, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
