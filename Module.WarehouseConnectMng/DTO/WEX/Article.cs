using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseConnectMng.DTO.WEX
{
    public class Article
    {
        public string out_clientid { get; set; }
        public string out_item_no { get; set; }
        public string out_barcode_base { get; set; }
        public int out_stock { get; set; }
        public int out_reserved_stock { get; set; }
        public int out_available_stock { get; set; }
        public string out_description { get; set; }
    }
}
