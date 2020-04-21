using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class SystemSettingMng : Lib.BLLBase2<DTO.SystemSettingMng.SearchFormData, DTO.SystemSettingMng.EditFormData, DTO.SystemSettingMng.SystemSetting>
    {
        private DAL.SystemSettingMng.DataFactory factory = new DAL.SystemSettingMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.SystemSettingMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of system settings");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.SystemSettingMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get system setting " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete sysetem setting " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.SystemSettingMng.SystemSetting dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update system setting " + id.ToString());

            // query data
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.SystemSettingMng.SystemSetting dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SystemSettingMng.SystemSetting dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

    }
}
