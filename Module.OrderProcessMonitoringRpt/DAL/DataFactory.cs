using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrderProcessMonitoringRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private OrderProcessMonitoringRptEntities CreateContext()
        {
            return new OrderProcessMonitoringRptEntities(Library.Helper.CreateEntityConnectionString("DAL.OrderProcessMonitoringRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        
        //
        // Description: get all data related to offer
        // Param description:
        // id: (int) id of object according to type
        // type: (string) type of object input, could be: "f" (factory order), "s" (sale order), other value will be treated as offer
        //
        public DTO.OfferDTO GetData(int userId, int id, string type, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferDTO data = new DTO.OfferDTO();
            try
            {
                int offerId = id;
                int saleOrderId = -1;
                int factoryOrderId = -1;
                using (OrderProcessMonitoringRptEntities context = CreateContext())
                {
                    try
                    {
                        switch (type)
                        {
                            case "f":
                                factoryOrderId = id;
                                saleOrderId = context.OrderProcessMonitoringRpt_FactoryOrder_View.FirstOrDefault(o => o.FactoryOrderID == factoryOrderId).SaleOrderID.Value;
                                offerId = context.OrderProcessMonitoringRpt_FactoryOrder_View.FirstOrDefault(o => o.FactoryOrderID == factoryOrderId).OrderProcessMonitoringRpt_SaleOrder_View.OfferID.Value;
                                break;

                            case "s":
                                saleOrderId = id;
                                offerId = context.OrderProcessMonitoringRpt_SaleOrder_View.FirstOrDefault(o => o.SaleOrderID == saleOrderId).OfferID.Value;
                                break;
                        }
                    }
                    catch
                    {
                        offerId = -1;
                    }

                    var dbItem = context.OrderProcessMonitoringRpt_Offer_View
                        .Include("OrderProcessMonitoringRpt_SaleOrder_View")
                        .Include("OrderProcessMonitoringRpt_SaleOrder_View.OrderProcessMonitoringRpt_FactoryOrder_View")
                        .Include("OrderProcessMonitoringRpt_SaleOrder_View.OrderProcessMonitoringRpt_LoadingPlan_View")
                        .Include("OrderProcessMonitoringRpt_SaleOrder_View.OrderProcessMonitoringRpt_ECommercialInvoice_View")
                        .FirstOrDefault(o => o.OfferID == offerId);

                    data = converter.DB2DTO_Offer(dbItem);

                    // set is selected
                    if (saleOrderId > 0)
                    {
                        data.SaleOrderDTOs.FirstOrDefault(o => o.SaleOrderID == saleOrderId).IsSelected = true;
                    }
                    if (factoryOrderId > 0)
                    {
                        data.SaleOrderDTOs.FirstOrDefault(o => o.SaleOrderID == saleOrderId).FactoryOrderDTOs.FirstOrDefault(o => o.FactoryOrderID == factoryOrderId).IsSelected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
