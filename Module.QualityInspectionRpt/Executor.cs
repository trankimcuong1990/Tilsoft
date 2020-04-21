using Library.Base;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt
{
    public class Executor : IExecutor
    {
        private readonly BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            switch (identifier.ToLower())
            {
                case "getqualityinspectiontype":
                    return bll.GetDataSupport(userId, filters, out notification);

                case "updatequalityinspectiontype":
                    return bll.UpdateDataSupport(userId, filters, out notification);

                case "deletequalityinspectiontype":
                    return bll.DeleteDataSupport(userId, filters, out notification);

                case "getqualityinspectioncorrectaction":
                    return bll.GetDataSupport(userId, filters, out notification);

                case "updatequalityinspectioncorrectaction":
                    return bll.UpdateDataSupport(userId, filters, out notification);

                case "deletequalityinspectioncorrectaction":
                    return bll.DeleteDataSupport(userId, filters, out notification);

                case "getqualityinspectiondefaultsetting":
                    return bll.GetDataSupport(userId, filters, out notification);

                case "updatequalityinspectiondefaultsetting":
                    return bll.UpdateDataSupport(userId, filters, out notification);

                case "deletequalityinspectiondefaultsetting":
                    return bll.DeleteDataSupport(userId, filters, out notification);

                case "getqualityinspection":
                    return bll.GetData(userId, filters, out notification);

                case "updatequalityinspection":
                    return bll.UpdateData(userId, filters, out notification);

                case "deletequalityinspection":
                    return bll.DeleteData(userId, filters, out notification);

                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not match identifier to handle!";
                    return null;
            }
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
