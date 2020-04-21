using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderQuantityCheckingRpt
    {
        public DAL.OrderQuantityCheckingRpt.DataFactory factory = new DAL.OrderQuantityCheckingRpt.DataFactory();

        public List<DTO.OrderQuantityCheckingRpt.SaleOrder> GetOrderQuantityCheckings(out Library.DTO.Notification notification)
        {
            return factory.GetSaleOrders(out notification);
        }
    }
}
