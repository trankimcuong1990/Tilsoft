using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Base
{
    public interface IExecutor
    {
        object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out DTO.Notification notification);
        object GetData(int userId, int id, out DTO.Notification notification);
        bool DeleteData(int userId, int id, out DTO.Notification notification);
        bool UpdateData(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        bool Approve(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        bool Reset(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        object GetInitData(int userId, out DTO.Notification notification);
        object CustomFunction(int userId, string identifier, Hashtable input, out DTO.Notification notification);
        string identifier { get; set; }
    }

    public interface IExecutor2
    {
        object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out DTO.Notification notification);
        object GetData(int userId, int id, Hashtable param, out DTO.Notification notification);
        bool DeleteData(int userId, int id, out DTO.Notification notification);
        bool UpdateData(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        bool Approve(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        bool Reset(int userId, int id, ref object dtoItem, out DTO.Notification notification);
        object GetInitData(int userId, out DTO.Notification notification);
        object GetSearchFilter(int userId, out DTO.Notification notification);
        object CustomFunction(int userId, string identifier, Hashtable input, out DTO.Notification notification);
        string identifier { get; set; }
    }
}
