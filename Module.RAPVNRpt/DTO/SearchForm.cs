using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RAPVNRpt.DTO
{
    public class SearchForm
    {
        //Total Order
        public decimal? TotalOrderQnt { get; set; }
        public decimal? TotalOrderInCont { get; set; }
        public decimal? TotalOrderShippedQnt { get; set; }
        public decimal? TotalOrderShippedQnt40HC { get; set; }
        public decimal? TotalOrderToBeShippedQnt { get; set; }
        public decimal? TotalOrderToBeShippedQnt40HC { get; set; }

        //Total Loaded
        public decimal? TotalLoadedQnt { get; set; }
        public decimal? TotalLoadedQnt40HC { get; set; }

        //TotalShipped
        public decimal? TotalShippedLoadedQnt { get; set; }
        public decimal? TotalShippedLoadedQnt40HC { get; set; }
        public decimal? ShippedQnt { get; set; }
        public decimal? ShippedQnt40HC { get; set; }

        public List<DTO.OrderData> OrderDatas { get; set; }
        public List<DTO.LoadedData> LoadedDatas { get; set; }
        public List<DTO.ShippedData> ShippedDatas { get; set; }
        public List<DTO.WorkOrderData> WorkOrderDatas { get; set; }
    }
}
