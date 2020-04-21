using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class ProductColliPiece
    {
        public int ProductColliPieceID { get; set; }
        public int? ProductColliID { get; set; }
        public int? PieceID { get; set; }
        public int? Colli { get; set; }
        public int? Pcs { get; set; }
        public string PiceDescription { get; set; }
        public string ColliPieceDescription { get; set; }
    }
}
