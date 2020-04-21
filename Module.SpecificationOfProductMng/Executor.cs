using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Library.Base;

namespace Module.SpecificationOfProductMng
{
    public class Executor : Library.Base.IExecutor
    {
        BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = NotificationType.Success };

            switch (identifier.Trim())
            {
                case "getdataspecification":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown id!";

                        return null;
                    }

                    if (!input.ContainsKey("sampleProductID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown sampleProductID!";

                        return null;
                    }

                    int id = (input["id"] != null && !string.IsNullOrEmpty(input["id"].ToString())) ? Convert.ToInt32(input["id"].ToString()) : 0;
                    int? sampleProductID = (input["sampleProductID"] != null && !string.IsNullOrEmpty(input["sampleProductID"].ToString())) ? (int?)Convert.ToInt32(input["sampleProductID"].ToString()) : null;
                    int? productID = (input["productID"] != null && !string.IsNullOrEmpty(input["productID"].ToString())) ? (int?)Convert.ToUInt32(input["productID"].ToString()) : null;

                    return bll.GetDataSpecification(userId, id, sampleProductID, productID, out notification);

                case "getreportdata":
                    if (!input.ContainsKey("productSpecificationID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown id";
                        return null;
                    }

                    int ProductSpecificationID = (input["productSpecificationID"] != null && !string.IsNullOrEmpty(input["productSpecificationID"].ToString())) ? Convert.ToInt32(input["productSpecificationID"].ToString()) : 0;
                    return bll.GetExportData(userId, ProductSpecificationID, out notification);

                case "searchsample":

                    if (!input.ContainsKey("factoryID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown sampleProductID!";

                        return null;
                    }

                    int? factoryID = (input["factoryID"] != null && !string.IsNullOrEmpty(input["factoryID"].ToString())) ? (int?)Convert.ToInt32(input["factoryID"].ToString()) : null;

                    return bll.QuickSearchSample(userId, factoryID, input, out notification);

                case "getspecofproductlist":
                    if (!input.ContainsKey("modelID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown modelID";
                        return null;
                    }

                    int? modelID = (input["modelID"] != null && !string.IsNullOrEmpty(input["modelID"].ToString())) ? (int?)Convert.ToInt32(input["modelID"].ToString()) : null;

                    return bll.GetsListSpec(userId, modelID, out notification);

                case "copyspecofproduct":
                    if (!input.ContainsKey("productSpecificationID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown productSpecificationID";
                        return null;
                    }

                    int? productSpecificationID = (input["productSpecificationID"] != null && !string.IsNullOrEmpty(input["productSpecificationID"].ToString())) ? (int?)Convert.ToInt32(input["productSpecificationID"].ToString()) : null;

                    return bll.CoppySpecOfProduct(userId, productSpecificationID, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = NotificationType.Error;
                    break;
            }
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.Delete(id, out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetdataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
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
