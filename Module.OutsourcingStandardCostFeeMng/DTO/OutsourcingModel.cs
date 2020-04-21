using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng.DTO
{
    public class OutsourcingModel
    {
        public Nullable<int> ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string StatusNM { get; set; }
        public long KeyID { get; set; }
        public int ProductTypeID { get; set; }

        public OutsourcingModel()
        {
            outSourcingModelDetailSearches = new List<OutSourcingModelDetailSearch>();
            outsourcingModelPieces = new List<OutsourcingModelPiece>();
        }
        public List<DTO.OutSourcingModelDetailSearch> outSourcingModelDetailSearches { get; set; }
        public List<DTO.OutsourcingModelPiece> outsourcingModelPieces { get; set; }
    }
}
