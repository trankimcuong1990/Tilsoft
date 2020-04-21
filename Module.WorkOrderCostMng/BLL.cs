using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderCostMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public List<DTO.ProductionItem> getProductionItemTypeCost(int userId, System.Collections.Hashtable filters)
        {
            return factory.getProductionItemTypeCost(userId, filters);
        }
        public DTO.EditFormData GetData(int iRequesterID, int id, int? workOrderID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get BOM" + id.ToString());

            return factory.GetData(iRequesterID, id, workOrderID, out notification);
        }
        public bool Update(int userId, int id, ref object dto, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(userId, id, ref dto, out notification);
        }
    }
}
