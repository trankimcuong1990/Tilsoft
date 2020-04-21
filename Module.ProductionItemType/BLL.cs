﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemType
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public BLL(string tempFolder)
        {
            factory = new DAL.DataFactory(tempFolder);
        }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of Production Item");

            // query data
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as Production Item Group" + id.ToString());

            // query data
            return factory.GetData(iRequesterID, id, out notification);

        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete as Production Item Group" + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);

        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update Production Item Group" + id.ToString());

            // query data
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);

        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }
    }
}
