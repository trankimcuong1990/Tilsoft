using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ShowroomReceiptMng
{
    public class ShowroomReceipt
    {
        public int? ShowroomReceiptID { get; set; }

        public int? ReceiptTypeID { get; set; }

        public string Season { get; set; }

        public string ReceiptNo { get; set; }

        public string ReceiptDate { get; set; }

        public int? ShowroomID { get; set; }

        public int? StorekeeperID { get; set; }

        public string ImportFrom { get; set; }

        public string ExportTo { get; set; }

        public string Remark { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string ReceiptTypeText { get; set; }

        public string StorekeeperName { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public List<ShowroomReceiptDetail> ShowroomReceiptDetails { get; set; }

        public string CreatorName2 { get; set; }

        public string UpdatorName2 { get; set; }
    }
}