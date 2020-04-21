using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LabelingPackagingMng
{
   public  class PackagingMethod
    {
       public int PackagingMethodID { get; set; }
       public string PackagingMethodNM { get; set; }
    }
}
