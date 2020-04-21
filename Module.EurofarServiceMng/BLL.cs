using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarServiceMng
{
    internal class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();
        public object GetWarehousePhysicalStock(out Library.DTO.Notification notification)
        {
            return factory.GetWarehousePhysicalStock(out notification);
        }
        public object GetWarehouseReservationStockByNLBL(out Library.DTO.Notification notification)
        {
            return factory.GetWarehouseReservationStockByNLBL(out notification);
        }
    }
}
