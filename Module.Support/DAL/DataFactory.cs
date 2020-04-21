using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Module.Support.DTO;
using Library.DTO;
using Library;

namespace Module.Support.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private SupportEntities CreateContext()
        {
            return new SupportEntities(Library.Helper.CreateEntityConnectionString("DAL.SupportModel"));
        }

        public List<DTO.WorkOrderType> GetWorkOrderType()
        {
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_WorkOrderType_View.ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_WorkOrderType_View>, List<DTO.WorkOrderType>>(dbItems);
            }
        }

        public List<DTO.EmployeeDepartmentDTO> GetDepartmentDTOs()
        {
            List<DTO.EmployeeDepartmentDTO> result = new List<DTO.EmployeeDepartmentDTO>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_EmployeeDeparment(context.AgendaMng_Employee_View.ToList());
                }
            }
            catch { }
            return result;
        }


        public List<DTO.WorkOrderStatus> GetWorkOrderStatus()
        {
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_WorkOrderStatus_View.ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_WorkOrderStatus_View>, List<DTO.WorkOrderStatus>>(dbItems);
            }
        }

        public List<DTO.WorkCenter> GetWorkCenter()
        {
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_WorkCenter_View.ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_WorkCenter_View>, List<DTO.WorkCenter>>(dbItems);
            }
        }

        public List<DTO.TransportCostChargeType> GetTransportCostChargeType()
        {
            List<DTO.TransportCostType> result = new List<TransportCostType>();
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_TransportCostChargeType_View.ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_TransportCostChargeType_View>, List<DTO.TransportCostChargeType>>(dbItems);
            }
        }

        public List<DTO.WeekSeason> GetWeekInSeason(string season)
        {
            List<WeekSeason> result = new List<WeekSeason>();
            string startDateText = season.Substring(0, 4) + "-09-01";
            string endDateText = season.Substring(5, 4) + "-08-31";

            System.Globalization.DateTimeFormatInfo dfi = (new System.Globalization.CultureInfo("nl-NL")).DateTimeFormat;
            System.Globalization.Calendar calendar = dfi.Calendar;
            DateTime startingDate = Convert.ToDateTime(startDateText);
            DateTime endingDate = Convert.ToDateTime(endDateText);

            DTO.WeekSeason weekSeasonItem;
            for (DateTime date = startingDate; date <= endingDate; date = date.AddDays(1))
            {
                int weekNo = calendar.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                if (!result.Select(o => o.WeekNo).ToList().Contains(weekNo))
                {
                    weekSeasonItem = new WeekSeason();
                    weekSeasonItem.WeekNo = weekNo;
                    weekSeasonItem.StartDate = date.ToString("dd/MM/yyyy");
                    weekSeasonItem.EndDate = date.AddDays(6).ToString("dd/MM/yyyy");
                    result.Add(weekSeasonItem);
                }
            }
            return result;
        }

        public List<DTO.TransportCostType> GetTransportCostType()
        {
            List<DTO.TransportCostType> result = new List<TransportCostType>();
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_TransportCostType_View.ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_TransportCostType_View>, List<DTO.TransportCostType>>(dbItems);
            }
        }

        public List<DTO.Currency> GetCurrency()
        {
            List<DTO.Currency> Currencies = new List<Currency>();
            Currencies.Add(new DTO.Currency() { CurrencyID = "USD", CurrencyNM = "USD" });
            Currencies.Add(new DTO.Currency() { CurrencyID = "EUR", CurrencyNM = "EUR" });
            return Currencies;
        }

        public List<DTO.Currency> GetCurrency2()
        {
            List<DTO.Currency> Currencies = new List<Currency>();
            Currencies.Add(new DTO.Currency() { CurrencyID = "USD", CurrencyNM = "USD" });
            Currencies.Add(new DTO.Currency() { CurrencyID = "EUR", CurrencyNM = "EUR" });
            Currencies.Add(new DTO.Currency() { CurrencyID = "VND", CurrencyNM = "VND" });
            return Currencies;
        }

        public List<DTO.ProductionItemType> GetProductionItemType()
        {
            List<DTO.ProductionItemType> result = new List<ProductionItemType>();
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_ProductionItemType_View.ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionItemType_View>, List<DTO.ProductionItemType>>(dbItems);
            }
        }

        public List<DTO.OPSequenceDetail> GetOPSequenceDetail(int? OPSequenceID)
        {
            List<DTO.OPSequenceDetail> result = new List<OPSequenceDetail>();
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_OPSequenceDetail_View.Where(o => o.OPSequenceID == (OPSequenceID.HasValue ? OPSequenceID.Value : o.OPSequenceID)).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_OPSequenceDetail_View>, List<DTO.OPSequenceDetail>>(dbItems);
            }
        }

        public List<OPSequence> GetOPSequence(int? companyID)
        {
            List<OPSequence> listOPSequence = new List<OPSequence>();
            using (var context = CreateContext())
            {
                var dbOPSequences = context.SupportMng_OPSequence_View.Where(o => o.CompanyID == companyID).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_OPSequence_View>, List<OPSequence>>(dbOPSequences);
            }
        }

        public List<DTO.ProductionTeam> GetProductionTeam(int? companyID)
        {
            List<DTO.ProductionTeam> result = new List<ProductionTeam>();
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_ProductionTeam_View.Where(o => o.CompanyID == companyID).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionTeam_View>, List<DTO.ProductionTeam>>(dbItems);
            }
        }

        public List<DTO.FactoryWarehousePallet> GetFactoryWarehousePallet(int? companyID)
        {
            List<DTO.FactoryWarehousePallet> result = new List<FactoryWarehousePallet>();
            using (SupportEntities context = this.CreateContext())
            {
                var dbItems = context.SupportMng_FactoryWarehousePallet_View.Where(o => o.CompanyID == companyID).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_FactoryWarehousePallet_View>, List<DTO.FactoryWarehousePallet>>(dbItems);
            }
        }

        public List<DTO.ProductionItem> GetProductionItem(int userId, Hashtable filters)
        {
            List<DTO.ProductionItem> result = new List<ProductionItem>();
            using (SupportEntities context = this.CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                string searchQuery = filters["searchQuery"].ToString();
                var dbItems = context.SupportMng_ProductionItem_View.Where(o => o.CompanyID == companyID && (o.ProductionItemNM.Contains(searchQuery) || o.ProductionItemUD.Contains(searchQuery))).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
            }
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromTeamToTeam(int userId, Hashtable filters)
        {
            List<DTO.ProductionItem> result = new List<ProductionItem>();
            using (SupportEntities context = this.CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                string searchQuery = filters["searchQuery"].ToString();
                string workOrderIDs = filters["workOrderIDs"].ToString();
                int? workCenterID = null;
                if (filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }
                var dbItems = context.SupportMng_function_GetProductionItemToDeliveryFromTeamToTeam(companyID, searchQuery, workOrderIDs, workCenterID).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
            }
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromWarehouseToTeam(int userId, Hashtable filters)
        {
            List<DTO.ProductionItem> result = new List<ProductionItem>();
            using (SupportEntities context = this.CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                string searchQuery = filters["searchQuery"].ToString();
                string workOrderIDs = filters["workOrderIDs"].ToString();
                int? workCenterID = null;
                if (filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }
                var dbItems = context.SupportMng_function_GetProductionItemToDeliveryFromWarehouseToTeam(companyID, searchQuery, workOrderIDs, workCenterID).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
            }
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromTeamToTeamToAmend(int userId, Hashtable filters)
        {
            List<DTO.ProductionItem> result = new List<ProductionItem>();
            using (SupportEntities context = this.CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                string searchQuery = filters["searchQuery"].ToString();
                string workOrderIDs = filters["workOrderIDs"].ToString();
                int? workCenterID = null;
                if (filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }
                var dbItems = context.SupportMng_function_GetProductionItemToDeliveryFromTeamToTeamToAmend(companyID, searchQuery, workOrderIDs, workCenterID).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
            }
        }

        public List<DTO.ProductionItem> GetProductionItemToDeliveryFromWarehouseToTeamToAmend(int userId, Hashtable filters)
        {
            List<DTO.ProductionItem> result = new List<ProductionItem>();
            using (SupportEntities context = this.CreateContext())
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                string searchQuery = filters["searchQuery"].ToString();
                string workOrderIDs = filters["workOrderIDs"].ToString();
                int? workCenterID = null;
                if (filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }
                var dbItems = context.SupportMng_function_GetProductionItemToDeliveryFromWarehouseToTeamToAmend(companyID, searchQuery, workOrderIDs, workCenterID).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_ProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
            }
        }

        public string GetSettingValue(string season, string settingKey)
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    if (!string.IsNullOrEmpty(season))
                    {
                        return context.SupportMng_SystemSetting_View.FirstOrDefault(o => o.Season == season && o.SettingKey == settingKey).SettingValue;
                    }
                    else
                    {
                        return context.SupportMng_SystemSetting_View.FirstOrDefault(o => o.SettingKey == settingKey).SettingValue;
                    }

                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public List<DTO.Season> GetSeason()
        {
            List<DTO.Season> result = new List<DTO.Season>();
            for (int i = 2006; i <= DateTime.Now.Year + 1; i++)
            {
                result.Add(new DTO.Season() { SeasonValue = i.ToString() + "/" + (i + 1).ToString(), SeasonText = i.ToString() + "/" + (i + 1).ToString() });
            }
            result = result.OrderByDescending(o => o.SeasonValue).ToList();

            return result;
        }

        public List<DTO.YesNo> GetYesNo()
        {
            List<DTO.YesNo> result = new List<DTO.YesNo>
            {
                new DTO.YesNo() { YesNoText = "Yes", YesNoValue = "true" },
                new DTO.YesNo() { YesNoText = "No", YesNoValue = "false" }
            };

            return result;
        }

        public List<DTO.Client> GetClient()
        {
            List<DTO.Client> result;
            using (SupportEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_Client(context.SupportMng_Client_View.ToList());
            }
            return result;
        }

        public List<DTO.Client> GetClient(Hashtable filters)
        {
            List<DTO.Client> result = new List<Client>();
            using (SupportEntities context = this.CreateContext())
            {
                string searchQuery = filters["searchQuery"].ToString();
                var dbItems = context.SupportMng_Client_View.Where(o => o.ClientUD.Contains(searchQuery)).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_Client_View>, List<DTO.Client>>(dbItems);
            }
        }

        public List<DTO.FactoryOrderDetail> GetFactoryOrderDetail(int userID, Hashtable filters)
        {
            List<DTO.FactoryOrderDetail> result = new List<FactoryOrderDetail>();
            using (SupportEntities context = this.CreateContext())
            {
                string searchQuery = filters["searchQuery"].ToString();
                int clientID = Convert.ToInt32(filters["clientID"]);
                var dbItems = context.SupportMng_function_FactoryOrderDetail(userID, clientID, searchQuery).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_FactoryOrderDetail_View>, List<DTO.FactoryOrderDetail>>(dbItems);
            }
        }

        public List<SampleProduct> GetSampleProduct(Hashtable filters)
        {
            List<SampleProduct> result = new List<SampleProduct>();
            using (SupportEntities context = this.CreateContext())
            {
                string searchQuery = filters["searchQuery"].ToString();
                int clientID = Convert.ToInt32(filters["clientID"]);
                var dbItems = context.SupportMng_SampleProduct_View.Where(o => o.ClientID == clientID && (o.ArticleDescription.Contains(searchQuery) || o.SampleOrderUD.Contains(searchQuery))).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_SampleProduct_View>, List<SampleProduct>>(dbItems);
            }
        }

        public List<Model> GetModel(Hashtable filters)
        {
            List<Model> result = new List<Model>();
            using (SupportEntities context = this.CreateContext())
            {
                string searchQuery = filters["searchQuery"].ToString();
                var dbItems = context.SupportMng_Model_View.Where(o => o.ModelUD.Contains(searchQuery) || o.ModelNM.Contains(searchQuery)).ToList();
                return AutoMapper.Mapper.Map<List<SupportMng_Model_View>, List<Model>>(dbItems);
            }
        }

        public List<DTO.ClientPaymentMethod> GetClientPaymentMethod()
        {
            List<DTO.ClientPaymentMethod> result;
            using (SupportEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_ClientPaymentMethod(context.SupportMng_ClientPaymentMethod_View.ToList());
            }
            return result;
        }

        public List<DTO.Factory> GetFactory()
        {
            List<DTO.Factory> result;
            using (SupportEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_Factory(context.List_Factory.ToList());
            }
            return result;
        }

        public List<DTO.Factory> GetFactory(int userId)
        {
            List<DTO.Factory> result = new List<DTO.Factory>();
            using (SupportEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_AccessibleFactory(context.SupportMng_AccessibleFactoy_View.Where(o => o.UserID == userId).ToList());
            }
            return result;
        }

        public List<DTO.FrameMaterial> GetFrameMaterial()
        {
            List<DTO.FrameMaterial> result;
            using (SupportEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_FrameMaterial(context.List_FrameMaterial_View.ToList());
            }
            return result;
        }

        //factory material unit
        public List<DTO.Unit> GetUnit(int? unitTypeID)
        {
            using (SupportEntities context = this.CreateContext())
            {
                if (unitTypeID == null) return converter.DB2DTO_Unit(context.SupportMng_Unit_View.ToList());
                else return converter.DB2DTO_Unit(context.SupportMng_Unit_View.Where(o => o.UnitTypeID == unitTypeID).ToList());
            }
        }

        //factory material group
        public List<DTO.FactoryMaterialGroup> GetFactoryMaterialGroup()
        {
            using (SupportEntities context = this.CreateContext())
            {
                return converter.DB2DTO_FactoryMaterialGroup(context.SupportMng_FactoryMaterialGroup_View.ToList());
            }
        }

        //factory material type
        public List<DTO.FactoryMaterialType> GetFactoryMaterialType()
        {
            using (SupportEntities context = this.CreateContext())
            {
                return converter.DB2DTO_FactoryMaterialType(context.SupportMng_FactoryMaterialType_View.ToList());
            }
        }

        //factory material type
        public List<DTO.FactoryMaterialColor> GetFactoryMaterialColor()
        {
            using (SupportEntities context = this.CreateContext())
            {
                return converter.DB2DTO_FactoryMaterialColor(context.SupportMng_FactoryMaterialColor_View.ToList());
            }
        }

        //quick search factory material
        public DTO.FactoryMaterials QuickSearchFactoryMaterial(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.FactoryMaterials Data = new DTO.FactoryMaterials();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //search info
            string searchQuery = string.Empty;
            if (filters.ContainsKey("searchQuery")) searchQuery = filters["searchQuery"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (SupportEntities context = CreateContext())
                {
                    Data.TotalRows = context.SupportMng_function_QuickSearchFactoryMaterial(sortedBy, sortedDirection, searchQuery).Count();
                    var result = context.SupportMng_function_QuickSearchFactoryMaterial(sortedBy, sortedDirection, searchQuery);
                    Data.Data = converter.DB2DTO_FactoryMaterial(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    foreach (var item in Data.Data)
                    {
                        item.FactoryMaterialImages = converter.DB2DTO_FactoryMaterialImage(context.SupportMng_FactoryMaterialImage_View.Where(o => o.FactoryMaterialID == item.FactoryMaterialID).ToList());
                    }
                }
                return Data;
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
                return Data;
            }
        }

        //quick search factory finished product
        public DTO.FactoryFinishedProducts QuickSearchFactoryFinishedProduct(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.FactoryFinishedProducts Data = new DTO.FactoryFinishedProducts();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //search info
            string searchQuery = string.Empty;
            if (filters.ContainsKey("searchQuery")) searchQuery = filters["searchQuery"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (SupportEntities context = CreateContext())
                {
                    Data.TotalRows = context.SupportMng_function_QuickSearchFactoryFinishedProduct(sortedBy, sortedDirection, searchQuery).Count();
                    var result = context.SupportMng_function_QuickSearchFactoryFinishedProduct(sortedBy, sortedDirection, searchQuery);
                    Data.Data = converter.DB2DTO_FactoryFinishedProduct(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return Data;
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
                return Data;
            }
        }

        //quick search factory order
        public DTO.FactoryOrders QuickSearchFactoryOrder(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.FactoryOrders data = new DTO.FactoryOrders();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string searchQuery = string.Empty;
            if (filters.ContainsKey("searchQuery")) searchQuery = filters["searchQuery"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (SupportEntities context = CreateContext())
                {
                    totalRows = context.SupportMng_function_QuickSearchFactoryOrder(sortedBy, sortedDirection, searchQuery).Count();
                    var result = context.SupportMng_function_QuickSearchFactoryOrder(sortedBy, sortedDirection, searchQuery);
                    data.Data = converter.DB2DTO_FactoryOrder(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
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
                return data;
            }
        }

        // quotation status
        public List<DTO.QuotationStatus> GetQuotationStatus()
        {
            List<DTO.QuotationStatus> result = new List<DTO.QuotationStatus>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_QUOTATION_STATUS);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_QuotationStatus(context.SupportMng_QuotationStatus_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_QUOTATION_STATUS, result);
                }
            }
            else
            {
                result = (List<DTO.QuotationStatus>)cacheObject;
            }

            return result;
        }

        //factory team
        public List<DTO.FactoryTeam> GetFactoryTeam()
        {
            using (SupportEntities context = this.CreateContext())
            {
                //get team
                List<DTO.FactoryTeam> teams = new List<DTO.FactoryTeam>();
                teams = converter.DB2DTO_FactoryTeam(context.SupportMng_FactoryTeam_View.ToList());

                //get goods procedure
                List<DTO.FactoryGoodsProcedureDetail> goodsProcedure = converter.DB2DTO_FactoryGoodsProcedureDetail(context.SupportMng_FactoryGoodsProcedureDetail_View.ToList());
                DTO.FactoryGoodsProcedureDetail xItem;

                //assign goods procedure for every step
                foreach (var teamItem in teams)
                {
                    foreach (var stepItem in teamItem.FactoryTeamSteps)
                    {
                        stepItem.FactoryGoodsProcedureDetails = new List<DTO.FactoryGoodsProcedureDetail>();
                        foreach (var procedureItem in goodsProcedure.Where(o => o.FactoryStepID == stepItem.FactoryStepID))
                        {
                            xItem = new DTO.FactoryGoodsProcedureDetail
                            {
                                FactoryGoodsProcedureDetailID = procedureItem.FactoryGoodsProcedureDetailID,
                                FactoryGoodsProcedureID = procedureItem.FactoryGoodsProcedureID,
                                FactoryStepID = procedureItem.FactoryStepID,
                                StepIndex = procedureItem.StepIndex,
                                FactoryGoodsProcedureNM = procedureItem.FactoryGoodsProcedureNM,
                                FactoryStepNM = procedureItem.FactoryStepNM
                            };
                            stepItem.FactoryGoodsProcedureDetails.Add(xItem);
                        }
                    }
                }
                return teams;
            }
        }

        //factory Area
        public List<DTO.FactoryArea> GetFactoryArea()
        {
            using (SupportEntities context = this.CreateContext())
            {
                return converter.DB2DTO_FactoryArea(context.SupportMng_FactoryArea_View.ToList());
            }
        }

        //factory step
        public List<DTO.FactoryStep> GetFactoryStep()
        {
            using (SupportEntities context = this.CreateContext())
            {
                List<DTO.FactoryStep> steps = converter.DB2DTO_FactoryStep(context.SupportMng_FactoryStep_View.ToList());
                List<DTO.FactoryGoodsProcedureDetail> goodsProcedure = converter.DB2DTO_FactoryGoodsProcedureDetail(context.SupportMng_FactoryGoodsProcedureDetail_View.ToList());
                DTO.FactoryGoodsProcedureDetail xItem;
                foreach (var stepItem in steps)
                {
                    stepItem.FactoryGoodsProcedureDetails = new List<DTO.FactoryGoodsProcedureDetail>();
                    foreach (var procedureItem in goodsProcedure.Where(o => o.FactoryStepID == stepItem.FactoryStepID))
                    {
                        xItem = new DTO.FactoryGoodsProcedureDetail
                        {
                            FactoryGoodsProcedureDetailID = procedureItem.FactoryGoodsProcedureDetailID,
                            FactoryGoodsProcedureID = procedureItem.FactoryGoodsProcedureID,
                            FactoryStepID = procedureItem.FactoryStepID,
                            StepIndex = procedureItem.StepIndex,
                            FactoryGoodsProcedureNM = procedureItem.FactoryGoodsProcedureNM,
                            FactoryStepNM = procedureItem.FactoryStepNM
                        };
                        stepItem.FactoryGoodsProcedureDetails.Add(xItem);
                    }
                }
                return steps;
            }
        }

        // plc image type
        public List<DTO.PLCImageType> GetPLCImageType()
        {
            List<DTO.PLCImageType> result = new List<DTO.PLCImageType>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_PLC_IMAGE_TYPE);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_PLCImageType(context.SupportMng_PLCImageType_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_PLC_IMAGE_TYPE, result);
                }
            }
            else
            {
                result = (List<DTO.PLCImageType>)cacheObject;
            }

            return result;
        }

        // container type
        public List<DTO.ContainerType> GetContainerType()
        {
            List<DTO.ContainerType> result = new List<DTO.ContainerType>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_CONTAINER_TYPE);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_ContainerType(context.SupportMng_ContainerType_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_CONTAINER_TYPE, result);
                }
            }
            else
            {
                result = (List<DTO.ContainerType>)cacheObject;
            }

            return result;
        }

        // supplier
        public List<DTO.Supplier> GetSupplier(int userId)
        {
            List<DTO.Supplier> result = new List<DTO.Supplier>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_Supplier(context.SupportMng_function_GetAuthorizedSupplier(userId).ToList());
                }
            }
            catch { }
            return result;
        }
        public DTO.Supplier GetSupplierInfo(int supplierId)
        {
            DTO.Supplier result = new DTO.Supplier();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_Supplier(context.SupportMng_Supplier_View.FirstOrDefault(o => o.SupplierID == supplierId));
                }
            }
            catch { }
            return result;
        }
        public List<DTO.Supplier> GetSupplierList()
        {
            List<DTO.Supplier> result = new List<DTO.Supplier>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_Supplier(context.SupportMng_Supplier_View.ToList());
                }
            }
            catch { }
            return result;
        }
        // meeting location
        public List<DTO.MeetingLocation> GetMeetingLocation()
        {
            List<DTO.MeetingLocation> result = new List<DTO.MeetingLocation>();
            object cacheObject = Library.CacheHelper.GetData("SUPPORT_MEETING_LOCATION");
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_MeetingLocation(context.SupportMng_MeetingLocation_View.ToList());
                    Library.CacheHelper.SetData("SUPPORT_MEETING_LOCATION", result);
                }
            }
            else
            {
                result = (List<DTO.MeetingLocation>)cacheObject;
            }

            return result;
        }

        // get time range
        public List<string> GetTimeRange()
        {
            List<string> result = new List<string>();
            for (int i = 0; i <= 23; i++)
            {
                result.Add(Library.Common.Helper.formatIndex(i.ToString(), 2, "0") + ":00");
                result.Add(Library.Common.Helper.formatIndex(i.ToString(), 2, "0") + ":30");
            }

            return result;
        }

        // product type
        public List<DTO.ProductType> GetProductType()
        {
            List<DTO.ProductType> result = new List<DTO.ProductType>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_PRODUCT_TYPE);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_ProductType(context.SupportMng_ProductType_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_PRODUCT_TYPE, result);
                }
            }
            else
            {
                result = (List<DTO.ProductType>)cacheObject;
            }

            return result;
        }

        // product group
        public List<DTO.ProductGroup> GetProductGroup()
        {
            List<DTO.ProductGroup> result = new List<DTO.ProductGroup>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_PRODUCT_GROUP);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_ProductGroup(context.SupportMng_ProductGroup_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_PRODUCT_GROUP, result);
                }
            }
            else
            {
                result = (List<DTO.ProductGroup>)cacheObject;
            }

            return result;
        }

        // manufacturer country
        public List<DTO.ManufacturerCountry> GetManufacturerCountry()
        {
            List<DTO.ManufacturerCountry> result = new List<DTO.ManufacturerCountry>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_MANUFACTURER_COUNTRY);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_ManufacturerCountry(context.SupportMng_ManufacturerCountry_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_MANUFACTURER_COUNTRY, result);
                }
            }
            else
            {
                result = (List<DTO.ManufacturerCountry>)cacheObject;
            }

            return result;
        }

        // model status
        public List<DTO.ModelStatus> GetModelStatus()
        {
            List<DTO.ModelStatus> result = new List<DTO.ModelStatus>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_MODEL_STATUS);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_ModelStatus(context.SupportMng_ModelStatus_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_MODEL_STATUS, result);
                }
            }
            else
            {
                result = (List<DTO.ModelStatus>)cacheObject;
            }

            return result;
        }

        // product category
        public List<DTO.ProductCategory> GetProductCategory()
        {
            List<DTO.ProductCategory> result = new List<DTO.ProductCategory>();
            object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_PRODUCT_CATEGORY);
            if (cacheObject == null)
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_ProductCategory(context.SupportMng_ProductCategory_View.ToList());
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_PRODUCT_CATEGORY, result);
                }
            }
            else
            {
                result = (List<DTO.ProductCategory>)cacheObject;
            }

            return result;
        }

        // user
        public List<DTO.User> GetUsers()
        {
            List<DTO.User> result = new List<DTO.User>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_User(context.SupportMng_ActiveUser_View.ToList());
                }
            }
            catch { }
            return result;
        }


        // user 2
        public List<DTO.User2> GetUsers2()
        {
            List<DTO.User2> result = new List<DTO.User2>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_User2(context.SupportMng_User2_View.ToList());
                }
            }
            catch { }
            return result;
        }

        // product element
        public List<DTO.ProductElement> GetProductElement()
        {
            List<DTO.ProductElement> result = new List<DTO.ProductElement>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_PRODUCT_ELEMENT");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_ProductElement(context.SupportMng_ProductElement_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_PRODUCT_ELEMENT", result);
                    }
                }
                else
                {
                    result = (List<DTO.ProductElement>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // task status
        public List<DTO.TaskStatus> GetTaskStatus()
        {
            List<DTO.TaskStatus> result = new List<DTO.TaskStatus>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_TASK_STATUS);
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_TaskStatus(context.SupportMng_TaskStatus_View.ToList());
                        Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_TASK_STATUS, result);
                    }
                }
                else
                {
                    result = (List<DTO.TaskStatus>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // pol
        public List<DTO.POL> GetPOL()
        {
            List<DTO.POL> result = new List<DTO.POL>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_POL(context.SupportMng_POL_View.ToList());
                }
            }
            catch { }

            return result;
        }

        // POD
        public List<DTO.POD> GetPOD()
        {
            List<DTO.POD> result = new List<DTO.POD>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_POD(context.SupportMng_POD_View.ToList());
                }
            }
            catch { }

            return result;
        }

        // MOVEMENT TERM
        public List<DTO.MovementTerm> GetMovementTerm()
        {
            List<DTO.MovementTerm> result = new List<DTO.MovementTerm>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_MOVEMENT_TERM);
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_MovementTerm(context.SupportMng_MovementTerm_View.ToList());
                        Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_MOVEMENT_TERM, result);
                    }
                }
                else
                {
                    result = (List<DTO.MovementTerm>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // delivery term
        public List<DTO.DeliveryTerm> GetDeliveryTerm()
        {
            List<DTO.DeliveryTerm> result = new List<DTO.DeliveryTerm>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_DELIVERY_TERM);
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_DeliveryTerm(context.SupportMng_DeliveryTerm_View.ToList());
                        Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_DELIVERY_TERM, result);
                    }
                }
                else
                {
                    result = (List<DTO.DeliveryTerm>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // ocean freight
        public List<DTO.StringCollectionItem> GetOceanFreight()
        {
            List<DTO.StringCollectionItem> result = new List<DTO.StringCollectionItem>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_OCEAN_FREIGHT);
                if (cacheObject == null)
                {
                    result.Add(new DTO.StringCollectionItem() { ItemValue = "P", ItemText = "Prepaid" });
                    result.Add(new DTO.StringCollectionItem() { ItemValue = "C", ItemText = "Collect" });
                    Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_OCEAN_FREIGHT, result);
                }
                else
                {
                    result = (List<DTO.StringCollectionItem>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        //ledger account list
        public List<DTO.LedgerAccount> GetLedgerAccount()
        {
            List<DTO.LedgerAccount> result;
            using (SupportEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_LedgerAccount(context.SupportMng_LedgerAccount_View.ToList());
            }
            return result;
        }

        // forwarder
        public List<DTO.Forwarder> GetForwarder()
        {
            List<DTO.Forwarder> result = new List<DTO.Forwarder>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_Forwarder(context.SupportMng_Forwarder_View.ToList());
                }
            }
            catch { }
            return result;
        }

        //factory cost type
        public List<DTO.FactoryCostType> GetFactoryCostType()
        {
            using (SupportEntities context = this.CreateContext())
            {
                return converter.DB2DTO_FactoryCostType(context.SupportMng_FactoryCostType_View.ToList());
            }
        }

        //
        // QUICK SEARCH FUNCTION
        //
        public List<DTO.Client> QuickSearchClient(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var data = new List<DTO.Client>();

            try
            {
                var clients = GetClient()
                    .Where(o => (!string.IsNullOrEmpty(o.ClientUD) && o.ClientUD.Contains(query)) ||
                                (!string.IsNullOrEmpty(o.ClientNM) && o.ClientNM.Contains(query)))
                    .ToList();

                data.AddRange(clients.Select(dtoClient => new DTO.Client() { ClientID = dtoClient.ClientID, ClientUD = dtoClient.ClientUD }));
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.FactoryGoodsProcedure> GetFactoryGoodsProcedure()
        {
            using (SupportEntities context = this.CreateContext())
            {
                return converter.DB2DTO_FactoryGoodsProcedure(context.SupportMng_FactoryGoodsProcedure_View.ToList());
            }
        }

        public List<DTO.FactoryGoodsProcedureDetail> GetFactoryGoodsProcedureDetail()
        {
            using (SupportEntities context = this.CreateContext())
            {
                return converter.DB2DTO_FactoryGoodsProcedureDetail(context.SupportMng_FactoryGoodsProcedureDetail_View.ToList());
            }
        }

        public List<DTO.AVFSupplier> QuickSearchAVFSupplier(Hashtable filters, out Library.DTO.Notification notification)
        {
            List<DTO.AVFSupplier> data = new List<DTO.AVFSupplier>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string searchQuery = string.Empty;
            if (filters.ContainsKey("searchQuery")) searchQuery = filters["searchQuery"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (SupportEntities context = CreateContext())
                {
                    totalRows = context.SupportMng_function_QuickSearchAVFSupplier(sortedBy, sortedDirection, searchQuery).Count();
                    var result = context.SupportMng_function_QuickSearchAVFSupplier(sortedBy, sortedDirection, searchQuery);
                    data = converter.DB2DTO_AVFSupplier(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return data;
            }
        }

        // payment term
        public List<DTO.PaymentTerm> GetPaymentTerm()
        {
            List<DTO.PaymentTerm> result = new List<DTO.PaymentTerm>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.SUPPORT_PAYMENT_TERM);
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_PaymentTerm(context.SupportMng_PaymentTerm_View.ToList());
                        Library.CacheHelper.SetData(Library.CacheHelper.SUPPORT_PAYMENT_TERM, result);
                    }
                }
                else
                {
                    result = (List<DTO.PaymentTerm>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        public List<DTO.MaterialGroupType> GetMaterialGroupType()
        {
            List<DTO.MaterialGroupType> materialGroupTypes = new List<DTO.MaterialGroupType>
            {
                new DTO.MaterialGroupType { MaterialGroupTypeID = 1, MaterialGroupTypeNM = "BAN NGUYET" },
                new DTO.MaterialGroupType { MaterialGroupTypeID = 2, MaterialGroupTypeNM = "CAP DOI" }
            };
            return materialGroupTypes;
        }

        // internal company
        public List<DTO.InternalCompany> GetInternalCompany()
        {
            List<DTO.InternalCompany> result = new List<DTO.InternalCompany>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_INTERNAL_COMPANY");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_InternalCompany(context.SupportMng_InternalCompany_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_INTERNAL_COMPANY", result);
                    }
                }
                else
                {
                    result = (List<DTO.InternalCompany>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // internal department
        public List<DTO.InternalDepartment> GetInternalDepartment()
        {
            List<DTO.InternalDepartment> result = new List<DTO.InternalDepartment>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_INTERNAL_DEPARTMENT");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_InternalDepartment(context.SupportMng_InternalDepartment_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_INTERNAL_DEPARTMENT", result);
                    }
                }
                else
                {
                    result = (List<DTO.InternalDepartment>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // task role
        public List<DTO.TaskRole> GetTaskRole()
        {
            List<DTO.TaskRole> result = new List<DTO.TaskRole>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_TASK_ROLE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_TaskRole(context.SupportMng_TaskRole_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_TASK_ROLE", result);
                    }
                }
                else
                {
                    result = (List<DTO.TaskRole>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // job level
        public List<DTO.JobLevel> GetJobLevel()
        {
            List<DTO.JobLevel> result = new List<DTO.JobLevel>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_JOB_LEVEL");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_JobLevel(context.SupportMng_JobLevel_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_JOB_LEVEL", result);
                    }
                }
                else
                {
                    result = (List<DTO.JobLevel>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // LP status
        public List<DTO.LabelingPackagingStatus> GetLabelingPackagingStatus()
        {
            List<DTO.LabelingPackagingStatus> result = new List<DTO.LabelingPackagingStatus>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_LPSTATUS");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_LabelingPackagingStatus(context.SupportMng_LabelingPackaging_LPStatus_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_LPSTATUS", result);
                    }
                }
                else
                {
                    result = (List<DTO.LabelingPackagingStatus>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // location
        public List<DTO.Location> GetLocation()
        {
            List<DTO.Location> result = new List<DTO.Location>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_LOCATION");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_Location(context.SupportMng_Location_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_LOCATION", result);
                    }
                }
                else
                {
                    result = (List<DTO.Location>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // factory location
        public List<DTO.FactoryLocation> GetFactoryLocation()
        {
            List<DTO.FactoryLocation> result = new List<DTO.FactoryLocation>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_FACTORY_LOCATION");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_Location(context.SupportMng_FactoryLocation_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_FACTORY_LOCATION", result);
                    }
                }
                else
                {
                    result = (List<DTO.FactoryLocation>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // leave request time
        public List<DTO.LeaveRequestTime> GetLeaveRequestTime()
        {
            List<DTO.LeaveRequestTime> result = new List<DTO.LeaveRequestTime>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_LEAVE_REQUEST_TIME");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_LeaveRequestTime(context.SupportMng_LeaveRequestTime_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_LEAVE_REQUEST_TIME", result);
                    }
                }
                else
                {
                    result = (List<DTO.LeaveRequestTime>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // leave type
        public List<DTO.LeaveType> GetLeaveType()
        {
            List<DTO.LeaveType> result = new List<DTO.LeaveType>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_LEAVE_TYPE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_LeaveType(context.SupportMng_LeaveType_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_LEAVE_TYPE", result);
                    }
                }
                else
                {
                    result = (List<DTO.LeaveType>)cacheObject;
                }
            }
            catch { }

            return result;
        }
        public List<DTO.Employee> QuickSearchEmployee(Hashtable filters, out Library.DTO.Notification notification)
        {
            List<DTO.Employee> data = new List<DTO.Employee>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string searchQuery = string.Empty;
            if (filters.ContainsKey("searchQuery")) searchQuery = filters["searchQuery"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (SupportEntities context = CreateContext())
                {
                    totalRows = context.SupportMng_function_QuickSearchEmployee(sortedBy, sortedDirection, searchQuery).Count();
                    var result = context.SupportMng_function_QuickSearchEmployee(sortedBy, sortedDirection, searchQuery);
                    data = converter.DB2DTO_Employee(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return data;
            }
        }

        // sample order status
        public List<DTO.SampleOrderStatus> GetSampleOrderStatus()
        {
            List<DTO.SampleOrderStatus> result = new List<DTO.SampleOrderStatus>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_SAMPLE_ORDERSTATUS");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_SampleOrderStatus(context.SupportMng_SampleOrderStatus_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_SAMPLE_ORDERSTATUS", result);
                    }
                }
                else
                {
                    result = (List<DTO.SampleOrderStatus>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // sample transport type
        public List<DTO.SampleTransportType> GetSampleTransportType()
        {
            List<DTO.SampleTransportType> result = new List<DTO.SampleTransportType>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_SAMPLE_TRANSPORTTYPE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_SampleTransportType(context.SupportMng_SampleTransportType_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_SAMPLE_TRANSPORTTYPE", result);
                    }
                }
                else
                {
                    result = (List<DTO.SampleTransportType>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // sample purpose
        public List<DTO.SamplePurpose> GetSamplePurpose()
        {
            List<DTO.SamplePurpose> result = new List<DTO.SamplePurpose>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_SAMPLE_PURPOSE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_SamplePurpose(context.SupportMng_SamplePurpose_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_SAMPLE_PURPOSE", result);
                    }
                }
                else
                {
                    result = (List<DTO.SamplePurpose>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // sample request type
        public List<DTO.SampleRequestType> GetSampleRequestType()
        {
            List<DTO.SampleRequestType> result = new List<DTO.SampleRequestType>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_SAMPLE_REQUESTTYPE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_SampleRequestType(context.SupportMng_SampleRequestType_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_SAMPLE_REQUESTTYPE", result);
                    }
                }
                else
                {
                    result = (List<DTO.SampleRequestType>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // sample product status
        public List<DTO.SampleProductStatus> GetSampleProductStatus()
        {
            List<DTO.SampleProductStatus> result = new List<DTO.SampleProductStatus>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_SAMPLE_PRODUCT_STATUS");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_SampleProductStatus(context.SupportMng_SampleProductStatus_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_SAMPLE_PRODUCT_STATUS", result);
                    }
                }
                else
                {
                    result = (List<DTO.SampleProductStatus>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // QUICK SEARCH
        // client
        public List<DTO.QuickSearchResult> QuickSearchClient2(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<Support.DTO.QuickSearchResult> data = new List<Support.DTO.QuickSearchResult>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchClient(context.SupportMng_ClientSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return data;
        }

        // model
        public List<DTO.QuickSearchResult> QuickSearchModel(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchModel(context.SupportMng_ModelSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // material
        public List<DTO.QuickSearchResult> QuickSearchMaterial(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchMaterial(context.SupportMng_MaterialSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // material type
        public List<DTO.QuickSearchResult> QuickSearchMaterialType(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchMaterialType(context.SupportMng_MaterialTypeSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // material color
        public List<DTO.QuickSearchResult> QuickSearchMaterialColor(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchMaterialColor(context.SupportMng_MaterialColorSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // frame material
        public List<DTO.QuickSearchResult> QuickSearchFrameMaterial(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchFrameMaterial(context.SupportMng_FrameMaterialSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // frame material color
        public List<DTO.QuickSearchResult> QuickSearchFrameMaterialColor(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchFrameMaterialColor(context.SupportMng_FrameMaterialColorSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // sub material
        public List<DTO.QuickSearchResult> QuickSearchSubMaterial(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchSubMaterial(context.SupportMng_SubMaterialSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // sub material color
        public List<DTO.QuickSearchResult> QuickSearchSubMaterialColor(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchSubMaterialColor(context.SupportMng_SubMaterialColorSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // back cushion
        public List<DTO.QuickSearchResult> QuickSearchBackCushion(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchBackCushion(context.SupportMng_BackCushionSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // seat cushion
        public List<DTO.QuickSearchResult> QuickSearchSeatCushion(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchSeatCushion(context.SupportMng_SeatCushionSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // cushion color
        public List<DTO.QuickSearchResult> QuickSearchCushionColor(string query, out Library.DTO.Notification notification)
        {
            List<DTO.QuickSearchResult> data = new List<DTO.QuickSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchCushionColor(context.SupportMng_CushionColorSearchResult_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // client 3 - client with client name
        public List<DTO.QuickSearchResult> QuickSearchClient3(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<Support.DTO.QuickSearchResult> data = new List<Support.DTO.QuickSearchResult>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_QuickSearchClient2(context.SupportMng_ClientSearchResult2_View.Where(o => o.OptionUD.Contains(query) || o.OptionNM.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return data;
        }

        //
        // OTHER FUNCION
        //
        public DTO.ModelImage GetModelImage(int modelId, out Library.DTO.Notification notification)
        {
            DTO.ModelImage data = new DTO.ModelImage();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ModelImage(context.SupportMng_ModelImage_View.FirstOrDefault(o => o.ModelID == modelId));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        public DTO.ProductInfo GetProductInfo(string articleCode, out Library.DTO.Notification notification)
        {
            DTO.ProductInfo data = new DTO.ProductInfo();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ProductInfo(context.SupportMng_ProductInfo_View.FirstOrDefault(o => o.ArticleCode == articleCode));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        // Inspection type
        public List<DTO.InspectionType> GetInspectionType()
        {
            List<DTO.InspectionType> result = new List<DTO.InspectionType>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_INSPECTION_TYPE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_InspectionType(context.SupportMng_InspectionType_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_INSPECTION_TYPE", result);
                    }
                }
                else
                {
                    result = (List<DTO.InspectionType>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // Packaging type
        public List<DTO.PackagingType> GetPackagingType()
        {
            List<DTO.PackagingType> result = new List<DTO.PackagingType>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_PACKAGING_TYPE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_PackagingType(context.SupportMng_PackagingType_View.ToList());
                        Library.CacheHelper.SetData("SUPPORT_PACKAGING_TYPE", result);
                    }
                }
                else
                {
                    result = (List<DTO.PackagingType>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // office
        public List<DTO.Office> GetOffice()
        {
            List<DTO.Office> result = new List<DTO.Office>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_Office(context.SupportMng_Office_View.ToList());
                }
            }
            catch { }

            return result;
        }

        // user group
        public List<DTO.UserGroup> GetUserGroup()
        {
            List<DTO.UserGroup> result = new List<DTO.UserGroup>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_UserGroup(context.SupportMng_UserGroup_View.ToList());
                }
            }
            catch { }

            return result;
        }


        // dev request history action
        public List<DTO.DevRequestHistoryAction> GetDevRequestHistoryAction()
        {
            List<DTO.DevRequestHistoryAction> result = new List<DTO.DevRequestHistoryAction>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_DEV_REQUEST_HISTORY_ACTION");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_DevRequestHistoryAction(context.SupportMng_DevRequestHistoryAction_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_DEV_REQUEST_HISTORY_ACTION", result);
                    }
                }
                else
                {
                    result = (List<DTO.DevRequestHistoryAction>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // dev request priority
        public List<DTO.DevRequestPriority> GetDevRequestPriority()
        {
            List<DTO.DevRequestPriority> result = new List<DTO.DevRequestPriority>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_DEV_REQUEST_PRIORITY");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_DevRequestPriority(context.SupportMng_DevRequestPriority_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_DEV_REQUEST_PRIORITY", result);
                    }
                }
                else
                {
                    result = (List<DTO.DevRequestPriority>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // dev request project
        public List<DTO.DevRequestProject> GetDevRequestProject()
        {
            List<DTO.DevRequestProject> result = new List<DTO.DevRequestProject>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_DEV_REQUEST_PROJECT");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_DevRequestProject(context.SupportMng_DevRequestProject_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_DEV_REQUEST_PROJECT", result);
                    }
                }
                else
                {
                    result = (List<DTO.DevRequestProject>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // dev request status
        public List<DTO.DevRequestStatus> GetDevRequestStatus()
        {
            List<DTO.DevRequestStatus> result = new List<DTO.DevRequestStatus>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_DEV_REQUEST_STATUS");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_DevRequestStatus(context.SupportMng_DevRequestStatus_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_DEV_REQUEST_STATUS", result);
                    }
                }
                else
                {
                    result = (List<DTO.DevRequestStatus>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // dev request type
        public List<DTO.DevRequestType> GetDevRequestType()
        {
            List<DTO.DevRequestType> result = new List<DTO.DevRequestType>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_DEV_REQUEST_TYPE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_DevRequestType(context.SupportMng_DevRequestType_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_DEV_REQUEST_TYPE", result);
                    }
                }
                else
                {
                    result = (List<DTO.DevRequestType>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        // employee list
        public List<DTO.Employee> GetEmployee()
        {
            List<DTO.Employee> result = new List<DTO.Employee>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_Employee(context.SupportMng_Employee_View.ToList());
                }
            }
            catch { }

            return result;
        }

        // saler list
        public List<DTO.Saler> GetSaler()
        {
            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_Saler(context.SupportMng_Saler_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Saler>();
            }
        }

        public IEnumerable<DTO.TypeOfCost> GetTypeOfCosts()
        {
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_TypeOfCost(context.SupportMng_TypeOfCost_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.TypeOfCost>();
            }
        }

        public IEnumerable<DTO.TypeOfCharge> GetTypeOfCharges()
        {
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_TypeOfCharge(context.SupportMng_TypeOfCharge_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.TypeOfCharge>();
            }
        }

        public List<DTO.Forwarder> QuickSearchForwarder(Hashtable filters, ref int totalRows, out Library.DTO.Notification notification)
        {
            var data = new List<DTO.Forwarder>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            var forwarderUd = string.Empty;

            if (filters.ContainsKey("forwarderUD")) forwarderUd = filters["forwarderUD"].ToString();


            var pageSize = 20;
            var pageIndex = 1;
            var sortedBy = string.Empty;
            var sortedDirection = string.Empty;


            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (var context = CreateContext())
                {
                    totalRows = context.SupportMng_function_QuickSearchForwarder(sortedBy, sortedDirection, forwarderUd).Count();

                    var result = context.SupportMng_function_QuickSearchForwarder(sortedBy, sortedDirection, forwarderUd);
                    data = converter.DB2DTO_Forwarder(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return data;
            }
        }

        public List<DTO.Forwarder> QuickSearchForwarder(string query, out Library.DTO.Notification notification)
        {
            var data = new List<DTO.Forwarder>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_Forwarder(context.SupportMng_Forwarder_View.Where(o => o.ForwarderUD.Contains(query) || o.ForwarderUD.Contains(query)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }

        public List<DTO.TypeOfCurrency> GetTypeOfCurrency()
        {
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_TypeOfCurrency(context.SupportMng_TypeOfCurrency_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.TypeOfCurrency>();
            }
        }

        public List<DTO.ConstantEntry> GetConstantEntries(EntryGroupType entryGroupType)
        {
            var groupType = entryGroupType.GetStringValue();
            try
            {
                using (var context = CreateContext())
                {
                    return context.SupportMng_ConstantEntry_View
                        .Where(ce => ce.EntryGroup == groupType)

                        .Select(ce => new DTO.ConstantEntry()
                        {
                            EntryValue = ce.EntryValue ?? 0,
                            EntryText = ce.DisplayText,
                            EntryOrder = ce.DisplayOrder ?? 0
                        })
                        .OrderBy(ce => ce.EntryOrder)
                        .ToList();

                }
            }
            catch
            {
                return new List<DTO.ConstantEntry>();
            }
        }

        public List<DTO.FSCType> GetFSCType()
        {
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_FSCType(context.SupportMng_FSCType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.FSCType>();
            }
        }

        public List<DTO.PackagingMethod> GetPackagingMethod()
        {
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_PackagingMethod(context.PackagingMethodMng_PackagingMethod_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.PackagingMethod>();
            }
        }

        /// <summary>
        /// Get factory warehouse
        /// </summary>
        /// <param name="companyID">Company of login user</param>
        /// <returns></returns>
        public List<FactoryWarehouseDto> GetFactoryWarehouse(int? companyID)
        {
            try
            {
                using (var context = this.CreateContext())
                {
                    return this.converter.DB2DTO_FactoryWarehouse(context.SupportMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                }
            }
            catch
            {
                return new List<FactoryWarehouseDto>();
            }
        }

        public List<DTO.ReportTemplate> GetReportTemplate()
        {
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_ReportTemplate(context.SupportMng_ReportTemplate_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.ReportTemplate>();
            }
        }

        public List<DTO.PriceDifference> GetPriceDifference(string season)
        {
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_PriceDifference(context.SupportMng_PriceDifference2_View.Where(o => season.Equals(o.Season)).ToList());
                }
            }
            catch
            {
                return null;
            }
        }

        public List<DTO.QuickSearchResult2> GetClient2(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                string searchString = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString().Replace("'", "''"))) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    return converter.DB2DTO_Client2(context.SupportMng_function_Client2(searchString).ToList());
                }
            }
            catch
            {
                return null;
            }
        }

        public List<DTO.QuickSearchResult2> GetFactory2(int userId, Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                string searchString = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString().Replace("'", "''"))) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    return converter.DB2DTO_Factory2(context.SupportMng_function_Factory2(searchString, userId).ToList());
                }
            }
            catch
            {
                return null;
            }
        }

        //
        // get appointment attach file type
        //
        public List<DTO.AppointmentAttachedFileType> GetAppointmentAttachedFileType()
        {
            List<DTO.AppointmentAttachedFileType> result = new List<DTO.AppointmentAttachedFileType>();
            try
            {
                object cacheObject = Library.CacheHelper.GetData("SUPPORT_APPOINTMENT_ATTACHED_FILE_TYPE");
                if (cacheObject == null)
                {
                    using (SupportEntities context = this.CreateContext())
                    {
                        result = converter.DB2DTO_AppointmentAttachedFileType(context.SupportMng_AppointmentAttachedFileType_View.OrderBy(o => o.DisplayOrder).ToList());
                        Library.CacheHelper.SetData("SUPPORT_APPOINTMENT_ATTACHED_FILE_TYPE", result);
                    }
                }
                else
                {
                    result = (List<DTO.AppointmentAttachedFileType>)cacheObject;
                }
            }
            catch { }

            return result;
        }

        public List<DTO.NotificationMember> GetNotificationMember(string NotificationGroupCode)
        {
            List<DTO.NotificationMember> result = new List<DTO.NotificationMember>();
            try
            {
                using (SupportEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_NotificationMember(context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == NotificationGroupCode).ToList());
                }
            }
            catch { }

            return result;
        }

        public List<DTO.QuickSearchProductionItem> GetProductionItem2(int userId, string searchString, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };
            try
            {
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_QuickSearchProductionItem(context.SupportMng_function_GetProductionItem(searchString, userId).ToList());
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<DTO.QuickSearchProductionItem> GetProductionItemWithFilters(int iRequesterID, string searchString, int branchID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(iRequesterID);

                using (var context = CreateContext())
                {
                    var productionItems = context.SupportMng_ProductionItem_QuickSearch_View.Where(o => o.CompanyID == companyID && o.BranchID == branchID && (o.Value.Contains(searchString) || o.Label.Contains(searchString)));
                    return converter.DB2DTO_QuickSearchProductionItem(productionItems.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                return new List<DTO.QuickSearchProductionItem>();
            }
        }

        public List<DTO.WorkOrderApproved> GetWorkOrderApproved(int userId, string searchString, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.WorkOrderApproved> data = new List<DTO.WorkOrderApproved>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    var dbItem = context.SupportMng_WorkOrderApproved_View.Where(o => o.CompanyID == companyID && o.Label.Contains(searchString));
                    data = converter.DB2DTO_WorkOrderApproved(dbItem.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.PurchaseOrderApprove> GetPurchaseOrderApprove(int userId, string searchString, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.PurchaseOrderApprove> data = new List<PurchaseOrderApprove>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    var dbItem = context.SupportMng_PurchaseOrderApprove_View.Where(o => o.CompanyID == companyID && o.Label.Contains(searchString));
                    data = converter.DB2DTO_PurchaseOrderApprove(dbItem.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.Country> GetCountries(out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.Country> data = new List<Country>();

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_Country(context.SupportMng_Country_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;

                Exception ex_1 = Library.Helper.GetInnerException(ex);
                notification.Message = ex_1.Message;
            }

            return data;
        }

        public List<DTO.Model2> GetModel2(string searchQuery, out Notification notification)
        {
            List<DTO.Model2> data = new List<Model2>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_Model2(context.SupportMng_Model2_View.Where(o => o.Value.Contains(searchQuery) || o.Label.Contains(searchQuery)).ToList());
                }
            }
            catch (Exception ex)
            {
                Exception ex_1 = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = ex_1.Message;
            }

            return data;
        }

        public List<DTO.WorkOrder> GetWorkOrder(string searchQuery, out Notification notification)
        {
            List<DTO.WorkOrder> data = new List<WorkOrder>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_WorkOrder(context.SupportMng_WorkOrder_View.Where(o => o.Value.Contains(searchQuery) || o.Label.Contains(searchQuery)).ToList());
                }
            }
            catch (Exception ex)
            {
                Exception ex_1 = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = ex_1.Message;
            }

            return data;
        }

        public List<DTO.FactoryRawMaterialData> GetSubSupplier(out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_SubSupplier_View>, List<DTO.FactoryRawMaterialData>>(context.SupportMng_SubSupplier_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<FactoryRawMaterialData>();
            }
        }

        public object GetUserProfiles(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.UserProfileData> data = new List<UserProfileData>();

            try
            {
                string searchString = filters.ContainsKey("searchString") && filters["searchString"] != null && !string.IsNullOrEmpty(filters["searchString"].ToString().Trim()) ? filters["searchString"].ToString().Trim() : null;

                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    var dbItem = context.SupportMng_UserProfile_View.Where(s => s.CompanyID == companyID && s.Label.Contains(searchString));
                    data = converter.DB2DTO_UserProfile(dbItem.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }



    #region support get ConstantEntry

    public class StringValueAttribute : Attribute
    {

        public string StringValue { get; protected set; }

        public StringValueAttribute(string value)
        {
            StringValue = value;
        }


    }

    public static class StringExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            var type = value.GetType();

            // Get fieldinfo for this type
            var fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            var attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }

    public enum EntryGroupType
    {
        [StringValue("complaint-status")]
        ComplaintStatus = 1,
        [StringValue("complaint-type")]
        ComplaintType = 2,
        [StringValue("purchase-order-status")]
        PurchaseOrderStatus = 3,
        [StringValue("employee-responsible")]
        ResponsibleFor = 4
    }
    #endregion support get ConstantEntry
}
