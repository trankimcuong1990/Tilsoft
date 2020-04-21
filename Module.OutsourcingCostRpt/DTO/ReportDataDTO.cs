using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostRpt.DTO
{
    public class ReportDataDTO
    {
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> ProductionTeamID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ClientUD { get; set; }
        public decimal TotalDeliveryQnt { get; set; }
        public decimal TotalReceivedQnt { get; set; }
        public decimal ReceivedQnt { get; set; }
        public decimal RemainReceivedQnt { get; set; }
        public decimal DeliveryQnt { get; set; }
        public decimal CanSuonPrice { get; set; }
        public Nullable<decimal> TransportAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public Nullable<decimal> CanSuonAmount { get; set; }
        public decimal WeavingFinishedProductAmount { get; set; }
        public Nullable<decimal> TotalQnt { get; set; }

        public List<ReportDataDetailDTO> ReportDataDetailDTOs { get; set; }
        public ReportDataDTO()
        {
            ReportDataDetailDTOs = new List<ReportDataDetailDTO>();
        }
    }
}
