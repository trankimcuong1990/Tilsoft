using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WarehouseReservationMng
    {
        private DAL.WarehouseReservationMng.DataFactory factory = new DAL.WarehouseReservationMng.DataFactory();
        private Framework fwBLL = new Framework();

        public bool CreateReservation(int saleOrderID, int iRequesterID, ref DTO.WarehouseReservationMng.WarehouseReservation dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "create warehouse reservation, saleorder: " + saleOrderID.ToString());

            // query data
            return factory.CreateReservation(saleOrderID, iRequesterID, ref dtoItem, out notification);
        }

        public bool CancelReservation(int saleOrderID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "cancel warehouse reservation, saleorder: " + saleOrderID.ToString());

            // query data
            return factory.CancelReservation(saleOrderID, iRequesterID, out notification);
        }
    }
}
