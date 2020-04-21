//using DAL.LabelingPackagingMng;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAL.LabelingPackagingMng
//{
//    public class DataFactory : DALBase.FactoryBase<DTO.LabelingPackagingMng.LabelingPackagingSearchResult, DTO.LabelingPackagingMng.LabelingPackaging>
//    {
//        private DataConverter converter = new DataConverter();
//        private string _tempFolder;
//        public DataFactory() { }
//        public DataFactory(string tempFolder)
//        {
//            _tempFolder = tempFolder + @"\";
//        }

//        private LabelingPackagingMngEntities CreateContext()
//        {
//            return new LabelingPackagingMngEntities(DALBase.Helper.CreateEntityConnectionString("LabelingPackagingMngModel"));
//        }

//        public override IEnumerable<DTO.LabelingPackagingMng.LabelingPackagingSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string OrderBy, string OrderDirection, out int totalRows, out Library.DTO.Notification notification)
//        {
//            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
//            totalRows = 0;

//            //try to get data
//            try
//            {
//                using (LabelingPackagingMngEntities context = CreateContext())
//                {
//                    string ProformaInvoiceNo = null;
//                    string ClientUD = null;
//                    string ClientNM = null;


//                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
//                    {
//                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
//                    }

//                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
//                    {
//                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
//                    }

//                    if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
//                    {
//                        ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
//                    }



//                    totalRows = context.LabelinPackagingMng_function_Search(ProformaInvoiceNo, ClientUD, ClientNM, OrderBy, OrderDirection).Count();
//                    var result = context.LabelinPackagingMng_function_Search(ProformaInvoiceNo, ClientUD, ClientNM, OrderBy, OrderDirection);

//                    return converter.DB2DTO_LabelingPackagingSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
//                }
//            }
//            catch (Exception ex)
//            {
//                notification = new Library.DTO.Notification() { Message = ex.Message };
//                return new List<DTO.LabelingPackagingMng.LabelingPackagingSearchResult>();
//            }
//        }

//        //public override DTO.LabelingPackagingMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
           
//        //    DTO.LabelingPackagingMng.EditFormData data = new DTO.LabelingPackagingMng.EditFormData();
//        //    data.Data = new DTO.LabelingPackagingMng.LabelingPackaging();

//        //    //try to get data
//        //    try
//        //    {
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {data.Data = converter.DB2DTO_LabelingPackaging(context.ProformaInvoiceEdit_View.FirstOrDefault(o => o.SaleOrderID == id));
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        notification.Type = Library.DTO.NotificationType.Error;
//        //        notification.Message = ex.Message;
//        //    }

//        //    return data;
  
            
//        //          }
//        public override bool UpdateData(int id, ref DTO.LabelingPackagingMng.LabelingPackaging  dtoItem, out Library.DTO.Notification notification)
//        {
//            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
//            int number;
//            string indexName;

//            try
//            {
//                using (LabelingPackagingMngEntities context = CreateContext())
//                {
//                    LabelingPackaging dbItem = null;
//                    if (id == 0)
//                    {
//                        dbItem = new LabelingPackaging();
//                        context.LabelingPackaging.Add(dbItem);
//                    }
//                    else
//                    {
//                        dbItem = context.LabelingPackaging.FirstOrDefault(o => o.LabelingPackagingID == id);
//                    }

//                    if (dbItem == null)
//                    {
//                        notification.Message = "Labeling Packaging not found!";
//                        return false;
//                    }
//                    else
//                    {
//                        converter.DTO2BD_LabelingPackaging(dtoItem, ref dbItem);
//                        context.SaveChanges();

//                        dtoItem = GetData(dbItem.LabelingPackagingID, out notification).Data;

//                        return true;
//                    }
//                }
//            }
//            catch (System.Data.DataException dEx)
//            {
//                notification.Type = Library.DTO.NotificationType.Error;
//                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
//                if (number == 2601 && !string.IsNullOrEmpty(indexName))
//                {
//                    if (indexName == "MaterialUDUnique")
//                    {
//                        notification.Message = "The Material Code is already exists";
//                    }
//                }
//                else
//                {
//                    notification.Message = dEx.Message;
//                }

//                return false;
//            }
//            catch (Exception ex)
//            {
//                notification.Type = Library.DTO.NotificationType.Error;
//                notification.Message = ex.Message;
//                return false;
//            }
//        }

//        //public DTO.LabelingPackagingMng.SupportData GetDataContainer(int id, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

//        //    //try to get data
//        //    try
//        //    {
//        //        DAL.Support.DataFactory factory = new Support.DataFactory();
//        //        DTO.OfferMng.DataContainer dtoItem = new DTO.OfferMng.DataContainer();
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            if (id > 0)
//        //            {
//        //                ProformaInvoiceList_View dbItem;
//        //                dbItem = context.ProformaInvoiceList_View
//        //                    .Include("GetLabalingPackagingProcessList_View")
//        //                    .Include("List_PackagingMethod_View")
//        //                    .Include("GetPackagingTypes_View")
//        //                    .FirstOrDefault(o => o.SaleOrderID == id)
//        //                    ;
//        //                dtoItem.OfferData = converter.DB2DTO_Offer(dbItem);
//        //            }
//        //            else
//        //            {
//        //                dtoItem.OfferData = new DTO.OfferMng.Offer();
//        //                dtoItem.OfferData.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
//        //                dtoItem.OfferData.OfferVersion = 1;
//        //                dtoItem.OfferData.Season = "2015/2016";
//        //                dtoItem.OfferData.OfferDateFormated = DateTime.Now.ToString("dd/MM/yyy");
//        //                dtoItem.OfferData.Currency = "USD";

//        //                dtoItem.OfferData.OfferLines = new List<DTO.OfferMng.OfferLine>();
//        //                dtoItem.OfferData.OfferLineSpareparts = new List<DTO.OfferMng.OfferLineSparepart>();
//        //            }
//        //        }
//        //        //GET SUPPORT DATA
//        //        dtoItem.SupportData = new DTO.OfferMng.SupportDataContainer();
//        //        dtoItem.SupportData.Seasons = factory.GetSeason().ToList();
//        //        dtoItem.SupportData.Sales = factory.GetSaler().ToList();
//        //        dtoItem.SupportData.PaymentTerms = factory.GetPaymentTerm().ToList();
//        //        dtoItem.SupportData.DeliveryTerms = factory.GetDeliveryTerm().ToList();
//        //        dtoItem.SupportData.PODs = factory.GetPOD().ToList();
//        //        dtoItem.SupportData.Currency = factory.GetCurrency().ToList();
//        //        dtoItem.SupportData.VATPercents = factory.GetVATPercent();
//        //        dtoItem.SupportData.LabelingTypes = factory.GetLabelingType().ToList();
//        //        dtoItem.SupportData.PackagingTypes = factory.GetPackagingType().ToList();
//        //        dtoItem.SupportData.Forwarders = factory.GetForwarder().ToList();
//        //        dtoItem.SupportData.PutInProductionTerms = factory.GetPutInProductionTerm().ToList();

//        //        //OFFER LINE SUPPORT
//        //        dtoItem.SupportData.POLs = factory.GetPOL().ToList();
//        //        dtoItem.SupportData.CommissionPercents = GetCommision().ToList();
//        //        //dtoItem.SupportData.PackagingMethods = factory.GetPackagingMethod().ToList();
//        //        //dtoItem.SupportData.ManufacturerCountrys = factory.GetManufacturerCountry().ToList();
//        //        //dtoItem.SupportData.FrameMaterials = factory.GetFrameMaterial().ToList();
//        //        dtoItem.SupportData.ModelStatuses = factory.GetModelStatus().ToList();
//        //        dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -1, ModelStatusNM = "TOTAL" });
//        //        dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -2, ModelStatusNM = "WAREHOUSE" });
//        //        return dtoItem;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        notification = new Library.DTO.Notification() { Message = ex.Message };
//        //        return new DTO.OfferMng.DataContainer();
//        //    }
//        //}
//        public override bool DeleteData(int id, out Library.DTO.Notification notification)
//        {
//            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

//            try
//            {
//                using (LabelingPackagingMngEntities context = CreateContext())
//                {
//                    ProformaInvoiceList_View dbItem = context.ProformaInvoiceList_View.FirstOrDefault(o => o.SaleOrderID == id);
//                    if (dbItem == null)
//                    {
//                        notification.Message = "Labeling not found!";
//                        return false;
//                    }
//                    else
//                    {
//                        context.ProformaInvoiceList_View.Remove(dbItem);
//                        context.SaveChanges();

//                        return true;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                notification.Type = Library.DTO.NotificationType.Error;
//                notification.Message = ex.Message;
//                return false;
//            }
//        }


        
//        //public DTO.LabelingPackagingMng.SupportDataContainer GetSupportData(out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
//        //    DAL.Support.DataFactory factory = new Support.DataFactory();

//        //    //try to get data
//        //    try
//        //    {
//        //        DTO.LabelingPackagingMng.SupportDataContainer dtoItem = new DTO.LabelingPackagingMng.SupportDataContainer();
//        //        dtoItem.Seasons = factory.GetSeason().ToList();
//        //        return dtoItem;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        notification = new Library.DTO.Notification() { Message = ex.Message };
//        //        return new DTO.LabelingPackagingMng.SupportDataContainer();
//        //    }
//        //}

//        //private IEnumerable<DTO.LabelingPackagingMng.RatePercent> GetVAT()
//        //{
//        //    List<DTO.LabelingPackagingMng.RatePercent> VATs = new List<DTO.LabelingPackagingMng.RatePercent>();

//        //    VATs.Add(new DTO.LabelingPackagingMng.RatePercent { RatePercentValue = 0, RatePercentText = "0%" });
//        //    VATs.Add(new DTO.LabelingPackagingMng.RatePercent { RatePercentValue = 19, RatePercentText = "19%" });
//        //    VATs.Add(new DTO.LabelingPackagingMng.RatePercent { RatePercentValue = 21, RatePercentText = "21%" });

//        //    return VATs;
//        //}

//        //private IEnumerable<DTO.LabelingPackagingMng.RatePercent> GetCommision()
//        //{
//        //    List<DTO.LabelingPackagingMng.RatePercent> Commissions = new List<DTO.LabelingPackagingMng.RatePercent>();

//        //    Commissions.Add(new DTO.LabelingPackagingMng.RatePercent { RatePercentValue = 8, RatePercentText = "8%" });
//        //    Commissions.Add(new DTO.LabelingPackagingMng.RatePercent { RatePercentValue = 15, RatePercentText = "15%" });

//        //    return Commissions;
//        //}

//        //public DTO.LabelingPackagingMng.SupportData GetDataContainer(int id, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

//        //    //try to get data
//        //    try
//        //    {
//        //        DAL.Support.DataFactory factory = new Support.DataFactory();
//        //        DTO.LabelingPackagingMng.SupportData dtoItem = new DTO.LabelingPackagingMng.SupportData();
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            if (id > 0)
//        //            {
//        //                ProformaInvoiceList_View dbItem;
//        //                dbItem = context.ProformaInvoiceList_View
//        //                    .Include("ProformaInvoiceList_View")
//        //                    .Include("GetLabalingPackagingProcessList_View")
//        //                    .FirstOrDefault(o => o.SaleOrderID == id)
//        //                    ;
//        //                dtoItem.LabelingPackagingData = converter.DB2DTO_LabelingPackaging(dbItem);
//        //            }
//        //            else
//        //            {
//        //                dtoItem.LabelingPackagingData = new DTO.LabelingPackagingMng.LabelingPackaging();
//        //                dtoItem.LabelingPackagingData.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
//        //                dtoItem.LabelingPackagingData.LabelingPackagingVersion = 1;
//        //                dtoItem.LabelingPackagingData.Season = "2015/2016";
//        //                dtoItem.LabelingPackagingData.LabelingPackagingDateFormated = DateTime.Now.ToString("dd/MM/yyy");
//        //                dtoItem.LabelingPackagingData.Currency = "USD";

//        //                dtoItem.LabelingPackagingData.LabelingPackagingLines = new List<DTO.LabelingPackagingMng.LabelingPackagingLine>();
//        //                dtoItem.LabelingPackagingData.LabelingPackagingLineSpareparts = new List<DTO.LabelingPackagingMng.LabelingPackagingLineSparepart>();
//        //            }
//        //        }
//        //        //GET SUPPORT DATA
//        //        dtoItem.SupportData = new DTO.LabelingPackagingMng.SupportDataContainer();
//        //        dtoItem.SupportData.Seasons = factory.GetSeason().ToList();
//        //        dtoItem.SupportData.Sales = factory.GetSaler().ToList();
//        //        dtoItem.SupportData.PaymentTerms = factory.GetPaymentTerm().ToList();
//        //        dtoItem.SupportData.DeliveryTerms = factory.GetDeliveryTerm().ToList();
//        //        dtoItem.SupportData.PODs = factory.GetPOD().ToList();
//        //        dtoItem.SupportData.Currency = factory.GetCurrency().ToList();
//        //        dtoItem.SupportData.VATPercents = factory.GetVATPercent();
//        //        dtoItem.SupportData.LabelingTypes = factory.GetLabelingType().ToList();
//        //        dtoItem.SupportData.PackagingTypes = factory.GetPackagingType().ToList();
//        //        dtoItem.SupportData.Forwarders = factory.GetForwarder().ToList();
//        //        dtoItem.SupportData.PutInProductionTerms = factory.GetPutInProductionTerm().ToList();

//        //        //LabelingPackaging LINE SUPPORT
//        //        dtoItem.SupportData.POLs = factory.GetPOL().ToList();
//        //        dtoItem.SupportData.CommissionPercents = GetCommision().ToList();
//        //        //dtoItem.SupportData.PackagingMethods = factory.GetPackagingMethod().ToList();
//        //        //dtoItem.SupportData.ManufacturerCountrys = factory.GetManufacturerCountry().ToList();
//        //        //dtoItem.SupportData.FrameMaterials = factory.GetFrameMaterial().ToList();
//        //        dtoItem.SupportData.ModelStatuses = factory.GetModelStatus().ToList();
//        //        dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -1, ModelStatusNM = "TOTAL" });
//        //        dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -2, ModelStatusNM = "WAREHOUSE" });
//        //        return dtoItem;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        notification = new Library.DTO.Notification() { Message = ex.Message };
//        //        return new DTO.LabelingPackagingMng.DataContainer();
//        //    }
//        //}

//        //public IEnumerable<DTO.LabelingPackagingMng.LabelingPackagingLine> SearchLabelingPackagingLines(int LabelingPackagingID, out int totalRows, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
//        //    totalRows = 0;
//        //    //try to get data
//        //    try
//        //    {
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            totalRows = context.LabelingPackagingMng_LabelingPackagingLine_View.Where(o => o.LabelingPackagingID == LabelingPackagingID).Count();
//        //            var result = context.LabelingPackagingMng_LabelingPackagingLine_View.Where(o => o.LabelingPackagingID == LabelingPackagingID);
//        //            return converter.DB2DTO_LabelingPackagingLine(result.ToList());
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        notification = new Library.DTO.Notification() { Message = ex.Message };
//        //        return new List<DTO.LabelingPackagingMng.LabelingPackagingLine>();
//        //    }
//        //}

//        //private void SetLabelingPackagingStatus(int? LabelingPackagingID, int trackingStatusID, int setStatusBy)
//        //{
//        //    using (LabelingPackagingMngEntities context = CreateContext())
//        //    {
//        //        foreach (LabelingPackagingStatus item in context.LabelingPackagingStatus.Where(o => o.LabelingPackagingID == LabelingPackagingID))
//        //        {
//        //            item.IsCurrentStatus = false;
//        //        }

//        //        LabelingPackagingStatus dbStatus = new LabelingPackagingStatus();
//        //        dbStatus.LabelingPackagingID = LabelingPackagingID;
//        //        dbStatus.TrackingStatusID = trackingStatusID;
//        //        dbStatus.StatusDate = DateTime.Now;
//        //        dbStatus.UpdatedBy = setStatusBy;
//        //        dbStatus.IsCurrentStatus = true;
//        //        context.LabelingPackagingStatus.Add(dbStatus);

//        //        context.SaveChanges();
//        //    }
//        //}

//        //public bool Confirm(int LabelingPackagingID, int actionType, ref DTO.LabelingPackagingMng.LabelingPackaging dtoItem, int setStatusBy, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "LabelingPackaging has been confirmed success" };
//        //    try
//        //    {
//        //        //SAVE DATA
//        //        Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
//        //        if (!UpdateData(LabelingPackagingID, actionType, ref dtoItem, out save_nofication))
//        //        {
//        //            notification = save_nofication;
//        //            return false;
//        //        }
//        //        int? ID = dtoItem.LabelingPackagingID;

//        //        //VALIDATE && CONFIRM
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            LabelingPackagingStatus dbLabelingPackagingStatus = context.LabelingPackagingStatus.Where(o => o.LabelingPackagingID == ID && o.IsCurrentStatus == true).FirstOrDefault();
//        //            if (dbLabelingPackagingStatus.TrackingStatusID != DALBase.Helper.REVISED && dbLabelingPackagingStatus.TrackingStatusID != DALBase.Helper.CREATED)
//        //            {
//        //                notification = new Library.DTO.Notification() { Message = "LabelingPackaging must be in REVISED/CREATED status before confirm", Type = Library.DTO.NotificationType.Warning };
//        //                return false;
//        //            }

//        //            int TrackingStatusID = (dbLabelingPackagingStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.CONFIRMED : DALBase.Helper.REVISION_CONFIRMED);
//        //            SetLabelingPackagingStatus(ID, TrackingStatusID, setStatusBy);
//        //            dtoItem.TrackingStatusNM = (dbLabelingPackagingStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.TEXT_STATUS_CONFIRMED : DALBase.Helper.TEXT_STATUS_REVISION_CONFIRMED);
//        //            dtoItem.TrackingStatusID = TrackingStatusID;
//        //            return true;
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (ex.InnerException == null)
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
//        //        }
//        //        else
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.InnerException.InnerException.Message, Type = Library.DTO.NotificationType.Error };
//        //        }
//        //        return false;
//        //    }
//        //}

//        //public bool Revise(int LabelingPackagingID, int actionType, ref DTO.LabelingPackagingMng.LabelingPackaging dtoItem, int setStatusBy, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "LabelingPackaging has been revised success" };
//        //    try
//        //    {
//        //        //SAVE DATA
//        //        Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
//        //        if (!UpdateData(LabelingPackagingID, actionType, ref dtoItem, out save_nofication))
//        //        {
//        //            notification = save_nofication;
//        //            return false;
//        //        }
//        //        int? ID = dtoItem.LabelingPackagingID;
//        //        //VALIDATE && REVISE
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            LabelingPackagingStatus dbLabelingPackagingStatus = context.LabelingPackagingStatus.Where(o => o.LabelingPackagingID == ID && o.IsCurrentStatus == true).FirstOrDefault();
//        //            if (dbLabelingPackagingStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbLabelingPackagingStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
//        //            {
//        //                notification = new Library.DTO.Notification() { Message = "LabelingPackaging must be in CONFIRMED status before revise", Type = Library.DTO.NotificationType.Warning };
//        //                return false;
//        //            }

//        //            SetLabelingPackagingStatus(ID, DALBase.Helper.REVISED, setStatusBy);
//        //            dtoItem.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_REVISED;
//        //            dtoItem.TrackingStatusID = DALBase.Helper.REVISED;
//        //            return true;
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (ex.InnerException == null)
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
//        //        }
//        //        else
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.InnerException.InnerException.Message, Type = Library.DTO.NotificationType.Error };
//        //        }
//        //        return false;
//        //    }
//        //}

//        //public bool UpdateData(int id, int actionType, ref DTO.LabelingPackagingMng.LabelingPackaging dtoItem, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
//        //    try
//        //    {
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            LabelingPackaging dbItem = null;
//        //            LabelingPackagingStatus dbStatus = null;

//        //            if (actionType == 0 || actionType == 2 || actionType == 3) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
//        //            {
//        //                dbItem = new LabelingPackaging();
//        //                context.LabelingPackaging.Add(dbItem);

//        //                //SET LabelingPackaging STATUS
//        //                dbStatus = new LabelingPackagingStatus();
//        //                dbStatus.TrackingStatusID = DALBase.Helper.CREATED;
//        //                dbStatus.StatusDate = DateTime.Now;
//        //                dbStatus.UpdatedBy = dtoItem.UpdatedBy;
//        //                dbStatus.IsCurrentStatus = true;
//        //                dbItem.LabelingPackagingStatus.Add(dbStatus);

//        //                //SET TRACKING INFO
//        //                dtoItem.CreatedBy = dtoItem.UpdatedBy;
//        //                dtoItem.CreatedDate = DateTime.Now;

//        //                switch (actionType)
//        //                {
//        //                    case 0: //NEW LabelingPackaging
//        //                        dtoItem.LabelingPackagingVersion = 1;
//        //                        dtoItem.LabelingPackagingUD = context.LabelingPackagingMng_function_GenerateLabelingPackagingCode(dtoItem.LabelingPackagingID, dtoItem.Season, dtoItem.SaleID, dtoItem.ClientID).FirstOrDefault();
//        //                        break;
//        //                    case 2: // COPY LabelingPackaging
//        //                        dtoItem.LabelingPackagingVersion = 1;
//        //                        dtoItem.LabelingPackagingUD = context.LabelingPackagingMng_function_GenerateLabelingPackagingCode(dtoItem.LabelingPackagingID, dtoItem.Season, dtoItem.SaleID, dtoItem.ClientID).FirstOrDefault();
//        //                        break;
//        //                    case 3: //NEW VERSION LabelingPackaging
//        //                        dtoItem.LabelingPackagingVersion = dtoItem.LabelingPackagingVersion + 1;
//        //                        break;
//        //                    default:
//        //                        break;
//        //                }
//        //            }
//        //            else if (id > 0)
//        //            {
//        //                dbItem = context.LabelingPackaging.FirstOrDefault(o => o.LabelingPackagingID == id);
//        //                if (dbItem.ClientID != dtoItem.ClientID || dbItem.SaleID != dtoItem.SaleID || dbItem.Season != dtoItem.Season)
//        //                {
//        //                    dtoItem.LabelingPackagingUD = context.LabelingPackagingMng_function_GenerateLabelingPackagingCode(dtoItem.LabelingPackagingID, dtoItem.Season, dtoItem.SaleID, dtoItem.ClientID).FirstOrDefault();
//        //                }
//        //            }

//        //            if (dbItem == null)
//        //            {
//        //                notification.Message = "LabelingPackaging not found!";
//        //                return false;
//        //            }
//        //            else
//        //            {
//        //                // check concurrency
//        //                if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
//        //                {
//        //                    throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
//        //                }

//        //                foreach (var item in dtoItem.LabelingPackagingLines)
//        //                {
//        //                    if (item.ModelID == null || item.MaterialID == null || item.MaterialTypeID == null || item.MaterialColorID == null
//        //                        || item.FrameMaterialID == null || item.FrameMaterialColorID == null
//        //                        || item.SubMaterialID == null || item.SubMaterialColorID == null
//        //                        || item.SeatCushionID == null || item.BackCushionID == null || item.CushionColorID == null
//        //                        || item.FSCTypeID == null || item.FSCPercentID == null)
//        //                    {
//        //                        notification = new Library.DTO.Notification() { Message = "Product : " + item.ArticleCode + " - " + item.Description + "<b> must be fill-in all attribute before confirm</b>", Type = Library.DTO.NotificationType.Warning };
//        //                        return false;
//        //                    }
//        //                }

//        //                foreach (var item in dtoItem.LabelingPackagingLineSpareparts)
//        //                {
//        //                    if (item.ModelID == null || item.PartID == null)
//        //                    {
//        //                        notification = new Library.DTO.Notification() { Message = "Sparepart : " + item.ArticleCode + " - " + item.Description + "<b> must be fill-in all attribute before confirm</b>", Type = Library.DTO.NotificationType.Warning };
//        //                        return false;
//        //                    }
//        //                }

//        //                //create article and description
//        //                foreach (DTO.LabelingPackagingMng.LabelingPackagingLine item in dtoItem.LabelingPackagingLines)
//        //                {
//        //                    item.ArticleCode = context.LabelingPackagingMng_function_GenerateArticleCode(item.ModelID, item.FrameMaterialID, item.FrameMaterialColorID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.SubMaterialID, item.SubMaterialColorID, item.SeatCushionID, item.BackCushionID, item.CushionColorID, item.ManufacturerCountryID, item.FSCTypeID, item.FSCPercentID).FirstOrDefault();
//        //                    item.Description = context.LabelingPackagingMng_function_GenerateDescription(item.ModelID, item.FrameMaterialID, item.FrameMaterialColorID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.SubMaterialID, item.SubMaterialColorID, item.SeatCushionID, item.BackCushionID, item.CushionColorID, item.CushionRemark, item.FSCTypeID, item.FSCPercentID).FirstOrDefault();
//        //                }

//        //                //create article and description for sparepart
//        //                foreach (DTO.LabelingPackagingMng.LabelingPackagingLineSparepart item in dtoItem.LabelingPackagingLineSpareparts)
//        //                {
//        //                    item.ArticleCode = context.LabelingPackagingMng_function_GenerateSparepartArticleCode(item.ModelID, item.PartID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.CushionID, item.CushionColorID, item.CountryID).FirstOrDefault();
//        //                    item.Description = context.LabelingPackagingMng_function_GenerateSparepartDescription(item.ModelID, item.PartID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.CushionID, item.CushionColorID).FirstOrDefault();
//        //                }

//        //                //read dto to db
//        //                converter.DTO2DB_LabelingPackaging(dtoItem, ref dbItem, actionType);

//        //                //delete LabelingPackagingline is null
//        //                context.LabelingPackagingLine.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingLine.Remove(o));

//        //                //delete LabelingPackagingline sparepart is null
//        //                context.LabelingPackagingLineSparepart.Local.Where(o => o.LabelingPackaging == null).ToList().ForEach(o => context.LabelingPackagingLineSparepart.Remove(o));

//        //                //save data
//        //                context.SaveChanges();

//        //                //reload data
//        //                dtoItem = GetData(dbItem.LabelingPackagingID, out notification);
//        //                return true;
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (ex.InnerException == null)
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Warning };
//        //        }
//        //        else
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.InnerException.InnerException.Message, Type = Library.DTO.NotificationType.Warning };
//        //        }
//        //        return false;
//        //    }
//        //}

//        //public bool DeleteLabelingPackaging(int id, int deletedBy, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "LabelingPackaging has been deleted" };
//        //    try
//        //    {
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            LabelingPackagingStatus dbStatus = context.LabelingPackagingStatus.Where(o => o.LabelingPackagingID == id && o.IsCurrentStatus == true).FirstOrDefault();
//        //            if (dbStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
//        //            {
//        //                throw new Exception("LabelingPackaging was confirmed. You can not delete");
//        //            }

//        //            if (dbStatus.TrackingStatusID == DALBase.Helper.REVISED)
//        //            {
//        //                throw new Exception("LabelingPackaging was revised. You can not delete");
//        //            }

//        //            context.LabelingPackagingMng_function_DeleteLabelingPackaging(id, deletedBy);
//        //            return true;
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (ex.InnerException == null)
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
//        //        }
//        //        else
//        //        {
//        //            notification = new Library.DTO.Notification() { Message = ex.InnerException.InnerException.Message, Type = Library.DTO.NotificationType.Error };
//        //        }
//        //        return false;
//        //    }
//        //}


//        //public DTO.LabelingPackagingMng.LabelingPackagingLineEdit GetLabelingPackagingLineEdit(int LabelingPackagingLineID, out Library.DTO.Notification notification)
//        //{
//        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
//        //    DAL.Support.DataFactory supportFactory = new Support.DataFactory();
//        //    //try to get data
//        //    try
//        //    {
//        //        DTO.LabelingPackagingMng.LabelingPackagingLineEdit data = new DTO.LabelingPackagingMng.LabelingPackagingLineEdit();
//        //        if (LabelingPackagingLineID > 0)
//        //        {
//        //            using (LabelingPackagingMngEntities context = CreateContext())
//        //            {
//        //                LabelingPackagingMng_LabelingPackagingLine_EditView dbItem = context.LabelingPackagingMng_LabelingPackagingLine_EditView.Where(o => o.LabelingPackagingLineID == LabelingPackagingLineID).FirstOrDefault();
//        //                data = converter.DB2DTO_LabelingPackagingLineEdit(dbItem);
//        //            }
//        //        }
//        //        else
//        //        {
//        //            data.MaterialThumbnailLocation = "no-image.jpg";
//        //            data.FrameMaterialThumbnailLocation = "no-image.jpg";
//        //            data.SubMaterialColorThumbnailLocation = "no-image.jpg";
//        //            data.CushionColorThumbnailLocation = "no-image.jpg";
//        //            data.BackCushionThumbnailLocation = "no-image.jpg";
//        //            data.SeatCushionThumbnailLocation = "no-image.jpg";
//        //        }

//        //        return data;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        notification = new Library.DTO.Notification() { Message = ex.Message };
//        //        return new DTO.LabelingPackagingMng.LabelingPackagingLineEdit();
//        //    }
//        //}

//        //public List<DTO.Support.ModelSparepart> GetModelSparepart(int modelID)
//        //{
//        //    DAL.Support.DataFactory supportFactory = new Support.DataFactory();
//        //    return supportFactory.GetModelSparepart(modelID).ToList();
//        //}

//        //public bool UploadLabelingPackagingScanFile(int LabelingPackagingID, bool LabelingPackagingScanFileHasChange, string newLabelingPackagingScanFile, out string LabelingPackagingScanFile, out string concurrencyFlag_String, out Library.DTO.Notification notification)
//        //{
//        //    LabelingPackagingScanFile = "";
//        //    concurrencyFlag_String = "";
//        //    notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = (!string.IsNullOrEmpty(newLabelingPackagingScanFile) ? "Upload LabelingPackaging scan file success" : "Remove LabelingPackaging scan file success") };

//        //    //file processing
//        //    DAL.Framework.DataFactory framework_factory = new Framework.DataFactory();
//        //    Library.FileHelper.FileManager fileMng = new Library.FileHelper.FileManager(FrameworkSetting.Setting.AbsoluteFileFolder);
//        //    string fileNeedDeleted = string.Empty;
//        //    string thumbnailFileNeedDeleted = string.Empty;

//        //    if (LabelingPackagingScanFileHasChange)
//        //    {
//        //        //check to delete file does not exist in database
//        //        if (!string.IsNullOrEmpty(LabelingPackagingScanFile))
//        //        {
//        //            framework_factory.GetDBFileLocation(LabelingPackagingScanFile, out fileNeedDeleted, out thumbnailFileNeedDeleted);
//        //            if (!string.IsNullOrEmpty(fileNeedDeleted))
//        //            {
//        //                try
//        //                {
//        //                    fileMng.DeleteFile(fileNeedDeleted);
//        //                }
//        //                catch { }
//        //            }
//        //        }
//        //        if (string.IsNullOrEmpty(newLabelingPackagingScanFile))
//        //        {
//        //            // remove file registration in File table
//        //            framework_factory.RemoveFile(LabelingPackagingScanFile);
//        //            // reset file in table Contract
//        //            LabelingPackagingScanFile = string.Empty;
//        //        }
//        //        else
//        //        {
//        //            string outDBFileLocation = "";
//        //            string outFileFullPath = "";
//        //            string outFilePointer = "";
//        //            // copy new file
//        //            fileMng.StoreFile(_tempFolder + newLabelingPackagingScanFile, out outDBFileLocation, out outFileFullPath);

//        //            if (File.Exists(outFileFullPath))
//        //            {
//        //                FileInfo info = new FileInfo(outFileFullPath);
//        //                // insert/update file registration in database
//        //                framework_factory.UpdateFile(LabelingPackagingScanFile, newLabelingPackagingScanFile, outDBFileLocation, info.Extension, "", (int)info.Length, out outFilePointer);
//        //                // set file database pointer
//        //                LabelingPackagingScanFile = outFilePointer;
//        //            }
//        //        }

//        //        //Update back new file to LabelingPackaging
//        //        using (LabelingPackagingMngEntities context = CreateContext())
//        //        {
//        //            LabelingPackaging dbItem = context.LabelingPackaging.Where(o => o.LabelingPackagingID == LabelingPackagingID).FirstOrDefault();
//        //            dbItem.LabelingPackagingScanFile = LabelingPackagingScanFile;
//        //            context.SaveChanges();
//        //            concurrencyFlag_String = Convert.ToBase64String(context.LabelingPackaging.Where(o => o.LabelingPackagingID == LabelingPackagingID).FirstOrDefault().ConcurrencyFlag);
//        //        }
//        //    }
//        //    return true;
//        //}
//    }
//}
