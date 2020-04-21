using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ShowroomReceiptMng
{
    public class ShowroomReceiptDetail
    {
        public int? ShowroomReceiptDetailID { get; set; }

        public int? ShowroomReceiptID { get; set; }

        public int? ShowroomItemID { get; set; }

        public int? Quantity { get; set; }

        public string Remark { get; set; }

        public int? ClientID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public List<ShowroomReceiptAreaDetail> ShowroomReceiptAreaDetails { get; set; }

    }

    public class ShowroomReceiptAreaDetail
    {
        public int? ShowroomReceiptAreaDetailID { get; set; }

        public int? ShowroomReceiptDetailID { get; set; }

        public int? ShowroomAreaID { get; set; }

        public int? Quantity { get; set; }

        public string Remark { get; set; }

        public string ShowroomAreaUD { get; set; }

        public string ShowroomAreaNM { get; set; }

    }

    


}