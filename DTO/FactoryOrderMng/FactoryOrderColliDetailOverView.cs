using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class FactoryOrderColliDetailOverView
    {
        public int FactoryOrderColliDetailID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? ProductColliID { get; set; }
        public int? ProductSetEANCodeID { get; set; }
        public string ColliEANCode { get; set; }
        public string ProductEANCode { get; set; }
    }
}
