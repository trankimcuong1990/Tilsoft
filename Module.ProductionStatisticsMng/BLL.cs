using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library.DTO;

namespace Module.ProductionStatisticsMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "search ProductionStatistics");
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get ProductionStatistics");
            return factory.GetData(userId, id, param, out notification);
        }
        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "delete ProductionStatistics" + id.ToString());
            return factory.DeleteData(userId, id, out notification);
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "update ProductionStatistics" + id.ToString());
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }
        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve ProductionStatistics");
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }
        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve ProductionStatistics");
            return factory.Reset(userId, id, ref dtoItem, out notification);
        }
        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get init ProductionStatistics");
            return factory.GetInitData(userId, out notification);
        }
        public object GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get search filter ProductionStatistics");
            return factory.GetInitData(userId, out notification);
        }
        //customize function
        public List<DTO.PlanningProductionTeamDTO> GetPlanningProductionTeam(int userId, int workCenterID, string producedDate, out Library.DTO.Notification notification)
        {
            return factory.GetPlanningProductionTeam(userId, workCenterID, producedDate, out notification);
        }
    }
}
