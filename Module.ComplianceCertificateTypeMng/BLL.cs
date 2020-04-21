using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ComplianceCertificateTypeMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

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
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.SupportFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.GetInitData(out notification);
        }
    }
}
