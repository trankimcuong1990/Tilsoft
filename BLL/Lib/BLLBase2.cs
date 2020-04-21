using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL.Lib
{
    public abstract class BLLBase2<TSearchFormData, TEditFormData, TDTO>
    {
        public abstract TSearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification);
        public abstract TEditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification);
        public abstract bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification);
        public abstract bool UpdateData(int id, ref TDTO dtoItem, int iRequesterID, out Library.DTO.Notification notification);
        public abstract bool Approve(int id, ref TDTO dtoItem, int iRequesterID, out Library.DTO.Notification notification);
        public abstract bool Reset(int id, ref TDTO dtoItem, int iRequesterID, out Library.DTO.Notification notification);
    }
}
