using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DTO
{
    public class Search
    {
        public Hashtable Filters { get; set; }
        public string SortedBy { get; set; }
        public string SortedDirection { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
