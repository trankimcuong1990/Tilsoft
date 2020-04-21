using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrderProcessMonitoringRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.OfferDTO GetData(int userId, int id, string type, out Library.DTO.Notification notification)
        {
            return factory.GetData(userId, id, type, out notification);
        }
    }
}
