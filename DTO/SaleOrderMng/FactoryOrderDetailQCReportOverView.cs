using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class FactoryOrderDetailQCReportOverView
    {
        public long KeyID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public int? QCReportID { get; set; }
        public Nullable<int> Type { get; set; }
        public string InspectedDate { get; set; }
        public string InspectionResultNM { get; set; }
    }
}
