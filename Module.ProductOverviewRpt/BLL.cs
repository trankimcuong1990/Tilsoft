using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductOverviewRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.ModelDTO GetOverviewData(int id, int? offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            return factory.GetOverviewData(id, offerSeasonDetailID, out notification);
        }

        public DTO.PriceComparison.FormData GetItemToCompareData(int id, int? offerSeasonDetailID,  out Library.DTO.Notification notification)
        {
            return factory.GetItemToCompareData(id, offerSeasonDetailID, out notification);
        }

        public List<DTO.PriceComparison.ProductOptionDetailDTO> GetComparableItemData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetComparableItemData(filters, out notification);
        }

        public List<DTO.PriceComparison.ProductOptionDetailDTO> GetBestComparableItemData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetBestComparableItemData(filters, out notification);
        }

        public List<DTO.PriceComparison.PriceOfferHistoryDTO> GetPriceOfferHistory(int id, out Library.DTO.Notification notification)
        {
            return factory.GetPriceOfferHistory(id, out notification);
        }
    }
}
