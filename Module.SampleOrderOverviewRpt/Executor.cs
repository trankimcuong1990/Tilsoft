using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleOrderOverviewRpt
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
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
            return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "getexcelreport":
                    if (!input.ContainsKey("Season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    if (input.ContainsKey("clientId") && input["clientId"] == null)
                    {
                        input["clientId"] = "";
                    }
                    return bll.GetExcelReportData(userId, input["Season"].ToString(), input["clientId"].ToString(), (int?)input["vnFactoryId"], (int?)input["sampleProductStatusID"], (int?)input["sampleOrderStatusID"], (int?)input["sampleOrderID"], (bool)input["canEdit"], (bool)input["canRead"], userId, out notification);

                case "getexcelprint":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.GetExcelReportData(userId, Convert.ToInt32(input["id"]), out notification);

                case "getsampleorder":
                    return bll.GetSampleOrder(userId, input, out notification);

                case "getinitdata":
                    return bll.GetInitDataCustomFunction(userId, (bool)input["canEdit"], (bool)input["canRead"], out notification);

                case "getexcelbarcode":
                    return bll.GetExportBarcode(userId, input, out notification);

                case "getexcelreportdatasampleprocess":
                    if (!input.ContainsKey("Season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    if (input.ContainsKey("clientId") && input["clientId"] == null)
                    {
                        input["clientId"] = "";
                    }
                    return bll.GetExcelReportDataSampleProcess(userId, input["Season"].ToString(), input["clientId"].ToString(), (int?)input["vnFactoryId"], (int?)input["sampleProductStatusID"], (int?)input["sampleOrderStatusID"], (int?)input["sampleOrderID"], (bool)input["canEdit"], (bool)input["canRead"], userId, out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
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
