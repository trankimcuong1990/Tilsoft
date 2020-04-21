using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ShowroomReceiptMng
{
    public class ShowroomReceiptSearch
    {
        public int? ShowroomReceiptID { get; set; }

        public string ReceiptNo { get; set; }

        public string ReceiptDate { get; set; }

        public string ImportFrom { get; set; }

        public string ExportTo { get; set; }

        public string Remark { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public string ReceiptTypeText { get; set; }

        public string StorekeeperName { get; set; }

        public string ShowroomNM { get; set; }

        public string CreatorName { get; set; }

        public int CreatorID { get; set; }

        public string UpdatorName { get; set; }

        public int UpdatorID { get; set; }

        public string Season { get; set; }

        public int? ReceiptTypeID { get; set; }

    }
}