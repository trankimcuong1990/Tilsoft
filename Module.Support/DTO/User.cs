using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string OfficeNM { get; set; }
        public bool? IsSCM { get; set; }
    }
}
