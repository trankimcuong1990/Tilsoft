using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class SubSupplierDocumentTypeDTO
    {
        public int ConstantEntryID { get; set; }
        public int? SubSupplierDocumentTypeID { get; set; }
        public string SubSupplierDocumentTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
