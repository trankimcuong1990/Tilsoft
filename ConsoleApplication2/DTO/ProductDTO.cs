using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductUD { get; set; }
        public string ProductNM { get; set; }

        public List<ImageDTO> ImageDTOs { get; set; }
    }
}
