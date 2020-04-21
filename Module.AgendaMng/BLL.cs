﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AgendaMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteAppointment(iRequesterID, id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
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

        public DTO.SupportData GetSupportData(string formName, out Library.DTO.Notification notification)
        {
            return factory.GetSupportData(formName, out notification);
        }

        public List<DTO.ClientSearchResult> QuickSearchClient(int iRequesterID, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchClient(query, out notification);
        }
    }
}
