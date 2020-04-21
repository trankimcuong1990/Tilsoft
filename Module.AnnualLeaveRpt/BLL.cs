using Library.DTO;
using Module.AnnualLeaveRpt.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveRpt
{
    internal class BLL
    {
        private DAL.DataFactory2 factory = new DAL.DataFactory2();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(iRequesterID , filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
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

        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {           
            return factory.GetInitData(iRequesterID, out notification);
        }

        public List<DetailDTO> GetDetails(int employeeID, int affectedYear, out Notification notification)
        {
            return factory.GetDetails(employeeID, affectedYear, out notification);
        }
        public List<DetailTotalDTO> GetDetailTotal(int employeeID, int affectedYear, int type, out Notification notification)
        {
            return factory.GetDetailTotal(employeeID, affectedYear, type, out notification);
        }
    }
}
