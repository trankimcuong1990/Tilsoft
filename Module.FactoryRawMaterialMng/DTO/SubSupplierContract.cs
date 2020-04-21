using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class SubSupplierContract
    {
        public int SubSupplierContractID { get; set; }
        public int? SubSupplierID { get; set; }
        public string ContractNo { get; set; }
        public string ContractFile { get; set; }
        public string ContractFileURL { get; set; }
        public string ContractFileFriendlyName { get; set; }
        public bool ContractFileHasChange { get; set; }
        public string NewContractFile { get; set; }
        public string Remark { get; set; }
        public int? ContractTypeID { get; set; }
        public bool? IsSigned { get; set; }
    }
}
