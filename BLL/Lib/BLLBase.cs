using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL.Lib
{
    public abstract class BLLBase<SearchResultDTOType, EditDTOType>
    {
        public abstract IEnumerable<SearchResultDTOType> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification);
        public abstract EditDTOType GetData(int id, int iRequesterID, out Library.DTO.Notification notification);
        public abstract bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification);
        public abstract bool UpdateData(int id, ref EditDTOType dtoItem, int iRequesterID, out Library.DTO.Notification notification);
    }
}
