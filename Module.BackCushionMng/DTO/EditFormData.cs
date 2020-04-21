using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BackCushionMng.DTO
{
    public class EditFormData
    {
        public BackCushionDTO Data { get; set; }
        public List<SeasonDTO> Seasons { get; set; }
    }
}
