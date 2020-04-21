using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class ReturnData<T>
    {
        public T Data { get; set; }
        public int TotalRows { get; set; }
        public Notification Message { get; set; }
    }
}
