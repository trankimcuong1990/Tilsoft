using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTestingMng.DTO
{
    public class MaterialTestStandardSearchViewDTO
    {
        public int MaterialTestUsingMaterialStandardID { get; set; }
        public int? MaterialTestReportID { get; set; }
        public string TestStandardNM { get; set; }
    }
}
