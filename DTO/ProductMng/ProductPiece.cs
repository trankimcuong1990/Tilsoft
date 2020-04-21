using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class ProductPiece
    {
        public int ProductPieceID { get; set; }
        public int? ProductID { get; set; }
        public int? PieceID { get; set; }
        public int? Quantity { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string EANCode { get; set; }
        public string PieceDescription { get; set; }
    }
}
