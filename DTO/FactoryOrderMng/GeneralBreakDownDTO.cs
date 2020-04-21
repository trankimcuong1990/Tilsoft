using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class GeneralBreakDownDTO
    {
        public int ProductBreakDownID { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ItemSize { get; set; }
        public string FrameDescription { get; set; }
        public string CushionDescription { get; set; }
        public string CartonSize { get; set; }
    }
}
