using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class ShowFactoryhouseNM
    {
        public int WarehouseTransferID { get; set; }
        public int FromFactoryWarehouseID { get; set; }
        public int ToFactoryWarehouseID { get; set; }
        public string ToFactoryWarehouseNM { get; set; }
        public string FromFactoryWarehouseNM { get; set; }

    }
}
