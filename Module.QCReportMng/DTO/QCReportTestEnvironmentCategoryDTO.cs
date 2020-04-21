using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class QCReportTestEnvironmentCategoryDTO
    {
        public int QCReportTestEnvironmentCategoryID { get; set; }
        public string Description { get; set; }
        public int? RowIndex { get; set; }

        public List<DTO.QCReportTestEnvironmentItemDTO> QCReportTestEnvironmentItemDTOs { get; set; }
    }
}
