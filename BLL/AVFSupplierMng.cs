using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    //public class AVFSupplierMng : Lib.BLLBase2<DTO.AVFSupplierMng.SearchFormData, DTO.AVFSupplierMng.EditFormData, DTO.AVFSupplierMng.AVFSupplier>
    //{
    //    private DAL.AVFSupplierMng.DataFactory factory = new DAL.AVFSupplierMng.DataFactory();
    //    private BLL.Framework fwBLL = new Framework();

    //    public override DTO.AVFSupplierMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
    //    {
    //        // keep log entry
    //        fwBLL.WriteLog(iRequesterID, 0, "get list of suppliers");
    //        return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
    //    }

    //    public override DTO.AVFSupplierMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
    //    {
    //        // keep log entry
    //        fwBLL.WriteLog(iRequesterID, 0, "get supplier " + id.ToString());
    //        return factory.GetData(id, out notification);
    //    }

    //    public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
    //    {
    //        // keep log entry
    //        fwBLL.WriteLog(iRequesterID, 0, "delete supplier " + id.ToString());
    //        return factory.DeleteData(id, out notification);
    //    }

    //    public override bool UpdateData(int id, ref DTO.AVFSupplierMng.AVFSupplier dtoItem, int iRequesterID, out Library.DTO.Notification notification)
    //    {
    //        // keep log entry
    //        fwBLL.WriteLog(iRequesterID, 0, "update supplier " + id.ToString());
    //        dtoItem.UpdatedBy = iRequesterID;
    //        return factory.UpdateData(id, ref dtoItem, out notification);
    //    }

    //    public override bool Approve(int id, ref DTO.AVFSupplierMng.AVFSupplier dtoItem, int iRequesterID, out Library.DTO.Notification notification)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool Reset(int id, ref DTO.AVFSupplierMng.AVFSupplier dtoItem, int iRequesterID, out Library.DTO.Notification notification)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
