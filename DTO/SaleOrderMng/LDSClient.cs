using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class LDSClient
    {
        public int LDSClientID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string LDSHTML { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<int> LDSTypeID { get; set; }
        public string LDSTypeNM { get; set; }
        public int? Version { get; set; }
        public string Remark { get; set; }
    }
}
