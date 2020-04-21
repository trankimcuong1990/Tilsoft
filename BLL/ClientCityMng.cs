using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class ClientCityMng : Lib.BLLBase2<DTO.ClientCityMng.SearchFormData, DTO.ClientCityMng.EditFormData, DTO.ClientCityMng.ClientCity >
    {
        private DAL.ClientCityMng.DataFactory factory = new DAL.ClientCityMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        //public ClientCityMng(string tempFolder)
        //{
        //    factory = new DAL.ClientCityMng.DataFactory(tempFolder);
        //}

        public override DTO.ClientCityMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of cities");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ClientCityMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get city " + id.ToString());

            // query data
            return factory.GetData(id, iRequesterID, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete city " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ClientCityMng.ClientCity dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update city " + id.ToString());

            // query data
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.ClientCityMng.ClientCity dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ClientCityMng.ClientCity dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //public DTO.ClientCityMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        //{
        //    return factory.GetFilterData(out notification);
        //}
    }
}
