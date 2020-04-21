using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DALBase
{
    public abstract class FactoryBase2<TSeachFormData, TEditFormData, TDTO>
    {
        public abstract TSeachFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification);
        public abstract TEditFormData GetData(int id, out Library.DTO.Notification notification);
        public abstract bool DeleteData(int id, out Library.DTO.Notification notification);
        public abstract bool UpdateData(int id, ref TDTO dtoItem, out Library.DTO.Notification notification);
        public abstract bool Approve(int id, ref TDTO dtoItem, out Library.DTO.Notification notification);
        public abstract bool Reset(int id, ref TDTO dtoItem, out Library.DTO.Notification notification);
      
    }
}
