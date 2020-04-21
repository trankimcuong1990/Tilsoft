using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DDCMng: Lib.BLLBase2<DTO.DDCMng.SearchFormData, DTO.DDCMng.EditFormData, DTO.DDCMng.DDC>
    {
        private DAL.DDCMng.DataFactory factory = new DAL.DDCMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.DDCMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of dcc");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.DDCMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get ddc " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete ddc " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.DDCMng.DDC dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update ddc " + id.ToString());

            // query data
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.DDCMng.DDC dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.DDCMng.DDC dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.DDCMng.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
        public DTO.DDCMng.EditFormData GetData(int id, int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get ddc" + id.ToString());

            // query data
            return factory.GetData(id, season, out notification);
        }
    }
}
