using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class QuickSearchProductionItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string ProductionItemNM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }

        public int? FactoryWarehouseID { get; set; }
    }
}
