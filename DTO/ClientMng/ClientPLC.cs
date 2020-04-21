using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientPLC
    {
        public int? PLCID { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Rating { get; set; }
        public string RatingComment { get; set; }
        public int? RatedBy { get; set; }
        public string RatorName { get; set; }
        public string RatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public int? ClientID { get; set; }
        public List<PLCLoadingPLan> PLCLoadingPLans { get; set; }
        public List<PLCSaleOrder> PLCSaleOrders { get; set; }
    }
}
