using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.FactoryBreakdownMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            int? sampleProductID = 0;
            int? modelID = 0;

            if (input != null)
            {
                sampleProductID = (input.ContainsKey("sampleProductID") && input["sampleProductID"] != null && !string.IsNullOrEmpty(input["sampleProductID"].ToString()) ? (int?)Convert.ToInt32(input["sampleProductID"].ToString()) : null);
                modelID = (input.ContainsKey("modelID") && input["modelID"] != null && !string.IsNullOrEmpty(input["modelID"].ToString()) ? (int?)Convert.ToInt32(input["modelID"].ToString()) : null);
            }
          
            switch (identifier.ToLower())
            {
                //case "custom-function-identifier":
                //    // to do list
                //    break;

                //case "updateqaremark":
                //    if (!input.ContainsKey("id"))
                //    {
                //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                //        return null;
                //    }
                //    if (!input.ContainsKey("data"))
                //    {
                //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                //        return null;
                //    }
                //    id = Convert.ToInt32(input["id"]);
                //    data = input["data"];
                //    if (bll.UpdateQARemarkData(userId, id, ref data, out notification))
                //    {
                //        return data;
                //    }
                //    return null;
                case "getfilterdata":
                    return bll.GetFilterData(out notification);
                case "getdata":
                    return bll.GetData(userId, Convert.ToInt32(input["id"]), sampleProductID, modelID, out notification);
                case "exportexcellistfactorybreakdown":
                    return bll.GetFactoryBreakdownExportToExcelList(userId, input, out notification);

                case "exportfactorybreakdowndetail":
                    return bll.ExportExcelFactoryBreakdownDetail(userId, Convert.ToInt32(input["id"]), out notification);

                case "getdataforbreakdown":
                    return bll.GetDataForBreakdown(userId, Convert.ToInt32(input["sampleProductID"]), out notification);

                case "updateimportdata":
                    object dtoItem = input["dtoItem"];
                    return bll.ImportExcelData(userId, ref dtoItem, out notification);
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public string identifier
        {
            get
            {
                return _identifier;
            }
            set
            {
                _identifier = value;
            }
        }

        private string _identifier = string.Empty;
    }
}
