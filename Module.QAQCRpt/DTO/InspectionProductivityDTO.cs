using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCRpt.DTO
{
    public class InspectionProductivityDTO
    {
        public Nullable<int> ProductGroupID { get; set; }
        public string ProductGroupNM { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<int> QuantityCritical { get; set; }
        public Nullable<int> QuantityMajor { get; set; }
        public Nullable<int> QuantityMinor { get; set; }
        public Nullable<int> FinalTotalQty { get; set; }
        public Nullable<int> InlineTotalQty { get; set; }
    }
}
