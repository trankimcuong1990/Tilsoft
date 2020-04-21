﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryMng2.DTO
{
    public class FactoryResponsiblePerson
    {
        public int FactoryResponsiblePersonID { get; set; }
        public int? FactoryID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public int? UserID { get; set; }
        public bool PIC { get; set; }
    }
}
