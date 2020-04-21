﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng
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
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            bll = new BLL(identifier);
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

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            string searchProductionItemUD = null;
            string searchProductionItem = null;
            switch (identifier.ToLower())
            {
                case "getdata":
                    if (!input.ContainsKey("ID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }
                    return bll.GetData(userId, Convert.ToInt32(input["ID"]), out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "getdetail":
                    if (!input.ContainsKey("ID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }
                    return bll.GetDetail(userId, Convert.ToInt32(input["ID"]), out notification);

                case "getproductionitem":
                    searchProductionItem = (input["searchProductionItem"] != null) ? input["searchProductionItem"].ToString() : null;
                    searchProductionItemUD = (input["searchProductionItemUD"] != null) ? input["searchProductionItemUD"].ToString() : null;

                    return bll.GetProductionItem(searchProductionItem, searchProductionItem, userId, out notification);

                case "updaterawlist":
                    return bll.UpdateRawList(userId, input["data"], out notification);

                case "removeraw":
                    return bll.RemoveRaw(userId, input["data"], out notification);

                case "getoverview":
                    if (!input.ContainsKey("ID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow raw material id" };
                        return null;
                    }
                    return bll.GetOverview(userId, Convert.ToInt32(input["ID"]), out notification);
                case "exporttoexcel":
                    return bll.ExportToExcel(userId, input, null, null, out notification);

            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }
    }
}
