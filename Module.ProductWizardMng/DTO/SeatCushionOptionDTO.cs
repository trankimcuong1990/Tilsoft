using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class SeatCushionOptionDTO
    {
        public int SeatCushionID { get; set; }
        public string SeatCushionUD { get; set; }
        public string SeatCushionNM { get; set; }
        public int IsStandard { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
    }
}
