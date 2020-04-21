using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotation2Mng.DTO
{
    public class ClientAdditionalConditionDTO
    {
        public int? ClientAdditionalConditionID { get; set; }
        public int? ClientID { get; set; }
        public int? AdditionalConditionID { get; set; }
        public bool? IsSelected { get; set; }
        public int? UpdatedCheckBy { get; set; }
        public DateTime? UpdatedCheckDate { get; set; }
        public int? UpdatedUnCheckBy { get; set; }
        public DateTime? UpdatedUnCheckDate { get; set; }
        public string UpdateName1 { get; set; }
        public string UpdateName2 { get; set; }
        public string AdditionalConditionNM { get; set; }
        public string AdditionalConditionTypeNM { get; set; }
        public string Remark { get; set; }
        public string ClientUD { get; set; }
        public string RemarkClient { get; set; }
        public string AttachFile { get; set; }
        public string AttachFileURL { get; set; }
        public string AttachFileFriendlyName { get; set; }
        public bool ActiveFactory { get; set; }
    }
}
