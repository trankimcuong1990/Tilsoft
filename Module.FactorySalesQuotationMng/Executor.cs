using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.FactorySalesQuotationMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }
        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.GetData(userId, id, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            bll = new BLL();
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            var searchQuery = "";
            if (input.ContainsKey("searchQuery") && !string.IsNullOrEmpty(input["searchQuery"].ToString()))
            {
                searchQuery = input["searchQuery"].ToString().Replace("'", "''");
            }
            switch (identifier.ToLower())
            {
                case "getfilterquotation":
                    return bll.GetFilterQuotation(userId, searchQuery, out notification);

                case "getfilterrawmaterial":
                    return bll.GetFilterRawMaterial(userId, searchQuery, out notification);

                case "getfiltersaleemployee":
                    return bll.GetFilterSaleEmployee(userId, searchQuery, out notification);

                case "getfilterproductitem":
                    return bll.GetFilterProductItem(userId, searchQuery, out notification);

                case "getfilterclientcontact":
                    return bll.GetFilterClientcontact(userId, searchQuery, out notification);

                case "cancel":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    object dtoItem = input["dtoItem"];
                    return bll.Cancel(userId, Convert.ToInt32(input["id"]), ref dtoItem, out notification);

                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Unknown match";
                    break;
            }
            return null;
        }
       
    }
}
