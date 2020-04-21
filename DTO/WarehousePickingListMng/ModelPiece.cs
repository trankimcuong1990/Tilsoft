using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehousePickingListMng
{
    public class ModelPiece
    {
        public int KeyID { get; set; }
        public int WarehousePickingListProductDetailID { get; set; }
        public int? ModelPieceID { get; set; }
        public int? ModelID { get; set; }
        public int? PieceModelID { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
    }
}
