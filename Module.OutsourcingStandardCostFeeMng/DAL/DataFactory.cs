using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.OutsourcingStandardCostFeeMng.DTO;
using Library;

namespace Module.OutsourcingStandardCostFeeMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory2<DTO.SearchForm, DTO.EditForm>
    {
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private OutsourcingStandardCostFeeMngEntities CreateContext()
        {
            return new OutsourcingStandardCostFeeMngEntities(Library.Helper.CreateEntityConnectionString("DAL.OutsourcingStandardCostFeeMngModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.OutsourcingStandardCostFeeDTO dtoData = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OutsourcingStandardCostFeeDTO>();
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.OutsourcingStandardCostFee.Where(o => o.OutsourcingStandardCostFeeID == dtoData.OutsourcingStandardCostFeeID).FirstOrDefault();
                    //Checking value
                    if (dbItem != null)
                    {
                        if (dbItem.IsConfirmed == true) throw new Exception("Model has been confirmed ");
                        //Update data
                        dbItem.Remark = dtoData.Remark;
                        dbItem.StandardPrice = dtoData.StandardPrice;
                        dbItem.ValidFrom = dtoData.ValidFrom.ConvertStringToDateTime();
                        //Tracking Update
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //Tracking Confirm
                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = userId;
                        dbItem.ConfirmedDate = DateTime.Now;
                        //Save
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditForm GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            throw new NotFiniteNumberException();
        }

        public List<DTO.OutsourcingStandardCostFeeDTO> GetData(int userId, int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<DTO.OutsourcingStandardCostFeeDTO> Data = new List<OutsourcingStandardCostFeeDTO>();
            int? modelID = id;
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Can't Find Model !!!");
                }
                else
                {
                    using (var context = CreateContext())
                    {
                        var result = context.OutsourcingStandardCostFeeMng_OutsourcingStandardCostFee_View.Where(o => o.ModelID == modelID).ToList();
                        Data = converter.DB2DTO_GetDetail(result).ToList();

                        return Data;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error };
                notification.Message = Library.Helper.HandleException(ex).ToString();

                return Data;
            }
        }

        public override SearchForm GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            SearchForm searchData = new SearchForm();
            searchData.outsourcingModels = new List<OutsourcingModel>();
            searchData.support = new SupportDTO();
            totalRows = 0;
            List<int?> modelIDs = new List<int?>();

            try
            {
                string modelUD = null;

                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    modelUD = filters["modelUD"].ToString().Replace("'", "''");
                }
                using (var context = CreateContext())
                {
                    //context.OutsourcingStandardCostFeeMng_Function_AutoCreateOutsourcingStandardCostFee();

                    totalRows = context.OutsourcingStandardCostFeeMng_Function_ModelSearchView(modelUD).Count();
                    var result = context.OutsourcingStandardCostFeeMng_Function_ModelSearchView(modelUD).ToList();
                    searchData.outsourcingModels = converter.BD2DTO_SearchModel(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //foreach (var item in searchData.outsourcingModels.ToList())
                    //{
                    //    modelIDs.Add(item.ModelID);
                    //}

                    //var resultDetail = context.OutsourcingStandardCostFeeMng_OutsourcingStandardCostFeeDetail_View.Where(o => modelIDs.Contains(o.ModelID)).ToList();
                    //searchData.outSourcingModelDetailSearches = converter.DB2DTO_SearchModelDetail(resultDetail).ToList();


                    searchData.support.outSourcingCostSPs = AutoMapper.Mapper.Map<List<OutsourcingStandardCostFeeMng_OutsourcingCost_View>, List<DTO.OutSourcingCostSP>>(context.OutsourcingStandardCostFeeMng_OutsourcingCost_View.ToList());
                    searchData.support.outSourcingCostTypeSPs = AutoMapper.Mapper.Map<List<OutsourcingStandardCostFeeMng_OutsourcingCostType_View>, List<DTO.OutSourcingCostTypeSP>>(context.OutsourcingStandardCostFeeMng_OutsourcingCostType_View.ToList());

                    return searchData;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error };
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return searchData;
            }
        }

        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool ResetMD(int userId, int id, out Notification notification) {

            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.OutsourcingStandardCostFee.Where(o => o.OutsourcingStandardCostFeeID == id).FirstOrDefault();
                    //Checking value
                    if (dbItem != null)
                    {
                        if (dbItem.IsConfirmed == false) throw new Exception("You need confirm before ");
                        //Tracking Update
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //Tracking Confirm
                        dbItem.IsConfirmed = false;
                        dbItem.ConfirmedBy = null;
                        dbItem.ConfirmedDate = null;
                        //Save
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.OutsourcingStandardCostFeeDTO dtoData = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OutsourcingStandardCostFeeDTO>();
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.OutsourcingStandardCostFee.Where(o => o.OutsourcingStandardCostFeeID == dtoData.OutsourcingStandardCostFeeID).FirstOrDefault();
                    //Checking value
                    if (dbItem != null)
                    {
                        dbItem.Remark = dtoData.Remark;
                        dbItem.StandardPrice = dtoData.StandardPrice;
                        dbItem.ValidFrom = dtoData.ValidFrom.ConvertStringToDateTime();
                        //Tracking 
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //Save
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public List<DTO.OutsourcingModelPiece> GetDetailModel(int userId, int? modelID, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<DTO.OutsourcingModelPiece> data = new List<OutsourcingModelPiece>();

            try
            {
                using (var conetxt = CreateContext())
                {
                    var result = conetxt.OutsourcingStandardCostFeeMng_ModelPiece_SearchView.Where(o => o.MasterModelPieceID == modelID).ToList();
                    data = converter.BD2DTO_SearchModelPice(result).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public bool InsertData(int userId, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    context.OutsourcingStandardCostFeeMng_Function_AutoCreateOutsourcingStandardCostFee();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }
    }
}
