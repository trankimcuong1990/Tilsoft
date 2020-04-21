using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class ProductColli
    {
        public int ProductColliID { get; set; }
        public int? ProductSetEANCodeID { get; set; }
        public string EANCode { get; set; }
        public int? ColliIndex { get; set; }
        public List<ProductColliPiece> ProductColliPieces { get; set; }

        public int? PackagingMethodID { get; set; }
        public string PackagingMethodUD { get; set; }
        public string PackagingMethodNM { get; set; }
        public decimal? CartonBoxDimL { get; set; }
        public decimal? CartonBoxDimW { get; set; }
        public decimal? CartonBoxDimH { get; set; }
        public decimal? NetWeight { get; set; }
        public decimal? GrossWeight { get; set; }
    }
}
