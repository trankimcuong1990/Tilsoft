using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALBase
{
    public abstract class FactoryBase<SearchResultDTOType, EditDTOType>
    {
        public abstract IEnumerable<SearchResultDTOType> GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection,out int totalRows, out Library.DTO.Notification notification);
        public abstract EditDTOType GetData(int id, out Library.DTO.Notification notification);
        public abstract bool DeleteData(int id, out Library.DTO.Notification notification);
        public abstract bool UpdateData(int id, ref EditDTOType dtoItem, out Library.DTO.Notification notification);
    }
}
