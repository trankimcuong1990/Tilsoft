using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductOverviewRpt.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private ProductOverviewRptEntities CreateContext()
        {
            return new ProductOverviewRptEntities(Library.Helper.CreateEntityConnectionString("/DAL.ProductOverviewRptModel"));
        }

        public DTO.ModelDTO GetOverviewData(int id, int? offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ModelDTO Data = new DTO.ModelDTO();
            try
            {
                using (ProductOverviewRptEntities context = CreateContext())
                {
                    ProductOverviewRpt_Model_View dbItem = context.ProductOverviewRpt_Model_View
                        .Include("ProductOverviewRpt_PriceOverview_View")
                        .FirstOrDefault(o => o.ModelID == id);                    

                    if (dbItem == null)
                    {
                        throw new Exception("Model not found!");
                    }
                   
                    DTO.ModelDTO data = converter.DB2DTO_Model(dbItem);

                    if (offerSeasonDetailID.HasValue)
                    {
                        var dbOfferDetail = context.ProductOverviewRpt_OfferSeasonDetail_View.Where(o => o.OfferSeasonDetailID == offerSeasonDetailID).FirstOrDefault();
                        if (dbOfferDetail != null)
                        {
                            data.ArticleCode = dbOfferDetail.ArticleCode;
                            data.Description = dbOfferDetail.Description;

                            data.PackagingMethodNM = dbOfferDetail.PackagingMethodNM;
                            data.ClientUD = dbOfferDetail.ClientUD;
                            data.PurchasingPrice = dbOfferDetail.PurchasingPrice;                            
                        }
                    }
                    
                    return data;
                }
            }
            catch (Exception ex)
            {
                ex = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return null;
        }

        public DTO.PriceComparison.FormData GetItemToCompareData(int id, int? offerSeasonDetailID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success};
            DTO.PriceComparison.FormData data = new DTO.PriceComparison.FormData();
            data.Data = new DTO.PriceComparison.ProductOptionDetailDTO();
            data.Factories = new List<Support.DTO.Factory>();
            data.QuotationStatuses = new List<Support.DTO.QuotationStatus>();
            try
            {
                using (ProductOverviewRptEntities context = CreateContext())
                {
                    if (offerSeasonDetailID.HasValue && offerSeasonDetailID.Value !=0 )
                    {
                        ProductOverviewRpt_OfferSeasonDetail_View dbItem = context.ProductOverviewRpt_OfferSeasonDetail_View.FirstOrDefault(o => o.OfferSeasonDetailID == offerSeasonDetailID);
                        if (dbItem == null)
                        {
                            throw new Exception("Item not found!");
                        }
                        data.Data = AutoMapper.Mapper.Map<ProductOverviewRpt_OfferSeasonDetail_View, DTO.PriceComparison.ProductOptionDetailDTO>(dbItem);
                    }
                    else {
                        ProductOverviewRpt_PriceComparison_ProductOptionDetail_View dbItem = context.ProductOverviewRpt_PriceComparison_ProductOptionDetail_View.FirstOrDefault(o => o.FactoryOrderDetailID == id);
                        if (dbItem == null)
                        {
                            throw new Exception("Item not found!");
                        }
                        data.Data = converter.DB2DTO_PriceComparison_ProductOptionDetail(dbItem);
                    }
                }

                data.Factories = supportFactory.GetFactory().ToList();
                data.QuotationStatuses = supportFactory.GetQuotationStatus().ToList();
                return data;
            }
            catch (Exception ex)
            {
                ex = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return null;
        }

        public List<DTO.PriceComparison.ProductOptionDetailDTO> GetComparableItemData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PriceComparison.ProductOptionDetailDTO> result = new List<DTO.PriceComparison.ProductOptionDetailDTO>();

            //try to get data
            try
            {
                using (ProductOverviewRptEntities context = CreateContext())
                {
                    string Season = null;
                    int? FactoryID = null;
                    int? QuotationStatusID = null;
                    int? ModelID = null;
                    int? FactoryOrderDetailID = null;
                    int? MaterialID = null;
                    int? MaterialTypeID = null;
                    int? MaterialColorID = null;
                    int? FrameMaterialID = null;
                    int? FrameMaterialColorID = null;
                    int? SubMaterialID = null;
                    int? SubMaterialColorID = null;
                    int? BackCushionID = null;
                    int? SeatCushionID = null;
                    int? CushionColorID = null;
                    int? FSCTypeID = null;
                    int? FSCPercentID = null;
                    bool? UseFSCLabel = null;
                    int? PackagingMethodID = null;
                    int? ClientSpecialPackagingMethodID = null;
                    if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("QuotationStatusID") && filters["QuotationStatusID"] != null && !string.IsNullOrEmpty(filters["QuotationStatusID"].ToString()))
                    {
                        QuotationStatusID = Convert.ToInt32(filters["QuotationStatusID"].ToString());
                    }
                    if (filters.ContainsKey("ModelID") && filters["ModelID"] != null && !string.IsNullOrEmpty(filters["ModelID"].ToString()))
                    {
                        ModelID = Convert.ToInt32(filters["ModelID"].ToString());
                    }
                    if (filters.ContainsKey("FactoryOrderDetailID") && filters["FactoryOrderDetailID"] != null && !string.IsNullOrEmpty(filters["FactoryOrderDetailID"].ToString()))
                    {
                        FactoryOrderDetailID = Convert.ToInt32(filters["FactoryOrderDetailID"].ToString());
                    }
                    if (filters.ContainsKey("MaterialID") && filters["MaterialID"] != null && !string.IsNullOrEmpty(filters["MaterialID"].ToString()))
                    {
                        MaterialID = Convert.ToInt32(filters["MaterialID"].ToString());
                    }
                    if (filters.ContainsKey("MaterialTypeID") && filters["MaterialTypeID"] != null && !string.IsNullOrEmpty(filters["MaterialTypeID"].ToString()))
                    {
                        MaterialTypeID = Convert.ToInt32(filters["MaterialTypeID"].ToString());
                    }
                    if (filters.ContainsKey("MaterialColorID") && filters["MaterialColorID"] != null && !string.IsNullOrEmpty(filters["MaterialColorID"].ToString()))
                    {
                        MaterialColorID = Convert.ToInt32(filters["MaterialColorID"].ToString());
                    }
                    if (filters.ContainsKey("FrameMaterialID") && filters["FrameMaterialID"] != null && !string.IsNullOrEmpty(filters["FrameMaterialID"].ToString()))
                    {
                        FrameMaterialID = Convert.ToInt32(filters["FrameMaterialID"].ToString());
                    }
                    if (filters.ContainsKey("FrameMaterialColorID") && filters["FrameMaterialColorID"] != null && !string.IsNullOrEmpty(filters["FrameMaterialColorID"].ToString()))
                    {
                        FrameMaterialColorID = Convert.ToInt32(filters["FrameMaterialColorID"].ToString());
                    }
                    if (filters.ContainsKey("SubMaterialID") && filters["SubMaterialID"] != null && !string.IsNullOrEmpty(filters["SubMaterialID"].ToString()))
                    {
                        SubMaterialID = Convert.ToInt32(filters["SubMaterialID"].ToString());
                    }
                    if (filters.ContainsKey("SubMaterialColorID") && filters["SubMaterialColorID"] != null && !string.IsNullOrEmpty(filters["SubMaterialColorID"].ToString()))
                    {
                        SubMaterialColorID = Convert.ToInt32(filters["SubMaterialColorID"].ToString());
                    }                    
                    if (filters.ContainsKey("BackCushionID") && filters["BackCushionID"] != null && !string.IsNullOrEmpty(filters["BackCushionID"].ToString()))
                    {
                        BackCushionID = Convert.ToInt32(filters["BackCushionID"].ToString());
                    }
                    if (filters.ContainsKey("SeatCushionID") && filters["SeatCushionID"] != null && !string.IsNullOrEmpty(filters["SeatCushionID"].ToString()))
                    {
                        SeatCushionID = Convert.ToInt32(filters["SeatCushionID"].ToString());
                    }
                    if (filters.ContainsKey("CushionColorID") && filters["CushionColorID"] != null && !string.IsNullOrEmpty(filters["CushionColorID"].ToString()))
                    {
                        CushionColorID = Convert.ToInt32(filters["CushionColorID"].ToString());
                    }
                    if (filters.ContainsKey("FSCTypeID") && filters["FSCTypeID"] != null && !string.IsNullOrEmpty(filters["FSCTypeID"].ToString()))
                    {
                        FSCTypeID = Convert.ToInt32(filters["FSCTypeID"].ToString());
                    }
                    if (filters.ContainsKey("FSCPercentID") && filters["FSCPercentID"] != null && !string.IsNullOrEmpty(filters["FSCPercentID"].ToString()))
                    {
                        FSCPercentID = Convert.ToInt32(filters["FSCPercentID"].ToString());
                    }
                    if (filters.ContainsKey("UseFSCLabel") && filters["UseFSCLabel"] != null && !string.IsNullOrEmpty(filters["UseFSCLabel"].ToString()))
                    {
                        UseFSCLabel = Convert.ToBoolean(filters["UseFSCLabel"].ToString());
                    }
                    if (filters.ContainsKey("PackagingMethodID") && filters["PackagingMethodID"] != null && !string.IsNullOrEmpty(filters["PackagingMethodID"].ToString()))
                    {
                        PackagingMethodID = Convert.ToInt32(filters["PackagingMethodID"].ToString());
                    }
                    if (filters.ContainsKey("ClientSpecialPackagingMethodID") && filters["ClientSpecialPackagingMethodID"] != null && !string.IsNullOrEmpty(filters["ClientSpecialPackagingMethodID"].ToString()))
                    {
                        ClientSpecialPackagingMethodID = Convert.ToInt32(filters["ClientSpecialPackagingMethodID"].ToString());
                    }
                    result = converter.DB2DTO_PriceComparison_ProductOptionDetailList(
                        context.ProductOverviewRpt_function_SearchComparableItem(
                            Season
                            , FactoryID
                            , QuotationStatusID
                            , ModelID
                            , FactoryOrderDetailID
                            , MaterialID
                            , MaterialTypeID
                            , MaterialColorID
                            , FrameMaterialID
                            , FrameMaterialColorID
                            , SubMaterialID
                            , SubMaterialColorID
                            , BackCushionID
                            , SeatCushionID
                            , CushionColorID
                            , FSCTypeID
                            , FSCPercentID
                            , UseFSCLabel
                            , PackagingMethodID
                            , ClientSpecialPackagingMethodID
                            ).ToList());
                }
            }
            catch (Exception ex)
            {
                ex = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }

        public List<DTO.PriceComparison.ProductOptionDetailDTO> GetBestComparableItemData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PriceComparison.ProductOptionDetailDTO> result = new List<DTO.PriceComparison.ProductOptionDetailDTO>();

            //try to get data
            try
            {
                using (ProductOverviewRptEntities context = CreateContext())
                {
                    string Season = null;
                    int? FactoryID = null;
                    int? QuotationStatusID = null;
                    int? ModelID = null;
                    int? FactoryOrderDetailID = null;
                    int? MaterialID = null;
                    int? MaterialTypeID = null;
                    int? MaterialColorID = null;
                    int? FrameMaterialID = null;
                    int? FrameMaterialColorID = null;
                    int? SubMaterialID = null;
                    int? SubMaterialColorID = null;
                    int? BackCushionID = null;
                    int? SeatCushionID = null;
                    int? CushionColorID = null;
                    int? FSCTypeID = null;
                    int? FSCPercentID = null;
                    bool? UseFSCLabel = null;
                    int? PackagingMethodID = null;
                    int? ClientSpecialPackagingMethodID = null;
                    if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("QuotationStatusID") && filters["QuotationStatusID"] != null && !string.IsNullOrEmpty(filters["QuotationStatusID"].ToString()))
                    {
                        QuotationStatusID = Convert.ToInt32(filters["QuotationStatusID"].ToString());
                    }
                    if (filters.ContainsKey("ModelID") && filters["ModelID"] != null && !string.IsNullOrEmpty(filters["ModelID"].ToString()))
                    {
                        ModelID = Convert.ToInt32(filters["ModelID"].ToString());
                    }
                    if (filters.ContainsKey("FactoryOrderDetailID") && filters["FactoryOrderDetailID"] != null && !string.IsNullOrEmpty(filters["FactoryOrderDetailID"].ToString()))
                    {
                        FactoryOrderDetailID = Convert.ToInt32(filters["FactoryOrderDetailID"].ToString());
                    }
                    if (filters.ContainsKey("MaterialID") && filters["MaterialID"] != null && !string.IsNullOrEmpty(filters["MaterialID"].ToString()))
                    {
                        MaterialID = Convert.ToInt32(filters["MaterialID"].ToString());
                    }
                    if (filters.ContainsKey("MaterialTypeID") && filters["MaterialTypeID"] != null && !string.IsNullOrEmpty(filters["MaterialTypeID"].ToString()))
                    {
                        MaterialTypeID = Convert.ToInt32(filters["MaterialTypeID"].ToString());
                    }
                    if (filters.ContainsKey("MaterialColorID") && filters["MaterialColorID"] != null && !string.IsNullOrEmpty(filters["MaterialColorID"].ToString()))
                    {
                        MaterialColorID = Convert.ToInt32(filters["MaterialColorID"].ToString());
                    }
                    if (filters.ContainsKey("FrameMaterialID") && filters["FrameMaterialID"] != null && !string.IsNullOrEmpty(filters["FrameMaterialID"].ToString()))
                    {
                        FrameMaterialID = Convert.ToInt32(filters["FrameMaterialID"].ToString());
                    }
                    if (filters.ContainsKey("FrameMaterialColorID") && filters["FrameMaterialColorID"] != null && !string.IsNullOrEmpty(filters["FrameMaterialColorID"].ToString()))
                    {
                        FrameMaterialColorID = Convert.ToInt32(filters["FrameMaterialColorID"].ToString());
                    }
                    if (filters.ContainsKey("SubMaterialID") && filters["SubMaterialID"] != null && !string.IsNullOrEmpty(filters["SubMaterialID"].ToString()))
                    {
                        SubMaterialID = Convert.ToInt32(filters["SubMaterialID"].ToString());
                    }
                    if (filters.ContainsKey("SubMaterialColorID") && filters["SubMaterialColorID"] != null && !string.IsNullOrEmpty(filters["SubMaterialColorID"].ToString()))
                    {
                        SubMaterialColorID = Convert.ToInt32(filters["SubMaterialColorID"].ToString());
                    }
                    if (filters.ContainsKey("BackCushionID") && filters["BackCushionID"] != null && !string.IsNullOrEmpty(filters["BackCushionID"].ToString()))
                    {
                        BackCushionID = Convert.ToInt32(filters["BackCushionID"].ToString());
                    }
                    if (filters.ContainsKey("SeatCushionID") && filters["SeatCushionID"] != null && !string.IsNullOrEmpty(filters["SeatCushionID"].ToString()))
                    {
                        SeatCushionID = Convert.ToInt32(filters["SeatCushionID"].ToString());
                    }
                    if (filters.ContainsKey("CushionColorID") && filters["CushionColorID"] != null && !string.IsNullOrEmpty(filters["CushionColorID"].ToString()))
                    {
                        CushionColorID = Convert.ToInt32(filters["CushionColorID"].ToString());
                    }
                    if (filters.ContainsKey("FSCTypeID") && filters["FSCTypeID"] != null && !string.IsNullOrEmpty(filters["FSCTypeID"].ToString()))
                    {
                        FSCTypeID = Convert.ToInt32(filters["FSCTypeID"].ToString());
                    }
                    if (filters.ContainsKey("FSCPercentID") && filters["FSCPercentID"] != null && !string.IsNullOrEmpty(filters["FSCPercentID"].ToString()))
                    {
                        FSCPercentID = Convert.ToInt32(filters["FSCPercentID"].ToString());
                    }
                    if (filters.ContainsKey("UseFSCLabel") && filters["UseFSCLabel"] != null && !string.IsNullOrEmpty(filters["UseFSCLabel"].ToString()))
                    {
                        UseFSCLabel = Convert.ToBoolean(filters["UseFSCLabel"].ToString());
                    }
                    if (filters.ContainsKey("PackagingMethodID") && filters["PackagingMethodID"] != null && !string.IsNullOrEmpty(filters["PackagingMethodID"].ToString()))
                    {
                        PackagingMethodID = Convert.ToInt32(filters["PackagingMethodID"].ToString());
                    }
                    if (filters.ContainsKey("ClientSpecialPackagingMethodID") && filters["ClientSpecialPackagingMethodID"] != null && !string.IsNullOrEmpty(filters["ClientSpecialPackagingMethodID"].ToString()))
                    {
                        ClientSpecialPackagingMethodID = Convert.ToInt32(filters["ClientSpecialPackagingMethodID"].ToString());
                    }
                    result = converter.DB2DTO_PriceComparison_ProductOptionDetailList(
                        context.ProductOverviewRpt_function_SearchBestComparableItem(
                            Season
                            , FactoryID
                            , QuotationStatusID
                            , ModelID
                            , FactoryOrderDetailID
                            , MaterialID
                            , MaterialTypeID
                            , MaterialColorID
                            , FrameMaterialID
                            , FrameMaterialColorID
                            , SubMaterialID
                            , SubMaterialColorID
                            , BackCushionID
                            , SeatCushionID
                            , CushionColorID
                            , FSCTypeID
                            , FSCPercentID
                            , UseFSCLabel
                            , PackagingMethodID
                            , ClientSpecialPackagingMethodID
                            ).ToList());
                }
            }
            catch (Exception ex)
            {
                ex = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }

        public List<DTO.PriceComparison.PriceOfferHistoryDTO> GetPriceOfferHistory(int id, out Library.DTO.Notification notification) // FactotyOrderDetailID
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PriceComparison.PriceOfferHistoryDTO> result = new List<DTO.PriceComparison.PriceOfferHistoryDTO>();

            //try to get data
            try
            {
                using (ProductOverviewRptEntities context = CreateContext())
                {
                    result = converter.DB2DTO_PriceComparison_PriceOfferHistoryList(context.ProductOverviewRpt_PriceComparison_PriceOfferHistory_View.Where(o => o.FactoryOrderDetailID == id).ToList());
                }
            }
            catch (Exception ex)
            {
                ex = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }
    }
}
