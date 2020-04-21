using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.EurofarServiceMng.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();

        private EurofarServiceEntities CreateContext()
        {
            return new EurofarServiceEntities(Library.Helper.CreateEntityConnectionString("DAL.EurofarServiceModel"));
        }

        public List<DTO.WarehousePhysicalStock> GetWarehousePhysicalStock(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            
            try
            {
                using (EurofarServiceEntities context = CreateContext())
                {
                    var result = context.EurofarServiceMng_WarehousePhysicalStock_View;
                    return converter.DB2DTO_WarehousePhysicalStock(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new List<DTO.WarehousePhysicalStock>();
            }
        }

        public List<DTO.WarehouseReservationStockBy_NLBL> GetWarehouseReservationStockByNLBL(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (EurofarServiceEntities context = CreateContext())
                {
                    var result = context.EurofarServiceMng_WarehouseReservationStockBy_NLBL_View;
                    return converter.DB2DTO_WarehouseReservationStock(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new List<DTO.WarehouseReservationStockBy_NLBL>();
            }
        }

    }
}
