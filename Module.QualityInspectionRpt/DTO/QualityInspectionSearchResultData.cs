using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt.DTO
{
    public class QualityInspectionSearchResultData
    {
        public int QualityInspectionID { get; set; }

        public int? QualityInspectionTypeID { get; set; }

        public string QualityInspectionTypeNM { get; set; }

        public int? WorkOrderID { get; set; }

        public int? ClientID { get; set; }

        public int? QCID { get; set; }

        public string QCName { get; set; }

        public string InspectionDate { get; set; }

        public int? ProductionItemID { get; set; }

        public string WorkCenterMethod { get; set; }

        public string WorkCenterQuantity { get; set; }

        public int? SupplierID { get; set; }

        public string SubSupplierFullNM { get; set; }

        public string ReceivedDate { get; set; }

        public int? QualityInspectionCorrectActionID { get; set; }

        public string QualityInspectionCorrectActionNM { get; set; }

        public int? TeamLeaderID { get; set; }

        public string TeamLeaderName { get; set; }

        public int? ProductionSupervisorID { get; set; }

        public string ProductionSupervisorName { get; set; }

        public int? ApprovedBy { get; set; }

        public string ApprovedName { get; set; }

        public string ApprovedDate { get; set; }

        public string WorkCenterWorkingDate { get; set; }

        public int? MaterialColorID { get; set; }

        public string MaterialColorNM { get; set; }

        public string WorkOrderUD { get; set; }

        public string ClientUD { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }
    }
}
