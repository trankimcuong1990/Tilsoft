using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleByMaterialRpt
{
    public class ProformaInvoiceWood
    {
        public string MaterialTypeNM { get; set; }
        public decimal? TotalCont { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Season { get; set; }
    }
}
