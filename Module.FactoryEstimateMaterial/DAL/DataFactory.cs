using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
namespace Module.FactoryEstimateMaterial.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.FactoryOrderDetail, DTO.FactoryOrderDetail>
    {
        private DataConverter converter = new DataConverter();

        private FactoryEstimateMaterialEntities CreateContext()
        {
            return new FactoryEstimateMaterialEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryEstimateMaterialModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            
        }

        public override DTO.FactoryOrderDetail GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.FactoryOrderDetail GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public List<DTO.FactoryMaterial> EstimateMaterial(int userId, string factoryOrderIds, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryMaterial> data = new List<DTO.FactoryMaterial>();
            try
            {
                if (string.IsNullOrEmpty(factoryOrderIds)) return data;
                int[] fIDs = factoryOrderIds.Split(';').Where(o => !string.IsNullOrEmpty(o)).Select(o => Convert.ToInt32(o)).ToArray();

                //search
                
                DTO.FactoryMaterial material;
                using (FactoryEstimateMaterialEntities context = CreateContext())
                {
                    List<DTO.FactoryOrderDetail> factoryOrderDetails = converter.DB2DTO_FactoryOrderDetail(context.FactoryEstimateMaterialMng_FactoryOrderDetail_View.Where(o => fIDs.Contains(o.FactoryOrderID.Value)).ToList());
                    var factoryMaterial =  from item in factoryOrderDetails
                                    group item by new { item.FactoryMaterialID } into g
                                    select new { g.Key.FactoryMaterialID, TotalOrderQnt = g.Sum(o => o.OrderQnt * o.NormValue) };

                    foreach (var item in factoryMaterial)
                    {
                        //get material entry
                        var fOrderDetail = factoryOrderDetails.Where(o => o.FactoryMaterialID == item.FactoryMaterialID).FirstOrDefault(); 

                        //init material object
                        material = new DTO.FactoryMaterial();
                        material.IsSelected = item.FactoryMaterialID.HasValue;
                        material.FactoryMaterialID = item.FactoryMaterialID;
                        material.Quantity = item.TotalOrderQnt;

                        material.FactoryMaterialUD = fOrderDetail.FactoryMaterialUD;
                        material.FactoryMaterialNM = fOrderDetail.FactoryMaterialNM;
                        material.UnitID = fOrderDetail.UnitID;
                        material.UnitNM = fOrderDetail.UnitNM;
                        material.TotalStockQnt = fOrderDetail.TotalStockQnt;

                        //init order info
                        material.FactoryOrderDetails = converter.DTO2DTO_FactoryOrderDetail(factoryOrderDetails.Where(o => o.FactoryMaterialID == item.FactoryMaterialID).ToList());

                        //add to object
                        data.Add(material);
                    }
                }
                return data;
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
                return new List<DTO.FactoryMaterial>(); ;
            }
        }

        public int ImportMaterial(int userID, object dtoItem, out Library.DTO.Notification notification) 
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "System have just created import material receipt success" };
            int factoryMaterialReceiptID = -1;
            List<DTO.FactoryMaterial> data = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.FactoryMaterial>>();
            try
            {
                using (FactoryEstimateMaterialEntities context = CreateContext())
                {
                    FactoryMaterialReceipt dbReceipt = new FactoryMaterialReceipt();
                    context.FactoryMaterialReceipt.Add(dbReceipt);
                    dbReceipt.ReceiptTypeID = 1; //1 :Import, 2: Export
                    dbReceipt.Season = Library.Helper.GetCurrentSeason();
                    dbReceipt.ReceiptDate = DateTime.Now;
                    dbReceipt.CreatedBy = userID;
                    dbReceipt.CreatedDate = DateTime.Now;
                    dbReceipt.UpdatedBy = userID;
                    dbReceipt.UpdatedDate = DateTime.Now;

                    //create receipt detail
                    FactoryMaterialReceiptDetail dbDetail;
                    foreach (var material in data.Where(o =>o.FactoryMaterialID.HasValue))
                    {
                        if (material.FactoryAreaID.HasValue)
                        {
                            foreach (var item in material.FactoryOrderDetails.Where(o => o.IsSelected.HasValue && o.IsSelected.Value))
                            {
                                dbDetail = new FactoryMaterialReceiptDetail();
                                dbReceipt.FactoryMaterialReceiptDetail.Add(dbDetail);
                                dbDetail.FactoryOrderDetailID = item.FactoryOrderDetailID;
                                dbDetail.FactoryMaterialID = material.FactoryMaterialID;
                                dbDetail.FactoryAreaID = material.FactoryAreaID;
                                dbDetail.Quantity = item.AmountQnt;
                                dbDetail.FactoryFinishedProductID = item.FactoryFinishedProductID;
                            }
                        }
                        else
                        {
                            throw new Exception("Area is empty. You need select Area before import to stock");
                        }
                    }
                    //save receipt
                    context.SaveChanges();
                    //get return id
                    factoryMaterialReceiptID = dbReceipt.FactoryMaterialReceiptID;
                    //update receipt no
                    context.FactoryMaterialReceiptMng_function_GenerateReceipNo(factoryMaterialReceiptID, dbReceipt.Season, dbReceipt.ReceiptTypeID);
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
            return factoryMaterialReceiptID;  
        }

        public DTO.SupportData GetSupportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SupportData supportData = new DTO.SupportData();
            try
            {
                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                supportData.FactoryAreas = support_factory.GetFactoryArea();
                return supportData;
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
                return supportData;
            }
        }
    }
}
