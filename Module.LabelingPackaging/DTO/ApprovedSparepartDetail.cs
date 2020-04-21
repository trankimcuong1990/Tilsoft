using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class ApprovedSparepartDetail
    {
        public int? LabelingPackagingSparepartDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string HangTagFile { get; set; }
        public string BoxShippingMarkFile { get; set; }
        public string BrassLabelFile { get; set; }
        public string AIFile { get; set; }
        public string WashCushionLabelFile { get; set; }
        public string UpdatedDate { get; set; }
        public string HangTagFileUrl { get; set; }
        public string HangTagFriendlyName { get; set; }
        public string BoxShippingMarkFileUrl { get; set; }
        public string BoxShippingMarkFriendlyName { get; set; }
        public string BrassLabelFileUrl { get; set; }
        public string BrassLabelFriendlyName { get; set; }
        public string AIFileUrl { get; set; }
        public string AIFriendlyName { get; set; }
        public string WashCushionLabelFileUrl { get; set; }
        public string WashCushionLabelFriendlyName { get; set; }
    }
}
