using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class EmailMessage
    {
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string SendTo { get; set; }
    }
}
