using System;
using System.Collections.Generic;

namespace Module.NormOfProduction.DTO
{
    public class WorkOrderDTO
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string StartDate { get; set; }
        public string UpdatedDate { get; set; }
        public Nullable<int> WorkOrderStatusID { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ArticleCode { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
        public List<BOMDTO> BOMDTOs { get; set; }
        public bool HasBOM { get; set; }
    }
}
