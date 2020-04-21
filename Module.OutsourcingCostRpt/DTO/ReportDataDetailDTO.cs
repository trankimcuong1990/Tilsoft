using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostRpt.DTO
{
    public class ReportDataDetailDTO
    {
        public int ID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> ProductionTeamID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ClientUD { get; set; }
        public string ReceiptType { get; set; }
        public string ReceiptUD { get; set; }
        public string ReceiptDate { get; set; }
        public Nullable<decimal> TotalReceivedQnt { get; set; }
        public Nullable<decimal> ReceivedQnt { get; set; }
        public Nullable<decimal> TotalDeliveryQnt { get; set; }
        public Nullable<decimal> DeliveryQnt { get; set; }
        public Nullable<decimal> OtherPrice { get; set; }
        public string ReasonOtherPrice { get; set; }
        public Nullable<decimal> WeavingFinishedProductPrice { get; set; }
        public Nullable<decimal> CanSuonPrice { get; set; }
        public Nullable<decimal> TransportPrice { get; set; }
        public Nullable<decimal> DistanceToFactory { get; set; }
        public string OutsourcingCostTypeUD { get; set; }
        public string OutsourcingCostTypeNM { get; set; }
    }
}
