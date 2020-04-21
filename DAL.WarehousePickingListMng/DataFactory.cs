using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace DAL.WarehousePickingListMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.WarehousePickingListMng.SearchFormData, DTO.WarehousePickingListMng.EditFormData, DTO.WarehousePickingListMng.WarehousePickingList>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();

        private WarehousePickingListMngEntities CreateContext()
        {
            var context = new WarehousePickingListMngEntities(DALBase.Helper.CreateEntityConnectionString("WarehousePickingListMngModel"));
            context.Database.CommandTimeout = 180;
            return context;
        }

        public override DTO.WarehousePickingListMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.WarehousePickingListMng.SearchFormData data = new DTO.WarehousePickingListMng.SearchFormData();
            data.Data = new List<DTO.WarehousePickingListMng.WarehousePickingListSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    string receiptNo = null;
                    string proformaInvoiceNo = null;
                    string clientUD = null;
                    string clientNM = null;
                    string articleCode = null;
                    string description = null;

                    if (filters.ContainsKey("ReceiptNo") && !string.IsNullOrEmpty(filters["ReceiptNo"].ToString()))
                    {
                        receiptNo = filters["ReceiptNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        proformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        clientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                    {
                        clientNM = filters["ClientNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        articleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        description = filters["Description"].ToString().Replace("'", "''");
                    }

                    totalRows = context.WarehousePickingListMng_function_SearchWarehousePickingList(receiptNo, proformaInvoiceNo, clientUD, clientNM, articleCode, description, orderBy, orderDirection).Count();
                    var result = context.WarehousePickingListMng_function_SearchWarehousePickingList(receiptNo, proformaInvoiceNo, clientUD, clientNM, articleCode, description, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_WarehousePickingListSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return data;
        }

        public override DTO.WarehousePickingListMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.WarehousePickingListMng.EditFormData data = new DTO.WarehousePickingListMng.EditFormData();
            data.Data = new DTO.WarehousePickingListMng.WarehousePickingList();
            data.Data.CMR1 = "EUROFAR INTERNATIONAL B.V" + "\n" + "BEELAARTS VAN BLOKLANDSTAAT 14" + "\n" + "NL-5042 PM" + " TILBURG" + "\n" + "THE NETHERLANDS";
            data.Data.CMR13 = "ALL LOADED ITEMS ARE CHECKED 100% ON QUALITY BY:";
            data.Data.CMR21 = "TILBURG:";
            data.Data.CMR22 = "PAUL VAN BIJNEN";
            data.Data.Season = DALBase.Helper.GetCurrentSeason();

            data.Data.SenderName = "EUROFAR INTERNATIONAL B.V";
            data.Data.SenderAddress = "BEELAARTS VAN BLOKLANDSTAAT 14";
            data.Data.SenderZipCode = "NL-5042 PM";
            data.Data.SenderCity = "TILBURG";
            data.Data.SenderCountry = "THE NETHERLANDS";

            data.Data.Details = new List<DTO.WarehousePickingListMng.WarehousePickingListProductDetail>();
            data.Data.SparepartDetails = new List<DTO.WarehousePickingListMng.WarehousePickingListSparepartDetail>();
            data.Users = supportFactory.GetUser().ToList();
            data.PhysicalStockByWarehouseAreas = GetPhysicalStockByWarehouseArea();
            data.PackagingMethods = supportFactory.GetPackagingMethod().ToList();
            data.Seasons = supportFactory.GetSeason().ToList();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            if (id > 0)
            {
                try
                {
                    using (WarehousePickingListMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_WarehousePickingList(context.WarehousePickingListMng_WarehousePickingList_View
                            .Include("WarehousePickingListMng_WarehousePickingListProductDetail_View.WarehousePickingListMng_WarehousePickingListAreaDetail_View")
                            .Include("WarehousePickingListMng_WarehousePickingListSparepartDetail_View.WarehousePickingListMng_WarehousePickingListAreaDetail_View")
                            .Include("WarehousePickingListMng_WarehousePickingListProductDetail_View.WarehousePickingListMng_ModelPiece_View")
                            .FirstOrDefault(o => o.WarehousePickingListID == id));
                        data.Data.ConcurrencyFlag_String = Convert.ToBase64String(data.Data.ConcurrencyFlag);
                    }
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = ex.Message;
                    notification.DetailMessage.Add(ex.Message);
                    if (ex.GetBaseException() != null)
                    {
                        notification.DetailMessage.Add(ex.GetBaseException().Message);
                    }
                }
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    WarehousePickingList dbItem = context.WarehousePickingList.FirstOrDefault(o => o.WarehousePickingListID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Picking list receipt: " + dbItem.ReceiptNo + " not found!");
                    }
                    else
                    {
                        if (dbItem.ProcessingStatusID == 1)
                        {
                            context.WarehousePickingList.Remove(dbItem);
                            context.SaveChanges();
                            result = true;
                        }
                        else
                        {
                            throw new Exception("Can not delete the approved/canceled picking list");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return result;
        }

        public override bool UpdateData(int id, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, out Library.DTO.Notification notification)
        {
            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    WarehousePickingList dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new WarehousePickingList();
                        context.WarehousePickingList.Add(dbItem);
                        dbItem.ProcessingStatusID = 1;
                        dbItem.StatusUpdatedBy = dtoItem.UpdatedBy;
                        dbItem.StatusUpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.WarehousePickingList.FirstOrDefault(o => o.WarehousePickingListID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Picking list receipt not found!";
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //check if status = CONFIRMED
                        if (dbItem.ProcessingStatusID == 2 || dbItem.ProcessingStatusID == 3)
                        {
                            throw new Exception("Picking list was finished. You can not change.");
                        }

                        //validate picked quantity with quantity in sale order
                        ValidatePickedQntWithOrderQnt(dtoItem);

                        //validate quanity in area
                        ValidateQuantityOfArea(dtoItem);

                        //validate physical stock qnt
                        ValidateStockQnt(dtoItem);

                        converter.DTO2DB(dtoItem, ref dbItem);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphans
                        context.WarehousePickingListAreaDetail.Local.Where(o => o.WarehousePickingListProductDetail == null && o.WarehousePickingListSparepartDetail == null).ToList().ForEach(o => context.WarehousePickingListAreaDetail.Remove(o));
                        context.WarehousePickingListProductDetail.Local.Where(o => o.WarehousePickingList == null).ToList().ForEach(o => context.WarehousePickingListProductDetail.Remove(o));
                        context.WarehousePickingListSparepartDetail.Local.Where(o => o.WarehousePickingList == null).ToList().ForEach(o => context.WarehousePickingListSparepartDetail.Remove(o));
                        context.SaveChanges();

                        if (id == 0)
                        {
                            context.WarehousePickingListMng_function_GenerateReceiptNo(dbItem.WarehousePickingListID,dbItem.Season);
                        }

                        dtoItem = GetData(dbItem.WarehousePickingListID, out notification).Data;

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return result;
        }

        public override bool Approve(int id, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public bool ChangeStatus(int id, int statusId, ref DTO.WarehousePickingListMng.WarehousePickingList dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {                
                // only update the current data if the status is different than CONFIRMED
                if (dtoItem.ProcessingStatusID == 1)
                {
                    // update current data
                    if (!UpdateData(id, ref dtoItem, out notification))
                    {
                        return false;
                    }
                }
                
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    WarehousePickingList dbItem = context.WarehousePickingList.FirstOrDefault(o => o.WarehousePickingListID == id);
                    if (dbItem != null)
                    {
                        dbItem.ProcessingStatusID = statusId;
                        dbItem.StatusUpdatedBy = dtoItem.UpdatedBy;
                        dbItem.StatusUpdatedDate = DateTime.Now;
                        context.SaveChanges();

                        dtoItem = GetData(id, out notification).Data;

                        return true;
                    }
                    else
                    {
                        throw new Exception("Picking list receipt not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        public List<DTO.WarehousePickingListMng.RemainReservation> QuickSearchRemainProduct(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.WarehousePickingListMng.RemainReservation> data = new List<DTO.WarehousePickingListMng.RemainReservation>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    string proformaInvoiceNo = null;
                    string articleCode = null;
                    string description = null;
                    int clientID = 0;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        proformaInvoiceNo = filters["searchQuery"].ToString().Replace("'", "''");
                        articleCode = filters["searchQuery"].ToString().Replace("'", "''");
                        description = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("clientID") && !string.IsNullOrEmpty(filters["clientID"].ToString()))
                    {
                        clientID = Convert.ToInt32(filters["clientID"]);
                    }

                    totalRows = context.WarehousePickingListMng_function_SearchRemainReservation(proformaInvoiceNo, articleCode, description, clientID, orderBy, orderDirection).Count();
                    var result = context.WarehousePickingListMng_function_SearchRemainReservation(proformaInvoiceNo, articleCode, description, clientID, orderBy, orderDirection);

                    data = converter.DB2DTO_RemainReservationList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return data;
        }

        public List<DTO.WarehousePickingListMng.RemainSparepart> QuickSearchRemainSparepart(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.WarehousePickingListMng.RemainSparepart> data = new List<DTO.WarehousePickingListMng.RemainSparepart>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    string proformaInvoiceNo = null;
                    string articleCode = null;
                    string description = null;
                    int clientID = 0;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        proformaInvoiceNo = filters["searchQuery"].ToString().Replace("'", "''");
                        articleCode = filters["searchQuery"].ToString().Replace("'", "''");
                        description = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("clientID") && !string.IsNullOrEmpty(filters["clientID"].ToString()))
                    {
                        clientID = Convert.ToInt32(filters["clientID"]);
                    }

                    totalRows = context.WarehousePickingListMng_function_SearchRemainSparepartReservation(proformaInvoiceNo, articleCode, description, clientID, orderBy, orderDirection).Count();
                    var result = context.WarehousePickingListMng_function_SearchRemainSparepartReservation(proformaInvoiceNo, articleCode, description, clientID, orderBy, orderDirection);

                    data = converter.DB2DTO_RemainSparepartList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return data;
        }

        public DTO.WarehousePickingListMng.PickingListContainerPrintout GetNewPickingListPrintData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    WarehousePickingListMng_PickingList_ReportView dbItem;
                    dbItem = context.WarehousePickingListMng_PickingList_ReportView
                        .Include("WarehousePickingListMng_PickingListAreaDetail_ReportView")
                        .FirstOrDefault(o => o.WarehousePickingListID == id);
                    return converter.DB2DTO_PickingListPrintout(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new DTO.WarehousePickingListMng.PickingListContainerPrintout();
            }
        }

        public string GetPickingListPrintoutData(int WarehousePickingListID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WarehousePickingListMng_function_GetPickingListPrintout", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WarehousePickingListID", WarehousePickingListID);

                adap.TableMappings.Add("Table", "WarehousePickingListMng_PickingList_ReportView");
                adap.TableMappings.Add("Table1", "WarehousePickingListMng_PickingListProductDetail_ReportView");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "PickingListPrint");
                //return string.Empty;

                // generate xml file                
                return DALBase.Helper.CreateReportFiles(ds, "PickingListPrint");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return string.Empty;
            }
        }

        public DTO.WarehousePickingListMng.CMRContainer GetCMR(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (WarehousePickingListMngEntities context = CreateContext())
                {
                    WarehousePickingListMng_PickingList_ReportView dbItem;
                    dbItem = context.WarehousePickingListMng_PickingList_ReportView
                        .Include("WarehousePickingListMng_PickingListProductDetail_ReportView")
                        .FirstOrDefault(o => o.WarehousePickingListID == id);
                                       
                    //return report
                    return converter.DB2DTO_CMR(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new DTO.WarehousePickingListMng.CMRContainer();
            }
        }

        public List<DTO.WarehousePickingListMng.PhysicalStockByWarehouseArea> GetPhysicalStockByWarehouseArea()
        {
            using (WarehousePickingListMngEntities context = CreateContext())
            {
                return converter.DB2DTO_PhysicalStockByWarehouseArea(context.WarehousePickingListMng_PhysicalStockByWarehouseArea_View.ToList());
            }
        }

        public string ExportXML(int id, out Library.DTO.Notification notification)
        {
            //Library.DTO.Notification notification = new Library.DTO.Notification();
            //DTO.WarehousePickingListMng.EditFormData Data = this.GetData(id, out notification);
            //string[] addressLines = Regex.Split(Data.Data.CMR2, "\n");
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //test export xlm
                DTO.WarehousePickingListMng.EditFormData Data = this.GetData(id, out notification);
                DTO.WarehousePickingListMng.WarehousePickingList data = Data.Data;
                
                //create export object
                DTO.WarehousePickingListMng.ExportXML shipment = new DTO.WarehousePickingListMng.ExportXML();
                shipment.id = "";
                shipment.number = "";

                //header
                DTO.WarehousePickingListMng.Header header = new DTO.WarehousePickingListMng.Header();
                shipment.header = header;
                header.codamount = "";
                header.codcurrency = "EUR";
                header.delivery = data.DeliveryDate;


                DTO.WarehousePickingListMng.References references = new DTO.WarehousePickingListMng.References();
                header.references = references;
                references.xxlcare = "";

                DTO.WarehousePickingListMng.Customer customer = new DTO.WarehousePickingListMng.Customer();
                references.customer = customer;
                customer.line = "";
                customer.value = "";

                //sender
                DTO.WarehousePickingListMng.Sender sender = new DTO.WarehousePickingListMng.Sender();
                shipment.sender = sender;
                sender.name = data.SenderName;
                sender.address = data.SenderAddress;
                sender.postalCode = data.SenderZipCode;
                sender.city = data.SenderCity;
                sender.country = data.SenderCountry;


                //receiver
                DTO.WarehousePickingListMng.Receiver receiver = new DTO.WarehousePickingListMng.Receiver();
                shipment.receiver = receiver;
                receiver.name = data.ReceiverName;
                receiver.address = data.ReceiverAddress;
                receiver.postalCode = data.ReceiverZipCode;
                receiver.city = data.ReceiverCity;
                receiver.countryCode = data.ReceiverCountry;
                receiver.phoneNumber = data.ReceiverPhone;
                receiver.emailAddress = data.ReceiverEmail;

                //item
                DTO.WarehousePickingListMng.Item item;
                List<DTO.WarehousePickingListMng.Item> items = new List<DTO.WarehousePickingListMng.Item>();
                foreach (var article in data.Details)
                {
                    item = new DTO.WarehousePickingListMng.Item();
                    items.Add(item);
                    item.name = article.Description;
                    item.size = "";
                    item.quantity = article.Quantity.ToString();

                    //product
                    DTO.WarehousePickingListMng.Product product = new DTO.WarehousePickingListMng.Product();
                    item.product = product;
                    product.sku = "";
                    product.manufacturerCode = article.Description;
                    product.ean = article.ArticleCode;
                    product.value = "";

                    //dimension
                    DTO.WarehousePickingListMng.Dimension dimension;
                    List<DTO.WarehousePickingListMng.Dimension> dimensions = new List<DTO.WarehousePickingListMng.Dimension>();

                    //set dimension
                    dimension = new DTO.WarehousePickingListMng.Dimension();
                    dimensions.Add(dimension);
                    dimension.lenght = article.OverallDimL;
                    dimension.width = article.OverallDimW;
                    dimension.height = article.OverallDimH;
                    dimension.size = "";

                    //piece dimension
                    foreach (var pdimension in article.ModelPieces)
                    {
                        dimension = new DTO.WarehousePickingListMng.Dimension();
                        dimensions.Add(dimension);
                        dimension.lenght = pdimension.OverallDimL;
                        dimension.width = pdimension.OverallDimW;
                        dimension.height = pdimension.OverallDimH;
                        dimension.size = "";
                    }
                    item.dimensions = dimensions.ToArray();
                }
                shipment.items = items.ToArray();

                //define file path
                string filename = System.Guid.NewGuid().ToString().Replace("-", "");
                string fullpath = FrameworkSetting.Setting.AbsoluteReportTmpFolder + filename + ".xml";

                //serialize object to xml
                XmlSerializer serializer = new XmlSerializer(typeof(DTO.WarehousePickingListMng.ExportXML));
                System.IO.TextWriter writer = new System.IO.StreamWriter(fullpath);
                serializer.Serialize(writer, shipment);

                return filename + ".xml";
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return string.Empty;
            }            
        }

        private bool ValidateQuantityOfArea(DTO.WarehousePickingListMng.WarehousePickingList dtoItem)
        {
            //validate quanity & location for every product
            foreach (var product in dtoItem.Details)
            {
                if (!product.Quantity.HasValue)
                {
                    throw new Exception("You have to fill-in quantity for product: " + product.ArticleCode + "(" + product.Description + ")");
                }

                int? areaQnt = 0;
                if (product.WarehousePickingListAreaDetails != null && product.WarehousePickingListAreaDetails.Count() > 0)
                {
                    foreach (var item in product.WarehousePickingListAreaDetails)
                    {
                        if (!item.WarehouseAreaID.HasValue)
                        {
                            throw new Exception("You have to fill-in area for product: " + product.ArticleCode + "(" + product.Description + ")");
                        }
                        else if (!item.Quantity.HasValue)
                        {
                            throw new Exception("You have to fill-in quantity for every area of product: " + product.ArticleCode + "(" + product.Description + ")");
                        }
                        else
                        {
                            areaQnt += item.Quantity;
                        }
                    }
                }
                else
                {
                    throw new Exception("You have to add location for product: " + product.ArticleCode + "(" + product.Description + ") and fill-in quanity for it");
                }
                if (areaQnt != product.Quantity)
                {
                    throw new Exception("The quanity of product: " + product.ArticleCode + "(" + product.Description + ") have to equal to total quantity in every area");
                }
            }

            //validate quanity & location for every sparepart
            foreach (var sparepart in dtoItem.SparepartDetails)
            {
                if (!sparepart.Quantity.HasValue)
                {
                    throw new Exception("You have to fill-in quantity for sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ")");
                }

                int? areaQnt = 0;
                if (sparepart.WarehousePickingListAreaDetails != null && sparepart.WarehousePickingListAreaDetails.Count() > 0)
                {
                    foreach (var item in sparepart.WarehousePickingListAreaDetails)
                    {
                        if (!item.WarehouseAreaID.HasValue)
                        {
                            throw new Exception("You have to fill-in area for sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ")");
                        }
                        else if (!item.Quantity.HasValue)
                        {
                            throw new Exception("You have to fill-in quantity for every area of sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ")");
                        }
                        else
                        {
                            areaQnt += item.Quantity;
                        }
                    }
                }
                else
                {
                    throw new Exception("You have to add location for sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ") and fill-in quanity for it");
                }
                if (areaQnt != sparepart.Quantity)
                {
                    throw new Exception("The quanity of sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ") have to equal to total quantity in every area");
                }
            }
            return true;
        }

        public string GetExportExcelPickingList(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WarehousePickingListMng_function_GetExportExcel", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.TableMappings.Add("Table", "WarehousePickingListMng_ExportExcel_View");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "PickingListExportExcel");
                //return string.Empty;

                // generate xml file                
                return DALBase.Helper.CreateReportFiles(ds, "PickingListExportExcel");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return string.Empty;
            }
        }

        private bool ValidateStockQnt(DTO.WarehousePickingListMng.WarehousePickingList dtoItem)
        {
            int? stockQnt = 0;
            int? inputQnt = 0;
            using (WarehousePickingListMngEntities context = CreateContext())
            {
                List<string> keyIDs = new List<string>();
                List<string> keyAreaIDs = new List<string>();

                //get physical stock
                keyIDs = dtoItem.Details.Where(o => o.ProductID.HasValue && o.ProductStatusID.HasValue).Select(s => s.ProductID.Value.ToString() + "_" + s.ProductStatusID.Value.ToString()).ToList();
                keyIDs.AddRange(dtoItem.SparepartDetails.Where(o => o.SparepartID.HasValue && o.ProductStatusID.HasValue).Select(s => s.SparepartID.Value.ToString() + "_" + s.ProductStatusID.Value.ToString()).ToList());
                var dbPhysicalStock = context.WarehousePickingListMng_PhysicalStock_View
                    .Where(o => o.GoodsID.HasValue && o.ProductStatusID.HasValue && keyIDs.Contains(o.GoodsID.Value.ToString() + "_" + o.ProductStatusID.Value.ToString())).ToList();

                //get physical stock by area
                dtoItem.Details.Where(o =>o.ProductID.HasValue && o.ProductStatusID.HasValue).ToList().ForEach(item => {
                    item.WarehousePickingListAreaDetails.Where(o =>o.WarehouseAreaID.HasValue).ToList().ForEach(aItem =>
                    {
                        keyAreaIDs.Add(item.ProductID.Value.ToString() + "_" + item.ProductStatusID.Value.ToString() + "_" + aItem.WarehouseAreaID.Value.ToString());
                    });
                });
                dtoItem.SparepartDetails.Where(o => o.SparepartID.HasValue && o.ProductStatusID.HasValue).ToList().ForEach(item => {
                    item.WarehousePickingListAreaDetails.Where(o => o.WarehouseAreaID.HasValue).ToList().ForEach(aItem =>
                    {
                        keyAreaIDs.Add(item.SparepartID.Value.ToString() + "_" + item.ProductStatusID.Value.ToString() + "_" + aItem.WarehouseAreaID.Value.ToString());
                    });
                });
                var dbStockByArea = context.WarehousePickingListMng_PhysicalStockByWarehouseArea_View.Where(o => o.GoodsID.HasValue && o.ProductStatusID.HasValue && o.WarehouseAreaID.HasValue && keyAreaIDs.Contains(o.GoodsID.Value.ToString() + "_" + o.ProductStatusID.Value.ToString() + "_" + o.WarehouseAreaID.Value.ToString()));

                //
                //validate product qnt
                //
                foreach (var item in dtoItem.Details)
                {
                    inputQnt = (item.Quantity.HasValue ? item.Quantity.Value : 0);
                    //if (item.WarehousePickingListProductDetailID > 0)
                    //{
                    //    var dbCurrent = context.WarehousePickingListProductDetail.Where(o => o.WarehousePickingListProductDetailID == item.WarehousePickingListProductDetailID).FirstOrDefault();
                    //    inputQnt = inputQnt - (dbCurrent == null ? 0 : (dbCurrent.Quantity.HasValue ? dbCurrent.Quantity.Value : 0));
                    //}
                    var dbStock = dbPhysicalStock.Where(o => o.GoodsID == item.ProductID && o.ProductStatusID == item.ProductStatusID && o.ProductType == "PRODUCT").FirstOrDefault();
                    stockQnt = (dbStock == null ? 0 : (dbStock.PhysicalQnt.HasValue ? dbStock.PhysicalQnt.Value : 0));
                    if (inputQnt > stockQnt)
                    {
                        throw new Exception("You can not save because quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of product " + item.ArticleCode + " (" + item.Description + " )"  + " is greater than current physical stock (" + stockQnt.ToString() + " pieces)");
                    }

                    //validate stock in every area
                    foreach (var sItem in item.WarehousePickingListAreaDetails)
                    {
                        inputQnt = (sItem.Quantity.HasValue ? sItem.Quantity.Value : 0);
                        var dbAreaStock = dbStockByArea.Where(o => o.GoodsID == item.ProductID && o.ProductStatusID == item.ProductStatusID && o.ProductType == "PRODUCT" && o.WarehouseAreaID == sItem.WarehouseAreaID).FirstOrDefault();
                        stockQnt = (dbAreaStock == null ? 0 : (dbAreaStock.PhysicalQnt.HasValue ? dbAreaStock.PhysicalQnt.Value : 0));
                        if (inputQnt > stockQnt)
                        {
                            throw new Exception("You can not save because quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of product " + item.ArticleCode + " (" + item.Description + " )" + " in location " + sItem.WarehouseAreaUD + " is greater than current physical stock (" + stockQnt.ToString() + " pieces)");
                        }
                    }
                }

                //
                //validate sparepart qnt
                //
                foreach (var item in dtoItem.SparepartDetails)
                {
                    inputQnt = (item.Quantity.HasValue ? item.Quantity.Value : 0);
                    //if (item.WarehousePickingListSparepartDetailID > 0)
                    //{
                    //    var dbCurrent = context.WarehousePickingListSparepartDetail.Where(o => o.WarehousePickingListSparepartDetailID == item.WarehousePickingListSparepartDetailID).FirstOrDefault();
                    //    inputQnt = inputQnt - (dbCurrent == null ? 0 : (dbCurrent.Quantity.HasValue ? dbCurrent.Quantity.Value : 0));
                    //}
                    var dbStock = dbPhysicalStock.Where(o => o.GoodsID == item.SparepartID && o.ProductStatusID == item.ProductStatusID && o.ProductType == "SPAREPART").FirstOrDefault();
                    stockQnt = (dbStock == null ? 0 : (dbStock.PhysicalQnt.HasValue ? dbStock.PhysicalQnt.Value : 0));
                    if (inputQnt > stockQnt)
                    {
                        throw new Exception("You can not save because quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of sparepart " + item.ArticleCode + " (" + item.Description + " )" + " is greater than current physical stock (" + stockQnt.ToString() + " pieces)");
                    }

                    //validate stock in every area
                    foreach (var sItem in item.WarehousePickingListAreaDetails)
                    {
                        inputQnt = (sItem.Quantity.HasValue ? sItem.Quantity.Value : 0);
                        var dbAreaStock = dbStockByArea.Where(o => o.GoodsID == item.SparepartID && o.ProductStatusID == item.ProductStatusID && o.ProductType == "SPAREPART" && o.WarehouseAreaID == sItem.WarehouseAreaID).FirstOrDefault();
                        stockQnt = (dbAreaStock == null ? 0 : (dbAreaStock.PhysicalQnt.HasValue ? dbAreaStock.PhysicalQnt.Value : 0));
                        if (inputQnt > stockQnt)
                        {
                            throw new Exception("You can not save because quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of sparepart " + item.ArticleCode + " (" + item.Description + " )" + " in location " + sItem.WarehouseAreaUD + " is greater than current physical stock (" + stockQnt.ToString() + " pieces)");
                        }
                    }
                }
            }
            return true;
        }

        private bool ValidatePickedQntWithOrderQnt(DTO.WarehousePickingListMng.WarehousePickingList dtoItem)
        {
            int? pickedQntRemaining = 0;
            int? inputQnt = 0;
            using (WarehousePickingListMngEntities context = CreateContext())
            {
                List<int> detailIDs = new List<int>();
                List<string> keyIDs = new List<string>();
                //validate product qnt
                detailIDs = dtoItem.Details.Select(o => o.WarehousePickingListProductDetailID).ToList();
                keyIDs = dtoItem.Details.Where(o => o.SaleOrderDetailID.HasValue && o.ProductStatusID.HasValue && o.ProductID.HasValue).Select(s => s.SaleOrderDetailID.Value.ToString() + "_" + s.ProductStatusID.Value.ToString() + "_" + s.ProductID.Value.ToString()).ToList();

                var dbPickingProduct = context.WarehousePickingListProductDetail.Where(s => detailIDs.Contains(s.WarehousePickingListProductDetailID)).ToList();
                var dbReserProduct = context.WarehousePickingListMng_RemainReservation_View.Where(o => o.ProductStatusID.HasValue && o.ProductID.HasValue && keyIDs.Contains(o.SaleOrderDetailID.ToString() + "_" + o.ProductStatusID.Value.ToString() + "_" + o.ProductID.Value.ToString())).ToList();
                foreach (var item in dtoItem.Details)
                {
                    inputQnt = (item.Quantity.HasValue ? item.Quantity.Value : 0);
                    if (dtoItem.ProcessingStatusID.HasValue && dtoItem.ProcessingStatusID.Value == 2 )
                    {
                        var dbCurrent = dbPickingProduct.Where(o => o.WarehousePickingListProductDetailID == item.WarehousePickingListProductDetailID).FirstOrDefault();
                        inputQnt = inputQnt - (dbCurrent == null ? 0 : (dbCurrent.Quantity.HasValue ? dbCurrent.Quantity.Value : 0));
                    }
                    var dbStock = dbReserProduct.Where(o => o.SaleOrderDetailID == item.SaleOrderDetailID && o.ProductStatusID == item.ProductStatusID && o.ProductID == item.ProductID).FirstOrDefault();
                    pickedQntRemaining = (dbStock == null ? 0 : (dbStock.RemainingReserved.HasValue ? dbStock.RemainingReserved.Value : 0));
                    if (inputQnt > pickedQntRemaining)
                    {
                        throw new Exception("You can not save because quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of product " + item.ArticleCode + " (" + item.Description + " )" + " is greater than total quantity ordered remaining(" + pickedQntRemaining.ToString() + " pieces)");
                    }                    
                }
                //validate sparepart qnt
                detailIDs = dtoItem.SparepartDetails.Select(o => o.WarehousePickingListSparepartDetailID).ToList();
                keyIDs = dtoItem.SparepartDetails.Where(o => o.SaleOrderDetailSparepartID.HasValue && o.ProductStatusID.HasValue && o.SparepartID.HasValue).Select(s => s.SaleOrderDetailSparepartID.Value.ToString() + "_" + s.ProductStatusID.Value.ToString() + "_" + s.SparepartID.Value.ToString()).ToList();

                var dbPickingSparepart = context.WarehousePickingListSparepartDetail.Where(s => detailIDs.Contains(s.WarehousePickingListSparepartDetailID)).ToList();
                var dbReserSparepart = context.WarehousePickingListMng_RemainSparepartReservation_View.Where(o => o.ProductStatusID.HasValue && o.SparepartID.HasValue && keyIDs.Contains(o.SaleOrderDetailSparepartID.ToString() + "_" + o.ProductStatusID.Value.ToString() + "_" + o.SparepartID.Value.ToString())).ToList();
                foreach (var item in dtoItem.SparepartDetails)
                {
                    inputQnt = (item.Quantity.HasValue ? item.Quantity.Value : 0);
                    if (dtoItem.ProcessingStatusID.HasValue && dtoItem.ProcessingStatusID.Value == 2)
                    {
                        var dbCurrent = dbPickingSparepart.Where(o => o.WarehousePickingListSparepartDetailID == item.WarehousePickingListSparepartDetailID).FirstOrDefault();
                        inputQnt = inputQnt - (dbCurrent == null ? 0 : (dbCurrent.Quantity.HasValue ? dbCurrent.Quantity.Value : 0));
                    }
                    var dbStock = dbReserSparepart.Where(o => o.SaleOrderDetailSparepartID == item.SaleOrderDetailSparepartID && o.ProductStatusID == item.ProductStatusID && o.SparepartID == item.SparepartID).FirstOrDefault();
                    pickedQntRemaining = (dbStock == null ? 0 : (dbStock.RemainingReserved.HasValue ? dbStock.RemainingReserved.Value : 0));
                    if (inputQnt > pickedQntRemaining)
                    {
                        throw new Exception("You can not save beause quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of sparepart " + item.ArticleCode + " (" + item.Description + " )" + " is greater than total quantity ordered remaining(" + pickedQntRemaining.ToString() + " pieces)");
                    }                    
                }
            }
            return true;
        }


        public bool DeletePickingListProductDetail(int id, out Library.DTO.Notification notification)
        {
            //bool result = false;
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (WarehousePickingListMngEntities context = CreateContext())
            //    {
            //        WarehousePickingListProductDetail dbItem = context.WarehousePickingListProductDetail.FirstOrDefault(o => o.WarehousePickingListProductDetailID == id);
            //        if (dbItem == null)
            //        {
            //            throw new Exception("data not found!");
            //        }
            //        else
            //        {
            //            if (dbItem.WarehousePickingList.ProcessingStatusID == 1)
            //            {
            //                foreach (var item in dbItem.WarehousePickingListAreaDetail.ToArray())
            //                {
            //                    dbItem.WarehousePickingListAreaDetail.Remove(item);
            //                }
            //                context.WarehousePickingListProductDetail.Remove(dbItem);
            //                context.WarehousePickingListAreaDetail.Local.Where(o => o.WarehousePickingListProductDetail == null && o.WarehousePickingListSparepartDetail == null).ToList().ForEach(o => context.WarehousePickingListAreaDetail.Remove(o));
            //                context.WarehousePickingListProductDetail.Local.Where(o => o.WarehousePickingList == null).ToList().ForEach(o => context.WarehousePickingListProductDetail.Remove(o));
            //                context.SaveChanges();
            //                result = true;
            //            }
            //            else
            //            {
            //                throw new Exception("Can not delete because picking list was approved/canceled");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //}

            //return result;
            throw new Exception("not need this function");
        }

        public bool DeletePickingListSparepartDetail(int id, out Library.DTO.Notification notification)
        {
            //bool result = false;
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (WarehousePickingListMngEntities context = CreateContext())
            //    {
            //        WarehousePickingListSparepartDetail dbItem = context.WarehousePickingListSparepartDetail.FirstOrDefault(o => o.WarehousePickingListSparepartDetailID == id);
            //        if (dbItem == null)
            //        {
            //            throw new Exception("data not found!");
            //        }
            //        else
            //        {
            //            if (dbItem.WarehousePickingList.ProcessingStatusID == 1)
            //            {
            //                foreach (var item in dbItem.WarehousePickingListAreaDetail.ToArray())
            //                {
            //                    dbItem.WarehousePickingListAreaDetail.Remove(item);
            //                }
            //                context.WarehousePickingListSparepartDetail.Remove(dbItem);
            //                context.WarehousePickingListAreaDetail.Local.Where(o => o.WarehousePickingListProductDetail == null && o.WarehousePickingListSparepartDetail == null).ToList().ForEach(o => context.WarehousePickingListAreaDetail.Remove(o));
            //                context.WarehousePickingListSparepartDetail.Local.Where(o => o.WarehousePickingList == null).ToList().ForEach(o => context.WarehousePickingListSparepartDetail.Remove(o));
            //                context.SaveChanges();
            //                result = true;
            //            }
            //            else
            //            {
            //                throw new Exception("Can not delete because picking list was approved/canceled");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //}

            //return result;
            throw new Exception("not need this function");
        }

        public bool DeletePickingListAreaDetail(int id, out Library.DTO.Notification notification)
        {
            //bool result = false;
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (WarehousePickingListMngEntities context = CreateContext())
            //    {
            //        WarehousePickingListAreaDetail dbItem = context.WarehousePickingListAreaDetail.FirstOrDefault(o => o.WarehousePickingListAreaDetailID == id);
            //        if (dbItem == null)
            //        {
            //            throw new Exception("data not found!");
            //        }
            //        else
            //        {
            //            if (dbItem.Quantity > 0)
            //            {
            //                throw new Exception("Can not delete because quantity is this location > 0. You need set quantity = 0 before detete this area.");
            //            }
            //            int? processingStatusID = -1;
            //            if (dbItem.WarehousePickingListProductDetail != null)
            //            {
            //                processingStatusID = dbItem.WarehousePickingListProductDetail.WarehousePickingList.ProcessingStatusID;
            //            }
            //            else if (dbItem.WarehousePickingListSparepartDetail != null)
            //            {
            //                processingStatusID = dbItem.WarehousePickingListSparepartDetail.WarehousePickingList.ProcessingStatusID;
            //            }
            //            if (processingStatusID == 1)
            //            {
            //                context.WarehousePickingListAreaDetail.Remove(dbItem);
            //                context.WarehousePickingListAreaDetail.Local.Where(o => o.WarehousePickingListProductDetail == null && o.WarehousePickingListSparepartDetail == null).ToList().ForEach(o => context.WarehousePickingListAreaDetail.Remove(o));
            //                context.SaveChanges();
            //                result = true;
            //            }
            //            else
            //            {
            //                throw new Exception("Can not delete because picking list was approved/canceled");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //}

            //return result;
            throw new Exception("not need this function");
        }


    }
}
