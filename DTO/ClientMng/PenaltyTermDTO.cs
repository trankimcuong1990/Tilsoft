using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class PenaltyTermDTO
    {
        public int PenaltyTermID { get; set; }
        public int? ClientID { get; set; }
        public string PenaltyTermNM { get; set; }
        public string PenaltyTermDate { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Value { get; set; }
        public string Remark { get; set; }
        public string NumberDate { get; set; }
}
}
