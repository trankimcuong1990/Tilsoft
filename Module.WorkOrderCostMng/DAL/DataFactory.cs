using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Module.WorkOrderCostMng.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private WorkOrderCostMngEntities CreateContext()
        {
            return new WorkOrderCostMngEntities(Library.Helper.CreateEntityConnectionString("DAL.WorkOrderCostMngModel"));
        }
        public List<DTO.ProductionItem> getProductionItemTypeCost(int userId, Hashtable filters)
        {
            List<DTO.ProductionItem> result = new List<DTO.ProductionItem>();
            using (WorkOrderCostMngEntities context = this.CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                string searchQuery = filters["searchQuery"].ToString();
                var dbItems = context.SupportMng_ProductionItem_View.Where(o => o.CompanyID == companyID && o.ProductionItemTypeID==7 && (o.ProductionItemNM.Contains(searchQuery) || o.ProductionItemUD.Contains(searchQuery))).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
            }
        }
        public DTO.EditFormData GetData(int userId, int id, int? workOrderID, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WorkOrderCostMngEntities context = CreateContext())
                {                                      
                    var workOrder = context.BOMMng_WorkOrder_View.Where(o => o.BOMID == id).FirstOrDefault();
                    editFormData.Data = new DTO.BOMDTO();
                    editFormData.Data.BOMDTOs = new List<DTO.BOMDTO>();

                    //init workorder info
                    editFormData.Data.WorkOrderID = workOrder.WorkOrderID;
                    editFormData.Data.WorkOrderUD = workOrder.WorkOrderUD;
                    editFormData.Data.WorkOrderDescription = workOrder.WorkOrderDescription;
                    editFormData.Data.WorkOrderQnt = workOrder.WorkOrderQnt;
                    editFormData.Data.OPSequenceID = workOrder.OPSequenceID;
                    editFormData.Data.OPSequenceNM = workOrder.OPSequenceNM;
                    editFormData.Data.ClientUD = workOrder.ClientUD;
                    editFormData.Data.ModelID = workOrder.ModelID;
                    editFormData.Data.ModelUD = workOrder.ModelUD;
                    editFormData.Data.ModelNM = workOrder.ModelNM;
                    editFormData.Data.WorkOrderStatusID = workOrder.WorkOrderStatusID;
                    editFormData.Data.WorkOrderTypeNM = workOrder.WorkOrderTypeNM;
                    editFormData.Data.WorkOrderStatusNM = workOrder.WorkOrderStatusNM;
                    if (workOrder.StartDate.HasValue) editFormData.Data.StartDate = workOrder.StartDate.Value.ToString("dd/MM/yyyy");
                    if (workOrder.FinishDate.HasValue) editFormData.Data.FinishDate = workOrder.FinishDate.Value.ToString("dd/MM/yyyy");
                    editFormData.Data.ProductArticleCode = workOrder.ProductArticleCode;
                    editFormData.Data.ProductDescription = workOrder.ProductDescription;
                    editFormData.Data.WorkOrderWastagePercent = workOrder.WastagePercent;
                    editFormData.Data.ProductID = workOrder.ProductID;

                    List<WorkOrderCostMng_WorkOrderCost_View> dbItem = context.WorkOrderCostMng_WorkOrderCost_View.Where(x => x.WorkOrderID == workOrderID).ToList();
                    editFormData.WorkOrderCosts = converter.DB2DTO_WorkOrderCost(dbItem);

                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return editFormData;
            }
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<WorkOrderCost> dataItem = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<WorkOrderCost>>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (WorkOrderCostMngEntities context = CreateContext())
                {
                    List<WorkOrderCost> dbWorkOrderCosts = context.WorkOrderCost.Where(x => x.WorkOrderID == id).ToList();
                    List<int> dbWorkOrderCostsDelete = dbWorkOrderCosts.Select(y=>y.WorkOrderCostID).Where(x=>!dataItem.Select(z=>z.WorkOrderCostID).Contains(x)).ToList();
                    foreach (WorkOrderCost item in dataItem)
                    {
                        if(!dbWorkOrderCosts.Select(x=>x.WorkOrderCostID).ToList().Contains(item.WorkOrderCostID))
                        {
                            context.WorkOrderCost.Add(item);
                        }
                        else
                        {
                            WorkOrderCost dbWorkOrderCost = dbWorkOrderCosts.Where(x => x.WorkOrderCostID == item.WorkOrderCostID).FirstOrDefault();
                            dbWorkOrderCost.Qty = item.Qty;
                            dbWorkOrderCost.CostPrice = item.CostPrice;
                        }
                    }
                    foreach(int iddelete in dbWorkOrderCostsDelete)
                    {
                        WorkOrderCost itemdelete = context.WorkOrderCost.Where(x => x.WorkOrderCostID == iddelete).FirstOrDefault();
                        if(itemdelete!= null)
                            context.WorkOrderCost.Remove(itemdelete);
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return false;
            }
        }
    }
}
