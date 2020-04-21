using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WareHouseMng
{
    public class WareHouseSearchResult
    {
        public int WareHouseID { get; set; }
        public string WareHouseUD { get; set; }
        public string WareHouseNM { get; set; }
        public string Address { get; set; }
        public string CountryNM { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Remark { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}