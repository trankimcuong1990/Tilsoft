using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.MRPRpt.DTO;

namespace Module.MRPRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private MRPEntities CreateContext()
        {
            return new MRPEntities(Library.Helper.CreateEntityConnectionString("DAL.MRPModel"));
        }

        public DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            //throw new NotImplementedException();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.MRPSearchFormData>();
            data.purchaseOrderDetailMRPs = new List<DTO.PurchaseOrderDetailMRP>();
            data.purchaseRequestDetailMRPs = new List<DTO.PurchaseRequestDetailMRP>();
            //data.supportProductionItemGroupSearchViews = new List<DTO.SupportProductionItemGroupSearchView>();
            totalRows = 0;

            int? productionItemID = null;
            int? productionItemGroupID = null;
            int? companyID = null;
            string fromDate = null;
            string toDate = null;

            if (filters.ContainsKey("ProductionItemID") && filters["ProductionItemID"] != null)
            {
                productionItemID = Convert.ToInt32(filters["ProductionItemID"]);
            }

            if (filters.ContainsKey("ProductionItemGroupID") && filters["ProductionItemGroupID"] != null)
            {
                productionItemGroupID = Convert.ToInt32(filters["ProductionItemGroupID"]);
            }

            if (filters.ContainsKey("fromDate") && !string.IsNullOrEmpty(filters["fromDate"].ToString()))
            {
                fromDate = filters["fromDate"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("toDate") && !string.IsNullOrEmpty(filters["toDate"].ToString()))
            {
                toDate = filters["toDate"].ToString().Replace("'", "''");
            }

            Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
            companyID = fw_factory.GetCompanyID(userId);

            //try to get data
            try
            {
                using (var context = CreateContext())
                {

                    totalRows = context.MRP_Function_ProductionItemSearch_View(companyID, orderBy, orderDirection, productionItemGroupID, productionItemID).Count();
                    var result = context.MRP_Function_ProductionItemSearch_View(companyID, orderBy, orderDirection, productionItemGroupID, productionItemID);
                    data.Data = converter.DB2DTO_SearchData(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    var resultOrder = context.MRP_Function_MRP_PurchaseOrderDetail_View(productionItemID, productionItemGroupID, fromDate, toDate);
                    data.purchaseOrderDetailMRPs = converter.Db2DTO_PurchaseOrder(resultOrder.ToList());

                    var resultRequest = context.MRP_Function_MRP_PurchaseRequestDetail_View(productionItemID, productionItemGroupID, fromDate, toDate);
                    data.purchaseRequestDetailMRPs = converter.DB2DTO_PurchaseRequest(resultRequest.ToList());

                    //data.supportProductionItemGroupSearchViews = converter.DB2DTO_SpProductionItemGroup(context.MRP_SupportProductionItemGroup_SearchView.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.PoductionItemMRP();
            data.Data.purchaseOrderDetailMRPs = new List<DTO.PurchaseOrderDetailMRP>();
            data.Data.purchaseRequestDetailMRPs = new List<DTO.PurchaseRequestDetailMRP>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Getproduction(context.MRP_ProductionItem_View
                            .Include("MRP_PurchaseOrderDetail_View")
                            .Include("MRP_PurchaseRequestDetail_View")
                            .FirstOrDefault(o => o.ProductionItemID == id));
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //DTO.SampleOrder dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleOrder>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleOrder dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new SampleOrder();
            //            context.SampleOrder.Add(dbItem);
            //            dbItem.CreatedBy = userId;
            //            dbItem.CreatedDate = DateTime.Now;
            //        }
            //        else
            //        {
            //            dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Order not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.UpdatedBy = userId;
            //            dbItem.UpdatedDate = DateTime.Now;
            //            converter.DTO2DB_SampleOrder(dtoOrder, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
            //            context.SampleProductMinuteImage.Local.Where(o => o.SampleProductMinute == null).ToList().ForEach(o => context.SampleProductMinuteImage.Remove(o));
            //            context.SampleProductMinute.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductMinute.Remove(o));
            //            context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
            //            context.SampleRemark.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleRemark.Remove(o));
            //            context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
            //            context.SampleTechnicalDrawing.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleTechnicalDrawing.Remove(o));
            //            context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
            //            context.SampleProgress.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProgress.Remove(o));
            //            context.SampleReferenceImage.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleReferenceImage.Remove(o));
            //            context.SampleProduct.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleProduct.Remove(o));
            //            context.SampleMonitor.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleMonitor.Remove(o));
            //            context.SaveChanges();

            //            // generate order number
            //            if (id <= 0)
            //            {
            //                using (DbContextTransaction scope = context.Database.BeginTransaction())
            //                {
            //                    context.Database.ExecuteSqlCommand("SELECT * FROM SampleOrder WITH (TABLOCKX, HOLDLOCK)");

            //                    try
            //                    {
            //                        context.SaveChanges();
            //                        dbItem.SampleOrderUD = Library.Common.Helper.formatIndex(dbItem.SampleOrderID.ToString(), 8, "0");
            //                        context.SaveChanges();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        throw ex;
            //                    }
            //                    finally
            //                    {
            //                        scope.Commit();
            //                    }
            //                }
            //            }
            //            dtoItem = GetData(dbItem.SampleOrderID, out notification).Data;
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.supportProductionItemGroupSearchViews = new List<SupportProductionItemGroupSearchView>();
            try
            {
                using (var context = CreateContext())
                {
                    data.supportProductionItemGroupSearchViews = converter.DB2DTO_SpProductionItemGroup(context.MRP_SupportProductionItemGroup_SearchView.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
