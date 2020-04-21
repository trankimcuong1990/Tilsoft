using DTO.ModelMng;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL
{
    public class ModelMng : Lib.BLLBase2<DTO.ModelMng.SearchFormData, DTO.ModelMng.EditFormData, DTO.ModelMng.Model>
    {
        private DAL.ModelMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public ModelMng(string tempFolder)
        {
            factory = new DAL.ModelMng.DataFactory(tempFolder);
        }

        public override DTO.ModelMng.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of Model");

            // query data
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ModelMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data
            DTO.ModelMng.EditFormData result = factory.GetData(iRequesterID, id, out notification);
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            if (fwFactory.CheckModelPricePermission(iRequesterID) <= 0)
            {
                // clear pricing data if not allow
                result.Data.ModelFixPriceConfigurations.Clear();
                result.Data.ModelPriceConfigurations.Clear();
                result.ModelPriceConfigurationDefault.Clear();
                result.IsPriceEnabled = false;
            }
            else
            {
                result.IsPriceEnabled = true;
            }

            if(fwFactory.CanPerformAction(iRequesterID, "ModelMng", Library.DTO.ModuleAction.CanApprove))
            {
                result.CanApprove = true;
            }
            else
            {
                result.CanApprove = false;
            }
            return result;
        }
        public List<DTO.ModelMng.CheckListGroupDTO> QuyeryCheckList(int id, string querycheck, System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QuyeryCheckList(param, out notification);
        }
        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete Model " + id.ToString());

            // query data            
            return factory.DeleteData(id, out notification);
        }

        public bool Restore(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data            
            return factory.Restore(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ModelMng.Model dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update Model " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            dtoItem.CreatedBy = iRequesterID;
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            if (fwFactory.CheckModelPricePermission(iRequesterID) <= 0)
            {
                // clear pricing data if not allow
                dtoItem.ModelPriceConfigurations = null;
                dtoItem.ModelFixPriceConfigurations = null;
            }

            return factory.UpdateData(id, ref dtoItem, iRequesterID, out notification);
        }
        public bool ApproveData(int userId, int id,int packagingID, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(userId, id,packagingID, ref dtoItem,out notification);
        }
        public bool ResetData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Reset(userId, id, ref dtoItem, out notification);
        }  
        public override bool Approve(int id, ref DTO.ModelMng.Model dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ModelMng.Model dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ModelMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }

        public DTO.ModelMng.EditFormData2 GetData2(int id, int mdlId, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get model issue tracking " + id.ToString());
            return factory.GetData2(iRequesterID, id, mdlId, out notification);
        }

        public bool UpdateDetailTracking(int id, int idIssue, ref DTO.ModelMng.ModelIssueTrack dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "update Model Issue Tracking" + id.ToString());
            return factory.UpdateDetailTracking(iRequesterID, id, idIssue, ref dtoItem, out notification);
        }

        public bool DeleteDetailTracking(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "delete Model " + id.ToString());
            return factory.DeleteDetailTracking(id, out notification);
        }

        public List<DTO.ModelMng.ModelDefaultFactoryDetail> GetModelDefaultFactoryDetail(int modelID, int factoryID, out Library.DTO.Notification notification)
        {
            return factory.GetModelDefaultFactoryDetail(modelID, factoryID, out notification);
        }

        public DTO.ModelMng.EditFormData GetSampleProductData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetSampleProductData(id, out notification);
        }

        public DTO.ModelMng.EditFormData GetProductData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetProductData(id, out notification);
        }

        public  DTO.ModelMng.EditFormData GetProductBreakDown(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetProductBreakDown(userId, id, out notification);
        }

        public string ExportExcelModel(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcelModel(filters, out notification);
        }

        #region Create Model

        public object GetInitDataCreateModel(int userId, out Notification notification)
        {
            return factory.GetInitDataCreateModel(userId, out notification);
        }

        public object GetDataCreateModel(int userId, int id, int sampleProductID, out Notification notification)
        {
            DTO.ModelMng.CreateModelEditData result = factory.GetDataCreateModel(userId, id, sampleProductID, out notification);

            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

            if (fwFactory.CheckModelPricePermission(userId) <= 0)
            {
                // clear pricing data if not allow
                result.Data.ModelFixPriceConfigurations.Clear();
                result.Data.ModelPriceConfigurations.Clear();
                result.ModelPriceConfigurationDefault.Clear();
                result.IsPriceEnabled = false;
            }
            else
            {
                result.IsPriceEnabled = true;
            }

            if (fwFactory.CanPerformAction(userId, "ModelMng", Library.DTO.ModuleAction.CanApprove))
            {
                result.CanApprove = true;
            }
            else
            {
                result.CanApprove = false;
            }

            return result;
        }

        #endregion

        #region Get model default option by season
        public List<ModelDefaultOptionDTO> GetDefaultOptionBySeason(int userId,int id , string season, out Notification notification)
        {
            return factory.GetDefaultOptionBySeason(userId, id, season, out notification);
        }
        #endregion

        #region Get model default option by Previous season
        public List<ModelDefaultOptionDTO> GetDefaultOptionByPreviousSeason(int userId, int id, string season, out Notification notification)
        {
            return factory.GetDefaultOptionByPreviousSeason(userId, id, season, out notification);
        }
        #endregion

        public List<ClientSaleData> GetClientSaleData(string season, int clientID, int modelID, out Notification notification)
        {
            return factory.GetClientSaleData(season, clientID, modelID, out notification);
        }
        public bool SelfCalculationPrice(int userId, int id, string season, ref List<DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO> dtoItem, out Notification notification)
        {
            return factory.SelfCalculationPrice(userId, id, season, ref dtoItem, out notification);
        }

        public List<Model> GetModelNoFactory(int userId, out Notification notification)
        {
            return factory.GetModelNoFactory(userId, out notification);
        }
    }
}