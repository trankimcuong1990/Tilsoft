using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.OrderQuantityCheckingRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();

        private OrderQuantityCheckingRptEntities CreateContext()
        {
            return new OrderQuantityCheckingRptEntities(DALBase.Helper.CreateEntityConnectionString("OrderQuantityCheckingRptModel"));
        }

        public List<DTO.OrderQuantityCheckingRpt.SaleOrder> GetSaleOrders(out Library.DTO.Notification notification)
        {
            List<DTO.OrderQuantityCheckingRpt.SaleOrder> Data = new List<DTO.OrderQuantityCheckingRpt.SaleOrder>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OrderQuantityCheckingRptEntities context = CreateContext())
                {
                    Data.AddRange(converter.DB2DTO_SaleOrderDetail2SaleOrder(context.OrderQuantityCheckingRpt_SaleOrderDetail_View.Include("OrderQuantityCheckingRpt_FactoryOrderDetail_View").ToList()));
                    Data.AddRange(converter.DB2DTO_SaleOrderSparepartDetail2SaleOrder(context.OrderQuantityCheckingRpt_SaleOrderDetailSparepart_View.Include("OrderQuantityCheckingRpt_FactoryOrderSparepartDetail_View").ToList()));
                    Data = Data.ToArray().OrderBy(o => o.ProformaInvoiceNo).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return Data;
        }
    }
}
