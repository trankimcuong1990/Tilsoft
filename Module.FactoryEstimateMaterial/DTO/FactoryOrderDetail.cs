using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryEstimateMaterial.DTO
{
    public class FactoryOrderDetail
    {
        public bool? IsSelected { get; set; }
        public object KeyID { get; set; }
        public int FactoryOrderDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string Season { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public string FactoryFinishedProductUD { get;set; }
        public string FactoryFinishedProductNM { get; set; }
        public int? FactoryMaterialID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public decimal? NormValue { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public decimal? TotalStockQnt { get; set; }
        public decimal? AmountQnt {
            get { return OrderQnt * NormValue; }
            set { }
        }
        
    }
}
