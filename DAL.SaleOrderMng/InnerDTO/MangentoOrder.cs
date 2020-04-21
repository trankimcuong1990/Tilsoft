using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SaleOrderMng.InnerDTO
{
    public class MagentoOrder
    {
        public string ClientUD { get; set; }
        public string Currency { get; set; }
        public string MagentoOrderUD { get; set; }
        public int? UserID { get; set; }
        public List<MagentoOrderDetail> MagentoOrderDetails { get; set; }
    }
}
