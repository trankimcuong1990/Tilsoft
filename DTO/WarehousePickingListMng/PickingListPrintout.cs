using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehousePickingListMng
{
    public class PickingListPrintout
    {
        public string CurrentDate 
        {
            get 
            {
                return DateTime.Now.ToString("dd/MM/yyyy hh:ss");
            }
        }
        public string ClientNM { get; set; }
        public string Address { get; set; }
        public string OrderNo { get; set; }
        public string RealPickingDate { get; set; }
        public string Remark { get; set; }
        public string RefNo { get; set; }
        public string DeliveryDate { get; set; }
        public int? TotalPickedQnt { get; set; }
        public string ReceiptNo { get; set; }
    }
}
