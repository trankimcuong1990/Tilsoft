using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng.DTO
{
    public class OutsourcingModelPiece
    {
        public Nullable<int> MasterModelPieceID { get; set; }
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string StatusNM { get; set; }
        public long KeyID { get; set; }
        public bool IsShow { get; set; }

        public OutsourcingModelPiece()
        {
            outSourcingModelDetailSearches = new List<OutSourcingModelDetailSearch>();
        }
        public List<DTO.OutSourcingModelDetailSearch> outSourcingModelDetailSearches { get; set; }
    }
}
