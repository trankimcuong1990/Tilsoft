using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DTO.ModelMaterialOptionMng
{
    public class ModelMaterialOptionSearchResult
    {
        public int ModelMaterialOptionID { get; set; }

        public string ModelNM { get; set; }

        public string MaterialOptionNM { get; set; }

    }
}