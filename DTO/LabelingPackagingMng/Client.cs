using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LabelingPackagingMng
{
  public  class Client
    {
      public string ClientUD { get; set; }
      public string ClientNM { get; set; }
    }
}
