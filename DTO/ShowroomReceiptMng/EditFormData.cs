using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShowroomReceiptMng
{
    public class EditFormData
    {
        public ShowroomReceipt Data { get; set; }
        public List<DTO.Support.User> Users { get; set; }
        public List<DTO.Support.Showroom> Showrooms { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
    } 
}
