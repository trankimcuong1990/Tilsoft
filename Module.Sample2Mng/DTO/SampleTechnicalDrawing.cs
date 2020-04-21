using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleTechnicalDrawing
    {
        public int SampleTechnicalDrawingID { get; set; }
        public int? SampleProductID { get; set; }
        public string Description { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmedDate { get; set; }
        public string ConfirmerName { get; set; }

        public List<DTO.SampleTechnicalDrawingFile> SampleTechnicalDrawingFiles { get; set; }
    }
}   
