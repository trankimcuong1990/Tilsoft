﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialOrderNorm.DTO
{
    public class ClientProduct
    {
        public object KeyID { get; set; }
        public int? ClientID { get; set; }
        public int? ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public List<DTO.DefaultNorm> DefaultNorms { get; set; }
    }
}
