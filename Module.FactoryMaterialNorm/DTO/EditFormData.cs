﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialNorm.DTO
{
    public class EditFormData
    {
        public FactoryMaterialNorm Data { get; set; }
        public List<Module.Support.DTO.Unit> Units { get; set; }
        public List<Module.Support.DTO.FactoryGoodsProcedure> FactoryGoodsProcedures { get; set; }
    }
}
