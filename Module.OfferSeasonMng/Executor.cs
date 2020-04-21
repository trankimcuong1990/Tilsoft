using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.OfferSeasonMng
{
    public class Executor : Library.Base.IExecutor2
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            return bll.GetData(userId, id, param, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public object GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetSearchFilter(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            int offerSeasonID = 0;
            int offerSeasonDetailID = 0;
            int totalRows = 0;
            int pageSize = 0;
            int pageIndex = 0;
            string sortedBy = "";
            string sortedDirection = "";
            int modelID = 0;
            switch (identifier.ToLower())
            {
                case "get-offer-season-type":
                    return bll.GetOfferSeasonType(out notification);

                case "get-offer-season-detail":
                    offerSeasonDetailID = Convert.ToInt32(input["offerSeasonDetailID"]);
                    return bll.GetOfferSeasonDetail(offerSeasonDetailID, out notification);

                case "update-offer-season-detail":
                    offerSeasonID = Convert.ToInt32(input["offerSeasonID"]);
                    offerSeasonDetailID = Convert.ToInt32(input["offerSeasonDetailID"]);
                    object dtoOfferSeasonDetail = input["dtoOfferSeasonDetail"];
                    return bll.UpdateOfferSeasonDetail(userId, offerSeasonID, offerSeasonDetailID, dtoOfferSeasonDetail, out notification);

                case "search-model":
                    pageSize = Convert.ToInt32(input["pageSize"]);
                    pageIndex = Convert.ToInt32(input["pageIndex"]);
                    sortedBy = input["sortedBy"].ToString();
                    sortedDirection = input["sortedDirection"].ToString();

                    totalRows = 0;
                    return bll.SearchModel(userId, input, pageSize, pageIndex, sortedBy, sortedDirection, out totalRows, out notification);

                case "search-product":
                    pageSize = Convert.ToInt32(input["pageSize"]);
                    pageIndex = Convert.ToInt32(input["pageIndex"]);
                    sortedBy = input["sortedBy"].ToString();
                    sortedDirection = input["sortedDirection"].ToString();

                    totalRows = 0;
                    return bll.SearchProduct(userId, input, pageSize, pageIndex, sortedBy, sortedDirection, out totalRows, out notification);

                case "search-sparepart":
                    pageSize = Convert.ToInt32(input["pageSize"]);
                    pageIndex = Convert.ToInt32(input["pageIndex"]);
                    sortedBy = input["sortedBy"].ToString();
                    sortedDirection = input["sortedDirection"].ToString();

                    totalRows = 0;
                    return bll.SearchSparepart(userId, input, pageSize, pageIndex, sortedBy, sortedDirection, out totalRows, out notification);

                case "get-offer-item-properties":
                    return bll.GetOfferItemProperties(input, out notification);

                case "get-offer-item-default-properties":
                    modelID = Convert.ToInt32(input["modelID"]);
                    string season = input["season"].ToString();
                    return bll.GetOfferItemDefaultProperties(modelID, season, out notification);

                case "search-client":
                    string searchQuery = input["searchQuery"].ToString();
                    return bll.SearchClient(searchQuery, out notification);

                case "search-model-sparepart":
                    modelID = Convert.ToInt32(input["modelID"]);
                    return bll.SearchModelSparepart(modelID, out notification);

                case "get-planning-purchasing-price":
                    return bll.GetPlanningPurchasingPrice(input, out notification);

                case "update-offer-season-detail2":
                    offerSeasonID = Convert.ToInt32(input["offerSeasonID"]);
                    object offerSeasonDetails = input["offerSeasonDetails"];
                    return bll.UpdateOfferSeasonDetail2(userId, offerSeasonID, offerSeasonDetails, out notification);

                case "delete-offer-season-detail":
                    offerSeasonDetailID = Convert.ToInt32(input["offerSeasonDetailID"]);
                    return bll.DeleteOfferSeasonDetail(offerSeasonDetailID, out notification);

                case "export-offer-season-detail":
                    offerSeasonID = Convert.ToInt32(input["offerSeasonID"]);
                    return bll.ExportOfferSeasonDetailToExcel(offerSeasonID, out notification);

                case "get-sale-price-table":
                    return bll.GetSalePriceTable(input, out notification);

                case "get-sale-price-table-last-season":
                    offerSeasonID = Convert.ToInt32(input["offerSeasonID"]);
                    return bll.GetSalePriceTableLastSeason(offerSeasonID, out notification);

                case "get-offerline-by-offer-season-detail":
                    offerSeasonDetailID = Convert.ToInt32(input["offerSeasonDetailID"]);
                    return bll.GetOfferLineByOfferSeasonDetail(offerSeasonDetailID, out notification);

                case "admin-update-sale-price":
                    offerSeasonDetailID = Convert.ToInt32(input["offerSeasonDetailID"]);
                    decimal? salePrice = Convert.ToDecimal(input["salePrice"]);
                    return bll.AdminUpdateSalePrice(offerSeasonDetailID, salePrice, userId, out notification);

                case "get-image-gallery":
                    offerSeasonID = Convert.ToInt32(input["offerSeasonID"]);
                    return bll.GetImageGallery(offerSeasonID, out notification);

                case "create-offer-season-sample":
                    offerSeasonDetailID = Convert.ToInt32(input["offerSeasonDetailID"]);
                    int clientID = Convert.ToInt32(input["clientID"]);
                    season = input["season"].ToString();
                    return bll.CreateOfferSeasonSample(offerSeasonDetailID, clientID, season, userId, out notification);

                case "get-related-factory-order-detail":
                    offerSeasonID = Convert.ToInt32(input["offerSeasonID"]);
                    return bll.GetRelatedFactoryOrderDetail(offerSeasonID, out notification);

                case "get-purchasing-price-last-year":
                    offerSeasonID = Convert.ToInt32(input["offerSeasonID"]);
                    return bll.GetPurchasingPriceLastYear(offerSeasonID, out notification);

                default:
                    break;
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public string identifier
        {
            get
            {
                return _identifier;
            }
            set
            {
                _identifier = value;
            }
        }

        private string _identifier = string.Empty;
    }
}
