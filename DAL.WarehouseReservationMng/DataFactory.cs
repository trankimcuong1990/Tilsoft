using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.WarehouseReservationMng
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private WarehouseReservationMngEntities CreateContext()
        {
            return new WarehouseReservationMngEntities(DALBase.Helper.CreateEntityConnectionString("WareHouseImportMngModel"));
        }

        public bool CreateReservation(int saleOrderID, int userID, ref DTO.WarehouseReservationMng.WarehouseReservation dtoItem, out Library.DTO.Notification notification)
        {
            dtoItem.Details.ForEach(o => o.ProductStatusID = 1);

            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // cancel old reservation
                CancelReservation(saleOrderID, userID, out notification);
                if (notification.Type != Library.DTO.NotificationType.Success)
                {
                    throw new Exception(notification.Message);
                }

                using (WarehouseReservationMngEntities context = CreateContext())
                {
                    WarehouseReservation dbItem = new WarehouseReservation();
                    context.WarehouseReservation.Add(dbItem);
                    converter.DTO2DB(dtoItem, ref dbItem);
                    dbItem.UpdatedBy = dtoItem.UpdatedBy;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.StatusUpdatedBy = dtoItem.UpdatedBy;
                    dbItem.StatusUpdatedDate = DateTime.Now;
                    dbItem.ProcessingStatusID = 2;
                    dbItem.SaleOrderID = saleOrderID;
                    context.SaveChanges();

                    dtoItem = converter.DB2DTO_WarehouseReservation(dbItem);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }

            return result;
        }

        public bool CancelReservation(int saleOrderID, int userID, out Library.DTO.Notification notification)
        {
            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseReservationMngEntities context = CreateContext())
                {
                    WarehouseReservation dbItem = context.WarehouseReservation.FirstOrDefault(o => o.SaleOrderID == saleOrderID);
                    dbItem.UpdatedBy = userID;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.ProcessingStatusID = 3;
                    dbItem.StatusUpdatedBy = userID;
                    dbItem.StatusUpdatedDate = DateTime.Now;
                    context.SaveChanges();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }

            return result;
        }
    }
}
