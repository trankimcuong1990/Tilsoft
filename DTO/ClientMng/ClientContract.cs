using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientMng
{
    public class ClientContract
    {
        [Display(Name = "ClientContractID")]
        public int? ClientContractID { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "ContractNo")]
        public string ContractNo { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "ContractFile")]
        public string ContractFile { get; set; }
        public string ContractFileURL { get; set; }
        public string ContractFileFriendlyName { get; set; }
        public bool ContractFileHasChange { get; set; }
        public string NewContractFile { get; set; }

        [Display(Name = "ContractTypeID")]
        public int? ContractTypeID { get; set; }
        public string ClientDocumentTypeNM { get; set; }
        public bool? IsSigned { get; set; }

        public List<ClientContractDetail> ClientContractDetails { get; set; }

    }
}