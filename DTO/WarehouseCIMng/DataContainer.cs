using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseCIMng
{
    public class DataContainer
    {
        public DTO.WarehouseCIMng.WarehouseCI WarehouseCIData { get; set; }
        public List<DTO.Support.Client> Clients { get; set; }
        public List<DTO.Support.Currency> Currency { get; set; }
        public List<DTO.Support.WareHouse> WareHouses { get; set; }
        public List<DTO.Support.TurnOverLedgerAccount> TurnOverLedgerAccounts { get; set; }
    }
}
