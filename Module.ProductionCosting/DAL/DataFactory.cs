using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.ProductionCosting.DTO;

namespace Module.ProductionCosting.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private ProductionCostingRptEntities CreateContext()
        {
            return new ProductionCostingRptEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionCostingRptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditForm GetData(int id, out Notification notification)
        {
            DTO.EditForm editFormData = new DTO.EditForm();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductionCostingRptEntities context = CreateContext())
                {
                    ProductionCostingMng_WorkOrder_View dbItem;
                    dbItem = context.ProductionCostingMng_WorkOrder_View.FirstOrDefault(o => o.WorkOrderID == id);
                    editFormData.Data = converter.DB2DTO_WorkOrder(dbItem);
                    return editFormData;
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
                return editFormData;
            }
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public string GetExcelReportData(int userId, int? workOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //SearchForm data = new SearchForm();
            //data.TotalReceivingNotes = new List<TotalReceivingNote>();
            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    int? bomID = context.BOM.Where(o => o.WorkOrderID == workOrderID && !o.ParentBOMID.HasValue).FirstOrDefault().BOMID;
                    ProductionCostingRpt_ProductionCosting_View dbItem;
                    dbItem = context.ProductionCostingRpt_ProductionCosting_View.Where(o => o.BOMID == bomID && o.CompanyID == companyID).FirstOrDefault();
                    //data.TotalReceivingNotes = converter.DB2DTO_TotalReceivingNote(context.ProductionCostingRpt_TotalQntReceivingNote_View.ToList());
                    //fill-in data to data set 
                    ReportDataObject dsResult = new ReportDataObject();
                    this.ParseProductionCostingToList(dbItem, ref dsResult);
                    return Library.Helper.CreateReportFileWithEPPlus2(dsResult, "ProductionCosting");
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
                return string.Empty;
            }
        }

        private void ParseProductionCostingToList(ProductionCostingRpt_ProductionCosting_View data, ref ReportDataObject dsResult)
        {
            if (data != null && data.ProductionCostingRpt_ProductionCosting_View1 != null)
            {   
                if(data.ProductionItemTypeID == 2 && (data.PlanQnt == null || data.PlanQnt == 0) && (data.UsingQnt == null || data.UsingQnt == 0))
                {}
                else
                {
                    //push to list result
                    ReportDataObject.ProductionCostingReportDataRow dr;
                    dr = dsResult.ProductionCostingReportData.NewProductionCostingReportDataRow();
                    if (data.PieceIndex.HasValue) dr.PieceIndex = data.PieceIndex.Value;
                    if (data.ProductionItemTypeID.HasValue) dr.ProductionItemTypeID = data.ProductionItemTypeID.Value;
                    if (!string.IsNullOrEmpty(data.ProductionItemTypeNM)) dr.ProductionItemTypeNM = data.ProductionItemTypeNM;
                    if (!string.IsNullOrEmpty(data.WorkCenterNM)) dr.WorkCenterNM = data.WorkCenterNM;
                    if (!string.IsNullOrEmpty(data.ProductionItemUD)) dr.ProductionItemUD = data.ProductionItemUD;
                    if (!string.IsNullOrEmpty(data.ProductionItemNM)) dr.ProductionItemNM = data.ProductionItemNM;
                    if (!string.IsNullOrEmpty(data.Unit)) dr.Unit = data.Unit;
                    if (data.Price.HasValue) dr.Price = data.Price.Value;
                    if (data.WorkOrderQnt.HasValue) dr.WorkOrderQnt = data.WorkOrderQnt.Value;
                    if (data.PlanQnt.HasValue) dr.PlanQnt = data.PlanQnt.Value;
                    if (data.UsingQnt.HasValue) dr.UsingQnt = data.UsingQnt.Value;
                    if (data.VarianceQnt.HasValue) dr.VarianceQnt = data.VarianceQnt.Value;
                    if (data.PlanCosting.HasValue) dr.PlanCosting = data.PlanCosting.Value;
                    if (data.UsingCosting.HasValue) dr.UsingCosting = data.UsingCosting.Value;
                    if (data.VarianceValue.HasValue) dr.VarianceValue = data.VarianceValue.Value;
                    if (data.VarianceCosting.HasValue) dr.VarianceCosting = data.VarianceCosting.Value;
                    dr.BOMID = data.BOMID;
                    if (data.WorkCenterID.HasValue) dr.WorkCenterID = data.WorkCenterID.Value;
                    if (data.CountChildBOM.HasValue) dr.CountChildBOM = data.CountChildBOM.Value;
                    if (data.ProductionItemID.HasValue) dr.ProductionItemID = (int)data.ProductionItemID;
                    if (data.WorkOrderID.HasValue) dr.WorkOrderID = (int)data.WorkOrderID;
                    if (data.ProductionItemTypeID == 1)
                    {
                        dr.UsingQnt = 0;
                        if (data.TotalReceivedQnt == null)
                        {
                            dr.UsingQnt = 0;
                        }
                        else
                        {
                            dr.UsingQnt = data.TotalReceivedQnt.Value;
                        }
                        if (data.Price.HasValue)
                        {
                            if (dr.PlanCosting > 0 && dr.PlanCosting != null)
                            {
                                dr.VarianceQnt = (decimal)(dr.UsingQnt - dr.PlanQnt);
                                dr.UsingCosting = dr.Price * dr.UsingQnt;
                                dr.VarianceValue = dr.UsingCosting - dr.PlanCosting;
                                dr.VarianceCosting = 0;

                            }
                            else
                            {
                                dr.VarianceQnt = (decimal)(dr.UsingQnt - dr.PlanQnt);
                                dr.UsingCosting = dr.Price * dr.UsingQnt;
                                dr.VarianceValue = dr.UsingCosting - dr.PlanCosting;
                                if(dr.PlanCosting == 0 || dr.PlanCosting == null)
                                {
                                    dr.VarianceCosting = 0;
                                }
                                else
                                {
                                    dr.VarianceCosting = dr.VarianceValue / dr.PlanCosting * 100;
                                }
                            }
                        }

                    }
                    dsResult.ProductionCostingReportData.AddProductionCostingReportDataRow(dr);

                    //it just only assign piece index for child of root node
                    if (data.ParentBOMID == null)
                    {
                        int i = 1;
                        foreach (var item in data.ProductionCostingRpt_ProductionCosting_View1.ToList())
                        {
                            item.PieceIndex = i;
                            i++;
                        }
                    }
                    // edit ActualQnt and VarianceQnt follow totalRecevingNote


                    foreach (var item in data.ProductionCostingRpt_ProductionCosting_View1.OrderBy(o => o.ProductionCostingRpt_ProductionCosting_View1.Count))
                    {
                        item.CountChildBOM = item.ProductionCostingRpt_ProductionCosting_View1.Count;
                        if (!item.PieceIndex.HasValue)
                        {
                            item.PieceIndex = data.PieceIndex;
                        }
                        ParseProductionCostingToList(item, ref dsResult);
                    }
                }
            }
        }

        public DTO.SearchForm GetDataWithFilters(int userId, int? workOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm data = new DTO.SearchForm();
            data.TotalReceivingNotes = new List<TotalReceivingNote>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    int? bomID = context.BOM.Where(o => o.WorkOrderID == workOrderID && !o.ParentBOMID.HasValue).FirstOrDefault().BOMID;
                    data.ProductionCostingData = converter.DB2DTO_ProductionCosting(context.ProductionCostingRpt_ProductionCosting_View.Where(o => o.BOMID == bomID && o.CompanyID == companyID).FirstOrDefault());
                    data.TotalReceivingNotes = converter.DB2DTO_TotalReceivingNote(context.ProductionCostingRpt_TotalQntReceivingNote_View.ToList());
                    if (data.ProductionCostingData == null)
                    {
                        data.ProductionCostingData = new ProductionCostingPrintData();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

    }
}
