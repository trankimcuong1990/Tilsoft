using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCRpt.DTO
{
    public class TotalInspectionForFactoryDetailDTO
    {
        public Nullable<int> DefectID { get; set; }
        public string DefectUD { get; set; }
        public string DefectNM { get; set; }
        public string DefectGroupUD { get; set; }
        public string DefectGroupNM { get; set; }
        public Nullable<int> DefectAQty { get; set; }
        public Nullable<int> DefectBQty { get; set; }
        public Nullable<int> DefectCQty { get; set; }
        public string TypeOfDefectA { get; set; }
        public string TypeOfDefectB { get; set; }
        public string TypeOfDefectC { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
    }
}
