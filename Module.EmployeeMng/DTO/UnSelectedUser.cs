using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.EmployeeMng.DTO
{
    public class UnSelectedUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string OfficeNM { get; set; }
    }
}
