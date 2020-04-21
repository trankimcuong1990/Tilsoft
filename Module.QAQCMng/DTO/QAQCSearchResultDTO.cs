using System;
using System.Collections.Generic;

namespace Module.QAQCMng.DTO
{
    public class QAQCSearchResultDTO
    {
        public int QAQCID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusNM { get; set; }
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> SampleQNT { get; set; }
        public string LDS { get; set; }
        public Nullable<int> ItemStandardID { get; set; }
        public string ItemStandardNM { get; set; }
        public Nullable<int> InspectionOptionID { get; set; }
        public string InspectionOptionNM { get; set; }
        public Nullable<int> TestSamplingOptionID { get; set; }
        public string TestSamplingOptionNM { get; set; }
        public Nullable<int> ShippedByFactoryID { get; set; }
        public string ShippedByFactoryNM { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string OrderDate { get; set; }
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int FactoryOrderDetailID { get; set; }
        public string ApprovalNM { get; set; }
        public string ClientNM { get; set; }
        public string LocationNM { get; set; }
        public bool? IsRemove { get; set; }
        public List<ModelCheckListGroupDTO> ModelCheckListGroupDTOs { get; set; }
    }
}
