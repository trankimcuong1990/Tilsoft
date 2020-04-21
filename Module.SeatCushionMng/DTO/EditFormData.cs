using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SeatCushionMng.DTO
{
    public class EditFormData
    {
        public SeatCushion Data { get; set; }
        public List<Season> Seasons { get; set; }
    }
}
