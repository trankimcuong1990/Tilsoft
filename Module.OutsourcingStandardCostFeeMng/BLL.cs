using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchForm GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "search OUTSOURCING");
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public List<DTO.OutsourcingStandardCostFeeDTO> GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get OUTSOURCING");
            return factory.GetData(userId, id, out notification);
        }
        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve OUTSOURCING");
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "update OUTSOURCING" + id.ToString());
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public List<DTO.OutsourcingModelPiece> GetDetailModel(int userId, int? id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get OUTSOURCING");
            return factory.GetDetailModel(userId, id, out notification);
        }
        public bool Reset(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "reset OUTSOURCING");
            return factory.ResetMD(userId, id, out notification);
        }

        public bool InsertData(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "Insert OUTSOURCING");
            return factory.InsertData(userId, out notification);
        }
    }
}
