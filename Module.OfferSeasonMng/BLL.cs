using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library.DTO;

namespace Module.OfferSeasonMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "search Delivery");
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get Delivery");
            return factory.GetData(userId, id, param, out notification);
        }
        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "delete Delivery" + id.ToString());
            return factory.DeleteData(userId, id, out notification);
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "update Delivery" + id.ToString());
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }
        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve Delivery");
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }
        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve Delivery");
            return factory.Reset(userId, id, ref dtoItem, out notification);
        }
        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get init Delivery");
            return factory.GetInitData(userId, out notification);
        }
        public object GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get search filter Delivery");
            return factory.GetInitData(userId, out notification);
        }

        //
        //customize function
        //

        public List<DTO.OfferSeasonTypeDTO> GetOfferSeasonType(out Library.DTO.Notification notification)
        {
            return factory.GetOfferSeasonType(out notification);
        }
        public DTO.OfferSeasonDetailDTO GetOfferSeasonDetail(int offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            return factory.GetOfferSeasonDetail(offerSeasonDetailID, out notification);
        }

        public DTO.OfferSeasonDetailDTO UpdateOfferSeasonDetail(int userId, int offerSeasonID, int offerSeasonDetailID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateOfferSeasonDetail(userId, offerSeasonID, offerSeasonDetailID, dtoItem, out notification);
        }

        public DTO.ModelSearchFormData SearchModel(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.SearchModel(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.ProductSearchFormData SearchProduct(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.SearchProduct(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.SparepartSearchFormData SearchSparepart(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.SearchSparepart(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.OfferItemProperiesDTO GetOfferItemProperties(Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.GetOfferItemProperties(param, out notification);
        }

        public DTO.OfferItemDefaultPropertiesDTO GetOfferItemDefaultProperties(int? modelID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetOfferItemDefaultProperties(modelID, season, out notification);
        }

        public List<DTO.ClientDTO> SearchClient(string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.SearchClient(searchQuery, out notification);
        }
        public List<DTO.ModelSparepartDTO> SearchModelSparepart(int modelID, out Library.DTO.Notification notification)
        {
            return factory.SearchModelSparepart(modelID, out notification);
        }

        public DTO.PlaningPurchasingPriceData GetPlanningPurchasingPrice(Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.GetPlanningPurchasingPrice(param, out notification);
        }

        public List<DTO.OfferSeasonDetailDTO> UpdateOfferSeasonDetail2(int userId, int offerSeasonID, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateOfferSeasonDetail2(userId, offerSeasonID, dtoItem, out notification);
        }

        public bool DeleteOfferSeasonDetail(int offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            return factory.DeleteOfferSeasonDetail(offerSeasonDetailID, out notification);
        }

        public string ExportOfferSeasonDetailToExcel(int id, out Library.DTO.Notification notification)
        {
            return factory.ExportOfferSeasonDetailToExcel(id, out notification);
        }

        public List<DTO.SalePriceTable> GetSalePriceTable(Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.GetSalePriceTable(param, out notification);
        }

        public List<DTO.SalePriceTableLastSeason> GetSalePriceTableLastSeason(int offerSeasonID, out Library.DTO.Notification notification)
        {
            return factory.GetSalePriceTableLastSeason(offerSeasonID, out notification);
        }

        public List<DTO.OfferLineDTO> GetOfferLineByOfferSeasonDetail(int offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            return factory.GetOfferLineByOfferSeasonDetail(offerSeasonDetailID, out notification);
        }

        public bool AdminUpdateSalePrice(int offerSeasonDetailID, decimal? salePrice, int updatedBy, out Library.DTO.Notification notification)
        {
            return factory.AdminUpdateSalePrice(offerSeasonDetailID, salePrice, updatedBy, out notification);
        }

        public List<DTO.ImageGalleryDTO> GetImageGallery(int offerSeasonID, out Library.DTO.Notification notification)
        {
            return factory.GetImageGallery(offerSeasonID, out notification);
        }

        public DTO.SampleOfferSeasonDTO CreateOfferSeasonSample(int? offerSeasonDetailID, int? clientID, string season, int? userID, out Library.DTO.Notification notification)
        {
            return factory.CreateOfferSeasonSample(offerSeasonDetailID, clientID, season, userID, out notification);
        }

        public List<DTO.RelatedFactoryOrderDetailDTO> GetRelatedFactoryOrderDetail(int offerSeasonID, out Library.DTO.Notification notification)
        {
            return factory.GetRelatedFactoryOrderDetail(offerSeasonID, out notification);
        }

        public List<DTO.PurchasingPriceLastYearDTO> GetPurchasingPriceLastYear(int offerSeasonID, out Library.DTO.Notification notification)
        {
            return factory.GetPurchasingPriceLastYear(offerSeasonID, out notification);
        }
    }
}
