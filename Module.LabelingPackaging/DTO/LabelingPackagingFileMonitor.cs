using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class LabelingPackagingFileMonitor
    {
        public int LPFileMonitorID { get; set; }
        public int? LabelingPackagingID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public bool? HangTagFileStatus { get; set; }
        public bool? BoxShippingMarkFileStatus { get; set; }
        public bool? BrassLabelFileStatus { get; set; }
        public bool? AIFileStatus { get; set; }
        public bool? WashCushionLabelFileStatus { get; set; }


        public string HangTagFileUrl { get; set; }
        public string HangTagFriendlyName { get; set; }
        public bool HangTagFileHasChange { get; set; }
        public string NewHangTagFile { get; set; }

        // Box Shipping Mark File
        public string BoxShippingMarkFileUrl { get; set; }
        public string BoxShippingMarkFriendlyName { get; set; }
        public bool BoxShippingMarkFileHasChange { get; set; }
        public string NewBoxShippingMarkFile { get; set; }

        // BrassLabelFile
        public string BrassLabelFileUrl { get; set; }
        public string BrassLabelFriendlyName { get; set; }
        public bool BrassLabelFileHasChange { get; set; }
        public string NewBrassLabelFile { get; set; }

        // AI File
        public string AIFileUrl { get; set; }
        public string AIFriendlyName { get; set; }
        public bool AIFileHasChange { get; set; }
        public string NewAIFile { get; set; }

        // WashCushionLabel
        public string WashCushionLabelFileUrl { get; set; }
        public string WashCushionLabelFriendlyName { get; set; }
        public bool WashCushionLabelFileHasChange { get; set; }
        public string NewWashCushionLabelFile { get; set; }
    }
}
