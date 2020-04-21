using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApplication2.DTO
{
    public class PersonDTO
    {
        public int PersonID { get; set; }

        [MaxLength(10, ErrorMessage = "FullName can not exceed 10 chars!")]
        public string FullName { get; set; }
    }
}
