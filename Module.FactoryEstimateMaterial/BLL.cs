using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryEstimateMaterial
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();


        public object EstimateMaterial(int iRequesterID, string factoryOrderIds, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get estimate material of orders");
            return factory.EstimateMaterial(iRequesterID, factoryOrderIds, out notification);
        }

        public int ImportMaterial(int iRequesterID, object data, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "create import material form estimate");
            return factory.ImportMaterial(iRequesterID, data, out notification);
        }

        public object GetSupportData(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get support data");
            return factory.GetSupportData(out notification);
        }

       
    }
}
