using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Base
{
    public abstract class DataFactory<TSeachFormData, TEditFormData>
    {
        public abstract TSeachFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out DTO.Notification notification);
        public abstract TEditFormData GetData(int id, out DTO.Notification notification);
        public abstract bool DeleteData(int id, out DTO.Notification notification);
        public abstract bool UpdateData(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        public abstract bool Approve(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        public abstract bool Reset(int userId, int id, ref object dtoItem, out DTO.Notification notification);
    }

    public abstract class DataFactory2<TSeachFormData, TEditFormData>
    {
        public abstract TSeachFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out DTO.Notification notification);
        public abstract TEditFormData GetData(int userId, int id, Hashtable param, out DTO.Notification notification);        
        public abstract bool DeleteData(int userId, int id, out DTO.Notification notification);
        public abstract bool UpdateData(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        public abstract bool Approve(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        public abstract bool Reset(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        public abstract object GetInitData(int userId, out DTO.Notification notification);
        public abstract object GetSearchFilter(int userId, out DTO.Notification notification);
    }
}
