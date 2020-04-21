using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WareHouseMng : Lib.BLLBase<DTO.WareHouseMng.WareHouseSearchResult, DTO.WareHouseMng.WareHouse>
    {
        private DAL.WareHouseMng.DataFactory factory = new DAL.WareHouseMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.WareHouseMng.WareHouseSearchResult> GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of warehouse");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.WareHouseMng.WareHouse GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete warehouse " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.WareHouseMng.WareHouse dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update warehouse " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        //
        // custom function
        //
        public DTO.WareHouseMng.DataContainer GetDataContainer(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get warehouse " + id.ToString());

            // query data
            return factory.GetDataContainer(id, out notification);
        }
    }
}
