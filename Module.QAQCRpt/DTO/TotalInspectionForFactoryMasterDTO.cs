using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCRpt.DTO
{
    public class TotalInspectionForFactoryMasterDTO
    {
        public TotalInspectionForFactoryMasterDTO()
        {
            detail = new List<TotalInspectionForFactoryDetailDTO>();
        }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }

        public List<DTO.TotalInspectionForFactoryDetailDTO> detail { get; set; }
    }
}
