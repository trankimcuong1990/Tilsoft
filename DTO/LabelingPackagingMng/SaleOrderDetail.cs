using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LabelingPackagingMng
{
    public class SaleOrderDetail
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}
