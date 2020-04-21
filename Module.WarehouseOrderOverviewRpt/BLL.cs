using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library.DTO;

namespace Module.WarehouseOrderOverviewRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        
        //
        //customize function
        //

        public string GetWarehouseSoldItem(string season, out Library.DTO.Notification notification)
        {
            return factory.GetWarehouseSoldItem(season, out notification);
        }        
    }
}
