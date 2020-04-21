using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportStockList
{
    public class SearchFormData
    {
        public List<StockList> Data { get; set; }
        public List<DTO.Support.FreeToSaleCategory> FreeToSaleCategories { get; set; }
        public List<DTO.Support.ProductStatus> ProductStatuses { get; set; }
        public List<DTO.Support.WareHouseArea> WarehouseAreas { get; set; }
        public List<ProductSetEANCode> ProductSetEANCodes { get; set; }

        //grand total field
        public int? TotalPhysicalQnt { get; set; }
        public decimal? TotalPhysicalQntIn40HC { get; set; }
        public int? TotalOnSeaQnt { get; set; }
        public int? TotalTobeShippedQnt { get; set; }
        public int? TotalReservationQnt { get; set; }
        public int? TotalFTSQnt { get; set; }

        //sub total field
        public int? SubTotalPhysicalQnt { get; set; }
        public decimal? SubTotalPhysicalQntIn40HC { get; set; }
        public int? SubTotalOnSeaQnt { get; set; }
        public int? SubTotalTobeShippedQnt { get; set; }
        public int? SubTotalReservationQnt { get; set; }
        public int? SubTotalFTSQnt { get; set; }
    }
}
    