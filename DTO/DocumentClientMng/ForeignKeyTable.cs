using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DocumentClientMng
{
    public class ForeignKeyTable
    {
        public int Foreign_Type { get; set; }
        public string Foreign_Title { get; set; }
        public int Foreign_ID { get; set; }
        public string Foreign_UD { get; set; }
        public string Foreign_NM { get; set; }
    }
}
