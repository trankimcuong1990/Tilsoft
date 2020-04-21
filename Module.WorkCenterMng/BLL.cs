using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng
{
    public class BLL
    {
        private DAL.DataFactory factoryWorkCenter = new DAL.DataFactory();

        public DTO.SearchForm GetDataWithFilter(int iRequesterID, Hashtable filter, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factoryWorkCenter.GetDataWithFilter(iRequesterID,filter, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditForm GetData(int iRequesterID, int id, out Notification notification)
        {
            return factoryWorkCenter.GetData(iRequesterID, id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            return factoryWorkCenter.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            return factoryWorkCenter.DeleteData(id, out notification);
        }

        public DTO.InitForm GetInitData(int iRequesterID, out Notification notification)
        {
            return factoryWorkCenter.GetInitData(iRequesterID, out notification);
        }
    }
}
